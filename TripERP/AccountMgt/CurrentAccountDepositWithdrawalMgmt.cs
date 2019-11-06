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
using System.IO;

namespace TripERP.AccountMgt
{
    public partial class CurrentAccountDepositWithdrawalMgmt : Form
    {
        public enum eCurrentAccountDepositWithdrawalDataGridView {
            BANK_NM,
            ACCT_NO,
            CUR_CD,
            TRAN_DT,
            TRAN_EXRT,
            DPWH_DVSN_CD,
            DPWH_WON_AMT,
            DPWH_FRCR_AMT,
            TRAN_CUST_NM,
            REMK_CNTS,
            TRAN_CMPN_NO,
            BANK_CD,
            DPWH_CNMB,
            TRAN_ACNT_TIT_CD
        };

        private string ACCT_NO {get; set;}                // 계좌번호
        private string TRAN_CUST_NM {get; set;}           // 거래고객명
        private string BANK_NM {get; set;}                // 은행명
        private string BANK_CD {get; set;}                // 은행코드
        private string CUR_CD {get; set;}                 // 통화코드
        private string TRAN_DT {get; set;}                // 거래일자
        private string DPWH_DVSN_CD {get; set;}           // 입출금구분코드
        private string DPWH_WON_AMT {get; set;}           // 입출금원화금액
        private string DPWH_FRCR_AMT {get; set;}          // 입출금외화금액
        private string TRAN_ACNT_TIT_CD {get; set;}       // 거래계정과목코드
        private string TRAN_CMPN_NO {get; set;}           // 거래업체번호
        private string REMK_CNTS {get; set;}              // 비고내용
        private string DPWH_CNMB {get; set;}              // 일련번호
        private string TRAN_EXRT { get; set; }            // 거래환율
        private string START_TRAN_DT { get; set; }
        private string END_TRAN_DT {get; set;}

        bool is_choice_mode = false;

        public CurrentAccountDepositWithdrawalMgmt()
        {
            InitializeComponent();
        }

        private void CurrentAccountDepositWithdrawalMgmt_Load(object sender, EventArgs e)
        {
            InitControls();
            searchCurrentAccountDepositWithdrawl();
        }

        // 초기화
        private void InitControls()
        {
            // 폼 입력필드 초기화
            ResetInputFormField();

            // 콤보박스 초기화
            ResetComboBox();

            // DataGridView 스타일 초기화
            InitDataGridView();
        }

        // 입력폼 초기화
        private void ResetInputFormField()
        {

            // 필드 초기화
            StartTransactionDateTimePicker.Value = DateTime.Now.AddDays(-7);
            TransactionCustomerNameTextBox.Text = "";
            
            //TransactionDateTimeTextBox.Text = "";
            //DPWH_Divison_TextBox.Text = "";
            DPWH_CNMB_TextBox.Text = "";
            
            TRAN_EXRT_TextBox.Text = "";
            
            DepositWithdrawlWonTextBox.Text = "";
            DepositWithdrawalFRCRTextbox.Text = "";
            REMK_CNTS_TextBox.Text = "";
        }

        private void ResetComboBox()
        {
            SearchAccountNoComboBox.Items.Clear();
            AccountNoComboBox.Items.Clear();
            //SearchTransactionCustomerNameComboBox.Items.Clear();
            CurrencyCodeComboBox.Items.Clear();
            DPWH_Division_CD_ComboBox.Items.Clear();
            TransactionAccountComboBox.Items.Clear();
            TransactionCompanyNoComboBox.Items.Clear();
            BankCodeComboBox.Items.Clear();

            SearchAccountNoComboBox.Items.Add(new ComboBoxItem("전체", ""));
            //SearchTransactionCustomerNameComboBox.Items.Add(new ComboBoxItem("전체", ""));

            LoadDPWHDivisionCodeItems();
            LoadAccountNoComboBoxItems();
            //LoadAccountCustomerNameComboBoxItems();
            LoadCurrencyCodeComboBoxItems();
            LoadBankCodeComboBoxItems();
            LoadTranAccountTitCodeComboBoxItems();
            LoadTranCompanyNumberComboBoxItems();
        }

        // 데이터 그리드뷰 초기화
        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = CurrentAcountDepositWithdrawlDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.DoubleBuffered(true);
        }

        // 입출금내역조회 버튼 클릭
        private void searchCostPriceListButton_Click(object sender, EventArgs e)
        {
            searchCurrentAccountDepositWithdrawl();
        }

        // 입출금내역 목록 조회
        private void searchCurrentAccountDepositWithdrawl()
        {
            CurrentAcountDepositWithdrawlDataGridView.Rows.Clear();

            ACCT_NO = Utils.GetSelectedComboBoxItemValue(SearchAccountNoComboBox);                 // 계좌번호
            //TRAN_CUST_NM = SearchTransactionCustomerNameComboBox.Text.Trim();    // 거래고객명
            BANK_NM = "";                // 은행명
            BANK_CD = "";                // 은행코드
            CUR_CD = Utils.GetSelectedComboBoxItemValue(SearchCurrencyCodeComboBox);                 // 통화코드
            TRAN_DT = "";                // 거래일자
            DPWH_DVSN_CD = "";           // 입출금구분코드
            TRAN_EXRT = "";              // 거래환율
            DPWH_WON_AMT = "";           // 입출금원화금액
            DPWH_FRCR_AMT = "";          // 입출금외화금액
            TRAN_ACNT_TIT_CD = "";       // 거래계정과목코드
            TRAN_CMPN_NO = "";           // 거래업체번호
            REMK_CNTS = "";              // 비고내용
            DPWH_CNMB = "";              // 일련번호

            if (ACCT_NO.Equals("전체"))
                ACCT_NO = "";
            //if (TRAN_CUST_NM.Equals("전체"))
            //    TRAN_CUST_NM = "";

            START_TRAN_DT = StartTransactionDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");
            END_TRAN_DT = EndTransactionDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");
            
            string query = string.Format("CALL SelectAllCurrentAccountDepositList ('{0}', '{1}', '{2}', '{3}')",
                                           ACCT_NO, CUR_CD, START_TRAN_DT, END_TRAN_DT);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("입출금내역을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                BANK_NM = dataRow["BANK_NM"].ToString();
                BANK_CD = dataRow["BANK_CD"].ToString();
                ACCT_NO = dataRow["ACCT_NO"].ToString();
                CUR_CD = dataRow["CUR_CD"].ToString();
                TRAN_DT = dataRow["TRAN_DT"].ToString().Substring(0, 10);
                TRAN_EXRT = dataRow["TRAN_EXRT"].ToString();
                DPWH_DVSN_CD = dataRow["DPWH_DVSN_CD"].ToString();
                DPWH_WON_AMT = Utils.SetComma(dataRow["DPWH_WON_AMT"].ToString());
                DPWH_FRCR_AMT = Utils.SetComma(dataRow["DPWH_FRCR_AMT"].ToString());
                TRAN_ACNT_TIT_CD = dataRow["TRAN_ACNT_TIT_CD"].ToString();
                TRAN_CMPN_NO = dataRow["TRAN_CMPN_NO"].ToString();
                TRAN_CUST_NM = dataRow["TRAN_CUST_NM"].ToString();
                REMK_CNTS = dataRow["REMK_CNTS"].ToString();
                DPWH_CNMB = dataRow["DPWH_CNMB"].ToString();

                if (DPWH_DVSN_CD.Equals("1"))
                    DPWH_DVSN_CD = "입금";
                else
                    DPWH_DVSN_CD = "출금";
                

                
                CurrentAcountDepositWithdrawlDataGridView.Rows.Add(
                    BANK_NM,
                    ACCT_NO,
                    CUR_CD,
                    TRAN_DT,
                    double.Parse(TRAN_EXRT),
                    DPWH_DVSN_CD,
                    double.Parse(DPWH_WON_AMT),
                    double.Parse(DPWH_FRCR_AMT),
                    TRAN_CUST_NM,
                    REMK_CNTS,
                    TRAN_CMPN_NO,
                    BANK_CD,
                    DPWH_CNMB,
                    TRAN_ACNT_TIT_CD);
            }
            CurrentAcountDepositWithdrawlDataGridView.ClearSelection();
        }

        // 그리드 행 클릭
        private void CurrentAcountDepositWithdrawlDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CurrentAcountDepositWithdrawlDataGridView.SelectedRows.Count == 0)
                return;

            BANK_NM = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.BANK_NM].Value.ToString(); 
            BANK_CD = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.BANK_CD].Value.ToString();
            ACCT_NO = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.ACCT_NO].Value.ToString();
            CUR_CD = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.CUR_CD].Value.ToString();
            TRAN_DT = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_DT].Value.ToString();
            TRAN_EXRT = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_EXRT].Value.ToString();
            DPWH_DVSN_CD = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_DVSN_CD].Value.ToString();
            DPWH_WON_AMT = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_WON_AMT].Value.ToString();
            DPWH_FRCR_AMT = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_FRCR_AMT].Value.ToString();
            TRAN_ACNT_TIT_CD = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_ACNT_TIT_CD].Value.ToString();            
            TRAN_CMPN_NO = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_CMPN_NO].Value.ToString();
            TRAN_CUST_NM = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_CUST_NM].Value.ToString();
            REMK_CNTS = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.REMK_CNTS].Value.ToString();
            DPWH_CNMB = CurrentAcountDepositWithdrawlDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_CNMB].Value.ToString();

            CurrentAcountDepositWithdrawlDataGridViewChoice();
        }

        private void CurrentAcountDepositWithdrawlDataGridViewChoice()
        {
            is_choice_mode = true;
            DPWH_CNMB_TextBox.Text = DPWH_CNMB;

            if (DPWH_DVSN_CD.Equals("입금"))
                DPWH_DVSN_CD = "1";
            else
                DPWH_DVSN_CD = "2";

            Utils.SelectComboBoxItemByValue(BankCodeComboBox, BANK_CD);
            Utils.SelectComboBoxItemByText(AccountNoComboBox, ACCT_NO); // 계좌번호 콤보박스
            Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD); // 판매통화코드 콤보박스
            Utils.SelectComboBoxItemByValue(DPWH_Division_CD_ComboBox, DPWH_DVSN_CD);

            TRAN_EXRT_TextBox.Text = TRAN_EXRT;

            DepositWithdrawlWonTextBox.Text = DPWH_WON_AMT;
            DepositWithdrawalFRCRTextbox.Text = DPWH_FRCR_AMT;
            // Utils.SelectComboBoxItemByValue(TransactionAccountComboBox, TRAN_ACNT_TIT_CD);         // 거래계정과목코드
            Utils.SelectComboBoxItemByText(TransactionCompanyNoComboBox, TRAN_CMPN_NO);      // 거래업체번호
            TransactionCustomerNameTextBox.Text = TRAN_CUST_NM;
            REMK_CNTS_TextBox.Text = REMK_CNTS;
        }

        // 저장 버튼 클릭
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Boolean updateFlag = false;
            string BANK_CD = Utils.GetSelectedComboBoxItemValue(BankCodeComboBox);
            string ACCT_NO = AccountNoComboBox.Text.Trim();                                                         // 계좌번호
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);                   // 통화코드
            string TRAN_DT = TransactionDateTimePicker.Value.ToString("yyyy-MM-dd");                         // 거래일자
            if (DepositWithdrawlWonTextBox.Text.Equals("")) DepositWithdrawlWonTextBox.Text = "0";        // 입출금원화잔액
            double DPWH_WON_AMT = Utils.GetDoubleValue(DepositWithdrawlWonTextBox.Text);
            if (DepositWithdrawalFRCRTextbox.Text.Equals("")) DepositWithdrawalFRCRTextbox.Text = "0";   // 입출금외화잔액
            double DPWH_FRCR_AMT = Utils.GetDoubleValue(DepositWithdrawalFRCRTextbox.Text);
            string TRAN_CUST_NM = TransactionCustomerNameTextBox.Text.Trim();                               // 거래고객명
            string DPWH_DVSN_CD = Utils.GetSelectedComboBoxItemValue(DPWH_Division_CD_ComboBox);           // 입출금구분코드
            string TRAN_ACNT_TIT_CD = "100";        // 거래계정과목코드
            string TRAN_CMPN_NO = "0";           // 거래업체번호
            string REMK_CNTS = REMK_CNTS_TextBox.Text.Trim();              // 비고내용
            string TRAN_EXRT = TRAN_EXRT_TextBox.Text.Trim();           // 거래환율

            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                         // 최초등록자ID
            string FINL_MDFR_ID = Global.loginInfo.ACNT_ID;                         // 최종변경자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            // 은행코드가 채워져있으면 계좌정보 업데이트
            if (DPWH_CNMB_TextBox.Text != "")
            {
                string DPWH_CNMB = DPWH_CNMB_TextBox.Text.Trim();
                query = string.Format("CALL UpdateCurrentAccountDeposit ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')",
                    DPWH_CNMB, BANK_CD, ACCT_NO, CUR_CD, TRAN_DT, DPWH_WON_AMT, DPWH_FRCR_AMT, TRAN_CUST_NM, DPWH_DVSN_CD, TRAN_ACNT_TIT_CD, TRAN_CMPN_NO, REMK_CNTS, TRAN_EXRT, FINL_MDFR_ID);
                updateFlag = true;
            }
            // 은행코드가 안채워져있으면 계좌정보 저장
            else
            {
                query = string.Format("CALL InsertCurrentAccountDeposit ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                    BANK_CD, ACCT_NO, CUR_CD, TRAN_DT, DPWH_WON_AMT, DPWH_FRCR_AMT, TRAN_CUST_NM, DPWH_DVSN_CD, TRAN_ACNT_TIT_CD, TRAN_CMPN_NO, REMK_CNTS, TRAN_EXRT, FRST_RGTR_ID);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("입출금 내역을 저장할 수 없습니다.");
                return;
            }
            else
            {
                searchCurrentAccountDepositWithdrawl(); // 등록 후 그리드를 최신상태로 ReFresh
                if (updateFlag)
                    MessageBox.Show("입출금 내역을 수정했습니다.");
                else
                    MessageBox.Show("입출금 내역을 저장했습니다.");
            }

            // 저장 후 입력폼 초기화
            ResetInputFormField();
            ResetComboBox();
            searchCurrentAccountDepositWithdrawl();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string DPWH_CNMB = DPWH_CNMB_TextBox.Text.Trim();                   // 일련번호
            string ACCT_NO = AccountNoComboBox.Text.Trim();                         // 계좌번호

            string TRAN_DT = TransactionDateTimePicker.Text;                            // 거래일자
            string CUST_NM = TransactionCustomerNameTextBox.Text.Trim();        // 고객명
            string DPWH_DVSN = Utils.GetSelectedComboBoxItemText(DPWH_Division_CD_ComboBox);    // 입출금구분
            string DPWH_DVSN_CD = Utils.GetSelectedComboBoxItemValue(DPWH_Division_CD_ComboBox);    // 입출금구분
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);
            double COST_AMT;                                                                    // 거래금액




            if (CUR_CD.Equals("KRW"))
                COST_AMT = Utils.GetDoubleValue(DepositWithdrawlWonTextBox.Text);
            else
                COST_AMT = Utils.GetDoubleValue(DepositWithdrawalFRCRTextbox.Text);

            if (DPWH_CNMB == "")
            {
                MessageBox.Show("일련번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                DPWH_CNMB_TextBox.Focus();
                return;
            }

            if (ACCT_NO == "")
            {
                MessageBox.Show("계좌번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                AccountNoComboBox.Focus();
                return;
            }

            if (MessageBox.Show(TRAN_DT + "일 " + CUST_NM + "의 '" + COST_AMT + "'" + DPWH_DVSN + "내역을 무효화하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            return;

            string query = string.Format("CALL DeleteCurrentAccountDeposit ('{0}', '{1}', '{2}', '{3}', '{4}')", DPWH_CNMB, ACCT_NO, DPWH_DVSN_CD, CUR_CD, COST_AMT);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("입출금 내역을 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                searchCurrentAccountDepositWithdrawl();
                MessageBox.Show("해당 입출금내역을 무효화 했습니다.");
            }

            ResetComboBox();
            ResetInputFormField();
            searchCurrentAccountDepositWithdrawl();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 외화금액 컨트롤
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void DepositWithdrawalFRCRTextbox_TextChanged(object sender, EventArgs e) {
            if (DepositWithdrawalFRCRTextbox.Text.Trim().Equals("")) {
                DepositWithdrawalFRCRTextbox.Text = "0";
                return;
            }

            if (TRAN_EXRT_TextBox.Text.Trim().Equals(""))
                return;
            
            double DBL_FRCR_AMT = double.Parse(DepositWithdrawalFRCRTextbox.Text.Trim());
            double DBL_TRAN_EXRT = double.Parse(TRAN_EXRT_TextBox.Text.Trim());

            if (DBL_FRCR_AMT == 0)
                DepositWithdrawlWonTextBox.Text = "0";


            string CUR_CD = Utils.GetSelectedComboBoxItemText(CurrencyCodeComboBox);
            if (!CUR_CD.Equals("KRW")) {
                DepositWithdrawlWonTextBox.Text = Utils.SetComma((DBL_FRCR_AMT * DBL_TRAN_EXRT), 2).ToString();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 외화금액 컨트롤
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void TRAN_EXRT_TextBox_TextChanged(object sender, EventArgs e) {
            if (TRAN_EXRT_TextBox.Text.Trim().Equals("")) {
                TRAN_EXRT_TextBox.Text = "0";
                return;
            }

            if (DepositWithdrawalFRCRTextbox.Text.Trim().Equals(""))
                return;

            double DBL_FRCR_AMT = double.Parse(DepositWithdrawalFRCRTextbox.Text.Trim());
            double DBL_TRAN_EXRT = double.Parse(TRAN_EXRT_TextBox.Text.Trim());
            string CUR_CD = Utils.GetSelectedComboBoxItemText(CurrencyCodeComboBox);

            if (!CUR_CD.Equals("KRW")) {
                DepositWithdrawlWonTextBox.Text = Utils.SetComma((DBL_FRCR_AMT * DBL_TRAN_EXRT).ToString());
            }
        }

        // 초기화 버튼 클릭
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
            ResetComboBox();
            searchCurrentAccountDepositWithdrawl();
        }

        // 닫기 버튼 클릭
        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadAccountNoComboBoxItems()
        {
            string query = "CALL SelectAllCurrentAccountListGroupByAccount ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("계좌번호 목록을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string ACCT_NO = dataRow["ACCT_NO"].ToString();
                string ACCT_NM = dataRow["ACCT_NM"].ToString();

                ComboBoxItem ACCT_NO_ITEM = new ComboBoxItem(ACCT_NO, ACCT_NO);

                SearchAccountNoComboBox.Items.Add(ACCT_NO_ITEM);
                AccountNoComboBox.Items.Add(ACCT_NO_ITEM);
            }
            if (SearchAccountNoComboBox.Items.Count > 0)
            {
                SearchAccountNoComboBox.SelectedIndex = 0;
            }

            if (AccountNoComboBox.Items.Count > 0)
                AccountNoComboBox.Text = "";

        }

        /* 고객 검색 조건 삭제 요청 (2019-09-05 오세연과장)
        private void LoadAccountCustomerNameComboBoxItems()
        {
            string query = "CALL SelectCurrentAccountDepositCustomerName ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("거래고객명 목록을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                int i = 0;
                string TRAN_CUST_NM = dataRow["TRAN_CUST_NM"].ToString();

                ComboBoxItem TRAN_CUST_NM_ITEM = new ComboBoxItem(TRAN_CUST_NM, i);

                SearchTransactionCustomerNameComboBox.Items.Add(TRAN_CUST_NM_ITEM);
                i++;
            }
            if (SearchTransactionCustomerNameComboBox.Items.Count > 0)
                SearchTransactionCustomerNameComboBox.SelectedIndex = 0;
        }
        */

        // 그리드 방향키 컨트롤
        private void CurrentAcountDepositWithdrawlDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스 
            int rowIndex = CurrentAcountDepositWithdrawlDataGridView.CurrentRow.Index;
            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0) return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && CurrentAcountDepositWithdrawlDataGridView.Rows.Count == rowIndex + 1) return;

            if (e.KeyCode.Equals(Keys.Up))
            {
                BANK_NM = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.BANK_NM].Value.ToString();
                BANK_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.BANK_CD].Value.ToString();
                ACCT_NO = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.ACCT_NO].Value.ToString();
                CUR_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.CUR_CD].Value.ToString();
                TRAN_DT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_DT].Value.ToString();
                TRAN_EXRT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_EXRT].Value.ToString();
                DPWH_DVSN_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_DVSN_CD].Value.ToString();
                DPWH_WON_AMT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_WON_AMT].Value.ToString();
                DPWH_FRCR_AMT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_FRCR_AMT].Value.ToString();
                TRAN_ACNT_TIT_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_ACNT_TIT_CD].Value.ToString();
                TRAN_CMPN_NO = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_CMPN_NO].Value.ToString();
                TRAN_CUST_NM = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_CUST_NM].Value.ToString();
                REMK_CNTS = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.REMK_CNTS].Value.ToString();
                DPWH_CNMB = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_CNMB].Value.ToString();
            }
            else if (e.KeyCode.Equals(Keys.Down))
            {
                BANK_NM = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.BANK_NM].Value.ToString();
                BANK_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.BANK_CD].Value.ToString();
                ACCT_NO = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.ACCT_NO].Value.ToString();
                CUR_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.CUR_CD].Value.ToString();
                TRAN_DT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_DT].Value.ToString();
                TRAN_EXRT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_EXRT].Value.ToString();
                DPWH_DVSN_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_DVSN_CD].Value.ToString();
                DPWH_WON_AMT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_WON_AMT].Value.ToString();
                DPWH_FRCR_AMT = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_FRCR_AMT].Value.ToString();
                TRAN_ACNT_TIT_CD = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_ACNT_TIT_CD].Value.ToString();
                TRAN_CMPN_NO = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_CMPN_NO].Value.ToString();
                TRAN_CUST_NM = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.TRAN_CUST_NM].Value.ToString();
                REMK_CNTS = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.REMK_CNTS].Value.ToString();
                DPWH_CNMB = CurrentAcountDepositWithdrawlDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDepositWithdrawalDataGridView.DPWH_CNMB].Value.ToString();
            }

            CurrentAcountDepositWithdrawlDataGridViewChoice();
        }

        private void exportExcelButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "파일 내보내기";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Trim() == "")
                return;

            this.Cursor = Cursors.WaitCursor;

            string filePath = saveFileDialog.FileName.Trim();
            string fileDirPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));

            CurrentAcountDepositWithdrawlDataGridView.SelectAll();

            if (Directory.Exists(fileDirPath))
            {
                //if (true == ExcelHelper.ExportExcel(filePath, reservationDataGridView))
                if (ExcelHelper.gridToExcel(filePath, CurrentAcountDepositWithdrawlDataGridView) == true)
                    MessageBox.Show(string.Format("{0}\r\n파일을 저장했습니다.", filePath));
                else
                    MessageBox.Show("파일을 저장 할 수 없습니다.");
            }
            else
            {
                MessageBox.Show("잘못된 저장 경로입니다.");
            }

            this.Cursor = Cursors.Default;
        }

        private void LoadCurrencyCodeComboBoxItems()
        {
            CurrencyCodeComboBox.Items.Clear();
            SearchCurrencyCodeComboBox.Items.Clear();

            string query = "CALL SelectCurListFromAccount ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("통화코드 정보를 가져올 수 없습니다.");
                return;
            }

            //currencyCodeComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CUR_CD = dataRow["CUR_CD"].ToString();
                string CUR_NM = dataRow["CUR_NM"].ToString();
                string CUR_SYBL = dataRow["CUR_SYBL"].ToString();

                ComboBoxItem item = new ComboBoxItem(string.Format("{0}({1})", CUR_NM, CUR_SYBL), CUR_CD);

                CurrencyCodeComboBox.Items.Add(item);      // 판매통화코드
                SearchCurrencyCodeComboBox.Items.Add(item);
            }

            if (CurrencyCodeComboBox.Items.Count > 0)
                CurrencyCodeComboBox.Text = "";

            if (SearchCurrencyCodeComboBox.Items.Count > 0)
                SearchCurrencyCodeComboBox.Text = "";
        }

        private void LoadDPWHDivisionCodeItems()
        {
            List<CommonCodeItem> list = Global.GetCommonCodeList("DPWH_DVSN_CD");

            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                DPWH_Division_CD_ComboBox.Items.Add(item);
            }

            if (DPWH_Division_CD_ComboBox.Items.Count > 0)
                DPWH_Division_CD_ComboBox.Text = "";

        }

        // 은행코드 콤보박스
        private void LoadBankCodeComboBoxItems()
        {
            BankCodeComboBox.Items.Clear();

            string query = "CALL SelectBankCode ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("은행코드 정보를 가져올 수 없습니다.");
                return;
            }

            //currencyCodeComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string FNCL_ORGN_CD = dataRow["FNCL_ORGN_CD"].ToString();
                string FNCL_ORGN_NM = dataRow["FNCL_ORGN_NM"].ToString();

                //ComboBoxItem item = new ComboBoxItem(string.Format("{0}({1})", FNCL_ORGN_CD, FNCL_ORGN_NM), FNCL_ORGN_CD);
                ComboBoxItem item = new ComboBoxItem(FNCL_ORGN_NM, FNCL_ORGN_CD);

                BankCodeComboBox.Items.Add(item);      // 은행코드
            }

            // 거래은행을 국민은행으로 기본값 설정
            if (BankCodeComboBox.Items.Count > 0) 
                Utils.SelectComboBoxItemByText(BankCodeComboBox, "국민은행");

        }

        private void LoadTranAccountTitCodeComboBoxItems()
        {
            //List<CommonCodeItem> list = Global.GetCommonCodeList("ACNT_TYP_CD");

            //for (int li = 0; li < list.Count; li++)
            //{
            //    string value = list[li].Value.ToString();
            //    string desc = list[li].Desc;

            //    ComboBoxItem item = new ComboBoxItem(desc, value);

            //    TransactionAccountComboBox.Items.Add(item);
            //}

            //if (TransactionAccountComboBox.Items.Count > 0)
            //    TransactionAccountComboBox.Text = "";

            //string query = "CALL SelectTranAccountCodeList ()";
            //DataSet dataSet = DbHelper.SelectQuery(query);

            //if (dataSet == null || dataSet.Tables.Count == 0)
            //{
            //    MessageBox.Show("거래계정과목코드를 가져올 수 없습니다.");
            //    return;
            //}

            //foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            //{
            //    string FNCL_ORGN_CD = dataRow["TRAN_ACNT_TIT_CD"].ToString();
            //    string FNCL_ORGN_NM = dataRow["TRAN_CMPN_NO"].ToString();

            //    ComboBoxItem CUR_CD_item = new ComboBoxItem(FNCL_ORGN_CD, FNCL_ORGN_NM);

            //    TransactionAccountComboBox.Items.Add(CUR_CD_item);
            //}

            //if (TransactionAccountComboBox.Items.Count > 0)
            //    TransactionAccountComboBox.Text = "";
        }

        private void LoadTranCompanyNumberComboBoxItems()
        {
            string query = "CALL SelectTranAccountCodeList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("거래업체번호를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string FNCL_ORGN_CD = dataRow["TRAN_CMPN_NO"].ToString();
                string FNCL_ORGN_NM = dataRow["TRAN_ACNT_TIT_CD"].ToString();

                ComboBoxItem CUR_CD_item = new ComboBoxItem(FNCL_ORGN_CD, FNCL_ORGN_NM);

                TransactionCompanyNoComboBox.Items.Add(CUR_CD_item);
            }

            if (TransactionCompanyNoComboBox.Items.Count > 0)
                TransactionCompanyNoComboBox.Text = "";
        }

        //private void BankNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    BankCodeTextBox.Text = Utils.GetSelectedComboBoxItemValue(BankCodeComboBox);

        //    if (BankCodeComboBox.Text == "")
        //        BankCodeTextBox.Text = "";
        //}

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string ACCT_NO = AccountNoComboBox.Text.Trim();                                  // 계좌번호
            string TRAN_CUST_NM = TransactionCustomerNameTextBox.Text.Trim();                                            // 거래고객명
            string CUR_CD = Utils.GetSelectedComboBoxItemText(CurrencyCodeComboBox);                                 // 통화코드
            string TRAN_DT = TransactionDateTimePicker.Value.ToString("yyyy-MM-dd");                                      // 거래일자
            string DPWH_DVSN_CD = Utils.GetSelectedComboBoxItemValue(DPWH_Division_CD_ComboBox);           // 입출금구분코드
            string DPWH_WON_AMT = DepositWithdrawlWonTextBox.Text.Trim(); ;                                               // 입출금원화금액
            string DPWH_FRCR_AMT = DepositWithdrawalFRCRTextbox.Text.Trim();                                            // 입출금외화금액
            // string TRAN_ACNT_TIT_CD = Utils.GetSelectedComboBoxItemValue(TransactionAccountComboBox);        // 거래계정과목코드
            string TRAN_CMPN_NO = Utils.GetSelectedComboBoxItemValue(TransactionCompanyNoComboBox);       // 거래업체번호
            string REMK_CNTS = REMK_CNTS_TextBox.Text.Trim();                                                                   // 비고내용
            string TRAN_EXRT = TRAN_EXRT_TextBox.Text.Trim();                                                                 // 거래환율
            string BANK_CD = Utils.GetSelectedComboBoxItemText(BankCodeComboBox);

            if (ACCT_NO == "")
            {
                MessageBox.Show("계좌번호는 필수 입력항목입니다.");
                AccountNoComboBox.Focus();
                return false;
            }

            //if (TRAN_CUST_NM == "")
            //{
            //    MessageBox.Show("거래고객명 필수 입력항목입니다.");
            //    TransactionCustomerNameTextBox.Focus();
            //    return false;
            //}

            if (BANK_CD == "")
            {
                MessageBox.Show("은행명(코드)는 필수 입력항목입니다.");
                BankCodeComboBox.Focus();
                return false;
            }

            if (CUR_CD == "")
            {
                MessageBox.Show("통화코드는 필수 입력항목입니다.");
                CurrencyCodeComboBox.Focus();
                return false;
            }

            if (TRAN_DT == "")
            {
                MessageBox.Show("거래일자 필수 입력항목입니다.");
                TransactionDateTimePicker.Focus();
                return false;
            }

            if (DPWH_DVSN_CD == "")
            {
                MessageBox.Show("입출금구분은 필수 입력항목입니다.");
                DPWH_Division_CD_ComboBox.Focus();
                return false;
            }

            if (TRAN_EXRT == "")
            {
                MessageBox.Show("거래환율은 필수 입력항목입니다.");
                TRAN_EXRT_TextBox.Focus();
                return false;
            }

            if (DPWH_WON_AMT == "")
            {
                MessageBox.Show("입출금원화금액 필수 입력항목입니다.");
                DepositWithdrawlWonTextBox.Focus();
                return false;
            }

            if (DPWH_FRCR_AMT == "")
            {
                MessageBox.Show("입출금외화금액 필수 입력항목입니다.");
                DepositWithdrawalFRCRTextbox.Focus();
                return false;
            }

            return true;
        }

        private void CurrencyCodeComboBox_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox));
        }

        private void CurrencyCodeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            {
                TRAN_EXRT_TextBox.Text = "0";
                TRAN_EXRT_TextBox.BackColor = SystemColors.Control;
                TRAN_EXRT_TextBox.ReadOnly = true;
                DepositWithdrawalFRCRTextbox.BackColor = SystemColors.Control;
                DepositWithdrawalFRCRTextbox.ReadOnly = true;
                DepositWithdrawalFRCRTextbox.Text = "0";
                DepositWithdrawlWonTextBox.BackColor = Color.White;
                DepositWithdrawlWonTextBox.ReadOnly = false;
            }
            else
            {
                //DepositWithdrawlWonTextBox.Enabled = false;
                DepositWithdrawlWonTextBox.BackColor = SystemColors.Control;
                DepositWithdrawlWonTextBox.ReadOnly = true;
                DepositWithdrawlWonTextBox.Text = "0";
                TRAN_EXRT_TextBox.BackColor = Color.White;
                TRAN_EXRT_TextBox.ReadOnly = false;
                DepositWithdrawalFRCRTextbox.BackColor = Color.White;
                DepositWithdrawalFRCRTextbox.ReadOnly = false;
            }
        }

        private void AccountNoComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!is_choice_mode) {
                string query = string.Format("CALL SelectAccountNoChangeList ('{0}')", Utils.GetSelectedComboBoxItemText(AccountNoComboBox));

                string BANK_CD = "";
                string CUR_CD = "";

                DataSet dataSet = DbHelper.SelectQuery(query);
                if (dataSet == null || dataSet.Tables.Count == 0) {
                    MessageBox.Show("계좌번호 목록을 가져올 수 없습니다.");
                    return;
                }

                foreach (DataRow dataRow in dataSet.Tables[0].Rows) {
                    BANK_CD = dataRow["BANK_CD"].ToString();
                    CUR_CD = dataRow["CUR_CD"].ToString();
                }

                Utils.SelectComboBoxItemByValue(BankCodeComboBox, BANK_CD);
                Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD);
            }
        }

        private void DPWH_CNMB_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (DPWH_CNMB_TextBox.Text.Equals(""))
                is_choice_mode = false;
        }

        private void DepositWithdrawalFRCRTextbox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        private void TRAN_EXRT_TextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        private void SearchAccountNoComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Utils.GetSelectedComboBoxItemText(SearchAccountNoComboBox);
        }
    }
}
