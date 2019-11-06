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
using TripERP.CustomerMgt;

namespace TripERP.ReservationMgt
{
    public partial class PopUpDepositMgt : Form
    {
        enum eDepositDataGridView { DPWH_CNMB = 0, TOT_RECT_AMT = 5 };
        private string _saveMode = "";
        private string _relatedAccountDepositWithdrawalNumber = "";
        private Int32 _accountBalance = 0;

        public string _productNo = "";
        public string _ticketterCompanyNo = "";
        public string _customerNumber = "";

        double _WON_TOT_SALE_AMT = 0;                 // 원화총판매금액
        double _reservationTotalReceiptAmount = 0;    // 예약기본의 총입금금액
        double _reservationWonPayableFee = 0;         // 예약기본의 원화지급수수료
        double _beforeReceiveAmount = 0;              // 그리드에서 선택한 행의 입금금액
        double _thisTimeDepositAmount = 0;            // 입력폼에서 새로 입력한 입금금액

        public double _totalReceiveAmount { get; set; }            // 총입금액
        public double _reservationAmount { get; set; }             // 예약금
        public double _prepaidAmount { get; set; }                 // 선금
        public double _midPayAmount { get; set; }                  // 중도금
        public double _unpaidBalance { get; set; }                 // 잔금
        public double _wonPaymentFee { get; set; }                 // 원화지급수수료
        public bool _receivedFeeYn { get; set; }                   // 위탁판매지급수수료 입금처리 여부
        public bool _socialMarketSales { get; set; }                // 소셜업체, B2B 업체 구분 (수수료 경고 관련)
        public string _COOP_CMPN_DVSN_CD { get; set; }      // 업체구분코드

        public ReservationDetailInfoMgt parent;

        int rate_count = 0;         // 입금관리 Open 시 수수료 경고 알림 Check (없애면 경고 2번뜸)

        public PopUpDepositMgt()
        {
            InitializeComponent();
        }




        //=====================================================================================================
        // 폼 로딩시
        //=====================================================================================================
        private void PopUpDepositMgt_Load(object sender, EventArgs e)
        {
            //InitControls();

            // Common code 아이템 로드
            // 현금대체, 입금유형
            LoadCommonCodeItems();

            // 상대계정 콤보박스 아이템 로드
            //LoadRelativeAccountComboBoxItems();

            if (searchReservationNumberTextBox.Text.Trim() != "")
            {
                LoadDepositList();
                _customerNumber = parent.reservationNumberTextBox.Text.Trim();              // 고객번호를 세팅해주어야 저장시 입금처리 진행됨... (박현호)                
                if (_saveMode == Global.SAVEMODE_ADD)
                {
                    SetReservationDetail();
                }

                if (remainedAmountTextBox.Text.Trim().Equals("0"))
                {
                    MessageBox.Show("입금이 완료된 예약건입니다.");
                }

                // 처리 구분 기본 Setting
                // 모객업체코드별로 처리구분 Setting 다르게 (10 : 소셜, 11:B2B, 1:직판)
                if (_COOP_CMPN_DVSN_CD.Equals("10"))
                {
                    relativeAccountComboBox.SelectedIndex = 0;
                }else if (_COOP_CMPN_DVSN_CD.Equals("11"))
                {
                    relativeAccountComboBox.SelectedIndex = 2;
                }
                else
                {
                    relativeAccountComboBox.SelectedIndex = 1;
                }
                
            }
        }





        //=====================================================================================================
        // 컨트롤러 초기화
        //=====================================================================================================
        private void InitControls()
        {
            //DataGridView dataGridView1 = depositDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
        }









        //=====================================================================================================
        // 예약 상세정보 세팅
        //=====================================================================================================
        private void SetReservationDetail()
        {
            if (searchReservationNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("예약번호를 입력해 주십시오.");
                searchReservationNumberTextBox.Focus();
                return;
            }

            if (DbHelper.GetValue(string.Format("CALL IsValidReservationNumber('{0}')", searchReservationNumberTextBox.Text.Trim()), "RSVT_NO", "0") == "0")
            {
                MessageBox.Show("올바른 예약번호가 아닙니다. 예약번호를 다시 입력해 주십시오.");
                searchReservationNumberTextBox.Focus();
                return;
            }

            string query = string.Format("CALL SelectRsvtItem ( '{0}' )", searchReservationNumberTextBox.Text.Trim());
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약상세정보를 가져올 수 없습니다.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            double remainedAmount = Double.Parse(dataRow["UNAM_BAL"].ToString());
            _reservationTotalReceiptAmount = double.Parse(dataRow["WON_TOT_RECT_AMT"].ToString());
            _reservationWonPayableFee = double.Parse(dataRow["WON_PYMT_FEE"].ToString());         // 위탁판매지급수수료

            if (remainedAmount == 0)
            {
                MessageBox.Show("입금이 완료된 예약건입니다.");
            }

            _wonPaymentFee = double.Parse(dataRow["WON_PYMT_FEE"].ToString());                     // 위탁판매지급수수료

            wonPayableFeeTextBox.Text = Utils.SetComma(_wonPaymentFee.ToString());

            _customerNumber = dataRow["CUST_NO"].ToString();                                       // 고객번호
            searchBookerNameTextBox.Text = dataRow["CUST_NM"].ToString();                          // 예약자
            bookerNameTextBox.Text = dataRow["CUST_NM"].ToString();                                // 예약자
            reservationNumberTextBox.Text = searchReservationNumberTextBox.Text.Trim();            // 주문번호 
            productNameTextBox.Text = dataRow["PRDT_NM"].ToString();                               // 상품
            depatureDateTextBox.Text = dataRow["DPTR_DT"].ToString().Substring(0, 10);             // 출발일자

            salesPriceTextBox.Text = Utils.SetComma(dataRow["SALE_SUM_AMT"].ToString());           // 판매금액
            totalDepositPriceTextBox.Text = Utils.SetComma(dataRow["TOT_RECT_AMT"].ToString());    // 총입금금액
            reservationPriceTextBox.Text = Utils.SetComma(dataRow["RSVT_AMT"].ToString());         // 예약금액
            partPriceTextBox.Text = Utils.SetComma(dataRow["MDPY_AMT"].ToString());                // 중도금금액
            remainedAmountTextBox.Text = Utils.SetComma(dataRow["UNAM_BAL"].ToString());           // 미수금잔액
            

            _productNo = dataRow["PRDT_CNMB"].ToString();                                          // 상품일련번호
            _ticketterCompanyNo = dataRow["TKTR_CMPN_NO"].ToString();                              // 모객업체번호
        }











        //=====================================================================================================                
        // 공통코드 테이블을 검색하여 콤보박스 값 설정
        //=====================================================================================================
        private void LoadCommonCodeItems()
        {
            // 현금대체, 입금유형
            string[] groupNameArray = { "CASH_TRNS_DVSN_CD", "DPWH_TYP_CD", "RECT_PROC_DVSN_CD" };

            ComboBox[] comboBoxArray = { cashTransferDivisionComboBox, depositTypeComboBox, relativeAccountComboBox };


            for (int gi = 0; gi < groupNameArray.Length; gi++)
            {
                comboBoxArray[gi].Items.Clear();

                List<CommonCodeItem> list = Global.GetCommonCodeList(groupNameArray[gi]);

                //comboBoxArray[i].Items.Add(new ComboBoxItem("전체", ""));
                for (int li = 0; li < list.Count; li++)
                {
                    string value = list[li].Value.ToString();
                    string desc = list[li].Desc;

                    ComboBoxItem item = new ComboBoxItem(desc, value);

                    comboBoxArray[gi].Items.Add(item);
                }

                if (comboBoxArray[gi].Items.Count > 0)
                {
                    switch (groupNameArray[gi])
                    {
                        case "CASH_TRNS_DVSN_CD":
                            comboBoxArray[gi].SelectedIndex = 1;
                            break;
                        case "DPWH_TYP_CD":
                            comboBoxArray[gi].SelectedIndex = 0;
                            break;
                        case "RECT_PROC_DVSN_CD":
                            comboBoxArray[gi].SelectedIndex = 0;
                            break;
                    }
                }
            }
        }







        // 미사용: 입금처리구분코드로 변경 (1: 소셜, 2: 직판, 3: 추가, 4: 에누리) - 2019-08-14 hih, 현업 실무 협의 결과 반영
        private void LoadRelativeAccountComboBoxItems()
        {
            relativeAccountComboBox.Items.Clear();

            string query = "CALL SelectAcntTitList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상대계정 정보를 가져올 수 없습니다.");
                return;
            }

            //productComboBox.Items.Add(new ComboBoxItem("전체", -1));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string ACNT_TIT_CD = dataRow["ACNT_TIT_CD"].ToString();
                string ACNT_TIT_NM = dataRow["ACNT_TIT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(ACNT_TIT_NM, ACNT_TIT_CD);

                relativeAccountComboBox.Items.Add(item);
            }

            if (relativeAccountComboBox.Items.Count > 0)
                relativeAccountComboBox.SelectedIndex = 0;
        }







        //=====================================================================================================                        
        // 입금내역목록 조회
        //=====================================================================================================
        private void searchBookerButton_Click(object sender, EventArgs e)
        {
            searchDepositList();
        }

        private void LoadDepositList()
        {
            if (searchReservationNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("예약번호를 입력해 주십시오.");
                searchReservationNumberTextBox.Focus();
                return;
            }

            if (DbHelper.GetValue(string.Format("CALL IsValidReservationNumber('{0}')", searchReservationNumberTextBox.Text.Trim()), "RSVT_NO", "0") == "0")
            {
                MessageBox.Show("올바른 예약번호가 아닙니다. 예약번호를 다시 입력해 주십시오.");
                searchReservationNumberTextBox.Focus();
                return;
            }

            searchDepositList();

        }







        //=====================================================================================================
        // 입금내역 조회
        //=====================================================================================================
        private void searchDepositList()
        {
            if (searchReservationNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("예약번호를 입력해 주십시오.");
                searchReservationNumberTextBox.Focus();
                return;
            }

            depositDataGridView.Rows.Clear();

            string query = string.Format("CALL SelectDepositList ('{0}', '{1}')", searchReservationNumberTextBox.Text.Trim(), (int)Global.eDepositCode.deposit);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("입금처리 정보를 가져올 수 없습니다.");
                return;
            }

            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string DPWH_CNMB = dataRow["DPWH_CNMB"].ToString();                           // 입출금일련번호
                string TRAN_DTM = dataRow["TRAN_DTM"].ToString();                             // 거래일시
                string RSVT_NO = dataRow["RSVT_NO"].ToString();                               // 예약번호
                string CUST_NM = dataRow["CUST_NM"].ToString();                               // 예약자고객명
                string RMTR_NM = dataRow["RMTR_NM"].ToString();                               // 입금인
                string DPWH_AMT = Utils.SetComma(dataRow["DPWH_AMT"].ToString());             // 입출금금액
                string CASH_TRNS_DVSN_CD = dataRow["CASH_TRNS_DVSN_CD"].ToString();           // 현금대체구분코드
                string CASH_TRNS_DVSN_NM = dataRow["CASH_TRNS_DVSN_NM"].ToString();           // 현금대체구분명
                string DPWH_TYP_CD = dataRow["DPWH_TYP_CD"].ToString();                       // 입출금유형코드
                string DPWH_TYP_NM = dataRow["DPWH_TYP_NM"].ToString();                       // 입출금유형명
                string RECT_PROC_DVSN_CD = dataRow["RECT_PROC_DVSN_CD"].ToString();           // 입금처리구분코드
                string RECT_PROC_DVSN_NM = dataRow["RECT_PROC_DVSN_NM"].ToString();           // 입금처리구분명
                //string ACNT_TIT_CD = dataRow["ACNT_TIT_CD"].ToString();                     // 계정과목코드
                //string ACNT_TIT_NM = dataRow["ACNT_TIT_NM"].ToString();                     // 계정과목명
                string REMK_CNTS = dataRow["REMK_CNTS"].ToString();                           // 비고내용

                /*
                //------------------------------------------------------------------------------------------
                // 암호화 내용 복호화       -->  191024 박현호
                //------------------------------------------------------- -----------------------------------
                CUST_NM = EncryptMgt.Decrypt(CUST_NM, EncryptMgt.aesEncryptKey);                    // 예약자명
                */

                depositDataGridView.Rows.Add
                (
                    DPWH_CNMB,
                    TRAN_DTM,
                    RSVT_NO,
                    CUST_NM,
                    RMTR_NM,
                    DPWH_AMT,
                    CASH_TRNS_DVSN_CD,
                    CASH_TRNS_DVSN_NM,
                    DPWH_TYP_CD,
                    DPWH_TYP_NM,
                    RECT_PROC_DVSN_CD,
                    RECT_PROC_DVSN_NM,
                    //ACNT_TIT_CD, 
                    //ACNT_TIT_NM, 
                    REMK_CNTS
                );
            }

            depositDataGridView.ClearSelection();

            // 입금건수가 하나도 없으면 수수료 입금 처리를 위해 수수료를 계산하여 화면에 표시한다
            if (dataRowList.Count == 0) {
                _receivedFeeYn = false;
                calcDefaultWonPayableFee();
            } else
            {
                _receivedFeeYn = true;
            }
        }









        //=====================================================================================================
        // 예약번호 입력후 엔터키를 누르면 입출금내역과 예약정보를 검색하여 세팅
        //=====================================================================================================
        private void searchReservationNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDepositList();
                SetReservationDetail();
            }
        }









        //=====================================================================================================
        // 초기화버튼 클릭
        //=====================================================================================================
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetFormItems();
        }










        //=====================================================================================================
        // 입력필드 초기화
        //=====================================================================================================
        private void ResetFormItems()
        {
            //depositNoTextBox.Text = "";                                                                           // 입금번호
            //bookerNameTextBox.Text = "";                                                                          // 예약자
            //productNameTextBox.Text = "";                                                                         // 상품
            //depatureDateTextBox.Text = "";                                                                        // 출발일자
            //salesPriceTextBox.Text = "";                                                                          // 판매총액
            //prepaidAmountTextBox.Text = "";                                                                       // 선금금액
            //reservationPriceTextBox.Text = "";                                                                    // 예약금액
            //totalDepositPriceTextBox.Text = "";                                                                   // 총입금액
            //remainedAmountTextBox.Text = "";                                                                      // 잔액
            remitterTextBox.Text = "";                                                                            // 입금인
            depositDateTimePicker.Value = DateTime.Now;                                                           // 입금일자
            cashTransferDivisionComboBox.SelectedIndex = 1;                                                       // 현금대체구분코드
            depositTypeComboBox.SelectedIndex = -1;                                                               // 입금유형
            receiveAmountTextBox.Text = "";                                                                       // 입금금액
            relativeAccountComboBox.SelectedIndex = -1;                                                           // 상대계정
            depositMemoTextBox.Text = "";                                                                         // 비고내용
        }











        //=====================================================================================================
        // 저장버튼 클릭
        //=====================================================================================================
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (reservationNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("예약번호를 입력해 주십시오.");
                reservationNumberTextBox.Focus();
                return;
            }
            if (depositTypeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("입출금유형코드를 입력해 주십시오.");
                depositTypeComboBox.Focus();
                return;
            }
            if (cashTransferDivisionComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("현금대체구분코드를 입력해 주십시오.");
                cashTransferDivisionComboBox.Focus();
                return;
            }
            if (receiveAmountTextBox.Text.Trim() == "")
            {
                MessageBox.Show("입출금금액을 입력해 주십시오.");
                receiveAmountTextBox.Focus();
                return;
            }
            if (relativeAccountComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("계정과목코드를 입력해 주십시오.");
                relativeAccountComboBox.Focus();
                return;
            }
            if (_customerNumber == "")
            {
                MessageBox.Show("올바른 예약자가 아닙니다. 다시 입력해 주십시오.");
                searchBookerNameTextBox.Focus();
                return;
            }

            string query = "";

            /// 입출금내역 저장
            string RSVT_NO = reservationNumberTextBox.Text.Trim();                                                                                              // 예약번호
            string DPWH_CNMB = depositNoTextBox.Text.Trim();                                                                                                        // 입출금일련번호
            string TRAN_DTM = depositDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                                                      // 거래일시
            string DPWH_DVSN_CD = ((int)Global.eDepositCode.deposit).ToString();                                                                        // 입출금구분코드
            string DPWH_TYP_CD = (depositTypeComboBox.SelectedItem as ComboBoxItem).Value.ToString();                               // 입출금유형코드
            string CASH_TRNS_DVSN_CD = (cashTransferDivisionComboBox.SelectedItem as ComboBoxItem).Value.ToString();      // 현금대체구분코드
            string CUR_CD = "KRW";                                                                                                                                              // 통화코드: 원화로 고정
            string DPWH_AMT = Utils.GetDoubleString(receiveAmountTextBox.Text.Trim());                                                              // 입출금금액
            //string ACNT_TIT_CD = (relativeAccountComboBox.SelectedItem as ComboBoxItem).Value.ToString();                         // 계정과목코드
            string ACNT_TIT_CD = "";
            string RECT_PROC_DVSN_CD = (relativeAccountComboBox.SelectedItem as ComboBoxItem).Value.ToString();              // 입금처리구분코드
            string CUST_NO = _customerNumber;                                                                                                                           // 고객번호
            string RMTR_NM = remitterTextBox.Text.Trim();                                                                                                           // 입금인
            string REMK_CNTS = depositMemoTextBox.Text.Trim();                                                                                                  // 비고내용
            string RLTE_ACCT_DPWH_CNMB = _relatedAccountDepositWithdrawalNumber;                                                             // 관련계좌입출금번호            

            _thisTimeDepositAmount = Double.Parse(DPWH_AMT);                                                                                                // 입금금액

            if (salesPriceTextBox.Text.Trim() == "") salesPriceTextBox.Text = "0";                                                                          // 총판매액
            double totalSaleAmount = Utils.GetDoubleValue(salesPriceTextBox.Text);

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 입출금일련번호가 없으면 신규 생성  
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (DPWH_CNMB == "")
            {
                // 잔금이 0원일 경우와 입금 잔액이 입금금액보다 적을경우 추가로 입금진행이 불가능하게 한다.           --> 박현호
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------- 여기서부터
                string REMAIN_AMT = remainedAmountTextBox.Text.Trim();                 // 잔금                           
                if (REMAIN_AMT == "0")
                {
                    MessageBox.Show("입금 처리가 완료된건입니다\n(미수잔금이 0원입니다.)");
                    return;
                }

                if(int.Parse(Utils.removeComma(REMAIN_AMT)) < int.Parse(Utils.removeComma(DPWH_AMT)))
                {
                    MessageBox.Show("입금 잔액이 입금 금액보다 적습니다.");
                    return;
                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------- 여기까지

                query = string.Format("CALL InsertDpwhItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}', '{10}', '{11}', '{12}', '{13}', '{14}')",
                    RSVT_NO, TRAN_DTM, DPWH_DVSN_CD, DPWH_TYP_CD, CASH_TRNS_DVSN_CD, RECT_PROC_DVSN_CD, CUR_CD, DPWH_AMT, _wonPaymentFee, ACNT_TIT_CD, RLTE_ACCT_DPWH_CNMB, CUST_NO, RMTR_NM, REMK_CNTS, Global.loginInfo.ACNT_ID);
            }
            else
            {
                query = string.Format("CALL UpdateDpwhItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    RSVT_NO, DPWH_CNMB, TRAN_DTM, DPWH_DVSN_CD, DPWH_TYP_CD, CASH_TRNS_DVSN_CD, RECT_PROC_DVSN_CD, CUR_CD, DPWH_AMT, _wonPaymentFee, ACNT_TIT_CD, RLTE_ACCT_DPWH_CNMB, CUST_NO, RMTR_NM, REMK_CNTS, Global.loginInfo.ACNT_ID);
            }

            // 일괄 Update (입출금내역, 예약기본)
            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal != -1)
            {                
                // 입금내역 성공시 저장된 입금정보를 토대로한 예약 변경정보를 불러온다 --> 박현호
                //---------------------------------------------------------------------------------------- 여기서부터
                parent.setReservationDetail();
                //---------------------------------------------------------------------------------------- 여기까지
                
                wonPayableFeeTextBox.Text = "0";
                searchDepositList();                           // 그리드를 최신상태로 갱신                
                MessageBox.Show("입금정보를 저장했습니다.");                
            }
            else
            {
                MessageBox.Show("입금정보를 저장할 수 없습니다.");
            }

            //======================================================================
            // 예약기본 금액 최신정보 검색
            //======================================================================
            //selectReservationPriceInfo();                  // 총입금액, 잔금 최종값 검색

            refreshData();                                          // 데이터 새로고침!!!  --> 박현호
        }












        //=====================================================================================================
        // 입금내역 삭제
        //=====================================================================================================
        private void deleteButton_Click(object sender, EventArgs e)

        {
            if (reservationNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("예약번호를 입력해 주십시오.");
                reservationNumberTextBox.Focus();
                return;
            }

            if (depositDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 항목을 선택해 주십시오.");
                return;
            }

            string RSVT_NO = reservationNumberTextBox.Text.Trim(); // 예약번호

            string DPWH_CNMB = depositDataGridView.SelectedRows[0].Cells[(int)eDepositDataGridView.DPWH_CNMB].Value.ToString();
            string RECT_AMT = depositDataGridView.SelectedRows[0].Cells[(int)eDepositDataGridView.TOT_RECT_AMT].Value.ToString();

            string DPWH_TYP_CD = (depositTypeComboBox.SelectedItem as ComboBoxItem).Value.ToString(); // 입출금유형코드

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = string.Format("CALL DeleteDpwhItem('{0}', '{1}', '{2}')", DPWH_CNMB, RSVT_NO, Global.loginInfo.ACNT_ID);
            queryStringArray[0] = query;

                var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal != -1)
            {
                //ResetFormItems();                              // 입력폼 초기화
                searchDepositList();                           // 그리드를 최신상태로 갱신
                //selectReservationPriceInfo();                  // 총입금액, 잔금 최종값 검색
                MessageBox.Show("입금정보를 삭제했습니다.");
            }
            else
            {
                MessageBox.Show("입금정보를 삭제할 수 없습니다.");
            }

            refreshData();                                          // 데이터 새로고침!!!  --> 박현호
        }















        //=====================================================================================================
        // 고객명으로 고객정보 검색
        //=====================================================================================================
        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            PopUpSearchCustomerInfo form = new PopUpSearchCustomerInfo();
            form.StartPosition = FormStartPosition.CenterParent;
            form.SetCustomerName(searchBookerNameTextBox.Text.Trim());
            form.ShowDialog();

            _customerNumber = form.GetCustomerNumber();
            searchBookerNameTextBox.Text = form.GetCustomerName();
        }

        // 예약번호 DB 검색
        private void reservationNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            searchReservationInfo();
        }

        // 예약기본 테이블 검색하여 상세입력필드에 세팅
        private void searchReservationInfo()
        {
            if (reservationNumberTextBox.Text == "") return;

            string query = string.Format("CALL SelectRsvtItem ( '{0}' )", reservationNumberTextBox.Text.Trim());
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약상세정보를 가져올 수 없습니다.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            _customerNumber = dataRow["CUST_NO"].ToString();                                       // 고객번호
            bookerNameTextBox.Text = dataRow["CUST_NM"].ToString();                                // 예약자
            productNameTextBox.Text = dataRow["PRDT_NM"].ToString();                               // 상품명

            _productNo = dataRow["PRDT_CNMB"].ToString();                                          // 상품일련번호
            _ticketterCompanyNo = dataRow["TKTR_CMPN_NO"].ToString();                              // 모객업체번호

            depatureDateTextBox.Text = dataRow["DPTR_DT"].ToString().Substring(0,10);              // 출발일자
            salesPriceTextBox.Text = Utils.SetComma(dataRow["WON_TOT_SALE_AMT"].ToString());       // 원화총판매금액
            _WON_TOT_SALE_AMT = Double.Parse(dataRow["WON_TOT_SALE_AMT"].ToString());              // 원화총판매금액

            reservationPriceTextBox.Text = Utils.SetComma(dataRow["RSVT_AMT"].ToString());         // 예약금
            prepaidAmountTextBox.Text = Utils.SetComma(dataRow["PRPY_AMT"].ToString());            // 선금
            partPriceTextBox.Text = Utils.SetComma(dataRow["MDPY_AMT"].ToString());                // 중도금
            totalDepositPriceTextBox.Text = Utils.SetComma(dataRow["TOT_RECT_AMT"].ToString());    // 총입금액
            remainedAmountTextBox.Text = Utils.SetComma(dataRow["UNAM_BAL"].ToString());           // 잔금

        }














        //=====================================================================================================
        // 그리드 행 클릭
        //=====================================================================================================
        private void depositDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (depositDataGridView.SelectedRows.Count == 0)
                return;

            string DPWH_CNMB = depositDataGridView.SelectedRows[0].Cells["DPWH_CNMB"].Value.ToString();
            string TRAN_DTM = depositDataGridView.SelectedRows[0].Cells["TRAN_DTM"].Value.ToString();
            string RSVT_NO = depositDataGridView.SelectedRows[0].Cells["RSVT_NO"].Value.ToString();
            string CUST_NM = depositDataGridView.SelectedRows[0].Cells["CUST_NM"].Value.ToString();
            string RMTR_NM = depositDataGridView.SelectedRows[0].Cells["RMTR_NM"].Value.ToString();
            string DPWH_AMT = Utils.SetComma(depositDataGridView.SelectedRows[0].Cells["DPWH_AMT"].Value.ToString());   // 입금금액
            string CASH_TRNS_DVSN_CD = depositDataGridView.SelectedRows[0].Cells["CASH_TRNS_DVSN_CD"].Value.ToString();
            string CASH_TRNS_DVSN_NM = depositDataGridView.SelectedRows[0].Cells["CASH_TRNS_DVSN_NM"].Value.ToString();
            string DPWH_TYP_CD = depositDataGridView.SelectedRows[0].Cells["DPWH_TYP_CD"].Value.ToString();
            string DPWH_TYP_NM = depositDataGridView.SelectedRows[0].Cells["DPWH_TYP_NM"].Value.ToString();
            //string ACNT_TIT_CD = depositDataGridView.SelectedRows[0].Cells["ACNT_TIT_CD"].Value.ToString();
            //string ACNT_TIT_NM = depositDataGridView.SelectedRows[0].Cells["ACNT_TIT_NM"].Value.ToString();
            //string RECT_PROC_DVSN_CD = depositDataGridView.SelectedRows[0].Cells["RECT_PROC_DVSN_CD"].Value.ToString();
            //string RECT_PROC_DVSN_NM = depositDataGridView.SelectedRows[0].Cells["RECT_PROC_DVSN_NM"].Value.ToString();
            string REMK_CNTS = depositDataGridView.SelectedRows[0].Cells["REMK_CNTS"].Value.ToString();

            reservationNumberTextBox.Text = RSVT_NO;                                                 // 예약번호
            bookerNameTextBox.Text = CUST_NM;                                                        // 예약자
            remitterTextBox.Text = RMTR_NM;                                                          // 입금인명
            receiveAmountTextBox.Text = DPWH_AMT;                                                    // 입금금액
            depositDateTimePicker.Text = TRAN_DTM;                                                   // 입금일자
            Utils.SelectComboBoxItemByValue(cashTransferDivisionComboBox, CASH_TRNS_DVSN_CD);        // 현금대체구분코드
            Utils.SelectComboBoxItemByValue(depositTypeComboBox, DPWH_TYP_CD);                       // 입금유형
            //Utils.SelectComboBoxItemByValue(relativeAccountComboBox, ACNT_TIT_CD);                   // 상대계정코드
            //Utils.SelectComboBoxItemByValue(relativeAccountComboBox, RECT_PROC_DVSN_CD);             // 입금처리구분코드
            depositNoTextBox.Text = DPWH_CNMB;                                                       // 입출금일련번호

            _beforeReceiveAmount = Double.Parse(DPWH_AMT);                                           // 변경전입금금액

            // 예약정보 검색 및 입력필드 설정
            searchReservationInfo();
        }










        //========================================================================================================
        // 엔터키를 누르면 입금인 목록 팝업을 띄워 선택할 수 있도록 함
        //========================================================================================================
        private void depositCustomerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showPopUpRemitterList();
            }
        }

        private void showPopUpRemitterList()
        {
            PopUpRemitterList form = new PopUpRemitterList(this);

            form.setRemitterName(remitterTextBox.Text.Trim());

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            remitterTextBox.Text = form.getRemitterName();
            _relatedAccountDepositWithdrawalNumber = form.getDepositWithdrawalNumber();
            _accountBalance = form.getDepositBalance();

            if(remitterTextBox.Text == "" && _relatedAccountDepositWithdrawalNumber == null)
            {
                MessageBox.Show("선택한 입금정보가 없습니다.");
                return;
            }

            if (remainedAmountTextBox.Text == "") remainedAmountTextBox.Text = "0";

            double remainedAmount = double.Parse(remainedAmountTextBox.Text.ToString());
            double netReceiveAmount = 0;

            if (_receivedFeeYn == true)
            {
                netReceiveAmount = remainedAmount;
            }
            else
            {
                netReceiveAmount = remainedAmount - _wonPaymentFee;
            }

            receiveAmountTextBox.Text = Utils.SetComma(netReceiveAmount.ToString());

        }













        //========================================================================================================
        // 입금인정보 세팅
        //========================================================================================================
        public void setRemitterInfo(string DPWH_CNMB, string TRAN_CUST_NM)
        {
            remitterTextBox.Text = TRAN_CUST_NM;
            // 당좌계좌입출금내역에서 입금금액의 잔액을 관리할 것인지 체크 필요
        }

        public double GetTotalDepositPrice()
        {
            return _totalReceiveAmount;
        }

        public double GetReservationPrice()
        {
            return _reservationAmount;
        }


        public double GetPrepaidPrice()
        {
            return _prepaidAmount;
        }

        public double GetPartPrice()
        {
            return _midPayAmount;
        }

        public double GetBalanceAmount()
        {
            return _unpaidBalance;
        }

        public double GetConsignmentSaleFee()
        {
            return _wonPaymentFee;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {               
            this.Close();
        }

        public void SetSaveMode(string saveMode)
        {
            _saveMode = saveMode;
        }

        public void SetReservationNumber(string reservationNumber)
        {
            searchReservationNumberTextBox.Text = reservationNumber;
            reservationNumberTextBox.Text = reservationNumber;
        }

        public void SetBookerName(string bookerName)
        {
            searchBookerNameTextBox.Text = bookerName;
            bookerNameTextBox.Text = bookerName;
        }

        public void SetProductName(string productName)
        {
            productNameTextBox.Text = productName;
        }

        public void SetDepatureDate(string depatureDate)
        {
            depatureDateTextBox.Text = depatureDate;
        }

        public void SetSalesPriceTextBox(string salesPrice)
        {
            salesPriceTextBox.Text = salesPrice;
        }

        public void SetReservationPriceTextBox(string reservationPrice)
        {
            reservationPriceTextBox.Text = reservationPrice;
        }

        public void SetPartPriceTextBox(string partPrice)
        {
            partPriceTextBox.Text = partPrice;
        }

        public void SetSocialMarketSales(bool isSocial)
        {
            _socialMarketSales = isSocial;
        }

        public void SetCooperationDivisionCode(string divisionCode)
        {
            _COOP_CMPN_DVSN_CD = divisionCode;
        }



        //========================================================================================================
        // 지급수수료 세팅
        //========================================================================================================
        public void SetWonPayableFeeTextBox(string wonPayableFee)
        {
            wonPayableFeeTextBox.Text = wonPayableFee;
            _wonPaymentFee = Double.Parse(wonPayableFee);
        }

        public void SetDepositPriceTextBox(string depositPrice)
        {
            totalDepositPriceTextBox.Text = depositPrice;
        }





        //========================================================================================================
        // 미수잔액 세팅
        //========================================================================================================
        public void SetOutstandingPriceTextBox(string outstandingPrice)
        {
            remainedAmountTextBox.Text = outstandingPrice;
        }




        //========================================================================================================
        // 상품일련번호 세팅
        //========================================================================================================
        public void SetProductNo(string productNo)
        {
            _productNo = productNo;
        }




        //========================================================================================================
        // 모객업체번호 세팅
        //========================================================================================================
        public void SetTicketterCompanyNo(string ticketterCompanyNo)
        {
            _ticketterCompanyNo = ticketterCompanyNo;
        }


        //========================================================================================================
        // 입금잔액 세팅 ~~ 19. 09. 19 배장훈
        //========================================================================================================
        public void SetReceiveAmount(string receiveAmount) {
            receiveAmountTextBox.Text = receiveAmount;
        }

        public void SetReceivedFeeYn(bool receivedFeeYn)
        {
            _receivedFeeYn = receivedFeeYn;
        }


        //========================================================================================================
        // 호출창으로 고객번호를 전달하기 위한 고객번호 세팅
        //========================================================================================================
        public void SetCustomerNumber(string customerNumber)
        {
            _customerNumber = customerNumber;
        }



        //=====================================================================================================================================================================
        // 수수료 기본값 버튼 클릭하면 요율기본 테이블에 설정된 수수료율을 적용하여 수수료를 자동 계산
        //=====================================================================================================================================================================
        private void calcDefaultFeeButton_Click(object sender, EventArgs e)
        {
            calcDefaultWonPayableFee();
        }









       
        //=====================================================================================================================================================================
        // 수수료 기본값 계산
        //=====================================================================================================================================================================
        private void calcDefaultWonPayableFee()
        {
            // 수수료 입금처리가 완료된 건은 중복 처리 방지
            if (_receivedFeeYn == true) return;

            if (salesPriceTextBox.Text == totalDepositPriceTextBox.Text)
            {
                _wonPaymentFee = 0;
                wonPayableFeeTextBox.Text = "0";
                return;
            }

            if (relativeAccountComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("처리구분을 선택하세요.");
                relativeAccountComboBox.Focus();
                return;
            }

            if (salesPriceTextBox.Text == "" || salesPriceTextBox.Text == "0")
            {
                MessageBox.Show("총판매액이 없습니다. 예약상세내용을 확인하세요.");
                return;
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 원화지급수수료 계산 (입금처리구분이 소셜인 경우에만 수수료 계산)
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
            string RECT_PROC_DVSN_CD = Utils.GetSelectedComboBoxItemValue(relativeAccountComboBox);
            if (RECT_PROC_DVSN_CD.Equals("1"))
            {
                string RATE_CD = "01";                                                                               // 위탁판매수수료율
                string query = string.Format("CALL SelectRateItem({0},'{1}','{2}')", _productNo, _ticketterCompanyNo, RATE_CD);
                DataSet dataSet = DbHelper.SelectQuery(query);
                int rowCount = dataSet.Tables[0].Rows.Count;          

                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBox.Show("요율정보를 검색할 수 없습니다. 운영담당자에게 연락하세요.");
                }
                else
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    double RATE = Double.Parse(dataRow["RATE"].ToString());
                   
                    if (RATE == 0.00)
                    {
                        if (_socialMarketSales)
                        {
                            if (rate_count == 0)
                            {
                                rate_count++;
                                MessageBox.Show("적용가능한 수수료 정보가 존재하지 않습니다.\n등록 수수료 정보를 확인하세요!");
                            }
                            else
                            {
                                rate_count = 0;
                            }
                        }
                    }

                    if (salesPriceTextBox.Text.Trim() == "") salesPriceTextBox.Text = "0";                            // 총판매액
                    double totalSaleAmount = Utils.GetDoubleValue(salesPriceTextBox.Text);

                    _wonPaymentFee = Math.Round(totalSaleAmount * RATE / 100, 0);
                    wonPayableFeeTextBox.Text = Utils.SetComma(_wonPaymentFee.ToString());
                }
            }
        }













        // 입금인 검색 버튼
        private void callPopUpDepositMgtButton_Click(object sender, EventArgs e)
        {
            showPopUpRemitterList();
        }

        // 소셜 판매분인 경우 위탁판매지급수수료를 계산
        private void relativeAccountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (relativeAccountComboBox.SelectedIndex == 0 && _wonPaymentFee == 0)
            {
                calcDefaultWonPayableFee();
            }
            else
            {
                wonPayableFeeTextBox.Text = "";
                _wonPaymentFee = 0;
            }
        }




        //===================================================================================
        // 입금이나 입금내역 삭제후 내용 갱신 --> 박현호
        //===================================================================================
        public void refreshData()
        {
            string rsvtSelect = "SELECT * FROM TB_RSVT_M WHERE RSVT_NO = '" + searchReservationNumberTextBox.Text.Trim() + "'";
            DataSet ds = DbHelper.SelectQuery(rsvtSelect);
            DataRow row = ds.Tables[0].Rows[0];
            string WON_TOT_SALE_AMT = row["WON_TOT_SALE_AMT"].ToString().Trim();        // 총판매액
            string TOT_RECT_AMT = row["TOT_RECT_AMT"].ToString().Trim();                // 총입금액
            string PRPY_AMT = row["PRPY_AMT"].ToString().Trim();                        // 선금
            string RSVT_AMT = row["RSVT_AMT"].ToString().Trim();                        // 예약금
            string MDPY_AMT = row["MDPY_AMT"].ToString().Trim();                        // 중도금
            string UNAM_BAL = row["UNAM_BAL"].ToString().Trim();                        // 잔금

            salesPriceTextBox.Text = Utils.SetComma(WON_TOT_SALE_AMT.ToString());      // 총판매금액
            totalDepositPriceTextBox.Text = Utils.SetComma(TOT_RECT_AMT.Substring(0, TOT_RECT_AMT.LastIndexOf(".")));       // 총입금금액
            prepaidAmountTextBox.Text = Utils.SetComma(PRPY_AMT.Substring(0, PRPY_AMT.LastIndexOf(".")));                   // 선금금액
            reservationPriceTextBox.Text = Utils.SetComma(RSVT_AMT.Substring(0, RSVT_AMT.LastIndexOf(".")));                // 예약금액
            partPriceTextBox.Text = Utils.SetComma(MDPY_AMT.Substring(0, MDPY_AMT.LastIndexOf(".")));                       // 중도금액
            remainedAmountTextBox.Text = Utils.SetComma(UNAM_BAL.Substring(0, UNAM_BAL.LastIndexOf(".")));                  // 잔금
            receiveAmountTextBox.Text = "";                                                                                                                                 // 입금금액
            remitterTextBox.Text = "";                                                                                                                                          // 입금인   
            depositNoTextBox.Text = "";                                                                                                                                         // 입금번호     
        }





















        //*********************************************************************************************************************
        // 미사용 Code
        //*********************************************************************************************************************
        /*
        // 예약기본 금액 최신정보 검색
        private void selectReservationPriceInfo()
        {
            string RSVT_NO = reservationNumberTextBox.Text.Trim();                                                   // 예약번호
            string query = string.Format("CALL SelectRsvtPriceInfo ('{0}')", RSVT_NO);

            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약금액 최신 정보 검색 중에 오류가 발생했습니다. 운영담당자에게 연락하세요.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            _prepaidAmount = double.Parse(dataRow["PRPY_AMT"].ToString());                          // 선금
            _reservationAmount = double.Parse(dataRow["RSVT_AMT"].ToString());                      // 예약금
            _midPayAmount = double.Parse(dataRow["MDPY_AMT"].ToString());                           // 중도금
            _unpaidBalance = double.Parse(dataRow["UNAM_BAL"].ToString());                          // 잔금
            _totalReceiveAmount = double.Parse(dataRow["TOT_RECT_AMT"].ToString());                 // 총입금액
            _wonPaymentFee = double.Parse(dataRow["WON_PYMT_FEE"].ToString());                      // 원화지급수수료

            prepaidAmountTextBox.Text = Utils.SetComma(dataRow["PRPY_AMT"].ToString());             // 선금
            reservationPriceTextBox.Text = Utils.SetComma(dataRow["RSVT_AMT"].ToString());          // 예약금
            partPriceTextBox.Text = Utils.SetComma(dataRow["MDPY_AMT"].ToString());                 // 중도금
            remainedAmountTextBox.Text = Utils.SetComma(dataRow["UNAM_BAL"].ToString());            // 잔금
            totalDepositPriceTextBox.Text = Utils.SetComma(dataRow["TOT_RECT_AMT"].ToString());     // 총입금액

            if (_unpaidBalance < 0)
            {
                MessageBox.Show("입금금액이 미수잔액 보다 큽니다. 입금금액을 확인하세요.");
                return;
            }
        }
        */
    }
}
