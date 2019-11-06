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
    public partial class PopUpAccountPayableMgt : Form
    {
        public string _productNo = "";
        public string _productGradeCode = "";
        public long _settlementEstimationWonAmt = 0;
        public string _settlementYn = "";

        enum eUnpaidListDataGridView
        {
            CSPR_CNMB,                    // 원가일련번호
            CSPR_NM,                      // 원가명
            ARPL_CMPN_NO,                 // 수배처업체번호
            ARPL_CMPN_NM,                 // 수배처업체명
            CSPR_CUR_CD,                  // 통화코드
            CSPR_CUR_NM,                  // 통화명
            ADLT_DVSN_CD,                 // 성인구분코드
            ADLT_DVSN_NM,                 // 인원구분
            CSPR_AMT,                     // 원가금액
            NMPS_NBR,                     // 인원구분
            CSPR_AMT_SUB_TOT,             // 원가소계
            STEM_BASI_EXRT,               // 가정산기준환율
            STEM_UNPA_WON_AMT,            // 가정산금액(\)
        };

        private string _saveMode = "";
        private string _customerNumber = "";

        public PopUpAccountPayableMgt()
        {
            InitializeComponent();
        }

        private void PopUpAccountPayableMgt_Load(object sender, EventArgs e)
        {
            InitControls();

            // 미지급/선급구분 콤보박스 설정
            payableDivisionCodeComboBox.Items.Clear();

            List<CommonCodeItem> list = Global.GetCommonCodeList("UNPA_DVSN_CD");

            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                payableDivisionCodeComboBox.Items.Add(item);
            }

            // 미지급비용을 기본값으로 설정
            if (payableDivisionCodeComboBox.Items.Count > 0)
                payableDivisionCodeComboBox.SelectedIndex = 0;

            if (searchReservationNumberTextBox.Text.Trim() != "")
            {
                searchPriceCostList();
            }


            //-------------------------------------------------------------------------------------
            // 정산이 완료된건일 경우 초기화, 미지급저장, 미지급삭제 버튼 비활성화 -> 박현호
            //-------------------------------------------------------------------------------------
            if (_settlementYn == "Y")
            {
                MessageBox.Show("정산이 완료된 건입니다.");
                resetButton.Enabled = false;
                saveButton.Enabled = false;
                deleteButton.Enabled = false;
            }
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
            payableDivisionCodeComboBox.SelectedIndex = -1;          // 미지급구분코드 (1: 미지급비용, 2: 선급비용)
            payableDivisionCodeComboBox.Text = "";
            unpaidExpenseForeignSumTextBox.Text = "";                // 미지급금합계
            unpaidExpenseWonSumTextBox.Text = "";                    // 미지급금합계
            exchangeRateTextBox.Text = "";                           // 가정산환율
        }

        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = costPriceDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
        }

        public void SetSaveMode(string saveMode)
        {
            _saveMode = saveMode;
        }

        public void SetCustomerNumber(string customerNumber)
        {
            _customerNumber = customerNumber;
        }

        public void SetReservationNumber(string reservationNumber)
        {
            searchReservationNumberTextBox.Text = reservationNumber;
        }
        public void SetBookerName(string bookerName)
        {
            searchBookerNameTextBox.Text = bookerName;
        }

        public void SetProductNo(string productNo)
        {
            _productNo = productNo;
        }


        public void SetProductGradeCode(string productGradeCode)
        {
            _productGradeCode = productGradeCode;
        }

        public void SetSettlementYn(string settlementYn)
        {
            _settlementYn = settlementYn;
        }





        // 검색버튼 클릭
        private void searchBookerButton_Click(object sender, EventArgs e)
        {
            searchPriceCostList();
        }

        // 등록된 예약원가내역 목록 조회
        private void searchPriceCostList()
        {

            // 기본가격을 세팅하기 전에 원가그리드를 초기화
            costPriceDataGridView.Rows.Clear();

            string RSVT_NO = searchReservationNumberTextBox.Text.Trim();
            string CSPR_CNMB = "";
            string CSPR_NM = "";
            string ARPL_CMPN_NO = "";
            string ARPL_CMPN_NM = "";
            string CSPR_CUR_CD = "";
            string CSPR_CUR_NM = "";
            string ADLT_DVSN_CD = "";
            string ADLT_DVSN_NM = "";
            string CSPR_AMT = "";
            string NMPS_NBR = "";
            string CSPR_AMT_SUB_TOT = "";            // 원가소계 (그리드 출력용)
            string STEM_BASI_EXRT = "";              // 가정산기준환율
            string STEM_UNPA_WON_AMT = "";           // 가정산금액(\)
            double costPriceForeignSubTotal = 0;     // 미지급외화총계
            Int32 costPriceWonSubTotal = 0;         // 미지급원화총계

            string query = string.Format("CALL SelectRsvtCsprAndUpnaList ( '{0}')", RSVT_NO);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약원가 정보가 없습니다. 등록된 원가정보를 확인하세요.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                CSPR_CNMB = datarow["CSPR_CNMB"].ToString();
                CSPR_NM = datarow["CSPR_NM"].ToString();
                ARPL_CMPN_NO = datarow["ARPL_CMPN_NO"].ToString();
                ARPL_CMPN_NM = datarow["ARPL_CMPN_NM"].ToString();
                CSPR_CUR_CD = datarow["CSPR_CUR_CD"].ToString();
                CSPR_CUR_NM = datarow["CSPR_CUR_NM"].ToString();
                ADLT_DVSN_CD = datarow["ADLT_DVSN_CD"].ToString();
                ADLT_DVSN_NM = datarow["ADLT_DVSN_NM"].ToString();
                CSPR_AMT = Utils.SetComma(datarow["CSPR_AMT"].ToString());
                NMPS_NBR = datarow["NMPS_NBR"].ToString();
                CSPR_AMT_SUB_TOT = Utils.SetComma(datarow["CSPR_AMT_SUB_TOT"].ToString());
                STEM_BASI_EXRT = datarow["STEM_BASI_EXRT"].ToString();
                //STEM_UNPA_WON_AMT = Utils.SetComma(datarow["STEM_UNPA_WON_AMT"].ToString());
                STEM_UNPA_WON_AMT = Utils.SetComma(datarow["CSPR_WON_SUB_AMT"].ToString());
                //STEM_UNPA_WON_AMT = STEM_UNPA_WON_AMT.Substring(0, STEM_UNPA_WON_AMT.LastIndexOf('.'));

                costPriceForeignSubTotal = costPriceForeignSubTotal + double.Parse(datarow["CSPR_AMT_SUB_TOT"].ToString());
                costPriceWonSubTotal = costPriceWonSubTotal + Int32.Parse(datarow["CSPR_WON_SUB_AMT"].ToString());

                costPriceDataGridView.Rows.Add
                (
                    CSPR_CNMB,
                    CSPR_NM,
                    ARPL_CMPN_NO,
                    ARPL_CMPN_NM,
                    CSPR_CUR_CD,
                    CSPR_CUR_NM,
                    ADLT_DVSN_CD,
                    ADLT_DVSN_NM,
                    CSPR_AMT,
                    NMPS_NBR,
                    CSPR_AMT_SUB_TOT,
                    STEM_BASI_EXRT,
                    STEM_UNPA_WON_AMT
               );
            }

            costPriceDataGridView.ClearSelection();

            unpaidExpenseForeignSumTextBox.Text = Utils.SetComma(costPriceForeignSubTotal.ToString());  // 미지급금 합계(외화)
            unpaidExpenseWonSumTextBox.Text = Utils.SetComma(costPriceWonSubTotal.ToString());          // 미지급금 합계(원화)
        }

        // 환율값 변경시 이벤트 처리
        private void exchangeRateTextBox_TextChanged(object sender, EventArgs e)
        {
            calcWonAmount();
        }

        // 환율이 입력되면 원화금액을 계산
        private void calcWonAmount()
        {
            if (exchangeRateTextBox.Text == "") return;

            double wonAmount = 0;

            string CSPR_CUR_CD = "";
            double CSPR_AMT_SUB_TOT = 0;
            double STEM_UNPA_WON_AMT = 0;
            double exchangeRate = 0;

            bool isNum = double.TryParse(exchangeRateTextBox.Text.Trim(), out exchangeRate);

            if (!isNum)
            {
                exchangeRate = 0;
            }

            for (int i = 0; i < costPriceDataGridView.Rows.Count; i++)
            {
                CSPR_CUR_CD = costPriceDataGridView.Rows[i].Cells["CSPR_CUR_CD"].Value.ToString();
                CSPR_AMT_SUB_TOT = Double.Parse(costPriceDataGridView.Rows[i].Cells["CSPR_AMT_SUB_TOT"].Value.ToString());

                // 원화는 환율 반영하지 않고 원화금액에 누적
                if (CSPR_CUR_CD == "KRW")
                {
                    STEM_UNPA_WON_AMT = STEM_UNPA_WON_AMT + CSPR_AMT_SUB_TOT;
                }
                else
                {
                    // 외화인 경우 원가소계에 인원수를 곱하고 환율을 적용하여 원가원화금액 산출
                    wonAmount = Math.Round(CSPR_AMT_SUB_TOT * exchangeRate);

                    STEM_UNPA_WON_AMT = STEM_UNPA_WON_AMT + wonAmount;
                }

                unpaidExpenseWonSumTextBox.Text = Utils.SetComma(STEM_UNPA_WON_AMT.ToString());
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (exchangeRateTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("환율은 필수 입력항목입니다.");
                return;
            }

            string UNPA_DVSN_CD = Utils.GetSelectedComboBoxItemValue(payableDivisionCodeComboBox);  // 미지급/선급비용 구분코드 (1: 미지급 2: 선급)
            string UNPA_DVSN_CD_NM = Global.GetCommonCodeDesc("UNPA_DVSN_CD", UNPA_DVSN_CD);

            string RSVT_NO = searchReservationNumberTextBox.Text.Trim();                            // 예약번호
            string CSPR_CNMB = searchReservationNumberTextBox.Text.Trim();                          // 원가일련번호
            string CUST_NO = _customerNumber;                                                       // 고객번호
            double STEM_BASI_EXRT = Double.Parse(exchangeRateTextBox.Text.Trim());                  // 가정산환율
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            query = string.Format("CALL InsertUnpaFromCostPriceByReservationNo('{0}','{1}',{2},'{3}', '{4}')", RSVT_NO, UNPA_DVSN_CD, STEM_BASI_EXRT, FRST_RGTR_ID, "200100001");

            queryStringArray[0] = query;
            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show(UNPA_DVSN_CD_NM + "내역을 저장할 수 없습니다.");
                return;
            }

            // 호출창으로 미지급원화합계금액을 리턴
            string SUM_STEM_WON_AMT = queryResultArray[0];
            _settlementEstimationWonAmt = long.Parse(SUM_STEM_WON_AMT);

            searchPriceCostList();

            MessageBox.Show(UNPA_DVSN_CD_NM + "내역을 저장했습니다.");
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string UNPA_DVSN_CD = Utils.GetSelectedComboBoxItemValue(payableDivisionCodeComboBox);  // 미지급/선급비용 구분코드 (1: 미지급 2: 선급)
            string RSVT_NO = searchReservationNumberTextBox.Text.Trim();                            // 예약번호
            string STEM_BASI_EXRT = exchangeRateTextBox.Text.Trim();                                // 가정산환율
            string STEM_UNPA_FRCR_AMT = unpaidExpenseForeignSumTextBox.Text.Trim();                 // 미지급외화금액합계

            if (RSVT_NO == "")
            {
                MessageBox.Show("예약번호가 입력되지 않았습니다.");
                return false;
            }

            // 미지급 처리건은 환율 필수 입력
            if (STEM_BASI_EXRT == "")
            {
                MessageBox.Show("환율은 필수 입력항목입니다.");
                exchangeRateTextBox.Focus();
                return false;
            }

            return true;
        }

        // 삭제버튼 클릭
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string RSVT_NO = searchReservationNumberTextBox.Text.Trim();                            // 예약번호
            string UNPA_DVSN_CD = Utils.GetSelectedComboBoxItemValue(payableDivisionCodeComboBox);  // 미지급/선급비용 구분코드 (1: 미지급 2: 선급)
            string UNPA_DVSN_CD_NM = Global.GetCommonCodeDesc("UNPA_DVSN_CD", UNPA_DVSN_CD);        // 미지급/선급구분명

            if (RSVT_NO == "")
            {
                MessageBox.Show("예약번호가 입력되지 않았습니다.");
                return;
            }

            DialogResult result = MessageBoxEx.Show("해당 예약건의 " + UNPA_DVSN_CD_NM + " 내역을 삭제하시겠습니까?", "미지급/선급비용 내역 삭제", "예", "아니오");
            if (result == DialogResult.No)
            {
                return;
            }

            string query = string.Format("CALL DeleteUnpaItemByReservationNo('{0}','{1}')", RSVT_NO, Global.loginInfo.ACNT_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);

            string userMessageText = "";

            if (retVal > 0)
            {
                userMessageText = UNPA_DVSN_CD_NM + "내역을 삭제했습니다.";
            }
            else if (retVal == 0)
            {
                userMessageText = UNPA_DVSN_CD_NM + "내역의 삭제 대상이 없습니다. 미지급관리화면에서 확인하세요.";
            }
            else 
            {
                userMessageText = UNPA_DVSN_CD_NM + "내역의 삭제가 불가합니다. 운영담당자에게 연락하세요.";
            }

            searchPriceCostList();
            MessageBox.Show(userMessageText);
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void payableDivisionCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            // 미지급/선급비용 구분에 따라 입력 레이블 처리 변경
            if (payableDivisionCodeComboBox.SelectedIndex == 0)
            {
                exchangeRateLabel.Text = "가정산환율";
                // 레이블을 미지급비용으로 설정
                unpaidExpenseForeignSumLabel.Text = "미지급금 합계(외화)";
                unpaidExpenseWonSumLabel.Text = "미지급금 합계(원화)";
            }
            else
            {
                exchangeRateLabel.Text = "적용환율";

                // 레이블을 선급비용으로 설정
                unpaidExpenseForeignSumLabel.Text = "선급금 합계(외화)";
                unpaidExpenseWonSumLabel.Text = "선급금 합계(원화)";
            }
        }

        public long GetSettlementEstimationWonAmt()
        {
            return _settlementEstimationWonAmt;
        }
    }
}
