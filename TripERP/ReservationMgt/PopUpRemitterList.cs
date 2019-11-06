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

namespace TripERP.ReservationMgt
{
    public partial class PopUpRemitterList : Form
    {
        public string _depositWithdrawalNumber { get; set; }                 // 입출금일련번호
        public string _remitterName { get; set; }                           // 거래고객명(입금인명)
        public Int32 _accountBalance { get; set; }                        // 계좌잔액

        private PopUpDepositMgt _parent = null;

        enum eDepositWithdrawalDataGridView
        {
            DPWH_CNMB,       // 입출금일련번호
            BANK_CD,         // 은행코드
            BANK_NM,         // 은행명
            ACCT_NO,         // 계좌번호
            CUR_CD,          // 통화코드
            TRAN_DT,         // 거래일자
            DPWH_DVSN_CD,    // 입출금구분코드 ('1' 입금, '2' 출금)
            DPWH_WON_AMT,    // 입출금원화금액
            DPWH_WON_BAL,    // 입출금원화잔액
            TRAN_CUST_NM     // 거래고객명
        };

        public PopUpRemitterList(PopUpDepositMgt parent) 
        {
            InitializeComponent();
            _parent = parent;
        }

        private void PopUpRemitterList_Load(object sender, EventArgs e)
        {
            InitControls();
            searchDepositList();   // 입금목록 조회
        }

        // 초기화
        private void InitControls()
        {
            // 그리드 스타일 초기화
            InitDataGridView();

            string query = "CALL SelectAllCurrentAccountListGroupByAccount()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("당좌계좌기본정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 상품일련번호와 상품명을 콤보에 설정
                string BANK_CD = dataRow["BANK_CD"].ToString() + "-" + dataRow["ACCT_NO"].ToString();
                string ACCT_NM = dataRow["ACCT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(ACCT_NM, BANK_CD);
                depositTypeComboBox.Items.Add(item);
            }

            depositTypeComboBox.SelectedIndex = -1;
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView1 = depositListDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
            dataGridView1.DoubleBuffered(true);
        }

        public string SetRemitterName(string _remitter)
        {
            searchRemitterTextBox.Text = _remitter;
            return _remitter;
        }

        public string GetRemitterName(string _remitter)
        {
            return _remitter;
        }

        // 검색버튼 클릭
        private void searchDepositListButton_Click(object sender, EventArgs e)
        {
            searchDepositList();
        }

        private void searchDepositList()
        {
            string DPWH_CNMB = "";                                             // 입출금일련번호
            string BANK_CD = "";                                               // 은행코드
            string BANK_NM = "";                                               // 은행명
            string ACCT_NO = "";                                               // 계좌번호
            string CUR_CD = "";                                                // 통화코드
            string TRAN_DT = "";                                               // 거래일자
            string DPWH_WON_AMT = "";                                          // 입출금원화금액
            string DPWH_WON_BAL = "";                                          // 입출금원화잔액
            string TRAN_CUST_NM = "";                                          // 입금인

            depositListDataGridView.Rows.Clear();

            if (depositTypeComboBox.SelectedIndex >= 0)
            {
                string accountNumber1 = Utils.GetSelectedComboBoxItemValue(depositTypeComboBox);  // 계좌
                string[] accountNumber2;
                accountNumber2 = accountNumber1.Split('-');
                BANK_CD = accountNumber2[0];                                // 은행코드
                ACCT_NO = accountNumber2[1];                                // 계좌번호
            }

            TRAN_DT = remittanceDateTimePicker.Text;                        // 거래일자
            TRAN_CUST_NM = searchRemitterTextBox.Text.Trim();               // 입금인

            string query = string.Format("CALL SelectCurrentAccountDepositList ('{0}', '{1}', '{2}', '{3}')", BANK_CD, ACCT_NO, TRAN_DT, TRAN_CUST_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("당좌입출금정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {

                DPWH_CNMB = datarow["DPWH_CNMB"].ToString();
                BANK_CD = datarow["BANK_CD"].ToString();
                BANK_NM = datarow["BANK_NM"].ToString();
                ACCT_NO = datarow["ACCT_NO"].ToString();
                CUR_CD = datarow["CUR_CD"].ToString();
                TRAN_DT = datarow["TRAN_DT"].ToString();
                DPWH_WON_AMT = Utils.SetComma(datarow["DPWH_WON_AMT"].ToString());
                DPWH_WON_BAL = Utils.SetComma(datarow["DPWH_WON_BAL"].ToString());
                TRAN_CUST_NM = datarow["TRAN_CUST_NM"].ToString();

                depositListDataGridView.Rows.Add
                (
                    DPWH_CNMB,
                    TRAN_DT,
                    BANK_CD,
                    BANK_NM,
                    ACCT_NO,
                    DPWH_WON_AMT,
                    DPWH_WON_BAL,
                    TRAN_CUST_NM
                );
            }

            depositListDataGridView.ClearSelection();
        }

        //선택 후 폼 닫기
        private void depositListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool result = returnChoiceValue();
            if (!result)
            {
                return;
            }
            this.Close();
        }

        // 폼 닫기
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //=========================================================================================================================================================================
        // 선택버튼 클릭
        //=========================================================================================================================================================================
        private void choiceButton_Click(object sender, EventArgs e)
        {
            returnChoiceValue();
            this.Close();
        }

        //=========================================================================================================================================================================
        // 그리드 목록 선택 사항 체크 및 리턴 값 설정
        //=========================================================================================================================================================================
        private bool returnChoiceValue()
        {
            if (depositListDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("목록에서 대상을 선택하세요.");
                depositListDataGridView.Focus();
                return false;
            }

            //-----------------------------------------------------------------------------------------------------
            // 현재 입금해야되는 잔금과 계좌 입금건에 대한 잔액을 비교
            // 1) 계좌 입금건 잔액이 0 원이면 잔액부족 처리 불가
            // 2) 계좌 입금건 잔액이 입금해야되는 금액보다 작으면 잔액부족으로 처리 불가
            //-----------------------------------------------------------------------------------------------------
            int WON_BAL = int.Parse(Utils.removeComma(depositListDataGridView.SelectedRows[0].Cells["DPWH_WON_BAL"].Value.ToString().Trim()));      // 계좌 입금건 잔액
            int UNAM_BAL = int.Parse(Utils.removeComma(_parent.remainedAmountTextBox.Text.ToString().Trim()));                                                          // 입금해야되는 금액 
            int CAN_DEPOSIT = WON_BAL - UNAM_BAL;                                                                                                                                                       // 계좌에서 입금액 차감액

            // 계좌 입금건 잔액이 0이면 잔액부족으로 처리 불가
            if (WON_BAL <= 0)
            {
                MessageBox.Show("잔액이 존재하지 않는 입금 건 입니다.");
                return false;
            }
            // 계좌 입금건 잔액에서 입금액 차감시 음수값 나오면 잔액부족으로 처리 불가
            else if(CAN_DEPOSIT < 0)
            {
                MessageBox.Show("잔액이 부족합니다..");
                return false;
            }

            _depositWithdrawalNumber = depositListDataGridView.SelectedRows[0].Cells["DPWH_CNMB"].Value.ToString();
            _remitterName = depositListDataGridView.SelectedRows[0].Cells["TRAN_CUST_NM"].Value.ToString();            
            _accountBalance = Int32.Parse(Utils.GetDoubleString(depositListDataGridView.SelectedRows[0].Cells["DPWH_WON_BAL"].Value.ToString()));

            return true;
    }

        //=========================================================================================================================================================================
        // 팝업창 파라미터 getter/setter
        //=========================================================================================================================================================================
        public void setDepositWithdrawalNumber(string depositWithdrawalNumber)
        {
            _depositWithdrawalNumber = depositWithdrawalNumber;
        }

        public void setRemitterName(string remitterName)
        {
            _remitterName = remitterName;
        }

        public void setAccountBalance(Int32 accountBalance)
        {
            _accountBalance = accountBalance;
        }

        public string getDepositWithdrawalNumber()
        {
            return _depositWithdrawalNumber;
        }

        public string getRemitterName()
        {
            return _remitterName;
        }

        public Int32 getDepositBalance()
        {
            return _accountBalance;
        }
    }
}
