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
using TripERP.ProductMgt;
using TripERP.DocumentMgt;
using System.Collections;

namespace TripERP.ReservationMgt
{
    public partial class ReservationDetailMgt : Form
    {
        /// 
        /// 전역변수 선언
        /// 
        // 예약기본 VO 클래스
        VOReservationDetail vOReservationDetail = new VOReservationDetail();

        enum eCostPriceDataGridView { CSPR_CNMB = 0 };

        bool _needSaveData = false;

        private string _reservationNumber = "";
        private string _saveMode = "";
        private string _customerNumber = "";      // 고객번호
        private string _emailId = "";
        private string _emailDomain = "";
        private string _employeeNumber = "";
        private string _employeeName = "";
        private string _productNo = "";
        private string _productGradeCode = "";
        private string _optionNo = "";
        private int _clickOptionRow = -1;
        private string _costPriceNo = "";
        private int _clickCostPriceRow = -1;
        private int _selectedReservarionListRow = -1;

        private bool _formLoad = false;                   // 폼이 로딩될 때 시점인지 체크하기 위한 용도
        private bool _resetButtonClick = false;           // 초기화버튼을 눌렀는지 감지하기 위한 용도
        private bool _customerChanged = false;            // 고객번호가 변경되었는지 감지하기 위한 용도

        private bool _checkCutOffList = false;            // 명단체크필요여부
        private bool _checkCutOffPassport = false;        // 여권체크필요여부
        private bool _checkCutOffArrangement = false;     // 수배체크필요여부
        private bool _checkCutOffVoucher = false;         // 바우처체크필요여부
        private bool _checkCutOffInsurance = false;       // 보험체크필요여부 
        private bool _checkCutOffAirline = false;         // 항공체크필요여부
        private bool _checkCutOffPersonalInfo = false;    // 개인체크필요여부

        private int _numberOfAdult = 0;     // 전역변수: 성인수
        private int _numberOfChild = 0;     // 전역변수: 소아수
        private int _numberOfInfant = 0;    // 전역변수: 유아수
        private int _numberOfPeople = 0;    // 전역변수: 총인원수

        Label[] _LabelArray = null;
        ComboBox[] _ComboBoxArray = null;
        DateTimePicker[] _DateTimePickerArray = null;
        Button[] _ButtonArray = null;

        List<string> _cutOffList = new List<string>();


        private DataGridView _reservationDataGridView = null;

        public ReservationDetailMgt()
        {
            InitializeComponent();
        }

        //=========================================================================================================================================================================
        // 폼 로딩 시 초기화
        //=========================================================================================================================================================================
        private void ReservationDetailMgt_Load(object sender, EventArgs e)
        {
            redrawReservationDetailInfo();
        }

        //=========================================================================================================================================================================
        // 예약상세정보 초기 로딩
        //=========================================================================================================================================================================
        public void redrawReservationDetailInfo()
        {
            _formLoad = true;

            // 입력필드 초기화
            resetInputField();

            InitControls();

            if (_saveMode == Global.SAVEMODE_UPDATE && _reservationNumber != "")
            {
                reservationNumberTextBox.ReadOnly = true;
                saleCurrencyCodeComboBox.Enabled = false;
                insertNewReservationDetailButton.Enabled = false;
                saveReservationDetailButton.Enabled = true;
                SetReservationInfoToForm();
            }
            else
            {
                reservationNumberTextBox.ReadOnly = false;
                saleCurrencyCodeComboBox.Enabled = true;
                insertNewReservationDetailButton.Enabled = true;
                saveReservationDetailButton.Enabled = false;
                _employeeNumber = Global.loginInfo.ACNT_ID;         // 등록모드인 경우 담당직원번호는 로그인ID로 세팅
            }

            _formLoad = false;
        }

        //=========================================================================================================================================================================
        // 예약정보를 검색하여 화면에 표시
        //=========================================================================================================================================================================
        private void SetReservationInfoToForm()
        {
            if (SetReservationDetail() == false) return;               // 예약기본조회
            SetReservationOptionList();           // 예약옵션조회
            SetReservationCostPriceList();        // 예약원가조회
            SetMemoList();                        // 진행관리메모조회

            paramSetReservationDetail("UPD");     // 예약VO에 값 설정
        }

        private void InitControls()
        {
            // DataGridView 초기화
            InitDataGridView();

            // 상품 콤보박스 아이템 로드
            LoadProductComboBoxItems();

            // Common code 아이템 로드
            // - email, 예약상태, 옵션구분, 원가, 명단
            // 
            LoadCommonCodeItems();

            // 모객업체 콤보박스 아이템 로드 
            LoadCooperativeCompanyComboBoxItems();

            // 박, 일 콤보박스 아이템 로드
            LoadNightDayComboBoxItems();

            // 원가수배처 콤보박스 아이템 로드
            LoadDestinationCompanyComboBoxItems();

            // 통화 코드 콤보박스 아이템 로드
            LoadCurrencyCodeComboBoxItems();

            /*
            if (_saveMode == Global.SAVEMODE_UPDATE)
            {
                searchCustomerButton.Enabled = false;
            }
            */
        }

        //=========================================================================================================================================================================
        // 각종 콤보박스 초기값 로드
        //=========================================================================================================================================================================
        private void LoadCommonCodeItems()
        {
            // email, 예약상태, 옵션구분, 원가구분, 명단
            // 수배, 여권, 보험, 항공, 개인
            /*
            string[] groupNameArray = { "EMAL_DOMN_ADDR", "RSVT_STTS_CD",  "LST_CHCK_STTS_CD",
                "ARGM_CHCK_STTS_CD", "PSPT_CHCK_STTS_CD", "ISRC_CHCK_STTS_CD", "AVAT_CHCK_STTS_CD", "PRSN_CHCK_STTS_CD" };

            ComboBox[] comboBoxArray = { domainComboBox, reservationStatusComboBox, cutOffListComboBox,
                cutOffArrangementComboBox, cutOffPassportComboBox, cutOffInsuranceComboBox, cutOffAirlineComboBox, cutOffPersonalInfoComboBox };
            */
            string[] groupNameArray = { "EMAL_DOMN_ADDR", "RSVT_STTS_CD" };

            ComboBox[] comboBoxArray = { domainComboBox, reservationStatusComboBox };

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
                    comboBoxArray[gi].SelectedIndex = -1;
            }

            // 예약상태는 미확정으로 기본 설정
            if (_reservationNumber == "")
            {
                reservationStatusComboBox.SelectedIndex = 0;
            }
        }

        //=========================================================================================================================================================================
        // 그리드 초기화
        //=========================================================================================================================================================================
        private void InitDataGridView()
        {
            DataGridView[] dataGridViewArray = { optionDataGridView, costPriceDataGridView, cutOffMgtMemoDataGridView };
            for (int i = 0; i < dataGridViewArray.Length; i++)
            {
                dataGridViewArray[i].RowHeadersVisible = false;
                dataGridViewArray[i].SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewArray[i].MultiSelect = false;
                dataGridViewArray[i].RowHeadersVisible = false;
                dataGridViewArray[i].ReadOnly = true;
                dataGridViewArray[i].RowHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Azure;
            }
        }

        //=========================================================================================================================================================================
        // 상품콤보박스 초기 로드
        //=========================================================================================================================================================================
        private void LoadProductComboBoxItems()
        {
            productComboBox.Items.Clear();

            string query = "CALL SelectPrdtList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품 정보를 가져올 수 없습니다.");
                return;
            }

            //productComboBox.Items.Add(new ComboBoxItem("전체", -1));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_NM, PRDT_CNMB);

                productComboBox.Items.Add(item);
            }

            productComboBox.SelectedIndex = -1;
        }

        //=========================================================================================================================================================================
        // 상품이 바뀔 때 이벤트 처리
        //=========================================================================================================================================================================
        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productComboBox.Items.Count > 0 && productComboBox.SelectedIndex != -1)
            {
                if (_resetButtonClick == false)
                {
                    DialogResult result = MessageBoxEx.Show("상품을 변경하면 예약진행상태 정보가 사라집니다. 계속하시겠습니까?", "상품변경", "예", "아니오");
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                _productNo = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();
                // 상품등급정보 검색 및 설정
                resetProductGradeComboBox();

                // 상품을 변경하면 예약기본 테이블의 진행상태정보를 초기화
                string query = string.Format("CALL UpdateRsvtCutOffStatusClear('{0}')", reservationNumberTextBox.Text.Trim());
                int retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal == -1)
                {
                    MessageBox.Show("상품변경에 따른 예약진행상태 정보를 초기화하지 못했습니다. 운영담당자에게 연락하세요.");
                    return;
                }

                // 진행확인항목 초기화
                resetCutOffCheckInfoField();
            }
        }

        //=========================================================================================================================================================================
        // 상품등급 콤보박스 초기화
        //=========================================================================================================================================================================
        private void resetProductGradeComboBox()
        {
            if (productComboBox.SelectedIndex == -1)
                return;

            string PRDT_CNMB = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            productGradeComboBox.Items.Clear();
            productGradeComboBox.Text = "";

            string query = string.Format("CALL SelectPrdtDtlsList ( {0} )", PRDT_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품 정보를 가져올 수 없습니다.");
                return;
            }

            //productGradeComboBox.Items.Add(new ComboBoxItem("전체", -1));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string PRDT_GRAD_CD = dataRow["PRDT_GRAD_CD"].ToString();
                string PRDT_GRAD_NM = dataRow["PRDT_GRAD_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_GRAD_NM, PRDT_GRAD_CD);

                productGradeComboBox.Items.Add(item);
            }

            if (productGradeComboBox.Items.Count > 0)
                productGradeComboBox.SelectedIndex = -1;
        }

        //=========================================================================================================================================================================
        // 상품이 변경되면 진행상태정보를 재설정 (레이블, 콤보박스, 데이트타임피커, 버튼)
        //=========================================================================================================================================================================
        private void resetCutOffCheckInfoField()
        {
            LoadCutOffComboBox();
        }

        //=========================================================================================================================================================================
        // 모객업체 콤보박스 초기화 
        //=========================================================================================================================================================================
        private void LoadCooperativeCompanyComboBoxItems()
        {
            cooperativeCompanyComboBox.Items.Clear();

            // 모객매체
            string CNSM_FILE_APLY_YN = "10";
            string query = "CALL SelectAllTicketerCompanyList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("모객업체 정보를 가져올 수 없습니다.");
                return;
            }

            //cooperativeCompanyComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                cooperativeCompanyComboBox.Items.Add(item);
            }

            if (cooperativeCompanyComboBox.Items.Count > 0)
                cooperativeCompanyComboBox.SelectedIndex = 0;
        }

        //=========================================================================================================================================================================
        // 박, 일 콤보박스 아이템 로드
        //=========================================================================================================================================================================
        private void LoadNightDayComboBoxItems()
        {
            for (int i = 1; i <= 20; i++)
            {
                nightComboBox.Items.Add(i.ToString());
                dayComboBox.Items.Add(i.ToString());
            }
            dayComboBox.Items.Add("21");
            dayComboBox.Items.Add("22");
        }

        //=========================================================================================================================================================================
        // 수배처 콤보박스 초기화 (옵션, 원가)
        //=========================================================================================================================================================================
        private void LoadDestinationCompanyComboBoxItems()
        {
            destinationCompanyForCostPriceComboBox.Items.Clear();

            string query = "CALL SelectDestinationCompanyList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("수배처 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                destinationCompanyForCostPriceComboBox.Items.Add(item);
            }

            if (destinationCompanyForCostPriceComboBox.Items.Count > 0)
            {
                destinationCompanyForCostPriceComboBox.SelectedIndex = -1;
            }
        }

        //=========================================================================================================================================================================
        // 통화코드 콤보박스 아이템 로드
        //=========================================================================================================================================================================
        private void LoadCurrencyCodeComboBoxItems()
        {
            costPricecurrencyCodeComboBox.Items.Clear();

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

                saleCurrencyCodeComboBox.Items.Add(item);      // 판매통화코드
                costPricecurrencyCodeComboBox.Items.Add(item); // 원가통화코드
                optionCurrencyCodeComboBox.Items.Add(item);    // 옵션통화코드
            }

            if (saleCurrencyCodeComboBox.Items.Count > 0)
            {
                saleCurrencyCodeComboBox.SelectedIndex = -1;
                costPricecurrencyCodeComboBox.SelectedIndex = -1;
                optionCurrencyCodeComboBox.SelectedIndex = -1;
            }
        }

        //=========================================================================================================================================================================
        // 저장된 예약기본정보값을 입력필드에 Set.
        //=========================================================================================================================================================================
        private bool SetReservationDetail()
        {
            reservationNumberTextBox.Text = _reservationNumber;
            if (_reservationNumber == "")
                return false;

            string query = string.Format("CALL SelectRsvtItem ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약상세정보를 가져올 수 없습니다.");
                return false;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            ////
            // 최상단 정보
            _customerNumber = dataRow["CUST_NO"].ToString(); // 고객번호
            bookerNameTextBox.Text = dataRow["CUST_NM"].ToString(); // 예약자

            productComboBox.SelectedIndexChanged -= productComboBox_SelectedIndexChanged;
            Utils.SelectComboBoxItemByValue(productComboBox, dataRow["PRDT_CNMB"].ToString());                       // 상품 
            resetProductGradeComboBox();
            productComboBox.SelectedIndexChanged += productComboBox_SelectedIndexChanged;
            Utils.SelectComboBoxItemByValue(productGradeComboBox, dataRow["PRDT_GRAD_CD"].ToString());               // 상품등급

            _productNo = dataRow["PRDT_CNMB"].ToString();
            _productGradeCode = dataRow["PRDT_GRAD_CD"].ToString();

            _employeeNumber = dataRow["RPSB_EMPL_NO"].ToString();                                                    // 담당직원번호
            _employeeName = dataRow["EMPL_NM"].ToString();                                                           // 담당직원이름

            if (dataRow["STMT_YN"].ToString().Equals("Y"))

                settlementStatusTextBox.Text = "예";                                                                 // 정산여부
            else
                settlementStatusTextBox.Text = "아니오";                                                             // 정산여부

            vOReservationDetail.STMT_YN = dataRow["STMT_YN"].ToString();

            //--------------------------------------------------------------------------------------------------------------------------------------------------
            //totalAccountPayableTextBox.Text = Utils.SetComma(dataRow["---------"].ToString());                     // 미지급금합계
            //totalAccountReceivableTextBox.Text = Utils.SetComma(dataRow["------"].ToString());                     // 선급금합계

            // 기본정보
            reservationDateDateTimePicker.Value = Utils.GetDateTimeFormatFromString(dataRow["RSVT_DT"].ToString());  // 예약일자
            purchageDateTimePicker.Value = Utils.GetDateTimeFormatFromString(dataRow["PRCS_DTM"].ToString());        // 구매일시
            Utils.SelectComboBoxItemByValue(cooperativeCompanyComboBox, dataRow["TKTR_CMPN_NO"].ToString());         // 모객업체번호
            vOReservationDetail.TKTR_CMPN_NO = dataRow["TKTR_CMPN_NO"].ToString();

            string email = dataRow["ORDE_EMAL_ADDR"].ToString(); // email 
            string[] emailTemp = email.Split('@');
            if (emailTemp.Length == 2)
            {
                emailIdTextBox.Text = emailTemp[0];


                // 예약상세 검색시 고객 이메일 도메인 정보 출력 --> 190820 박현호
                //--------------------------------------------------------------------------
                setEmailDomain(emailTemp[1].ToString());
                //--------------------------------------------------------------------------
                //Utils.SelectComboBoxItemByText(domainComboBox, emailTemp[1]);
            }
            else
            {
                emailIdTextBox.Text = email;
            }

            cellphoneNumberTextBox.Text = dataRow["ORDE_CTIF_PHNE_NO"].ToString();                                   // 휴대폰
            customerEngNameTextBox.Text = dataRow["RPRS_ENG_NM"].ToString();                                         // 대표자영문명
            departureDateTimePicker.Value = Utils.GetDateTimeFormatFromString(dataRow["DPTR_DT"].ToString());        // 출발일자
            nightComboBox.SelectedItem = dataRow["LGMT_DAYS"].ToString();                                            // 숙박일수
            dayComboBox.SelectedItem = dataRow["TOT_TRIP_DAYS"].ToString();                                          // 총여행일수    

            if (dayComboBox.SelectedIndex != -1)                                                                     // 도착일자
                arrivalDateTimePicker.Value = departureDateTimePicker.Value.AddDays(Convert.ToInt32(dayComboBox.SelectedItem));
            else
                arrivalDateTimePicker.Value = departureDateTimePicker.Value;

            // 금액정보
            salesPriceTextBox.Text = Utils.SetComma(dataRow["TOT_SALE_AMT"].ToString());        // 총판매금액 (판매합계금액 + 옵션합계금액)
            vOReservationDetail.TOT_SALE_AMT = double.Parse(dataRow["TOT_SALE_AMT"].ToString());

            totalDepositPriceTextBox.Text = Utils.SetComma(dataRow["TOT_RECT_AMT"].ToString());      // 입금총액
            depositPriceTextBox.Text = Utils.SetComma(dataRow["PRPY_AMT"].ToString());      // 선금
            reservationPriceTextBox.Text = Utils.SetComma(dataRow["RSVT_AMT"].ToString());      // 예약금액
            partPriceTextBox.Text = Utils.SetComma(dataRow["MDPY_AMT"].ToString());             // 중도금금액
            outstandingPriceTextBox.Text = Utils.SetComma(dataRow["UNAM_BAL"].ToString());      // 미수금잔액

            // 인원수 (기본가격)
            adultPeopleCountTextBox.Text = dataRow["ADLT_NBR"].ToString();                      // 성인수
            childPeopleCountTextBox.Text = dataRow["CHLD_NBR"].ToString();                      // 소아수
            infantPeopleCountTextBox.Text = dataRow["INFN_NBR"].ToString();                     // 유아수

            // 인원수 (원가)
            /*
            adultPeopleCountForCostPriceTextBox.Text = dataRow["ADLT_NBR"].ToString();          // 성인수
            childPeopleCountForCostPriceTextBox.Text = dataRow["CHLD_NBR"].ToString();          // 소아수
            infantPeopleCountForCostPriceTextBox.Text = dataRow["INFN_NBR"].ToString();         // 유아수
            */
            _numberOfAdult = Int32.Parse(dataRow["ADLT_NBR"].ToString());                       // 성인수를 변수에 보관
            _numberOfChild = Int32.Parse(dataRow["CHLD_NBR"].ToString());                       // 소아수를 변수에 보관
            _numberOfInfant = Int32.Parse(dataRow["INFN_NBR"].ToString());                      // 유아수를 변수에 보관

            _numberOfPeople = _numberOfAdult + _numberOfChild + _numberOfInfant;

            totalNumberOfPersonTextBox.Text = _numberOfPeople.ToString();                       // 총인원수
            totalNumberOfPersonsTextbox.Text = "0";                                             // 옵션 총인원수

            ////
            // 판매/원가
            // 판매가

            string ADLT_SALE_PRCE = dataRow["ADLT_SALE_PRCE"].ToString();
            string CHLD_SALE_PRCE = dataRow["CHLD_SALE_PRCE"].ToString();
            string INFN_SALE_PRCE = dataRow["INFN_SALE_PRCE"].ToString();

            string SALE_SUM_AMT = dataRow["SALE_SUM_AMT"].ToString();
            salesPriceSumTextBox.Text = Utils.SetComma(SALE_SUM_AMT);       // 판매가격합계
            totalSalePriceTextBox.Text = Utils.SetComma(SALE_SUM_AMT);      // 판매가격합계

            Utils.SelectComboBoxItemByValue(saleCurrencyCodeComboBox, dataRow["SALE_CUR_CD"].ToString()); // 판매통화코드

            adultSalesPriceTextBox.Text = Utils.SetComma(ADLT_SALE_PRCE);   // 성인판매가격
            childSalesPriceTextBox.Text = Utils.SetComma(CHLD_SALE_PRCE);   // 소아판매가격
            infantSalesPriceTextBox.Text = Utils.SetComma(INFN_SALE_PRCE);  // 유아판매가격

            double adultSalesPrice = Double.Parse(ADLT_SALE_PRCE);          // 성인판매가격소계
            double childSalesPrice = Double.Parse(CHLD_SALE_PRCE);          // 소아판매가격소계
            double infantSalesPrice = Double.Parse(INFN_SALE_PRCE);         // 유아판매가격소계

            double adultSalesPriceSubTotal = adultSalesPrice * _numberOfAdult;
            double childSalesPriceSubTotal = childSalesPrice * _numberOfChild;
            double infantSalesPriceSubTotal = infantSalesPrice * _numberOfInfant;

            adultSalesPriceSubTotTextBox.Text = Utils.SetComma(adultSalesPriceSubTotal.ToString());
            childSalesPriceSubTotTextBox.Text = Utils.SetComma(childSalesPriceSubTotal.ToString());
            infantSalesPriceSubTotTextBox.Text = Utils.SetComma(infantSalesPriceSubTotal.ToString());


            //// 수익합계
            // 위탁판매지급수수료

            vOReservationDetail.WON_PYMT_FEE = Double.Parse(dataRow["WON_PYMT_FEE"].ToString());

            // 위탁판매지급수수료 계산
            if (vOReservationDetail.WON_PYMT_FEE == 0)
            {
                double wonPayableFee = calcPayableFee();  // 금액이 없으면 새로 계산
                redrawingConsignmentSaleFeeInputform(wonPayableFee.ToString(), "수수료(미입금)", 38, 32);
            }
            else
            {
                redrawingConsignmentSaleFeeInputform(vOReservationDetail.WON_PYMT_FEE.ToString(), "수수료(입금완료)", 30, 32);
            }

            /// 옵션합계금액
            string OPTN_SUM_AMT = dataRow["OPTN_SUM_AMT"].ToString();
            totalOptionAmountTextBox.Text = Utils.SetComma(OPTN_SUM_AMT);   // 옵션합계금액
            /// 

            // 원가
            string CSPR_SUM_AMT = dataRow["CSPR_SUM_AMT"].ToString();
            totalCostPriceAmountTextBox.Text = Utils.SetComma(CSPR_SUM_AMT); // 원가합계금액

            // 예약상태코드
            Utils.SelectComboBoxItemByValue(reservationStatusComboBox, dataRow["RSVT_STTS_CD"].ToString()); // 예약상태코드
            vOReservationDetail.RSVT_STTS_CD = dataRow["RSVT_STTS_CD"].ToString();                          // 예약상태코드

            // 가정산금액
            settlementEstimationWonAmtTextBox.Text = Utils.SetComma(dataRow["STEM_WON_AMT"].ToString());

            // 정산금액
            settlementWonAmtTextBox.Text = Utils.SetComma(dataRow["STMT_WON_AMT"].ToString());

            // 환차손익 및 수익합계 산출
            vOReservationDetail.WON_PYMT_FEE = Double.Parse(dataRow["WON_PYMT_FEE"].ToString());
            vOReservationDetail.STEM_WON_AMT = Double.Parse(dataRow["STEM_WON_AMT"].ToString());
            vOReservationDetail.STMT_WON_AMT = Double.Parse(dataRow["STMT_WON_AMT"].ToString());


            /// 수익금액 계산
            calcRevenue();


            // 고객 요청사항
            customerRequestMemoTextBox.Text = dataRow["CUST_RQST_CNTS"].ToString();   // 고객요청사항

            // 내부 메모
            insideMemoTextBox.Text = dataRow["INTR_MEMO_CNTS"].ToString();   // 내부메모
            ////
            ///
            // 여행상품별 진행상태체크 항목 대상 설정
            vOReservationDetail.LST_CHCK_STTS_CD = dataRow["LST_CHCK_STTS_CD"].ToString();
            vOReservationDetail.PSPT_CHCK_STTS_CD = dataRow["PSPT_CHCK_STTS_CD"].ToString();
            vOReservationDetail.ARGM_CHCK_STTS_CD = dataRow["ARGM_CHCK_STTS_CD"].ToString();
            vOReservationDetail.VOCH_CHCK_STTS_CD = dataRow["VOCH_CHCK_STTS_CD"].ToString();
            vOReservationDetail.ISRC_CHCK_STTS_CD = dataRow["ISRC_CHCK_STTS_CD"].ToString();
            vOReservationDetail.AVAT_CHCK_STTS_CD = dataRow["AVAT_CHCK_STTS_CD"].ToString();
            vOReservationDetail.PRSN_CHCK_STTS_CD = dataRow["PRSN_CHCK_STTS_CD"].ToString();

            vOReservationDetail.LST_CHCK_DTM = dataRow["LST_CHCK_DTM"].ToString();
            vOReservationDetail.PSPT_CHCK_DTM = dataRow["PSPT_CHCK_DTM"].ToString();
            vOReservationDetail.ARGM_CHCK_DTM = dataRow["ARGM_CHCK_DTM"].ToString();
            vOReservationDetail.VOCH_CHCK_DTM = dataRow["VOCH_CHCK_DTM"].ToString();
            vOReservationDetail.ISRC_CHCK_DTM = dataRow["ISRC_CHCK_DTM"].ToString();
            vOReservationDetail.AVAT_CHCK_DTM = dataRow["AVAT_CHCK_DTM"].ToString();
            vOReservationDetail.PRSN_CHCK_DTM = dataRow["PRSN_CHCK_DTM"].ToString();

            // 수정건은 신규등록버튼을 비활성화
            insertNewReservationDetailButton.Enabled = false;

            // 여행상품별 진행상태체크 항목 대상 설정
            LoadCutOffComboBox();

            return true;
        }

        //=========================================================================================================================================================================
        // 여행상품별 진행상태체크 항목 대상 설정
        //=========================================================================================================================================================================
        private void LoadCutOffComboBox()
        {
            // 상품이 지정되지 않으면 종료
            if (_productNo.Length == 0) return;

            string query = string.Format("CALL SelectProductInfoByPK ( '{0}')", _productNo);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품기본정보를 가져올 수 없습니다.");
                return;
            }

            // 기존의 진행상태항목을 초기화
            if (_cutOffList.Count >= 1)
            {
                initializeCutOffCheckInfoField();
            }
            /*
            for (int ii = 0; ii < _cutOffList.Count; ii++)
            {
                _cutOffList.RemoveAt(ii);
            }

            _cutOffList.Clear();
            */
            string LST_CHCK_NEED_YN = "";
            string PSPT_CHCK_NEED_YN = "";
            string ARGM_CHCK_NEED_YN = "";
            string VOCH_CHCK_NEED_YN = "";
            string ISRC_CHCK_NEED_YN = "";
            string AVAT_CHCK_NEED_YN = "";
            string PRSN_CHCK_NEED_YN = "";

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                LST_CHCK_NEED_YN = dataRow["LST_CHCK_NEED_YN"].ToString();
                PSPT_CHCK_NEED_YN = dataRow["PSPT_CHCK_NEED_YN"].ToString();
                ARGM_CHCK_NEED_YN = dataRow["ARGM_CHCK_NEED_YN"].ToString();
                VOCH_CHCK_NEED_YN = dataRow["VOCH_CHCK_NEED_YN"].ToString();
                AVAT_CHCK_NEED_YN = dataRow["AVAT_CHCK_NEED_YN"].ToString();
                ISRC_CHCK_NEED_YN = dataRow["ISRC_CHCK_NEED_YN"].ToString();
                PRSN_CHCK_NEED_YN = dataRow["PRSN_CHCK_NEED_YN"].ToString();

                if (LST_CHCK_NEED_YN.Equals("Y"))          // 명단체크
                {
                    _cutOffList.Add("LST_CHCK_STTS_CD");
                    _checkCutOffList = true;
                }
                else
                {
                    _checkCutOffList = false;
                }

                if (PSPT_CHCK_NEED_YN.Equals("Y"))         // 여권체크
                {
                    _cutOffList.Add("PSPT_CHCK_STTS_CD");
                    _checkCutOffPassport = true;
                }
                else
                {
                    _checkCutOffPassport = false;
                }

                if (ARGM_CHCK_NEED_YN.Equals("Y"))         // 수배체크
                {
                    _cutOffList.Add("ARGM_CHCK_STTS_CD");
                    _checkCutOffArrangement = true;
                }
                else
                {
                    _checkCutOffArrangement = false;
                }

                if (VOCH_CHCK_NEED_YN.Equals("Y"))         // 바우처확인
                {
                    _cutOffList.Add("VOCH_CHCK_STTS_CD");
                    _checkCutOffVoucher = true;
                }
                else
                {
                    _checkCutOffVoucher = false;
                }

                if (ISRC_CHCK_NEED_YN.Equals("Y"))
                {
                    _cutOffList.Add("ISRC_CHCK_STTS_CD");
                    _checkCutOffInsurance = true;
                }
                else
                {
                    _checkCutOffInsurance = false;
                }

                if (AVAT_CHCK_NEED_YN.Equals("Y"))
                {
                    _cutOffList.Add("AVAT_CHCK_STTS_CD");
                    _checkCutOffAirline = true;
                }
                else
                {
                    _checkCutOffAirline = false;
                }

                if (PRSN_CHCK_NEED_YN.Equals("Y"))
                {
                    _cutOffList.Add("PRSN_CHCK_STTS_CD");
                    _checkCutOffPersonalInfo = true;
                }
                else
                {
                    _checkCutOffPersonalInfo = false;
                }
            }

            int xx1 = 0;
            int yy1 = 0;
            int xx2 = 0;
            int yy2 = 0;
            int xx3 = 0;
            int yy3 = 0;
            int xx4 = 0;
            int yy4 = 0;

            string labelText = "";
            string cutOffstatusCode = "";
            string cutOffstatusCodeValue = "";
            string cutOffCheckDateTime = "";

            // 레이블, 콤보박스, DateTimePicker, Button 배열 생성
            this._LabelArray = new Label[_cutOffList.Count];
            this._ComboBoxArray = new ComboBox[_cutOffList.Count];
            this._DateTimePickerArray = new DateTimePicker[_cutOffList.Count];
            this._ButtonArray = new Button[_cutOffList.Count];

            // 명단, 여권, 수배, 바우처 등 확인대상만 화면에 표시
            //for (int jj = 0; jj < 6; jj++ )
            for (int kk = 0; kk < _cutOffList.Count; kk++)
            {
                // 컴포넌트 렌더링 위치 결정
                switch (kk)
                {
                    case 0:
                        xx1 = 14; yy1 = 650;    // 레이블 위치
                        xx2 = 76; yy2 = 650;    // 콤보박스 위치
                        xx3 = 163; yy3 = 650;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 650;    // 버튼 위치
                        break;
                    case 1:
                        xx1 = 14; yy1 = 685;    // 레이블 위치
                        xx2 = 76; yy2 = 685;    // 콤보박스 위치
                        xx3 = 163; yy3 = 685;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 685;    // 버튼 위치
                        break;
                    case 2:
                        xx1 = 14; yy1 = 719;   // 레이블 위치
                        xx2 = 76; yy2 = 719;   // 콤보박스 위치
                        xx3 = 163; yy3 = 719;   // DateTimePicker 위치
                        xx4 = 420; yy4 = 719;   // 버튼 위치
                        break;
                    case 3:
                        xx1 = 14; yy1 = 753;     // 레이블 위치
                        xx2 = 76; yy2 = 753;     // 콤보박스 위치
                        xx3 = 163; yy3 = 753;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 753;    // 버튼 위치
                        break;
                    case 4:
                        xx1 = 14; yy1 = 787;     // 레이블 위치
                        xx2 = 76; yy2 = 787;     // 콤보박스 위치
                        xx3 = 163; yy3 = 787;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 787;    // 버튼 위치
                        break;
                    case 5:
                        xx1 = 14; yy1 = 821;     // 레이블 위치
                        xx2 = 76; yy2 = 821;     // 콤보박스 위치
                        xx3 = 163; yy3 = 821;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 821;    // 버튼 위치
                        break;
                    case 6:
                        xx1 = 14; yy1 = 855;     // 레이블 위치
                        xx2 = 76; yy2 = 855;     // 콤보박스 위치
                        xx3 = 163; yy3 = 855;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 855;    // 버튼 위치
                        break;
                }

                if (_cutOffList[kk].Equals("LST_CHCK_STTS_CD"))
                {
                    labelText = "명단";
                    cutOffstatusCodeValue = vOReservationDetail.LST_CHCK_STTS_CD;
                    cutOffCheckDateTime = vOReservationDetail.LST_CHCK_DTM;
                }
                else if (_cutOffList[kk].Equals("PSPT_CHCK_STTS_CD"))
                {
                    labelText = "여권";
                    cutOffstatusCodeValue = vOReservationDetail.PSPT_CHCK_STTS_CD;
                    cutOffCheckDateTime = vOReservationDetail.PSPT_CHCK_DTM;
                }
                else if (_cutOffList[kk].Equals("ARGM_CHCK_STTS_CD"))
                {
                    labelText = "수배";
                    cutOffstatusCodeValue = vOReservationDetail.ARGM_CHCK_STTS_CD;
                    cutOffCheckDateTime = vOReservationDetail.ARGM_CHCK_DTM;
                }
                else if (_cutOffList[kk].Equals("VOCH_CHCK_STTS_CD"))
                {
                    labelText = "바우처";
                    cutOffstatusCodeValue = vOReservationDetail.VOCH_CHCK_STTS_CD;
                    cutOffCheckDateTime = vOReservationDetail.VOCH_CHCK_DTM;
                }
                else if (_cutOffList[kk].Equals("ISRC_CHCK_STTS_CD"))
                {
                    labelText = "보험";
                    cutOffstatusCodeValue = vOReservationDetail.ISRC_CHCK_STTS_CD;
                    cutOffCheckDateTime = vOReservationDetail.ISRC_CHCK_DTM;
                }
                else if (_cutOffList[kk].Equals("AVAT_CHCK_STTS_CD"))
                {
                    labelText = "항공";
                    cutOffstatusCodeValue = vOReservationDetail.AVAT_CHCK_STTS_CD;
                    cutOffCheckDateTime = vOReservationDetail.AVAT_CHCK_DTM;
                }
                else if (_cutOffList[kk].Equals("PRSN_CHCK_STTS_CD"))
                {
                    labelText = "개인";
                    cutOffstatusCodeValue = vOReservationDetail.PRSN_CHCK_STTS_CD;
                    cutOffCheckDateTime = vOReservationDetail.PRSN_CHCK_DTM;
                }

                cutOffstatusCode = _cutOffList[kk];

                setCreateCutOffLabel(labelText, kk, xx1, yy1);
                setCreateCutOffComboBox(cutOffstatusCodeValue, kk, xx2, yy2);
                setCreateCutOffDateTimePicker(kk, cutOffCheckDateTime, xx3, yy3);
                setCreateCutOffButton(kk, xx4, yy4);
            }
        }

        //=========================================================================================================================================================================
        // 진행상태명 레이블 동적 생성
        //=========================================================================================================================================================================
        private void setCreateCutOffLabel(string labelText, int tag, int xx1, int yy1)
        {
            // 레이블 생성
            Label label = new Label();
            label.Text = labelText;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Location = new Point(xx1, yy1);
            label.Size = new Size(62, 21);
            label.Tag = tag;
            this.Controls.Add(label);
            this._LabelArray[tag] = label;
        }

        //=========================================================================================================================================================================
        // 진행상태구분 콤보박스 동적 생성
        //=========================================================================================================================================================================
        private void setCreateCutOffComboBox(string cutOffstatusCodeValue, int tag, int xx2, int yy2)
        {
            // 콤보박스 생성
            ComboBox comboBox = new ComboBox();
            comboBox.Name = cutOffstatusCodeValue + "ComboBox";
            comboBox.Location = new Point(xx2, yy2);
            comboBox.Size = new Size(80, 29);
            comboBox.Tag = tag;
            comboBox.Items.Clear();

            List<CommonCodeItem> list = Global.GetCommonCodeList(_cutOffList[tag]);

            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                comboBox.Items.Add(item);
            }

            // 기존 예약건의 진행상태정보가 있으면 콤보에 최종 상태 저장값을 설정하고 그렇지 않으면 선택하도록 처리
            if (comboBox.Items.Count > 0)
            {
                if (cutOffstatusCodeValue.Length > 0)
                {
                    Utils.SelectComboBoxItemByValue(comboBox, cutOffstatusCodeValue);  // 콤보박스 값 설정
                }
                else
                {
                    comboBox.SelectedIndex = -1;
                }
            }

            comboBox.SelectedIndexChanged += new EventHandler(cutOffCheckComboBox_SelectedChange);

            this.Controls.Add(comboBox);
            _ComboBoxArray[tag] = comboBox;
        }

        //=========================================================================================================================================================================
        // 진행상태 콤보박스 항목 변경 이벤트 동적 생성
        //=========================================================================================================================================================================
        private void cutOffCheckComboBox_SelectedChange(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            int tag = (int)comboBox.Tag;

            int selecteComboBoxItem = comboBox.SelectedIndex;
        }

        //=========================================================================================================================================================================
        // 진행확인일시 동적 생성
        //=========================================================================================================================================================================
        private void setCreateCutOffDateTimePicker(int tag, string cutOffCheckDateTime, int xx3, int yy3)
        {
            // DateTimePicker 생성
            DateTimePicker dateTimePicker = new DateTimePicker();
            dateTimePicker.Location = new Point(xx3, yy3);
            dateTimePicker.Size = new Size(250, 29);
            dateTimePicker.Tag = tag;

            if (cutOffCheckDateTime.Equals("0000-00-00 00:00:00")) cutOffCheckDateTime = "";

            if (cutOffCheckDateTime == "")
            {
                dateTimePicker.Value = Utils.GetDateTimeFormatFromString(cutOffCheckDateTime);
            }
            else
            {
                dateTimePicker.Value = DateTime.Now;
            }

            this.Controls.Add(dateTimePicker);
            _DateTimePickerArray[tag] = dateTimePicker;
        }

        //=========================================================================================================================================================================
        // 진행확인버튼 동적 생성
        //=========================================================================================================================================================================
        private void setCreateCutOffButton(int tag, int xx4, int yy4)
        {
            // 버튼 생성
            Button button = new Button();
            button.Location = new Point(xx4, yy4);
            //button.BackgroundImage = TripERP.Properties.Resources.check;
            button.Text = "확인";
            button.Size = new Size(50, 30);
            button.Tag = tag;
            //button.BackgroundImage = TripERP.Properties.Resources.check;
            button.Click += new EventHandler(cutOffCheckButton_Click);
            this.Controls.Add(button);
            _ButtonArray[tag] = button;
        }

        //=========================================================================================================================================================================
        // 확인버튼 클릭 이벤트 동적 생성
        //=========================================================================================================================================================================
        private void cutOffCheckButton_Click(object sender, EventArgs e)
        {
            if (_cutOffList.Count == 0) return;

            // 명단확인
            Button button = sender as Button;

            int tag = (int)button.Tag;

            CheckCutOffStatus(_ComboBoxArray[tag], _cutOffList[tag], _DateTimePickerArray[tag]);
        }

        //=========================================================================================================================================================================
        // 진행상태 콤보박스 동적 생성 
        //=========================================================================================================================================================================
        private void setCutOffComboBox(ComboBox comboBox, string DomainEngName, int x, int y)
        {
            comboBox.Location = new Point(x, y);
            comboBox.Size = new Size(80, 29);
            comboBox.Items.Clear();
            List<CommonCodeItem> list = Global.GetCommonCodeList(DomainEngName);

            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                comboBox.Items.Add(item);
            }

            comboBox.Visible = true;
            comboBox.Refresh();
        }

        //=========================================================================================================================================================================
        // 진행 확인일자 DateTimePicker 동적 생성
        //=========================================================================================================================================================================
        private void setCutOffDateTimePicker(DateTimePicker dateTimePicker, int x, int y)
        {
            dateTimePicker.Location = new Point(x, y);
            dateTimePicker.Size = new Size(200, 29);
            dateTimePicker.Value = DateTime.Now;
            this.Controls.Add(dateTimePicker);
            dateTimePicker.Visible = true;
            dateTimePicker.Refresh();
        }

        //=========================================================================================================================================================================
        // 저장된 예약옵션정보를 입력필드에 Set.
        //=========================================================================================================================================================================
        private void SetReservationOptionList()
        {
            double optionTotalAmount = 0;

            optionDataGridView.Rows.Clear();

            string query = string.Format("CALL SelectRsvtOptnList ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("옵션정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string OPTN_CNMB = dataRow["OPTN_CNMB"].ToString();                                      // 옵션일련번호
                string OPTN_NM = dataRow["OPTN_NM"].ToString();                                          // 옵션명
                string CUR_CD = dataRow["CUR_CD"].ToString();                                            // 옵션통화코드
                string CUR_NM = dataRow["CUR_NM"].ToString();                                            // 옵션통화명
                string PRDT_OPTN_SALE_AMT = Utils.SetComma(dataRow["PRDT_OPTN_SALE_AMT"].ToString());                   // 상품옵션판매금액
                string NMPS_NBR = dataRow["NMPS_NBR"].ToString();                                                       // 인원수

                double optionSubtotal = Convert.ToInt32(Math.Round(Double.Parse(PRDT_OPTN_SALE_AMT) * int.Parse(NMPS_NBR)));   // 옵션소계금액
                optionTotalAmount = optionTotalAmount + optionSubtotal;

                string TOT_OPTN_SALE_AMT = Utils.SetComma(optionSubtotal.ToString());

                optionDataGridView.Rows.Add(OPTN_CNMB, OPTN_NM, CUR_CD, CUR_NM, PRDT_OPTN_SALE_AMT, NMPS_NBR, TOT_OPTN_SALE_AMT);
            }

            totalOptionAmountTextBox.Text = Utils.SetComma(optionTotalAmount.ToString());

            optionDataGridView.ClearSelection();
            optionDataGridView.Refresh();
        }

        //=========================================================================================================================================================================
        // 원가정보 화면 출력
        //=========================================================================================================================================================================
        private void SetReservationCostPriceList()
        {
            double costPriceSumAmount = 0;

            costPriceDataGridView.Rows.Clear();

            string query = string.Format("CALL SelectRsvtCsprList ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("원가정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CSPR_CNMB = Utils.GetString(dataRow["CSPR_CNMB"]);                   // 원가일련번호
                string CSPR_NM = Utils.GetString(dataRow["CSPR_NM"]);                       // 원가명
                string ARPL_CMPN_NO = Utils.GetString(dataRow["ARPL_CMPN_NO"]);             // 수배처코드
                string ARPL_CMPN_NM = Utils.GetString(dataRow["ARPL_CMPN_NM"]);             // 수배처명 
                string CSPR_CUR_CD = Utils.GetString(dataRow["CSPR_CUR_CD"]);               // 통화코드
                string CSPR_CUR_NM = Utils.GetString(dataRow["CSPR_CUR_NM"]);               // 통화코드
                string ADLT_FRCR_CSPR_AMT = Utils.GetString(dataRow["ADLT_FRCR_CSPR_AMT"]); // 성인외화원가금액
                string CHLD_FRCR_CSPR_AMT = Utils.GetString(dataRow["CHLD_FRCR_CSPR_AMT"]); // 소아외화원가금액
                string INFN_FRCR_CSPR_AMT = Utils.GetString(dataRow["INFN_FRCR_CSPR_AMT"]); // 유아외화원가금액
                string ADLT_NBR = Utils.GetString(dataRow["ADLT_NBR"]);                     // 성인수
                string CHLD_NBR = Utils.GetString(dataRow["CHLD_NBR"]);                     // 소아수
                string INFN_NBR = Utils.GetString(dataRow["INFN_NBR"]);                     // 유아수

                double costPriceSubTotal = calcCostPrice(ADLT_FRCR_CSPR_AMT, CHLD_FRCR_CSPR_AMT, INFN_FRCR_CSPR_AMT, ADLT_NBR, CHLD_NBR, INFN_NBR);
                costPriceSumAmount = costPriceSumAmount + costPriceSubTotal;

                costPriceDataGridView.Rows.Add
                (
                    CSPR_CNMB,
                    CSPR_NM,
                    ARPL_CMPN_NO,
                    ARPL_CMPN_NM,
                    CSPR_CUR_CD,
                    CSPR_CUR_NM,
                    Utils.SetComma(ADLT_FRCR_CSPR_AMT),
                    Utils.SetComma(ADLT_NBR),
                    Utils.SetComma(CHLD_FRCR_CSPR_AMT),
                    Utils.SetComma(CHLD_NBR),
                    Utils.SetComma(INFN_FRCR_CSPR_AMT),
                    Utils.SetComma(INFN_NBR),
                    Utils.SetComma(costPriceSubTotal.ToString())
                );
            }

            totalCostPriceAmountTextBox.Text = Utils.SetComma(costPriceSumAmount.ToString());
            costPriceDataGridView.ClearSelection();
        }

        //=========================================================================================================================================================================
        // 원가계산
        //=========================================================================================================================================================================
        double calcCostPrice(string ADLT_FRCR_CSPR_AMT, string CHLD_FRCR_CSPR_AMT, string INFN_FRCR_CSPR_AMT, string ADLT_NBR, string CHLD_NBR, string INFN_NBR)
        {
            if (ADLT_FRCR_CSPR_AMT.Equals("")) ADLT_FRCR_CSPR_AMT = "0";
            if (CHLD_FRCR_CSPR_AMT.Equals("")) CHLD_FRCR_CSPR_AMT = "0";
            if (INFN_FRCR_CSPR_AMT.Equals("")) INFN_FRCR_CSPR_AMT = "0";
            if (ADLT_NBR.Equals("")) ADLT_NBR = "0";
            if (CHLD_NBR.Equals("")) CHLD_NBR = "0";
            if (INFN_NBR.Equals("")) INFN_NBR = "0";

            // 원가에 인원수를 곱하여 원가소계금액 산출
            double adultCostPrice = Convert.ToDouble(ADLT_FRCR_CSPR_AMT);
            double childCostPrice = Convert.ToDouble(CHLD_FRCR_CSPR_AMT);
            double infantCostPrice = Convert.ToDouble(INFN_FRCR_CSPR_AMT);

            int adultPeopleCount = Convert.ToInt16(ADLT_NBR);
            int childPeopleCount = Convert.ToInt16(CHLD_NBR);
            int infantPeopleCount = Convert.ToInt16(INFN_NBR);

            double sumAdultCostPrice = adultCostPrice * adultPeopleCount;     // 성인원가합계
            double sumChildCostPrice = childCostPrice * childPeopleCount;     // 소아원가합계
            double sumInfantCostPrice = infantCostPrice * infantPeopleCount;  // 유아원가합계

            double costPriceSubTotal = sumAdultCostPrice + sumChildCostPrice + sumInfantCostPrice;  // 원가소계

            return costPriceSubTotal;
        }

        //=========================================================================================================================================================================
        // 메모정보조회
        //=========================================================================================================================================================================
        private void SetMemoList()
        {
            cutOffMgtMemoDataGridView.Rows.Clear();

            string query = string.Format("CALL SelectDocuSndList ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("메모정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string FRST_RGST_DTM = Utils.GetString(dataRow["FRST_RGST_DTM"]);                              // 등록일
                FRST_RGST_DTM = FRST_RGST_DTM.Substring(0, 10);
                string CUST_NM = Utils.GetString(dataRow["CUST_NM"]);                                          // 고객명 
                string RSVT_PRGS_DVSN_CD = Utils.GetString(dataRow["RSVT_PRGS_DVSN_CD"]);                      // 예약진행구분코드 
                string RSVT_PRGS_DVSN_NAME = Global.GetCommonCodeDesc("RSVT_PRGS_DVSN_CD", RSVT_PRGS_DVSN_CD); // 예약진행구분명 
                //string FILE_NM = Utils.GetString(dataRow["FILE_NM"]);                                          // 파일명
                //string EMPL_NM = Utils.GetString(dataRow["EMPL_NM"]);                                          // 담당자명

                cutOffMgtMemoDataGridView.Rows.Add(FRST_RGST_DTM, CUST_NM, RSVT_PRGS_DVSN_NAME);
            }

            cutOffMgtMemoDataGridView.ClearSelection();
        }


        public void SetReservationNumber(string reservationNumber)
        {
            _reservationNumber = reservationNumber;
        }

        public void SetSaveMode(string saveMode)
        {
            _saveMode = saveMode;
        }

        //=========================================================================================================================================================================
        // 예약목록에서 예약건을 선택했을 때 예약번호를 전역변수에 저장
        //=========================================================================================================================================================================
        public void SetReservationDataGridView(DataGridView reservationDataGridView)
        {
            _reservationDataGridView = reservationDataGridView;
        }

        //=========================================================================================================================================================================
        // 박이 변경될 때 이벤트 처리
        //=========================================================================================================================================================================
        private void nightComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (nightComboBox.SelectedIndex != -1)
                return;

            string night = (nightComboBox.SelectedItem as string);
            dayComboBox.SelectedItem = (Convert.ToInt32(night) + 1).ToString();
            */
        }

        //=========================================================================================================================================================================
        // 숙박일수가 변경되면 도착일자를 자동 계산
        //=========================================================================================================================================================================
        private void dayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dayComboBox.SelectedIndex != -1)
                return;

            string night = (nightComboBox.SelectedItem as string);
            arrivalDateTimePicker.Value = departureDateTimePicker.Value.AddDays(Convert.ToInt32(night) + 1);
            arrivalDateTimePicker.Refresh();
        }

        //=========================================================================================================================================================================
        // 기본판매가격 설정
        //=========================================================================================================================================================================
        private void setDefaultSalesPriceButton_Click(object sender, EventArgs e)
        {
            if (productComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품을 선택해 주십시오.");
                productComboBox.Focus();
                return;
            }
            if (productGradeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품등급을 선택해 주십시오.");
                productGradeComboBox.Focus();
                return;
            }

            setDefaultSalesPrice();
        }

        //=========================================================================================================================================================================
        // 기본판매가격 계산
        //=========================================================================================================================================================================
        private void setDefaultSalesPrice()
        {
            string ADLT_NBR = adultPeopleCountTextBox.Text.Trim();
            if (ADLT_NBR == "") ADLT_NBR = "0";

            string CHLD_NBR = childPeopleCountTextBox.Text.Trim();
            if (CHLD_NBR == "") CHLD_NBR = "0";

            string INFN_NBR = infantPeopleCountTextBox.Text.Trim();
            if (INFN_NBR == "") INFN_NBR = "0";

            _numberOfAdult = Int16.Parse(ADLT_NBR);
            _numberOfChild = Int16.Parse(CHLD_NBR);
            _numberOfInfant = Int16.Parse(INFN_NBR);

            if (_numberOfAdult == 0 && _numberOfChild == 0 && _numberOfInfant == 0)
            {
                MessageBox.Show("인원수를 입력하세요.");
                adultPeopleCountTextBox.Focus();
                return;
            }

            string productCode = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            string gradeCode = (productGradeComboBox.SelectedItem as ComboBoxItem).Value.ToString();

            string query = string.Format("CALL SelectPrdtDtlsItem ( {0}, '{1}' )", productCode, gradeCode);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("판매가 정보를 가져올 수 없습니다.");
                return;
            }

            DataRow row = dataSet.Tables[0].Rows[0];

            string ADLT_SALE_PRCE = "0";
            string CHLD_SALE_PRCE = "0";
            string INFN_SALE_PRCE = "0";

            if (_numberOfAdult > 0)
            {
                ADLT_SALE_PRCE = row["ADLT_SALE_PRCE"].ToString();
            }

            if (_numberOfChild > 0)
            {
                CHLD_SALE_PRCE = row["CHLD_SALE_PRCE"].ToString();
            }

            if (_numberOfInfant > 0)
            {
                INFN_SALE_PRCE = row["INFN_SALE_PRCE"].ToString();
            }

            adultSalesPriceTextBox.Text = Utils.SetComma(ADLT_SALE_PRCE);                               // 성인판매가격
            childSalesPriceTextBox.Text = Utils.SetComma(CHLD_SALE_PRCE);                               // 소아판매가격
            infantSalesPriceTextBox.Text = Utils.SetComma(INFN_SALE_PRCE);                              // 유아판매가격

            string SALE_CUR_CD = row["SALE_CUR_CD"].ToString();                                         // 판매통화코드
            Utils.SelectComboBoxItemByValue(saleCurrencyCodeComboBox, SALE_CUR_CD);                     // 판매통화코드

            // 판매합계금액 계산 = (성인판매가*성인수) + (소아판매가*소아수) + (유아판매가*유아수)
            double adultTotalSaleAmount = Convert.ToDouble(row["ADLT_SALE_PRCE"].ToString()) * _numberOfAdult;
            double childTotalSaleAmount = Convert.ToDouble(row["CHLD_SALE_PRCE"].ToString()) * _numberOfChild;
            double infantTotalSaleAmount = Convert.ToDouble(row["INFN_SALE_PRCE"].ToString()) * _numberOfInfant;

            double SaleSumAmount = adultTotalSaleAmount + childTotalSaleAmount + infantTotalSaleAmount;

            totalSalePriceTextBox.Text = Utils.SetComma(SaleSumAmount.ToString());                       // 판매가격합계

            adultSalesPriceSubTotTextBox.Text = Utils.SetComma(adultTotalSaleAmount.ToString());         // 성인판매가소계
            childSalesPriceSubTotTextBox.Text = Utils.SetComma(childTotalSaleAmount.ToString());         // 성인판매가소계
            infantSalesPriceSubTotTextBox.Text = Utils.SetComma(infantTotalSaleAmount.ToString());       // 성인판매가소계
            salesPriceSumTextBox.Text = Utils.SetComma(SaleSumAmount.ToString());                        // 성인판매가소계

            string OPTN_SUM_AMT = OptionSumAmountTextBox.Text.Trim();
            if (OPTN_SUM_AMT == "") OPTN_SUM_AMT = "0";
            double optionTotalAmount = Double.Parse(OPTN_SUM_AMT);

            // 총판매금액 계산 = (판매합계금액 + 옵션합계금액)
            double totalSaleAmount = SaleSumAmount + optionTotalAmount;
            salesPriceTextBox.Text = Utils.SetComma(totalSaleAmount.ToString());                         // 판매총액

            // 총인원수 계산 = (성인수 + 소아수 + 유아수)
            _numberOfPeople = _numberOfAdult + _numberOfChild + _numberOfInfant;
            totalNumberOfPersonTextBox.Text = _numberOfPeople.ToString();

            // 미수잔액 재계산
            double unReceivableAmount = totalSaleAmount - (vOReservationDetail.RSVT_AMT - vOReservationDetail.PRPY_AMT + vOReservationDetail.TOT_RECT_AMT + vOReservationDetail.MDPY_AMT);
            outstandingPriceTextBox.Text = Utils.SetComma(unReceivableAmount.ToString());
        }

        private void Delete_dataGridView(DataGridView dgv)
        {
            // Line(행) 삭제
            foreach (DataGridViewRow dgr in dgv.SelectedRows)
            {
                dgv.Rows.Remove(dgr);
            }
        }

        //=========================================================================================================================================================================
        // 예약옵션정보 그리드 출력
        //=========================================================================================================================================================================
        private void searchReservationOptionList()
        {
            // 기본가격을 세팅하기 전에 옵션그리드를 초기화
            //Utils.ClearDataGridView(optionDataGridView);

            //Delete_dataGridView(optionDataGridView);
            optionDataGridView.Rows.Clear();

            // 상품옵션내역을 검색하여 옵션그리드에 추가
            string productCode = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            string gradeCode = (productGradeComboBox.SelectedItem as ComboBoxItem).Value.ToString();

            string PRDT_CNMB = "";
            string PRDT_GRAD_CD = "";
            string OPTN_CNMB = "";
            string PRDT_OPTN_NM = "";
            string CUR_CD = "";
            string CUR_NM = "";
            string PRDT_OPTN_SALE_AMT = "";

            // 인원수 계산
            int totalNumberOfPerson = _numberOfAdult + _numberOfChild + _numberOfInfant;
            double totalOptionAmount = 0;  // 옵션총액 계산용 변수

            string query = string.Format("CALL SelectPrdtOptnItem ( '{0}', '{1}' )", productCode, gradeCode);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("옵션정보가 등록되어 있지 않습니다. 담당자에게 문의하세요.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();
                PRDT_GRAD_CD = datarow["PRDT_GRAD_CD"].ToString();
                OPTN_CNMB = datarow["OPTN_CNMB"].ToString();
                PRDT_OPTN_NM = datarow["PRDT_OPTN_NM"].ToString();
                CUR_CD = datarow["CUR_CD"].ToString();
                CUR_NM = datarow["CUR_NM"].ToString();
                PRDT_OPTN_SALE_AMT = Utils.SetComma(datarow["PRDT_OPTN_SALE_AMT"].ToString());

                // 옵션가격에 인원수를 곱하여 옵션소계금액 산출
                //int _numberOfPersons = Int32.Parse(adultPeopleCountTextBox.Text.Trim()) + Int32.Parse(childPeopleCountTextBox.Text.Trim()) + Int32.Parse(infantPeopleCountTextBox.Text.Trim());
                double OptionSumAmount = Convert.ToDouble(PRDT_OPTN_SALE_AMT);
                OptionSumAmount = OptionSumAmount * totalNumberOfPerson;  // 옵션소계
                totalOptionAmount = totalOptionAmount + OptionSumAmount;  // 옵션총계
                optionDataGridView.Rows.Add(PRDT_CNMB, PRDT_OPTN_NM, CUR_CD, CUR_NM, PRDT_OPTN_SALE_AMT, totalNumberOfPerson.ToString(), Utils.SetComma(OptionSumAmount.ToString()));
            }

            optionDataGridView.ClearSelection();

            totalOptionAmountTextBox.Text = Utils.SetComma(totalOptionAmount.ToString());  // 옵션총계 화면 표시
        }

        //=========================================================================================================================================================================
        // 옵션기본가격 적용
        //=========================================================================================================================================================================
        private void setOptionSalesPriceButton_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }
            if (productComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품을 선택해 주십시오.");
                productComboBox.Focus();
                return;
            }
            if (productGradeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품등급을 선택해 주십시오.");
                productGradeComboBox.Focus();
                return;
            }
            if (totalNumberOfPersonsTextbox.Text.Trim() == "" || totalNumberOfPersonsTextbox.Text.Trim() == "0")
            {
                MessageBox.Show("인원수는 필수 입력항목입니다.");
                totalNumberOfPersonsTextbox.Focus();
                return;
            }

            DialogResult result = MessageBoxEx.Show("옵션정보가 기본값으로 저장됩니다. 계속하시겠습까?", "옵션 기본값 설정", "예", "아니오");
            if (result == DialogResult.No)
            {
                return;
            }

            //searchReservationOptionList();

            // 상품옵션내역의 옵션정보를 예약옵션내역으로 Copy
            createReservationOptionInfo();

            // 옵션입력필드 초기화
            ResetOptionInputField();

            // 옵션그리드 Refresh
            SetReservationOptionList();
        }

        //=========================================================================================================================================================================
        // 상품옵션내역의 옵션정보를 예약옵션내역으로 Copy
        //=========================================================================================================================================================================
        private void createReservationOptionInfo()
        {
            if (totalNumberOfPersonsTextbox.Text.Trim() == "") totalNumberOfPersonsTextbox.Text = "0";
            int NMPS_NBR = Int16.Parse(totalNumberOfPersonsTextbox.Text);

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = string.Format("CALL InsertRsvtOptnFromPrdtOptn ('{0}','{1}','{2}','{3}','{4}')",
                            _reservationNumber, _productNo, _productGradeCode, NMPS_NBR, Global.loginInfo.ACNT_ID);

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("기본옵션정보를 설정할 수 없습니다.");
                return;
            }
            else
            {
                refreshReservationPriceInfo();
                MessageBox.Show("기본옵션정보를 설정했습니다.");
            }
        }

        //=========================================================================================================================================================================
        // 원가 기본값 검색 및 그리드 세팅
        //=========================================================================================================================================================================
        private void setDefaultCostPriceButton_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }

            if (adultPeopleCountForCostPriceTextBox.Text.Trim() == "") adultPeopleCountForCostPriceTextBox.Text = "0";
            if (childPeopleCountForCostPriceTextBox.Text.Trim() == "") childPeopleCountForCostPriceTextBox.Text = "0";
            if (infantPeopleCountForCostPriceTextBox.Text.Trim() == "") infantPeopleCountForCostPriceTextBox.Text = "0";

            int adultPeopleCount = Int16.Parse(adultPeopleCountForCostPriceTextBox.Text);
            int childPeopleCount = Int16.Parse(childPeopleCountForCostPriceTextBox.Text);
            int infantPeopleCount = Int16.Parse(infantPeopleCountForCostPriceTextBox.Text);

            if (adultPeopleCount == 0 && childPeopleCount == 0 && infantPeopleCount == 0)
            {
                MessageBox.Show("인원수를 확인하세요.");
                if (adultPeopleCount == 0)
                {
                    adultPeopleCountForCostPriceTextBox.Focus();
                }
                else if (adultPeopleCount > 0 && childPeopleCount == 0)
                {
                    childPeopleCountForCostPriceTextBox.Focus();
                }
                else if (adultPeopleCount > 0 && childPeopleCount > 0 && infantPeopleCount == 0)
                {
                    infantPeopleCountForCostPriceTextBox.Focus();
                }

                return;
            }

            DialogResult result = MessageBoxEx.Show("원가정보가 사전에 설정된 상품의 원가 기본값으로 저장됩니다. 계속하시겠습까?", "원가 기본값 설정", "예", "아니오");
            if (result == DialogResult.No)
            {
                return;
            }

            // 기본가격을 세팅하기 전에 원가그리드를 초기화
            costPriceDataGridView.Rows.Clear();

            string productCode = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            string gradeCode = (productGradeComboBox.SelectedItem as ComboBoxItem).Value.ToString();

            // 상품원가내역의 원가정보를 예약원가내역으로 Copy
            createReservationCostPriceInfo(adultPeopleCount, childPeopleCount, infantPeopleCount);

            // 원가입력필드 초기화
            ResetCostPriceInputField();

            // 원가그리드 Refresh
            SetReservationCostPriceList();
        }

        //=========================================================================================================================================================================
        // 상품원가내역의 원가정보를 예약원가내역으로 Copy
        //=========================================================================================================================================================================
        private void createReservationCostPriceInfo(int adultPeopleCount, int childPeopleCount, int infantPeopleCount)
        {
            string RSVT_NO = _reservationNumber;
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = string.Format("CALL InsertRsvtCostPriceFromPrdtCostPrice ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            RSVT_NO, _productNo, _productGradeCode, adultPeopleCount, childPeopleCount, infantPeopleCount, FRST_RGTR_ID);

            queryStringArray[0] = query;
            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("기본원가정보를 설정할 수 없습니다.");
                return;
            }
            else
            {
                string CSPR_SUM_AMT = queryResultArray[0];

                totalCostPriceAmountTextBox.Text = Utils.SetComma(CSPR_SUM_AMT);
                adultPeopleCountForCostPriceTextBox.Text = adultPeopleCount.ToString();
                childPeopleCountForCostPriceTextBox.Text = childPeopleCount.ToString();
                infantPeopleCountForCostPriceTextBox.Text = infantPeopleCount.ToString();

                MessageBox.Show("기본원가정보를 설정했습니다.");
            }
        }

        //=========================================================================================================================================================================
        // 원가정보 저장
        //=========================================================================================================================================================================
        private void saveCostPriceInfoButton_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }

            // 입력값 유효성 검증
            if (ValidateForCostPriceInfo() == false) return;

            // 이전에 등록되어 있는 원가정보는 일괄삭제 후 Insert
            string RSVT_NO = _reservationNumber;
            string CSPR_CNMB = _costPriceNo;
            string CSPR_NM = costPriceNameTextBox.Text.Trim();
            string ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(destinationCompanyForCostPriceComboBox);
            string ARPL_CMPN_NM = destinationCompanyForCostPriceComboBox.Text.Trim();
            string CSPR_CUR_CD = Utils.GetSelectedComboBoxItemValue(costPricecurrencyCodeComboBox);
            string CSPR_CUR_NM = costPricecurrencyCodeComboBox.Text.Trim();

            if (adultCostPriceTextBox.Text.Trim() == "") adultCostPriceTextBox.Text = "0";
            if (childCostPriceTextBox.Text.Trim() == "") childCostPriceTextBox.Text = "0";
            if (infantCostPriceTextBox.Text.Trim() == "") infantCostPriceTextBox.Text = "0";

            if (adultPeopleCountForCostPriceTextBox.Text.Trim() == "") adultPeopleCountForCostPriceTextBox.Text = "0";
            if (childPeopleCountForCostPriceTextBox.Text.Trim() == "") childPeopleCountForCostPriceTextBox.Text = "0";
            if (infantPeopleCountForCostPriceTextBox.Text.Trim() == "") infantPeopleCountForCostPriceTextBox.Text = "0";

            string ADLT_FRCR_CSPR_AMT = Utils.GetDoubleString(adultCostPriceTextBox.Text);
            string CHLD_FRCR_CSPR_AMT = Utils.GetDoubleString(childCostPriceTextBox.Text);
            string INFN_FRCR_CSPR_AMT = Utils.GetDoubleString(infantCostPriceTextBox.Text);

            int adultPeopleCount = Int16.Parse(adultPeopleCountForCostPriceTextBox.Text);
            int childPeopleCount = Int16.Parse(childPeopleCountForCostPriceTextBox.Text);
            int infantPeopleCount = Int16.Parse(infantPeopleCountForCostPriceTextBox.Text);

            string CSPR_SUM_AMT = "";
            string CSPR_AMT_SUB_TOT = "";
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = "";

            // 원가일련번호가 없으면 예약원가내역을 Insert하고 있으면 Update
            if (_costPriceNo == "")
            {
                query = string.Format("CALL InsertRsvtCsprItem ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                                       RSVT_NO, CSPR_NM, ARPL_CMPN_NO, CSPR_CUR_CD, ADLT_FRCR_CSPR_AMT, CHLD_FRCR_CSPR_AMT, INFN_FRCR_CSPR_AMT,
                                       adultPeopleCount, childPeopleCount, infantPeopleCount, FRST_RGTR_ID);
            }
            else
            {
                query = string.Format("CALL UpdateRsvtCsprItem ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                                       RSVT_NO, CSPR_CNMB, CSPR_NM, ARPL_CMPN_NO, CSPR_CUR_CD, ADLT_FRCR_CSPR_AMT, CHLD_FRCR_CSPR_AMT, INFN_FRCR_CSPR_AMT,
                                       adultPeopleCount, childPeopleCount, infantPeopleCount, FRST_RGTR_ID);
            }

            queryStringArray[0] = query;
            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("원가정보를 저장할 수 없습니다.");
                return;
            }

            /*
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("원가정보를 저장할 수 없습니다.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];
            CSPR_SUM_AMT = dataRow["CSPR_SUM_AMT"].ToString();                                          // 원가합계금액

            // 원가소계 산출 및 필드 반영
            double costPriceSubTotal = Math.Round(Double.Parse(ADLT_FRCR_CSPR_AMT) * adultPeopleCount) +
                                       Math.Round(Double.Parse(CHLD_FRCR_CSPR_AMT) * childPeopleCount) +
                                       Math.Round(Double.Parse(INFN_FRCR_CSPR_AMT) * infantPeopleCount);
            OptionSumAmountTextBox.Text = CSPR_AMT_SUB_TOT;                                             // 원가소계금액
            OptionSumAmountTextBox.Text = Utils.SetComma(costPriceSubTotal.ToString());                 // 원가소계금액

            // 원가가 추가된 경우 그리드에 추가하고, 변경된 경우에는 그리드를 변경
            if (_costPriceNo == "")
            {
                CSPR_CNMB = dataRow["CSPR_CNMB"].ToString();                                            // 채번된 원가일련번호

                costPriceDataGridView.Rows.Add
                (
                    CSPR_CNMB,
                    CSPR_NM,
                    ARPL_CMPN_NO,
                    ARPL_CMPN_NM,
                    CSPR_CUR_CD,
                    CSPR_CUR_NM,
                    Utils.SetComma(ADLT_FRCR_CSPR_AMT),
                    Utils.SetComma(adultPeopleCount.ToString()),
                    Utils.SetComma(CHLD_FRCR_CSPR_AMT),
                    Utils.SetComma(childPeopleCount.ToString()),
                    Utils.SetComma(INFN_FRCR_CSPR_AMT),
                    Utils.SetComma(infantPeopleCount.ToString()),
                    Utils.SetComma(CSPR_AMT_SUB_TOT)
                );
            }
            else
            {
                costPriceDataGridView["CSPR_CNMB", _clickCostPriceRow].Value = CSPR_CNMB;
                costPriceDataGridView["CSPR_NM", _clickCostPriceRow].Value = CSPR_NM;
                costPriceDataGridView["CSPR_NM", _clickCostPriceRow].Value = ARPL_CMPN_NO;
                costPriceDataGridView["CSPR_NM", _clickCostPriceRow].Value = ARPL_CMPN_NM;

                costPriceDataGridView["CSPR_CUR_CD", _clickCostPriceRow].Value = CSPR_CUR_CD;
                costPriceDataGridView["CSPR_CUR_NM", _clickCostPriceRow].Value = CSPR_CUR_NM;

                costPriceDataGridView["ADLT_FRCR_CSPR_AMT", _clickCostPriceRow].Value = Utils.SetComma(ADLT_FRCR_CSPR_AMT);
                costPriceDataGridView["ADLT_NBR", _clickCostPriceRow].Value = Utils.SetComma(adultPeopleCount.ToString());

                costPriceDataGridView["CHLD_FRCR_CSPR_AMT", _clickCostPriceRow].Value = Utils.SetComma(CHLD_FRCR_CSPR_AMT);
                costPriceDataGridView["CHLD_NBR", _clickCostPriceRow].Value = Utils.SetComma(childPeopleCount.ToString());

                costPriceDataGridView["INFN_FRCR_CSPR_AMT", _clickCostPriceRow].Value = Utils.SetComma(INFN_FRCR_CSPR_AMT);
                costPriceDataGridView["INFN_NBR", _clickCostPriceRow].Value = Utils.SetComma(infantPeopleCount.ToString());

                costPriceDataGridView["CSPR_AMT_SUB_TOT", _clickCostPriceRow].Value = Utils.SetComma(CSPR_AMT_SUB_TOT);
            }
            */
            // 원가총계를 화면에 표시
            totalCostPriceAmountTextBox.Text = Utils.SetComma(CSPR_SUM_AMT);

            SetReservationCostPriceList();        // 예약원가조회

            // 원가그리드 refresh
            //costPriceDataGridView.Refresh();

            // 원가 입력필드 초기화
            ResetCostPriceInputField();
        }

        //=========================================================================================================================================================================
        // 원가 그리드 행 클릭
        //=========================================================================================================================================================================
        private void costPriceDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (costPriceDataGridView.SelectedRows.Count == 0)
                return;

            // 선택한 원가그리드 행번호를 기억
            _clickCostPriceRow = costPriceDataGridView.CurrentCell.RowIndex;
            _costPriceNo = costPriceDataGridView.SelectedRows[0].Cells["CSPR_CNMB"].Value.ToString();                     // 원가일련번호

            costPriceDataGridViewRowChoice();
        }

        //=========================================================================================================================================================================
        // 원가그리드 행 선택
        //=========================================================================================================================================================================
        private void costPriceDataGridViewRowChoice()
        {
            string CSPR_CNMB = costPriceDataGridView.SelectedRows[0].Cells["CSPR_CNMB"].Value.ToString();                     // 원가일련번호
            string CSPR_NM = costPriceDataGridView.SelectedRows[0].Cells["CSPR_NM"].Value.ToString();                         // 원가명
            string ARPL_CMPN_NO = costPriceDataGridView.SelectedRows[0].Cells["ARPL_CMPN_NO"].Value.ToString();               // 수배처업체번호
            string ARPL_CMPN_NM = costPriceDataGridView.SelectedRows[0].Cells["ARPL_CMPN_NM"].Value.ToString();               // 수배처업체명
            string CSPR_CUR_CD = costPriceDataGridView.SelectedRows[0].Cells["CSPR_CUR_CD"].Value.ToString();                 // 원가통화코드
            string CSPR_CUR_NM = costPriceDataGridView.SelectedRows[0].Cells["CSPR_CUR_NM"].Value.ToString();                 // 원가통화명
            string ADLT_FRCR_CSPR_AMT = costPriceDataGridView.SelectedRows[0].Cells["ADLT_FRCR_CSPR_AMT"].Value.ToString();   // 성인원가금액
            string ADLT_NBR = costPriceDataGridView.SelectedRows[0].Cells["ADLT_NBR"].Value.ToString();                       // 성인수
            string CHLD_FRCR_CSPR_AMT = costPriceDataGridView.SelectedRows[0].Cells["CHLD_FRCR_CSPR_AMT"].Value.ToString();   // 소아원가금액
            string CHLD_NBR = costPriceDataGridView.SelectedRows[0].Cells["CHLD_NBR"].Value.ToString();                       // 소아수
            string INFN_FRCR_CSPR_AMT = costPriceDataGridView.SelectedRows[0].Cells["INFN_FRCR_CSPR_AMT"].Value.ToString();   // 유아원가금액
            string INFN_NBR = costPriceDataGridView.SelectedRows[0].Cells["INFN_NBR"].Value.ToString();                       // 유아수
            string CSPR_AMT_SUB_TOT = costPriceDataGridView.SelectedRows[0].Cells["CSPR_AMT_SUB_TOT"].Value.ToString();       // 원가소계

            costPriceNameTextBox.Text = CSPR_NM;                                                    // 원가명
            Utils.SelectComboBoxItemByValue(destinationCompanyForCostPriceComboBox, ARPL_CMPN_NO);  // 수배처업체 콤보박스

            Utils.SelectComboBoxItemByValue(costPricecurrencyCodeComboBox, CSPR_CUR_CD);            // 원가통화코드
            adultCostPriceTextBox.Text = Utils.SetComma(ADLT_FRCR_CSPR_AMT.ToString()); ;           // 성인원가금액
            childCostPriceTextBox.Text = Utils.SetComma(CHLD_FRCR_CSPR_AMT.ToString()); ;           // 소아원가금액
            infantCostPriceTextBox.Text = Utils.SetComma(INFN_FRCR_CSPR_AMT.ToString()); ;          // 유아원가금액
            costPriceSumAmountTextBox.Text = Utils.SetComma(CSPR_AMT_SUB_TOT.ToString());           // 원가소계

            adultPeopleCountForCostPriceTextBox.Text = ADLT_NBR;                                    // 성인수
            childPeopleCountForCostPriceTextBox.Text = CHLD_NBR;                                    // 소아수
            infantPeopleCountForCostPriceTextBox.Text = INFN_NBR;                                   // 유아수
        }

        //=========================================================================================================================================================================
        // 원가입력필드 초기화
        //=========================================================================================================================================================================
        private void ResetCostPriceInputField()
        {
            costPriceNameTextBox.Text = "";
            destinationCompanyForCostPriceComboBox.SelectedIndex = -1;
            costPricecurrencyCodeComboBox.SelectedIndex = -1;

            //exchangeRateTextBox.Text = "";
            adultCostPriceTextBox.Text = "0";

            adultPeopleCountForCostPriceTextBox.Text = "0";
            childCostPriceTextBox.Text = "0";
            childPeopleCountForCostPriceTextBox.Text = "0";
            infantCostPriceTextBox.Text = "0";
            infantPeopleCountForCostPriceTextBox.Text = "0";
            costPriceSumAmountTextBox.Text = "0";
            _costPriceNo = "";
            _clickCostPriceRow = -1;
        }


        //=========================================================================================================================================================================
        // 원가입력필드 유효성 검증
        //=========================================================================================================================================================================
        private Boolean ValidateForCostPriceInfo()
        {
            string CSPR_NM = costPriceNameTextBox.Text.Trim();
            string CSPR_CUR_CD = Utils.GetSelectedComboBoxItemValue(costPricecurrencyCodeComboBox);               // 통화코드
            string ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(destinationCompanyForCostPriceComboBox);     // 수배처업체번호

            if (adultCostPriceTextBox.Text == "") adultCostPriceTextBox.Text = "0";
            if (childCostPriceTextBox.Text == "") childCostPriceTextBox.Text = "0";
            if (infantCostPriceTextBox.Text == "") infantCostPriceTextBox.Text = "0";

            if (adultPeopleCountForCostPriceTextBox.Text.Trim() == "") adultPeopleCountForCostPriceTextBox.Text = "0";
            if (childPeopleCountForCostPriceTextBox.Text.Trim() == "") childPeopleCountForCostPriceTextBox.Text = "0";
            if (infantPeopleCountForCostPriceTextBox.Text.Trim() == "") infantPeopleCountForCostPriceTextBox.Text = "0";

            string ADLT_FRCR_CSPR_AMT = adultCostPriceTextBox.Text.Trim();
            string CHLD_FRCR_CSPR_AMT = childCostPriceTextBox.Text.Trim();
            string INFN_FRCR_CSPR_AMT = infantCostPriceTextBox.Text.Trim();

            double adultCostPrice = Double.Parse(ADLT_FRCR_CSPR_AMT);
            double childCostPrice = Double.Parse(CHLD_FRCR_CSPR_AMT);
            double infantCostPrice = Double.Parse(INFN_FRCR_CSPR_AMT);

            int adultPeopleCount = Int16.Parse(adultPeopleCountForCostPriceTextBox.Text);
            int childPeopleCount = Int16.Parse(childPeopleCountForCostPriceTextBox.Text);
            int infantPeopleCount = Int16.Parse(infantPeopleCountForCostPriceTextBox.Text);

            if (CSPR_NM == "")
            {
                MessageBox.Show("원가명은 필수 입력 항목입니다.");
                costPriceNameTextBox.Focus();
                return false;
            }

            if (CSPR_CUR_CD == "")
            {
                MessageBox.Show("통화코드를 선택하세요.");
                costPricecurrencyCodeComboBox.Focus();
                return false;
            }

            if (ARPL_CMPN_NO == "")
            {
                MessageBox.Show("수배처를 선택하세요.");
                destinationCompanyForCostPriceComboBox.Focus();
                return false;
            }

            if (adultCostPrice == 0 && childCostPrice == 0 && infantCostPrice == 0)
            {
                MessageBox.Show("원가금액이 입력되지 않았습니다");
                costPriceNameTextBox.Focus();
                return false;
            }

            if (adultCostPrice > 0 && adultPeopleCount == 0)
            {
                MessageBox.Show("성인 원가를 확인하세요");
                adultPeopleCountForCostPriceTextBox.Focus();
                return false;
            }

            if (adultCostPrice == 0 && adultPeopleCount > 0)
            {
                MessageBox.Show("성인 인원수를 확인하세요");
                adultCostPriceTextBox.Focus();
                return false;
            }

            if (childCostPrice > 0 && childPeopleCount == 0)
            {
                MessageBox.Show("소아 원가를 확인하세요");
                childPeopleCountForCostPriceTextBox.Focus();
                return false;
            }

            if (childCostPrice == 0 && childPeopleCount > 0)
            {
                MessageBox.Show("소아 인원수를 확인하세요");
                childCostPriceTextBox.Focus();
                return false;
            }

            if (infantCostPrice > 0 && infantPeopleCount == 0)
            {
                MessageBox.Show("유아 원가를 확인하세요");
                infantPeopleCountForCostPriceTextBox.Focus();
                return false;
            }

            if (infantCostPrice == 0 && infantPeopleCount > 0)
            {
                MessageBox.Show("유아 인원수를 확인하세요");
                infantCostPriceTextBox.Focus();
                return false;
            }

            return true;
        }

        //=========================================================================================================================================================================
        // 옵션그리드 행 클릭
        //=========================================================================================================================================================================
        private void optionDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (optionDataGridView.SelectedRows.Count == 0)
                return;

            _clickOptionRow = optionDataGridView.CurrentCell.RowIndex;
            optionDataGridViewRowChoice();
        }

        //=========================================================================================================================================================================
        // 옵션그리드 행 선택
        //=========================================================================================================================================================================
        private void optionDataGridViewRowChoice()
        {
            string OPTN_CNMB = optionDataGridView.SelectedRows[0].Cells["OPTN_CNMB"].Value.ToString();
            string OPTN_NM = optionDataGridView.SelectedRows[0].Cells["OPTN_NM"].Value.ToString();
            string CUR_CD = optionDataGridView.SelectedRows[0].Cells["CUR_CD"].Value.ToString();
            string CUR_NM = optionDataGridView.SelectedRows[0].Cells["CUR_NM"].Value.ToString();
            string PRDT_OPTN_SALE_AMT = optionDataGridView.SelectedRows[0].Cells["PRDT_OPTN_SALE_AMT"].Value.ToString();
            string TOT_PRDT_OPTN_SALE_AMT = optionDataGridView.SelectedRows[0].Cells["TOT_PRDT_OPTN_SALE_AMT"].Value.ToString();
            string NMPS_NBR = optionDataGridView.SelectedRows[0].Cells["NMPS_NBR"].Value.ToString();

            optionNameTextBox.Text = OPTN_NM;                                                  // 옵션명
            Utils.SelectComboBoxItemByValue(optionCurrencyCodeComboBox, CUR_CD);               // 옵션통화코드
            optionSaleAmountTextBox.Text = Utils.SetComma(PRDT_OPTN_SALE_AMT);                 // 옵션판매금액
            totalNumberOfPersonsTextbox.Text = NMPS_NBR;                                       // 인원수
            OptionSumAmountTextBox.Text = Utils.SetComma(TOT_PRDT_OPTN_SALE_AMT);              // 옵션판매금액소계
            _optionNo = OPTN_CNMB;
        }

        //=========================================================================================================================================================================
        // 옵션가격 저장 (옵션그리드의 내용을 예약옵션내역으로 저장한다)
        //=========================================================================================================================================================================
        private void btnSaveOptionPrice_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }

            if (optionSaleAmountTextBox.Text.Trim() == "") optionSaleAmountTextBox.Text = "0";
            if (optionSaleAmountTextBox.Text.Equals("0"))
            {
                MessageBox.Show("옵션가격은 필수 입력항목입니다.");
                optionSaleAmountTextBox.Focus();
                return;
            }

            if (totalNumberOfPersonsTextbox.Text.Trim() == "") totalNumberOfPersonsTextbox.Text = "0";
            if (totalNumberOfPersonsTextbox.Text.Trim() == "0")
            {
                MessageBox.Show("인원수는 필수 입력항목입니다.");
                totalNumberOfPersonsTextbox.Focus();
                return;
            }

            // 옵션 입력필드 유효성 검증
            if (ValidateForOptionInfo() == false) return;

            string RSVT_NO = _reservationNumber;

            string OPTN_CNMB = "";
            string OPTN_NM = optionNameTextBox.Text.Trim();
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(optionCurrencyCodeComboBox);
            string CUR_NM = optionCurrencyCodeComboBox.Text.Trim();
            string PRDT_OPTN_SALE_AMT = Utils.GetDoubleString(optionSaleAmountTextBox.Text);
            string NMPS_NBR = totalNumberOfPersonsTextbox.Text.Trim();
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = "";

            if (_optionNo == "")
            {
                query = string.Format("CALL InsertRsvtOptnItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                       RSVT_NO, OPTN_CNMB, OPTN_NM, CUR_CD, PRDT_OPTN_SALE_AMT, NMPS_NBR, FRST_RGTR_ID);
            }
            else
            {
                query = string.Format("CALL UpdateRsvtOptnItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                       RSVT_NO, _optionNo, OPTN_NM, CUR_CD, PRDT_OPTN_SALE_AMT, NMPS_NBR, FRST_RGTR_ID);
            }

            queryStringArray[0] = query;

            long retVal = DbHelper.ExecuteScalarAndNonQueryWithTransaction(queryStringArray);

            if (retVal == -1)
            {
                MessageBox.Show("옵션정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                OPTN_CNMB = retVal.ToString();
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 등록/갱신한 예약옵션내역을 검색
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            query = string.Format("CALL SelectRsvtOptnItem ( '{0}', {1} )", _reservationNumber, OPTN_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약금액정보를 가져올 수 없습니다.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            // 옵션소계 산출
            int totalPrice = Convert.ToInt32(Math.Round(Double.Parse(PRDT_OPTN_SALE_AMT) * int.Parse(NMPS_NBR)));
            OPTN_CNMB = dataRow["OPTN_CNMB"].ToString();                                      // 옵션일련번호

            // 옵션 소계 반영
            OptionSumAmountTextBox.Text = Utils.SetComma(totalPrice.ToString());

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 옵션이 추가된 경우 그리드에 추가하고, 변경된 경우에는 그리드를 변경
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (_optionNo == "")
            {
                // 옵션그리드에 등록/갱신한 옵션내역을 반영
                optionDataGridView.Rows.Add(OPTN_CNMB, OPTN_NM, CUR_CD, CUR_NM, Utils.SetComma(PRDT_OPTN_SALE_AMT), Utils.SetComma(NMPS_NBR), Utils.SetComma(totalPrice));
            }
            else
            {
                optionDataGridView["OPTN_CNMB", _clickOptionRow].Value = OPTN_CNMB;
                optionDataGridView["OPTN_NM", _clickOptionRow].Value = OPTN_NM;
                optionDataGridView["CUR_CD", _clickOptionRow].Value = CUR_CD;
                optionDataGridView["CUR_NM", _clickOptionRow].Value = CUR_NM;
                optionDataGridView["PRDT_OPTN_SALE_AMT", _clickOptionRow].Value = Utils.SetComma(PRDT_OPTN_SALE_AMT);
                optionDataGridView["NMPS_NBR", _clickOptionRow].Value = NMPS_NBR;
                optionDataGridView["TOT_PRDT_OPTN_SALE_AMT", _clickOptionRow].Value = Utils.SetComma(totalPrice.ToString());   // 옵션소계
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 예약기본에서 반영된 최신 금액정보를 재검색
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            refreshReservationPriceInfo();

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 그리드 Refresh
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            optionDataGridView.Refresh();

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 옵션입력필드 초기화
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            ResetOptionInputField();

            MessageBox.Show("옵션정보를 저장했습니다.");
            return;
        }

        //=========================================================================================================================================================================
        // 옵션 삭제
        //=========================================================================================================================================================================
        private void btnDeleteOptionPrice_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보가 등록되지 않아 옵션정보를 삭제할 수 없습니다.");
                reservationNumberTextBox.Focus();
                return;
            }

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            if (optionDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제하려는 옵션 행을 선택하세요.");
                optionDataGridView.Focus();
                return;
            }

            // 삭제대상 행번호 저장
            int rowToDelete = optionDataGridView.SelectedRows[0].Index;

            string OPTN_CNMB = optionDataGridView.SelectedRows[0].Cells["OPTN_CNMB"].Value.ToString();
            string OPTN_NM = optionDataGridView.SelectedRows[0].Cells["OPTN_NM"].Value.ToString();
            string CUR_CD = optionDataGridView.SelectedRows[0].Cells["CUR_CD"].Value.ToString();
            string CUR_NM = optionDataGridView.SelectedRows[0].Cells["CUR_NM"].Value.ToString();
            string PRDT_OPTN_SALE_AMT = optionDataGridView.SelectedRows[0].Cells["PRDT_OPTN_SALE_AMT"].Value.ToString();          // 옵션가격
            string TOT_PRDT_OPTN_SALE_AMT = optionDataGridView.SelectedRows[0].Cells["TOT_PRDT_OPTN_SALE_AMT"].Value.ToString();  // 옵션소계
            string NMPS_NBR = optionDataGridView.SelectedRows[0].Cells["NMPS_NBR"].Value.ToString();

            if (OPTN_CNMB == "")
            {
                MessageBox.Show("옵션번호가 없습니다. 운영담당자에게 연락하세요.");
                optionDataGridView.Focus();
                return;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 예약옵션내역 테이블 삭제
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            string query = string.Format("CALL DeleteRsvtOptnItem ('{0}', {1})", _reservationNumber, OPTN_CNMB);
            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("선택한 옵션정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                refreshReservationPriceInfo();

                // 옵션그리드에서 해당 행을 제거
                optionDataGridView.Rows.RemoveAt(rowToDelete);

                // 옵션입력필드 초기화
                ResetOptionInputField();
                MessageBox.Show("옵션정보를 삭제했습니다.");
            }
        }

        private void refreshReservationPriceInfo()
        {
            string query = string.Format("CALL SelectRsvtPriceInfo ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약금액정보를 가져올 수 없습니다.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            string TOT_SALE_AMT = dataRow["TOT_SALE_AMT"].ToString();
            string OPTN_SUM_AMT = dataRow["OPTN_SUM_AMT"].ToString();
            string UNAM_BAL = dataRow["UNAM_BAL"].ToString();

            salesPriceTextBox.Text = Utils.SetComma(TOT_SALE_AMT);
            totalOptionAmountTextBox.Text = Utils.SetComma(OPTN_SUM_AMT);
            totalNumberOfPersonsTextbox.Text = NMPS_NBR.ToString();
            outstandingPriceTextBox.Text = Utils.SetComma(UNAM_BAL);
        }

        //=========================================================================================================================================================================
        // 옵션 입력필드 초기화버튼 클릭
        //=========================================================================================================================================================================
        private void resetOptionSalesPriceButton_Click(object sender, EventArgs e)
        {
            ResetOptionInputField();
        }

        //=========================================================================================================================================================================
        // 옵션입력필드 초기화
        //=========================================================================================================================================================================
        private void ResetOptionInputField()
        {
            optionNameTextBox.Text = "";
            optionSaleAmountTextBox.Text = "";
            totalNumberOfPersonsTextbox.Text = "";
            optionCurrencyCodeComboBox.SelectedIndex = -1;
            OptionSumAmountTextBox.Text = "";
            _optionNo = "";
            _clickOptionRow = -1;
        }

        //=========================================================================================================================================================================
        // 옵션입력필드 유효성 검증
        //=========================================================================================================================================================================
        private Boolean ValidateForOptionInfo()
        {
            string OPTN_NM = optionNameTextBox.Text.Trim();
            string OPTN_CUR_CD = Utils.GetSelectedComboBoxItemValue(optionCurrencyCodeComboBox);    // 통화코드
            string PRDT_OPTN_SALE_AMT = optionSaleAmountTextBox.Text.Trim();                        // 옵션금액
            string NMPS_NBR = totalNumberOfPersonsTextbox.Text.Trim();                              // 인원수

            if (OPTN_NM == "")
            {
                MessageBox.Show("옵션명은 필수 입력 항목입니다.");
                optionNameTextBox.Focus();
                return false;
            }

            if (OPTN_CUR_CD == "")
            {
                MessageBox.Show("통화코드를 선택하세요.");
                optionCurrencyCodeComboBox.Focus();
                return false;
            }

            if (PRDT_OPTN_SALE_AMT == "" || PRDT_OPTN_SALE_AMT == "0")
            {
                MessageBox.Show("옵션금액은 필수 입력항목입니다.");
                optionSaleAmountTextBox.Focus();
                return false;
            }

            if (NMPS_NBR == "" || NMPS_NBR == "0")
            {
                MessageBox.Show("인원수는 필수 입력항목입니다.");
                totalNumberOfPersonsTextbox.Focus();
                return false;
            }

            return true;
        }

        //=========================================================================================================================================================================
        // 진행상태 팝업 실행 제어
        //=========================================================================================================================================================================
        private void CheckCutOffStatus(ComboBox comboBox, string cutOffType, DateTimePicker dateTimePicker)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }

            string message = "";
            if (cutOffType == "LST_CHCK_STTS_CD")
            {
                message = "명단확인을 완료하시겠습니까?";
            }
            else if (cutOffType == "PSPT_CHCK_STTS_CD")
            {
                message = "여권확인을 완료하시겠습니까?";
            }
            else if (cutOffType == "ARGM_CHCK_STTS_CD")
                message = "수배확인을 완료하시겠습니까?";
            else if (cutOffType == "VOCH_CHCK_STTS_CD")
                message = "바우처확인을 완료하시겠습니까?";
            else if (cutOffType == "ISRC_CHCK_STTS_CD")
                message = "보험확인을 완료하시겠습니까?";
            else if (cutOffType == "AVAT_CHCK_STTS_CD")
                message = "항공확인을 완료하시겠습니까?";
            else if (cutOffType == "PRSN_CHCK_STTS_CD")
                message = "개인정보확인을 완료하시겠습니까?";


            DialogResult result = MessageBoxEx.Show(message, "진행상태 확인", "예", "추가확인");
            if (result == DialogResult.Yes)
            {
                if (comboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("진행상태를 선택해 주십시오.");
                    return;
                }

                string cutOffStatusCode = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();
                //string query = string.Format("CALL UpdateRsvtCutOffStatus('{0}', '{1}', '{2}')", _reservationNumber, cutOffType, Global.eCutOffStatus.confirmed);
                string query = string.Format("CALL UpdateRsvtCutOffStatus('{0}', '{1}', '{2}')", _reservationNumber, cutOffType, cutOffStatusCode);
                int retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal > 0)
                {
                    Utils.SelectComboBoxItemByValue(comboBox, cutOffStatusCode);            // 콤보박스 설정
                    dateTimePicker.Text = DateTime.Now.ToString("yyyy-MM-dd");              // 진행상태확인일자를 현재일자로 세팅
                    //MessageBox.Show("진행상태를 확인했습니다.");

                    // 진행상태가 모두 완료되면 예약상태를 확정으로 갱신
                    checkAndUpdateReservationStatus();

                    if (cutOffType.Equals("LST_CHCK_STTS_CD")) vOReservationDetail.cutOffListStatusName = "완료";
                    else if (cutOffType.Equals("PSPT_CHCK_STTS_CD")) vOReservationDetail.cutOffPassportStatusName = "완료";
                    else if (cutOffType.Equals("ARGM_CHCK_STTS_CD")) vOReservationDetail.cutOffArrangementStatusName = "완료";
                    else if (cutOffType.Equals("VOCH_CHCK_STTS_CD")) vOReservationDetail.cutOffVoucherStatusName = "완료";
                    else if (cutOffType.Equals("ISRC_CHCK_STTS_CD")) vOReservationDetail.cutOffInsuranceStatusName = "완료";
                    else if (cutOffType.Equals("AVAT_CHCK_STTS_CD")) vOReservationDetail.cutOffAirlineStatusName = "완료";
                    else if (cutOffType.Equals("PRSN_CHCK_STTS_CD")) vOReservationDetail.cutOffPersonalInfoStatusName = "완료";

                    /* 변경시에는 예약목록의 행번호를 알지만 신규등록시에는 예외 처리 필요. 일단 주석처리
                    string LST_CHCK_STTS_CD = "";
                    string PSPT_CHCK_STTS_CD = "";
                    string ARGM_CHCK_STTS_CD = "";
                    string VOCH_CHCK_STTS_CD = "";
                    string ISRC_CHCK_STTS_CD = "";
                    string AVAT_CHCK_STTS_CD = "";
                    string PRSN_CHCK_STTS_CD = "";

                    if (vOReservationDetail.cutOffListStatusName.Equals("완료"))
                        LST_CHCK_STTS_CD = "●";
                    else
                        LST_CHCK_STTS_CD = "";

                    if (vOReservationDetail.cutOffPassportStatusName.Equals("완료"))
                        PSPT_CHCK_STTS_CD = "●";
                    else
                        PSPT_CHCK_STTS_CD = "";

                    if (vOReservationDetail.cutOffArrangementStatusName.Equals("완료"))
                        ARGM_CHCK_STTS_CD = "●";
                    else
                        ARGM_CHCK_STTS_CD = "";

                    if (vOReservationDetail.cutOffVoucherStatusName.Equals("완료"))
                        VOCH_CHCK_STTS_CD = "●";
                    else
                        VOCH_CHCK_STTS_CD = "";

                    if (vOReservationDetail.cutOffInsuranceStatusName.Equals("완료"))
                        ISRC_CHCK_STTS_CD = "●";
                    else
                        ISRC_CHCK_STTS_CD = "";

                    if (vOReservationDetail.cutOffAirlineStatusName.Equals("완료"))
                        AVAT_CHCK_STTS_CD = "●";
                    else
                        AVAT_CHCK_STTS_CD = "";

                    if (vOReservationDetail.cutOffPersonalInfoStatusName.Equals("완료"))
                        PRSN_CHCK_STTS_CD = "●";
                    else
                        PRSN_CHCK_STTS_CD = "";

                    _reservationDataGridView.Rows[_selectedReservarionListRow].Cells[(int)ReservationList.eReservationDataGridView.PSPT_CHCK_STTS_CD].Value = LST_CHCK_STTS_CD;
                    _reservationDataGridView.Rows[_selectedReservarionListRow].Cells[(int)ReservationList.eReservationDataGridView.PSPT_CHCK_STTS_CD].Value = PSPT_CHCK_STTS_CD;
                    _reservationDataGridView.Rows[_selectedReservarionListRow].Cells[(int)ReservationList.eReservationDataGridView.ARGM_CHCK_STTS_CD].Value = ARGM_CHCK_STTS_CD;
                    _reservationDataGridView.Rows[_selectedReservarionListRow].Cells[(int)ReservationList.eReservationDataGridView.VOCH_CHCK_STTS_CD].Value = VOCH_CHCK_STTS_CD;
                    _reservationDataGridView.Rows[_selectedReservarionListRow].Cells[(int)ReservationList.eReservationDataGridView.ISRC_CHCK_STTS_CD].Value = ISRC_CHCK_STTS_CD;
                    _reservationDataGridView.Rows[_selectedReservarionListRow].Cells[(int)ReservationList.eReservationDataGridView.AVAT_CHCK_STTS_CD].Value = AVAT_CHCK_STTS_CD;
                    _reservationDataGridView.Rows[_selectedReservarionListRow].Cells[(int)ReservationList.eReservationDataGridView.PRSN_CHCK_STTS_CD].Value = PRSN_CHCK_STTS_CD;
                    */

                    return;
                }
            }
            else
            {
                string cutOffStatusCode = "";
                string cutOffStatusName = "";
                if (comboBox.SelectedIndex != -1)
                {
                    cutOffStatusCode = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();
                    cutOffStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;
                }

                string emailAddress = emailIdTextBox.Text + "@";
                if (domainComboBox.SelectedIndex != -1)
                    emailAddress += domainComboBox.SelectedItem.ToString();

                PopUpCheckCutOff form = new PopUpCheckCutOff();
                form.SetReservationNumber(_reservationNumber);
                form.SetBookerName(bookerNameTextBox.Text.Trim());
                form.SetCellPhoneNumber(cellphoneNumberTextBox.Text.Trim());
                form.SetEmailAddress(emailAddress);
                form.SetCutOffType(cutOffType);
                form.SetCutOffStatusCode(cutOffStatusCode);
                form.SetCutOffStatusName(cutOffStatusName);
                form.SetCustomerNumber(_customerNumber);
                form.SetCustomerName(bookerNameTextBox.Text.Trim());
                form.SetEmployeeNumber(_employeeNumber);
                form.SetEmployeeName(_employeeName);
                form.SetCutOffMgtMemoDataGridView(cutOffMgtMemoDataGridView);
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();
            }
        }

        //=========================================================================================================================================================================
        // 진행상태가 모두 완료되면 예약상태를 확정으로 갱신
        //=========================================================================================================================================================================
        private void checkAndUpdateReservationStatus()
        {

        }

        //=========================================================================================================================================================================
        // 폼 닫기
        //=========================================================================================================================================================================
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (_needSaveData == true)
            {
                DialogResult result = MessageBoxEx.Show("저장하지 않은 정보가 있습니다. 화면을 닫으시겠습니까?", "화면 닫기", "예", "아니오");
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        //=========================================================================================================================================================================
        // 예약기본정보 입력 유효성 검증
        //=========================================================================================================================================================================
        private Boolean ValidateForReservationInfo(string procType)
        {
            if (_saveMode == Global.SAVEMODE_UPDATE && _reservationNumber == "")
            {
                MessageBox.Show("예약번호를 확인해 주십시오.");
                reservationNumberTextBox.Focus();
                return false;
            }
            if (productComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품을 선택해 주십시오.");
                productComboBox.Focus();
                return false;
            }
            if (productGradeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품등급을 선택해 주십시오.");
                productGradeComboBox.Focus();
                return false;
            }
            if (reservationStatusComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("예약상태를 선택해 주십시오.");
                reservationStatusComboBox.Focus();
                return false;
            }
            if (cooperativeCompanyComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("모객업체를 선택해 주십시오.");
                cooperativeCompanyComboBox.Focus();
                return false;
            }

            // 예약복사인 경우 미수잔액 체크 하지 않음
            if (procType.Equals("COPY"))
            {
                outstandingPriceTextBox.Text = "0";
            } else
            {
                // 예약상태가 '확정'인 경우 미수 잔액이 남아 있으면 확정 처리 못하도록 체크
                string RSVT_STTS_CD = Utils.GetSelectedComboBoxItemValue(reservationStatusComboBox);     // 예약상태코드;
                double remainedAccountReceivable = Double.Parse(outstandingPriceTextBox.Text.Trim());

                if (RSVT_STTS_CD.Equals("2") && remainedAccountReceivable > 0)
                {
                    MessageBox.Show("입금이 완료되지 않았습니다. 입금금액과 예약상태를 확인하세요.");
                    return false;
                }
            }


            return true;
        }

        //=========================================================================================================================================================================
        // 저장버튼 클릭
        //=========================================================================================================================================================================
        private void saveReservationDetailButton_Click(object sender, EventArgs e)
        {
            // 예약기본정보 저장
            saveReservationDetail();

            _needSaveData = false;
        }

        //=========================================================================================================================================================================
        // 예약기본정보 저장
        //=========================================================================================================================================================================
        private void saveReservationDetail()
        {
            // 상품변경되었는지 정보 확인


            // 예약정보 입력필드 유효성 검증
            if (ValidateForReservationInfo("UPD") == false) return;

            // 입력 파라미터 설정
            paramSetReservationDetail("UPD");

            if (_saveMode == Global.SAVEMODE_UPDATE)
            {
                vOReservationDetail.RSVT_NO = _reservationNumber;

                string query = "CALL UpdateRsvtItem(";
                query += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',";
                query += "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',";
                query += "'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',";
                query += "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',";
                query += "'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}')";
                query = string.Format(query,
                    vOReservationDetail.RSVT_NO,
                    vOReservationDetail.RSVT_DT,
                    vOReservationDetail.ORDR_NO,
                    vOReservationDetail.CNFM_NO,
                    vOReservationDetail.PRDT_CNMB,
                    vOReservationDetail.PRDT_GRAD_CD,
                    vOReservationDetail.CUST_NO,
                    vOReservationDetail.TKTR_CMPN_NO,
                    vOReservationDetail.RPRS_ENG_NM,
                    vOReservationDetail.ORDE_CTIF_PHNE_NO,
                    vOReservationDetail.ORDE_EMAL_ADDR,
                    vOReservationDetail.RPRS_EMAL_ADDR,
                    vOReservationDetail.AGE,
                    vOReservationDetail.ADLT_SALE_PRCE,
                    vOReservationDetail.CHLD_SALE_PRCE,
                    vOReservationDetail.INFN_SALE_PRCE,
                    vOReservationDetail.TOT_SALE_AMT,
                    vOReservationDetail.ADLT_NBR,
                    vOReservationDetail.CHLD_NBR,
                    vOReservationDetail.INFN_NBR,
                    vOReservationDetail.FREE_TCKT_NBR,
                    vOReservationDetail.DPTR_DT,
                    vOReservationDetail.LGMT_DAYS,
                    vOReservationDetail.TOT_TRIP_DAYS,
                    vOReservationDetail.SALE_SUM_AMT,
                    vOReservationDetail.OPTN_SUM_AMT,
                    vOReservationDetail.CSPR_SUM_AMT,
                    vOReservationDetail.SALE_CUR_CD,
                    vOReservationDetail.RSVT_AMT,
                    vOReservationDetail.TOT_RECT_AMT,
                    vOReservationDetail.MDPY_AMT,
                    vOReservationDetail.UNAM_BAL,
                    vOReservationDetail.DSCT_AMT,
                    vOReservationDetail.WON_PYMT_FEE = double.Parse(consignmentSaleFeeTextBox.Text.Trim()),
                    vOReservationDetail.STEM_WON_AMT,
                    vOReservationDetail.STMT_YN,
                    vOReservationDetail.STMT_WON_AMT,
                    vOReservationDetail.PRCS_DTM,
                    vOReservationDetail.SHTL_ICUD_EN,
                    vOReservationDetail.RPSB_EMPL_NO,
                    vOReservationDetail.CUSL_EMPL_NO,
                    vOReservationDetail.RSVT_STTS_CD,
                    vOReservationDetail.TCKT_STTS_CD,
                    vOReservationDetail.QUOT_CNMB,
                    vOReservationDetail.DEL_YN,
                    vOReservationDetail.CUST_RQST_CNTS,
                    vOReservationDetail.INTR_MEMO_CNTS,
                    vOReservationDetail.REMK_CNTS,
                    Global.loginInfo.ACNT_ID
                );

                int retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal > 0)
                {
                    if (_reservationDataGridView != null) // null 인 경우는 예약상세를 바로 호출했을 때
                    {
                        int _numberOfPeople = _numberOfAdult + _numberOfChild + _numberOfInfant;
                        /*
                        int selectedRow = _reservationDataGridView.SelectedRows[0].Index;
                        _selectedReservarionListRow = _reservationDataGridView.SelectedRows[0].Index;

                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.RSVT_DT].Value = vOReservationDetail.RSVT_DT;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.RSVT_NO].Value = _reservationNumber;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.DPTR_DT].Value = vOReservationDetail.DPTR_DT;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.CUST_NM].Value = bookerNameTextBox.Text.Trim();
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.CMPN_NM].Value = (cooperativeCompanyComboBox.SelectedItem as ComboBoxItem).Text;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.PRDT_NM].Value = (productComboBox.SelectedItem as ComboBoxItem).Text;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.number].Value = _numberOfPeople.ToString();
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.reservationStatus].Value = (reservationStatusComboBox.SelectedItem as ComboBoxItem).Text;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.SALE_CUR_CD].Value = (saleCurrencyCodeComboBox.SelectedItem as ComboBoxItem).Value;

                        if (vOReservationDetail.SALE_CUR_CD.Equals("KRW"))
                        {
                            _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.TOT_SALE_AMT].Value = Utils.SetComma(Convert.ToInt32(vOReservationDetail.TOT_SALE_AMT));
                            _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.TOT_RECT_AMT].Value = Utils.SetComma(Convert.ToInt32(vOReservationDetail.TOT_RECT_AMT));
                        }
                        else
                        {
                            _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.TOT_SALE_AMT].Value = Utils.SetComma(vOReservationDetail.TOT_SALE_AMT);
                            _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.TOT_RECT_AMT].Value = Utils.SetComma(vOReservationDetail.TOT_RECT_AMT);
                        }

                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.RSVT_STTS_CD].Value = vOReservationDetail.cutOffPersonalInfoStatusName;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.CUST_NO].Value = vOReservationDetail.CUST_NO;
                        _reservationDataGridView.Rows[selectedRow].Cells[(int)ReservationList.eReservationDataGridView.EMPL_NM].Value = _employeeName;

                        */
                    }

                    MessageBox.Show("예약정보를 수정했습니다.");
                }
                else
                {
                    MessageBox.Show("예약정보를 수정 할 수 없습니다.");
                }

            }
        }


        //=========================================================================================================================================================================
        // 기존 예약정보를 바탕으로 새로운 예약정보를 생성
        //=========================================================================================================================================================================
        private void copyReservationDetailButton_Click(object sender, EventArgs e)
        {
            // 복사하려는 예약자가 복사대상 예약자와 같으면 경고 메시지 처리
            if (_customerChanged == false)
            {
                DialogResult result = MessageBoxEx.Show("복사대상 고객이 동일 고객입니다. 계속하시겠습니까?", "예약복사", "예", "아니오");
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // 예약정보 입력필드 유효성 검증
            if (ValidateForReservationInfo("COPY") == false) return;

            //// 입력 파라미터 설정
            paramSetReservationDetail("COPY");

            // 복사할 때에는 예약상태를 미확정으로 초기화
            vOReservationDetail.RSVT_STTS_CD = "1";
            Utils.SelectComboBoxItemByValue(reservationStatusComboBox, "1");

            InsertReservationInfoAllCopy();

            _customerChanged = false;
            _needSaveData = false;
            _selectedReservarionListRow = -1;             // 예약목록에서 선택한 행번호를 지움
        }

        //=========================================================================================================================================================================
        // 예약기본 입력 파라미터 설정
        //=========================================================================================================================================================================
        private void paramSetReservationDetail(string cudType)
        {
            vOReservationDetail.RSVT_NO = _reservationNumber;
            vOReservationDetail.RSVT_DT = reservationDateDateTimePicker.Value.ToString("yyyy-MM-dd");                        // 예약일자
            vOReservationDetail.ORDR_NO = "";                                                                                // 주문번호
            vOReservationDetail.CNFM_NO = "";                                                                                // CFM번호
            if (productComboBox.SelectedItem != null)
            {
                vOReservationDetail.PRDT_CNMB = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 상품일련번호
            }
            if (productGradeComboBox.Items.Count > 0)
            {
                vOReservationDetail.PRDT_GRAD_CD = (productGradeComboBox.SelectedItem as ComboBoxItem).Value.ToString();     // 상품등급코드
            }
            else
            {
                vOReservationDetail.PRDT_GRAD_CD = "";
            }

            vOReservationDetail.CUST_NO = _customerNumber;                                                                   // 고객번호
            vOReservationDetail.TKTR_CMPN_NO = (cooperativeCompanyComboBox.SelectedItem as ComboBoxItem).Value.ToString();   // 모객업체번호

            string RPRS_ENG_NM = customerEngNameTextBox.Text.Trim();                                                         // 대표자영문명
            vOReservationDetail.RPRS_ENG_NM = EncryptMgt.Encrypt(RPRS_ENG_NM, EncryptMgt.aesEncryptKey);                     // 대표자영문명(암호화)
            vOReservationDetail.RPRS_ENG_NM = EncryptMgt.Decrypt(vOReservationDetail.RPRS_ENG_NM, EncryptMgt.aesEncryptKey); // 대표자영문명(복호화)

            vOReservationDetail.ORDE_CTIF_PHNE_NO = cellphoneNumberTextBox.Text.Trim();                                      // 주문자연락처전화번호
            vOReservationDetail.ORDE_EMAL_ADDR = emailIdTextBox.Text.Trim() + "@" + domainComboBox.Text.Trim();              // 주문자이메일주소
            vOReservationDetail.RPRS_EMAL_ADDR = vOReservationDetail.ORDE_EMAL_ADDR;                                         // 대표자이메일주소
            vOReservationDetail.AGE = 0;                                                                                     // 연령

            string ADLT_SALE_PRCE = adultSalesPriceTextBox.Text;                                                             // 성인판매가격
            if (ADLT_SALE_PRCE == "") ADLT_SALE_PRCE = "0";
            vOReservationDetail.ADLT_SALE_PRCE = Double.Parse(ADLT_SALE_PRCE);

            string CHLD_SALE_PRCE = childSalesPriceTextBox.Text;                                                             // 소아판매가격
            if (CHLD_SALE_PRCE == "") CHLD_SALE_PRCE = "0";
            vOReservationDetail.CHLD_SALE_PRCE = Double.Parse(CHLD_SALE_PRCE);

            string INFN_SALE_PRCE = infantSalesPriceTextBox.Text;                                                            // 유아판매가격
            if (INFN_SALE_PRCE == "") INFN_SALE_PRCE = "0";
            vOReservationDetail.INFN_SALE_PRCE = Double.Parse(INFN_SALE_PRCE);

            string SALE_SUM_AMT = salesPriceSumTextBox.Text;                                                                 // 판매합계금액
            if (SALE_SUM_AMT == "") SALE_SUM_AMT = "0";
            vOReservationDetail.SALE_SUM_AMT = Utils.GetDoubleValue(SALE_SUM_AMT);

            string OPTN_SUM_AMT = totalOptionAmountTextBox.Text;                                                             // 옵션합계금액
            if (OPTN_SUM_AMT == "") OPTN_SUM_AMT = "0";
            vOReservationDetail.OPTN_SUM_AMT = Utils.GetDoubleValue(OPTN_SUM_AMT);

            string CSPR_SUM_AMT = totalCostPriceAmountTextBox.Text;                                                          // 원가합계금액
            if (CSPR_SUM_AMT == "") CSPR_SUM_AMT = "0";
            vOReservationDetail.CSPR_SUM_AMT = Utils.GetDoubleValue(CSPR_SUM_AMT);

            // 총판매금액 계산 = 판매합계금액 + 옵션합계금액
            Double totalSaleAmount = Double.Parse(SALE_SUM_AMT) + Double.Parse(OPTN_SUM_AMT);                                // 총판매금액
            vOReservationDetail.TOT_SALE_AMT = totalSaleAmount;

            string ADLT_NBR = adultPeopleCountTextBox.Text.Trim();                                                           // 성인수
            if (ADLT_NBR == "") ADLT_NBR = "0";
            vOReservationDetail.ADLT_NBR = Int16.Parse(ADLT_NBR);

            string CHLD_NBR = childPeopleCountTextBox.Text.Trim();                                                           // 소아수
            if (CHLD_NBR == "") CHLD_NBR = "0";
            vOReservationDetail.CHLD_NBR = Int16.Parse(CHLD_NBR);

            string INFN_NBR = infantPeopleCountTextBox.Text.Trim();                                                          // 유아수
            if (INFN_NBR == "") INFN_NBR = "0";
            vOReservationDetail.INFN_NBR = Int16.Parse(INFN_NBR);

            vOReservationDetail.FREE_TCKT_NBR = 0;                                                                           // 프리티켓수

            vOReservationDetail.DPTR_DT = departureDateTimePicker.Value.ToString("yyyy-MM-dd");                              // 출발일자

            if (nightComboBox.SelectedIndex > -1)
            {
                vOReservationDetail.LGMT_DAYS = Int16.Parse(nightComboBox.SelectedItem as string);                           // 숙박일수
            }
            else
            {
                vOReservationDetail.LGMT_DAYS = 0;
            }

            if (dayComboBox.SelectedIndex > -1)
            {
                vOReservationDetail.TOT_TRIP_DAYS = Int16.Parse(dayComboBox.SelectedItem as string);                         // 총여행일수
            }
            else
            {
                vOReservationDetail.TOT_TRIP_DAYS = 0;
            }

            vOReservationDetail.SALE_CUR_CD = Utils.GetSelectedComboBoxItemValue(saleCurrencyCodeComboBox);                  // 판매통화코드
            string RSVT_AMT = reservationPriceTextBox.Text;                                                                  // 예약금액
            if (RSVT_AMT == "") RSVT_AMT = "0";

            string PRPY_AMT = depositPriceTextBox.Text;                                                                      // 선금액
            if (RSVT_AMT == "") PRPY_AMT = "0";

            if (totalDepositPriceTextBox.Text.Trim() == "") totalDepositPriceTextBox.Text = "0";
            string TOT_RECT_AMT = totalDepositPriceTextBox.Text;                                                             // 총입금금액

            string MDPY_AMT = partPriceTextBox.Text;                                                                         // 중도금금액
            if (MDPY_AMT == "") MDPY_AMT = "0";

            // 예약복사 시에는 입금금액 정보를 0으로 초기화
            if (cudType.Equals("COPY"))
            {
                vOReservationDetail.RSVT_AMT = 0;
                vOReservationDetail.PRPY_AMT = 0;
                vOReservationDetail.TOT_RECT_AMT = 0;
                vOReservationDetail.MDPY_AMT = 0;
                vOReservationDetail.UNAM_BAL = vOReservationDetail.TOT_SALE_AMT;
                vOReservationDetail.STEM_WON_AMT = 0;
                vOReservationDetail.STMT_WON_AMT = 0;
                vOReservationDetail.WON_PYMT_FEE = 0;
                vOReservationDetail.DSCT_AMT = 0;
            }
            else
            {
                vOReservationDetail.RSVT_AMT = Double.Parse(RSVT_AMT);
                vOReservationDetail.PRPY_AMT = Double.Parse(PRPY_AMT);
                vOReservationDetail.TOT_RECT_AMT = Double.Parse(TOT_RECT_AMT);
                vOReservationDetail.MDPY_AMT = Double.Parse(MDPY_AMT);
            }

            // 복사/신규등록/수정 건에 따라 미수 잔액 수정 (프로시저에서 자동 계산)
            /*
            switch (cudType)
            {
                case "NEW":    // 신규등록건은 미수잔액이 총판매액과 동일
                    vOReservationDetail.UNAM_BAL = vOReservationDetail.TOT_SALE_AMT;
                    break;
                case "UPD":    // 수정건은 미수잔액을 화면에서 받은 값으로 저장
                    UNAM_BAL = Utils.GetDoubleString(outstandingPriceTextBox.Text);
                    if (UNAM_BAL == "") UNAM_BAL = "0";
                    vOReservationDetail.UNAM_BAL = Double.Parse(UNAM_BAL);
                    break;
                case "COPY":   // 복사건은 미수잔액이 총판매액과 동일
                    vOReservationDetail.UNAM_BAL = vOReservationDetail.TOT_SALE_AMT;
                    break;
            }
            */
            vOReservationDetail.DSCT_AMT = 0;                                                                                // 할인금액
            vOReservationDetail.WON_PYMT_FEE = 0;                                                                            // 원화지급수수료
            vOReservationDetail.STEM_WON_AMT = 0;                                                                            // 가정산원화금액
            vOReservationDetail.STMT_YN = "N";                                                                               // 정산여부
            vOReservationDetail.STMT_WON_AMT = 0;                                                                            // 정산원화금액
            vOReservationDetail.PRCS_DTM = purchageDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 구매일시
            vOReservationDetail.SHTL_ICUD_EN = "";                                                                           // 셔틀포함유무

            // 담당 직원번호 Parameter Setting   --> 190820 박현호
            //=====================================================================================================
            vOReservationDetail.RPSB_EMPL_NO = Global.loginInfo.EMPL_NO;                                                     // 담당직원번호

            /*
            string query = "SELECT EMPL_NO FROM TB_EMPL_M WHERE EMPL_NM = '" + Global.loginInfo.EMPL_NO + "'";
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection row = dataSet.Tables[0].Rows;
            if (row.Count > 1)
            {
                MessageBox.Show("담당자 번호가 2개 이상 존재합니다.");
                return;
            }
            for (int i = 0; i < row.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string EMPL_NO = dataRow["EMPL_NO"].ToString().Trim();
                vOReservationDetail.RPSB_EMPL_NO = EMPL_NO;
            }
            */
            //=====================================================================================================

            vOReservationDetail.CUSL_EMPL_NO = "";                                                                           // 상담직원번호

            ///
            // 예약진행상태정보 설정
            // 예약진행상태는 각 진행항목에서 직접 갱신하므로 Skip
            /*
            // 명단
            ComboBox comboBox = this.Controls.Find("cutOffListComboBox", true).FirstOrDefault() as ComboBox;
            DateTimePicker dateTimePicker = this.Controls.Find("cutOffListDateTimePicker", true).FirstOrDefault() as DateTimePicker;
            if (comboBox != null)
            {
                vOReservationDetail.LST_CHCK_STTS_CD = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 명단확인상태코드
                vOReservationDetail.cutOffListStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;                     // 명단확인상태명
                vOReservationDetail.LST_CHCK_DTM = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 명단확인일시
            }
            // 여권
            comboBox = this.Controls.Find("cutOffPassportComboBox", true).FirstOrDefault() as ComboBox;
            dateTimePicker = this.Controls.Find("cutOffPassportDateTimePicker", true).FirstOrDefault() as DateTimePicker;
            if (comboBox != null)
            {
                vOReservationDetail.PSPT_CHCK_STTS_CD = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 여권확인상태코드
                vOReservationDetail.cutOffPassportStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;                  // 여권확인상태명
                vOReservationDetail.PSPT_CHCK_DTM = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 여권확인일시
            }
            // 수배
            comboBox = this.Controls.Find("cutOffArrangementComboBox", true).FirstOrDefault() as ComboBox;
            dateTimePicker = this.Controls.Find("cutOffArrangementDateTimePicker", true).FirstOrDefault() as DateTimePicker;
            if (comboBox != null)
            {
                vOReservationDetail.ARGM_CHCK_STTS_CD = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 수배확인상태코드
                vOReservationDetail.cutOffArrangementStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;               // 수배확인상태명
                vOReservationDetail.PSPT_CHCK_DTM = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 수배확인일시
            }
            // 바우처
            comboBox = this.Controls.Find("cutoffVoucherComboBox", true).FirstOrDefault() as ComboBox;
            dateTimePicker = this.Controls.Find("cutoffVoucherDateTimePicker", true).FirstOrDefault() as DateTimePicker;
            if (comboBox != null)
            {
                vOReservationDetail.VOCH_CHCK_STTS_CD = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 바우처확인상태코드
                vOReservationDetail.cutOffVoucherStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;                   // 바우처확인상태명
                vOReservationDetail.VOCH_CHCK_DTM = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 바우처확인일시
            }
            // 보험
            comboBox = this.Controls.Find("cutOffInsuranceComboBox", true).FirstOrDefault() as ComboBox;
            dateTimePicker = this.Controls.Find("cutOffInsuranceDateTimePicker", true).FirstOrDefault() as DateTimePicker;
            if (comboBox != null)
            {
                vOReservationDetail.ISRC_CHCK_STTS_CD = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 보험확인상태코드
                vOReservationDetail.cutOffInsuranceStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;                 // 보험확인상태명
                vOReservationDetail.ISRC_CHCK_DTM = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 보험확인일시
            }
            // 항공
            comboBox = this.Controls.Find("cutOffAirlineComboBox", true).FirstOrDefault() as ComboBox;
            dateTimePicker = this.Controls.Find("cutOffAirlineDateTimePicker", true).FirstOrDefault() as DateTimePicker;
            if (comboBox != null)
            {
                vOReservationDetail.AVAT_CHCK_STTS_CD = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 항공확인상태코드
                vOReservationDetail.cutOffInsuranceStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;                 // 항공확인상태명
                vOReservationDetail.AVAT_CHCK_DTM = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 항공확인일시
            }
            // 개인
            comboBox = this.Controls.Find("cutOffPersonalInfoComboBox", true).FirstOrDefault() as ComboBox;
            dateTimePicker = this.Controls.Find("cutOffPersonalInfoDateTimePicker", true).FirstOrDefault() as DateTimePicker;
            if (comboBox != null)
            {
                vOReservationDetail.PRSN_CHCK_STTS_CD = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();             // 개인확인상태코드
                vOReservationDetail.cutOffInsuranceStatusName = (comboBox.SelectedItem as ComboBoxItem).Text;                 // 개인확인상태명
                vOReservationDetail.PRSN_CHCK_DTM = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");                     // 개인확인일시
            }
            */

            /*
            if (cutOffListComboBox.SelectedIndex != -1)
            {
                vOReservationDetail.LST_CHCK_STTS_CD = (cutOffListComboBox.SelectedItem as ComboBoxItem).Value.ToString();          // 명단확인상태코드
                vOReservationDetail.cutOffListStatusName = (cutOffListComboBox.SelectedItem as ComboBoxItem).Text;                  // 명단확인상태명
            }
            if (cutOffPassportComboBox.SelectedIndex != -1)
            {
                vOReservationDetail.PSPT_CHCK_STTS_CD = (cutOffPassportComboBox.SelectedItem as ComboBoxItem).Value.ToString();     // 여권확인상태코드
                vOReservationDetail.cutOffPassportStatusName = (cutOffPassportComboBox.SelectedItem as ComboBoxItem).Text;          // 여권확인상태명
            }
            if (cutOffArrangementComboBox.SelectedIndex != -1)
            {
                vOReservationDetail.ARGM_CHCK_STTS_CD = (cutOffArrangementComboBox.SelectedItem as ComboBoxItem).Value.ToString();  // 수배확인상태코드
                vOReservationDetail.cutOffArrangementStatusName = (cutOffArrangementComboBox.SelectedItem as ComboBoxItem).Text;    // 수배확인상태명
            }
            if (cutoffVoucherComboBox.SelectedIndex != -1)
            {
                vOReservationDetail.VOCH_CHCK_STTS_CD = (cutoffVoucherComboBox.SelectedItem as ComboBoxItem).Value.ToString();      // 바우처확인상태코드
                vOReservationDetail.cutOffVoucherStatusName = (cutOffPersonalInfoComboBox.SelectedItem as ComboBoxItem).Text;       // 바우처확인상태명
            }
            if (cutOffAirlineComboBox.SelectedIndex != -1)
            {
                vOReservationDetail.AVAT_CHCK_STTS_CD = (cutOffAirlineComboBox.SelectedItem as ComboBoxItem).Value.ToString();      // 항공확인상태코드
                vOReservationDetail.cutOffAirlineStatusName = (cutOffAirlineComboBox.SelectedItem as ComboBoxItem).Text;            // 항공확인상태명
            }
            if (cutOffInsuranceComboBox.SelectedIndex != -1)
            {
                vOReservationDetail.ISRC_CHCK_STTS_CD = (cutOffInsuranceComboBox.SelectedItem as ComboBoxItem).Value.ToString();    // 보험확인상태코드
                vOReservationDetail.cutOffInsuranceStatusName = (cutOffInsuranceComboBox.SelectedItem as ComboBoxItem).Text;        // 보험확인상태명
            }
            if (cutOffPersonalInfoComboBox.SelectedIndex != -1)
            {
                vOReservationDetail.PRSN_CHCK_STTS_CD = (cutOffPersonalInfoComboBox.SelectedItem as ComboBoxItem).Value.ToString(); // 개인확인상태코드
                vOReservationDetail.cutOffPersonalInfoStatusName = (cutOffPersonalInfoComboBox.SelectedItem as ComboBoxItem).Text;  // 개인확인상태명
            }
            */

            vOReservationDetail.RSVT_STTS_CD = (reservationStatusComboBox.SelectedItem as ComboBoxItem).Value.ToString();    // 예약상태코드
            vOReservationDetail.TCKT_STTS_CD = "";                                                                           // 티켓상태코드
            vOReservationDetail.QUOT_CNMB = "0";                                                                             // 견적일련번호
            vOReservationDetail.DEL_YN = "N";                                                                                // 삭제여부
            vOReservationDetail.CUST_RQST_CNTS = Utils.ReplaceSpecialChar(customerRequestMemoTextBox.Text.Trim());           // 고객요청내용
            vOReservationDetail.INTR_MEMO_CNTS = Utils.ReplaceSpecialChar(insideMemoTextBox.Text.Trim());                    // 내부메모내용
            vOReservationDetail.REMK_CNTS = "";                                                                              // 비고내용
        }

        //=========================================================================================================================================================================
        // 기존예약건을 새로운 예약번호 건에 복사
        //=========================================================================================================================================================================
        private void InsertReservationInfoAllCopy()
        {
            string query = "CALL InsertReservationInfoAllCopy(";
            query += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',";
            query += "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',";
            query += "'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',";
            query += "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',";
            query += "'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}')";
            query = string.Format(query,
                    vOReservationDetail.RSVT_NO,
                    vOReservationDetail.RSVT_DT,
                    vOReservationDetail.ORDR_NO,
                    vOReservationDetail.CNFM_NO,
                    vOReservationDetail.PRDT_CNMB,
                    vOReservationDetail.PRDT_GRAD_CD,
                    vOReservationDetail.CUST_NO,
                    vOReservationDetail.TKTR_CMPN_NO,
                    vOReservationDetail.RPRS_ENG_NM,
                    vOReservationDetail.ORDE_CTIF_PHNE_NO,
                    vOReservationDetail.ORDE_EMAL_ADDR,
                    vOReservationDetail.RPRS_EMAL_ADDR,
                    vOReservationDetail.AGE,
                    vOReservationDetail.ADLT_SALE_PRCE,
                    vOReservationDetail.CHLD_SALE_PRCE,
                    vOReservationDetail.INFN_SALE_PRCE,
                    vOReservationDetail.TOT_SALE_AMT,
                    vOReservationDetail.ADLT_NBR,
                    vOReservationDetail.CHLD_NBR,
                    vOReservationDetail.INFN_NBR,
                    vOReservationDetail.FREE_TCKT_NBR,
                    vOReservationDetail.DPTR_DT,
                    vOReservationDetail.LGMT_DAYS,
                    vOReservationDetail.TOT_TRIP_DAYS,
                    vOReservationDetail.SALE_SUM_AMT,
                    vOReservationDetail.OPTN_SUM_AMT,
                    vOReservationDetail.CSPR_SUM_AMT,
                    vOReservationDetail.SALE_CUR_CD,
                    vOReservationDetail.RSVT_AMT,
                    vOReservationDetail.TOT_RECT_AMT,
                    vOReservationDetail.MDPY_AMT,
                    vOReservationDetail.UNAM_BAL,
                    vOReservationDetail.DSCT_AMT,
                    vOReservationDetail.WON_PYMT_FEE,
                    vOReservationDetail.STEM_WON_AMT,
                    vOReservationDetail.STMT_YN,
                    vOReservationDetail.STMT_WON_AMT,
                    vOReservationDetail.PRCS_DTM,
                    vOReservationDetail.SHTL_ICUD_EN,
                    vOReservationDetail.RPSB_EMPL_NO,
                    vOReservationDetail.CUSL_EMPL_NO,
                    vOReservationDetail.RSVT_STTS_CD,
                    vOReservationDetail.TCKT_STTS_CD,
                    vOReservationDetail.QUOT_CNMB,
                    vOReservationDetail.DEL_YN,
                    vOReservationDetail.CUST_RQST_CNTS,
                    vOReservationDetail.INTR_MEMO_CNTS,
                    vOReservationDetail.REMK_CNTS,
                    Global.loginInfo.ACNT_ID
            );

            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRow dataRow = dataSet.Tables[0].Rows[0];

            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약정보를 복사할 수 없습니다.");
                return;
            }

            // 채번된 예약번호를 저장
            _reservationNumber = dataRow["RSVT_NO"].ToString();
            reservationNumberTextBox.Text = _reservationNumber;

            if (_reservationDataGridView != null)     // null 인 경우는 예약상세를 바로 호출했을 때
            {
                // 내부 보관용 예약정보 그리드 값 설정
                setReservationDataGridView();
            }

            // 등록된 예약기본, 예약옵션, 예약원가내역을 검색하여 화면에 표시
            SetReservationInfoToForm();

            MessageBox.Show("예약정보를 복사했습니다.");
        }

        //=========================================================================================================================================================================
        // 예약기본 테이블 신규 생성
        //=========================================================================================================================================================================
        private void insertReservationInfo(string procType)
        {
            // 예약정보 입력필드 유효성 검증
            if (ValidateForReservationInfo(procType) == false) return;

            // 예약번호 채번
            //RSVT_NO = GetNextReservationNumber();

            string query = "CALL InsertRsvtItem(";
            query += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',";
            query += "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',";
            query += "'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',";
            query += "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',";
            query += "'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}')";
            query = string.Format(query,
                    vOReservationDetail.RSVT_NO,
                    vOReservationDetail.RSVT_DT,
                    vOReservationDetail.ORDR_NO,
                    vOReservationDetail.CNFM_NO,
                    vOReservationDetail.PRDT_CNMB,
                    vOReservationDetail.PRDT_GRAD_CD,
                    vOReservationDetail.CUST_NO,
                    vOReservationDetail.TKTR_CMPN_NO,
                    vOReservationDetail.RPRS_ENG_NM,
                    vOReservationDetail.ORDE_CTIF_PHNE_NO,
                    vOReservationDetail.ORDE_EMAL_ADDR,
                    vOReservationDetail.RPRS_EMAL_ADDR,
                    vOReservationDetail.AGE,
                    vOReservationDetail.ADLT_SALE_PRCE,
                    vOReservationDetail.CHLD_SALE_PRCE,
                    vOReservationDetail.INFN_SALE_PRCE,
                    vOReservationDetail.TOT_SALE_AMT,
                    vOReservationDetail.ADLT_NBR,
                    vOReservationDetail.CHLD_NBR,
                    vOReservationDetail.INFN_NBR,
                    vOReservationDetail.FREE_TCKT_NBR,
                    vOReservationDetail.DPTR_DT,
                    vOReservationDetail.LGMT_DAYS,
                    vOReservationDetail.TOT_TRIP_DAYS,
                    vOReservationDetail.SALE_SUM_AMT,
                    vOReservationDetail.OPTN_SUM_AMT,
                    vOReservationDetail.CSPR_SUM_AMT,
                    vOReservationDetail.SALE_CUR_CD,
                    vOReservationDetail.RSVT_AMT,
                    vOReservationDetail.TOT_RECT_AMT,
                    vOReservationDetail.MDPY_AMT,
                    vOReservationDetail.UNAM_BAL,
                    vOReservationDetail.DSCT_AMT,
                    vOReservationDetail.WON_PYMT_FEE,
                    vOReservationDetail.STEM_WON_AMT,
                    vOReservationDetail.STMT_YN,
                    vOReservationDetail.STMT_WON_AMT,
                    vOReservationDetail.PRCS_DTM,
                    vOReservationDetail.SHTL_ICUD_EN,
                    vOReservationDetail.RPSB_EMPL_NO,
                    vOReservationDetail.CUSL_EMPL_NO,
                    vOReservationDetail.RSVT_STTS_CD,
                    vOReservationDetail.TCKT_STTS_CD,
                    vOReservationDetail.QUOT_CNMB,
                    vOReservationDetail.DEL_YN,
                    vOReservationDetail.CUST_RQST_CNTS,
                    vOReservationDetail.INTR_MEMO_CNTS,
                    vOReservationDetail.REMK_CNTS,
                    Global.loginInfo.ACNT_ID
                );

            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRow dataRow = dataSet.Tables[0].Rows[0];

            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약정보를 복사할 수 없습니다.");
                return;
            }

            // 채번된 예약번호를 저장
            _reservationNumber = dataRow["RSVT_NO"].ToString();
            reservationNumberTextBox.Text = _reservationNumber;
            if (_reservationDataGridView != null)     // null 인 경우는 예약상세를 바로 호출했을 때
            {
                // 내부 보관용 예약정보 그리드 값 설정
                setReservationDataGridView();
                // 인원수 계산
            }

            MessageBox.Show("예약정보를 추가했습니다.");
        }

        //=========================================================================================================================================================================
        // 예약목록의 그리드 값 설정
        //=========================================================================================================================================================================
        private void setReservationDataGridView()
        {
            int number = _numberOfAdult + _numberOfChild + _numberOfInfant;

            string LST_CHCK_STTS_CD = "";
            string PSPT_CHCK_STTS_CD = "";
            string ARGM_CHCK_STTS_CD = "";
            string VOCH_CHCK_STTS_CD = "";
            string ISRC_CHCK_STTS_CD = "";
            string AVAT_CHCK_STTS_CD = "";
            string PRSN_CHCK_STTS_CD = "";

            if (vOReservationDetail.cutOffListStatusName.Equals("완료"))
                LST_CHCK_STTS_CD = "●";
            else
                LST_CHCK_STTS_CD = "";

            if (vOReservationDetail.cutOffPassportStatusName.Equals("완료"))
                PSPT_CHCK_STTS_CD = "●";
            else
                PSPT_CHCK_STTS_CD = "";

            if (vOReservationDetail.cutOffArrangementStatusName.Equals("완료"))
                ARGM_CHCK_STTS_CD = "●";
            else
                ARGM_CHCK_STTS_CD = "";

            if (vOReservationDetail.cutOffVoucherStatusName.Equals("완료"))
                VOCH_CHCK_STTS_CD = "●";
            else
                VOCH_CHCK_STTS_CD = "";

            if (vOReservationDetail.cutOffInsuranceStatusName.Equals("완료"))
                ISRC_CHCK_STTS_CD = "●";
            else
                ISRC_CHCK_STTS_CD = "";

            if (vOReservationDetail.cutOffAirlineStatusName.Equals("완료"))
                AVAT_CHCK_STTS_CD = "●";
            else
                AVAT_CHCK_STTS_CD = "";

            if (vOReservationDetail.cutOffPersonalInfoStatusName.Equals("완료"))
                PRSN_CHCK_STTS_CD = "●";
            else
                PRSN_CHCK_STTS_CD = "";

            _reservationDataGridView.Rows.Add
            (
                vOReservationDetail.RSVT_DT,                                        // 예약일자
                _reservationNumber,                                                 // 예약번호
                vOReservationDetail.DPTR_DT,                                        // 출발일자
                bookerNameTextBox.Text.Trim(),                                      // 고객명
                (cooperativeCompanyComboBox.SelectedItem as ComboBoxItem).Text,     // 모객업체명
                (productComboBox.SelectedItem as ComboBoxItem).Text,                // 상품명
                _numberOfPeople.ToString(),                                         // 인원수
                (reservationStatusComboBox.SelectedItem as ComboBoxItem).Text,      // 예약상태
                (saleCurrencyCodeComboBox.SelectedItem as ComboBoxItem).Value,      // 판매통화코드
                Utils.SetComma(vOReservationDetail.TOT_SALE_AMT.ToString()),        // 총판매액
                Utils.SetComma(vOReservationDetail.TOT_RECT_AMT.ToString()),        // 입금액
                LST_CHCK_STTS_CD,                                                   // 명단
                PSPT_CHCK_STTS_CD,                                                  // 여권
                ARGM_CHCK_STTS_CD,                                                  // 수배
                VOCH_CHCK_STTS_CD,                                                  // 바우처
                AVAT_CHCK_STTS_CD,                                                  // 항공
                ISRC_CHCK_STTS_CD,                                                  // 보험
                PRSN_CHCK_STTS_CD,                                                  // 개인
                vOReservationDetail.CUST_NO,                                        // 고객번호
                _employeeName                                                       // 담당자
            );
        }


        //=========================================================================================================================================================================
        // 고객검색버튼 클릭
        //=========================================================================================================================================================================
        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            PopUpSearchCustomerInfo form = new PopUpSearchCustomerInfo();

            form.SetCustomerName(bookerNameTextBox.Text.Trim());           // 고객명을 팝업창에 전달
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            // 고객이 변경되면 변경되었다는 플래그를 설정
            if (form.GetCustomerNumber() == _customerNumber)
            {
                _customerChanged = false;
            }
            else
            {
                _customerChanged = true;
            }

            _customerNumber = form.GetCustomerNumber();
            bookerNameTextBox.Text = form.GetCustomerName();

            cellphoneNumberTextBox.Text = form.GetCustomerCellPhoneNo();   // 팝업창에서 선택한 고객 휴대폰번호를 화면에 표시
            customerEngNameTextBox.Text = form.GetCustomerEngName();       // 고객영문명 리턴

            // 선택된 이메일주소를 ID와 도메인으로 분리하여 화면에 표시
            emailIdTextBox.Text = form.GetEmailId();
            _emailDomain = form.GetEmailDomainName();

            // 예약상세 검색시 고객 이메일 도메인 정보 출력 --> 190820 박현호
            //--------------------------------------------------------------------------
            setEmailDomain(_emailDomain);
            //--------------------------------------------------------------------------

            //Utils.SelectComboBoxItemByValue(domainComboBox, _emailDomain); // 이메일도메인주소

            // 고객이 바뀌면 예약일자, 구매일자를 변경
            reservationDateDateTimePicker.Value = DateTime.Now;  // 예약일자
            purchageDateTimePicker.Value = DateTime.Now;         // 구매일자
            _numberOfAdult = 0;
            _numberOfChild = 0;
            _numberOfInfant = 0;
        }

        //=========================================================================================================================================================================
        // 입금관리버튼 클릭
        //=========================================================================================================================================================================
        private void depositMgtButton_Click(object sender, EventArgs e)
        {
            PopUpDepositMgt form = new PopUpDepositMgt();
            //form.parent = this;
            form.SetSaveMode(Global.SAVEMODE_UPDATE);
            form.SetReservationNumber(_reservationNumber);
            form.SetBookerName(bookerNameTextBox.Text.Trim());
            form.SetCustomerNumber(_customerNumber);
            if (productComboBox.SelectedIndex != -1) form.SetProductName((productComboBox.SelectedItem as ComboBoxItem).Text);
            form.SetDepatureDate(departureDateTimePicker.Value.ToString("yyyy-MM-dd"));
            form.SetSalesPriceTextBox(salesPriceTextBox.Text.Trim());
            form.SetReservationPriceTextBox(reservationPriceTextBox.Text.Trim());
            form.SetPartPriceTextBox(partPriceTextBox.Text.Trim());
            form.SetDepositPriceTextBox(depositPriceTextBox.Text.Trim());
            form.SetOutstandingPriceTextBox(outstandingPriceTextBox.Text.Trim());
            form.SetWonPayableFeeTextBox(vOReservationDetail.WON_PYMT_FEE.ToString());

            form.SetProductNo(_productNo);
            form.SetTicketterCompanyNo(vOReservationDetail.TKTR_CMPN_NO);

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            /* 입금현황 TextBox Setting (미사용)
            // 입금총액
            if (form.GetTotalDepositPrice() != 0)
                totalDepositPriceTextBox.Text = Utils.SetComma(form.GetTotalDepositPrice().ToString());
            // 선금액
            if (form.GetReservationPrice() != 0)
                depositPriceTextBox.Text = Utils.SetComma(form.GetReservationPrice().ToString());
            // 예약금액
            if (form.GetReservationPrice() != 0)
                reservationPriceTextBox.Text = Utils.SetComma(form.GetReservationPrice().ToString());
            // 중도금
            if (form.GetPartPrice() != 0)
                partPriceTextBox.Text = Utils.SetComma(form.GetPartPrice().ToString());
            // 미수잔액
            if (form.GetBalanceAmount() != 0)
                outstandingPriceTextBox.Text = Utils.SetComma(form.GetBalanceAmount().ToString());
            */

            // 위탁판매지급수수료 (계산금액이 없으면 미입금수수료로 재계산)
            if (form.GetConsignmentSaleFee() != 0)
            {
                vOReservationDetail.WON_PYMT_FEE = form.GetConsignmentSaleFee();
                redrawingConsignmentSaleFeeInputform(form.GetConsignmentSaleFee().ToString(), "수수료(입금완료)", 30, 32);
            }
            else
            {
                double wonPayableFee = calcPayableFee();
                redrawingConsignmentSaleFeeInputform(wonPayableFee.ToString(), "수수료(미입금)", 38, 32);
            }
        }

        private void redrawingConsignmentSaleFeeInputform(string consignmentSaleFee, string labelText, int xx, int yy)
        {
            consignmentSaleFeeTextBox.Text = Utils.SetComma(consignmentSaleFee);
            consignmentSaleFeeLabel.Text = labelText;
            consignmentSaleFeeLabel.Location = new Point(xx, yy);
        }

        //=========================================================================================================================================================================
        // 미지급금등록 팝업 호출
        //=========================================================================================================================================================================
        private void btnUnpaidMgmt_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }

            if (_productNo == "" || _productGradeCode == "")
            {
                MessageBox.Show("상품명과 상품등급을 확인하세요.");
                return;
            }

            if (costPriceDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("등록된 원가내역이 없습니다. 원가정보를 등록하세요.");
                return;
            }

            PopUpAccountPayableMgt form = new PopUpAccountPayableMgt();
            form.SetSaveMode(Global.SAVEMODE_UPDATE);
            form.SetReservationNumber(_reservationNumber);
            form.SetBookerName(bookerNameTextBox.Text.Trim());
            form.SetCustomerNumber(_customerNumber);
            form.SetProductNo(_productNo);
            form.SetProductGradeCode(_productGradeCode);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            // 미지급 팝업에서 계산된 미지급원화금액 산출 결과를 화면에 반영
            if (form.GetSettlementEstimationWonAmt() != 0)
                settlementEstimationWonAmtTextBox.Text = Utils.SetComma(form.GetSettlementEstimationWonAmt().ToString());
        }

        //=========================================================================================================================================================================
        // 예약번호 채번: YYMMDD + 일련번호 3자리 (001~999)
        //=========================================================================================================================================================================
        private string GetNextReservationNumber()
        {
            int _nextReservationNumber = 0;
            string RSVT_NO = "";
            string _ymd = DateTime.Now.ToString("yyMMdd");

            string query = string.Format("CALL SelectMaxReservationNoByYMD ( '{0}' )", _ymd);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("최종 예약번호를 가져올 수 없습니다.");
                return null;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];
            string RSVT_SEQ_NO = dataRow["RSVT_SEQ_NO"].ToString(); // 예약번호 시퀀스 3자리

            // 채번값이 없으면 1로 설정하고 있으면 +1 증가
            if (RSVT_NO == "0")
            {
                RSVT_NO = _ymd + "01";
            }
            else
            {
                _nextReservationNumber = int.Parse(RSVT_SEQ_NO);

                _nextReservationNumber = _nextReservationNumber + 1;
                RSVT_NO = Convert.ToString(_nextReservationNumber);
                RSVT_NO = _ymd + RSVT_NO.PadLeft(2, '0');
            }
            return RSVT_NO;
        }


        //=========================================================================================================================================================================
        // 원가 입력필드 초기화버튼 클릭
        //=========================================================================================================================================================================
        private void resetCostPriceButton_Click(object sender, EventArgs e)
        {
            ResetCostPriceInputField();
        }

        //=========================================================================================================================================================================
        // 원가내역 삭제버튼 클릭
        //=========================================================================================================================================================================
        private void deleteCostPriceInfoButton_Click(object sender, EventArgs e)
        {
            string RSVT_NO = reservationNumberTextBox.Text.Trim();            // 예약번호
            if (RSVT_NO == "")
            {
                MessageBox.Show("예약정보가 등록되지 않아 원가내역을 삭제할 수 없습니다.");
                reservationNumberTextBox.Focus();
                return;
            }

            if (costPriceDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 원가항목을 선택해 주십시오.");
                return;
            }

            // 삭제대상 행번호 저장
            int rowToDelete = costPriceDataGridView.SelectedRows[0].Index;

            string CSPR_CNMB = costPriceDataGridView.Rows[rowToDelete].Cells[(int)eCostPriceDataGridView.CSPR_CNMB].Value.ToString();
            string query = string.Format("CALL DeleteRsvtCsprItemByPK('{0}','{1}')", RSVT_NO, CSPR_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("선택한 원가항목을 삭제 할 수 없습니다.");
            }
            else
            {
                SetReservationCostPriceList();        // 예약원가조회
                /*
                DataRow dataRow = dataSet.Tables[0].Rows[0];

                string CSPR_SUM_AMT = dataRow["CSPR_SUM_AMT"].ToString();
                totalCostPriceAmountTextBox.Text = Utils.SetComma(CSPR_SUM_AMT);

                // 원가그리드에서 해당 행을 제거
                costPriceDataGridView.Rows.RemoveAt(rowToDelete);
                */
                // 원가입력필드 초기화
                ResetCostPriceInputField();
                MessageBox.Show("선택한 원가항목을 삭제했습니다.");
            }
        }


        //=========================================================================================================================================================================
        // 예약자명을 입력하고 엔터키를 누르면 예약목록 검색 창을 띄워 고객을 선택하도록 함
        //=========================================================================================================================================================================
        private void bookerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PopUpSearchReservationInfo form = new PopUpSearchReservationInfo();

                form.SetCustomerName(bookerNameTextBox.Text.Trim());

                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();

                // 선택된 예약번호를 화면에 표시
                _reservationNumber = form.GetReservationNumber();
                reservationNumberTextBox.Text = _reservationNumber;

                // 예약정보를 화면에 표시
                SetReservationInfoToForm();
            }

        }


        //=========================================================================================================================================================================
        // 예약번호를 입력하고 엔터키를 누르면 예약정보를 검색하여 화면에 표시
        //=========================================================================================================================================================================
        private void reservationNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (reservationNumberTextBox.Text == "" || (reservationNumberTextBox.Text.Length < 8))
                {
                    MessageBox.Show("예약번호를 정확하게 입력하세요.");
                    return;
                }

                // 예약번호를 보관
                _reservationNumber = reservationNumberTextBox.Text.Trim();

                // 예약정보를 화면에 표시
                SetReservationInfoToForm();

                // 수정건이므로 신규등록 버튼을 비활성화, 수정버튼을 활성화
                insertNewReservationDetailButton.Enabled = false;
                saveReservationDetailButton.Enabled = true;
            }
        }

        //=========================================================================================================================================================================
        // 예약번호 변경을 감지
        //=========================================================================================================================================================================
        private void reservationNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (reservationNumberTextBox.Text.Length == 8)
            {
                if (reservationNumberTextBox.Text != _reservationNumber)
                {
                    //_customerChanged = true;
                }
            }
        }

        //=========================================================================================================================================================================
        // 초기화버튼 클릭
        //=========================================================================================================================================================================
        private void resetButton_Click(object sender, EventArgs e)
        {
            _resetButtonClick = true;

            reservationNumberTextBox.ReadOnly = false;
            reservationNumberTextBox.Text = "";

            resetInputField();

            // 진행상태체크 입력폼 초기화
            initializeCutOffCheckInfoField();

            // 신규등록처리를 위해 신규등록버튼을 활성화하고 수정버튼은 비활성화
            insertNewReservationDetailButton.Enabled = true;
            saveReservationDetailButton.Enabled = false;

            _resetButtonClick = false;
            saleCurrencyCodeComboBox.Enabled = true;
            _needSaveData = false;
            reservationStatusComboBox.SelectedIndex = 0;  // 미확정으로 설정
            _selectedReservarionListRow = -1;             // 예약목록에서 선택한 행번호를 지움

        }

        //=========================================================================================================================================================================
        // VO 및 입력필드 전체 초기화
        //=========================================================================================================================================================================
        private void resetInputField()
        {
            // 예약기본 VO 객체 초기화
            clearVOReservationDetail();

            // 예약기본정보 입력폼 초기화
            resetReservationBasicInfoField();
        }

        //=========================================================================================================================================================================
        // 예약기본 VO 객체 초기화
        //=========================================================================================================================================================================
        private void clearVOReservationDetail()
        {
            vOReservationDetail.RSVT_NO = "";                                       // 예약번호
            vOReservationDetail.RSVT_DT = DateTime.Now.ToString("yyyy-MM-dd");      // 예약일자
            vOReservationDetail.ORDR_NO = "";                                       // 주문번호
            vOReservationDetail.CNFM_NO = "";                                       // CFM번호
            vOReservationDetail.PRDT_CNMB = "";                                     // 상품일련번호
            vOReservationDetail.PRDT_GRAD_CD = "";                                  // 상품등급코드
            vOReservationDetail.CUST_NO = "";                                       // 고객번호
            vOReservationDetail.TKTR_CMPN_NO = "";                                  // 모객업체번호
            vOReservationDetail.RPRS_ENG_NM = "";                                   // 대표자영문명
            vOReservationDetail.ORDE_CTIF_PHNE_NO = "";                             // 주문자연락처전화번호
            vOReservationDetail.ORDE_EMAL_ADDR = "";                                // 주문자이메일주소
            vOReservationDetail.RPRS_EMAL_ADDR = "";                                // 대표자이메일주소
            vOReservationDetail.AGE = 0;                                            // 연령
            vOReservationDetail.ADLT_SALE_PRCE = 0;                                 // 성인판매가격
            vOReservationDetail.CHLD_SALE_PRCE = 0;                                 // 소아판매가격
            vOReservationDetail.INFN_SALE_PRCE = 0;                                 // 유아판매가격
            vOReservationDetail.TOT_SALE_AMT = 0;                                   // 총판매금액=판매합계금액+옵션합계금액
            vOReservationDetail.ADLT_NBR = 0;                                       // 성인수
            vOReservationDetail.CHLD_NBR = 0;                                       // 소아수
            vOReservationDetail.INFN_NBR = 0;                                       // 유아수
            vOReservationDetail.FREE_TCKT_NBR = 0;                                  // 프리티켓수
            vOReservationDetail.DPTR_DT = "";                                       // 도착일자
            vOReservationDetail.LGMT_DAYS = 0;                                      // 숙박일수
            vOReservationDetail.TOT_TRIP_DAYS = 0;                                  // 총여행일수
            vOReservationDetail.SALE_SUM_AMT = 0;                                   // 판매합계금액
            vOReservationDetail.OPTN_SUM_AMT = 0;                                   // 옵션합계금액
            vOReservationDetail.CSPR_SUM_AMT = 0;                                   // 원가합계금액
            vOReservationDetail.SALE_CUR_CD = "";                                   // 판매통화코드
            vOReservationDetail.RSVT_AMT = 0;                                       // 예약금액
            vOReservationDetail.PRPY_AMT = 0;                                       // 선금금액
            vOReservationDetail.TOT_RECT_AMT = 0;                                   // 입금금액
            vOReservationDetail.MDPY_AMT = 0;             // 중도금금액
            vOReservationDetail.UNAM_BAL = 0;             // 미수금잔액
            vOReservationDetail.DSCT_AMT = 0;             // 할인금액
            vOReservationDetail.WON_PYMT_FEE = 0;         // 원화지급수수료
            vOReservationDetail.STEM_WON_AMT = 0;         // 가정산원화금액
            vOReservationDetail.STMT_YN = "";             // 정산여부
            vOReservationDetail.STMT_WON_AMT = 0;         // 정산원화금액
            vOReservationDetail.PRCS_DTM = "";            // 구매일시
            vOReservationDetail.SHTL_ICUD_EN = "";        // 셔틀포함여부
            vOReservationDetail.RPSB_EMPL_NO = "";        // 담당직원번호
            vOReservationDetail.CUSL_EMPL_NO = "";        // 상담직원번호
            vOReservationDetail.LST_CHCK_STTS_CD = "";    // 명단확인상태코드
            vOReservationDetail.PSPT_CHCK_STTS_CD = "";   // 여권확인상태코드
            vOReservationDetail.ARGM_CHCK_STTS_CD = "";   // 수배확인상태코드
            vOReservationDetail.VOCH_CHCK_STTS_CD = "";   // 바우처확인상태코드
            vOReservationDetail.AVAT_CHCK_STTS_CD = "";   // 항공확인상태코드
            vOReservationDetail.ISRC_CHCK_STTS_CD = "";              // 보험확인상태코드
            vOReservationDetail.PRSN_CHCK_STTS_CD = "";              // 개인확인상태코드
            vOReservationDetail.cutOffListStatusName = "";           // 명단확인상태명
            vOReservationDetail.cutOffPassportStatusName = "";       // 여권확인상태코드
            vOReservationDetail.cutOffArrangementStatusName = "";    // 수배확인상태코드
            vOReservationDetail.cutOffVoucherStatusName = "";        // 바우처확인상태코드
            vOReservationDetail.cutOffAirlineStatusName = "";        // 항공확인상태코드
            vOReservationDetail.cutOffInsuranceStatusName = "";      // 보험확인상태코드
            vOReservationDetail.cutOffPersonalInfoStatusName = "";   // 개인확인상태코드
            vOReservationDetail.LST_CHCK_DTM = "";                   // 명단확인일시
            vOReservationDetail.PSPT_CHCK_DTM = "";                  // 여권확인일시
            vOReservationDetail.ARGM_CHCK_DTM = "";                  // 수배확인일시
            vOReservationDetail.VOCH_CHCK_DTM = "";                  // 바우처확인일시
            vOReservationDetail.AVAT_CHCK_DTM = "";                  // 항공확인일시
            vOReservationDetail.ISRC_CHCK_DTM = "";                  // 보험확인일시
            vOReservationDetail.PRSN_CHCK_DTM = "";                  // 개인확인일시
            vOReservationDetail.RSVT_STTS_CD = "";                   // 예약상태코드
            vOReservationDetail.TCKT_STTS_CD = "";                   // 티켓상태코드
            vOReservationDetail.QUOT_CNMB = "";                      // 견적일련번호
            vOReservationDetail.DEL_YN = "";                         // 삭제여부
            vOReservationDetail.OPTN_NM_1 = "";                      // 옵션명1
            vOReservationDetail.OPTN_NM_2 = "";                      // 옵션명2
            vOReservationDetail.OPTN_NM_3 = "";                      // 옵션명3
            vOReservationDetail.OPTN_NM_4 = "";                      // 옵션명4
            vOReservationDetail.OPTN_NM_5 = "";                      // 옵션명5
            vOReservationDetail.CUST_RQST_CNTS = "";                 // 고객요청내용
            vOReservationDetail.INTR_MEMO_CNTS = "";                 // 내부메모내용
            vOReservationDetail.REMK_CNTS = "";                      // 비고내용
            vOReservationDetail.FRST_RGST_DTM = "";                  // 최초등록일시
            vOReservationDetail.FRST_RGTR_ID = "";                   // 최초등록자ID
            vOReservationDetail.FINL_MDFC_DTM = "";                  // 최종변경일시
            vOReservationDetail.FINL_MDFR_ID = "";                   // 최종변경자ID
        }


        //=========================================================================================================================================================================
        // 예약기본정보 입력폼 초기화
        //=========================================================================================================================================================================
        private void resetReservationBasicInfoField()
        {
            dayComboBox.SelectedIndex = -1;
            nightComboBox.SelectedIndex = -1;
            domainComboBox.SelectedIndex = -1;
            destinationCompanyForCostPriceComboBox.SelectedIndex = -1;
            domainTextBox.Visible = false;                                 // 이메일도메인 텍스트박스 히든 처리
            costPricecurrencyCodeComboBox.SelectedIndex = -1;
            reservationStatusComboBox.SelectedIndex = -1;
            productComboBox.SelectedIndex = -1;
            productGradeComboBox.SelectedIndex = -1;
            cooperativeCompanyComboBox.SelectedIndex = -1;
            optionCurrencyCodeComboBox.SelectedIndex = -1;
            saleCurrencyCodeComboBox.SelectedIndex = -1;
            optionDataGridView.Rows.Clear();
            costPriceDataGridView.Rows.Clear();
            cutOffMgtMemoDataGridView.Rows.Clear();
            arrivalDateTimePicker.Value = DateTime.Now;
            departureDateTimePicker.Value = DateTime.Now;
            purchageDateTimePicker.Value = DateTime.Now;
            reservationDateDateTimePicker.Value = DateTime.Now;
            insideMemoTextBox.Text = "";
            customerRequestMemoTextBox.Text = "";
            reservationPriceTextBox.Text = "0";                             // 예약금
            salesPriceTextBox.Text = "0";
            depositPriceTextBox.Text = "0";
            partPriceTextBox.Text = "0";
            outstandingPriceTextBox.Text = "0";
            infantPeopleCountTextBox.Text = "0";
            childPeopleCountTextBox.Text = "0";
            adultPeopleCountTextBox.Text = "0";
            customerEngNameTextBox.Text = "";
            cellphoneNumberTextBox.Text = "";
            emailIdTextBox.Text = "";
            totalNumberOfPersonsTextbox.Text = "0";
            optionSaleAmountTextBox.Text = "0";
            optionNameTextBox.Text = "";
            infantCostPriceTextBox.Text = "0";
            infantPeopleCountForCostPriceTextBox.Text = "0";
            costPriceNameTextBox.Text = "";
            childCostPriceTextBox.Text = "0";
            childPeopleCountForCostPriceTextBox.Text = "0";
            adultPeopleCountForCostPriceTextBox.Text = "0";
            adultCostPriceTextBox.Text = "0";
            childSalesPriceTextBox.Text = "0";
            infantSalesPriceTextBox.Text = "0";
            adultSalesPriceTextBox.Text = "0";
            bookerNameTextBox.Text = "";

            reservationNumberTextBox.Text = "";
            OptionSumAmountTextBox.Text = "0";
            totalSalePriceTextBox.Text = "0";
            totalOptionAmountTextBox.Text = "0";
            totalCostPriceAmountTextBox.Text = "0";
            costPriceSumAmountTextBox.Text = "0";
            totalNumberOfPersonTextBox.Text = "0";                          // 총인원수
            settlementStatusTextBox.Text = "";                              // 정산여부
            consignmentSaleFeeTextBox.Text = "0";                           // 위탁판매수수료

            toolTip1.SetToolTip(consignmentSaleFeeTextBox, "입금처리를 하면 자동 생성됩니다.");
            toolTip1.SetToolTip(bookerNameTextBox, "성명을 입력하고 엔터키를 누르면 예약자 검색을 하실 수 있습니다.");
            toolTip1.SetToolTip(searchCustomerButton, "성명을 입력하고 돋보기를 누르면 고객검색을 하실 수 있습니다.");

            settlementEstimationWonAmtTextBox.Text = "0";                   // 가정산원화금액
            settlementWonAmtTextBox.Text = "0";                             // 정산원화금액
            profitLossTextBox.Text = "0";                                   // 환차손익
            totalRevenueTextBox.Text = "0";                                 // 수익합계
            totalDepositPriceTextBox.Text = "0";                            // 입금총액
            adultSalesPriceSubTotTextBox.Text = "0";                        // 성인소계
            salesPriceSumTextBox.Text = "0";                                // 판매가합계
        }

        //=========================================================================================================================================================================
        // 진행상태체크 입력폼 초기화
        //=========================================================================================================================================================================
        private void initializeCutOffCheckInfoField()
        {
            if (_cutOffList.Count < 1) return;

            for (int ii = 0; ii < _cutOffList.Count; ii++)
            {
                this.Controls.Remove(_LabelArray[ii]);
                this.Controls.Remove(_ComboBoxArray[ii]);
                this.Controls.Remove(_DateTimePickerArray[ii]);
                this.Controls.Remove(_ButtonArray[ii]);
                /*
                _LabelArray[ii].Dispose();
                _ComboBoxArray[ii].Dispose();
                _DateTimePickerArray[ii].Dispose();
                _ButtonArray[ii].Dispose();
                */
            }

            for (int ii = 0; ii < _LabelArray.Length; ii++) _LabelArray[ii].Dispose();
            for (int ii = 0; ii < _ComboBoxArray.Length; ii++) _ComboBoxArray[ii].Dispose();
            for (int ii = 0; ii < _DateTimePickerArray.Length; ii++) _DateTimePickerArray[ii].Dispose();
            for (int ii = 0; ii < _ButtonArray.Length; ii++) _ButtonArray[ii].Dispose();
            for (int ii = 0; ii < _cutOffList.Count; ii++) _cutOffList.RemoveAt(ii);

            _cutOffList.Clear();

            /*
            this._LabelArray = new Label[0];
            this._ComboBoxArray = new ComboBox[0];
            this._DateTimePickerArray = new DateTimePicker[0];
            this._ButtonArray = new Button[0];
            */
        }

        //=========================================================================================================================================================================
        // 예약건 신규 등록
        //=========================================================================================================================================================================
        private void insertNewReservationDetailButton_Click(object sender, EventArgs e)
        {
            // 기존 데이터 조회 중에 신규등록 버튼을 누르면 
            if (reservationNumberTextBox.Text.Trim() != "")
            {
                MessageBox.Show("기 예약건입니다. 변경하시려면 수정버튼을 누르세요.");
                return;
            }

            // 예약정보 입력필드 유효성 검증
            if (ValidateForReservationInfo("NEW") == false) return;

            // 입력 파라미터 설정
            paramSetReservationDetail("NEW");

            // 예약기본 테이블 생성
            insertReservationInfo("NEW");

            _customerChanged = false;
            _needSaveData = false;
        }

        //=========================================================================================================================================================================
        // 예약옵션내역 그리드 키 다운 이벤트 처리
        //=========================================================================================================================================================================
        private void optionDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스 
            int rowIndex = optionDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0) return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && optionDataGridView.Rows.Count == rowIndex + 1) return;

            optionDataGridViewRowChoice();
        }

        //=========================================================================================================================================================================
        // 예약원가내역 그리드 키보드 선택 기능
        //=========================================================================================================================================================================
        private void costPriceDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스 
            int rowIndex = costPriceDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0) return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && costPriceDataGridView.Rows.Count == rowIndex + 1) return;

            costPriceDataGridViewRowChoice();
        }

        //=========================================================================================================================================================================
        // 상품등급 변경 시 판매코드 설정
        //=========================================================================================================================================================================
        private void productGradeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PRDT_CNMB = "";

            if (productComboBox.Items.Count > 0 && productComboBox.SelectedIndex != -1)
            {
                PRDT_CNMB = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            }
            else
            {
                return;
            }

            string _productGradeCode = (productGradeComboBox.SelectedItem as ComboBoxItem).Value.ToString();

            string query = string.Format("CALL SelectProductDetailItem ( {0}, '{1}' )", PRDT_CNMB, _productGradeCode);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품상세정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string SALE_CUR_CD = dataRow["SALE_CUR_CD"].ToString();
                Utils.SelectComboBoxItemByValue(saleCurrencyCodeComboBox, SALE_CUR_CD);        // 판매통화코드
            }

        }

        //=========================================================================================================================================================================
        // 원가 성인인원수 변경 시 인원 체크
        //=========================================================================================================================================================================
        private void adultPeopleCountForCostPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            /*
             * string ADLT_NBR = adultPeopleCountForCostPriceTextBox.Text.Trim();
            if (ADLT_NBR.Equals("")) ADLT_NBR = "0";
            int numberOfAdult = Int16.Parse(ADLT_NBR);

            if (_numberOfAdult != numberOfAdult)
            {
                DialogResult result = MessageBoxEx.Show("성인 인원수가 기본판매가의 인원수와 다릅니다. 변경 인원수를 적용하시겠습니까?", "성인 수 변경", "예", "아니오");
                if (result == DialogResult.No)
                {
                    this.Close();
                    return;
                }


                // 성인 인원수 변경
                _numberOfAdult = numberOfAdult;
                adultPeopleCountTextBox.Text = numberOfAdult.ToString();
                vOReservationDetail.ADLT_NBR = numberOfAdult;

                // 기본판매가격 재계산
                setDefaultSalesPrice();
                // 예약기본정보 저장
                saveReservationDetail();
                // 예약기본조회
                SetReservationDetail();

                MessageBox.Show("인원수가 변경되었습니다. 옵션금액과 원가정보를 확인하세요.");
            }
            */
        }

        //=========================================================================================================================================================================
        // 원가 소아인원수 변경 시 인원 체크
        //=========================================================================================================================================================================
        private void childPeopleCountForCostPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            /*
            string CHLD_NBR = childPeopleCountForCostPriceTextBox.Text.Trim();
            if (CHLD_NBR.Equals("")) CHLD_NBR = "0";
            int numberOfChild = Int16.Parse(CHLD_NBR);

            if (_numberOfChild != numberOfChild)
            {
                DialogResult result = MessageBoxEx.Show("소아 인원수가 기본판매가의 인원수와 다릅니다. 변경 인원수를 적용하시겠습니까?", "소아 수 변경", "예", "아니오");
                if (result == DialogResult.No)
                {
                    this.Close();
                    return;
                }

                // 소아 인원수 변경
                _numberOfChild = numberOfChild;
                childPeopleCountForCostPriceTextBox.Text = numberOfChild.ToString();
                vOReservationDetail.CHLD_NBR = numberOfChild;

                // 기본판매가격 재계산
                setDefaultSalesPrice();
                // 예약기본정보 저장
                saveReservationDetail();
                // 예약기본조회
                SetReservationDetail();

                MessageBox.Show("인원수가 변경되었습니다. 옵션금액과 원가정보를 확인하세요.");
            }
            */
        }

        //=========================================================================================================================================================================
        // 원가 유아인원수 변경 시 인원 체크
        //=========================================================================================================================================================================
        private void infantPeopleCountForCostPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            /*
            string INFN_NBR = infantPeopleCountForCostPriceTextBox.Text.Trim();
            if (INFN_NBR.Equals("")) INFN_NBR = "0";
            int numberOfInfant = Int16.Parse(INFN_NBR);

            if (_numberOfInfant != numberOfInfant)
            {
                DialogResult result = MessageBoxEx.Show("유아 인원수가 기본판매가의 인원수와 다릅니다. 변경 인원수를 적용하시겠습니까?", "유아 수 변경", "예", "아니오");
                if (result == DialogResult.No)
                {
                    this.Close();
                    return;
                }

                // 소아 인원수 변경
                _numberOfInfant = numberOfInfant;
                infantPeopleCountForCostPriceTextBox.Text = numberOfInfant.ToString();
                vOReservationDetail.INFN_NBR = numberOfInfant;

                // 기본판매가격 재계산
                setDefaultSalesPrice();
                // 예약기본정보 저장
                saveReservationDetail();
                // 예약기본조회
                SetReservationDetail();

                MessageBox.Show("인원수가 변경되었습니다. 옵션금액과 원가정보를 확인하세요.");
            }
            */
        }

        //=========================================================================================================================================================================
        // 기본판매가격 계산
        //=========================================================================================================================================================================
        private void calcSalePrice()
        {
            // 기본판매가격 계산
            calcDefaultSalePrice();

            // 위탁판매지급수수료 계산
            if (vOReservationDetail.WON_PYMT_FEE == 0)
            {
                double wonPayableFee = calcPayableFee();  // 금액이 없으면 새로 계산
                redrawingConsignmentSaleFeeInputform(wonPayableFee.ToString(), "수수료(미입금)", 38, 32);
            }
            else
            {
                redrawingConsignmentSaleFeeInputform(vOReservationDetail.WON_PYMT_FEE.ToString(), "수수료(입금완료)", 30, 32);
            }

            // 수익금액 계산
            calcRevenue();
        }

        //=========================================================================================================================================================================
        // 기본판매가격 계산
        //=========================================================================================================================================================================
        private void calcDefaultSalePrice()
        {
            string ADLT_SALE_PRCE = adultSalesPriceTextBox.Text.Trim();
            string CHLD_SALE_PRCE = childSalesPriceTextBox.Text.Trim();
            string INFN_SALE_PRCE = infantSalesPriceTextBox.Text.Trim();

            if (adultSalesPriceTextBox.Text.Trim().Equals("")) adultSalesPriceTextBox.Text = "0";
            if (childSalesPriceTextBox.Text.Trim().Equals("")) childSalesPriceTextBox.Text = "0";
            if (infantSalesPriceTextBox.Text.Trim().Equals("")) infantSalesPriceTextBox.Text = "0";

            double adultSalePrice = double.Parse(adultSalesPriceTextBox.Text);
            double childSalePrice = double.Parse(childSalesPriceTextBox.Text);
            double infantSalePrice = double.Parse(infantSalesPriceTextBox.Text);

            if (adultPeopleCountTextBox.Text.Trim().Equals("")) adultPeopleCountTextBox.Text = "0";
            if (childPeopleCountTextBox.Text.Trim().Equals("")) childPeopleCountTextBox.Text = "0";
            if (infantPeopleCountTextBox.Text.Trim().Equals("")) infantPeopleCountTextBox.Text = "0";

            // 기본판매가격의 인원수를 원가인원수 입력필드에 기본 설정
            adultPeopleCountForCostPriceTextBox.Text = adultPeopleCountTextBox.Text;
            childPeopleCountForCostPriceTextBox.Text = childPeopleCountTextBox.Text;
            infantPeopleCountForCostPriceTextBox.Text = infantPeopleCountTextBox.Text;

            _numberOfAdult = Int16.Parse(adultPeopleCountTextBox.Text);
            _numberOfChild = Int16.Parse(childPeopleCountTextBox.Text);
            _numberOfInfant = Int16.Parse(infantPeopleCountTextBox.Text);

            // 총인원수 계산 = (성인수 + 소아수 + 유아수)
            _numberOfPeople = _numberOfAdult + _numberOfChild + _numberOfInfant;

            // 판매합계금액 계산 = (성인판매가*성인수) + (소아판매가*소아수) + (유아판매가*유아수)
            double adultTotalSaleAmount = Math.Round(adultSalePrice * _numberOfAdult);
            double childTotalSaleAmount = Math.Round(childSalePrice * _numberOfChild);
            double infantTotalSaleAmount = Math.Round(infantSalePrice * _numberOfInfant);

            double SaleSumAmount = adultTotalSaleAmount + childTotalSaleAmount + infantTotalSaleAmount;

            totalSalePriceTextBox.Text = Utils.SetComma(SaleSumAmount.ToString());                       // 기본가
            salesPriceSumTextBox.Text = Utils.SetComma(SaleSumAmount.ToString());                        // 기본판매가합계

            adultSalesPriceSubTotTextBox.Text = Utils.SetComma(adultTotalSaleAmount.ToString());         // 성인판매가소계
            childSalesPriceSubTotTextBox.Text = Utils.SetComma(childTotalSaleAmount.ToString());         // 소아판매가소계
            infantSalesPriceSubTotTextBox.Text = Utils.SetComma(infantTotalSaleAmount.ToString());       // 유아판매가소계

            string OPTN_SUM_AMT = OptionSumAmountTextBox.Text.Trim();
            if (OPTN_SUM_AMT == "") OPTN_SUM_AMT = "0";
            double optionTotalAmount = Double.Parse(OPTN_SUM_AMT);

            // 총판매금액 계산 = (판매합계금액 + 옵션합계금액)
            double totalSaleAmount = SaleSumAmount + optionTotalAmount;
            vOReservationDetail.TOT_SALE_AMT = totalSaleAmount;

            salesPriceTextBox.Text = Utils.SetComma(totalSaleAmount.ToString());                         // 판매총액

            totalNumberOfPersonTextBox.Text = _numberOfPeople.ToString();                                // 총인원수
            totalNumberOfPersonsTextbox.Text = "";                                                       // 옵션 인원수

            // 미수잔액 재계산
            double unReceivableAmount = totalSaleAmount - vOReservationDetail.TOT_RECT_AMT;
            outstandingPriceTextBox.Text = Utils.SetComma(unReceivableAmount.ToString());
        }

        //=========================================================================================================================================================================
        /// 원화지급수수료 계산
        //=========================================================================================================================================================================
        private double calcPayableFee()
        {
            double wonPayableFee = 0;

            string query = string.Format("CALL SelectRateItem({0},'{1}','{2}')", _productNo, vOReservationDetail.TKTR_CMPN_NO, "01");
            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("요율정보를 검색할 수 없습니다. 운영담당자에게 연락하세요.");
            }
            else
            {
                DataRow dataRow = dataSet.Tables[0].Rows[0];

                double totalSalePrice = Double.Parse(salesPriceTextBox.Text.Trim());
                double RATE = Double.Parse(dataRow["RATE"].ToString());
                wonPayableFee = Math.Round(totalSalePrice * RATE / 100, 0);
            }

            return wonPayableFee;
        }

        //=========================================================================================================================================================================
        // 성인 기본판매가격 변경시 금액 재계산
        //=========================================================================================================================================================================
        private void adultSalesPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == true) return;    // 폼이 로딩되고 있을 때에는 skip

            if (adultPeopleCountTextBox.Text.Trim().Equals("")) adultPeopleCountTextBox.Text = "0";
            if (adultPeopleCountTextBox.Text.Trim() != "0") calcSalePrice();
        }
        //=========================================================================================================================================================================
        // 소아 기본판매가격 변경시 금액 재계산
        //=========================================================================================================================================================================
        private void childSalesPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == true) return;    // 폼이 로딩되고 있을 때에는 skip

            if (childPeopleCountTextBox.Text.Trim().Equals("")) childPeopleCountTextBox.Text = "0";
            if (childPeopleCountTextBox.Text.Trim() != "0") calcSalePrice();
        }
        //=========================================================================================================================================================================
        // 유아 기본판매가격 변경시 금액 재계산
        //=========================================================================================================================================================================
        private void infantSalesPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == true) return;    // 폼이 로딩되고 있을 때에는 skip

            if (infantPeopleCountTextBox.Text.Trim().Equals("")) infantPeopleCountTextBox.Text = "0";
            if (infantPeopleCountTextBox.Text.Trim() != "0") calcSalePrice();
        }

        //=========================================================================================================================================================================
        // 성인수가 입력되면 판매가격을 재계산
        //=========================================================================================================================================================================
        private void adultPeopleCountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == true) return;    // 폼이 로딩되고 있을 때에는 skip

            if (adultPeopleCountTextBox.Text.Trim().Equals("")) adultPeopleCountTextBox.Text = "0";
            if (adultPeopleCountTextBox.Text.Trim() != "0") calcSalePrice();
        }

        //=========================================================================================================================================================================
        // 소아수가 입력되면 판매가격을 재계산
        //=========================================================================================================================================================================
        private void childPeopleCountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == true) return;    // 폼이 로딩되고 있을 때에는 skip

            if (childPeopleCountTextBox.Text.Trim().Equals("")) childPeopleCountTextBox.Text = "0";
            if (childPeopleCountTextBox.Text.Trim() != "0") calcSalePrice();
        }

        //=========================================================================================================================================================================
        // 유아수가 입력되면 판매가격을 재계산
        //=========================================================================================================================================================================
        private void infantPeopleCountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == true) return;    // 폼이 로딩되고 있을 때에는 skip

            if (infantPeopleCountTextBox.Text.Trim().Equals("")) infantPeopleCountTextBox.Text = "0";
            if (infantPeopleCountTextBox.Text.Trim() != "0") calcSalePrice();
        }

        //=========================================================================================================================================================================
        // 수익금액 계산
        //=========================================================================================================================================================================
        private void calcRevenue()
        {
            double settlementAmount = 0;
            double profitLoss = 0;

            // 정산이 완료된 건은 환차손익을 계산
            if (vOReservationDetail.STMT_WON_AMT > 0)
            {
                profitLoss = vOReservationDetail.STMT_WON_AMT - vOReservationDetail.STEM_WON_AMT;
                profitLossTextBox.Text = Utils.SetComma(profitLoss.ToString());
            }
            else
            {
                profitLossTextBox.Text = "0";
            }

            // 정산이 완료되었으면 환차손익으로, 가정산이면 예상환차손익으로 계산
            if (vOReservationDetail.STMT_YN.Equals("Y"))
            {
                settlementAmount = vOReservationDetail.STMT_WON_AMT;
            }
            else
            {
                settlementAmount = vOReservationDetail.STEM_WON_AMT;
            }

            // 총수익 = 총판매금액 = 원화지급수수료 - (가)정산금액 ± 환차손익            
            double totalRevenue = vOReservationDetail.TOT_SALE_AMT -
                                  vOReservationDetail.WON_PYMT_FEE -
                                  settlementAmount +
                                 (profitLoss);

            totalRevenueTextBox.Text = Utils.SetComma(totalRevenue.ToString());
        }

        //=========================================================================================================================================================================
        // 예약상세조회 --> 바우처 연동 [19/08/13 배장훈]
        //=========================================================================================================================================================================
        private void voucherProcButton_Click(object sender, EventArgs e)
        {
            string reservationNum = reservationNumberTextBox.Text.Trim();
            string bookerName = bookerNameTextBox.Text.Trim();

            if (reservationNum == "" || reservationNum == null)
                MessageBox.Show("예약번호를 확인해주세요.");
            if (bookerName == "" || bookerName == null)
                MessageBox.Show("예약자 이름을 확인해주세요.");

            if ((reservationNum != "" && reservationNum != null) && (bookerName != "" && bookerName != null))
            {
                string _Filestr = "C:\\TripERP\\Vouchers\\" + productComboBox.Text + ".xlsx";
                System.IO.FileInfo fi = new System.IO.FileInfo(_Filestr);
                if (fi.Exists)
                {
                    VoucherPrintOutMgt form = new VoucherPrintOutMgt();
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.SetReservationNum(reservationNum);
                    form.SetBookerName(bookerName);
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("'" + productComboBox.Text + "'상품의 바우처 템플릿이 존재하지 않습니다.");
                }



            }
        }

        //=========================================================================================================================================================================
        // 이메일도메인을 직접입력하려는 경우 도메인명 TextBox를 활성화
        //=========================================================================================================================================================================
        private void domainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (domainComboBox.SelectedIndex == 0)
            {
                domainTextBox.Visible = true;
                domainTextBox.Focus();
            }
            else
            {
                domainTextBox.Visible = false;
            }
        }



        // 예약상세 검색시 고객 이메일 도메인 정보를 ComboBox 에서 선택 출력 --> 190820 박현호
        //================================================================================================================================================================================================
        public void setEmailDomain(string domain)
        {
            ComboBox.ObjectCollection itemList = domainComboBox.Items;
            IEnumerator ie = itemList.GetEnumerator();
            int count = 0;
            while (ie.MoveNext())
            {
                ComboBoxItem ob = (ComboBoxItem)ie.Current;
                string itemstr = ob.ToString();
                if (itemstr.Equals(_emailDomain))
                {
                    string getEmailValue = ob.Text.ToString();
                    domainComboBox.ResetText();
                    domainComboBox.SelectedText = getEmailValue;
                    break;
                }
                else
                {
                    count++;
                }
            }

            if (count == itemList.Count)
            {
                domainComboBox.Text = domain;
            }
        }

        //================================================================================================================================================================================================
        // 상품등록버튼 클릭
        //================================================================================================================================================================================================
        private void insertProductInfoButton_Click(object sender, EventArgs e)
        {
            PopUpAddProductInfo form = new PopUpAddProductInfo();

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            _productNo = form.GetProductNumber();
            _productGradeCode = form.GetProductGradeCode();

            if (_productNo == null || _productNo == "") return;
            if (_productGradeCode == null || _productGradeCode == "") return;

            vOReservationDetail.PRDT_CNMB = _productNo;
            vOReservationDetail.PRDT_GRAD_CD = _productGradeCode;

            // 상품 콤보박스 아이템 로드
            LoadProductComboBoxItems();
            Utils.SelectComboBoxItemByValue(productComboBox, _productNo);

            // 상품등급정보 검색 및 설정
            resetProductGradeComboBox();
            Utils.SelectComboBoxItemByValue(productGradeComboBox, _productGradeCode);

            // 상품을 변경하면 예약기본 테이블의 진행상태정보를 초기화
            string query = string.Format("CALL UpdateRsvtCutOffStatusClear('{0}')", reservationNumberTextBox.Text.Trim());
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("상품변경에 따른 예약진행상태 정보를 초기화하지 못했습니다. 운영담당자에게 연락하세요.");
                return;
            }

            // 진행확인항목 초기화
            resetCutOffCheckInfoField();
        }

    }
}