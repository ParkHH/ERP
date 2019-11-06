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
    public partial class NationCodeMgt : Form
    {
        enum eNationListDataGridView { NATN_CD, KOR_NATN_NM, ENG_NATN_NM, CUR_CD, TRAN_PSBL_YN_NM, SCRN_SORT_ORD, TRAN_PSBL_YN };

        public NationCodeMgt()
        {
            InitializeComponent();
        }

        private void NationCodeMgmt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchNationCodeList();             // form load 시 내용 바로 출력
        }

        private void InitControls()
        {
            // 국가코드
            SearchNationCodeComboBox.Items.Clear();

            string query = "CALL SelectAllNationList";
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
            }

            SearchNationCodeComboBox.SelectedIndex = -1;

            ResetInputFormField();
        }

        // 초기화버튼 클릭
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 입력필드 초기화
        private void ResetInputFormField()
        {
            NationCodeTextBox.Text = "";
            KorNationNameTextBox.Text = "";
            EngNationNameTextBox.Text = "";

            // 통화코드
            CurrencyCodeComboBox.Items.Clear();

            string query = "CALL SelectCurList";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("통화코드정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 통화코드와 통화명을 콤보에 설정
                string CUR_CD = dataRow["CUR_CD"].ToString();
                string CUR_NM = dataRow["CUR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CUR_NM, CUR_CD);
                CurrencyCodeComboBox.Items.Add(item);
            }

            CurrencyCodeComboBox.SelectedIndex = -1;

            // 거래가능사용여부
            TransactionPossibleYnComboBox.Items.Clear();

            query = "CALL SelectCommoncodeList ('YN')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용여부 공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string USE_YN = dataRow["CD_VLID_VAL"].ToString();
                string USE_YN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(USE_YN_NM, USE_YN);
                TransactionPossibleYnComboBox.Items.Add(item);
            }

            TransactionPossibleYnComboBox.SelectedIndex = -1;

            ScreenSortOrderTextBox.Text = "";      // 화면정렬순서

            InitDataGridView();
        }

        // 그리드 초기화
        private void InitDataGridView()
        {
            DataGridView dataGridView = NationListDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;
            //dataGridView.RowTemplate.Resizable = false;
        }

        // 검색버튼 클릭
        private void searchNationCodeListButton_Click(object sender, EventArgs e)
        {
            SearchNationCodeList();
        }

        // 국가목록 테이블 조회
        private void SearchNationCodeList()
        {
            NationListDataGridView.Rows.Clear();

            string NATN_CD = Utils.GetSelectedComboBoxItemValue(SearchNationCodeComboBox);
            string KOR_NATN_NM = SearchKorNationNameTextBox.Text.Trim();
            string ENG_NATN_NM = SearchEngNationNameTextBox.Text.Trim();
            string CUR_CD = "";
            string TRAN_PSBL_YN_NM = "";
            string SCRN_SORT_ORD = "";
            string TRAN_PSBL_YN = "";

            string query = string.Format("CALL SelectNationCodeList ('{0}', '{1}', '{2}')", NATN_CD, KOR_NATN_NM, ENG_NATN_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가목록정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                NATN_CD = datarow["NATN_CD"].ToString();
                KOR_NATN_NM = datarow["KOR_NATN_NM"].ToString();
                ENG_NATN_NM = datarow["ENG_NATN_NM"].ToString();
                CUR_CD = datarow["CUR_CD"].ToString();
                TRAN_PSBL_YN_NM = datarow["TRAN_PSBL_YN_NM"].ToString();
                SCRN_SORT_ORD = datarow["SCRN_SORT_ORD"].ToString();
                TRAN_PSBL_YN = datarow["TRAN_PSBL_YN"].ToString();

                NationListDataGridView.Rows.Add(NATN_CD, KOR_NATN_NM, ENG_NATN_NM, CUR_CD, TRAN_PSBL_YN_NM, SCRN_SORT_ORD, TRAN_PSBL_YN);
            }

            NationListDataGridView.ClearSelection();
        }

        // 그리드 행 클릭
        private void NationListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (NationListDataGridView.SelectedRows.Count == 0)
                return;

            string NATN_CD = NationListDataGridView.SelectedRows[0].Cells[(int)eNationListDataGridView.NATN_CD].Value.ToString();
            string KOR_NATN_NM = NationListDataGridView.SelectedRows[0].Cells[(int)eNationListDataGridView.KOR_NATN_NM].Value.ToString();
            string ENG_NATN_NM = NationListDataGridView.SelectedRows[0].Cells[(int)eNationListDataGridView.ENG_NATN_NM].Value.ToString();
            string CUR_CD = NationListDataGridView.SelectedRows[0].Cells[(int)eNationListDataGridView.CUR_CD].Value.ToString();
            string TRAN_PSBL_YN = NationListDataGridView.SelectedRows[0].Cells[(int)eNationListDataGridView.TRAN_PSBL_YN].Value.ToString();
            string SCRN_SORT_ORD = NationListDataGridView.SelectedRows[0].Cells[(int)eNationListDataGridView.SCRN_SORT_ORD].Value.ToString();

            NationCodeTextBox.Text = NATN_CD;
            KorNationNameTextBox.Text = KOR_NATN_NM;
            EngNationNameTextBox.Text = ENG_NATN_NM;

            CurrencyCodeComboBox.SelectedIndex = -1;

            Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD);                   // 통화코드
            Utils.SelectComboBoxItemByValue(TransactionPossibleYnComboBox, TRAN_PSBL_YN);    // 거래가능여부

            ScreenSortOrderTextBox.Text = SCRN_SORT_ORD;
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            string NATN_CD = NationCodeTextBox.Text.Trim();                                           // 국가코드
            string KOR_NATN_NM = KorNationNameTextBox.Text.Trim();                                    // 한글국가명
            string ENG_NATN_NM = EngNationNameTextBox.Text.Trim();                                    // 영문국가명
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);                 // 통화코드
            string TRAN_PSBL_YN = Utils.GetSelectedComboBoxItemValue(TransactionPossibleYnComboBox);  // 거래가능여부
            string SCRN_SORT_ORD = ScreenSortOrderTextBox.Text.Trim();                                // 화면정렬순서
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                           // 최초등록자ID

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            string query = string.Format("CALL InsertNationCodeItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                NATN_CD, KOR_NATN_NM, ENG_NATN_NM, CUR_CD, TRAN_PSBL_YN, SCRN_SORT_ORD, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("국가목록정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchNationCodeList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("국가목록정보를 저장했습니다.");
            }

        }

        // 삭제버튼 클릭
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string NATN_CD = NationCodeTextBox.Text.Trim();

            if (NATN_CD == "")
            {
                MessageBox.Show("국가코드는 필수 입력항목입니다.");
                NationCodeTextBox.Focus();
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteNationCode ('{0}')", NATN_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("국가코드를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchNationCodeList();
                MessageBox.Show("선택한 항목을 삭제했습니다.");
            }
        }

        // 닫기버튼 클릭
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckRequireItems()
        {
            string NATN_CD = NationCodeTextBox.Text.Trim();
            string KOR_NATN_NM = KorNationNameTextBox.Text.Trim();
            string ENG_NATN_NM = EngNationNameTextBox.Text.Trim();
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);
            string TRAN_PSBL_YN = Utils.GetSelectedComboBoxItemValue(TransactionPossibleYnComboBox);
            string SCRN_SORT_ORD = ScreenSortOrderTextBox.Text.Trim();

            if (NATN_CD == "")
            {
                MessageBox.Show("국가코드는 필수입력항목입니다.");
                NationCodeTextBox.Focus();
                return false;
            }

            if (KOR_NATN_NM == "")
            {
                MessageBox.Show("한글국가명은 필수입력항목입니다.");
                KorNationNameTextBox.Focus();
                return false;
            }

            if (ENG_NATN_NM == "")
            {
                MessageBox.Show("영문국가명은 필수입력항목입니다.");
                EngNationNameTextBox.Focus();
                return false;
            }

            if (CUR_CD == "")
            {
                MessageBox.Show("통화코드는 필수입력항목입니다.");
                CurrencyCodeComboBox.Focus();
                return false;
            }

            if (TRAN_PSBL_YN == "")
            {
                MessageBox.Show("거래가능여부는 필수입력항목입니다.");
                TransactionPossibleYnComboBox.Focus();
                return false;
            }

            if (SCRN_SORT_ORD == "")
            {
                MessageBox.Show("화면정렬순서는 필수입력항목입니다.");
                ScreenSortOrderTextBox.Focus();
                return false;
            }

            return true;
        }

        private void NationCodeGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = NationListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && NationListDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (NationListDataGridView.SelectedRows.Count == 0)
                return;

            string NATN_CD = NationListDataGridView.Rows[rowIndex].Cells[(int)eNationListDataGridView.NATN_CD].Value.ToString();
            string KOR_NATN_NM = NationListDataGridView.Rows[rowIndex].Cells[(int)eNationListDataGridView.KOR_NATN_NM].Value.ToString();
            string ENG_NATN_NM = NationListDataGridView.Rows[rowIndex].Cells[(int)eNationListDataGridView.ENG_NATN_NM].Value.ToString();
            string CUR_CD = NationListDataGridView.Rows[rowIndex].Cells[(int)eNationListDataGridView.CUR_CD].Value.ToString();
            string TRAN_PSBL_YN = NationListDataGridView.Rows[rowIndex].Cells[(int)eNationListDataGridView.TRAN_PSBL_YN].Value.ToString();
            string SCRN_SORT_ORD = NationListDataGridView.Rows[rowIndex].Cells[(int)eNationListDataGridView.SCRN_SORT_ORD].Value.ToString();

            NationCodeTextBox.Text = NATN_CD;
            KorNationNameTextBox.Text = KOR_NATN_NM;
            EngNationNameTextBox.Text = ENG_NATN_NM;

            CurrencyCodeComboBox.SelectedIndex = -1;

            Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD);                   // 통화코드
            Utils.SelectComboBoxItemByValue(TransactionPossibleYnComboBox, TRAN_PSBL_YN);    // 거래가능여부

            ScreenSortOrderTextBox.Text = SCRN_SORT_ORD;
        }
    }
}
