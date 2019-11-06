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

namespace TripERP.AccountMgt
{
    public partial class CurrentAccountMgmt : Form
    {
        enum eCurrentAccountDataGridView { BANK_NM, ACCT_NO, CUR_CD, ACCT_NM, BANK_BZBR_NM, BANK_RPTV_NM, ACCT_OPEN_DT, WON_BAL, FRCR_BAL, USE_YN, BANK_RPTV_PHNE_NO, BANK_RPTV_EMAL_ADDR, BANK_CD };

        string BANK_NM = "";
        string ACCT_NO = "";
        string CUR_CD = "";
        string ACCT_NM = "";
        string BANK_BZBR_NM = "";
        string BANK_RPTV_NM = "";
        string ACCT_OPEN_DT = "";
        string WON_BAL = "";
        string FRCR_BAL = "";
        string USE_YN = "";
        string BANK_RPTV_PHNE_NO = "";
        string BANK_RPTV_EMAL_ADDR = "";
        string BANK_CD = "";
        string[] spEmail;

        public CurrentAccountMgmt()
        {
            InitializeComponent();
        }

        private void CurrentAccountMgmt_Load(object sender, EventArgs e)
        {
            InitControls();
            searchCurrentAccountList();
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
            BankNmTextBox.Text = "";
            BankNameTextBox.Text = "";
            BankRPTVTextBox.Text = "";
            BankRPTVPhoneTextBox.Text = "";
            BankRPTVEmailTextBox.Text = "";
            WonBalTextBox.Text = "";
            FrcrBalTextbox.Text = "";
            AccountNoTextBox.Text = "";
            AccountNoTextBox_Before.Text = "";
            AccountNameTextBox.Text = "";

            ApplyOpenDateTimePicker.Value = DateTime.Now;
        }

        // 콤보박스 초기화
        private void ResetComboBox()
        {
            SearchAccountNameComboBox.Items.Clear();
            SearchAccountNoComboBox.Items.Clear();
            BankRPTVEmailComboBox.Items.Clear();
            CurrencyCodeComboBox.Items.Clear();
            BankRPTVEmailComboBox.Items.Clear();
            BankCodeComboBox.Items.Clear();

            SearchAccountNoComboBox.Items.Add(new ComboBoxItem("전체", ""));
            SearchAccountNameComboBox.Items.Add(new ComboBoxItem("전체", ""));
            CurrencyCodeComboBox.Items.Add(new ComboBoxItem("전체", ""));

            LoadCurrencyCodeComboBoxItems();
            LoadEmailComboBoxItems();
            LoadUseYNComboBoxItems();
            LoadAccountNoComboBoxItems();
            LoadAccountNameComboBoxItems();
            LoadBankCodeComboBoxItems();
        }

        // 데이터 그리드뷰 초기화
        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = CurrentAccountDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            //dataGridView1.RowHeadersVisible = false;
        }

        // 계좌조회버튼 클릭
        private void searchCostPriceListButton_Click(object sender, EventArgs e)
        {
            searchCurrentAccountList();
        }

        // 당좌계좌 목록 조회
        private void searchCurrentAccountList()
        {
            CurrentAccountDataGridView.Rows.Clear();

            BANK_CD = "";                                            // 은행코드
            BANK_NM = "";                                            // 은행명
            ACCT_NO = SearchAccountNoComboBox.Text.Trim();           // 계좌번호
            CUR_CD = "";                                             // 통화코드
            ACCT_NM = SearchAccountNameComboBox.Text.Trim();         // 계좌명
            BANK_BZBR_NM = "";                                       // 은행영업점명
            BANK_RPTV_NM = "";                                       // 은행담당자명
            ACCT_OPEN_DT = "";                                       // 계좌개설일자
            WON_BAL = "";                                            // 원화잔액
            FRCR_BAL = "";                                           // 외화잔액
            USE_YN = "";                                             // 사용여부
            BANK_RPTV_PHNE_NO = "";
            BANK_RPTV_EMAL_ADDR = "";

            if (ACCT_NO.Equals("전체"))
                ACCT_NO = "";
            if (ACCT_NM.Equals("전체"))
                ACCT_NM = "";

            string query = string.Format("CALL SelectCurrentAccountList ('{0}', '{1}')", ACCT_NM, ACCT_NO);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("계좌정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                BANK_CD = datarow["BANK_CD"].ToString();
                BANK_NM = datarow["BANK_NM"].ToString();
                ACCT_NO = datarow["ACCT_NO"].ToString();
                CUR_CD = datarow["CUR_CD"].ToString();
                ACCT_NM = datarow["ACCT_NM"].ToString();
                BANK_BZBR_NM = datarow["BANK_BZBR_NM"].ToString();
                BANK_RPTV_NM = datarow["BANK_RPTV_NM"].ToString();
                ACCT_OPEN_DT = datarow["ACCT_OPEN_DT"].ToString().Substring(0, 10);
                WON_BAL = datarow["WON_BAL"].ToString();
                FRCR_BAL = datarow["FRCR_BAL"].ToString();
                USE_YN = datarow["USE_YN"].ToString();
                BANK_RPTV_PHNE_NO = datarow["BANK_RPTV_PHNE_NO"].ToString();
                BANK_RPTV_EMAL_ADDR = datarow["BANK_RPTV_EMAL_ADDR"].ToString();

                if (USE_YN.Equals("Y"))
                    USE_YN = "●";
                else
                    USE_YN = "";

                CurrentAccountDataGridView.Rows.Add(BANK_NM, ACCT_NO, CUR_CD, ACCT_NM, BANK_BZBR_NM, BANK_RPTV_NM, ACCT_OPEN_DT, double.Parse(WON_BAL), double.Parse(FRCR_BAL), USE_YN, BANK_RPTV_PHNE_NO, BANK_RPTV_EMAL_ADDR, BANK_CD);
            }
            CurrentAccountDataGridView.ClearSelection();
        }

        // 그리드 행 클릭
        private void CurrentAccountDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CurrentAccountDataGridView.SelectedRows.Count == 0)
                return;

            BANK_NM = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.BANK_NM].Value.ToString();
            ACCT_NO = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.ACCT_NO].Value.ToString();
            CUR_CD = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.CUR_CD].Value.ToString();
            ACCT_NM = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.ACCT_NM].Value.ToString();
            BANK_BZBR_NM = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.BANK_BZBR_NM].Value.ToString();
            BANK_RPTV_NM = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_NM].Value.ToString();
            ACCT_OPEN_DT = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.ACCT_OPEN_DT].Value.ToString();
            WON_BAL = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.WON_BAL].Value.ToString();
            FRCR_BAL = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.FRCR_BAL].Value.ToString();
            USE_YN = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.USE_YN].Value.ToString();
            BANK_RPTV_PHNE_NO = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_PHNE_NO].Value.ToString();
            BANK_RPTV_EMAL_ADDR = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_EMAL_ADDR].Value.ToString();
            BANK_CD = CurrentAccountDataGridView.SelectedRows[0].Cells[(int)eCurrentAccountDataGridView.BANK_CD].Value.ToString();
            spEmail = BANK_RPTV_EMAL_ADDR.Split('@');

            CurrentAccountDataGridViewChoice();
        }

        private void CurrentAccountDataGridViewChoice()
        {
            BankNmTextBox.Text = BANK_NM;
            Utils.SelectComboBoxItemByValue(BankCodeComboBox, BANK_CD);
            ApplyOpenDateTimePicker.Text = ACCT_OPEN_DT;
            BankNameTextBox.Text = BANK_BZBR_NM;
            BankRPTVTextBox.Text = BANK_RPTV_NM;
            BankRPTVPhoneTextBox.Text = BANK_RPTV_PHNE_NO;

            BankRPTVEmailTextBox.Text = spEmail[0];
            BankRPTVEmailComboBox.Text = spEmail[1];
            
            WonBalTextBox.Text = WON_BAL;
            AccountNoTextBox.Text = ACCT_NO;
            AccountNoTextBox_Before.Text = ACCT_NO;
            AccountNameTextBox.Text = ACCT_NM;
            if (USE_YN.Equals("●"))
                USE_YN = "Y";
            else
                USE_YN = "N";
            Utils.SelectComboBoxItemByValue(UseYNComboBox, USE_YN);
            Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD); // 판매통화코드 콤보박스

            //if (Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            //    FrcrBalTextbox.BackColor = SystemColors.Control;
            //if (FrcrBalTextbox.BackColor.Equals(SystemColors.Control) && !Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            //    FrcrBalTextbox.BackColor = Color.White;

            FrcrBalTextbox.Text = FRCR_BAL;
        }

        // 초기화 버튼 클릭
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
            ResetComboBox();
        }

        // 저장 버튼 클릭
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Boolean updateFlag = false;

            string BANK_NM = BankNmTextBox.Text.Trim();                                                     // 은행명
            string BANK_CD = Utils.GetSelectedComboBoxItemValue(BankCodeComboBox);           // 은행코드
            string ACCT_NO = AccountNoTextBox.Text.Trim();                                                  // 계좌번호
            string ACCT_NO_BEFORE = AccountNoTextBox_Before.Text.Trim();                                    // 변경전 계좌번호
            string ACCT_NM = AccountNameTextBox.Text.Trim();                                           // 계좌명
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);        // 통화코드
            string BANK_BZBR_NM = BankNameTextBox.Text.Trim();                                        // 은행영업점명  
            string BANK_RPTV_NM = BankRPTVTextBox.Text.Trim();                                         // 은행담당자명
            string ACCT_OPEN_DT = ApplyOpenDateTimePicker.Value.ToString("yyyy-MM-dd");     // 계좌개설일자
            string BANK_RPTV_PHNE_NO = BankRPTVPhoneTextBox.Text.Trim();                    // 은행담당자전화번호
            string BANK_RPTV_EMAL_ADDR = "";

            if (Utils.GetSelectedComboBoxItemText(BankRPTVEmailComboBox).Equals("직접입력"))
            {
                BANK_RPTV_EMAL_ADDR = BankRPTVEmailTextBox.Text.Trim() + "@" + BankRPTVEmailAddOnBox.Text.Trim();
                BankRPTVEmailComboBox.Text = BankRPTVEmailAddOnBox.Text.Trim();
                BankRPTVEmailAddOnBox.Text = "";
                BankRPTVEmailAddOnBox.Visible = false;
            }
            else
                BANK_RPTV_EMAL_ADDR = BankRPTVEmailTextBox.Text.Trim() + "@" + Utils.GetSelectedComboBoxItemText(BankRPTVEmailComboBox);
                

            if (WonBalTextBox.Text.Equals("")) WonBalTextBox.Text = "0";                    // 원화잔액
            double WON_BAL = Utils.GetDoubleValue(WonBalTextBox.Text);
            if (FrcrBalTextbox.Text.Equals("")) FrcrBalTextbox.Text = "0";                     // 외화잔액
            double FRCR_BAL = Utils.GetDoubleValue(FrcrBalTextbox.Text);
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYNComboBox);              // 사용여부

            string FINL_MDFR_ID = Global.loginInfo.ACNT_ID;                         // 최종변경자ID
            string RGST_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");         // 최초+최종 등록일시

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            // 은행명이 채워져있으면 계좌정보 업데이트
            if (BANK_NM != "")
            {
                query = string.Format("CALL UpdateCurrentAccount ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')",
                    BANK_CD, ACCT_NO, ACCT_NO_BEFORE, CUR_CD, ACCT_OPEN_DT, BANK_BZBR_NM, BANK_RPTV_NM, ACCT_NM, BANK_RPTV_PHNE_NO, BANK_RPTV_EMAL_ADDR, WON_BAL, FRCR_BAL, USE_YN, FINL_MDFR_ID);
                updateFlag = true;
            }
            // 은행명이 안채워져있으면 계좌정보 저장
            else
            {
                //-------------------------------------------------------------------------------------------------------------------------------------
                // 정렬번호 채번      --> 191105 박현호 ㅎ
                //-------------------------------------------------------------------------------------------------------------------------------------
                string selectOrderNumber = "SELECT IFNULL(Max(ORDR_NO)+1, 1) AS NEW_ORDER_NUM FROM TB_CRAC_ACCT_M";
                DataSet orderNumber = DbHelper.SelectQuery(selectOrderNumber);
                DataRow row = orderNumber.Tables[0].Rows[0];
                int new_orderNumber = int.Parse(row["NEW_ORDER_NUM"].ToString().Trim());

                query = string.Format("CALL InsertCurrentAccount ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')",
                    BANK_CD, ACCT_NO, CUR_CD, ACCT_OPEN_DT, BANK_BZBR_NM, BANK_RPTV_NM, ACCT_NM, BANK_RPTV_PHNE_NO, BANK_RPTV_EMAL_ADDR, WON_BAL, FRCR_BAL, USE_YN, FINL_MDFR_ID, new_orderNumber);
            }

            
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("계좌를 저장할 수 없습니다.");
                return;
            }
            else
            {
                searchCurrentAccountList(); // 등록 후 그리드를 최신상태로 ReFresh
                if (updateFlag)
                    MessageBox.Show("계좌를 수정했습니다.");
                else
                    MessageBox.Show("계좌를 저장했습니다.");
            }

            // 저장 후 입력폼 초기화
            // ResetInputFormField();
            // ResetComboBox();
            searchCurrentAccountList();
            // Test
        }

        // 삭제 버튼 클릭
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string BANK_CD = Utils.GetSelectedComboBoxItemText(BankCodeComboBox);
            string ACCT_NO = AccountNoTextBox.Text.Trim();

            if (ACCT_NO == "")
            {
                MessageBox.Show("계좌번호 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                BankCodeComboBox.Focus();
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteCurrentAccount ('{0}')", ACCT_NO);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("계좌정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                ResetInputFormField();
                ResetComboBox();
                searchCurrentAccountList();
                MessageBox.Show("계좌정보를 삭제했습니다.");
            }
        }

        // 닫기 버튼
        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string BANK_CD = Utils.GetSelectedComboBoxItemText(BankCodeComboBox);                // 은행코드
            string ACCT_NO = AccountNoTextBox.Text.Trim();                                              // 계좌번호
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);        // 통화코드
            string ACCT_NM = AccountNameTextBox.Text.Trim();                         // 계좌명
            string BANK_BZBR_NM = BankNameTextBox.Text.Trim();                              // 은행영업점명
            string BANK_RPTV_NM = BankRPTVTextBox.Text.Trim();                              // 은행담당자명
            string ACCT_OPEN_DT = ApplyOpenDateTimePicker.Value.ToString("yyyy-MM-dd");     // 계좌개설일자
            string WON_BAL = WonBalTextBox.Text.Trim();                                     // 원화잔액
            string FRCR_BAL = FrcrBalTextbox.Text.Trim();                                   // 외화잔액
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYNComboBox);

            if (BANK_CD == "")
            {
                MessageBox.Show("은행코드는 필수 입력항목입니다.");
                BankCodeComboBox.Focus();
                return false;
            }

            if (ACCT_NO == "")
            {
                MessageBox.Show("계좌번호는 필수 입력항목입니다.");
                AccountNoTextBox.Focus();
                return false;
            }

            if (CUR_CD == "")
            {
                MessageBox.Show("통화코드는 필수 입력항목입니다.");
                CurrencyCodeComboBox.Focus();
                return false;
            }

            if (ACCT_NM == "")
            {
                MessageBox.Show("계좌명은 필수 입력항목입니다.");
                AccountNameTextBox.Focus();
                return false;
            }

            //if (BANK_BZBR_NM == "")
            //{
            //    MessageBox.Show("은행영업점명은 필수 입력항목입니다.");
            //    BankNameTextBox.Focus();
            //    return false;
            //}

            //if (BANK_RPTV_NM == "")
            //{
            //    MessageBox.Show("은행담당자명은 필수 입력항목입니다.");
            //    BankRPTVTextBox.Focus();
            //    return false;
            //}

            //if (ACCT_OPEN_DT == "")
            //{
            //    MessageBox.Show("계좌개설일자는 필수 입력항목입니다.");
            //    ApplyOpenDateTimePicker.Focus();
            //    return false;
            //}
            
            if (WON_BAL == "" && CUR_CD == "KRW")
            {
                MessageBox.Show("원화잔액은 필수 입력항목입니다.");
                WonBalTextBox.Focus();
                return false;
            }

            if (FRCR_BAL == "" && CUR_CD != "KRW")
            {
                MessageBox.Show("외화잔액은 필수 입력항목입니다.");
                FrcrBalTextbox.Focus();
                return false;
            }

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
                UseYNComboBox.Focus();
                return false;
            }

            return true;
        }

        private void LoadAccountNoComboBoxItems()
        {
            string query = "CALL SelectAllCurrentAccountListGroupByAccount ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("계좌번호를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string ACCT_NO = dataRow["ACCT_NO"].ToString();
                string BANK_CD = dataRow["BANK_CD"].ToString();
                string WON_BAL = dataRow["WON_BAL"].ToString();

                ComboBoxItem ACCT_NO_item = new ComboBoxItem(ACCT_NO, BANK_CD);

                SearchAccountNoComboBox.Items.Add(ACCT_NO_item);
            }
            
            if (SearchAccountNoComboBox.Items.Count > 0)
                SearchAccountNoComboBox.SelectedIndex = 0;
        }

        private void LoadAccountNameComboBoxItems()
        {
            string query = "CALL SelectAccountNoComboBoxItems ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("계좌명을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string ACCT_NM = dataRow["ACCT_NM"].ToString();

                ComboBoxItem ACCT_NM_ITEM = new ComboBoxItem(ACCT_NM, "");
                SearchAccountNameComboBox.Items.Add(ACCT_NM_ITEM);
            }
            if (SearchAccountNameComboBox.Items.Count > 0)
                SearchAccountNameComboBox.SelectedIndex = 0;
        }

        private void LoadCurrencyCodeComboBoxItems()
        {
            CurrencyCodeComboBox.Items.Clear();

            string query = "CALL SelectCurList ()";
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
            }

            if (CurrencyCodeComboBox.Items.Count > 0)
                CurrencyCodeComboBox.Text = "";
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

                BankCodeComboBox.Items.Add(item);      // 판매통화코드
            }

            // 거래은행을 국민은행으로 기본값 설정
            Utils.SelectComboBoxItemByText(BankCodeComboBox, "국민은행");
        }

        private void LoadEmailComboBoxItems()
        {
            // 이메일 도메인
            BankRPTVEmailComboBox.Items.Clear();

            List<CommonCodeItem> list = Global.GetCommonCodeList("EMAL_DOMN_ADDR");

            //comboBoxArray[i].Items.Add(new ComboBoxItem("전체", ""));
            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                BankRPTVEmailComboBox.Items.Add(item);
            }

            if (BankRPTVEmailComboBox.Items.Count > 0)
                BankRPTVEmailComboBox.Text = "";
        }

        private void LoadUseYNComboBoxItems()
        {
            string query = "CALL SelectCommoncodeList ('YN')";
            DataSet dataSet = DbHelper.SelectQuery(query);
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
                UseYNComboBox.Items.Add(item);
            }
        }

        // 그리드 방향키 컨트롤
        private void CurrentAccountDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스 
            int rowIndex = CurrentAccountDataGridView.CurrentRow.Index;
            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0) return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && CurrentAccountDataGridView.Rows.Count == rowIndex + 1) return;

            if (e.KeyCode.Equals(Keys.Up))
            {
                BANK_NM = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.BANK_NM].Value.ToString();
                ACCT_NO = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.ACCT_NO].Value.ToString();
                CUR_CD = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.CUR_CD].Value.ToString();
                ACCT_NM = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.ACCT_NM].Value.ToString();
                BANK_BZBR_NM = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.BANK_BZBR_NM].Value.ToString();
                BANK_RPTV_NM = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_NM].Value.ToString();
                ACCT_OPEN_DT = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.ACCT_OPEN_DT].Value.ToString();
                WON_BAL = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.WON_BAL].Value.ToString();
                FRCR_BAL = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.FRCR_BAL].Value.ToString();
                USE_YN = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.USE_YN].Value.ToString();
                BANK_RPTV_PHNE_NO = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_PHNE_NO].Value.ToString();
                BANK_RPTV_EMAL_ADDR = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_EMAL_ADDR].Value.ToString();
                BANK_CD = CurrentAccountDataGridView.Rows[rowIndex - 1].Cells[(int)eCurrentAccountDataGridView.BANK_CD].Value.ToString();
            }
            else if (e.KeyCode.Equals(Keys.Down))
            {
                BANK_NM = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.BANK_NM].Value.ToString();
                ACCT_NO = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.ACCT_NO].Value.ToString();
                CUR_CD = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.CUR_CD].Value.ToString();
                ACCT_NM = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.ACCT_NM].Value.ToString();
                BANK_BZBR_NM = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.BANK_BZBR_NM].Value.ToString();
                BANK_RPTV_NM = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_NM].Value.ToString();
                ACCT_OPEN_DT = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.ACCT_OPEN_DT].Value.ToString();
                WON_BAL = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.WON_BAL].Value.ToString();
                FRCR_BAL = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.FRCR_BAL].Value.ToString();
                USE_YN = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.USE_YN].Value.ToString();
                BANK_RPTV_PHNE_NO = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_PHNE_NO].Value.ToString();
                BANK_RPTV_EMAL_ADDR = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.BANK_RPTV_EMAL_ADDR].Value.ToString();
                BANK_CD = CurrentAccountDataGridView.Rows[rowIndex + 1].Cells[(int)eCurrentAccountDataGridView.BANK_CD].Value.ToString();
            }
            spEmail = BANK_RPTV_EMAL_ADDR.Split('@');

            CurrentAccountDataGridViewChoice();
        }

        private void CurrencyCodeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox).Equals("KRW"))
            //{
            //    WonBalTextBox.BackColor = Color.White;
            //    FrcrBalTextbox.BackColor = SystemColors.Control;
            //}
            //else
            //{
            //    WonBalTextBox.BackColor = SystemColors.Control;
            //    FrcrBalTextbox.BackColor = Color.White;
            //}
        }

        private void BankRPTVEmailComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Utils.GetSelectedComboBoxItemText(BankRPTVEmailComboBox).Equals("직접입력"))
            {
                BankRPTVEmailAddOnBox.Text = "";
                BankRPTVEmailAddOnBox.Visible = true;
            }
            else
            {
                BankRPTVEmailAddOnBox.Text = "";
                BankRPTVEmailAddOnBox.Visible = false;
            }
        }

        private void BankCodeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string BANK_NM = Utils.GetSelectedComboBoxItemValue(BankCodeComboBox);
        }
    }
}
