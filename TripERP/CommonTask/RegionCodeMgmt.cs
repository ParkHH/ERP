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
    public partial class RegionCodeMgmt : Form
    {
        enum eRegionListDataGridView { KOR_NATN_NM, NATN_REGN_CD, NATN_REGN_NM};

        string NATN_CD;
        string KOR_NATN_NM;
        string NATN_REGN_CD;

        public RegionCodeMgmt()
        {
            InitializeComponent();
        }

        private void RegionCodeMgmt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchRegionCodeList();         // form load 시 내용 바로 출력
        }

        // 조회 콤보박스 초기 적재
        private void InitControls()
        {
            // 국가코드
            SearchNationCodeComboBox.Items.Clear();

            string query = "CALL SelectNationCodeTransactionPossibleList";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가목록정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 국가코드와 국가명을 콤보에 설정
                string NATN_CD = dataRow["NATN_CD"].ToString();
                string KOR_NATN_NM = dataRow["KOR_NATN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(KOR_NATN_NM, NATN_CD);
                SearchNationCodeComboBox.Items.Add(item);
                InputNationNameComboBox.Items.Add(item);
            }

            SearchNationCodeComboBox.SelectedIndex = -1;
            InputNationNameComboBox.SelectedIndex = -1;

            ResetInputFormField();
        }

        // 초기화버튼 클릭
        private void resetButton_Click_1(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 입력필드 초기화
        private void ResetInputFormField()
        {
            InputNationNameComboBox.Text = "";           
            InputRegionCodetextBox.Text = "";
            InputRegionNameTextBox.Text = "";            

            InitDataGridView();
        }

        // 그리드 초기화
        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = RegionListDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
        }

        // 검색 버튼 클릭
        private void searchRegionCodeListButton_Click(object sender, EventArgs e)
        {
            SearchRegionCodeList();
        }

        // 지역목록 테이블 조회
        private void SearchRegionCodeList()
        {
            RegionListDataGridView.Rows.Clear();            

            string NATN_CD = Utils.GetSelectedComboBoxItemValue(SearchNationCodeComboBox);
            string KOR_NATN_NM = SearchNationCodeComboBox.Text.Trim();            
            string NATN_REGN_CD = SearchRegionCodeTextBox.Text.Trim();
            string NATN_REGN_NM = SearchRegionNameTextBox.Text.Trim();
            string SCRN_SORT_ORD = "";
            
            string query = string.Format("CALL SelectRegionCodeList ('{0}', '{1}', '{2}')", NATN_CD, NATN_REGN_CD, NATN_REGN_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("지역목록정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                NATN_CD = datarow["NATN_CD"].ToString();
                NATN_REGN_CD = datarow["NATN_REGN_CD"].ToString();
                NATN_REGN_NM = datarow["NATN_REGN_NM"].ToString();
                //SCRN_SORT_ORD = datarow["SCRN_SORT_ORD"].ToString();

                string query2 = string.Format("CALL SelectNationCodeList4 ('{0}', '{1}')", NATN_CD, "");
                DataSet dataSet2 = DbHelper.SelectQuery(query2);
                KOR_NATN_NM = dataSet2.Tables[0].Rows[0]["KOR_NATN_NM"].ToString();

                RegionListDataGridView.Rows.Add(KOR_NATN_NM, NATN_REGN_CD, NATN_REGN_NM);
            }
            RegionListDataGridView.ClearSelection();
        }

        // 그리드 행 클릭
        private void RegionListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RegionListDataGridView.SelectedRows.Count == 0)
                return;

            string KOR_NATN_NM = RegionListDataGridView.SelectedRows[0].Cells[(int)eRegionListDataGridView.KOR_NATN_NM].Value.ToString();
            string NATN_REGN_CD = RegionListDataGridView.SelectedRows[0].Cells[(int)eRegionListDataGridView.NATN_REGN_CD].Value.ToString();
            string NATN_REGN_NM = RegionListDataGridView.SelectedRows[0].Cells[(int)eRegionListDataGridView.NATN_REGN_NM].Value.ToString();

            InputNationNameComboBox.Text = KOR_NATN_NM;
            InputRegionCodetextBox.Text = NATN_REGN_CD;
            InputRegionNameTextBox.Text = NATN_REGN_NM;            
        }

        private void RegionListDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = RegionListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && RegionListDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (RegionListDataGridView.SelectedRows.Count == 0)
                return;

            string KOR_NATN_NM = RegionListDataGridView.Rows[rowIndex].Cells[(int)eRegionListDataGridView.KOR_NATN_NM].Value.ToString();
            string NATN_REGN_CD = RegionListDataGridView.Rows[rowIndex].Cells[(int)eRegionListDataGridView.NATN_REGN_CD].Value.ToString();
            string NATN_REGN_NM = RegionListDataGridView.Rows[rowIndex].Cells[(int)eRegionListDataGridView.NATN_REGN_NM].Value.ToString();

            InputNationNameComboBox.Text = KOR_NATN_NM;
            InputRegionCodetextBox.Text = NATN_REGN_CD;
            InputRegionNameTextBox.Text = NATN_REGN_NM;            
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            string KOR_NATN_NM = InputNationNameComboBox.Text.Trim();                                     // 국가명

            // 국가명에 해당하는 국가코드 변수 저장
            string query = string.Format("CALL SelectNationCodeList4 ('{0}', '{1}')", "", KOR_NATN_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            NATN_CD = dataSet.Tables[0].Rows[0]["NATN_CD"].ToString();

            // 지역코드가 반영되어 있지 않은 경우 DB에 반영되어 있는 해당국가의 Max+1을 읽어 와서 해당 코드를 부여함.
            if (InputRegionCodetextBox.Text == null || InputRegionCodetextBox.Text == "")
            {
                string query2 = string.Format("CALL SelectMaxNatnRegnCd ('{0}')", NATN_CD);
                DataSet dataSet2 = DbHelper.SelectQuery(query2);
                if (dataSet2 == null || dataSet2.Tables.Count == 0)
                {
                    MessageBox.Show("지역목록정보를 읽을 수 없습니다.");
                }
                else
                {
                    NATN_REGN_CD = dataSet2.Tables[0].Rows[0]["NATN_REGN_CD"].ToString();
                }
            }
            else
            {
                NATN_REGN_CD = InputRegionCodetextBox.Text.Trim();                                    // 지역코드
            }
            string NATN_REGN_NM = InputRegionNameTextBox.Text.Trim();                                 // 지역명
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                           // 최초등록자ID

            // 입력값 유효성 검증
            /*
            if (CheckRequireItems() == false)
                return;
            */

            string query3 = string.Format("CALL InsertRegionCodeItem ('{0}', '{1}', '{2}', '{3}')",
                NATN_CD, NATN_REGN_CD, NATN_REGN_NM, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query3);
            if (retVal == -1)
            {
                MessageBox.Show("지역목록정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchRegionCodeList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("지역목록정보를 저장했습니다.");
            }
        }

        // 삭제 버튼 클릭
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (InputNationNameComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("국가명은 필수 입력항목입니다.");
                InputNationNameComboBox.Focus();
                return;
            }

            if (InputRegionCodetextBox.Text.Trim() == "")
            {
                MessageBox.Show("삭제대상 지역코드가 선택되지 않았습니다. 목록에서 삭제 대상을 클릭하세요.");
                RegionListDataGridView.Focus();
                return;
            }

            string NATN_CD = Utils.GetSelectedComboBoxItemValue(InputNationNameComboBox);
            string NATN_REGN_CD = InputRegionCodetextBox.Text.Trim();

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteRegionCode ('{0}', '{1}')", NATN_CD, NATN_REGN_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("지역코드를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchRegionCodeList();
                MessageBox.Show("선택한 항목을 삭제했습니다.");
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}