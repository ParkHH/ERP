using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;

namespace TripERP.CommonTask
{
    public partial class MenuInfoMgt : Form
    {
        enum eMenudataGridView
        {
            SCRN_CD,                   // 화면코드
            SCRN_NM,                   // 화면이름
            FORM_NM,                   // 폼이름
            USE_YN,                    // 사용여부            
            FRST_RGST_DTM,             // 최초등록시간
            FRST_RGTR_ID,              // 최초등록자ID
            FINL_MDFC_DTM,             // 최종수정시간
            FINL_MDFR_ID               // 최종수정자ID
        };

        public MenuInfoMgt()
        {
            InitializeComponent();
        }

        private void MenuInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchScreenMenuList();             // form load 시 내용 바로 출력
        }

        // 초기화
        private void InitControls()
        {
            string query = "";
            DataSet dataSet;

            // 화면코드, 화면명, 사용여부            
            SearchScreenNametextBox.Clear();
            SearchScreenCodecomboBox.Items.Clear();

            query = "CALL SelectSearchScreenCodeList()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("화면코드 정보를 가져올 수 없습니다.");
                return;
            }            

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 도시코드를 콤보에 설정
                string SCRN_CD = dataRow["SCRN_CD"].ToString();
                string SCRN_NM = dataRow["SCRN_NM"].ToString();
                ComboBoxItem item = new ComboBoxItem(SCRN_NM, SCRN_CD);
                SearchScreenCodecomboBox.Items.Add(item);
            }

            SearchScreenCodecomboBox.SelectedIndex = -1;

            ResetInputFormField();    // 그리드 스타일 초기화
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = MenudataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 입력필드 초기화
        private void ResetInputFormField()
        {
            MgtScreenCodetextBox.Text = "";
            MgtScreenNametextBox.Text = "";
            MgtFormNametextBox.Text = "";
            MgtYesNocomboBox.Text = "";
            MgtYesNocomboBox.Items.Clear();

            string query = "";
            DataSet dataSet;

            query = "CALL SelectCommoncodeList ('YN')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string USE_YN = dataRow["CD_VLID_VAL"].ToString();
                string USE_YN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(USE_YN_NM, USE_YN);
                MgtYesNocomboBox.Items.Add(item);
            }

            MgtYesNocomboBox.SelectedIndex = -1;

            InitDataGridView();
        }

        private void searchMenuListButton_Click(object sender, EventArgs e)
        {
            SearchScreenMenuList();
        }

        // 화면목록 테이블 조회
        private void SearchScreenMenuList()
        {
            MenudataGridView.Rows.Clear();

            string SCRN_CD = Utils.GetSelectedComboBoxItemValue(SearchScreenCodecomboBox);
            string SCRN_NM = SearchScreenNametextBox.Text.Trim();
            string FORM_NM;
            string USE_YN;
            string FRST_RGTR_ID;            


            string query = string.Format("CALL SelectScreenCodeList ('{0}', '{1}')", SCRN_CD, SCRN_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("화면메뉴정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                SCRN_CD = datarow["SCRN_CD"].ToString();
                SCRN_NM = datarow["SCRN_NM"].ToString();
                FORM_NM = datarow["FORM_NM"].ToString();
                USE_YN = datarow["USE_YN"].ToString();                

                MenudataGridView.Rows.Add(SCRN_CD, SCRN_NM, FORM_NM, USE_YN);
            }
            MenudataGridView.ClearSelection();
        }

        private void MenudataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MenudataGridView.SelectedRows.Count == 0)
                return;

            string SCRN_CD = MenudataGridView.SelectedRows[0].Cells[(int)eMenudataGridView.SCRN_CD].Value.ToString();
            string SCRN_NM = MenudataGridView.SelectedRows[0].Cells[(int)eMenudataGridView.SCRN_NM].Value.ToString();
            string FORM_NM = MenudataGridView.SelectedRows[0].Cells[(int)eMenudataGridView.FORM_NM].Value.ToString();
            string USE_YN = MenudataGridView.SelectedRows[0].Cells[(int)eMenudataGridView.USE_YN].Value.ToString();

            MgtScreenCodetextBox.Text = SCRN_CD;
            MgtScreenNametextBox.Text = SCRN_NM;
            MgtFormNametextBox.Text = FORM_NM;
            MgtYesNocomboBox.Text = USE_YN;            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string SCRN_CD = MgtScreenCodetextBox.Text.Trim();                                               // 도시번호

            if (SCRN_CD == "")
            {
                MessageBox.Show("화면코드는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요.");
                return;
            }

            string query = string.Format("CALL DeleteScreenCodeInfo ('{0}')", SCRN_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("화면번호에 해당하는 화면을 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchScreenMenuList();
                MessageBox.Show("고객정보를 삭제했습니다.");
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string SCRN_CD = MgtScreenCodetextBox.Text.Trim();                                 // 화면코드
            string SCRN_NM = MgtScreenNametextBox.Text.Trim();                                 // 화면이름
            string FORM_NM = MgtFormNametextBox.Text.Trim();                                   // 폼이름
            string USE_YN = Utils.GetSelectedComboBoxItemValue(MgtYesNocomboBox);              // 사용여부            
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                    // 최초등록자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;

            query = string.Format("CALL InsertMenuInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}')",
                SCRN_CD, SCRN_NM, FORM_NM, USE_YN, FRST_RGTR_ID);

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("메뉴정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchScreenMenuList();                            // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("메뉴정보를 저장했습니다.");
            }
            ResetInputFormField();
        }

        
        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string SCRN_CD = MgtScreenCodetextBox.Text.Trim();                                // 화면코드
            string SCRN_NM = MgtScreenNametextBox.Text.Trim();                                // 화면이름
            //string FORM_NM = MgtFormNametextBox.Text.Trim();                                  // 폼이름           
            string USE_YN = Utils.GetSelectedComboBoxItemValue(MgtYesNocomboBox);             // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                   // 최초등록자ID

            if (SCRN_CD == "")
            {
                MessageBox.Show("화면코드는 필수 입력항목입니다.");
                MgtScreenCodetextBox.Focus();
                return false;
            }

            if (SCRN_NM == "")
            {
                MessageBox.Show("화면이름은 필수 입력항목입니다.");
                MgtScreenNametextBox.Focus();
                return false;
            }

            /*
            if (FORM_NM == "")
            {
                MessageBox.Show("폼이름은 필수 입력항목입니다.");
                MgtFormNametextBox.Focus();
                return false;
            }
            */

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
                MgtYesNocomboBox.Focus();
                return false;
            }

            return true;
        }

        private void MenuInfoGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = MenudataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && MenudataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (MenudataGridView.SelectedRows.Count == 0)
                return;

            string SCRN_CD = MenudataGridView.Rows[rowIndex].Cells[(int)eMenudataGridView.SCRN_CD].Value.ToString();
            string SCRN_NM = MenudataGridView.Rows[rowIndex].Cells[(int)eMenudataGridView.SCRN_NM].Value.ToString();
            string FORM_NM = MenudataGridView.Rows[rowIndex].Cells[(int)eMenudataGridView.FORM_NM].Value.ToString();
            string USE_YN = MenudataGridView.Rows[rowIndex].Cells[(int)eMenudataGridView.USE_YN].Value.ToString();

            MgtScreenCodetextBox.Text = SCRN_CD;
            MgtScreenNametextBox.Text = SCRN_NM;
            MgtFormNametextBox.Text = FORM_NM;
            MgtYesNocomboBox.Text = USE_YN;
        }
    }
}