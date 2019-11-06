using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TripERP.Common;
using TripERP.CustomerMgt;

namespace TripERP.ReservationMgt
{
    public partial class UnpaidExpenseMgt : Form
    {
        string _customerNo = "";          // 고객번호 전역변수

        enum eUnpaidListDataGridView
        {
            UNPA_CNMB,                    // 미지급일련번호
            UNPA_DVSN_CD_NM,              // 미지급구분코드명
            UNPA_DVSN_CD,                 // 미지급구분코드
            RSVT_NO,                      // 예약번호
            PRDT_NM,                      // 상품명
            DPTR_DT,                      // 출발일자
            CSPR_CNMB,                    // 원가일련번호
            TRAN_DT,                      // 거래일자
            ARPL_CMPN_NO,                 // 수배처업체번호
            ARPL_CMPN_NM,                 // 수배처업체명
            CUR_CD,                       // 통화코드
            CUR_NM,                       // 통화명
            CUST_NO,                      // 고객번호
            CUST_NM,                      // 고객명
            STEM_BASI_EXRT,               // 가정산기준환율
            STEM_UNPA_FRCR_AMT,           // 가정산미지급외화금액
            STEM_UNPA_WON_AMT,            // 가정산미지급원화금액
            STMT_YN,                      // 정산여부
            STMT_YN_NM,                   // 정산여부명
            STMT_CNMB,                    // 정산일련번호
            REMK_CNTS                     // 비고
        };

        public UnpaidExpenseMgt()
        {
            InitializeComponent();
        }

        private void UnpaidExpenseMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            searchUnpaidCostList();             // form load 시 내역 바로 출력
        }


        private void InitControls()
        {
            // 폼 입력필드 초기화
            ResetInputFormField();

            InitDataGridView();
        }

        // 입력폼 초기화
        private void ResetInputFormField()
        {
            unpaidExpenseNoTextBox.Text = "";                        // 미지급일련번호
            costPriceNoTextBox.Text = "";                            // 원가일련번호
            reservationNoTextBox.Text = "";                          // 예약번호
            customerNameTextBox.Text = "";                           // 예약자명
            departureDateTextBox.Text = "";                          // 출발일자
            productNameTextBox.Text = "";                            // 상품명
            settlementForeignAmountTextBox.Text = "";                // 정산외화금액
            settlementWonAmountTextBox.Text = "";                    // 정산원화금액
            settlementNoTextBox.Text = "";                           // 정산번호
            exchangeRateTextBox.Text = "";                           // 가정산환율
            remarkTextBox.Text = "";                                 // 비고내용

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 모객업체 콤보박스 설정
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            searchTakeTouristCompanyComboBox.Items.Clear();
            searchTakeTouristCompanyComboBox.Text = "";

            string query = "CALL SelectTakeTouristCompanyList()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("모객업체 정보를 가져올 수 없습니다.");
                return;
            }

            string TKTR_CMPN_NO = "ALL";
            string TKTR_CMPN_NM = "전체";
            ComboBoxItem item = new ComboBoxItem(TKTR_CMPN_NM, TKTR_CMPN_NO);

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                TKTR_CMPN_NO = dataRow["CMPN_NO"].ToString();
                TKTR_CMPN_NM = dataRow["CMPN_NM"].ToString();

                item = new ComboBoxItem(TKTR_CMPN_NM, TKTR_CMPN_NO);

                searchTakeTouristCompanyComboBox.Items.Add(item);
            }

            searchTakeTouristCompanyComboBox.SelectedIndex = -1;

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 수배처 콤보박스 설정
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            arrangementCompanyNoComboBox.Items.Clear();
            arrangementCompanyNoComboBox.Text = "";

            query = "CALL SelectDestinationCompanyList()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("수배처 정보를 가져올 수 없습니다.");
                return;
            }

            string CMPN_NO = "ALL";
            string CMPN_NM = "전체";
            item = new ComboBoxItem(CMPN_NM, CMPN_NO);

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                CMPN_NO = dataRow["CMPN_NO"].ToString();
                CMPN_NM = dataRow["CMPN_NM"].ToString();

                item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                arrangementCompanyNoComboBox.Items.Add(item);
                searchCooperativeCompanyComboBox.Items.Add(item);
            }

            arrangementCompanyNoComboBox.SelectedIndex = -1;
            searchCooperativeCompanyComboBox.SelectedIndex = -1;

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 통화코드
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            currencyCodeComboBox.Items.Clear();
            currencyCodeComboBox.Text = "";

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

                item = new ComboBoxItem(CUR_NM, CUR_CD);

                currencyCodeComboBox.Items.Add(item);
            }

            currencyCodeComboBox.SelectedIndex = -1;

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 미지급/선급구분 콤보박스 설정
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            payableDivisionCodeComboBox.Items.Clear();

            List<CommonCodeItem> list = Global.GetCommonCodeList("UNPA_DVSN_CD");

            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                item = new ComboBoxItem(desc, value);

                payableDivisionCodeComboBox.Items.Add(item);
            }

            // 미지급비용을 기본값으로 설정
            if (payableDivisionCodeComboBox.Items.Count > 0)
                payableDivisionCodeComboBox.SelectedIndex = 0;

        }

        // 그리드 초기화
        private void InitDataGridView()
        {

            DataGridView dataGridView1 = unpaidListDataGridView;
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

        // 등록된 미지급금내역 목록 조회
        private void searchUnpaidCostList()
        {
            unpaidListDataGridView.Rows.Clear();

            string UNPA_CNMB = "";                                                                      // 미지급일련번호
            string UNPA_DVSN_CD = searchReservationNumberTextBox.Text.Trim();                           // 미지급구분코드 (1: 미지급비용, 2: 선급비용)
            string UNPA_DVSN_CD_NM = searchReservationNumberTextBox.Text.Trim();                        // 미지급구분코드명 (1: 미지급비용, 2: 선급비용)
            string RSVT_NO = searchReservationNumberTextBox.Text.Trim();                                // 예약번호

            string CSPR_CNMB = "";                                                                      // 원가일련번호
            string TRAN_DT_FROM = searchStartTranDateTimePicker.Text.Trim();                            // 거래일자FROM
            string TRAN_DT_TO = searchEndTranDateTimePicker.Text.Trim();                                // 거래일자To
            string TRAN_DT = "";                                                                        // 거래일자

            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(searchTakeTouristCompanyComboBox); // 모객업체번호
            string ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(searchCooperativeCompanyComboBox); // 수배처업체번호
            string ARPL_CMPN_NM = "";                                                                   // 수배처업체명
            string CUR_CD = "";                                                // 통화코드
            string CUR_NM = "";                                                // 통화명
            string CUST_NO = "";                                               // 고객번호
            string CUST_NM = "";                                               // 고객명
            string STEM_BASI_EXRT = "";                                        // 가정산기준환율
            string STEM_UNPA_FRCR_AMT = "";                                    // 가정산미지급외화금액
            string STEM_UNPA_WON_AMT = "";                                     // 가정산미지급원화금액
            string STMT_YN = "";                                               // 정산여부
            string STMT_YN_NM = "";                                            // 정산여부명
            string STMT_CNMB = "";                                             // 정산일련번호
            string DPTR_DT = "";                                               // 정산여부명
            string PRDT_NM = "";                                               // 상품명
            string REMK_CNTS = "";                                             // 비고내용

            string query = string.Format("CALL SelectUnpaidExpenseList ('{0}','{1}','{2}','{3}','{4}')", TKTR_CMPN_NO, ARPL_CMPN_NO, TRAN_DT_FROM, TRAN_DT_TO, RSVT_NO);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("미지급비용내역을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {

                UNPA_CNMB = datarow["UNPA_CNMB"].ToString();
                UNPA_DVSN_CD = datarow["UNPA_DVSN_CD"].ToString();
                UNPA_DVSN_CD_NM = datarow["UNPA_DVSN_CD_NM"].ToString();
                RSVT_NO = datarow["RSVT_NO"].ToString();
                CSPR_CNMB = datarow["CSPR_CNMB"].ToString();
                TRAN_DT = datarow["TRAN_DT"].ToString().Substring(0, 10);
                CSPR_CNMB = datarow["CSPR_CNMB"].ToString();
                ARPL_CMPN_NO = datarow["ARPL_CMPN_NO"].ToString();
                ARPL_CMPN_NM = datarow["ARPL_CMPN_NM"].ToString();
                CUR_CD = datarow["CUR_CD"].ToString();
                CUR_NM = datarow["CUR_NM"].ToString();
                CUST_NO = datarow["CUST_NO"].ToString();
                CUST_NM = datarow["CUST_NM"].ToString();
                STEM_BASI_EXRT = Utils.SetComma(datarow["STEM_BASI_EXRT"].ToString());
                STEM_UNPA_FRCR_AMT = Utils.SetComma(datarow["STEM_UNPA_FRCR_AMT"].ToString());
                STEM_UNPA_WON_AMT = Utils.SetComma(datarow["STEM_UNPA_WON_AMT"].ToString());
                STMT_YN = datarow["STMT_YN"].ToString();
                STMT_YN_NM = datarow["STMT_YN_NM"].ToString();
                STMT_CNMB = datarow["STMT_CNMB"].ToString();
                DPTR_DT = datarow["DPTR_DT"].ToString().Substring(0,10);
                PRDT_NM = datarow["PRDT_NM"].ToString() + " " + datarow["PRDT_GRAD_NM"].ToString();
                REMK_CNTS = datarow["REMK_CNTS"].ToString();

                unpaidListDataGridView.Rows.Add
                (
                    UNPA_CNMB,                    // 미지급일련번호
                    UNPA_DVSN_CD_NM,
                    UNPA_DVSN_CD,                 // 미지급구분코드
                    RSVT_NO,                      // 예약번호
                    PRDT_NM,                      // 상품명
                    DPTR_DT,                      // 출발일자
                    CSPR_CNMB,                    // 원가일련번호
                    TRAN_DT,                      // 거래일자
                    ARPL_CMPN_NO,                 // 수배처업체번호
                    ARPL_CMPN_NM,                 // 수배처업체명
                    CUR_CD,                       // 통화코드
                    CUR_NM,                       // 통화명
                    CUST_NO,                      // 고객번호
                    CUST_NM,                      // 고객명
                    double.Parse(STEM_BASI_EXRT),               // 가정산기준환율
                    double.Parse(STEM_UNPA_FRCR_AMT),           // 가정산미지급외화금액
                    double.Parse(STEM_UNPA_WON_AMT),            // 가정산미지급원화금액
                    STMT_YN,                      // 정산여부
                    STMT_YN_NM,                   // 정산여부명
                    STMT_CNMB,                    // 정산일련번호
                    REMK_CNTS                     // 비고내용
                );
            }
            unpaidListDataGridView.ClearSelection();
        }

        // 검색버튼 클릭
        private void searchBookerButton_Click(object sender, EventArgs e)
        {
            searchUnpaidCostList();
        }

        // 그리드 행 클릭
        private void unpaidListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (unpaidListDataGridView.SelectedRows.Count == 0)
                return;

            string UNPA_CNMB = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.UNPA_CNMB].Value.ToString();
            string UNPA_DVSN_CD = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.UNPA_DVSN_CD].Value.ToString();
            string RSVT_NO = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.RSVT_NO].Value.ToString();
            string CSPR_CNMB = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.CSPR_CNMB].Value.ToString();
            string TRAN_DT = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.TRAN_DT].Value.ToString();
            string ARPL_CMPN_NO = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.ARPL_CMPN_NO].Value.ToString();
            string ARPL_CMPN_NM = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.ARPL_CMPN_NM].Value.ToString();
            string CUR_CD = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.CUR_CD].Value.ToString();
            string CUR_NM = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.CUR_NM].Value.ToString();
            string CUST_NO = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.CUST_NO].Value.ToString();
            string CUST_NM = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.CUST_NM].Value.ToString();
            string STEM_BASI_EXRT = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.STEM_BASI_EXRT].Value.ToString();
            string STEM_UNPA_FRCR_AMT = Utils.SetComma(unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.STEM_UNPA_FRCR_AMT].Value.ToString());
            string STEM_UNPA_WON_AMT = Utils.SetComma(unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.STEM_UNPA_WON_AMT].Value.ToString());
            string STMT_YN = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.STMT_YN].Value.ToString();
            string STMT_YN_NM = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.STMT_YN_NM].Value.ToString();
            string STMT_CNMB = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.STMT_CNMB].Value.ToString();
            string DPTR_DT = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.DPTR_DT].Value.ToString();                           // 도착일자
            string PRDT_NM = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.PRDT_NM].Value.ToString();                           // 상품명
            string REMK_CNTS = unpaidListDataGridView.SelectedRows[0].Cells[(int)eUnpaidListDataGridView.REMK_CNTS].Value.ToString();                       // 비고내용

            _customerNo = CUST_NO;

            unpaidExpenseNoTextBox.Text = UNPA_CNMB;                                         // 미지급일련번호
            costPriceNoTextBox.Text = CSPR_CNMB;                                             // 원가일련번호
            Utils.SelectComboBoxItemByValue(payableDivisionCodeComboBox, UNPA_DVSN_CD);      // 미지급구분코드
            reservationNoTextBox.Text = RSVT_NO;                                             // 예약번호
            customerNameTextBox.Text = CUST_NM;                                              // 통화코드
            departureDateTextBox.Text = DPTR_DT;                                             // 출발일자
            productNameTextBox.Text = PRDT_NM;                                               // 상품명
            Utils.SelectComboBoxItemByValue(currencyCodeComboBox, CUR_CD);                   // 통화코드
            settlementForeignAmountTextBox.Text = STEM_UNPA_FRCR_AMT;                        // 가정산외화금액
            settlementWonAmountTextBox.Text = STEM_UNPA_WON_AMT;                             // 가정산원화금액
            settlementYnTextBox.Text = STMT_YN_NM;                                           // 정산여부
            settlementNoTextBox.Text = STMT_CNMB;                                            // 정산번호
            Utils.SelectComboBoxItemByValue(arrangementCompanyNoComboBox, ARPL_CMPN_NO);     // 수배처업체명
            exchangeRateTextBox.Text = STEM_BASI_EXRT;                                       // 가정산환율
            remarkTextBox.Text = REMK_CNTS;                                                  // 비고내용
        }

        // 초기화버튼 클릭
        private void resetButton_Click(object sender, EventArgs e)
        {
            // 폼 입력필드 초기화
            ResetInputFormField();
        }

        // 닫기버튼 클릭
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            string UNPA_CNMB = unpaidExpenseNoTextBox.Text.Trim();                                  // 미지급일련번호
            string UNPA_DVSN_CD = Utils.GetSelectedComboBoxItemValue(payableDivisionCodeComboBox);  // 미지급구분코드
            string UNPA_DVSN_CD_NM = Global.GetCommonCodeDesc("UNPA_DVSN_CD", UNPA_DVSN_CD);        // 미지급구분명
            string RSVT_NO = reservationNoTextBox.Text.Trim();                                      // 예약번호
            string CSPR_CNMB = costPriceNoTextBox.Text.Trim();                                      // 원가일련번호
            string TRAN_DT = tranDateDateTimePicker.Text.Trim();                                    // 거래일자
            string ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(arrangementCompanyNoComboBox); // 수배처업체번호
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);               // 통화코드
            string CUST_NO = _customerNo;                                                           // 고객번호

            string STMT_YN = "";                                                                    // 정산여부

            if (settlementYnTextBox.Text.Trim() == "예")
            {
                STMT_YN = "Y";
            }
            else
            {
                STMT_YN = "N";
            }

            string STEM_BASI_EXRT = exchangeRateTextBox.Text.Trim();                                // 가정산기준환율
            string STEM_UNPA_FRCR_AMT = settlementForeignAmountTextBox.Text.Trim();                 // 가정산미지급외화금액
            string STEM_UNPA_WON_AMT = settlementWonAmountTextBox.Text.Trim();                      // 가정산미지급원화금액
            string REMK_CNTS = remarkTextBox.Text.Trim();                                           // 비고
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;
            
            // 미지급번호가 없으면 신규 등록 처리
            if (UNPA_CNMB == "")
            {
                query = string.Format("CALL InsertUnpaItem ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                                RSVT_NO, UNPA_DVSN_CD, CSPR_CNMB, ARPL_CMPN_NO, CUST_NO, CUR_CD, TRAN_DT, STEM_BASI_EXRT, STEM_UNPA_FRCR_AMT, STEM_UNPA_WON_AMT, STMT_YN, REMK_CNTS, FRST_RGTR_ID);
            }
            else
            {
                query = string.Format("CALL UpdateUnpaItem ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
                               UNPA_CNMB, RSVT_NO, UNPA_DVSN_CD, CSPR_CNMB, ARPL_CMPN_NO, CUST_NO, CUR_CD, TRAN_DT, STEM_BASI_EXRT, STEM_UNPA_FRCR_AMT, STEM_UNPA_WON_AMT, STMT_YN, REMK_CNTS, FRST_RGTR_ID);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show(UNPA_DVSN_CD_NM + " 내역을 저장할 수 없습니다.");
                return;
            }
            else
            {
                searchUnpaidCostList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show(UNPA_DVSN_CD_NM + " 내역을 저장했습니다.");
            }

            // 저장 후 입력폼 초기화
            ResetInputFormField();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string UNPA_CNMB = unpaidExpenseNoTextBox.Text.Trim();                                  // 미지급일련번호
            string RSVT_NO = reservationNoTextBox.Text.Trim();                                      // 예약번호
            string CSPR_CNMB = costPriceNoTextBox.Text.Trim();                                      // 원가일련번호
            string TRAN_DT = tranDateDateTimePicker.Text.Trim();                                    // 거래일자
            string ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(arrangementCompanyNoComboBox); // 수배처업체번호
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);               // 통화코드
            string CUST_NO = _customerNo;                                                           // 고객번호
            string STEM_BASI_EXRT = exchangeRateTextBox.Text.Trim();                                // 가정산기준환율
            string STEM_UNPA_FRCR_AMT = settlementForeignAmountTextBox.Text.Trim();                 // 가정산미지급외화금액
            string STEM_UNPA_WON_AMT = settlementWonAmountTextBox.Text.Trim();                      // 가정산미지급원화금액
            string REMK_CNTS = remarkTextBox.Text.Trim();                                           // 비고


            if (CUR_CD == "")
            {
                MessageBox.Show("통화코드를 선택하세요.");
                currencyCodeComboBox.Focus();
                return false;
            }


            if (STEM_BASI_EXRT == "")
            {
                MessageBox.Show("가정산환율은 필수 입력항목입니다.");
                exchangeRateTextBox.Focus();
                return false;
            }

            if (STEM_UNPA_FRCR_AMT == "")
            {
                MessageBox.Show("미지급외화금액은 필수 입력항목입니다.");
                settlementForeignAmountTextBox.Focus();
                return false;
            }


            return true;

        }

        // 미지급외화금액을 입력하면 환율을 적용하여 원화금액을 계산하여 화면에 표시
        private void settlementForeignAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            // 통화코드가 선택되지 않으면 계산 종료
            if (currencyCodeComboBox.SelectedIndex == -1) return;

            // 외화금액이 입력되지 않으면 계산 종료
            if (settlementForeignAmountTextBox.Text.Trim() == "") return;

            // 원화는 환율 계산하지 않고 외화금액을 원화금액 필드로 set
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);               // 통화코드

            if (CUR_CD == "KRW")
            {
                settlementWonAmountTextBox.Text = settlementForeignAmountTextBox.Text.Trim();
            }
            else
            {
                // 환율이 입력되지 않으면 계산 종료
                if (exchangeRateTextBox.Text == "") return;

                double foreignAmt = Double.Parse(settlementForeignAmountTextBox.Text.Trim());
                double exchangeRate = Double.Parse(exchangeRateTextBox.Text.Trim());
                double wonAmt = Math.Round(foreignAmt * exchangeRate);
                settlementWonAmountTextBox.Text = Utils.SetComma(wonAmt.ToString());
            }
        }

        // 삭제버튼 클릭
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string UNPA_CNMB = unpaidExpenseNoTextBox.Text.Trim();                                  // 미지급일련번호

            if (UNPA_CNMB == "")
            {
                MessageBox.Show("목록에서 삭제대상을 선택하십시오.");
                return;
            }

            string UNPA_DVSN_CD = Utils.GetSelectedComboBoxItemValue(payableDivisionCodeComboBox);  // 미지급구분코드
            string UNPA_DVSN_CD_NM = Global.GetCommonCodeDesc("UNPA_DVSN_CD", UNPA_DVSN_CD);        // 미지급구분명

            string query = string.Format("CALL DeleteUnpaItemByPK ('{0}')", UNPA_CNMB);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show(UNPA_DVSN_CD_NM + " 내역을 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                searchUnpaidCostList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show(UNPA_DVSN_CD_NM + " 내역을 삭제했습니다.");
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        // 엑셀 다운로드 버튼 클릭
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

            unpaidListDataGridView.SuspendLayout();

            unpaidListDataGridView.SelectAll();

            if (Directory.Exists(fileDirPath))
            {
                //if (ExcelHelper.gridToExcel(filePath, unpaidListDataGridView) == true)
                if (true == ExcelHelper.ExportExcel(filePath, unpaidListDataGridView))
                    MessageBox.Show(string.Format("{0}\r\n파일을 저장했습니다.", filePath));
                else
                    MessageBox.Show("파일을 저장 할 수 없습니다.");
            }
            else
            {
                MessageBox.Show("잘못된 저장 경로입니다.");
            }

            this.Cursor = Cursors.Default;
            unpaidListDataGridView.ClearSelection();

            unpaidListDataGridView.ResumeLayout();
        }
    }
}
