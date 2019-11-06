using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MetroFramework.Forms;
using TripERP.Common;
using TripERP.CustomerMgt;
using TripERP.ProductMgt;
using TripERP.DocumentMgt;
using TripERP.CommonTask;
using System.Collections;

// SSH
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Renci.SshNet.Common;
using System.IO;
using TripERP.Invoice;

namespace TripERP.ReservationMgt
{
    public partial class ReservationDetailInfoMgt : Form
    {
        /// 
        /// 전역변수 선언
        /// 
        // 예약기본 VO 클래스
        public VOReservationDetail vOReservationDetail = new VOReservationDetail();

        bool _needSaveData = false;

        private string _userMessage = "";

        private string _reservationNumber = "";
        private string _saveMode = "";
        private string _customerNumber = "";      // 고객번호
        private string _emailDomain = "";
        private string _employeeNumber = "";
        private string _employeeName = "";
        private string _productNo = "";
        private string _productGradeCode = "";

        private bool _receivedFeeYn = false;              // 위탁판매지급수수료를 이미 입금처리했는지 판단
        private bool _socialMarketSales = false;          // 소셜모객업체인지 여부

        private int _numberOfAdult = 0;     // 전역변수: 성인수
        private int _numberOfChild = 0;     // 전역변수: 소아수s
        private int _numberOfInfant = 0;    // 전역변수: 유아수
        private int _numberOfPeople = 0;    // 전역변수: 총인원수

        ComboBox _currencyCodeComboBox = new ComboBox();                                          // 통화코드 공통 콤보박스
        ComboBox _arrangementCompanyComboBox = new ComboBox();                                    // 수배처 공통 콤보박스
        ComboBox _adultDivisioncodeComboBox = new ComboBox();                                     // 성인구분코드 공통 콤보박스

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 진행상태 객체 동적 생성용
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Label[] _LabelArray = null;
        ComboBox[] _ComboBoxArray = null;
        DateTimePicker[] _DateTimePickerArray = null;
        Button[] _ButtonArray = null;
        List<string> _cutOffList = new List<string>();

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 상품판매가격 객체 동적 생성용
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        int _salePriceArrayCount = 0;

        private FlowLayoutPanel fl;
        private List<FlowLayoutPanel> fl_salePrice_list = new List<FlowLayoutPanel>();

        List<Button> _salePriceAddButtonArray = new List<Button>();                      // 행추가 버튼
        List<string> _salePriceNoStringArray = new List<string>();                       // 일련번호 텍스트박스
        List<TextBox> _salePriceNameTextBoxArray = new List<TextBox>();                  // 판매가격명 텍스트박스
        List<ComboBox> _saleAdultDivisionCodeComboBoxArray = new List<ComboBox>();       // 성인구분코드 콤보박스
        List<ComboBox> _saleSaleCurrencyCodeComboBoxArray = new List<ComboBox>();        // 판매통화코드 콤보박스
        List<TextBox> _salePriceTextBoxArray = new List<TextBox>();                      // 판매단가 텍스트박스
        List<TextBox> _saleApplyExchangeRateTextBoxArray = new List<TextBox>();          // 적용환율 텍스트박스
        List<TextBox> _saleWonPriceTextBoxArray = new List<TextBox>();                   // 원화판매단가 텍스트박스
        List<TextBox> _salePricePeopleCountTextBoxArray = new List<TextBox>();           // 인원수 텍스트박스
        List<TextBox> _salePriceSumAmountTextBoxArray = new List<TextBox>();             // 판매가격소계 텍스트박스
        List<TextBox> _saleWonPriceSumAmountTextBoxArray = new List<TextBox>();          // 원화판매가격소계 텍스트박스
        List<Button> _salePriceEraseButtonOptionArray = new List<Button>();              // 행삭제 버튼

        int _salePriceXPoint1 = 625;    // 판매가격 행추가버튼 x좌표
        int _salePriceXPoint2 = 650;    // 판매가격명 x좌표
        int _salePriceXPoint3 = 900;    // 성인구분 x좌표
        int _salePriceXPoint4 = 1000;   // 통화코드 x좌표
        int _salePriceXPoint5 = 1115;   // 판매가격 x좌표
        int _salePriceXPoint6 = 1176;   // 원화판매가격 x좌표
        int _salePriceXPoint7 = 1297;   // 인원수 행삭제 x좌표
        int _salePriceXPoint8 = 1397;   // 판매가격 소계 x좌표
        int _salePriceXPoint9 = 1497;   // 원화판매가격 소계 x좌표
        int _salePriceXPoint10 = 1597;  // 판매가격 행삭제 x좌표
        int _salePriceYPoint = 240;     // 판매가격 y좌표(첫행)
        int _salePriceLastYPoint = 0;   // 판매가격 y좌표(최종행)

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 원가 객체 동적 생성용
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        int _costPriceArrayCount = 0;

        private FlowLayoutPanel _fl_costPrice;
        private List<FlowLayoutPanel> _fl_costPrice_list = new List<FlowLayoutPanel>();

        List<Button> _costPriceAddButtonArray = new List<Button>();                      // 행추가 버튼
        List<string> _costPriceNoStringArray = new List<string>();                       // 원가일련번호 텍스트박스
        List<string> _costPriceReservationNoStringArray = new List<string>();            // 예약원가일련번호 텍스트박스
        List<TextBox> _costPriceNameTextBoxArray = new List<TextBox>();                  // 원가명 텍스트박스
        List<ComboBox> _costPriceArrangementCompanyComboxArray = new List<ComboBox>();   // 수배처 콤보박스
        List<ComboBox> _costPriceAdultDivisionCodeComboBoxArray = new List<ComboBox>();  // 성인구분코드 콤보박스
        List<ComboBox> _costPriceCurrencyCodeComboBoxArray = new List<ComboBox>();       // 통화코드 콤보박스
        List<TextBox> _costPriceTextBoxArray = new List<TextBox>();                      // 원가금액 텍스트박스
        List<TextBox> _costPricePeopleCountTextBoxArray = new List<TextBox>();           // 인원수 텍스트박스
        List<TextBox> _costPriceSumAmountTextBoxArray = new List<TextBox>();             // 원가소계금액 텍스트박스
        List<Button> _costPriceEraseButtonOptionArray = new List<Button>();              // 원가 행삭제 버튼

        int _costPriceXPoint1 = 325;    // 원가 행추가버튼 x좌표
        int _costPriceXPoint2 = 350;    // 원가명 x좌표
        int _costPriceXPoint3 = 400;    // 수배처 x좌표
        int _costPriceXPoint4 = 450;    // 성인구분 x좌표
        int _costPriceXPoint5 = 500;    // 원가통화코드 x좌표
        int _costPriceXPoint6 = 600;    // 원가금액 x좌표
        int _costPriceXPoint7 = 680;    // 원가 인원수 x좌표
        int _costPriceXPoint8 = 750;    // 원가 원가소계 x좌표
        int _costPriceXPoint9 = 870;    // 원가 행삭제 x좌표
        int _costPriceYPoint = 200;     // 원가 y좌표(첫행)
        int _costPriceLastYPoint = 0;   // 원가 y좌표(최종행)

        public ReservationDetailInfoMgt()
        {
            InitializeComponent();
        }

        //=========================================================================================================================================================================
        // 예약상세정보 초기 로딩
        //=========================================================================================================================================================================
        public void redrawReservationDetailInfo()
        {

            // 입력필드 초기화
            resetInputField();

            InitControls();

            if (_saveMode == Global.SAVEMODE_UPDATE && _reservationNumber != "")
            {
                reservationNumberTextBox.ReadOnly = true;
                insertNewReservationDetailButton.Enabled = false;
                saveReservationDetailButton.Enabled = true;
                setReservationInfoToForm();
            }
            else
            {
                reservationNumberTextBox.ReadOnly = false;
                insertNewReservationDetailButton.Enabled = true;
                saveReservationDetailButton.Enabled = false;
                _employeeNumber = Global.loginInfo.ACNT_ID;         // 등록모드인 경우 담당직원번호는 로그인ID로 세팅
                setCreateDefaultSalePriceInfoLine();                        // 판매가격 입력 필드 초기화
                setCreateDefaultCostPriceInfoLine();                        // 옵션 입력 필드 초기화
                bookerNameTextBox.Focus();
            }

        }

        //=========================================================================================================================================================================
        // 그리드 및 콤보박스 초기화
        //=========================================================================================================================================================================
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

            // 통화 코드 콤보박스 아이템 로드
            LoadCurrencyCodeComboBoxItems();

            // 수배처 콤보박스 아이템 로드
            LoadDestinationCompanyComboBoxItems();

            /*
            if (_saveMode == Global.SAVEMODE_UPDATE)
            {
                searchCustomerButton.Enabled = false;
            }
            */
        }

        //=========================================================================================================================================================================
        // 공통코드 콤보박스 초기값 로드
        //=========================================================================================================================================================================
        private void LoadCommonCodeItems()
        {
            string[] groupNameArray = { "EMAL_DOMN_ADDR", "RSVT_STTS_CD" , "ADLT_DVSN_CD"};

            ComboBox[] comboBoxArray = { domainComboBox, reservationStatusComboBox, _adultDivisioncodeComboBox };

            for (int gi = 0; gi < groupNameArray.Length; gi++)
            {
                if (comboBoxArray[gi].Items.Count > 0) comboBoxArray[gi].Items.Clear();

                List<CommonCodeItem> list = Global.GetCommonCodeList(groupNameArray[gi]);

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
            DataGridView[] dataGridViewArray = { cutOffMgtMemoDataGridView };
            for (int i = 0; i < dataGridViewArray.Length; i++)
            {
                dataGridViewArray[i].RowHeadersVisible = false;
              //dataGridViewArray[i].SelectionMode = DataGridViewSelectionMode.FullRowSelect;
              //dataGridViewArray[i].MultiSelect = false;
                dataGridViewArray[i].AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 254);
              //dataGridViewArray[i].EnableHeadersVisualStyles = false;
              //dataGridViewArray[i].ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
              //dataGridViewArray[i].ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
              //dataGridViewArray[i].RowHeadersVisible = false;
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
        // 폼 로딩 시 초기화
        //=========================================================================================================================================================================
        private void ReservationDetailInfoMgt_Load(object sender, EventArgs e)
        {
            redrawReservationDetailInfo();
            isInvoiceButtonTrue();              // B2C 직판일 경우에 INVOICE 생성 BUTTON 출력
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
            vOReservationDetail.TOT_SALE_AMT = 0;                                   // 외화총판매금액=판매합계금액+옵션합계금액
            vOReservationDetail.WON_TOT_SALE_AMT = 0;                               // 원화총판매금액=판매합계금액+옵션합계금액 (예약판매가격내역.적용환율을 반영)
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
            
            domainTextBox.Visible = false;                                 // 이메일도메인 텍스트박스 히든 처리
            
            reservationStatusComboBox.SelectedIndex = -1;
            productComboBox.SelectedIndex = -1;
            productGradeComboBox.SelectedIndex = -1;
            cooperativeCompanyComboBox.SelectedIndex = -1;

            saleCurrencyCodeComboBox.SelectedIndex = -1;
            
            cutOffMgtMemoDataGridView.Rows.Clear();
            arrivalDateTimePicker.Value = DateTime.Now;
            departureDateTimePicker.Value = DateTime.Now;
            purchageDateTimePicker.Value = DateTime.Now;
            reservationDateDateTimePicker.Value = DateTime.Now;
            insideMemoTextBox.Text = "";
            customerRequestMemoTextBox.Text = "";
            reservationPriceTextBox.Text = "0";                             // 예약금
            totalWonSalePriceTextBox.Text = "0";
            depositPriceTextBox.Text = "0";
            partPriceTextBox.Text = "0";
            outstandingPriceTextBox.Text = "0";
            customerEngNameTextBox.Text = "";
            cellphoneNumberTextBox.Text = "";
            emailIdTextBox.Text = "";
            totalNumberOfPersonTextBox.Text = "0";

            bookerNameTextBox.Text = "";

            reservationNumberTextBox.Text = "";

            totalSalePriceTextBox.Text = "0";                                       // 판매기본가
            totalNumberOfPersonTextBox.Text = "0";                          // 총인원수
            settlementStatusTextBox.Text = "";                                  // 정산여부
            consignmentSaleFeeTextBox.Text = "0";                           // 위탁판매수수료

            salePriceSumTextBox.Text = "0";                                     // 기본가합계 (기본가 영역)

            //optionPriceSumTextBox.Text = "0";                               // 옵션합계 (옵션가 영역)
            costPriceSumTextBox.Text = "0";                                      // 원가합계 (원가 영역)

            settlementEstimationWonAmtTextBox.Text = "0";                   // 가정산원화금액
            settlementWonAmtTextBox.Text = "0";                                 // 정산원화금액
            profitLossTextBox.Text = "0";                                               // 환차손익
            totalRevenueTextBox.Text = "0";                                         // 수익합계
            totalDepositPriceTextBox.Text = "0";                                    // 입금총액

            adultPeopleCountTextBox.Text = "";                          // 성인수
            childPeopleCountTextBox.Text = "";                          // 소아수
            infantPeopleCountTextBox.Text = "";                         // 유아수

            toolTip1.SetToolTip(consignmentSaleFeeTextBox, "입금처리를 하면 자동 생성됩니다.");
            toolTip1.SetToolTip(bookerNameTextBox, "성명을 입력하고 엔터키를 누르면 예약자 검색을 하실 수 있습니다.");
            toolTip1.SetToolTip(searchCustomerButton, "성명을 입력하고 돋보기를 누르면 고객검색을 하실 수 있습니다.");
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
            }

            for (int ii = 0; ii < _LabelArray.Length; ii++) _LabelArray[ii].Dispose();
            for (int ii = 0; ii < _ComboBoxArray.Length; ii++) _ComboBoxArray[ii].Dispose();
            for (int ii = 0; ii < _DateTimePickerArray.Length; ii++) _DateTimePickerArray[ii].Dispose();
            for (int ii = 0; ii < _ButtonArray.Length; ii++) _ButtonArray[ii].Dispose();
            for (int ii = 0; ii < _cutOffList.Count; ii++) _cutOffList.RemoveAt(ii);

            _cutOffList.Clear();
        }

        //=========================================================================================================================================================================
        // 예약정보를 검색하여 화면에 표시
        //=========================================================================================================================================================================
        private void setReservationInfoToForm()
        {
            if (setReservationDetail() == false) return;               // 예약기본조회

            setReservationSalePriceList();                             // 예약판매가격내역조회
            setReservationCostPriceList();                             // 예약원가조회
            SetMemoList();                                             // 진행관리메모조회

            //paramsetReservationDetail("UPD");                        // 예약VO에 값 설정

            //-----------------------------------------------------------------------
            // 정산여부를 확인하여 예, 아니요 분기처리       --> 191028 박현호
            //-----------------------------------------------------------------------
            string STMT_STATE = settlementStatusTextBox.Text.Trim();
            if (STMT_STATE.Equals("예"))
            {
                settlementEstimationWonAmtTextBox.Text = "0";
            }
        }


        //=========================================================================================================================================================================
        // 상품이 바뀔 때 이벤트 처리
        //=========================================================================================================================================================================
        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productComboBox.Items.Count > 0 && productComboBox.SelectedIndex != -1)
            {
                if (_reservationNumber != "")
                {
                    DialogResult result = MessageBoxEx.Show("상품을 변경하면 예약정보가 사라집니다. 계속하시겠습니까?", "상품변경", "예", "아니오");
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                _productNo = (productComboBox.SelectedItem as ComboBoxItem).Value.ToString();
                // 상품등급정보 검색 및 설정
                resetProductGradeComboBox(_productNo, departureDateTimePicker.Value.ToString("yyyy-MM-dd").Substring(0,10));

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
        private void resetProductGradeComboBox(string PRDT_CNMB, string baseDate)
        {
            if (productComboBox.SelectedIndex == -1) return;

            productGradeComboBox.Items.Clear();
            productGradeComboBox.Text = "";

            string query = string.Format("CALL SelectPrdtDtlsList ( {0}, '{1}' )", PRDT_CNMB, baseDate);
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
            string query = "CALL SelectAllTicketerCompanyList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("모객업체 정보를 가져올 수 없습니다.");
                return;
            }

            cooperativeCompanyComboBox.Items.Add(new ComboBoxItem("--- 선택 ---", " "));
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
            nightComboBox.Items.Add(0.ToString());
            for (int i = 1; i <= 20; i++)
            {
                nightComboBox.Items.Add(i.ToString());
                dayComboBox.Items.Add(i.ToString());
            }
            dayComboBox.Items.Add("21");
            dayComboBox.Items.Add("22");
        }

        //=========================================================================================================================================================================
        // 통화코드 콤보박스 아이템 로드
        //=========================================================================================================================================================================
        private void LoadCurrencyCodeComboBoxItems()
        {
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
                _currencyCodeComboBox.Items.Add(item);         // 통화코드 공통 콤보박스
            }
            
            if (saleCurrencyCodeComboBox.Items.Count > 0)
            {
                saleCurrencyCodeComboBox.SelectedIndex = -1;
            }
            
        }

        //=========================================================================================================================================================================
        // 수배처 콤보박스 초기화 (옵션, 원가)
        //=========================================================================================================================================================================
        private void LoadDestinationCompanyComboBoxItems()
        {
            _arrangementCompanyComboBox.Items.Clear();

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

                _arrangementCompanyComboBox.Items.Add(item);
            }

            if (_arrangementCompanyComboBox.Items.Count > 0)
            {
                _arrangementCompanyComboBox.SelectedIndex = -1;
            }
        }

        //=========================================================================================================================================================================
        // 저장된 예약기본정보값을 입력필드에 Set.
        //=========================================================================================================================================================================
        public bool setReservationDetail()
        {
            reservationNumberTextBox.Text = _reservationNumber;
            if (_reservationNumber == "")
                return false;

            //------------------------------------------------------------------------------------------------------------------------------------------------
            // 191023 - 박현호
            // 해당 예약번호가 구매자등록을 통해 등록한 (티몬, 위메프 etc..) 예약건이라면 가격정보의 기본값 Button 을 Disabled 처리..
            // Excel 로 Import 한 내역의 예약건이라면 판매가 정보가 Excel 에 기재되어있는 값을 기반으로 해야하기 때문, 변경되어선 안됨            
            //------------------------------------------------------------------------------------------------------------------------------------------------
            string RSVT_NO = reservationNumberTextBox.Text.ToString().Trim();            
            string querySelect = string.Format("CALL SelectSocialCompanyReservation('{0}')", RSVT_NO);
            DataSet ds = DbHelper.SelectQuery(querySelect);
            DataRow row = ds.Tables[0].Rows[0];
            int COUNT = int.Parse(row["RSVT_FROM_EXCEL"].ToString().Trim());
            string CMPN_NM = row["CMPN_NM"].ToString().Trim();            
            if(COUNT > 0)
            {
                setDefaultSalesPriceButton.Enabled = false;
            }

            string query = string.Format("CALL SelectRsvtItem ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약상세정보를 가져올 수 없습니다.");
                return false;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];
            string CUST_NM = dataRow["CUST_NM"].ToString().Trim();
            string RPRS_ENG_NM = dataRow["RPRS_ENG_NM"].ToString().Trim();
            string ORDE_CTIF_PHNE_NO = dataRow["ORDE_CTIF_PHNE_NO"].ToString().Trim();
            string ORDE_EMAL_ADDR = dataRow["ORDE_EMAL_ADDR"].ToString().Trim();
            string SALE_CUR_CD = dataRow["SALE_CUR_CD"].ToString();

            /*
            //-----------------------------------------------------------------------------------
            // 암호화 내용 복호화
            //-----------------------------------------------------------------------------------
            CUST_NM = EncryptMgt.Decrypt(dataRow["CUST_NM"].ToString().Trim(), EncryptMgt.aesEncryptKey);                                                               // 고객명
            ORDE_CTIF_PHNE_NO = EncryptMgt.Decrypt(ORDE_CTIF_PHNE_NO, EncryptMgt.aesEncryptKey);                                                                                // 주문자 전화번호 
            RPRS_ENG_NM = EncryptMgt.Decrypt(dataRow["RPRS_ENG_NM"].ToString().Trim(), EncryptMgt.aesEncryptKey);                                               // 대표자 영문명
            ORDE_EMAL_ADDR = EncryptMgt.Decrypt(ORDE_EMAL_ADDR, EncryptMgt.aesEncryptKey);                                  // 고객 이메일
            //string EMPL_NM = EncryptMgt.Decrypt(dataRow["EMPL_NM"].ToString().Trim(), EncryptMgt.aesEncryptKey);                                                                // 담당자명            
            */

            ////
            // 최상단 정보
            _customerNumber = dataRow["CUST_NO"].ToString();                                                                                                        // 고객번호
            bookerNameTextBox.Text = CUST_NM;                                                                                                                               // 예약자
            cellphoneNumberTextBox.Text = ORDE_CTIF_PHNE_NO;                                                                                                    // 휴대폰
            customerEngNameTextBox.Text = RPRS_ENG_NM;                                                                                                             // 대표자영문명
            departureDateTimePicker.Value = Utils.GetDateTimeFormatFromString(dataRow["DPTR_DT"].ToString());        // 출발일자
            nightComboBox.SelectedItem = dataRow["LGMT_DAYS"].ToString();                                            // 숙박일수
            dayComboBox.SelectedItem = dataRow["TOT_TRIP_DAYS"].ToString();                                          // 총여행일수    

            productComboBox.SelectedIndexChanged -= productComboBox_SelectedIndexChanged;
            Utils.SelectComboBoxItemByValue(productComboBox, dataRow["PRDT_CNMB"].ToString());                       // 상품일련번호 

            resetProductGradeComboBox(dataRow["PRDT_CNMB"].ToString(), dataRow["DPTR_DT"].ToString());
            productComboBox.SelectedIndexChanged += productComboBox_SelectedIndexChanged;
            Utils.SelectComboBoxItemByValue(productGradeComboBox, dataRow["PRDT_GRAD_CD"].ToString());               // 상품등급

            _productNo = dataRow["PRDT_CNMB"].ToString();
            _productGradeCode = dataRow["PRDT_GRAD_CD"].ToString();

            _employeeNumber = dataRow["RPSB_EMPL_NO"].ToString();                                                    // 담당직원번호
            _employeeName = dataRow["EMPL_NM"].ToString();                                                           // 담당직원이름

            if (dataRow["STMT_YN"].ToString().Equals("Y")) { 
                settlementStatusTextBox.Text = "예";                                                                 // 정산여부
            }
            else { 
                settlementStatusTextBox.Text = "아니오";                                                             // 정산여부
            }
            //--------------------------------------------------------------------------------------------------------------------------------------------------
            //totalAccountPayableTextBox.Text = Utils.SetComma(dataRow["---------"].ToString());                     // 미지급금합계
            //totalAccountReceivableTextBox.Text = Utils.SetComma(dataRow["------"].ToString());                     // 선급금합계

            // 기본정보
            reservationDateDateTimePicker.Value = Utils.GetDateTimeFormatFromString(dataRow["RSVT_DT"].ToString());  // 예약일자
            purchageDateTimePicker.Value = Utils.GetDateTimeFormatFromString(dataRow["PRCS_DTM"].ToString());        // 구매일시
            Utils.SelectComboBoxItemByValue(cooperativeCompanyComboBox, dataRow["TKTR_CMPN_NO"].ToString());         // 모객업체번호 
            string CMPN_DVSN_CD = dataRow["COOP_CMPN_DVSN_CD"].ToString().Trim();


            //  ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶여기다가 직판 / 소셜일 경우에는  Voucher Button 활성화 아닐경우 비활성화          --> 박현호(고객요구사항 반영)
            //------------------------------------------------------------------------------ 여기서부터 
            if (CMPN_DVSN_CD == "10" || CMPN_DVSN_CD == "01")
            {
                voucherProcButton.Enabled = true;
                checkSocialCompany();
            }
            else
            {
                voucherProcButton.Enabled = false;
            }
            //------------------------------------------------------------------------------ 여기 까지

            string email = ORDE_EMAL_ADDR;                                                     // email 
            string[] emailTemp = email.Split('@');
            if (emailTemp.Length == 2)
            {
                emailIdTextBox.Text = emailTemp[0];

                //--------------------------------------------------------------------------
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

            if (dayComboBox.SelectedIndex != -1)                                                                     // 도착일자
                arrivalDateTimePicker.Value = departureDateTimePicker.Value.AddDays(Convert.ToInt32(dayComboBox.SelectedItem));
            else
                arrivalDateTimePicker.Value = departureDateTimePicker.Value;

            // 금액정보
            totalWonSalePriceTextBox.Text = Utils.SetComma(dataRow["WON_TOT_SALE_AMT"].ToString());  // 원화총판매금액

            double totalSaleAmount = double.Parse(dataRow["TOT_SALE_AMT"].ToString());
            totalSalePriceTextBox.Text = String.Format("{0:0,0.00}", totalSaleAmount);               // 외화판매합계금액 (성인가격*성인수+소아가격*소아수+유아가격*유아수)

            if (SALE_CUR_CD.Equals("KRW"))
            {
                salePriceSumTextBox.Text = Utils.SetComma(String.Format("{0:0}", totalSaleAmount));                 // 판매가영역-외화판매합계금액 (성인가격*성인수+소아가격*소아수+유아가격*유아수)
            }
            else
            {
                salePriceSumTextBox.Text = Utils.SetComma(String.Format("{0:0,0.00}", totalSaleAmount));                 // 판매가영역-외화판매합계금액 (성인가격*성인수+소아가격*소아수+유아가격*유아수)
            }

            totalDepositPriceTextBox.Text = Utils.SetComma(dataRow["TOT_RECT_AMT"].ToString());      // 입금총액

            depositPriceTextBox.Text = Utils.SetComma(dataRow["PRPY_AMT"].ToString());               // 선금
            reservationPriceTextBox.Text = Utils.SetComma(dataRow["RSVT_AMT"].ToString());           // 예약금액
            partPriceTextBox.Text = Utils.SetComma(dataRow["MDPY_AMT"].ToString());                  // 중도금금액 
            outstandingPriceTextBox.Text = Utils.SetComma(dataRow["UNAM_BAL"].ToString());           // 미수금잔액

            _numberOfAdult = Int32.Parse(dataRow["ADLT_NBR"].ToString());                            // 성인수를 변수에 보관
            _numberOfChild = Int32.Parse(dataRow["CHLD_NBR"].ToString());                            // 소아수를 변수에 보관
            _numberOfInfant = Int32.Parse(dataRow["INFN_NBR"].ToString());                           // 유아수를 변수에 보관


            adultPeopleCountTextBox.Text = dataRow["ADLT_NBR"].ToString();
            childPeopleCountTextBox.Text = dataRow["CHLD_NBR"].ToString();
            infantPeopleCountTextBox.Text = dataRow["INFN_NBR"].ToString();

            _numberOfPeople = _numberOfAdult + _numberOfChild + _numberOfInfant;

            totalNumberOfPersonTextBox.Text = _numberOfPeople.ToString();                            // 총인원수

            ////
            // 판매/원가
            // 판매가

            string ADLT_SALE_PRCE = dataRow["ADLT_SALE_PRCE"].ToString();
            string CHLD_SALE_PRCE = dataRow["CHLD_SALE_PRCE"].ToString();
            string INFN_SALE_PRCE = dataRow["INFN_SALE_PRCE"].ToString();

            Utils.SelectComboBoxItemByValue(saleCurrencyCodeComboBox, dataRow["SALE_CUR_CD"].ToString()); // 판매통화코드

            double adultSalesPrice = Double.Parse(ADLT_SALE_PRCE);          // 성인판매가격소계
            double childSalesPrice = Double.Parse(CHLD_SALE_PRCE);          // 소아판매가격소계
            double infantSalesPrice = Double.Parse(INFN_SALE_PRCE);         // 유아판매가격소계

            double adultSalesPriceSubTotal = adultSalesPrice * _numberOfAdult;
            double childSalesPriceSubTotal = childSalesPrice * _numberOfChild;
            double infantSalesPriceSubTotal = infantSalesPrice * _numberOfInfant;


            // 원가
            string CSPR_SUM_AMT = dataRow["CSPR_SUM_AMT"].ToString();
            //totalCostPriceAmountTextBox.Text = Utils.SetComma(CSPR_SUM_AMT);                                // 원가합계금액

            // 예약상태코드
            Utils.SelectComboBoxItemByValue(reservationStatusComboBox, dataRow["RSVT_STTS_CD"].ToString()); // 예약상태코드

            // 가정산금액
            settlementEstimationWonAmtTextBox.Text = Utils.SetComma(dataRow["STEM_WON_AMT"].ToString());

            // 정산금액
            settlementWonAmtTextBox.Text = Utils.SetComma(dataRow["STMT_WON_AMT"].ToString());

            // 고객 요청사항
            customerRequestMemoTextBox.Text = dataRow["CUST_RQST_CNTS"].ToString();                         // 고객요청사항

            // 내부 메모
            insideMemoTextBox.Text = dataRow["INTR_MEMO_CNTS"].ToString();                                  // 내부메모
            ////
            ///
            // VO 값 저장
            vOReservationDetail.CUST_NO = dataRow["CUST_NO"].ToString();                                             // 고객번호
            vOReservationDetail.CUST_NM = dataRow["CUST_NM"].ToString();                                             // 고객번호
            vOReservationDetail.PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();                                         // 상품일련번호
            vOReservationDetail.PRDT_GRAD_CD = dataRow["PRDT_GRAD_CD"].ToString();                                   // 상품등급
            vOReservationDetail.STMT_YN = dataRow["STMT_YN"].ToString();                                             // 정산여부
            vOReservationDetail.RSVT_DT = dataRow["RSVT_DT"].ToString();                                             // 예약일자
            vOReservationDetail.PRCS_DTM = dataRow["PRCS_DTM"].ToString();                                           // 구매일시
            vOReservationDetail.TKTR_CMPN_NO = dataRow["TKTR_CMPN_NO"].ToString();                                   // 모객업체번호
            vOReservationDetail.ORDE_CTIF_PHNE_NO = dataRow["ORDE_CTIF_PHNE_NO"].ToString();                         // 휴대폰
            vOReservationDetail.RPRS_ENG_NM = dataRow["RPRS_ENG_NM"].ToString();                                     // 대표자영문명
            vOReservationDetail.DPTR_DT = dataRow["DPTR_DT"].ToString();                                             // 출발일자
            vOReservationDetail.LGMT_DAYS = Int16.Parse(dataRow["LGMT_DAYS"].ToString());                            // 숙박일수
            vOReservationDetail.TOT_TRIP_DAYS = Int16.Parse(dataRow["TOT_TRIP_DAYS"].ToString());                    // 총여행일수
            vOReservationDetail.TOT_SALE_AMT = double.Parse(dataRow["TOT_SALE_AMT"].ToString());                     // 외화총판매금액
            vOReservationDetail.WON_TOT_SALE_AMT = double.Parse(dataRow["WON_TOT_SALE_AMT"].ToString());             // 원화총판매금액
            vOReservationDetail.SALE_SUM_AMT = double.Parse(dataRow["SALE_SUM_AMT"].ToString());                     // 판매합계금액 (성인가격*성인수+소아가격*소아수+유아가격*유아수)
            vOReservationDetail.PRPY_AMT = double.Parse(dataRow["PRPY_AMT"].ToString());                             // 선금
            vOReservationDetail.RSVT_AMT = double.Parse(dataRow["RSVT_AMT"].ToString());                             // 예약금액
            vOReservationDetail.MDPY_AMT = double.Parse(dataRow["MDPY_AMT"].ToString());                             // 중도금금액
            vOReservationDetail.UNAM_BAL = double.Parse(dataRow["UNAM_BAL"].ToString());                             // 미수금잔액
            vOReservationDetail.ADLT_NBR = Int16.Parse(dataRow["ADLT_NBR"].ToString());                              // 성인수
            vOReservationDetail.CHLD_NBR = Int16.Parse(dataRow["CHLD_NBR"].ToString());                              // 소아수
            vOReservationDetail.INFN_NBR = Int16.Parse(dataRow["INFN_NBR"].ToString());                              // 유아수
            vOReservationDetail.ADLT_SALE_PRCE = double.Parse(dataRow["ADLT_SALE_PRCE"].ToString());                 // 성인판매가
            vOReservationDetail.CHLD_SALE_PRCE = double.Parse(dataRow["CHLD_SALE_PRCE"].ToString());                 // 소아판매가
            vOReservationDetail.INFN_SALE_PRCE = double.Parse(dataRow["INFN_SALE_PRCE"].ToString());                 // 유아판매가
            vOReservationDetail.SALE_CUR_CD = dataRow["SALE_CUR_CD"].ToString();                                     // 판매통화코드
            vOReservationDetail.OPTN_SUM_AMT = double.Parse(dataRow["OPTN_SUM_AMT"].ToString());                     // 옵션합계금액
            vOReservationDetail.CSPR_SUM_AMT = double.Parse(dataRow["CSPR_SUM_AMT"].ToString());                     // 원가합계금액
            vOReservationDetail.STEM_WON_AMT = double.Parse(dataRow["STEM_WON_AMT"].ToString());                     // 가정산금액
            vOReservationDetail.STMT_WON_AMT = double.Parse(dataRow["STMT_WON_AMT"].ToString());                     // 정산금액
            vOReservationDetail.RSVT_STTS_CD = dataRow["RSVT_STTS_CD"].ToString();                                   // 예약상태코드
            vOReservationDetail.WON_PYMT_FEE = Double.Parse(dataRow["WON_PYMT_FEE"].ToString());                     // 원화지급수수료
            vOReservationDetail.STEM_WON_AMT = Double.Parse(dataRow["STEM_WON_AMT"].ToString());                     // 가정산금액
            vOReservationDetail.STMT_WON_AMT = Double.Parse(dataRow["STMT_WON_AMT"].ToString());                     // 정산금액
            vOReservationDetail.CUST_RQST_CNTS = dataRow["CUST_RQST_CNTS"].ToString();                               // 고객요청사항
            vOReservationDetail.INTR_MEMO_CNTS = dataRow["INTR_MEMO_CNTS"].ToString();                               // 내부메모

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

            // 위탁판매지급수수료
            vOReservationDetail.WON_PYMT_FEE = Double.Parse(dataRow["WON_PYMT_FEE"].ToString());

            if (vOReservationDetail.WON_PYMT_FEE > 0 )
            {
                _receivedFeeYn = true;
            } else
            {
                _receivedFeeYn = false;
            }

            consignmentSaleFeeTextBox.Text = Utils.SetComma(dataRow["WON_PYMT_FEE"].ToString());

            if (vOReservationDetail.WON_PYMT_FEE > 0) {
                redrawingConsignmentSaleFeeInputform(vOReservationDetail.WON_PYMT_FEE.ToString(), "수수료", 422, 42);
            }

            /// 수익금액 계산
            calcRevenue();

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
                }

                if (PSPT_CHCK_NEED_YN.Equals("Y"))         // 여권체크
                {
                    _cutOffList.Add("PSPT_CHCK_STTS_CD");
                }

                if (ARGM_CHCK_NEED_YN.Equals("Y"))         // 수배체크
                {
                    _cutOffList.Add("ARGM_CHCK_STTS_CD");
                }

                if (VOCH_CHCK_NEED_YN.Equals("Y"))         // 바우처확인
                {
                    _cutOffList.Add("VOCH_CHCK_STTS_CD");
                }

                if (ISRC_CHCK_NEED_YN.Equals("Y"))
                {
                    _cutOffList.Add("ISRC_CHCK_STTS_CD");
                }

                if (AVAT_CHCK_NEED_YN.Equals("Y"))
                {
                    _cutOffList.Add("AVAT_CHCK_STTS_CD");
                }

                if (PRSN_CHCK_NEED_YN.Equals("Y"))
                {
                    _cutOffList.Add("PRSN_CHCK_STTS_CD");
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

            cutOffCheckGroupBox.SuspendLayout();

            // 명단, 여권, 수배, 바우처 등 확인대상만 화면에 표시
            for (int kk = 0; kk < _cutOffList.Count; kk++)
            {
                // 컴포넌트 렌더링 위치 결정
                switch (kk)
                {
                    case 0:
                        xx1 = 14; yy1 = 460;    // 레이블 위치
                        xx2 = 76; yy2 = 460;    // 콤보박스 위치
                        xx3 = 163; yy3 = 460;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 460;    // 버튼 위치
                        break;
                    case 1:
                        xx1 = 14; yy1 = 495;    // 레이블 위치
                        xx2 = 76; yy2 = 495;    // 콤보박스 위치
                        xx3 = 163; yy3 = 495;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 495;    // 버튼 위치
                        break;
                    case 2:
                        xx1 = 14; yy1 = 530;   // 레이블 위치
                        xx2 = 76; yy2 = 530;   // 콤보박스 위치
                        xx3 = 163; yy3 = 530;   // DateTimePicker 위치
                        xx4 = 420; yy4 = 530;   // 버튼 위치
                        break;
                    case 3:
                        xx1 = 14; yy1 = 565;     // 레이블 위치
                        xx2 = 76; yy2 = 565;     // 콤보박스 위치
                        xx3 = 163; yy3 = 565;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 565;    // 버튼 위치
                        break;
                    case 4:
                        xx1 = 14; yy1 = 600;     // 레이블 위치
                        xx2 = 76; yy2 = 600;     // 콤보박스 위치
                        xx3 = 163; yy3 = 600;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 600;    // 버튼 위치
                        break;
                    case 5:
                        xx1 = 14; yy1 = 635;     // 레이블 위치
                        xx2 = 76; yy2 = 635;     // 콤보박스 위치
                        xx3 = 163; yy3 = 635;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 635;    // 버튼 위치
                        break;
                    case 6:
                        xx1 = 14; yy1 = 670;     // 레이블 위치
                        xx2 = 76; yy2 = 670;     // 콤보박스 위치
                        xx3 = 163; yy3 = 670;    // DateTimePicker 위치
                        xx4 = 420; yy4 = 670;    // 버튼 위치
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

            cutOffCheckGroupBox.ResumeLayout();
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
            label.BringToFront();
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
            comboBox.BringToFront();
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
            dateTimePicker.BringToFront();
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
            button.BringToFront();
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
            comboBox.BringToFront();
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
            dateTimePicker.BringToFront();
            dateTimePicker.Refresh();
        }

        //=========================================================================================================================================================================
        // 저장된 예약판매가격정보를 입력필드에 Set.
        //=========================================================================================================================================================================
        private void setReservationSalePriceList()
        {
            salePriceFlowLayoutPanel.SuspendLayout();

            clearSalePriceList();

            string query = string.Format("CALL SelectRsvtSalePriceList ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            // 등록된 판매가격의 건수를 보관
            _salePriceArrayCount = dataRowList.Count;

            // 기등록된 판매가격이 하나도 없으면 빈값을 갖는 판매가격 행을 한개 생성
            if (_salePriceArrayCount == 0)
            {
                setCreateDefaultSalePriceInfoLine();
                salePriceFlowLayoutPanel.ResumeLayout();
                return;
            }

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약판매가격정보를 가져올 수 없습니다.");
                return;
            }

            bool firstStep = true;
            int arrayRow = 0;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CNMB = dataRow["CNMB"].ToString();                                                                       // 일련번호
                string SALE_PRCE_NM = dataRow["SALE_PRCE_NM"].ToString();                                                       // 판매가격명
                string ADLT_DVSN_CD = dataRow["ADLT_DVSN_CD"].ToString();                                                       // 성인구분코드
                string ADLT_DVSN_NM = dataRow["ADLT_DVSN_NM"].ToString();                                                       // 성인구분명
                string CUR_CD = dataRow["SALE_CUR_CD"].ToString();                                                              // 통화코드
                string SALE_UTPR = Utils.SetComma(dataRow["SALE_UTPR"].ToString());                                             // 판매단가
                string WON_SALE_UTPR = Utils.SetComma(dataRow["WON_SALE_UTPR"].ToString());                                     // 원화판매단가
                string APLY_EXRT = Utils.SetComma(dataRow["APLY_EXRT"].ToString());                                             // 적용환율
                string NMPS_NBR = dataRow["NMPS_NBR"].ToString();                                                               // 인원수

                //double salePriceSubtotal = Convert.ToInt32(Double.Parse(SALE_UTPR) * int.Parse(NMPS_NBR));          // 판매소계금액
                double salePriceSubtotal = (Double.Parse(SALE_UTPR) * int.Parse(NMPS_NBR));          // 판매소계금액
                double wonSalePriceSubtotal = Convert.ToInt32(Double.Parse(WON_SALE_UTPR) * int.Parse(NMPS_NBR));   // 판매소계금액(원화)

                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                // 판매가격 객체 동적 생성 (추가버튼, 판매금액명, 성인구분, 판매단가, 인원수, 판매금액소계)
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                if (firstStep == true)
                {
                    _salePriceLastYPoint = _salePriceYPoint;
                    firstStep = false;
                }
                else
                {
                    _salePriceLastYPoint = _salePriceLastYPoint + 35;
                }

                arrayRow++;

                this.salePriceFlowLayoutPanel.HorizontalScroll.Maximum = 0;
                this.salePriceFlowLayoutPanel.HorizontalScroll.Visible = false;
                this.salePriceFlowLayoutPanel.AutoScroll = false;
                this.salePriceFlowLayoutPanel.WrapContents = false;
                this.salePriceFlowLayoutPanel.VerticalScroll.Visible = false;
                this.salePriceFlowLayoutPanel.AutoScroll = true;
                this.salePriceFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;

                // FlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
                setSalePriceFlowLayoutPanel();

                // 추가버튼 (+)
                createSalePriceAddButton(arrayRow, _salePriceXPoint1, _salePriceLastYPoint);

                // 판매가격일련번호 list
                _salePriceNoStringArray.Add(CNMB);

                // 판매가격명 텍스트박스
                createSalePriceNameTextBox(arrayRow, _salePriceXPoint2, _salePriceLastYPoint, SALE_PRCE_NM);

                // 성인구분 콤보박스
                createSaleAdultDivisionCodeComboBox(_salePriceArrayCount, _salePriceXPoint3, _salePriceYPoint, ADLT_DVSN_CD);

                // 통화코드 콤보박스
                createSaleCurrencyCodeComboBox(_salePriceArrayCount, _salePriceXPoint4, _salePriceYPoint, CUR_CD);

                // 판매가격 텍스트박스
                createSalePriceTextBox(arrayRow, _salePriceXPoint5, _salePriceLastYPoint, SALE_UTPR);

                // 환율 텍스트박스
                createSaleApplyExchangeRateTextBox(arrayRow, _salePriceXPoint5, _salePriceLastYPoint, APLY_EXRT);

                // 원화판매가격 텍스트박스
                createWonSalePriceTextBox(arrayRow, _salePriceXPoint6, _salePriceLastYPoint, WON_SALE_UTPR);

                // 인원 텍스트박스
                createSalePricePeopleCountTextBox(arrayRow, _salePriceXPoint7, _salePriceLastYPoint, NMPS_NBR);

                // 판매가격소계(외화) 텍스트박스
                createSalePriceSumAmountTextBox(arrayRow, _salePriceXPoint8, _salePriceLastYPoint, Utils.SetComma(salePriceSubtotal.ToString()));

                // 원화판매가격소계(₩) 텍스트박스
                createWonSalePriceSumAmountTextBox(arrayRow, _salePriceXPoint9, _salePriceLastYPoint, Utils.SetComma(wonSalePriceSubtotal.ToString()));

                // 삭제버튼 (-)
                createSalePriceErasebuttonButton(arrayRow, _salePriceXPoint10, _salePriceLastYPoint);

                salePriceFlowLayoutPanel.Controls.Add(fl);
            }

            salePriceFlowLayoutPanel.ResumeLayout();
        }

        private void setSalePriceFlowLayoutPanel()
        {
            fl = new FlowLayoutPanel();
            fl.FlowDirection = FlowDirection.LeftToRight;
            fl.Name = $"fl{_salePriceArrayCount}";
            fl.Size = new Size(1259, 32);
            fl.BorderStyle = BorderStyle.None;
            fl_salePrice_list.Add(fl);
        }

        //=========================================================================================================================================================================
        // 예약판매가격정보가 없는 예약건은 옵션 행 1라인을 기본으로 생성
        //=========================================================================================================================================================================
        private void setCreateDefaultSalePriceInfoLine()
        {
            _salePriceArrayCount = 1;

            _salePriceLastYPoint = _salePriceYPoint;

            this.salePriceFlowLayoutPanel.HorizontalScroll.Maximum = 0;
            this.salePriceFlowLayoutPanel.HorizontalScroll.Visible = false;
            this.salePriceFlowLayoutPanel.AutoScroll = false;
            this.salePriceFlowLayoutPanel.WrapContents = false;
            this.salePriceFlowLayoutPanel.VerticalScroll.Visible = false;
            this.salePriceFlowLayoutPanel.AutoScroll = true;
            this.salePriceFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;

            // FlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setSalePriceFlowLayoutPanel();

            // 추가버튼 (+)
            createSalePriceAddButton(0, _salePriceXPoint1, _salePriceYPoint);

            // 판매가격일련번호 list
            _salePriceNoStringArray.Add("");

            // 판매가격명 텍스트박스
            createSalePriceNameTextBox(_salePriceArrayCount, _salePriceXPoint2, _salePriceYPoint, "");

            // 성인구분 콤보박스
            createSaleAdultDivisionCodeComboBox(_salePriceArrayCount, _salePriceXPoint3, _salePriceYPoint, "");

            // 통화코드 콤보박스
            createSaleCurrencyCodeComboBox(_salePriceArrayCount, _salePriceXPoint4, _salePriceYPoint, "");

            // 판매가격 텍스트박스
            createSalePriceTextBox(_salePriceArrayCount, _salePriceXPoint4, _salePriceYPoint, "");

            // 환율 텍스트박스
            createSaleApplyExchangeRateTextBox(_salePriceArrayCount, _salePriceXPoint5, _salePriceLastYPoint, "");

            // 원화판매가격 텍스트박스
            createWonSalePriceTextBox(_salePriceArrayCount, _salePriceXPoint6, _salePriceLastYPoint, "");

            // 인원수 텍스트박스
            createSalePricePeopleCountTextBox(_salePriceArrayCount, _salePriceXPoint5, _salePriceYPoint, "");

            // 판매가격소계 텍스트박스
            createSalePriceSumAmountTextBox(_salePriceArrayCount, _salePriceXPoint6, _salePriceYPoint, "");

            // 원화판매가격소계 텍스트박스
            createWonSalePriceSumAmountTextBox(_salePriceArrayCount, _salePriceXPoint9, _salePriceLastYPoint, "");

            // 판매가격 행삭제 버튼
            createSalePriceErasebuttonButton(_salePriceArrayCount, _salePriceXPoint7, _salePriceYPoint);

            salePriceFlowLayoutPanel.Controls.Add(fl);
        }

        //=========================================================================================================================================================================
        // 판매가격 동적객체 생성용 리스트 초기화
        //=========================================================================================================================================================================
        private void clearSalePriceList()
        {
            salePriceFlowLayoutPanel.Controls.Clear();

            _salePriceArrayCount = 0;                          
            _salePriceAddButtonArray.Clear();                                            // 행추가 버튼
            _salePriceNoStringArray.Clear();                                             // 일련번호
            _salePriceNameTextBoxArray.Clear();                                      // 판매가격명
            _saleAdultDivisionCodeComboBoxArray.Clear();                       // 성인구분코드
            _saleSaleCurrencyCodeComboBoxArray.Clear();                       // 판매통화코드
            _salePriceTextBoxArray.Clear();                                               // 판매단가
            _saleApplyExchangeRateTextBoxArray.Clear();                         // 적용환율
            _saleWonPriceTextBoxArray.Clear();                                        // 원화판매단가
            _salePricePeopleCountTextBoxArray.Clear();                            // 인원수
            _salePriceSumAmountTextBoxArray.Clear();                            // 판매가격소계
            _saleWonPriceSumAmountTextBoxArray.Clear();                      // 원화판매가격소계
            _salePriceEraseButtonOptionArray.Clear();                               // 행삭제
        }

        //=========================================================================================================================================================================
        // 판매가격 행추가 버튼 생성
        //=========================================================================================================================================================================
        private void createSalePriceAddButton(int arrayRow, int xPoint, int yPoint)
        {
            Button button = new Button();
            button.Location = new Point(xPoint, yPoint);
            button.Text = "+";
            button.Size = new Size(24, 29);
            button.Tag = arrayRow;
            button.Click += new EventHandler(addSalePriceInfoLineButton_Click);
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.BringToFront();

            fl.Controls.Add(button);
            _salePriceAddButtonArray.Add(button);
        }

        //=========================================================================================================================================================================
        // 판매가격 성인구분 콤보박스 생성
        //=========================================================================================================================================================================
        private void createSaleAdultDivisionCodeComboBox(int arrayRow, int xPoint, int yPoint, string adultDivisionCode)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(xPoint, yPoint);
            comboBox.Size = new Size(81, 29);
            comboBox.Tag = arrayRow;
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            comboBox.BringToFront();

            List<CommonCodeItem> list = Global.GetCommonCodeList("ADLT_DVSN_CD");

            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                comboBox.Items.Add(item);
            }

            if (comboBox.Items.Count > 0 && adultDivisionCode != "")
            {
                Utils.SelectComboBoxItemByValue(comboBox, adultDivisionCode);
            }

            fl.Controls.Add(comboBox);
            _saleAdultDivisionCodeComboBoxArray.Add(comboBox);
        }

        //=========================================================================================================================================================================
        // 통화코드 콤보박스 생성
        //=========================================================================================================================================================================
        private void createSaleCurrencyCodeComboBox(int arrayRow, int xPoint, int yPoint, string currencyCode)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(xPoint, yPoint);
            comboBox.Size = new Size(135, 29);
            comboBox.Tag = arrayRow;
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            comboBox.LostFocus += new EventHandler(checkSalePriceCurrencyCode);
            comboBox.SelectedIndexChanged += new EventHandler(changedSalePriceCurrencyCode);
            comboBox.BringToFront();

            comboBox.Items.AddRange(_currencyCodeComboBox.Items.Cast<Object>().ToArray());

            if (comboBox.Items.Count > 0)
            {
                if (currencyCode != "")
                {
                    Utils.SelectComboBoxItemByValue(comboBox, currencyCode);
                }
                else
                {
                    comboBox.SelectedIndex = -1;
                }
            }

            fl.Controls.Add(comboBox);
            _saleSaleCurrencyCodeComboBoxArray.Add(comboBox);
        }

        //=========================================================================================================================================================================
        // 판매통화코드 이벤트 처리
        //=========================================================================================================================================================================
        private void checkSalePriceCurrencyCode(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            int tag = (int)comboBox.Tag;

            string CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[tag - 1]);
            
            if (CUR_CD.Equals("KRW"))
            {
                _saleApplyExchangeRateTextBoxArray[tag - 1].Text = "0";
                _saleApplyExchangeRateTextBoxArray[tag - 1].ReadOnly = true;
            }
            else
            {
                _saleApplyExchangeRateTextBoxArray[tag - 1].ReadOnly = false;
                _saleApplyExchangeRateTextBoxArray[tag - 1].Focus();
            }
        }

        //=========================================================================================================================================================================
        // 판매통화코드 이벤트 처리 - 인덱스변화 감지
        //=========================================================================================================================================================================
        private void changedSalePriceCurrencyCode(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            int tag = (int)comboBox.Tag;

            if (_saleSaleCurrencyCodeComboBoxArray.Count < tag)
                return;

            string CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[tag - 1]);

            if (CUR_CD.Equals("KRW"))
            {
                _saleApplyExchangeRateTextBoxArray[tag - 1].Text = "0";
                _saleApplyExchangeRateTextBoxArray[tag - 1].ReadOnly = true;
                _salePriceTextBoxArray[tag - 1].Text = "0";
                _salePriceTextBoxArray[tag - 1].ReadOnly = true;
                _saleWonPriceTextBoxArray[tag - 1].ReadOnly = false;
                _salePriceSumAmountTextBoxArray[tag - 1].Text = "0";
            }
            else
            {
                _saleApplyExchangeRateTextBoxArray[tag - 1].ReadOnly = false;
                _saleApplyExchangeRateTextBoxArray[tag - 1].Focus();
                _salePriceTextBoxArray[tag - 1].ReadOnly = false;
                _saleWonPriceTextBoxArray[tag - 1].Text = "0";
                _saleWonPriceTextBoxArray[tag - 1].ReadOnly = true;
            }
        }

        //=========================================================================================================================================================================
        // 판매가격명 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createSalePriceNameTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            textBox.Text = value;
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Left;
            textBox.Size = new Size(250, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            fl.Controls.Add(textBox);
            _salePriceNameTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 판매단가 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createSalePriceTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);

            if (value != "" && value != null)
            {
                double doubleValue = double.Parse(value);
                string stringValue = String.Format("{0:0,0.00}", doubleValue);
                textBox.Text = stringValue;
            }
            else
            {
                textBox.Text = "0";
            }
            
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.LostFocus += new EventHandler(calcSalePriceSumAmount);
            textBox.Size = new Size(110, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            string CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[arrayRow - 1]);

            if (CUR_CD.Equals("KRW"))
            {
                textBox.ReadOnly = true;
                textBox.Text = "0";
            }
            else
            {
                textBox.ReadOnly = false;
            }

            fl.Controls.Add(textBox);
            _salePriceTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 환율 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createSaleApplyExchangeRateTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            textBox.Text = value;
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.LostFocus += new EventHandler(calcSalePriceSumAmount);
            textBox.Size = new Size(100, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();


            string CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[arrayRow - 1]);

            if (CUR_CD.Equals("KRW"))
            {
                textBox.ReadOnly = true;
            }
            else
            {
                textBox.ReadOnly = false;
            }

            fl.Controls.Add(textBox);
            _saleApplyExchangeRateTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 원화판매단가 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createWonSalePriceTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            textBox.Text = value;
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.LostFocus += new EventHandler(calcSalePriceSumAmount);
            textBox.Size = new Size(110, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            /**
             * 원화 : 판매단가 x 인원수 => 판매소계 로직임으로 ReadOnly
             * 외화 : 판매단가 x 환율   => 원화판매단가 로직임으로 ReadOnly
             * */
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[arrayRow - 1]);

            if (CUR_CD.Equals("KRW"))
            {
                textBox.ReadOnly = false;
            }
            else
            {
                textBox.ReadOnly = true;
            }

            fl.Controls.Add(textBox);
            _saleWonPriceTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 판매가격 인원수 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createSalePricePeopleCountTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            textBox.Text = value;
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.LostFocus += new EventHandler(calcSalePriceSumAmount);
            textBox.Size = new Size(60, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            fl.Controls.Add(textBox);
            _salePricePeopleCountTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 판매가격 소계 텍스트박스 생성  -- 소계(외화)
        //=========================================================================================================================================================================
        private void createSalePriceSumAmountTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[arrayRow - 1]);

            if (value != "" && value != null)
            {
                double doubleValue = double.Parse(value);
                string stringValue = String.Format("{0:0,0.00}", doubleValue);
                textBox.Text = stringValue;
            }
            else
            {
                textBox.Text = "0";
            }

            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.Size = new Size(110, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            /**
             * 원화, 외화 둘다 자동계산영역임으로 ReadOnly 처리
             * */
            textBox.ReadOnly = true;
          


            if (CUR_CD.Equals("KRW"))
            {
                textBox.Text = "0";
            }

            fl.Controls.Add(textBox);
            _salePriceSumAmountTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 원화판매가격 소계 텍스트박스 생성               -- 소계(₩)
        //=========================================================================================================================================================================
        private void createWonSalePriceSumAmountTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            textBox.Text = value;
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.Size = new Size(110, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();
            textBox.Font = new Font(textBox.Font, FontStyle.Bold);

            /**
             * 원화, 외화 둘다 자동계산영역임으로 ReadOnly 처리
             * */
            textBox.ReadOnly = true;

            fl.Controls.Add(textBox);
            _saleWonPriceSumAmountTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 판매가격소계 이벤트 처리
        //=========================================================================================================================================================================
        private void calcSalePriceSumAmount(object sender, EventArgs e)
        {
            // 버튼 이벤트 정의 (판매가격)
            TextBox textBox = sender as TextBox;
            int tag = (int)textBox.Tag;

            if (_salePriceTextBoxArray[tag - 1].Text.Trim().Equals("")) _salePriceTextBoxArray[tag - 1].Text = "0";
            if (_salePricePeopleCountTextBoxArray[tag - 1].Text.Trim().Equals("")) _salePricePeopleCountTextBoxArray[tag - 1].Text = "0";
            if (_saleApplyExchangeRateTextBoxArray[tag - 1].Text.Trim().Equals("")) _saleApplyExchangeRateTextBoxArray[tag - 1].Text = "0";
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[tag - 1]);

            double salePrice = Utils.GetDoubleValue(_salePriceTextBoxArray[tag - 1].Text);
            double saleWonPrice = Utils.GetDoubleValue(_saleWonPriceTextBoxArray[tag - 1].Text);
            double applyExchangeRate = Utils.GetDoubleValue(_saleApplyExchangeRateTextBoxArray[tag - 1].Text);
            int salePricePeopleCount = Utils.GetInteger32Value(_salePricePeopleCountTextBoxArray[tag - 1].Text);
            double salePriceSumAmount = 0;
            double wonSalePriceAmount = 0;
            double wonSalePriceSumAmount = 0;

            // 환율이 입력되었으면 원화판매단가 계산
            if (salePrice > 0)
            {
                // 원화판매단가 계산: 원화통화이면 원화가격을 그대로 표시
                if (CUR_CD.Equals("KRW"))
                {
                    wonSalePriceAmount = saleWonPrice;
                    _saleWonPriceTextBoxArray[tag - 1].Text = Utils.SetComma(wonSalePriceAmount.ToString());
                }
                else if (!CUR_CD.Equals("KRW") && applyExchangeRate > 0)
                {
                    wonSalePriceAmount = Math.Round(salePrice * applyExchangeRate, 2);
                    _saleWonPriceTextBoxArray[tag - 1].Text = Utils.SetComma(wonSalePriceAmount.ToString());
                }
            }

            // 판매가격소계/원화판매가격소계 계산
            if (salePrice > 0 && salePricePeopleCount > 0)
            {
                // 판매가격소계
                salePriceSumAmount = Math.Round(salePrice * salePricePeopleCount, 2);
                _salePriceSumAmountTextBoxArray[tag - 1].Text = String.Format("{0:0,0.00}", salePriceSumAmount);

                // 원화판매가격소계
                if (CUR_CD.Equals("KRW"))
                {
                    wonSalePriceSumAmount = saleWonPrice;
                    _saleWonPriceSumAmountTextBoxArray[tag - 1].Text = Utils.SetComma(wonSalePriceSumAmount.ToString());
                }
                else if (!CUR_CD.Equals("KRW") && applyExchangeRate > 0)
                {
                    wonSalePriceSumAmount = Math.Round((salePrice * applyExchangeRate * salePricePeopleCount), 2);
                    _saleWonPriceSumAmountTextBoxArray[tag - 1].Text = Utils.SetComma(wonSalePriceSumAmount.ToString());
                }
            }

            /**
             * 19. 11. 6(수) 원화계산
             * */
            if (saleWonPrice > 0 && salePricePeopleCount > 0)
            {
                salePriceSumAmount = saleWonPrice * salePricePeopleCount;
                //_salePriceSumAmountTextBoxArray[tag - 1].Text = String.Format("{0:0}", salePriceSumAmount);
                _saleWonPriceSumAmountTextBoxArray[tag - 1].Text = Utils.SetComma(salePriceSumAmount.ToString());
            }
        }

        //=========================================================================================================================================================================
        // 판매가격 행삭제 버튼 생성
        //=========================================================================================================================================================================
        private void createSalePriceErasebuttonButton(int arrayRow, int xPoint, int yPoint)
        {
            Button button = new Button();
            button.Location = new Point(xPoint, yPoint);
            button.Text = "-";
            button.Size = new Size(24, 29);
            button.Tag = arrayRow;
            button.Click += new EventHandler(erasePriceInfoLineButton_Click);
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.BringToFront();

            fl.Controls.Add(button);
            _salePriceEraseButtonOptionArray.Add(button);
        }

        //=========================================================================================================================================================================
        // 판매가격 행추가 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void addSalePriceInfoLineButton_Click(object sender, EventArgs e)
        {
            salePriceFlowLayoutPanel.SuspendLayout();

            int _salePriceArrayCount = _salePriceAddButtonArray.Count;
            _salePriceArrayCount++;

            // 버튼 이벤트 정의
            Button button = sender as Button;
            int tag = (int)button.Tag;

            _salePriceLastYPoint = _salePriceLastYPoint + 35;

            // FlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setSalePriceFlowLayoutPanel();

            // 추가버튼 (+)
            createSalePriceAddButton(_salePriceArrayCount, _salePriceXPoint1, _salePriceLastYPoint);

            // 판매가격일련번호 list
            _salePriceNoStringArray.Add("");

            // 판매가격명 텍스트박스
            createSalePriceNameTextBox(_salePriceArrayCount, _salePriceXPoint2, _salePriceLastYPoint, "");

            // 성인구분 콤보박스
            createSaleAdultDivisionCodeComboBox(_salePriceArrayCount, _salePriceXPoint3, _salePriceLastYPoint, "");

            // 통화코드 콤보박스
            createSaleCurrencyCodeComboBox(_salePriceArrayCount, _salePriceXPoint4, _salePriceYPoint, "");

            // 판매가격 텍스트박스
            createSalePriceTextBox(_salePriceArrayCount, _salePriceXPoint4, _salePriceLastYPoint, "");

            // 환율 텍스트박스
            createSaleApplyExchangeRateTextBox(_salePriceArrayCount, _salePriceXPoint5, _salePriceLastYPoint, "0");

            // 원화판매가격 텍스트박스
            createWonSalePriceTextBox(_salePriceArrayCount, _salePriceXPoint6, _salePriceLastYPoint, "0");

            // 인원수 텍스트박스
            createSalePricePeopleCountTextBox(_salePriceArrayCount, _salePriceXPoint5, _salePriceLastYPoint, "");

            // 판매가격소계 텍스트박스
            createSalePriceSumAmountTextBox(_salePriceArrayCount, _salePriceXPoint6, _salePriceLastYPoint, "");

            // 원화판매가격소계 텍스트박스
            createWonSalePriceSumAmountTextBox(_salePriceArrayCount, _salePriceXPoint9, _salePriceLastYPoint, "");

            // 판매가격 행삭제 버튼
            createSalePriceErasebuttonButton(_salePriceArrayCount, _salePriceXPoint7, _salePriceLastYPoint);

            fl_salePrice_list.Add(fl);

            salePriceFlowLayoutPanel.Controls.Add(fl);
            salePriceFlowLayoutPanel.ResumeLayout();
        }

        //=========================================================================================================================================================================
        // 판매가격 행삭제 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void erasePriceInfoLineButton_Click(object sender, EventArgs e)
        {
            if (_salePriceArrayCount <= 0) return;

            // 버튼 이벤트 정의
            Button button = sender as Button;
            int tag = (int)button.Tag;
            if (tag <= 0) return;
            if (_salePriceNoStringArray[tag - 1] == null)
                return;

            int arrayCount = _salePriceAddButtonArray.Count;

            // 이전 행을 지우려고 하면 무시
            if (arrayCount > tag) {
                arrayCount = tag;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 해당 행의 판매가격을 삭제 (판매가격일련번호가 있는 경우에만 삭제)
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (_salePriceNoStringArray[tag - 1] != "")
            {
                salePriceFlowLayoutPanel.SuspendLayout();

                string[] queryStringArray = new string[1];           // sql 배열
                string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

                string CNMB = _salePriceNoStringArray[tag - 1];

                string query = string.Format("CALL DeleteRsvtSalePriceItem ('{0}','{1}', '{2}')",
                                _reservationNumber, CNMB, Global.loginInfo.ACNT_ID);

                queryStringArray[0] = query;
                var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
                int retVal = queryResult.Item1;
                queryResultArray = queryResult.Item2;

                if (retVal == -1)
                {
                    MessageBox.Show("판매가격을 삭제할 수 없습니다.");
                    return;
                }
            }



            // 첫번째 판매가격 입력행의 삭제버튼을 누르면 값만 지움
            if (tag == 1 && arrayCount == 1)
            {
                _salePriceNoStringArray[0] = "";                                         // 일련번호
                _salePriceNameTextBoxArray[0].Text = "";                                 // 판매가격명
                _saleAdultDivisionCodeComboBoxArray[0].SelectedIndex = -1;               // 성인구분코드
                _saleSaleCurrencyCodeComboBoxArray[0].SelectedIndex = -1;                // 판매통화코드
                _salePriceTextBoxArray[0].Text = "";                                     // 판매단가
                _saleApplyExchangeRateTextBoxArray[0].Text = "";                         // 적용환율
                _saleWonPriceTextBoxArray[0].Text = "";                                  // 원화판매단가
                _salePricePeopleCountTextBoxArray[0].Text = "";                          // 인원수
                _salePriceSumAmountTextBoxArray[0].Text = "";                            // 판매가격소계
                _saleWonPriceSumAmountTextBoxArray[0].Text = "";                         // 원화판매가격소계

                salePriceFlowLayoutPanel.ResumeLayout();
                return;
            }



            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 판매가격 객체 제거
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 최종 y좌표 감소
            _salePriceLastYPoint = _salePriceLastYPoint - 35;

            // 배열크기 감소
            arrayCount--;

            FlowLayoutPanel fl = fl_salePrice_list[arrayCount];
            //ComboBox comboBox = null;
            //TextBox textBox = null;

            // 행추가버튼
            //button = _salePriceAddButtonArray[tag - 1];
            //fl.Controls.Remove(button);

            //// 성인구분코드
            //comboBox = _saleAdultDivisionCodeComboBoxArray[tag - 1];
            //fl.Controls.Remove(comboBox);

            //// 판매가격명
            //textBox = _salePriceNameTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 판매통화코드
            //comboBox = _saleSaleCurrencyCodeComboBoxArray[tag - 1];
            //salePriceFlowLayoutPanel.Controls.Remove(comboBox);
            //fl.Controls.Remove(comboBox);

            //// 판매단가
            //textBox = _salePriceTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 적용환율
            //textBox = _saleApplyExchangeRateTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 원화판매단가
            //textBox = _saleWonPriceTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 인원수
            //textBox = _salePricePeopleCountTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 판매가격소계
            //textBox = _salePriceSumAmountTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 원화판매가격소계
            //textBox = _saleWonPriceSumAmountTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 행삭제버튼
            //button = _salePriceEraseButtonOptionArray[tag - 1];
            //fl.Controls.Remove(button);

            _salePriceAddButtonArray.RemoveAt(arrayCount);                     // 행추가 버튼
            _salePriceNoStringArray.RemoveAt(arrayCount);                      // 일련번호
            _salePriceNameTextBoxArray.RemoveAt(arrayCount);                   // 판매가격명 텍스트박스
            _saleAdultDivisionCodeComboBoxArray.RemoveAt(arrayCount);          // 성인구분코드 콤보박스
            _saleSaleCurrencyCodeComboBoxArray.RemoveAt(arrayCount);           // 판매통화코드 콤보박스
            _salePriceTextBoxArray.RemoveAt(arrayCount);                       // 판매단가 텍스트박스
            _saleApplyExchangeRateTextBoxArray.RemoveAt(arrayCount);           // 적용환율 텍스트박스
            _saleWonPriceTextBoxArray.RemoveAt(arrayCount);                    // 원화판매단가 텍스트박스
            _salePricePeopleCountTextBoxArray.RemoveAt(arrayCount);            // 인원수 텍스트박스
            _salePriceSumAmountTextBoxArray.RemoveAt(arrayCount);              // 판매가격소계 텍스트박스
            _saleWonPriceSumAmountTextBoxArray.RemoveAt(arrayCount);           // 원화판매가격소계 텍스트박스
            _salePriceEraseButtonOptionArray.RemoveAt(arrayCount);             // 행삭제 버튼

            fl_salePrice_list.RemoveAt(arrayCount);
            salePriceFlowLayoutPanel.Controls.RemoveAt(arrayCount);
            // salePriceFlowLayoutPanel.Controls.Remove(fl)

            foreach (Button btn in _salePriceEraseButtonOptionArray)
            {
                if ((int)btn.Tag > tag)
                    btn.Tag = (int)btn.Tag - 1;
            }

            salePriceFlowLayoutPanel.ResumeLayout();
        }


        //=========================================================================================================================================================================
        // [판매가격 기본값 설정] 상품기본가격 정보를 예약판매가격내역으로 Insert
        //=========================================================================================================================================================================
        private void createDefaultReservationSalePriceInfo()
        {
            _numberOfAdult = Int16.Parse(adultPeopleCountTextBox.Text);
            _numberOfChild = Int16.Parse(childPeopleCountTextBox.Text);
            _numberOfInfant = Int16.Parse(infantPeopleCountTextBox.Text);

            string DPTR_DT = departureDateTimePicker.Value.ToString("yyyy-MM-dd");
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(saleCurrencyCodeComboBox);

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = string.Format("CALL InsertRsvtSalePriceDefaultValue ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                            _reservationNumber, _productNo, _productGradeCode, CUR_CD, DPTR_DT, DPTR_DT, _numberOfAdult, _numberOfChild, _numberOfInfant, Global.loginInfo.ACNT_ID);

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("기본판매가격정보를 설정할 수 없습니다.");
                return;
            }
        }

        //=========================================================================================================================================================================
        // 저장된 예약원가정보를 입력필드에 Set.
        //=========================================================================================================================================================================
        private void setReservationCostPriceList()
        {
            costPriceFlowLayoutPanel.SuspendLayout();

            clearCostPriceList();

            double costPriceSum = 0;

            string query = string.Format("CALL SelectRsvtCsprList ( '{0}' )", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            // 등록된 원가의 건수를 보관
            _costPriceArrayCount = dataRowList.Count;

            // 기등록된 원가정보가 하나도 없으면 빈값을 갖는 판매가격 행을 한개 생성
            if (_costPriceArrayCount == 0)
            {
                setCreateDefaultCostPriceInfoLine();
                costPriceFlowLayoutPanel.ResumeLayout();
                return;
            }

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약원가정보를 가져올 수 없습니다.");
                return;
            }

            bool firstStep = true;
            int arrayRow = 0;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CSPR_CNMB = Utils.GetString(dataRow["CSPR_CNMB"]);                   // 원가일련번호
                string RSVT_CSPR_CNMB = Utils.GetString(dataRow["RSVT_CSPR_CNMB"]);         // 예약원가일련번호
                string CSPR_NM = Utils.GetString(dataRow["CSPR_NM"]);                       // 원가명
                string ARPL_CMPN_NO = Utils.GetString(dataRow["ARPL_CMPN_NO"]);             // 수배처코드
                string ARPL_CMPN_NM = Utils.GetString(dataRow["ARPL_CMPN_NM"]);             // 수배처명 
                string CSPR_CUR_CD = Utils.GetString(dataRow["CSPR_CUR_CD"]);               // 통화코드
                string CSPR_CUR_NM = Utils.GetString(dataRow["CSPR_CUR_NM"]);               // 통화코드
                string CSPR_AMT = Utils.SetComma(dataRow["CSPR_AMT"].ToString());           // 원가금액
                string NMPS_NBR = Utils.GetString(dataRow["NMPS_NBR"]);                     // 인원수
                string ADLT_DVSN_CD = Utils.GetString(dataRow["ADLT_DVSN_CD"]);             // 성인구분코드
                string ADLT_DVSN_NM = Utils.GetString(dataRow["ADLT_DVSN_NM"]);             // 성인구분명

                double costPriceSubtotal = Math.Round(Double.Parse(CSPR_AMT) * int.Parse(NMPS_NBR), 2);  // 원가소계금액
                costPriceSum = costPriceSum + costPriceSubtotal;

                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                // 원가 객체 동적 생성 (추가버튼, 원가명, 수배처, 성인구분, 원가, 인원수, 원가소계)
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                if (firstStep == true)
                {
                    _costPriceLastYPoint = _costPriceYPoint;
                    firstStep = false;
                }
                else
                {
                    _costPriceLastYPoint = _costPriceLastYPoint + 35;
                }

                arrayRow++;

                this.costPriceFlowLayoutPanel.HorizontalScroll.Maximum = 0;
                this.costPriceFlowLayoutPanel.HorizontalScroll.Visible = false;
                this.costPriceFlowLayoutPanel.AutoScroll = false;
                this.costPriceFlowLayoutPanel.WrapContents = false;
                this.costPriceFlowLayoutPanel.VerticalScroll.Visible = false;
                this.costPriceFlowLayoutPanel.AutoScroll = true;
                this.costPriceFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;

                // FlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
                setCostPriceFlowLayoutPanel();

                // 추가버튼 (+)
                createCostPriceAddButton(arrayRow, _costPriceXPoint1, _costPriceLastYPoint);

                // 원가일련번호 list
                _costPriceNoStringArray.Add(CSPR_CNMB);

                // 예약원가일련번호 list
                _costPriceReservationNoStringArray.Add(RSVT_CSPR_CNMB);

                // 원가명 텍스트박스
                createCostPriceNameTextBox(arrayRow, _costPriceXPoint2, _costPriceLastYPoint, CSPR_NM);

                // 원가통화코드 콤보박스
                createCostPriceCurrencyCodeComboBox(arrayRow, _costPriceXPoint3, _costPriceYPoint, CSPR_CUR_CD);

                // 성인구분 콤보박스
                createCostPriceAdultDivisionCodeComboBox(arrayRow, _costPriceXPoint3, _costPriceYPoint, ADLT_DVSN_CD);

                // 원가 텍스트박스
                createCostPriceTextBox(arrayRow, _costPriceXPoint4, _costPriceLastYPoint, CSPR_AMT);

                // 원가 인원 텍스트박스
                createCostPricePeopleCountTextBox(arrayRow, _costPriceXPoint5, _costPriceLastYPoint, NMPS_NBR);

                // 원가소계 텍스트박스
                createCostPriceSumAmountTextBox(arrayRow, _costPriceXPoint6, _costPriceLastYPoint, Utils.SetComma(costPriceSubtotal.ToString()));

                // 수배처 콤보박스
                createCostPriceArrangementCompanyComboBox(arrayRow, _costPriceXPoint3, _costPriceYPoint, ARPL_CMPN_NO);

                // 삭제버튼 (-)
                createCostPriceErasebuttonButton(arrayRow, _costPriceXPoint7, _costPriceLastYPoint);

                costPriceFlowLayoutPanel.Controls.Add(_fl_costPrice);
            }
            
            costPriceFlowLayoutPanel.ResumeLayout();

            costPriceSumTextBox.Text = Utils.SetComma(costPriceSum.ToString());
        }

        //=========================================================================================================================================================================
        // 원가 동적객체 생성용 리스트 초기화
        //=========================================================================================================================================================================
        private void clearCostPriceList()
        {
            costPriceFlowLayoutPanel.Controls.Clear();

            _costPriceArrayCount = 0;
            _costPriceAddButtonArray.Clear();
            _costPriceNoStringArray.Clear();
            _costPriceReservationNoStringArray.Clear();
            _costPriceNameTextBoxArray.Clear();
            _costPriceArrangementCompanyComboxArray.Clear();
            _costPriceCurrencyCodeComboBoxArray.Clear();
            _costPriceAdultDivisionCodeComboBoxArray.Clear();
            _costPriceTextBoxArray.Clear();
            _costPricePeopleCountTextBoxArray.Clear();
            _costPriceSumAmountTextBoxArray.Clear();
            _costPriceEraseButtonOptionArray.Clear();

            costPriceSumTextBox.Text = "0";
        }

        private void setCostPriceFlowLayoutPanel()
        {
            _fl_costPrice = new FlowLayoutPanel();
            _fl_costPrice.FlowDirection = FlowDirection.LeftToRight;
            _fl_costPrice.Name = $"fl{_costPriceArrayCount}";
            _fl_costPrice.Size = new Size(1259, 32);
            _fl_costPrice.BorderStyle = BorderStyle.None;
            _fl_costPrice_list.Add(_fl_costPrice);
        }

        //=========================================================================================================================================================================
        // 예약원가정보가 없는 예약건은 원가 행 1라인을 기본으로 생성
        //=========================================================================================================================================================================
        private void setCreateDefaultCostPriceInfoLine()
        {
            _costPriceArrayCount = 1;

            _costPriceLastYPoint = _costPriceYPoint;

            this.costPriceFlowLayoutPanel.HorizontalScroll.Maximum = 0;
            this.costPriceFlowLayoutPanel.HorizontalScroll.Visible = false;
            this.costPriceFlowLayoutPanel.AutoScroll = false;
            this.costPriceFlowLayoutPanel.WrapContents = false;
            this.costPriceFlowLayoutPanel.VerticalScroll.Visible = false;
            this.costPriceFlowLayoutPanel.AutoScroll = true;
            this.costPriceFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;

            // FlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setCostPriceFlowLayoutPanel();

            // 추가버튼 (+)
            createCostPriceAddButton(0, _costPriceXPoint1, _costPriceYPoint);

            // 원가일련번호 list
            _costPriceNoStringArray.Add(" ");

            // 예약원가일련번호 list
            _costPriceReservationNoStringArray.Add(" ");

            // 원가명 텍스트박스
            createCostPriceNameTextBox(_costPriceArrayCount, _costPriceXPoint2, _costPriceYPoint, "");

            // 원가통화코드 콤보박스
            createCostPriceCurrencyCodeComboBox(_costPriceArrayCount, _costPriceXPoint5, _costPriceYPoint, "");

            // 성인구분 콤보박스
            createCostPriceAdultDivisionCodeComboBox(_costPriceArrayCount, _costPriceXPoint4, _costPriceYPoint, "");

            // 원가 텍스트박스
            createCostPriceTextBox(_costPriceArrayCount, _costPriceXPoint6, _costPriceYPoint, "");

            // 인원수 텍스트박스
            createCostPricePeopleCountTextBox(_costPriceArrayCount, _costPriceXPoint7, _costPriceYPoint, "");

            // 원가소계 텍스트박스
            createCostPriceSumAmountTextBox(_costPriceArrayCount, _costPriceXPoint8, _costPriceYPoint, "");

            // 수배처 콤보박스
            createCostPriceArrangementCompanyComboBox(_costPriceArrayCount, _costPriceXPoint3, _costPriceYPoint, "");

            // 원가 행삭제 버튼
            createCostPriceErasebuttonButton(_costPriceArrayCount, _costPriceXPoint9, _costPriceYPoint);

            costPriceFlowLayoutPanel.Controls.Add(_fl_costPrice);
        }

        //=========================================================================================================================================================================
        // 원가 행추가 버튼 생성
        //=========================================================================================================================================================================
        private void createCostPriceAddButton(int arrayRow, int xPoint, int yPoint)
        {
            Button button = new Button();
            button.Location = new Point(xPoint, yPoint);
            button.Text = "+";
            button.Size = new Size(24, 29);
            button.Tag = arrayRow;
            button.Click += new EventHandler(addCostPriceInfoLineButton_Click);
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.BringToFront();

            _fl_costPrice.Controls.Add(button);
            _costPriceAddButtonArray.Add(button);
        }

        //=========================================================================================================================================================================
        // 원가명 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createCostPriceNameTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            textBox.Text = value;
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Left;
            textBox.Size = new Size(250, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            _fl_costPrice.Controls.Add(textBox);
            _costPriceNameTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 원가수배처 콤보박스 생성
        //=========================================================================================================================================================================
        private void createCostPriceArrangementCompanyComboBox(int arrayRow, int xPoint, int yPoint, string arrangementCompanyNumber)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(xPoint, yPoint);
            comboBox.Size = new Size(250, 29);
            comboBox.Tag = arrayRow;
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            comboBox.BringToFront();

            _fl_costPrice.Controls.Add(comboBox);

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 수배처 정보를 콤보박스에 설정
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            comboBox.Items.AddRange(_arrangementCompanyComboBox.Items.Cast<Object>().ToArray());

            if (comboBox.Items.Count > 0)
            {
                if (arrangementCompanyNumber != "")
                {
                    Utils.SelectComboBoxItemByValue(comboBox, arrangementCompanyNumber);
                } else
                {
                    comboBox.SelectedIndex = -1;
                }
            }

            _costPriceArrangementCompanyComboxArray.Add(comboBox);
        }

        //=========================================================================================================================================================================
        // 인원구분 콤보박스 생성
        //=========================================================================================================================================================================
        private void createCostPriceAdultDivisionCodeComboBox(int arrayRow, int xPoint, int yPoint, string adultDivisionCode)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(xPoint, yPoint);
            comboBox.Size = new Size(70, 29);
            comboBox.Tag = arrayRow;
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            comboBox.BringToFront();

            _fl_costPrice.Controls.Add(comboBox);

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 성인구분 정보를 콤보박스에 설정
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            comboBox.Items.AddRange(_adultDivisioncodeComboBox.Items.Cast<Object>().ToArray());

            if (comboBox.Items.Count > 0)
            {
                if (adultDivisionCode != "")
                {
                    Utils.SelectComboBoxItemByValue(comboBox, adultDivisionCode);
                }
                else
                {
                    comboBox.SelectedIndex = -1;
                }
            }

            _costPriceAdultDivisionCodeComboBoxArray.Add(comboBox);
        }

        //=========================================================================================================================================================================
        // 원가통화코드 콤보박스 생성
        //=========================================================================================================================================================================
        private void createCostPriceCurrencyCodeComboBox(int arrayRow, int xPoint, int yPoint, string currencyCode)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(xPoint, yPoint);
            comboBox.Size = new Size(120, 29);
            comboBox.Tag = arrayRow;
            comboBox.SelectedIndexChanged += new EventHandler(costPriceCurrencyCodeComboBox_SelectedIndexChanged);
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            comboBox.BringToFront();

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 통화코드를 콤보박스에 설정
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            comboBox.Items.AddRange(_currencyCodeComboBox.Items.Cast<Object>().ToArray());

            if (comboBox.Items.Count > 0)
            {
                if (currencyCode != "")
                {
                    Utils.SelectComboBoxItemByValue(comboBox, currencyCode);
                }
                else
                {
                    comboBox.SelectedIndex = -1;
                }
            }

            _fl_costPrice.Controls.Add(comboBox);

            _costPriceCurrencyCodeComboBoxArray.Add(comboBox);
        }

        //=========================================================================================================================================================================
        // 원가 인원수 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createCostPricePeopleCountTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);
            textBox.Text = value;
            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.LostFocus += new EventHandler(calcCostPriceSumAmount);
            textBox.Size = new Size(43, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            _fl_costPrice.Controls.Add(textBox);
            _costPricePeopleCountTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 원가금액 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createCostPriceTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);

            if (value != "" && value != null)
            {
                double doubleValue = double.Parse(value);
                string stringValue = String.Format("{0:0,0.00}", doubleValue);
                textBox.Text = stringValue;
            }
            else
            {
                textBox.Text = "0";
            }

            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.LostFocus += new EventHandler(calcCostPriceSumAmount);
            textBox.Size = new Size(100, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            _fl_costPrice.Controls.Add(textBox);
            _costPriceTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 원가 소계 텍스트박스 생성
        //=========================================================================================================================================================================
        private void createCostPriceSumAmountTextBox(int arrayRow, int xPoint, int yPoint, string value)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(xPoint, yPoint);

            if (value != "" && value != null)
            {
                double doubleValue = double.Parse(value);
                string stringValue = String.Format("{0:0,0.00}", doubleValue);
                textBox.Text = stringValue;
            }
            else
            {
                textBox.Text = "0";
            }

            textBox.ImeMode = ImeMode.Alpha;
            textBox.Font = new Font("맑은 고딕", 12);
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.Size = new Size(100, 29);
            textBox.Tag = arrayRow;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox.BringToFront();

            _fl_costPrice.Controls.Add(textBox);
            _costPriceSumAmountTextBoxArray.Add(textBox);
        }

        //=========================================================================================================================================================================
        // 원가 행삭제 버튼 생성
        //=========================================================================================================================================================================
        private void createCostPriceErasebuttonButton(int arrayRow, int xPoint, int yPoint)
        {
            Button button = new Button();
            button.Location = new Point(xPoint, yPoint);
            button.Text = "-";
            button.Size = new Size(24, 29);
            button.Tag = arrayRow;
            button.Click += new EventHandler(eraseCostPriceInfoLineButton_Click);
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            button.BringToFront();

            _fl_costPrice.Controls.Add(button);
            _costPriceEraseButtonOptionArray.Add(button);
        }

        //=========================================================================================================================================================================
        // 원가 행추가 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void addCostPriceInfoLineButton_Click(object sender, EventArgs e)
        {
            costPriceFlowLayoutPanel.SuspendLayout();

            int _costPriceArrayCount = _costPriceAddButtonArray.Count;
            _costPriceArrayCount++;

            // 버튼 이벤트 정의
            Button button = sender as Button;
            int tag = (int)button.Tag;

            _costPriceLastYPoint = _costPriceLastYPoint + 35;

            // FlowLayoutPanel(TopDown) 안에 FlowLayoutPanel(LeftToRight) 생성
            setCostPriceFlowLayoutPanel();

            // 추가버튼 (+)
            createCostPriceAddButton(_costPriceArrayCount, _costPriceXPoint1, _costPriceLastYPoint);

            // 원가일련번호 list
            _costPriceNoStringArray.Add(" ");

            // 예약원가일련번호 list
            _costPriceReservationNoStringArray.Add(" ");

            // 원가명 텍스트박스
            createCostPriceNameTextBox(_costPriceArrayCount, _costPriceXPoint2, _costPriceLastYPoint, "");

            // 원가통화코드 콤보박스
            createCostPriceCurrencyCodeComboBox(_costPriceArrayCount, _costPriceXPoint3, _costPriceYPoint, "");

            // 성인구분 콤보박스
            createCostPriceAdultDivisionCodeComboBox(_costPriceArrayCount, _costPriceXPoint3, _costPriceYPoint, "");

            // 원가 텍스트박스
            createCostPriceTextBox(_costPriceArrayCount, _costPriceXPoint4, _costPriceLastYPoint, "");

            // 원가 인원 텍스트박스
            createCostPricePeopleCountTextBox(_costPriceArrayCount, _costPriceXPoint5, _costPriceLastYPoint, "");

            // 원가소계 텍스트박스
            createCostPriceSumAmountTextBox(_costPriceArrayCount, _costPriceXPoint6, _costPriceLastYPoint, Utils.SetComma(""));

            // 수배처 콤보박스
            createCostPriceArrangementCompanyComboBox(_costPriceArrayCount, _costPriceXPoint3, _costPriceYPoint, "");

            // 삭제버튼 (-)
            createCostPriceErasebuttonButton(_costPriceArrayCount, _costPriceXPoint7, _costPriceLastYPoint);

            _fl_costPrice_list.Add(_fl_costPrice);

            costPriceFlowLayoutPanel.Controls.Add(_fl_costPrice);
            costPriceFlowLayoutPanel.ResumeLayout();
        }

        //=========================================================================================================================================================================
        // 원가소계 이벤트 처리
        //=========================================================================================================================================================================
        private void calcCostPriceSumAmount(object sender, EventArgs e)
        {
            // 버튼 이벤트 정의 (원가)
            TextBox textBox = sender as TextBox;
            int tag = (int)textBox.Tag;

            if (_costPriceTextBoxArray[tag - 1].Text.Trim().Equals("")) _costPriceTextBoxArray[tag - 1].Text = "0";
            if (_costPricePeopleCountTextBoxArray[tag - 1].Text.Trim().Equals("")) _costPricePeopleCountTextBoxArray[tag - 1].Text = "0";

            double costPrice = 0;

            if (_costPriceTextBoxArray[tag - 1].Text != "")
            {
                costPrice = double.Parse(_costPriceTextBoxArray[tag - 1].Text);
            }

            int costPricePeopleCount = Utils.GetInteger32Value(_costPricePeopleCountTextBoxArray[tag - 1].Text);
            double costPriceSumAmount = 0;
            double costPriceTotal = 0;

            if (costPriceSumTextBox.Text != "")
            {
                costPriceTotal = double.Parse(costPriceSumTextBox.Text);
            }

            if (costPrice > 0 && costPricePeopleCount > 0)
            {
                costPriceSumAmount = costPrice * costPricePeopleCount;
                _costPriceSumAmountTextBoxArray[tag - 1].Text = Utils.SetComma(costPriceSumAmount.ToString());
            }

            costPriceTotal = costPriceTotal + costPriceSumAmount;
            costPriceSumTextBox.Text = String.Format("{0:0,0.00}", costPriceTotal);
        }

        //=========================================================================================================================================================================
        // 원가 행삭제 버튼 클릭 이벤트
        //=========================================================================================================================================================================
        private void eraseCostPriceInfoLineButton_Click(object sender, EventArgs e)
        {
            if (_costPriceArrayCount <= 0) return;

            // 버튼 이벤트 정의
            Button button = sender as Button;
            int tag = (int)button.Tag;
            if (tag <= 0) return;
            if (_costPriceNoStringArray[tag - 1] == null)
                return;

            double costPriceTotal = 0;

            if (costPriceSumTextBox.Text != "")
            {
                costPriceTotal = double.Parse(costPriceSumTextBox.Text);
            }

            double costPrice = 0;

            int arrayCount = _costPriceAddButtonArray.Count;

            // 이전 행을 지우려고 하면 무시
            if (arrayCount > tag) {
                arrayCount = tag;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 해당 행의 원가를 삭제 (원가일련번호가 있는 경우에만 삭제)
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (_costPriceNoStringArray[tag - 1].Trim() != "" && _costPriceReservationNoStringArray[tag - 1].Trim() != "")
            {
                string[] queryStringArray = new string[1];           // sql 배열
                string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

                string CSPR_CNMB = _costPriceNoStringArray[tag - 1];
                string RSVT_CSPR_CNMB = _costPriceReservationNoStringArray[tag - 1];

                if (_costPriceSumAmountTextBoxArray[tag - 1].Text != "")
                {
                    costPrice = double.Parse(_costPriceSumAmountTextBoxArray[tag - 1].Text);
                }

                string query = string.Format("CALL DeleteRsvtCsprItem ('{0}','{1}', '{2}', '{3}')",
                                _reservationNumber, CSPR_CNMB, RSVT_CSPR_CNMB, Global.loginInfo.ACNT_ID);

                queryStringArray[0] = query;
                var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
                int retVal = queryResult.Item1;
                queryResultArray = queryResult.Item2;

                if (retVal == -1)
                {
                    MessageBox.Show("원가금액을 삭제할 수 없습니다.");
                    return;
                }

                costPriceTotal = costPriceTotal - costPrice;
            }

            costPriceSumTextBox.Text = String.Format("{0:0,0.00}", costPriceTotal);



            // 첫번째 판매가격 입력행의 삭제버튼을 누르면 값만 지움
            if (tag == 1 && arrayCount == 1)
            {
                _costPriceNoStringArray[0] = "";
                _costPriceReservationNoStringArray[0] = "";
                _costPriceNameTextBoxArray[0].Text = "";
                _costPriceArrangementCompanyComboxArray[0].SelectedIndex = -1;
                _costPriceAdultDivisionCodeComboBoxArray[0].SelectedIndex = -1;
                _costPriceCurrencyCodeComboBoxArray[0].SelectedIndex = -1;
                _costPriceTextBoxArray[0].Text = "";
                _costPricePeopleCountTextBoxArray[0].Text = "";
                _costPriceSumAmountTextBoxArray[0].Text = "";

                return;
            }


            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 판매가격 객체 제거
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 최종 y좌표 감소
            _costPriceLastYPoint = _costPriceLastYPoint - 35;

            // 배열크기 감소
            arrayCount--; 

            FlowLayoutPanel fl = _fl_costPrice_list[arrayCount];
            //ComboBox comboBox = null;
            //TextBox textBox = null;

            // 행추가버튼
            //button = _costPriceAddButtonArray[tag - 1];
            //fl.Controls.Remove(button);

            //// 원가명 텍스트박스
            //textBox = _costPriceNameTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 수배처 콤보박스
            //comboBox = _costPriceArrangementCompanyComboxArray[tag - 1];
            //fl.Controls.Remove(comboBox);

            //// 인원구분 콤보박스
            //comboBox = _costPriceAdultDivisionCodeComboBoxArray[tag - 1];
            //fl.Controls.Remove(comboBox);

            //// 원가통화코드 콤보박스
            //comboBox = _costPriceCurrencyCodeComboBoxArray[tag - 1];
            //fl.Controls.Remove(comboBox);

            //// 원가금액 텍스트박스
            //textBox = _costPriceTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 인원수 텍스트박스
            //textBox = _costPricePeopleCountTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 원가소계 텍스트박스
            //textBox = _costPriceSumAmountTextBoxArray[tag - 1];
            //fl.Controls.Remove(textBox);

            //// 행삭제 버튼
            //button = _costPriceEraseButtonOptionArray[arrayCount];
            //fl.Controls.Remove(button);

            _costPriceAddButtonArray.RemoveAt(arrayCount);
            _costPriceNoStringArray.RemoveAt(arrayCount);
            _costPriceReservationNoStringArray.RemoveAt(arrayCount);
            _costPriceNameTextBoxArray.RemoveAt(arrayCount);
            _costPriceArrangementCompanyComboxArray.RemoveAt(arrayCount);
            _costPriceAdultDivisionCodeComboBoxArray.RemoveAt(arrayCount);
            _costPriceCurrencyCodeComboBoxArray.RemoveAt(arrayCount);
            _costPriceTextBoxArray.RemoveAt(arrayCount);
            _costPricePeopleCountTextBoxArray.RemoveAt(arrayCount);
            _costPriceSumAmountTextBoxArray.RemoveAt(arrayCount);
            _costPriceEraseButtonOptionArray.RemoveAt(arrayCount);

            _fl_costPrice_list.RemoveAt(arrayCount);
            costPriceFlowLayoutPanel.Controls.RemoveAt(arrayCount);
            //costPriceFlowLayoutPanel.Controls.Remove(fl);
            
            foreach (Button btn in _costPriceEraseButtonOptionArray)
            {
                if ((int)btn.Tag > tag)
                    btn.Tag = (int)btn.Tag - 1;
            }

            costPriceFlowLayoutPanel.ResumeLayout();
        }


        //=========================================================================================================================================================================
        // 원가통화코드 변경 이벤트
        //=========================================================================================================================================================================
        private void costPriceCurrencyCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            // 통화코드콤보박스 이벤트 정의
            ComboBox comboBox = sender as ComboBox;
            int tag = (int)comboBox.Tag;

            if (tag < 0) return;

            //MessageBox.Show("선택한 원가통화코드=" + _costPriceCurrencyCodeComboBoxArray[tag - 1].SelectedValue);
            */
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
        // 상품원가내역의 원가정보를 예약원가내역으로 Copy
        //=========================================================================================================================================================================
        private void createReservationCostPriceInfo()
        {
            if (adultPeopleCountTextBox.Text.Trim().Equals("")) adultPeopleCountTextBox.Text = "0";
            if (childPeopleCountTextBox.Text.Trim().Equals("")) childPeopleCountTextBox.Text = "0";
            if (infantPeopleCountTextBox.Text.Trim().Equals("")) infantPeopleCountTextBox.Text = "0";

            vOReservationDetail.ADLT_NBR = Int16.Parse(adultPeopleCountTextBox.Text);
            vOReservationDetail.CHLD_NBR = Int16.Parse(childPeopleCountTextBox.Text);
            vOReservationDetail.INFN_NBR = Int16.Parse(infantPeopleCountTextBox.Text);

            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;
            string DPTR_DT = departureDateTimePicker.Value.ToShortDateString();

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = string.Format("CALL InsertRsvtCostPriceDefaultValue ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                            _reservationNumber, _productNo, _productGradeCode, vOReservationDetail.ADLT_NBR, vOReservationDetail.CHLD_NBR, vOReservationDetail.INFN_NBR, DPTR_DT, FRST_RGTR_ID);

            queryStringArray[0] = query;
            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("기본원가정보를 설정할 수 없습니다.");
                return;
            }
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
        // 예약기본 입력 파라미터 설정
        //=========================================================================================================================================================================
        private void paramsetReservationDetail(string cudType)
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

            vOReservationDetail.TKTR_CMPN_NO = (cooperativeCompanyComboBox.SelectedItem as ComboBoxItem).Value.ToString();          // 모객업체번호

            vOReservationDetail.CUST_NO = _customerNumber;                                                                                                                   // 고객번호
            vOReservationDetail.CUST_NM = bookerNameTextBox.Text.Trim();                                                                                                // 고객명

            string RPRS_ENG_NM = customerEngNameTextBox.Text.Trim();                                                                                                    // 대표자영문명
            vOReservationDetail.RPRS_ENG_NM = RPRS_ENG_NM;                                                                                                               // 대표자영문명            

            vOReservationDetail.ORDE_CTIF_PHNE_NO = cellphoneNumberTextBox.Text.Trim();                                                     // 주문자연락처전화번호
            vOReservationDetail.ORDE_EMAL_ADDR = emailIdTextBox.Text.Trim() + "@" + domainComboBox.Text.Trim();              // 주문자이메일주소
            vOReservationDetail.RPRS_EMAL_ADDR = vOReservationDetail.ORDE_EMAL_ADDR;                                         // 대표자이메일주소
            vOReservationDetail.AGE = 0;                                                                                     // 연령
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

            if (adultPeopleCountTextBox.Text.Trim() == "") adultPeopleCountTextBox.Text = "0";
            if (childPeopleCountTextBox.Text.Trim() == "") childPeopleCountTextBox.Text = "0";
            if (infantPeopleCountTextBox.Text.Trim() == "") infantPeopleCountTextBox.Text = "0";

            vOReservationDetail.ADLT_NBR = Int16.Parse(adultPeopleCountTextBox.Text);
            vOReservationDetail.CHLD_NBR = Int16.Parse(childPeopleCountTextBox.Text);
            vOReservationDetail.INFN_NBR = Int16.Parse(infantPeopleCountTextBox.Text);

            string RSVT_AMT = reservationPriceTextBox.Text;                                                                  // 예약금액
            if (RSVT_AMT == "") RSVT_AMT = "0";

            string PRPY_AMT = depositPriceTextBox.Text;                                                                      // 선금액
            if (RSVT_AMT == "") PRPY_AMT = "0";

            if (totalDepositPriceTextBox.Text.Trim() == "") totalDepositPriceTextBox.Text = "0";
            string TOT_RECT_AMT = totalDepositPriceTextBox.Text;                                                             // 총입금금액

            string MDPY_AMT = partPriceTextBox.Text;                                                                         // 중도금금액
            if (MDPY_AMT == "") MDPY_AMT = "0";

            // 예약복사 시에는 입금금액 정보를 0으로 초기화
            if (cudType.Equals("COPY") || cudType.Equals("NEW"))
            {
                vOReservationDetail.RSVT_AMT = 0;
                vOReservationDetail.PRPY_AMT = 0;
                vOReservationDetail.TOT_RECT_AMT = 0;
                vOReservationDetail.MDPY_AMT = 0;
                vOReservationDetail.UNAM_BAL = vOReservationDetail.WON_TOT_SALE_AMT;                                         // 미수잔액은 원화를 적용
                vOReservationDetail.STEM_WON_AMT = 0;
                vOReservationDetail.STMT_WON_AMT = 0;
                vOReservationDetail.WON_PYMT_FEE = 0;
                vOReservationDetail.DSCT_AMT = 0;
                vOReservationDetail.STMT_YN = "N";                                                                           // 정산여부
                vOReservationDetail.STMT_WON_AMT = 0;                                                                        // 정산원화금액
                vOReservationDetail.DSCT_AMT = 0;                                                                            // 할인금액
                vOReservationDetail.WON_PYMT_FEE = 0;                                                                        // 원화지급수수료
                vOReservationDetail.STEM_WON_AMT = 0;                                                                        // 가정산원화금액
            }
            else
            {
                vOReservationDetail.RSVT_AMT = Double.Parse(RSVT_AMT);
                vOReservationDetail.PRPY_AMT = Double.Parse(PRPY_AMT);
                vOReservationDetail.TOT_RECT_AMT = Double.Parse(TOT_RECT_AMT);
                vOReservationDetail.MDPY_AMT = Double.Parse(MDPY_AMT);
            }

            
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

            vOReservationDetail.RSVT_STTS_CD = (reservationStatusComboBox.SelectedItem as ComboBoxItem).Value.ToString();    // 예약상태코드
            vOReservationDetail.TCKT_STTS_CD = "";                                                                           // 티켓상태코드
            vOReservationDetail.QUOT_CNMB = "0";                                                                             // 견적일련번호
            vOReservationDetail.DEL_YN = "N";                                                                                // 삭제여부
            vOReservationDetail.CUST_RQST_CNTS = Utils.ReplaceSpecialChar(customerRequestMemoTextBox.Text.Trim());           // 고객요청내용
            vOReservationDetail.INTR_MEMO_CNTS = Utils.ReplaceSpecialChar(insideMemoTextBox.Text.Trim());                    // 내부메모내용
            vOReservationDetail.REMK_CNTS = "";                                                                              // 비고내용


            /*
            //-------------------------------------------------------------------------------------------------------------------------------
            // 예약건 새로 저장 및 수정 사항이 아닐경우는 예약 상세를 출력하는것이므로 복호화, 저장/갱신건일 경우에는 암호화
            //-------------------------------------------------------------------------------------------------------------------------------
            if (_saveMode != Global.SAVEMODE_UPDATE)
            {
                vOReservationDetail.CUST_NM = EncryptMgt.Decrypt(vOReservationDetail.CUST_NM, EncryptMgt.aesEncryptKey);                                            // 고객명
                vOReservationDetail.RPRS_ENG_NM = EncryptMgt.Decrypt(vOReservationDetail.RPRS_ENG_NM, EncryptMgt.aesEncryptKey);                            // 대표자 영문명
                vOReservationDetail.ORDE_CTIF_PHNE_NO = EncryptMgt.Decrypt(vOReservationDetail.ORDE_CTIF_PHNE_NO, EncryptMgt.aesEncryptKey);        // 주문자 전화번호
                vOReservationDetail.ORDE_EMAL_ADDR = EncryptMgt.Decrypt(vOReservationDetail.ORDE_EMAL_ADDR, EncryptMgt.aesEncryptKey);              // 주문자 이메일주소
                vOReservationDetail.RPRS_EMAL_ADDR = EncryptMgt.Decrypt(vOReservationDetail.RPRS_EMAL_ADDR, EncryptMgt.aesEncryptKey);                // 대표자 이메일주소 
            }
            else
            {
                vOReservationDetail.CUST_NM = EncryptMgt.Encrypt(vOReservationDetail.CUST_NM, EncryptMgt.aesEncryptKey);                                               // 고객명
                vOReservationDetail.RPRS_ENG_NM = EncryptMgt.Encrypt(vOReservationDetail.RPRS_ENG_NM, EncryptMgt.aesEncryptKey);                            // 대표자 영문명
                vOReservationDetail.ORDE_CTIF_PHNE_NO = EncryptMgt.Encrypt(vOReservationDetail.ORDE_CTIF_PHNE_NO, EncryptMgt.aesEncryptKey);         // 고객 전화번호
                vOReservationDetail.ORDE_EMAL_ADDR = EncryptMgt.Encrypt(vOReservationDetail.ORDE_EMAL_ADDR, EncryptMgt.aesEncryptKey);                  // 고객 이메일 주소
                vOReservationDetail.RPRS_EMAL_ADDR = EncryptMgt.Encrypt(vOReservationDetail.RPRS_EMAL_ADDR, EncryptMgt.aesEncryptKey);                  // 예약 대표자 이메일 주소
            }
            */
        }



        //=========================================================================================================================================================================
        // 예약상세 검색시 고객 이메일 도메인 정보를 ComboBox 에서 선택 출력 --> 190820 박현호
        //=========================================================================================================================================================================
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

        //=========================================================================================================================================================================
        /// 원화지급수수료 계산
        //=========================================================================================================================================================================
        private double calcPayableFee()
        {
            // 모객업체가 소셜마켓이 아닌 경우 수수료 계산을 생략
            if (_socialMarketSales == false) return 0;

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

                double totalSalePrice = Double.Parse(totalWonSalePriceTextBox.Text.Trim());
                double RATE = Double.Parse(dataRow["RATE"].ToString());                
                wonPayableFee = Math.Round(totalSalePrice * RATE / 100, 0);
            }

            return wonPayableFee;
        }

        //=========================================================================================================================================================================
        // 위탁판매수수료 필드 표시
        //=========================================================================================================================================================================
        private void redrawingConsignmentSaleFeeInputform(string consignmentSaleFee, string labelText, int xx, int yy)
        {
            consignmentSaleFeeTextBox.Text = Utils.SetComma(consignmentSaleFee);

            /*
            consignmentSaleFeeLabel.Text = labelText;
            consignmentSaleFeeLabel.Location = new Point(xx, yy);
            */

            // 고객사 요청 사항으로 변경 코드 미사용
            /*
            if (labelText.Equals("입금수수료"))
            {
                consignmentSaleFeeLabel.ForeColor = Color.RoyalBlue;
            }
            else if (labelText.Equals("미수수수료"))
            {
                consignmentSaleFeeLabel.ForeColor = Color.Red;
            }
            */
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
            double totalRevenue = vOReservationDetail.WON_TOT_SALE_AMT -
                                  vOReservationDetail.WON_PYMT_FEE -
                                  settlementAmount +
                                 (profitLoss);

            totalRevenueTextBox.Text = Utils.SetComma(totalRevenue.ToString());
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

                if (_reservationNumber != "")
                {
                    // 예약정보를 화면에 표시
                    setReservationInfoToForm();

                    // 수정건이므로 신규등록 버튼을 비활성화, 수정버튼을 활성화
                    _saveMode = Global.SAVEMODE_UPDATE;
                    insertNewReservationDetailButton.Enabled = false;
                    saveReservationDetailButton.Enabled = true;
                }
            }
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

            if(form.GetCustomerNumber() != "")
            {
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
        }



        /*
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
                setReservationInfoToForm();

                // 선택한 고객이 있을경우에만 저장버튼의 Enable 속성을 True 로 놔야함...
                string reservationNumber = form.GetReservationNumber();
                if(reservationNumber != "")
                {
                    saveReservationDetailButton.Enabled = true;
                    insertNewReservationDetailButton.Enabled = false;                    
                }

            }
        }
        */




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
            resetProductGradeComboBox(_productNo, departureDateTimePicker.Value.ToString("yyyy-MM-dd"));
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


        //=========================================================================================================================================================================
        // 예약금액정보를 최신 값으로 화면에 표시
        //=========================================================================================================================================================================
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

            string WON_TOT_SALE_AMT = dataRow["WON_TOT_SALE_AMT"].ToString();
            string UNAM_BAL = dataRow["UNAM_BAL"].ToString();

            totalWonSalePriceTextBox.Text = Utils.SetComma(WON_TOT_SALE_AMT);
            outstandingPriceTextBox.Text = Utils.SetComma(UNAM_BAL);
        }


        //=========================================================================================================================================================================
        // 초기화버튼 클릭
        //=========================================================================================================================================================================
        private void resetButton_Click(object sender, EventArgs e)
        {
            reservationNumberTextBox.ReadOnly = false;
            reservationNumberTextBox.Text = "";

            resetInputField();

            // 진행상태체크 입력폼 초기화
            initializeCutOffCheckInfoField();

            // 신규등록처리를 위해 신규등록버튼을 활성화하고 수정버튼은 비활성화
            insertNewReservationDetailButton.Enabled = true;
            saveReservationDetailButton.Enabled = false;

            clearSalePriceList();
            setCreateDefaultSalePriceInfoLine();

            clearCostPriceList();
            setCreateDefaultCostPriceInfoLine();

            _needSaveData = false;
            reservationStatusComboBox.SelectedIndex = 0;  // 미확정으로 설정
            //_selectedReservarionListRow = -1;             // 예약목록에서 선택한 행번호를 지움

            // 판매가 기본값 Button 활성화
            setDefaultSalesPriceButton.Enabled = true;
        }

        //=========================================================================================================================================================================
        // 저장버튼 클릭
        //=========================================================================================================================================================================
        private void saveReservationDetailButton_Click(object sender, EventArgs e)
        {
            _userMessage = "";
            _saveMode = Global.SAVEMODE_UPDATE;
            ComboBoxItem item = (ComboBoxItem)cooperativeCompanyComboBox.SelectedItem;
            string cooperationName = item.Text.Trim();

            if(cooperationName.Equals("--- 선택 ---"))
            {
                MessageBox.Show("모객업체를 선택해주세요..");
                cooperativeCompanyComboBox.Focus();
                return;
            }


            // 예약기본정보 저장
            if (saveReservationDetail() == false)
            {
                MessageBox.Show(_userMessage);
                _userMessage = "";
                return;
            }

            // 예약판매가격정보 저장
            if (saveReservationSalePriceInfo() == false)
            {
                MessageBox.Show(_userMessage);
                _userMessage = "";
                return;
            }

            // 예약원가정보 저장
            if (saveReservationCostInfo() == false)
            {
                MessageBox.Show(_userMessage);
                _userMessage = "";
                return;
            }

            _needSaveData = false;

            //redrawReservationDetailInfo();
            setReservationInfoToForm();

            MessageBox.Show("예약정보를 수정하였습니다.");
        }

        //=========================================================================================================================================================================
        // 예약기본정보 저장
        //=========================================================================================================================================================================
        private bool saveReservationDetail()
        {
            // 예약정보 입력필드 유효성 검증
            if (validateForReservationInfo("UPD") == false) return false;

            // 입력 파라미터 설정
            paramsetReservationDetail("UPD");

            if (_saveMode == Global.SAVEMODE_UPDATE)
            {
                vOReservationDetail.RSVT_NO = _reservationNumber;

                string query = "CALL UpdateRsvtItem(";
                query += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',";
                query += "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',";
                query += "'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',";
                query += "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',";
                query += "'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}')";
                query = string.Format(query,
                    vOReservationDetail.RSVT_NO,
                    vOReservationDetail.RSVT_DT,
                    vOReservationDetail.ORDR_NO,
                    vOReservationDetail.CNFM_NO,
                    vOReservationDetail.PRDT_CNMB,
                    vOReservationDetail.PRDT_GRAD_CD,
                    vOReservationDetail.CUST_NO,
                    vOReservationDetail.CUST_NM,
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
                    vOReservationDetail.WON_TOT_SALE_AMT,
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
                    //vOReservationDetail.STMT_YN,
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
                if (retVal == -1)
                {
                    MessageBox.Show("예약정보를 수정 할 수 없습니다.");
                    return false;
                }
            }

            return true;
        }

        //=========================================================================================================================================================================
        // 예약기본정보 입력 유효성 검증
        //=========================================================================================================================================================================
        private Boolean validateForReservationInfo(string procType)
        {
            if (_saveMode == Global.SAVEMODE_UPDATE && _reservationNumber == "")
            {
                _userMessage = "예약번호를 확인해 주십시오.";
                reservationNumberTextBox.Focus();
                return false;
            }
            if (productComboBox.SelectedIndex == -1)
            {
                _userMessage = "상품을 선택해 주십시오.";
                productComboBox.Focus();
                return false;
            }
            if (productGradeComboBox.SelectedIndex == -1)
            {
                _userMessage = "상품등급을 선택해 주십시오.";
                productGradeComboBox.Focus();
                return false;
            }
            if (reservationStatusComboBox.SelectedIndex == -1)
            {
                _userMessage = "예약상태를 선택해 주십시오.";
                reservationStatusComboBox.Focus();
                return false;
            }
            if (cooperativeCompanyComboBox.SelectedIndex == -1)
            {
                _userMessage = "모객업체를 선택해 주십시오.";
                cooperativeCompanyComboBox.Focus();
                return false;
            }

            if (_salePriceNameTextBoxArray.Count == 0 &&
                _salePriceTextBoxArray.Count == 0 &&
                _saleAdultDivisionCodeComboBoxArray.Count == 0 &&
                _salePricePeopleCountTextBoxArray.Count == 0)
            {
                _userMessage = "가격정보를 입력해 주십시오.";
                cooperativeCompanyComboBox.Focus();
                return false;
            }

            // 예약복사인 경우 미수잔액 체크 하지 않음
            if (procType.Equals("COPY"))
            {
                outstandingPriceTextBox.Text = "0";
            }

            return true;
        }

        //=========================================================================================================================================================================
        // 예약판매가격정보 저장
        //=========================================================================================================================================================================
        private bool saveReservationSalePriceInfo()
        {
            // 저장된 리스트값이 없으면 종료
            if (_salePriceNameTextBoxArray.Count == 0)
            {
                _userMessage = "가격정보를 확인하십시오.";
                return true;
            }

            if (_salePriceNameTextBoxArray.Count == 1)
            {
                if (_salePriceNameTextBoxArray[0].Text.Trim() == "" && _salePriceTextBoxArray[0].Text.Trim() == "" && _salePricePeopleCountTextBoxArray[0].Text.Trim() == "") return true;
            }

            if (_reservationNumber == "")
            {
                _userMessage = "예약정보를 먼저 저장해 주십시오.";
                return false;
            }

            string CNMB = "";
            string SALE_PRCE_NM = "";
            string ADLT_DVSN_CD = "";
            string CUR_CD = "";
            string SALE_UTPR = "";
            string WON_SALE_UTPR = "";
            string NMPS_NBR = "0";
            string APLY_EXRT = "";

            string[] queryStringArray = new string[_salePriceNoStringArray.Count];           // sql 배열
            string[] queryResultArray = new string[_salePriceNoStringArray.Count];           // 건별 sql 처리 결과 리턴 배열

            string query = "";

            for (int ii = 0; ii < _salePriceNoStringArray.Count; ii++)
            {
                CNMB = _salePriceNoStringArray[ii];
                SALE_PRCE_NM = _salePriceNameTextBoxArray[ii].Text.Trim();
                CUR_CD = Utils.GetSelectedComboBoxItemValue(_saleSaleCurrencyCodeComboBoxArray[ii]);
                ADLT_DVSN_CD = Utils.GetSelectedComboBoxItemValue(_saleAdultDivisionCodeComboBoxArray[ii]);
                SALE_UTPR = Utils.GetDoubleString(_salePriceTextBoxArray[ii].Text.Trim());
                WON_SALE_UTPR = Utils.GetDoubleString(_saleWonPriceTextBoxArray[ii].Text.Trim());
                APLY_EXRT = _saleApplyExchangeRateTextBoxArray[ii].Text.Trim();
                NMPS_NBR = _salePricePeopleCountTextBoxArray[ii].Text.Trim();

                /**
                 * 19. 11. 6(수)
                 * 옵션의 인원수가 예약건의 총인원수에 반영이 되는 문제를, 인원구분의 옵션["3"]을 추가해서 구분
                 * */
                //if (!ADLT_DVSN_CD.Equals("3"))
                    
                //else
                //    NMPS_NBR = "0";

                if (SALE_PRCE_NM == "" && ADLT_DVSN_CD == "" && SALE_UTPR == "" && NMPS_NBR == "")
                {
                    break;
                }

                if (SALE_PRCE_NM == "" && ADLT_DVSN_CD != "")
                {
                    _userMessage = "판매가격명은 필수 입력항목입니다.";
                    _salePriceNameTextBoxArray[ii].Focus();
                    return false;
                }

                if (SALE_PRCE_NM != "" && ADLT_DVSN_CD == "")
                {
                    _userMessage = "성인구분은 필수 입력항목입니다.";
                    _saleAdultDivisionCodeComboBoxArray[ii].Focus();
                    return false;
                }

                if (!CUR_CD.Equals("KRW") && (SALE_PRCE_NM != "" && (SALE_UTPR == "" || SALE_UTPR == "0")))
                {
                    _userMessage = "판매가격은 필수 입력항목입니다.";
                    _salePriceTextBoxArray[ii].Focus();
                    return false;
                }

                if (CUR_CD.Equals("KRW") && (SALE_PRCE_NM != "" && (WON_SALE_UTPR == "" || WON_SALE_UTPR == "0")))
                {
                    _userMessage = "판매가격은 필수 입력항목입니다.";
                    _saleWonPriceTextBoxArray[ii].Focus();
                    return false;
                }

                if (SALE_PRCE_NM != "" && (NMPS_NBR == "" || NMPS_NBR == "0"))
                {
                    _userMessage = "인원수는 필수 입력항목입니다.";
                    _salePricePeopleCountTextBoxArray[ii].Focus();
                    return false;
                }

                if (CNMB == "")
                {
                    query = string.Format("CALL InsertRsvtSalePriceItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
                                           _reservationNumber, SALE_PRCE_NM, ADLT_DVSN_CD, CUR_CD, SALE_UTPR, WON_SALE_UTPR, NMPS_NBR, APLY_EXRT, Global.loginInfo.ACNT_ID);
                }
                else
                {
                    query = string.Format("CALL UpdateRsvtSalePriceItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                                           _reservationNumber, CNMB, SALE_PRCE_NM, ADLT_DVSN_CD, CUR_CD, SALE_UTPR, WON_SALE_UTPR, NMPS_NBR, APLY_EXRT, Global.loginInfo.ACNT_ID);
                }

                queryStringArray[ii] = query;
            }

            long retVal = DbHelper.ExecuteScalarAndNonQueryWithTransaction(queryStringArray);

            if (retVal == -1)
            {
                _userMessage = "판매가격정보를 저장할 수 없습니다.";
                return false;
            }

            return true;
        }

        //=========================================================================================================================================================================
        // 예약원가정보 저장
        //=========================================================================================================================================================================
        private bool saveReservationCostInfo()
        {
            // 저장된 리스트값이 없으면 종료
            if (_costPriceNameTextBoxArray.Count == 0) return true;

            if (_costPriceNameTextBoxArray.Count == 1)
            {
                if (_costPriceNameTextBoxArray[0].Text.Trim() == "" || _costPriceNameTextBoxArray[0].Text.Trim() == "") return true;

            }

            if (_reservationNumber == "")
            {
                _userMessage = "예약정보를 먼저 저장해 주십시오.";
                return false;
            }

            string CSPR_CNMB = "";
            string RSVT_CSPR_CNMB = "";
            string CSPR_NM = "";
            string CSPR_CUR_CD = "";
            string ARPL_CMPN_NO = "";
            string ADLT_DVSN_CD = "";
            string CSPR_AMT = "";
            string NMPS_NBR = "";

            string[] queryStringArray = new string[_costPriceNameTextBoxArray.Count];           // sql 배열
            string[] queryResultArray = new string[_costPriceNameTextBoxArray.Count];           // 건별 sql 처리 결과 리턴 배열
            string query = "";

            for (int ii = 0; ii < _costPriceNameTextBoxArray.Count; ii++)
            {
                CSPR_CNMB = _costPriceNoStringArray[ii].Trim();
                RSVT_CSPR_CNMB = _costPriceReservationNoStringArray[ii].Trim();
                CSPR_NM = _costPriceNameTextBoxArray[ii].Text.Trim();
                ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(_costPriceArrangementCompanyComboxArray[ii]);
                CSPR_CUR_CD = Utils.GetSelectedComboBoxItemValue(_costPriceCurrencyCodeComboBoxArray[ii]);
                ADLT_DVSN_CD = Utils.GetSelectedComboBoxItemValue(_costPriceAdultDivisionCodeComboBoxArray[ii]);
                CSPR_AMT = Utils.GetDoubleString(_costPriceTextBoxArray[ii].Text.Trim());
                NMPS_NBR = _costPricePeopleCountTextBoxArray[ii].Text.Trim();

                if (CSPR_NM == "" && CSPR_AMT == "" && NMPS_NBR == "") break;

                if (CSPR_NM == "" && (CSPR_AMT != "" && NMPS_NBR != ""))
                {
                    _userMessage = "원가명은 필수 입력항목입니다.";
                    _costPriceNameTextBoxArray[ii].Focus();
                    return false;
                }

                if (CSPR_NM != "" && (CSPR_AMT != "" && NMPS_NBR != "") && ARPL_CMPN_NO == "")
                {
                    _userMessage = "원가 수배처는 필수 입력항목입니다.";
                    _costPriceArrangementCompanyComboxArray[ii].Focus();
                    return false;
                }

                if (CSPR_NM != "" && (CSPR_AMT != "" && NMPS_NBR != "") && CSPR_CUR_CD == "")
                {
                    _userMessage = "원가 통화코드는 필수 입력항목입니다.";
                    _costPriceCurrencyCodeComboBoxArray[ii].Focus();
                    return false;
                }

                if (CSPR_NM != "" && (CSPR_AMT != "" && NMPS_NBR != "") && ADLT_DVSN_CD == "")
                {
                    _userMessage = "원가 인원구분은 필수 입력항목입니다.";
                    _costPriceAdultDivisionCodeComboBoxArray[ii].Focus();
                    return false;
                }

                if (CSPR_NM != "" && CSPR_AMT == "")
                {
                    _userMessage = "원가금액은 필수 입력항목입니다.";
                    _costPriceTextBoxArray[ii].Focus();
                    return false;
                }

                if (CSPR_NM != "" && NMPS_NBR == "")
                {
                    _userMessage = "인원수는 필수 입력항목입니다.";
                    _costPricePeopleCountTextBoxArray[ii].Focus();
                    return false;
                }

                if (CSPR_CNMB == "" && RSVT_CSPR_CNMB == "")
                {
                    query = string.Format("CALL InsertRsvtCsprItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                                           _reservationNumber, CSPR_NM, ARPL_CMPN_NO, CSPR_CUR_CD, ADLT_DVSN_CD, CSPR_AMT, NMPS_NBR, Global.loginInfo.ACNT_ID);
                }
                else
                {
                    query = string.Format("CALL UpdateRsvtCsprItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                                           _reservationNumber, CSPR_CNMB, RSVT_CSPR_CNMB, CSPR_NM, ARPL_CMPN_NO, CSPR_CUR_CD, ADLT_DVSN_CD, CSPR_AMT, NMPS_NBR, Global.loginInfo.ACNT_ID);
                }

                queryStringArray[ii] = query;
            }

            long retVal = DbHelper.ExecuteScalarAndNonQueryWithTransaction(queryStringArray);

            if (retVal == -1)
            {
                _userMessage = "원가정보를 저장할 수 없습니다.";
                return false;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 예약기본에서 반영된 최신 금액정보를 재검색
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            setReservationCostPriceList();

            return true;
        }

        //=========================================================================================================================================================================
        // [복사버튼 클릭] 기존 예약정보를 바탕으로 새로운 예약정보를 생성
        //=========================================================================================================================================================================
        private void copyReservationDetailButton_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보가 없어 복사할 수 없습니다. 복사대상 예약건으로 조회를 하십시오.");
                return;
            }

            // 복사하려는 예약자가 복사대상 예약자와 같으면 경고 메시지 처리

            string message = vOReservationDetail.CUST_NM + "님 예약건을 " + bookerNameTextBox.Text + "님으로 복사합니다.";
            DialogResult result = MessageBoxEx.Show(message, "예약복사", "예", "아니오");
            if (result == DialogResult.No)
            {
                return;
            }

            // 예약정보 입력필드 유효성 검증
            if (validateForReservationInfo("COPY") == false) return;

            //// 입력 파라미터 설정
            paramsetReservationDetail("COPY");

            // 복사할 때에는 예약상태를 미확정으로 초기화
            vOReservationDetail.RSVT_STTS_CD = "1";
            Utils.SelectComboBoxItemByValue(reservationStatusComboBox, "1");

            string newReservationNumber = insertReservationInfoAllCopy();

            if (newReservationNumber != null)
            {
                _needSaveData = false;
                //_selectedReservarionListRow = -1;             // 예약목록에서 선택한 행번호를 지움
                _reservationNumber = newReservationNumber;
            }
            else
            {
                _needSaveData = true;
            }

            setReservationInfoToForm();
            MessageBox.Show(_userMessage);
        }

        //=========================================================================================================================================================================
        // 기존예약건을 새로운 예약번호 건에 복사
        //=========================================================================================================================================================================
        private string insertReservationInfoAllCopy()
        {
            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string query = "CALL InsertReservationInfoAllCopy(";
            query += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',";
            query += "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',";
            query += "'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',";
            query += "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',";
            query += "'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}')";
            query = string.Format(query,
                    vOReservationDetail.RSVT_NO,
                    vOReservationDetail.RSVT_DT,
                    vOReservationDetail.ORDR_NO,
                    vOReservationDetail.CNFM_NO,
                    vOReservationDetail.PRDT_CNMB,
                    vOReservationDetail.PRDT_GRAD_CD,
                    vOReservationDetail.CUST_NO,
                    vOReservationDetail.CUST_NM,
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
                    vOReservationDetail.WON_TOT_SALE_AMT,
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

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                _userMessage = "예약정보를 복사할 수 없습니다.";
                return null;
            }

            // 채번된 예약번호를 저장
            _reservationNumber = queryResultArray[0];
            reservationNumberTextBox.Text = _reservationNumber;

            // 등록된 예약기본, 예약옵션, 예약원가내역을 검색하여 화면에 표시
            setReservationInfoToForm();
            _userMessage = "예약정보를 복사했습니다.";

            return queryResultArray[0];     // 신규 채번된 예약번호를 리턴
        }

        //=========================================================================================================================================================================
        // 입금관리버튼 클릭
        //=========================================================================================================================================================================
        private void depositMgtButton_Click(object sender, EventArgs e)
        {
            // 판매가격정보가 없을 경우 입금처리를 하지 못하도록 설정
            if (totalWonSalePriceTextBox.Text == "" || totalWonSalePriceTextBox.Text == "0")
            {
                MessageBox.Show("가격정보를 먼저 저장해주세요.");
                return;
            }

            //------------------------------------------------------------------------------------------------------------------------------
            // 모객업체에서 업체구분코드 추출 (10 : 소셜, 11 : B2B, 1:본사)   --> 191024 박현호
            //------------------------------------------------------------------------------------------------------------------------------
            ComboBoxItem item = (ComboBoxItem)cooperativeCompanyComboBox.SelectedItem;
            string CMPN_NO = item.Value.ToString().Trim();
            string COOP_CMPN_DVSN_CD = "";
            string query = "SELECT COOP_CMPN_DVSN_CD FROM TB_COOP_CMPN_M WHERE CMPN_NO=" + CMPN_NO;
            DataSet ds = DbHelper.SelectQuery(query);
            DataRow row = ds.Tables[0].Rows[0];
            COOP_CMPN_DVSN_CD = row["COOP_CMPN_DVSN_CD"].ToString().Trim();


            //---------------------------------------------------------------------------------------------------------------
            // 입금관리 Form 띄우고 기본값 Setting
            //---------------------------------------------------------------------------------------------------------------
            PopUpDepositMgt form = new PopUpDepositMgt();
            form.parent = this;
            form.SetSaveMode(Global.SAVEMODE_UPDATE);
            form.SetReservationNumber(_reservationNumber); 
            form.SetBookerName(bookerNameTextBox.Text.Trim());
            form.SetCustomerNumber(_customerNumber);
            if (productComboBox.SelectedIndex != -1) form.SetProductName((productComboBox.SelectedItem as ComboBoxItem).Text);
            form.SetDepositPriceTextBox(totalDepositPriceTextBox.Text);
            form.SetDepatureDate(departureDateTimePicker.Value.ToString("yyyy-MM-dd"));
            form.SetSalesPriceTextBox(totalWonSalePriceTextBox.Text.Trim());
            form.SetReservationPriceTextBox(reservationPriceTextBox.Text.Trim());
            form.SetPartPriceTextBox(partPriceTextBox.Text.Trim());
            form.SetOutstandingPriceTextBox(outstandingPriceTextBox.Text.Trim());
            form.SetReceivedFeeYn(_receivedFeeYn);
            form.SetSocialMarketSales(_socialMarketSales);
            form.SetCooperationDivisionCode(COOP_CMPN_DVSN_CD);
            

            if (_receivedFeeYn == true)
            {
                form.SetWonPayableFeeTextBox("0");
            } else
            {
                form.SetWonPayableFeeTextBox(vOReservationDetail.WON_PYMT_FEE.ToString());
            }

            form.SetProductNo(_productNo);
            form.SetTicketterCompanyNo(vOReservationDetail.TKTR_CMPN_NO);

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();


            // 위탁판매지급수수료 (계산금액이 없으면 미입금수수료로 재계산)
            if (form.GetConsignmentSaleFee() != 0)
            {
                vOReservationDetail.WON_PYMT_FEE = form.GetConsignmentSaleFee();
                redrawingConsignmentSaleFeeInputform(form.GetConsignmentSaleFee().ToString(), "수수료", 422, 42);
            }
            else
            {
                double wonPayableFee = calcPayableFee();
                redrawingConsignmentSaleFeeInputform(wonPayableFee.ToString(), "수수료", 422, 42);
            }

            // 수익금액 재계산
            //calcRevenue();

            setReservationDetail();         // 예약상세 내역 다시 불러오기 -> 박현호            
        }

        //=========================================================================================================================================================================
        // 예약건 신규 등록
        //=========================================================================================================================================================================
        private void insertNewReservationDetailButton_Click(object sender, EventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cooperativeCompanyComboBox.SelectedItem;
            string cooperationName = item.Text.Trim();

            if (cooperationName.Equals("--- 선택 ---"))
            {
                MessageBox.Show("모객업체를 선택해주세요..");
                cooperativeCompanyComboBox.Focus();
                return;
            }

            // 기존 데이터 조회 중에 신규등록 버튼을 누르면 
            if (reservationNumberTextBox.Text.Trim() != "")
            {
                MessageBox.Show("기 예약건입니다. 변경하시려면 수정버튼을 누르세요.");
                return;
            }

            // 예약정보 입력필드 유효성 검증
            if (validateForReservationInfo("NEW") == false)
            {
                MessageBox.Show(_userMessage);
                return;
            }

            // 입력 파라미터 설정
            paramsetReservationDetail("NEW");

            // 예약기본 테이블 생성
            insertReservationInfo("NEW");

            _saveMode = Global.SAVEMODE_UPDATE;

            // 예약정보 화면 갱신
            redrawReservationDetailInfo();

            _needSaveData = false;
            saveReservationDetailButton.Enabled = true;
            insertNewReservationDetailButton.Enabled = false;
        }

        //=========================================================================================================================================================================
        // 예약기본 테이블 신규 생성
        //=========================================================================================================================================================================
        private void insertReservationInfo(string procType)
        {
            // 예약정보 입력필드 유효성 검증
            if (validateForReservationInfo(procType) == false) return;

            string query = "CALL InsertRsvtItem(";
            query += "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',";
            query += "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',";
            query += "'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',";
            query += "'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',";
            query += "'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}')";
            query = string.Format(query,
                    vOReservationDetail.RSVT_NO,
                    vOReservationDetail.RSVT_DT,
                    vOReservationDetail.ORDR_NO,
                    vOReservationDetail.CNFM_NO,
                    vOReservationDetail.PRDT_CNMB, 
                    vOReservationDetail.PRDT_GRAD_CD,
                    vOReservationDetail.CUST_NO,
                    vOReservationDetail.CUST_NM,
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
                    vOReservationDetail.WON_TOT_SALE_AMT,
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

            MessageBox.Show("예약정보를 추가했습니다.");
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
            /*
            if (costPriceDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("등록된 원가내역이 없습니다. 원가정보를 등록하세요.");
                return;
            }
            */
            PopUpAccountPayableMgt form = new PopUpAccountPayableMgt();

            form.SetSettlementYn(vOReservationDetail.STMT_YN);
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
            {
                settlementEstimationWonAmtTextBox.Text = Utils.SetComma(form.GetSettlementEstimationWonAmt().ToString());
            }

            setReservationDetail();         // 예약상세 내역 다시 불러오기 -> 박현호
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
                // SFTP로 서버의 바우처 양식 Download후, 열기
                const string Host = "gbrdg111.vps.phps.kr";
                const int Port = 15342;
                const string Username = "gbridge";
                const string Password = "1q2w3e$$";
                const string Source = "/home/gbridge/template/Voucher";
                //const string Destination = @"c:\TripERP";

                // 다운로드 경로 : TripERP 프로그램 아래의 Downloads 폴더 안
                DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Downloads");

                if (!di.Exists)
                    di.Create();

                var connectionInfo = new KeyboardInteractiveConnectionInfo(Host, Port, Username);

                connectionInfo.AuthenticationPrompt += delegate (object senders, AuthenticationPromptEventArgs ex)
                {
                    foreach (var prompt in ex.Prompts)
                    {
                        if (prompt.Request.Equals("Password: ", StringComparison.InvariantCultureIgnoreCase))
                        {
                            prompt.Response = Password;
                        }
                    }
                };

                using (var client = new SftpClient(Host, 15342, Username, Password))
                {
                    client.Connect();
                    DownloadDirectory(client, Source, di.ToString());
                }
            }
        }

        //=========================================================================================================================================================================
        // SFTP ServerFile Download Function_1 [19/09/03 배장훈]
        //=========================================================================================================================================================================
        private void DownloadDirectory(SftpClient client, string source, string destination)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(productComboBox); // 19
            string COOP_CMPN_NO = "0000";   // 바우처는 업체별로 동일하므로 0000으로 고정

            string reservationNum = reservationNumberTextBox.Text.Trim();
            string bookerName = bookerNameTextBox.Text.Trim();
            var files = client.ListDirectory(source);

            bool is_template_exist = false;

            foreach (var file in files)
            {
                // 디렉토리, 심볼릭링크 제외
                if (!file.IsDirectory && !file.IsSymbolicLink)
                {
                    // /home/gbridge/template/Voucher 경로의 바우처 템플릿 조회
                    if (file.Name.Equals("V_" + COOP_CMPN_NO + "_" + PRDT_CNMB + ".xlsx"))
                    {
                        is_template_exist = true;
                        DownloadFile(client, file, destination);
                        VoucherPrintOutMgt form = new VoucherPrintOutMgt();
                        form.StartPosition = FormStartPosition.CenterParent;
                        form.SetReservationNum(reservationNum);
                        form.SetBookerName(bookerName);
                        form.SetCooperationCompanyNo(COOP_CMPN_NO);
                        form.ShowDialog();

                        client.Disconnect();
                    }
                }
            }

            if (!is_template_exist)
                MessageBox.Show("'" + productComboBox.Text + "'상품의 바우처 템플릿이 존재하지 않습니다. 운영담당자에게 연락하세요.");
        }

        //=========================================================================================================================================================================
        // SFTP ServerFile Download Function_2 [19/09/03 배장훈]
        //=========================================================================================================================================================================
        private void DownloadFile(SftpClient client, SftpFile file, string directory)
        {
            DirectoryInfo di = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\Downloads\");

            // cooperativeCompanyComboBox :모객업체번호
            // productComboBox :상품명 일련번호

            string reservationNum = reservationNumberTextBox.Text.Trim();
            string bookerName = bookerNameTextBox.Text.Trim();

            string fileNamePath = di + reservationNum + "_" + departureDateTimePicker.Value.ToString("yyyy-MM-dd") + "_" + bookerName + "_" + file.Name;
            // 파일명 변경 (하노이통킨쇼 --> 19090216_2019-09-24_조아영_하노이통킨쇼)

            using (Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name)))
            {
                client.DownloadFile(file.FullName, fileStream);
            }
        }

        //=========================================================================================================================================================================
        // 폼 닫기
        //=========================================================================================================================================================================
        private void closeButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //=========================================================================================================================================================================
        // 상품등급 변경 시 판매통화코드 설정
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

             _productGradeCode = (productGradeComboBox.SelectedItem as ComboBoxItem).Value.ToString();            
            string DPTR_DT = departureDateTimePicker.Value.ToString("yyyy-MM-dd").Substring(0, 10);

            string query = string.Format("CALL SelectProductDetailItem ( {0}, '{1}', '{2}', '{3}' )", PRDT_CNMB, _productGradeCode, DPTR_DT, DPTR_DT);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품상세정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CUR_CD = dataRow["CUR_CD"].ToString();
                Utils.SelectComboBoxItemByValue(saleCurrencyCodeComboBox, CUR_CD);        // 판매통화코드
            }
            
        }

        private void dayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dayComboBox.SelectedIndex != -1)
                return;

            string night = (nightComboBox.SelectedItem as string);
            arrivalDateTimePicker.Value = departureDateTimePicker.Value.AddDays(Convert.ToInt32(night) + 1);
            arrivalDateTimePicker.Refresh();
        }

        //=========================================================================================================================================================================
        // 원가 기본값 설정
        //=========================================================================================================================================================================
        private void setDefaultCostPriceButton_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }
            if (productComboBox.SelectedIndex == -1 || _productNo == "")
            {
                MessageBox.Show("상품을 선택해 주십시오.");
                productComboBox.Focus();
                return;
            }
            if (productGradeComboBox.SelectedIndex == -1 || _productGradeCode == "")
            {
                MessageBox.Show("상품등급을 선택해 주십시오.");
                productGradeComboBox.Focus();
                return;
            }

            ComboBoxItem productItem = (ComboBoxItem)productComboBox.SelectedItem;                          // 상품 내역 아이템
            ComboBoxItem productGradeItem = (ComboBoxItem)productGradeComboBox.SelectedItem;        // 상품 등급 아이템

            if (adultPeopleCountTextBox.Text.Trim() == "") adultPeopleCountTextBox.Text = "0";
            if (childPeopleCountTextBox.Text.Trim() == "") childPeopleCountTextBox.Text = "0";
            if (infantPeopleCountTextBox.Text.Trim() == "") infantPeopleCountTextBox.Text = "0";

            if (adultPeopleCountTextBox.Text.Trim() == "0" && childPeopleCountTextBox.Text.Trim() == "0" && infantPeopleCountTextBox.Text.Trim() == "0")
            {
                MessageBox.Show("인원수는 필수 입력항목입니다.");
                adultPeopleCountTextBox.Focus();
                return;
            }

            DialogResult result = MessageBoxEx.Show("원가정보가 상품의 원가 기본값으로 저장됩니다. 계속하시겠습니까?", "원가 기본값 설정", "예", "아니오");
            if (result == DialogResult.No)
            {
                return;
            }

            // 적용원가 조회 개수가 2개 이상일 경우 확인 경고창 띄움
            bool confirmResult = confirmReservationCostPriceQuantity(productItem, productGradeItem);
            if (!confirmResult)
            {
                return;
            }

            // 상품원가목록의 원가정보를 예약원가내역으로 Copy
            createReservationCostPriceInfo();

            // 원가입력 창에 기본값 설정 정보를 표시
            setReservationCostPriceList();
        }


        //================================================================================================
        // 적용원가 조회개수가 2개 이상인지 확인
        //================================================================================================
        private bool confirmReservationCostPriceQuantity(Object in_productItem, Object in_productGradeItem)
        {
            bool result = false;
            ComboBoxItem productItem = (ComboBoxItem)in_productItem;
            ComboBoxItem productGradeItem = (ComboBoxItem)in_productGradeItem;
            string PRDT_CNMB = productItem.Value.ToString().Trim();
            string PRDT_GRAD_CD = productGradeItem.Value.ToString().Trim();
            string DPTR_DT = departureDateTimePicker.Value.ToShortDateString();
            string query = "SELECT COUNT(*) AS COUNT " +
                "                   FROM TB_PRDT_CSPR_D " +
                "                   WHERE PRDT_CNMB = " + PRDT_CNMB 
                                    +" AND PRDT_GRAD_CD = '" + PRDT_GRAD_CD+"'"
                                    +" AND USE_YN = 'Y'"
                                    +" AND APLY_LNCH_DT <= '"+ DPTR_DT+"'"
                                    + " AND APLY_END_DT  >= '"+ DPTR_DT+"'";
            DataSet ds = DbHelper.SelectQuery(query);
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                MessageBox.Show("적용가능한 원가정보가 존재하지 않습니다.");
                return result;
            }
            DataRow row = ds.Tables[0].Rows[0];
            int COUNT = int.Parse(row["COUNT"].ToString());
            if (COUNT > 1)
            {
                MessageBox.Show("적용 가능한 원가정보가 2건 이상 조회되었습니다.\n등록된 원가정보를 확인해주시기 바랍니다.");
                return result;
            }
            else if(COUNT == 0)
            {
                MessageBox.Show("적용 가능한 원가정보가 존재하지 않습니다.\n출발일을 포함하는 기간의 원가정보를 등록해주세요.");
                return result;
            }
            else
            {
                result = true;
            }

            return result;
        }



        //=========================================================================================================================================================================
        // 폼 키보드 이벤트 처리
        //=========================================================================================================================================================================
        private void ReservationDetailInfoMgt_KeyDown(object sender, KeyEventArgs e)
        {
            // F5를 누르면 화면을 Refresh
            if (e.KeyCode == Keys.F5)
            {
                setReservationInfoToForm();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                saveReservationDetailButton_Click(sender, e);
            }

        }

        //=========================================================================================================================================================================
        // 소셜모객업체인 경우 위탁판매지급수수료 계산 대상을 true로 설정
        //=========================================================================================================================================================================
        private void cooperativeCompanyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isInvoiceButtonTrue();
            checkSocialCompany();
        }

        // 소셜 모객업체 판별 및 계산 대상 True 동작 Method
        private void checkSocialCompany()
        {            
            if (cooperativeCompanyComboBox.Items.Count == 0) return;
            if (Utils.GetSelectedComboBoxItemText(cooperativeCompanyComboBox).Equals("지브리지") || Utils.GetSelectedComboBoxItemText(cooperativeCompanyComboBox).Equals("--- 선택 ---")) return;
            if (cooperativeCompanyComboBox.SelectedIndex == -1) return;

            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(cooperativeCompanyComboBox);
            string query = string.Format("CALL SelectCoopCmpnItem ('{0}')", CMPN_NO);

            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("모객업체 정보를 가져올 수 없습니다.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];
            vOReservationDetail.COOP_CMPN_DVSN_CD = dataRow["COOP_CMPN_DVSN_CD"].ToString();

            if (vOReservationDetail.COOP_CMPN_DVSN_CD.Equals("10"))
            {
                _socialMarketSales = true;
            }
            else
            {
                _socialMarketSales = false;
            }

            if (!vOReservationDetail.COOP_CMPN_DVSN_CD.Equals("1") && !vOReservationDetail.COOP_CMPN_DVSN_CD.Equals("10"))
            {
                consignmentSaleFeeTextBox.Text = "0";
                vOReservationDetail.WON_PYMT_FEE = 0;
                voucherProcButton.Enabled = false;
            }
            else
            {
                voucherProcButton.Enabled = true;
                double wonPayableFee = calcPayableFee();
                redrawingConsignmentSaleFeeInputform(wonPayableFee.ToString(), "수수료", 422, 42);
            }
        }



        //=========================================================================================================================================================================
        // [판매가격 기본값 설정] 상품기본가격 정보를 예약판매가격내역으로 Insert
        //=========================================================================================================================================================================
        private void setDefaultSalesPriceButton_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약정보를 먼저 저장해 주십시오.");
                return;
            }
            if (productComboBox.SelectedIndex == -1 || _productNo == "")
            {
                MessageBox.Show("상품을 선택해 주십시오.");
                productComboBox.Focus();
                return;
            }
            if (productGradeComboBox.SelectedIndex == -1 || _productGradeCode == "")
            {
                MessageBox.Show("상품등급을 선택해 주십시오.");
                productGradeComboBox.Focus();
                return;
            }

            if (adultPeopleCountTextBox.Text.Trim() == "") adultPeopleCountTextBox.Text = "0";
            if (childPeopleCountTextBox.Text.Trim() == "") childPeopleCountTextBox.Text = "0";
            if (infantPeopleCountTextBox.Text.Trim() == "") infantPeopleCountTextBox.Text = "0";

            if (adultPeopleCountTextBox.Text.Trim() == "0" && childPeopleCountTextBox.Text.Trim() == "0" && infantPeopleCountTextBox.Text.Trim() == "0")
            {
                MessageBox.Show("인원수는 필수 입력항목입니다.");
                adultPeopleCountTextBox.Focus();
                return;
            }

            DialogResult result = MessageBoxEx.Show("판매가격정보가 기본값으로 저장됩니다. 계속하시겠습니까?", "판매가격 기본값 설정", "예", "아니오");
            if (result == DialogResult.No)
            {
                return;
            }

            // 상품상세목록의 판매가격정보를 예약판매가격내역으로 Copy
            createDefaultReservationSalePriceInfo();

            // 판매가격입력 창에 기본값 설정 정보를 표시
            setReservationSalePriceList();
        }

        // 예약자명이 변경되면 기존의 고객번호를 초기화
        private void bookerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _customerNumber = "";
            customerEngNameTextBox.Text = "";
            cellphoneNumberTextBox.Text = "";
            emailIdTextBox.Text = "";
            domainComboBox.SelectedIndex = -1;
            domainTextBox.Text = "";
        }

        //=========================================================================================================================================================================
        // 핸드폰번호 옆 SMS팝업 연동
        //=========================================================================================================================================================================
        private void PopupSMS_RsvtDtls_Click(object sender, EventArgs e) {
            PopUpSendSMS popUpSendSMS = new PopUpSendSMS();

            popUpSendSMS.SetReceiverName(bookerNameTextBox.Text.Trim());
            popUpSendSMS.SetReceiverCellPhoneNo(cellphoneNumberTextBox.Text.Trim());
            // popUpSendSMS.SetCutOffType(_cutOffType);
            popUpSendSMS.SetReservationNo(_reservationNumber);
            popUpSendSMS.SetCustomerNumber(_customerNumber);
            popUpSendSMS.SetEmailAddress(emailIdTextBox + "@" + Utils.GetSelectedComboBoxItemText(domainComboBox));
            // popUpSendSMS.SetMessageType(IN_MSG_TYPE);
            popUpSendSMS.SetEmployeeNumber(_employeeNumber);

            popUpSendSMS.ShowDialog();
        }



        //=========================================================================================================================================================================
        // INVOICE BUTTON 출력 여부 판단 METHOD           --> 191028 박현호
        //=========================================================================================================================================================================
        private void isInvoiceButtonTrue()
        {
            ComboBoxItem cooperationItem = (ComboBoxItem)cooperativeCompanyComboBox.SelectedItem;       // 모객업체 선택  ITEM

            if(cooperationItem == null)
            {
                cooperativeCompanyComboBox.SelectedIndex = 0;
                cooperationItem = (ComboBoxItem)cooperativeCompanyComboBox.SelectedItem;
            }
               
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // INVOICE 생성 BUTTON 의 출력 여부판단      --> 모객업체 구분 지브리지 일 경우 출력 (직판일 경우)
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------            

            string cooperationNumber = cooperationItem.Value.ToString().Trim();                                                  // 모객업체 선택  ITEM VALUE (모객업체 번호)            

            if (cooperationNumber != "0001")
            {
                bt_createInvoice.Visible = false;
            }
            else
            {
                bt_createInvoice.Visible = true;
            }            
          
        }







        //=========================================================================================================================================================================
        // INVOICE 생성 BUTTON CLICK EVENT METHOD         -->  191028 박현호
        //=========================================================================================================================================================================
        private void bt_createInvoice_Click(object sender, EventArgs e)
        {
            createInvoiceForBtoC();
        }

        //------------------------------------------------------------------------------------
        // INVOICE 생성 METHOD (B2C 직판에 관련하여 생성)      --> 191028 박현호
        //------------------------------------------------------------------------------------
        private void createInvoiceForBtoC()
        {                            
            string RSVT_NO = reservationNumberTextBox.Text;                                                                             // 예약번호
            string invoiceNumber = createInvoiceCNMB();                                                                                     // INVOICE 일련번호

            //-----------------------------------------------------------------------------------------------
            // 예약 상세화면에서 예약번호가 없으면 예약 등록이 되지 않은건이므로 INVOICE 생성 차단
            //-----------------------------------------------------------------------------------------------
            if (RSVT_NO.Equals(""))
            {
                MessageBox.Show("예약 등록 후 INVOICE 를  생성할 수 있습니다.");
                return;
            }

            //---------------------------------------------------------------------------------------------------
            // 미지급내역 등록 확인, 해당 예약번호로 등록된 미지급내역 DATA 가 없으면 INVOICE 생성 차단
            //---------------------------------------------------------------------------------------------------
            string selectQuery = "SELECT IFNULL(COUNT(*),0) AS COUNT FROM TB_UNPA_D WHERE RSVT_NO = " + RSVT_NO;
            DataSet ds = DbHelper.SelectQuery(selectQuery);
            DataRow rowFromUnpaid = ds.Tables[0].Rows[0];
            int COUNT = int.Parse(rowFromUnpaid["COUNT"].ToString().Trim());
            if(COUNT == 0)
            {
                MessageBox.Show("미지급내역 저장 후 INVOICE 를 생성할 수 있습니다.");
                return;
            }

            //---------------------------------------------------------------------------------------------------
            // INVOICE 생성 DATA 를 조회
            //---------------------------------------------------------------------------------------------------
            string message = "[Invoice]";
            ComboBoxItem companyItem = (ComboBoxItem)cooperativeCompanyComboBox.SelectedItem;
            ComboBoxItem productItem = (ComboBoxItem)productComboBox.SelectedItem;

            string IN_CMPN_NO = companyItem.Value.ToString().Trim();
            string IN_PRDT_CNMB = productItem.Value.ToString().Trim();
            string IN_DPTR_FROM_DT = departureDateTimePicker.Text.ToString().Trim();                        

            string selectInvoiceTarget = string.Format("CALL SelectInvoiceForDetail('{0}','{1}','{2}','{3}','{4}')", IN_CMPN_NO, IN_PRDT_CNMB, IN_DPTR_FROM_DT, IN_DPTR_FROM_DT, RSVT_NO);
            DataSet ds2 = DbHelper.SelectQuery(selectInvoiceTarget);
            int rowCount2 = ds2.Tables[0].Rows.Count;
            if (rowCount2 == 0)
            {
                message += "INVOICE 생성 조건 DATA 가 존재하지 않습니다.";
                MessageBox.Show(message);
            }
            else
            {
                //---------------------------------------------------------------------------------------------------
                // INVOICE 생성 시도시 조회되는 INVOICE 대상 DATA 가 1건 이상이면 진행 중지 (이상현상)
                //---------------------------------------------------------------------------------------------------
                if (rowCount2 > 1)
                {
                    MessageBox.Show("생성 대상 INVOICE 조회 DATA 가 1건 이상입니다.", "경고");
                    return;                   
                }

                //---------------------------------------------------------------------------------------------------
                // 검색 결과 DATA 를 기반으로 INVOICE 생성
                //---------------------------------------------------------------------------------------------------
                for (int i = 0; i < rowCount2; i++)
                {
                    DataRow row = ds2.Tables[0].Rows[i];
                    string RSVT_NO_GET = row["RSVT_NO"].ToString().Trim();
                    string DPTR_DT = row["DPTR_DT"].ToString().Trim().Substring(0, 10);
                    string TOT_SALE_AMT = row["TOT_SALE_AMT"].ToString().Trim();
                    string PRDT_CNMB = row["PRDT_CNMB"].ToString().Trim();
                    string PRDT_NM = row["PRDT_NM"].ToString().Trim();
                    string CUR_SYBL = row["CUR_SYBL"].ToString().Trim();
                    string CUR_CD = row["CUR_CD"].ToString().Trim();
                    string INVC_EXRT = row["INVC_EXRT"].ToString().Trim();
                    string INVC_AMT = row["INVC_AMT"].ToString().Trim().Substring(0, row["INVC_AMT"].ToString().Trim().LastIndexOf("."));
                    string CMPN_NO = row["CMPN_NO"].ToString().Trim();
                    string CMPN_NM = row["CMPN_NM"].ToString().Trim();                    
                    string INVC_YN = row["INVC_YN"].ToString().Trim();
                    string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;
                    
                    // INVOICE 생성 PROCEDURE 호출                                        
                    string createInvoiceQuery = string.Format("CALL InsertInvoice('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", RSVT_NO, invoiceNumber, PRDT_CNMB, CUR_CD, CMPN_NO, Utils.removeComma(TOT_SALE_AMT), INVC_EXRT, Utils.removeComma(INVC_AMT), DPTR_DT, "", FRST_RGTR_ID);
                    int result = DbHelper.ExecuteNonQuery(createInvoiceQuery);
                    if (result > 0)
                    {
                        MessageBox.Show("INVOICE 생성 완료");
                    }                    
                }

                //--------------------------------------------------------------------------------------------------------
                // INVOICE 문서 POPUP 출력
                //--------------------------------------------------------------------------------------------------------
                PopUpInvoiceDocumentForm form = new PopUpInvoiceDocumentForm();
                form.setCooperationInfo(companyItem);
                form.setProductInfo(productItem);
                form.setDepartureDate(IN_DPTR_FROM_DT);
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();                
            }
        }






        //-----------------------------------------------------
        // Invoice 일련번호 채번
        // 1. select 하여 일련번호의 Max + 1 값을 가져옴
        // 2. null 일 경우 1번부터
        // 3. null 이 아닐경우 가져온 값을 채번 Data 로 사용
        //-----------------------------------------------------
        public string createInvoiceCNMB()
        {
            string INVC_CNMB = "";
            string selectQuery = "SELECT MAX(INVC_CNMB)+1 AS INVC_CNMB FROM TB_INVC_D";
            DataSet ds = DbHelper.SelectQuery(selectQuery);
            DataRow row = ds.Tables[0].Rows[0];
            INVC_CNMB = row["INVC_CNMB"].ToString().Trim();
            if (INVC_CNMB.Equals(""))
            {
                INVC_CNMB = "1";
            }

            return INVC_CNMB;
        }





        //============================================================================================================
        // 입력한 고객명으로 등록된 예약건 검색 Button Click Event Method           --> 191105 박현호
        //============================================================================================================
        private void searchCustomerReservationInfo_Click(object sender, EventArgs e)
        {
            serarchCustomerReservation();
        }

        //--------------------------------------------------------------------------------------------
        // 입력한 고객명으로 등록된 예약건 조회     --> 191105 박현호
        //--------------------------------------------------------------------------------------------
        private void serarchCustomerReservation()
        {
            PopUpSearchReservationInfo form = new PopUpSearchReservationInfo();

            form.SetCustomerName(bookerNameTextBox.Text.Trim());

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            // 선택된 예약번호를 화면에 표시
            if (form.GetReservationNumber() != "")
            {
                _reservationNumber = form.GetReservationNumber();
                reservationNumberTextBox.Text = _reservationNumber;


                // 예약정보를 화면에 표시
                setReservationInfoToForm();

                // 선택한 고객이 있을경우에만 저장버튼의 Enable 속성을 True 로 놔야함...
                string reservationNumber = form.GetReservationNumber();
                if (reservationNumber != "")
                {
                    saveReservationDetailButton.Enabled = true;
                    insertNewReservationDetailButton.Enabled = false;
                }
            }         
        }
    }
}