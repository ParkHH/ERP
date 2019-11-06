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

namespace TripERP.CustomerMgt
{
    public partial class CooperativeCompanyInfoMgt : Form
    {
        enum eCooperativeCompanyListDataGridView
        {
            CMPN_NO,               // 협력업체번호
            CMPN_NM,               // 협력업체명
            COOP_CMPN_DVSN_NM,     // 협력업체구분명
            COOP_CMPN_DVSN_CD,     // 협력업체구분코드
            INDU_RGST_NO,          // 사업자등록번호
            RPRS_NM,               // 대표자명
            RPRS_OFFC_PHNE_NO,     // 대표회사전화번호
            RPRS_OFFC_EXT_NO,      // 대표회사전화내선번호
            RPRS_CELL_PHNE_NO,     // 대표휴대폰번호
            RPRS_FAX_NO,           // 대표팩스번호
            CMNY_POST_NO,          // 회사우편번호
            CMNY_ADDR1,            // 회사주소1
            CMNY_ADDR2,            // 회사주소2
            MAIN_TRAN_BANK_NM,     // 주거래은행명
            MAIN_TRAN_BANK_CD,     // 주거래은행코드
            MAIN_TRAN_ACCT_NO,     // 주거래은행계좌번호
            MAIN_TRAN_CUR_CD,      // 주거래통화코드
            CMPN_MEMO_CNTS         // 업체메모
        };

        string CMPN_NO;               // 협력업체번호    
        string CMPN_NM;              // 협력업체명
        string COOP_CMPN_DVSN_NM;     // 협력업체구분명
        string COOP_CMPN_DVSN_CD;     // 협력업체구분코드
        string INDU_RGST_NO;          // 사업자등록번호
        string RPRS_NM;               // 대표자명
        string RPRS_OFFC_PHNE_NO;     // 대표회사전화번호
        string RPRS_OFFC_EXT_NO;      // 대표회사전화내선번호
        string RPRS_CELL_PHNE_NO;     // 대표휴대폰번호
        string RPRS_FAX_NO;           // 대표팩스번호
        string CMNY_POST_NO;          // 회사우편번호
        string CMNY_ADDR1;            // 회사주소1
        string CMNY_ADDR2;            // 회사주소2
        string MAIN_TRAN_BANK_NM;     // 주거래은행명
        string MAIN_TRAN_BANK_CD;     // 주거래은행코드
        string MAIN_TRAN_ACCT_NO;     // 주거래은행계좌번호
        string MAIN_TRAN_CUR_CD;      // 주거래통화코드
        string CMPN_MEMO_CNTS;         // 업체메모

        public CooperativeCompanyInfoMgt()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void CooperativeCompanyInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchCoopCompanyList();            // form load 시 내용 바로 출력
        }

        // 초기화
        private void InitControls()
        {
            string query = "";
            DataSet dataSet;

            // 협력업체구분(COOP_CMPN_DVSN_CD)
            CompanyDivisionComboBox.Items.Clear();

            query = "CALL SelectCommoncodeList ('COOP_CMPN_DVSN_CD')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("업체구분 공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string COOP_CMPN_DVSN_CD = dataRow["CD_VLID_VAL"].ToString();
                string COOP_CMPN_DVSN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(COOP_CMPN_DVSN_NM, COOP_CMPN_DVSN_CD);
                SearchCompanyDivisionComboBox.Items.Add(item);
            }

            SearchCompanyDivisionComboBox.SelectedIndex = -1;

            // 주거래은행
            BankCodeComboBox.Items.Clear();

            query = "CALL SelectAllBankList ()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("금융기관 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string FNCL_ORGN_CD = dataRow["FNCL_ORGN_CD"].ToString();
                string FNCL_ORGN_NM = dataRow["FNCL_ORGN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(FNCL_ORGN_NM, FNCL_ORGN_CD);

                BankCodeComboBox.Items.Add(item);
            }

            BankCodeComboBox.SelectedIndex = -1;

            // 통화코드
            CurrencyCodeComboBox.Items.Clear();

            query = "CALL SelectCurList ()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("통화코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CUR_CD = dataRow["CUR_CD"].ToString();
                string CUR_NM = dataRow["CUR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CUR_NM, CUR_CD);

                CurrencyCodeComboBox.Items.Add(item);
            }

            CurrencyCodeComboBox.SelectedIndex = -1;

            // 폼 입력필드 초기화
            ResetInputFormField();

            // 그리드 스타일 초기화
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = CooperativeCompanyListDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.RowHeadersWidthSizeMode =     DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        // 입력폼 초기화
        private void ResetInputFormField()
        {
            CompanyNoTextBox.Text = "";
            CompanyNameTextBox.Text = "";
            CompanyDivisionComboBox.SelectedIndex = -1;
            CompanyDivisionComboBox.Text = "";
            CompanyRegistrationNoTextBox.Text = "";
            PostalNoTextBox.Text = "";
            OfficeAddr1TextBox.Text = "";
            OfficeAddr2TextBox.Text = "";
            RepresentativeNameTextBox.Text = "";
            OfficeTelNoTextBox.Text = "";
            OfficeExtensionNoTextBox.Text = "";
            RepresentativeCellPhoneNoTextBox.Text = "";
            OfficeFaxTextBox.Text = "";

            BankCodeComboBox.SelectedIndex = -1;
            BankCodeComboBox.Text = "";

            AccountNoTextBox.Text = "";
            CurrencyCodeComboBox.SelectedIndex = -1;
            CurrencyCodeComboBox.Text = "";

            CompanyMemoTextBox.Text = "";

            string query = "";
            DataSet dataSet;

            // 협력업체구분(COOP_CMPN_DVSN_CD)
            CompanyDivisionComboBox.Items.Clear();

            query = "CALL SelectCommoncodeList ('COOP_CMPN_DVSN_CD')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("업체구분 공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string COOP_CMPN_DVSN_CD = dataRow["CD_VLID_VAL"].ToString();
                string COOP_CMPN_DVSN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(COOP_CMPN_DVSN_NM, COOP_CMPN_DVSN_CD);
                CompanyDivisionComboBox.Items.Add(item);
            }

            CompanyDivisionComboBox.SelectedIndex = -1;
        }

        // 협력업체목록 조회버튼 클릭
        private void SearchCostPriceListButton_Click(object sender, EventArgs e)
        {
            SearchCoopCompanyList();
        }

        // 협력업체 목록조회
        private void SearchCoopCompanyList()
        {
            CooperativeCompanyListDataGridView.Rows.Clear();

            string CMPN_NO = "";              // 협력업체번호
            string CMPN_NM = SearchCompanyNameTextBox.Text.Trim();                   // 협력업체명
            string COOP_CMPN_DVSN_NM = "";         // 협력업체구분명
            string COOP_CMPN_DVSN_CD = Utils.GetSelectedComboBoxItemValue(SearchCompanyDivisionComboBox);         // 협력업체구분코드
            string INDU_RGST_NO = "";              // 사업자등록번호
            string RPRS_NM = "";                   // 대표자명
            string RPRS_OFFC_PHNE_NO = "";         // 대표전화번호
            string RPRS_OFFC_EXT_NO = "";          // 대표전화내선번호
            string RPRS_CELL_PHNE_NO = "";         // 대표휴대폰번호
            string RPRS_FAX_NO = "";               // 대표팩스번호
            string CMNY_POST_NO = "";              // 회사우편번호
            string CMNY_ADDR1 = "";                // 회사주소1
            string CMNY_ADDR2 = "";                // 회사주소2
            string MAIN_TRAN_BANK_NM = "";         // 주거래은행명
            string MAIN_TRAN_BANK_CD = "";         // 주거래은행코드
            string MAIN_TRAN_ACCT_NO = "";         // 주거래계좌번호
            string MAIN_TRAN_CUR_CD = "";          // 주거래통화코드
            string CMPN_MEMO_CNTS = "";            // 업체메모내용

            string query = string.Format("CALL SelectCooperativeCompanyInfoList ('{0}', '{1}')", COOP_CMPN_DVSN_CD, CMPN_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("협력업체기본정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {

                CMPN_NO = datarow["CMPN_NO"].ToString();
                CMPN_NM = datarow["CMPN_NM"].ToString();
                COOP_CMPN_DVSN_NM = datarow["COOP_CMPN_DVSN_NM"].ToString();
                COOP_CMPN_DVSN_CD = datarow["COOP_CMPN_DVSN_CD"].ToString();
                INDU_RGST_NO = datarow["INDU_RGST_NO"].ToString();
                RPRS_NM = datarow["RPRS_NM"].ToString();
                RPRS_OFFC_PHNE_NO = datarow["RPRS_OFFC_PHNE_NO"].ToString();
                RPRS_OFFC_EXT_NO = datarow["RPRS_OFFC_EXT_NO"].ToString();
                RPRS_CELL_PHNE_NO = datarow["RPRS_CELL_PHNE_NO"].ToString();
                RPRS_FAX_NO = datarow["RPRS_FAX_NO"].ToString();
                CMNY_POST_NO = datarow["CMNY_POST_NO"].ToString();
                CMNY_ADDR1 = datarow["CMNY_ADDR1"].ToString();
                CMNY_ADDR2 = datarow["CMNY_ADDR2"].ToString();
                MAIN_TRAN_BANK_NM = datarow["MAIN_TRAN_BANK_NM"].ToString();
                MAIN_TRAN_BANK_CD = datarow["MAIN_TRAN_BANK_CD"].ToString();
                MAIN_TRAN_ACCT_NO = datarow["MAIN_TRAN_ACCT_NO"].ToString();
                MAIN_TRAN_CUR_CD = datarow["MAIN_TRAN_CUR_CD"].ToString();
                CMPN_MEMO_CNTS = datarow["CMPN_MEMO_CNTS"].ToString();

                CooperativeCompanyListDataGridView.Rows.Add
                (
                    CMPN_NO,               // 협력업체번호
                    CMPN_NM,               // 협력업체명
                    COOP_CMPN_DVSN_NM,     // 협력업체구분명
                    COOP_CMPN_DVSN_CD,     // 협력업체구분코드
                    INDU_RGST_NO,          // 사업자등록번호
                    RPRS_NM,               // 대표자명
                    RPRS_OFFC_PHNE_NO,     // 대표회사전화번호
                    RPRS_OFFC_EXT_NO,      // 대표회사전화내선번호
                    RPRS_CELL_PHNE_NO,     // 대표휴대폰번호
                    RPRS_FAX_NO,           // 대표팩스번호
                    CMNY_POST_NO,          // 회사우편번호
                    CMNY_ADDR1,            // 회사주소1
                    CMNY_ADDR2,            // 회사주소2
                    MAIN_TRAN_BANK_NM,     // 주거래은행명
                    MAIN_TRAN_BANK_CD,     // 주거래은행코드
                    MAIN_TRAN_ACCT_NO,     // 주거래은행계좌번호
                    MAIN_TRAN_CUR_CD,      // 주거래통화코드
                    CMPN_MEMO_CNTS         // 업체메모
                );
            }

            CooperativeCompanyListDataGridView.ClearSelection();
        }

        // 그리드 행 클릭
        private void CooperativeCompanyListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CooperativeCompanyListDataGridView.SelectedRows.Count == 0)
                return;

            CMPN_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.CMPN_NO].Value.ToString();
            CMPN_NM = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.CMPN_NM].Value.ToString();
            COOP_CMPN_DVSN_NM = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.COOP_CMPN_DVSN_NM].Value.ToString();
            COOP_CMPN_DVSN_CD = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.COOP_CMPN_DVSN_CD].Value.ToString();
            INDU_RGST_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.INDU_RGST_NO].Value.ToString();
            RPRS_NM = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_NM].Value.ToString();
            RPRS_OFFC_PHNE_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_OFFC_PHNE_NO].Value.ToString();
            RPRS_OFFC_EXT_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_OFFC_EXT_NO].Value.ToString();
            RPRS_CELL_PHNE_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_CELL_PHNE_NO].Value.ToString();
            RPRS_FAX_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_FAX_NO].Value.ToString();
            CMNY_POST_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.CMNY_POST_NO].Value.ToString();
            CMNY_ADDR1 = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.CMNY_ADDR1].Value.ToString();
            CMNY_ADDR2 = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.CMNY_ADDR2].Value.ToString();
            MAIN_TRAN_BANK_NM = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_BANK_NM].Value.ToString();
            MAIN_TRAN_BANK_CD = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_BANK_CD].Value.ToString();
            MAIN_TRAN_ACCT_NO = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_ACCT_NO].Value.ToString();
            MAIN_TRAN_CUR_CD = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_CUR_CD].Value.ToString();
            CMPN_MEMO_CNTS = CooperativeCompanyListDataGridView.SelectedRows[0].Cells[(int)eCooperativeCompanyListDataGridView.CMPN_MEMO_CNTS].Value.ToString();

            optionDataGridViewRowChoice();
        }

        private void CooperativeCompanyGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = CooperativeCompanyListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && CooperativeCompanyListDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (CooperativeCompanyListDataGridView.SelectedRows.Count == 0)
                return;

            CMPN_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.CMPN_NO].Value.ToString();
            CMPN_NM = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.CMPN_NM].Value.ToString();
            COOP_CMPN_DVSN_NM = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.COOP_CMPN_DVSN_NM].Value.ToString();
            COOP_CMPN_DVSN_CD = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.COOP_CMPN_DVSN_CD].Value.ToString();
            INDU_RGST_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.INDU_RGST_NO].Value.ToString();
            RPRS_NM = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_NM].Value.ToString();
            RPRS_OFFC_PHNE_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_OFFC_PHNE_NO].Value.ToString();
            RPRS_OFFC_EXT_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_OFFC_EXT_NO].Value.ToString();
            RPRS_CELL_PHNE_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_CELL_PHNE_NO].Value.ToString();
            RPRS_FAX_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.RPRS_FAX_NO].Value.ToString();
            CMNY_POST_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.CMNY_POST_NO].Value.ToString();
            CMNY_ADDR1 = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.CMNY_ADDR1].Value.ToString();
            CMNY_ADDR2 = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.CMNY_ADDR2].Value.ToString();
            MAIN_TRAN_BANK_NM = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_BANK_NM].Value.ToString();
            MAIN_TRAN_BANK_CD = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_BANK_CD].Value.ToString();
            MAIN_TRAN_ACCT_NO = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_ACCT_NO].Value.ToString();
            MAIN_TRAN_CUR_CD = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.MAIN_TRAN_CUR_CD].Value.ToString();
            CMPN_MEMO_CNTS = CooperativeCompanyListDataGridView.Rows[rowIndex].Cells[(int)eCooperativeCompanyListDataGridView.CMPN_MEMO_CNTS].Value.ToString();

            optionDataGridViewRowChoice();
        }

        private void optionDataGridViewRowChoice()
        {
            CompanyNoTextBox.Text = CMPN_NO;                                               // 협력업체번호
            CompanyNameTextBox.Text = CMPN_NM;                                             // 협력업체명
            Utils.SelectComboBoxItemByValue(CompanyDivisionComboBox, COOP_CMPN_DVSN_CD);   // 협력업체구분코드
            CompanyRegistrationNoTextBox.Text = INDU_RGST_NO;                              // 사업자등록번호
            PostalNoTextBox.Text = CMNY_POST_NO;                                           // 회사우편번호
            OfficeAddr1TextBox.Text = CMNY_ADDR1;                                          // 회사주소1
            OfficeAddr2TextBox.Text = CMNY_ADDR2;                                          // 회사주소2
            RepresentativeNameTextBox.Text = RPRS_NM;                                      // 대표자명
            OfficeTelNoTextBox.Text = RPRS_OFFC_PHNE_NO;                                   // 대표회사전화번호
            OfficeExtensionNoTextBox.Text = RPRS_OFFC_EXT_NO;                              // 대표회사전화내선번호
            RepresentativeCellPhoneNoTextBox.Text = RPRS_CELL_PHNE_NO;                     // 대표휴대폰번호
            OfficeFaxTextBox.Text = RPRS_FAX_NO;                                           // 대표팩스번호
            Utils.SelectComboBoxItemByValue(BankCodeComboBox, MAIN_TRAN_BANK_CD);          // 주거래은행코드
            AccountNoTextBox.Text = MAIN_TRAN_ACCT_NO;                                     // 주거래은행계좌번호
            Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, MAIN_TRAN_CUR_CD);       // 주거래은행코드
            CompanyMemoTextBox.Text = CMPN_MEMO_CNTS;                                      // 업체메모
        }

        // 초기화버튼 클릭
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 저장버튼 클릭
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string CMPN_NO = CompanyNoTextBox.Text.Trim();                                               // 협력업체번호
            string CMPN_NM = CompanyNameTextBox.Text.Trim();                                             // 협력업체명
            string COOP_CMPN_DVSN_CD = Utils.GetSelectedComboBoxItemValue(CompanyDivisionComboBox);      // 협력업체구분코드
            string INDU_RGST_NO = CompanyRegistrationNoTextBox.Text.Trim();                              // 사업자등록번호
            string CMNY_POST_NO = PostalNoTextBox.Text.Trim();                                           // 회사우편번호
            string CMNY_ADDR1 = OfficeAddr1TextBox.Text.Trim();                                          // 회사주소1
            string CMNY_ADDR2 = OfficeAddr2TextBox.Text.Trim();                                          // 회사주소2
            string RPRS_NM = RepresentativeNameTextBox.Text.Trim();                                      // 대표자명
            string RPRS_OFFC_PHNE_NO = OfficeTelNoTextBox.Text.Trim();                                   // 대표회사전화번호
            string RPRS_OFFC_EXT_NO = OfficeExtensionNoTextBox.Text.Trim();                              // 대표회사전화내선번호
            string RPRS_CELL_PHNE_NO = RepresentativeCellPhoneNoTextBox.Text.Trim();                     // 대표휴대폰번호
            string RPRS_FAX_NO = OfficeFaxTextBox.Text.Trim();                                           // 대표팩스번호
            string MAIN_TRAN_BANK_CD = Utils.GetSelectedComboBoxItemValue(BankCodeComboBox);             // 주거래은행코드
            string MAIN_TRAN_ACCT_NO = AccountNoTextBox.Text.Trim();                                     // 주거래은행계좌번호
            string MAIN_TRAN_CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);          // 주거래은행코드
            string CMPN_MEMO_CNTS = CompanyMemoTextBox.Text.Trim();                                      // 업체메모
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                              // 최초등록자ID

            string query = "";
            int _companyNo = 0;

            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;

            // 업체번호가 입력되지 않으면 MAX+1로 채번하고 Insert처리
            if (CMPN_NO == "" || CMPN_NO == null)
            {
                query = "CALL SelectMaxCooperativeCompanyNo";
                DataSet dataSet = DbHelper.SelectQuery(query);
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBox.Show("협력업체번호를 가져올 수 없습니다.");
                    return;
                }

                DataRow dataRow = dataSet.Tables[0].Rows[0];
                CMPN_NO = dataRow["CMPN_NO"].ToString(); // 협력업체번호

                // 채번값이 없으면 1로 설정하고 있으면 +1 증가
                if (CMPN_NO == "0")
                {
                    CMPN_NO = "1";
                }
                else
                {
                    _companyNo = Convert.ToInt16(CMPN_NO);
                    _companyNo = _companyNo + 1;
                    CMPN_NO = Convert.ToString(_companyNo);
                }

                query = string.Format("CALL InsertCooperativeCompanyInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
                            CMPN_NO, CMPN_NM, COOP_CMPN_DVSN_CD, INDU_RGST_NO, RPRS_NM, RPRS_OFFC_PHNE_NO, RPRS_OFFC_EXT_NO, RPRS_CELL_PHNE_NO, RPRS_FAX_NO, 
                            CMNY_POST_NO, CMNY_ADDR1, CMNY_ADDR2, MAIN_TRAN_BANK_CD, MAIN_TRAN_ACCT_NO, MAIN_TRAN_CUR_CD, CMPN_MEMO_CNTS, FRST_RGTR_ID);
            }
            else
            {
                query = string.Format("CALL UpdateCooperativeCompanyInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
                        CMPN_NO, CMPN_NM, COOP_CMPN_DVSN_CD, INDU_RGST_NO, RPRS_NM, RPRS_OFFC_PHNE_NO, RPRS_OFFC_EXT_NO, RPRS_CELL_PHNE_NO, RPRS_FAX_NO,
                        CMNY_POST_NO, CMNY_ADDR1, CMNY_ADDR2, MAIN_TRAN_BANK_CD, MAIN_TRAN_ACCT_NO, MAIN_TRAN_CUR_CD, CMPN_MEMO_CNTS, FRST_RGTR_ID);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("협력업체정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchCoopCompanyList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("협력업체정보를 저장했습니다.");
            }

            // 저장 후 입력폼 초기화
            ResetInputFormField();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string CMPN_NO = CompanyNoTextBox.Text.Trim();                                               // 협력업체번호
            string CMPN_NM = CompanyNameTextBox.Text.Trim();                                             // 협력업체명
            string COOP_CMPN_DVSN_CD = Utils.GetSelectedComboBoxItemValue(CompanyDivisionComboBox);      // 협력업체구분코드
            string INDU_RGST_NO = CompanyRegistrationNoTextBox.Text.Trim();                              // 사업자등록번호
            string CMNY_POST_NO = PostalNoTextBox.Text.Trim();                                           // 회사우편번호
            string CMNY_ADDR1 = OfficeAddr1TextBox.Text.Trim();                                          // 회사주소1
            string CMNY_ADDR2 = OfficeAddr2TextBox.Text.Trim();                                          // 회사주소2
            string RPRS_NM = RepresentativeNameTextBox.Text.Trim();                                      // 대표자명
            string RPRS_OFFC_PHNE_NO = OfficeTelNoTextBox.Text.Trim();                                   // 대표회사전화번호
            string RPRS_OFFC_EXT_NO = OfficeExtensionNoTextBox.Text.Trim();                              // 대표회사전화내선번호
            string RPRS_CELL_PHNE_NO = RepresentativeCellPhoneNoTextBox.Text.Trim();                     // 대표휴대폰번호
            string RPRS_FAX_NO = OfficeFaxTextBox.Text.Trim();                                           // 대표팩스번호
            string MAIN_TRAN_BANK_CD = Utils.GetSelectedComboBoxItemValue(BankCodeComboBox);             // 주거래은행코드
            string MAIN_TRAN_ACCT_NO = AccountNoTextBox.Text.Trim();                                     // 주거래은행계좌번호
            string MAIN_TRAN_CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);          // 주거래은행코드
            string CMPN_MEMO_CNTS = CompanyMemoTextBox.Text.Trim();                                      // 업체메모

            if (CMPN_NM == "")
            {
                MessageBox.Show("업체명은 필수 입력항목입니다.");
                CompanyNameTextBox.Focus();
                return false;
            }

            if (COOP_CMPN_DVSN_CD == "")
            {
                MessageBox.Show("업체구분을 선택하세요.");
                CompanyDivisionComboBox.Focus();
                return false;
            }

            if (RPRS_NM == "")
            {
                MessageBox.Show("대표자명은 필수 입력항목입니다.");
                CompanyNameTextBox.Focus();
                return false;
            }

            if (MAIN_TRAN_CUR_CD == "")
            {
                MessageBox.Show("통화코드를 선택하세요.");
                CurrencyCodeComboBox.Focus();
                return false;
            }

            return true;
        }

        // 삭제버튼 클릭
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string CMPN_NO = CompanyNoTextBox.Text.Trim();                                               // 협력업체번호

            if (CMPN_NO == "")
            {
                MessageBox.Show("업체번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요.");
                return;
            }

            string query = string.Format("CALL DeleteCooperativeCompanyInfo ('{0}')", CMPN_NO);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show(" 협력업체정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchCoopCompanyList();
                MessageBox.Show("협력업체정보를 삭제했습니다.");
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        // 폼닫기
        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 도로명 주소찾기
        private void SearchAddressButton_Click(object sender, EventArgs e)
        {
            // 도로명주소찾기 팝업
            PopUpSearchAddress form = new PopUpSearchAddress();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            CMNY_POST_NO = form.get_zipcode();
            string ROAD_NM_TEMP = form.get_roadname();
            string ROAD_NM_TEMP2 = form.get_roadname2();

            PostalNoTextBox.Text = CMNY_POST_NO;
            OfficeAddr1TextBox.Text = String.Concat(ROAD_NM_TEMP, ROAD_NM_TEMP2);
        }
    }
}