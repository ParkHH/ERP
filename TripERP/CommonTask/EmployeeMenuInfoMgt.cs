using System;
using System.Data;
using System.Windows.Forms;
using TripERP.Common;
using System.Drawing;

namespace TripERP.CommonTask
{
    public partial class EmployeeMenuInfoMgt : Form
    {
        enum eEmpMenuInfodataGridView
        {
            SCRN_YN,                // 메뉴명 선택여부
            SCRN_CD,                // 화면코드
            SCRN_NM,                // 화면명       
        };

        bool SCRN_YN = false;                // 메뉴명 선택여부
        string SCRN_CD = "";                // 화면코드
        string SCRN_NM = "";                // 화면명 
        string EMPL_NO = "";                // 직원번호
        string EMPL_NM = "";                // 직원명
        string FRST_RGTR_ID = "";           // 최초등록자

        public EmployeeMenuInfoMgt()
        {
            InitializeComponent();
        }


        private void EmployeeMenuInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchMenuCodeList();                       // 메뉴 목록 바로 출력
        }

        // 초기화
        private void InitControls()
        {
            string query = "";
            DataSet dataSet;

            // 임직원코드, 임직원 성명
            SearchScreenCodecomboBox.Items.Clear();
            SearchEmployeeNamecomboBox.Items.Clear();
            SearchCodeScreenNameTextBox.Text = "";

            // 화면코드 정보조회 콤보박스 적재
            query = "CALL SelectSearchScreenCodeList ()";
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("화면코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 임직원 정보를 콤보에 설정
                string SCRN_CD = dataRow["SCRN_CD"].ToString();
                string SCRN_NM = dataRow["SCRN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(SCRN_NM, SCRN_CD);
                SearchScreenCodecomboBox.Items.Add(item);
            }

            SearchScreenCodecomboBox.SelectedIndex = -1;


            // 임직원 정보조회 콤보박스 적재
            query = "CALL SelectEmployeeCodeList ()";
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("임직원 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 임직원 정보를 콤보에 설정
                string EMPL_NO = dataRow["EMPL_NO"].ToString();
                string EMPL_NM = dataRow["EMPL_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(EMPL_NM, EMPL_NO);
                SearchEmployeeNamecomboBox.Items.Add(item);
            }

            SearchEmployeeNamecomboBox.SelectedIndex = -1;

            InitDataGridView();
        }

        private void InitDataGridView()
        {
            // 메뉴목록조회
            DataGridView dataGridView = CommonCodeListDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;     



            // 직원별 메뉴목록조회
            DataGridView dataGridView2 = EmployeeMenudataGridView;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.ColumnHeadersVisible = true;
        }

        private void searchCommonCodeListButton_Click(object sender, EventArgs e)
        {
            SearchMenuCodeList();
        }

        // 메뉴코드 리스트 조회
        private void SearchMenuCodeList()
        {
            CommonCodeListDataGridView.Rows.Clear();

            string SCRN_CD = Utils.GetSelectedComboBoxItemValue(SearchScreenCodecomboBox);
            string SCRN_NM = SearchCodeScreenNameTextBox.Text.Trim();

            string query = string.Format("CALL SelectScreenCodeList ('{0}', '{1}')", SCRN_CD, SCRN_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("메뉴 정보를 가져올 수 없습니다.");
                return;
            }
            
            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {                
                SCRN_CD = datarow["SCRN_CD"].ToString();
                SCRN_NM = datarow["SCRN_NM"].ToString();

                CommonCodeListDataGridView.Rows.Add(SCRN_YN, SCRN_CD, SCRN_NM);
            }
            CommonCodeListDataGridView.ClearSelection();
        }

        private void SearchEmployeeButton_Click(object sender, EventArgs e)
        {
            SearchEmployeeMenuCodeList();
        }

        private void SearchEmployeeMenuCodeList()
        {
            EmployeeMenudataGridView.Rows.Clear();

            if (SearchEmployeeNamecomboBox.Text.Trim() == null || SearchEmployeeNamecomboBox.Text.Trim() == "")
            {
                MessageBox.Show("반드시 직원을 선택하셔야 합니다.");
                return;
            }

            string EMPL_NO = Utils.GetSelectedComboBoxItemValue(SearchEmployeeNamecomboBox);            

            string query = string.Format("CALL SelectEmployeeMenuCodeList ('{0}')", EMPL_NO);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("메뉴 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {                
                EMPL_NO = datarow["EMPL_NO"].ToString();
                EMPL_NM = datarow["EMPL_NM"].ToString();
                SCRN_CD = datarow["SCRN_CD"].ToString();
                SCRN_NM = datarow["SCRN_NM"].ToString();

                EmployeeMenudataGridView.Rows.Add(SCRN_YN, EMPL_NO, EMPL_NM, SCRN_CD, SCRN_NM);
            }
            EmployeeMenudataGridView.ClearSelection();
        }

        // 선택된 메뉴 권한을 직원에게 부여함.
        private void InsertMenuToEmployeebutton_Click(object sender, EventArgs e)
        {
            InsertMenuToEmployee();
        }
        
        // 선택된 메뉴 권한을 직원에게 부여함.
        private void InsertMenuToEmployee()
        {
            int retVal;
            int count = 0;
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                    // 최초등록자ID

            for (int i = 0; i < CommonCodeListDataGridView.RowCount; i++)
            {
                if(CommonCodeListDataGridView[0, i].Value.Equals(true))
                {
                    SCRN_CD = CommonCodeListDataGridView[1, i].Value.ToString();
                    count = count + 1;
                }

                if (SearchEmployeeNamecomboBox.Equals(null))
                {
                    MessageBox.Show(string.Format("임직원을 선택하셔야합니다."));
                    return;
                }
                else
                {
                    EMPL_NO = Utils.GetSelectedComboBoxItemValue(SearchEmployeeNamecomboBox);          // 사번
                }

                string query = "";                

                query = string.Format("CALL InsertEmployeeMenuInfoItem ('{0}', '{1}', '{2}')", SCRN_CD, EMPL_NO, FRST_RGTR_ID);
                retVal = DbHelper.ExecuteNonQuery(query);                
            }

            MessageBox.Show(string.Format("총 {0} 건 저장", count));
            SearchEmployeeMenuCodeList();                            // 등록 후 그리드를 최신상태로 Refresh
            
            return;
        }

        private void DeleteMenuToEmployeeButton_Click(object sender, EventArgs e)
        {
            DeleteMenuToEmployee();
        }

        private void DeleteMenuToEmployee()
        {
            int retVal;
            int count = 0;

            for (int i = 0; i < EmployeeMenudataGridView.RowCount; i++)
            {
                if (EmployeeMenudataGridView[0, i].Value.Equals(true))
                {
                    EMPL_NO = EmployeeMenudataGridView[1, i].Value.ToString();
                    SCRN_CD = EmployeeMenudataGridView[3, i].Value.ToString();
                    count = count + 1;
                }                

                string query = "";

                query = string.Format("CALL DeleteEmployeeMenuInfoItem ('{0}', '{1}')", SCRN_CD, EMPL_NO);
                retVal = DbHelper.ExecuteNonQuery(query);
            }

            MessageBox.Show(string.Format("총 {0} 건 삭제", count));
            SearchEmployeeMenuCodeList();                            // 삭제 후 그리드를 최신상태로 Refresh

            return;
        }        

        
        private void CommonCodeListDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            /*
            // 목록 전체선택 Checkbox 생성
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                int nChkBoxWidth = 15;
                int nChkBoxHeight = 15;
                int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
                int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

                pt.X += offsetx;
                pt.Y += offsety;

                CheckBox cb = new CheckBox();
                cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                cb.Location = pt;
                cb.CheckedChanged += new EventHandler(gvSheetListCheckBox_CheckedChanged);

                ((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }            
            */
        }

        // 목록 전체선택 Checkbox 클릭
        private void gvSheetListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            DataGridView gridView = (DataGridView)cb.Parent;
            int rowCount = 0;       
            foreach (DataGridViewRow r in gridView.Rows)
            {                
                if(r.Cells[0].OwningColumn.Name.Equals("colCheck"))
                {
                    r.Cells["colCheck"].Value = cb.Checked;                    
                    rowCount = rowCount + 1;                    
                }

                else if (r.Cells[0].OwningColumn.Name.Equals("colCheck2"))
                {
                    r.Cells["colCheck2"].Value = cb.Checked;                    
                    rowCount = rowCount + 1;                    
                }
                
            }                        
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}