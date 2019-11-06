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
    // 예약기본 VO
    public partial class PopUpInsertReservationFromPurchaseList : Form
    {
        // 처리과정 중에 발생한 예외사항을 사용자에게 안내하기 위한 문구자료
        List<string> _userMessageText = new List<string>();

        private bool _exceptionOccurred = false;
        private bool _overflowReservationSequenceNo = false;

        // 예약기본 VO 클래스
        VOReservationDetail vOReservationDetail = new VOReservationDetail();

        // 구매자목록의 동일 행을 하나의 예약번호에 담기 위한 List 배열 선언
        List<List<string>> arrayPurchaseItem = new List<List<string>>();

        public PopUpInsertReservationFromPurchaseList()
        {
            InitializeComponent();
        }

        private void PopUpInsertReservationFromPurchaseList_Load(object sender, EventArgs e)
        {
            // 입력폼 초기화
            resetReservationBasicInfoField();
        }

        //================================================================================================================================================
        // 입력폼 초기화
        //================================================================================================================================================
        private void resetReservationBasicInfoField()
        {
            PurchaseDateFromDateTimePicker.Value = DateTime.Now;
            PurchaseDateToDateTimePicker.Value = DateTime.Now;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //================================================================================================================================================
        // 실행버튼 클릭
        //================================================================================================================================================
        private void excuteButton_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBoxEx.Show("예약전환을 실행하시겠습니까?", "구매자목록 예약전환", "예", "아니오");
            if (result == DialogResult.No)
            {
                this.Close();
                return;
            }

            // 구매자목록 테이블을 검색하여 예약기본 테이블을 생성
            bool resultTran = insertReservationInfo();
            

            // 사용자의 예약 전환 실행 의사에 따라 동작
            if (!resultTran)
            {                
                return;
            }

            // 예약전환 실행 도중 발생하는 Exception  상황에 따라 동작
            if (_exceptionOccurred == true || _userMessageText.Count > 0)
            {                
                showUserMessage();
            }
            else
            {
                MessageBox.Show("예약전환이 완료되었습니다.");
            }
        }

        //================================================================================================================================================
        // 구매자목록 테이블을 검색하여 예약기본 테이블을 생성
        //================================================================================================================================================
        private bool insertReservationInfo()
        {
            string FROM_PRCS_DT = PurchaseDateFromDateTimePicker.Text.Substring(0,10);  // 구매일자 시작일
            string TO_PRCS_DT = PurchaseDateToDateTimePicker.Text.Substring(0, 10);     // 구매일자 종료일

            string PRCS_CNMB = "";                                             // 구매일련번호
            string PRCS_DT = "";                                               // 구매일자
            string PRDT_CNMB = "";                                             // 상품일련번호
            string PRDT_NM = "";                                               // 상품명
            string CMPN_NO = "";                                               // 모객업체번호
            string CMPN_NM = "";                                               // 모객업체명
            string ORDR_NO = "";                                               // 주문번호
            string TCKT_NO = "";                                               // 티켓번호
            string ORDE_CUST_NM = "";                                          // 주문자고객명
            string ORDE_CTIF_PHNE_NO = "";                                     // 주문자연락처전화번호
            string ORDE_EMAL_ADDR = "";                                        // 주문자이메일주소
            string OPTN_NM = "";                                               // 옵션명
            string PRCS_AMT = "";                                              // 구매금액
            string RPRS_ENG_NM = "";                                           // 대표자영문명
            string AGE_CNTS = "";                                              // 연령내용
            string REMK_CNTS = "";                                             // 비고내용
            string RSVT_RGST_YN = "";                                          // 예약등록여부

            bool procStarted = true;
            string beforeProductNo = "";
            string beforeCompanyNo = "";
            string beforeOrderNo = "";
            string beforeOrderCustomerName = "";
            string beforeOrderTelNo = "";
            string beforeOrderEmailAddress = "";

            int arraySize = 0;

            //------------------------------------------------------------------------------------------------------------------
            // 파일에서 저장한 구매자 목록 Data 전체를 읽어옴...
            //------------------------------------------------------------------------------------------------------------------
            string selectQuery = "SELECT IFNULL(COUNT(*),0) AS COUNT FROM TB_CNSM_L WHERE RSVT_RGST_YN='N'";
            DataSet ds = DbHelper.SelectQuery(selectQuery);
            int rowCount = ds.Tables[0].Rows.Count;                 // 예약 전환이 되지않은 등록된 구매자 목록 전체 개수
            DataRow row = ds.Tables[0].Rows[0];
            int COUNT = int.Parse(row["COUNT"].ToString().Trim()); 

            //------------------------------------------------------------------------------------------------------------------
            // 191021 - 박현호
            // 설정한 구매기간 내에 존재하는 예약 전환 대상 Data 를 조회
            //------------------------------------------------------------------------------------------------------------------
            string query = string.Format("CALL SelectPurchaseList ('{0}', '{1}')", FROM_PRCS_DT, TO_PRCS_DT);
            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("구매자목록 정보를 가져올 수 없습니다.");
                return false;
            }            

            int rowCount2 = dataSet.Tables[0].Rows.Count;                                                                                               // 예약 전환 조건에 해당되는 구매자 목록 Data 건수

            if (rowCount2 == 0)
            {
                MessageBox.Show("기간내 예약 전환 처리 대상 Data 가 존재하지 않습니다.");
                return false;
            }

            //-----------------------------------------------------------------------------------------------------------------------------------
            // 191021 - 박현호
            // 등록된 구매자 목록 Data 개수와 예약전환 조건 Data 개수를 비교하여 사용자에게 예약전환 작업을 계속 수행할 것인지 물어봄
            //-----------------------------------------------------------------------------------------------------------------------------------
            if (COUNT != rowCount2)
            {
                if (MessageBox.Show("예약전환 기간 외 미처리 Data 가 존재합니다..\n▶예약전환대상 전체 Data 개수 : " + COUNT + "\n▶기간내 대상 Data 개수 : " + rowCount2 + "\n진행하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return false;
                }
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                PRCS_CNMB = datarow["PRCS_CNMB"].ToString().Trim();
                PRDT_CNMB = datarow["PRDT_CNMB"].ToString().Trim();
                PRDT_NM = datarow["PRDT_NM"].ToString().Trim();
                PRCS_DT = datarow["PRCS_DT"].ToString().Trim();
                CMPN_NO = datarow["CMPN_NO"].ToString().Trim();
                CMPN_NM = datarow["CMPN_NM"].ToString().Trim();
                ORDR_NO = datarow["ORDR_NO"].ToString().Trim();
                TCKT_NO = datarow["TCKT_NO"].ToString().Trim();
                ORDE_CUST_NM = datarow["ORDE_CUST_NM"].ToString().Trim();
                ORDE_CTIF_PHNE_NO = datarow["ORDE_CTIF_PHNE_NO"].ToString().Trim();
                ORDE_EMAL_ADDR = datarow["ORDE_EMAL_ADDR"].ToString().Trim();
                OPTN_NM = datarow["OPTN_NM"].ToString().Trim();
                PRCS_AMT = datarow["PRCS_AMT"].ToString().Trim();
                RPRS_ENG_NM = datarow["RPRS_ENG_NM"].ToString().Trim();
                AGE_CNTS = datarow["AGE_CNTS"].ToString().Trim();
                REMK_CNTS = datarow["REMK_CNTS"].ToString().Trim();
                RSVT_RGST_YN = datarow["RSVT_RGST_YN"].ToString().Trim();               

                if (procStarted)
                {
                    beforeProductNo = PRDT_CNMB;
                    beforeCompanyNo = CMPN_NO;
                    beforeOrderNo = ORDR_NO;
                    beforeOrderCustomerName = ORDE_CUST_NM;
                    beforeOrderTelNo = ORDE_CTIF_PHNE_NO;
                    beforeOrderEmailAddress = ORDE_EMAL_ADDR;

                    procStarted = false;
                    arraySize = 0;
                }

                // 이전값과 다르면 예약기본 테이블을 생성하고, 같으면 배열에 구매자파일정보를 추가
                if (beforeProductNo.Equals(PRDT_CNMB) && 
                    beforeCompanyNo.Equals(CMPN_NO) && 
                    //beforeOrderNo.Equals(ORDR_NO) && 
                    beforeOrderCustomerName.Equals(ORDE_CUST_NM) && 
                    beforeOrderTelNo.Equals(ORDE_CTIF_PHNE_NO))
                    //beforeOrderEmailAddress.Equals(ORDE_EMAL_ADDR))
                {
                    arraySize++;

                    arrayPurchaseItem.Add(new List<string>());

                    arrayPurchaseItem[arraySize - 1].Add(PRCS_CNMB);
                    arrayPurchaseItem[arraySize - 1].Add(PRDT_CNMB);
                    arrayPurchaseItem[arraySize - 1].Add(PRDT_NM);
                    arrayPurchaseItem[arraySize - 1].Add(PRCS_DT);
                    arrayPurchaseItem[arraySize - 1].Add(CMPN_NO);
                    arrayPurchaseItem[arraySize - 1].Add(CMPN_NM);
                    arrayPurchaseItem[arraySize - 1].Add(ORDR_NO);
                    arrayPurchaseItem[arraySize - 1].Add(TCKT_NO);
                    arrayPurchaseItem[arraySize - 1].Add(ORDE_CUST_NM);
                    arrayPurchaseItem[arraySize - 1].Add(ORDE_CTIF_PHNE_NO);
                    arrayPurchaseItem[arraySize - 1].Add(ORDE_EMAL_ADDR);
                    arrayPurchaseItem[arraySize - 1].Add(OPTN_NM);
                    arrayPurchaseItem[arraySize - 1].Add(PRCS_AMT);
                    arrayPurchaseItem[arraySize - 1].Add(RPRS_ENG_NM);
                    arrayPurchaseItem[arraySize - 1].Add(AGE_CNTS);
                    arrayPurchaseItem[arraySize - 1].Add(REMK_CNTS);
                }
                else
                {

                    if (insertReservationItem() == false)
                    {
                        _exceptionOccurred = true;
                        if (_overflowReservationSequenceNo == true)
                        {
                            _userMessageText.Add("예약건수가 99건을 초과했습니다. 처리를 종료합니다.");
                            return false;
                        }
                    }

                    beforeProductNo = PRDT_CNMB;
                    beforeCompanyNo = CMPN_NO;
                    beforeOrderNo = ORDR_NO;
                    beforeOrderCustomerName = ORDE_CUST_NM;
                    beforeOrderTelNo = ORDE_CTIF_PHNE_NO;
                    beforeOrderEmailAddress = ORDE_EMAL_ADDR;

                    arrayPurchaseItem.Clear();

                    arraySize = 1;

                    arrayPurchaseItem.Add(new List<string>());

                    arrayPurchaseItem[arraySize - 1].Add(PRCS_CNMB);
                    arrayPurchaseItem[arraySize - 1].Add(PRDT_CNMB);
                    arrayPurchaseItem[arraySize - 1].Add(PRDT_NM);
                    arrayPurchaseItem[arraySize - 1].Add(PRCS_DT);
                    arrayPurchaseItem[arraySize - 1].Add(CMPN_NO);
                    arrayPurchaseItem[arraySize - 1].Add(CMPN_NM);
                    arrayPurchaseItem[arraySize - 1].Add(ORDR_NO);
                    arrayPurchaseItem[arraySize - 1].Add(TCKT_NO);
                    arrayPurchaseItem[arraySize - 1].Add(ORDE_CUST_NM);
                    arrayPurchaseItem[arraySize - 1].Add(ORDE_CTIF_PHNE_NO);
                    arrayPurchaseItem[arraySize - 1].Add(ORDE_EMAL_ADDR);
                    arrayPurchaseItem[arraySize - 1].Add(OPTN_NM);
                    arrayPurchaseItem[arraySize - 1].Add(PRCS_AMT);
                    arrayPurchaseItem[arraySize - 1].Add(RPRS_ENG_NM);
                    arrayPurchaseItem[arraySize - 1].Add(AGE_CNTS);
                    arrayPurchaseItem[arraySize - 1].Add(REMK_CNTS);
                }

            }  // End of foreach

            // 맨끝 처리 대상의 잔여분이 남아 있을 때 추가 예약 등록 처리 (테이블 값의 이전 값 비교 시에 남은 미처리분)
            if (arrayPurchaseItem.Count > 0)
            {
                if (insertReservationItem() == false)
                {
                    return false;
                }
            }

            return true;
        }

        //================================================================================================================================================
        // 예약기본 테이블 생성
        //================================================================================================================================================
        private bool insertReservationItem()
        {
            string PRCS_CNMB = "";                                             // 구매일련번호
            string PRCS_DT = "";                                               // 구매일자
            string PRDT_CNMB = "";                                             // 상품일련번호
            string PRDT_NM = "";                                               // 상품명
            string CMPN_NO = "";                                               // 모객업체번호
            string CMPN_NM = "";                                               // 모객업체명
            string ORDR_NO = "";                                               // 주문번호
            string TCKT_NO = "";                                               // 티켓번호
            string ORDE_CUST_NM = "";                                          // 주문자고객명
            string ORDE_CTIF_PHNE_NO = "";                                     // 주문자연락처전화번호
            string ORDE_EMAL_ADDR = "";                                        // 주문자이메일주소
            string OPTN_NM = "";                                               // 옵션명
            string PRCS_AMT = "";                                              // 구매금액
            string RPRS_ENG_NM = "";                                           // 대표자영문명
            string AGE_CNTS = "";                                              // 연령내용
            string REMK_CNTS = "";                                             // 비고내용
            string CUST_RQST_CNTS = "";                                        // 고객요청내용

            string reservationNumber = "";                                     // 예약번호

            bool procFirst = true;                                             // 배열의 첫건 처리를 위한 flag
            bool mainProductExists = false;                                    // 옵션만 들어온 경우 예약목록을 생성할 수 없어 체크하기 위한 용도

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 예약기본 테이블 생성용 배열 선언
            //---------------------------------------------------------------------------------------------------------------------------------------
            PRCS_CNMB = arrayPurchaseItem[0][0].Trim();
            PRDT_CNMB = arrayPurchaseItem[0][1].Trim();
            PRDT_NM = arrayPurchaseItem[0][2].Trim();
            PRCS_DT = arrayPurchaseItem[0][3].Trim();
            CMPN_NO = arrayPurchaseItem[0][4].Trim();
            CMPN_NM = arrayPurchaseItem[0][5].Trim();
            ORDR_NO = arrayPurchaseItem[0][6].Trim();
            TCKT_NO = arrayPurchaseItem[0][7].Trim();
            ORDE_CUST_NM = arrayPurchaseItem[0][8].Trim();
            ORDE_CTIF_PHNE_NO = arrayPurchaseItem[0][9].Trim();
            ORDE_EMAL_ADDR = arrayPurchaseItem[0][10].Trim();

            // 이메일이 유효하지 않으면 공백으로 초기화                   --> 암호화 처리시 문제가 되어 미사용
            //if (Utils.IsValidEmail(ORDE_EMAL_ADDR) == false)
            //{
            //     ORDE_EMAL_ADDR = "";
            //}

            RPRS_ENG_NM = arrayPurchaseItem[0][13].Trim();
            AGE_CNTS = arrayPurchaseItem[0][14].Trim();
            REMK_CNTS = arrayPurchaseItem[0][15].Trim();

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 예약판매가격내역 생성용 배열 선언
            //---------------------------------------------------------------------------------------------------------------------------------------
            List<string> listReservationAdultDivisionCode = new List<string>();
            List<string> listReservationSalePriceName = new List<string>();
            List<double> listReservationSalePriceAmount = new List<double>();
            List<int> listReservationSalePricePeopleCount = new List<int>();

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 예약옵션내역 생성용 배열 선언
            //---------------------------------------------------------------------------------------------------------------------------------------
            List<string> listReservationOptionAdultDivisionCode = new List<string>();
            List<string> listReservationOptionName = new List<string>();
            List<double> listReservationOptionAmount = new List<double>();
            List<int> listReservationOptionPeopleCount = new List<int>();

            // 고객요청사항 조합
            CUST_RQST_CNTS = REMK_CNTS + Environment.NewLine + AGE_CNTS;
            /*
            if (PRDT_NM.IndexOf("하노이통킨쇼") > 0) PRDT_NM = "하노이통킨쇼";
            if (PRDT_NM.IndexOf("프레지던트크루즈") > 0) PRDT_NM = "프레지던트크루즈";
            if (PRDT_NM.IndexOf("럭셔리데이크루즈") > 0) PRDT_NM = "럭셔리데이크루즈";
            */
            //---------------------------------------------------------------------------------------------------------------------------------------
            // 예약기본정보 VO 초기화
            //---------------------------------------------------------------------------------------------------------------------------------------
            clearVOReservationDetail();

            vOReservationDetail.PRDT_CNMB = PRDT_CNMB;
            vOReservationDetail.ORDR_NO = ORDR_NO;

            if(PRCS_DT.Length > 10)
            {
                vOReservationDetail.PRCS_DTM = PRCS_DT.Substring(0, 10);
            } else
            {
                vOReservationDetail.PRCS_DTM = PRCS_DT;
            }
            
            vOReservationDetail.TKTR_CMPN_NO = CMPN_NO;
            vOReservationDetail.ORDE_CTIF_PHNE_NO = ORDE_CTIF_PHNE_NO;
            vOReservationDetail.ORDE_EMAL_ADDR = ORDE_EMAL_ADDR;
            vOReservationDetail.RPRS_ENG_NM = RPRS_ENG_NM;
            vOReservationDetail.CUST_RQST_CNTS = CUST_RQST_CNTS;
            vOReservationDetail.RSVT_STTS_CD = "1";                          // 예약상태: 미확정

            string productGradeName = "";

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 상품등급, 가격, 성인여부 판단
            //---------------------------------------------------------------------------------------------------------------------------------------
            for (int arrayItem = 0; arrayItem < arrayPurchaseItem.Count; arrayItem++)
            {
                OPTN_NM = arrayPurchaseItem[arrayItem][11].Trim();
                PRCS_AMT = arrayPurchaseItem[arrayItem][12].Trim();

                //---------------------------------------------------------------------------------------------------------------------------------------
                // Loop 처리 내부 Working 변수 선언
                //---------------------------------------------------------------------------------------------------------------------------------------

                string[] optionName;
                string[] optionName1;
                string[] optionName2;
                string optionCountText1 = "";
                string optionCountText2 = "";

                int optionCount = 0;

                string productGradeNameAndAgeDivision = "";
                string[] optionSpec;
                string realOptionInfo = "";
                string realOptionName = "";
                string ageText = "";
                string ageFullText = "";

                productGradeName = "";

                int numberOfAdult = 0;
                int numberOfChild = 0;
                int numberOfInfant = 0;

                //---------------------------------------------------------------------------------------------------------------------------------------
                // 상품과 모객매체에 따라 옵션, 상품등급코드, 성인/소아/유아 구분 판단
                //---------------------------------------------------------------------------------------------------------------------------------------
                switch (CMPN_NM)
                {
                    case "티몬":
                        if (PRDT_NM.Equals("통킨쇼"))
                        {
                            // 옵션명을 | 문자열로 분리
                            optionName = OPTN_NM.Split(new char[] { '|' });

                            vOReservationDetail.DPTR_DT = optionName[0].Substring(0, 10);                      // 출발일자
                            productGradeNameAndAgeDivision = optionName[1].Trim();                             // 상품등급 (ex: A1. 쇼(실버석) + 셔틀포함)

                            // 옵션 및 성인/소아 구분 추출
                            optionSpec = productGradeNameAndAgeDivision.Split(new char[] { '+' });             // 상품과 옵션을 분리

                            // 배열이 1건이면 성인소아 구분으로 판단
                            int optionSpecArraySize = optionSpec.GetLength(0);
                            if (optionSpecArraySize == 1)
                            {
                                if (optionSpec[0].Equals("성인") && optionSpec[0].Equals("소아") && optionSpec[0].Equals("유아")) {
                                    ageText = optionSpec[0].Trim();
                                }
                            }

                            // 옵션이 여러 개인 경우 옵션을 문자열로 묶기
                            else if (optionSpecArraySize > 1)
                            {
                                for (int jj = 1; jj < optionSpec.Length; jj++)
                                {
                                    realOptionInfo = realOptionInfo + " " + optionSpec[jj].Trim();
                                }
                            }

                            int len2 = optionSpec[0].Length - 5;
                            string productGradeName1 = optionSpec[0].Trim();
                            productGradeName = productGradeName1.Substring(4, len2);                           // 상품등급

                            if (productGradeName.Contains("실버") == true)
                            {
                                productGradeName = "SILVER";
                            }
                            else if (productGradeName.Contains("골드") == true)
                            {
                                productGradeName = "GOLD";
                            }

                            // 성인소아 여부 판단 ('|'로 문자열을 나눈 배열의 맨끝이 성인소아 구분)
                            ageText = optionName[optionName.Length - 1];                                       // '성인'만 추출

                            if (ageText.Equals("성인"))
                            {
                                ageText = "성인";
                                numberOfAdult = 1;
                            }

                            if (ageText.Equals("소아"))
                            {
                                ageText = "소아";
                                numberOfChild = 1;
                            }
                            if (ageText.Equals("유아"))
                            {
                                ageText = "유아";
                                numberOfInfant = 1;
                            }

                            mainProductExists = true;
                        }
                        else if(PRDT_NM.Equals("프레지던트크루즈"))
                        {
                            //-----------------------------------------------------------------------------------------------------------------------------------
                            // 예시: 2019-10-16(수)|B. 프리미어 발코니 1박2일|1. 성인(2인1실기준) 1인요금
                            //-----------------------------------------------------------------------------------------------------------------------------------
                            // 옵션명을 | 문자열로 분리 (첫번째: 출발일자, 두번째: 등급, 세번째: 성인구분)
                            optionName = OPTN_NM.Split(new char[] { '|' });

                            vOReservationDetail.DPTR_DT = optionName[0].Substring(0, 10);                      // 출발일자

                            // 상품등급 판단
                            productGradeName = optionName[1].Trim();                                           // 상품등급 (ex: B. 프리미어 발코니 1박2일)
                            if (productGradeName.IndexOf("프리미어 발코니") > 0) productGradeName = "PREMIER";
                            if (productGradeName.IndexOf("앰버서더 발코니") > 0) productGradeName = "AMBASSADOR";

                            // 성인/소아 판단
                            ageText = optionName[2].Trim();                                                    // 1. 성인(2인1실기준) 1인요금
                            if (ageText.IndexOf("성인") > 0)
                            {
                                ageText = "성인";
                                numberOfAdult = 1;
                            }

                            if (ageText.IndexOf("소아") > 0)
                            {
                                ageText = "소아";
                                numberOfChild = 1;
                            }
                            if (ageText.IndexOf("유아") > 0)
                            {
                                ageText = "유아";
                                numberOfInfant = 1;
                            }

                            mainProductExists = true;
                        }
                        else if(PRDT_NM.Equals("럭셔리 데이 크루즈"))
                        {
                            //-----------------------------------------------------------------------------------------------------------------------------------
                            // 예시: 2019-06-11(화)|A1. 데이 크루즈+디럭스 캐빈(2인1실기준)
                            //       2019-08-24(토)|C1. 소아 추가비용(만5~11세)
                            //       2019-08-24(토)|C2. 유아 추가비용(만4세이하)
                            //-----------------------------------------------------------------------------------------------------------------------------------
                            // 옵션명을 | 문자열로 분리 (첫번째: 출발일자, 두번째: 등급, 세번째: 성인구분)
                            optionName = OPTN_NM.Split(new char[] { '|' });

                            vOReservationDetail.DPTR_DT = optionName[0].Substring(0, 10);                      // 출발일자

                            // 데이 크루즈인 경우
                            if (optionName[1].IndexOf("데이 크루즈") > 0)
                            {
                                optionSpec = optionName[1].Split(new char[] { '+' });         // 상품등급 (ex: A1. 데이 크루즈+디럭스 캐빈(2인 1실기준))
                                productGradeName = optionSpec[1];                             // 디럭스 캐빈(2인 1실기준)

                                if (productGradeName.Contains("디럭스 발코니") == true)
                                {
                                    productGradeName = "DLX BALCONY";
                                }
                                else if (productGradeName.Contains("디럭스 캐빈") == true)
                                {
                                    productGradeName = "DLX WINDOW";
                                }
                                /*
                                if (productGradeName.IndexOf("디럭스 발코니") > 0)
                                {
                                    productGradeName = "DLX BALCONY";
                                }
                                else if (productGradeName.IndexOf("디럭스 캐빈") > 0)
                                {
                                    productGradeName = "DLX WINDOW";
                                }
                                */
                                ageText = "성인";
                                numberOfAdult = 1;
                                ageFullText = ageFullText + productGradeName;
                            }
                            else if  (optionName[1].IndexOf("소아 추가비용") > 0)
                            {
                                ageText = "소아";
                                numberOfChild = 1;
                                ageFullText = ageFullText + optionName[1];
                            }
                            else if (optionName[1].IndexOf("유아 추가비용") > 0)
                            {
                                ageText = "유아";
                                numberOfInfant = 1;
                                ageFullText = ageFullText + optionName[1];
                            }

                            if (ageText.IndexOf("성인") > 0) ageText = "성인";
                            if (ageText.IndexOf("소아") > 0) ageText = "소아";
                            if (ageText.IndexOf("유아") > 0) ageText = "유아";

                            mainProductExists = true;
                        }
                        break;
                    // End of 티몬
                    case "위메프":
                        //--------------------------------------------------------------------------------------------------------------------------------------------------
                        //[구분^출발일 : 01-2. [하롱베이 럭셔리 데이 크루즈] 디럭스 발코니 캐빈(2인1실) / 1인요금^2019-07-29 : 1개] 
                        //[구분^출발일 : 02-1. [추가옵션] 소아 1인 추가^2019-07-21 : 1개] 
                        //[구분^출발일 : 02-2. [추가옵션] 유아 1인 추가^2019-05-25 : 1개] 
                        //--------------------------------------------------------------------------------------------------------------------------------------------------
                        if (PRDT_NM.Equals("럭셔리 데이 크루즈"))
                        {
                            if (OPTN_NM.IndexOf("하롱베이 럭셔리 데이 크루즈") > 0)
                            {
                                // 옵션명을 ^ 문자열로 분리 (분리결과: "[구분", "출발일 : 01-2. [하롱베이 럭셔리 데이 크루즈] 디럭스 발코니 캐빈(2인1실) / 1인요금", "2019-07-29 : 1개]"
                                optionName = OPTN_NM.Split(new char[] { '^' });

                                // 상품등급 판단
                                string productGradeName1 = optionName[1].Substring(0, 10);               // 출발일 : 01-2. [하롱베이 럭셔리 데이 크루즈] 디럭스 발코니 캐빈(2인1실) / 1인요금
                                if (optionName[1].IndexOf("디럭스 발코니 캐빈(2인1실)") > 0)
                                {
                                    productGradeName = "DLX BALCONY";
                                }
                                else if (optionName[1].IndexOf("디럭스 캐빈(2인1실)") > 0)
                                {
                                    productGradeName = "DLX WINDOW";
                                }

                                mainProductExists = true;

                                vOReservationDetail.DPTR_DT = optionName[2].Substring(0, 10);                  // 출발일자
                                ageText = "성인";

                                if (optionName[1].IndexOf("1인요금") > 0)
                                {
                                    numberOfAdult = 1;
                                }
                                else if (optionName[1].IndexOf("2인요금") > 0)
                                {
                                    numberOfAdult = 2;
                                }
                                else if (optionName[1].IndexOf("3인요금") > 0)
                                {
                                    numberOfAdult = 3;
                                }
                                else if (optionName[1].IndexOf("4인요금") > 0)
                                {
                                    numberOfAdult = 4;
                                }
                                else if (optionName[1].IndexOf("5인요금") > 0)
                                {
                                    numberOfAdult = 5;
                                }
                                else if (optionName[1].IndexOf("6인요금") > 0)
                                {
                                    numberOfAdult = 6;
                                }
                                else if (optionName[1].IndexOf("7인요금") > 0)
                                {
                                    numberOfAdult = 7;
                                }
                                else if (optionName[1].IndexOf("8인요금") > 0)
                                {
                                    numberOfAdult = 8;
                                }
                                else if (optionName[1].IndexOf("9인요금") > 0)
                                {
                                    numberOfAdult = 9;
                                }
                            }
                            else if (OPTN_NM.IndexOf("추가옵션") > 0)
                            {
                                int peopleCount = 0;

                                // 옵션명을 ^ 문자열로 분리 ([구분^출발일 : 02-1. [추가옵션] 소아 1인 추가^2019-07-21 : 1개])
                                optionName = OPTN_NM.Split(new char[] { '^' });

                                vOReservationDetail.DPTR_DT = optionName[2].Substring(0, 10);                  // 출발일자

                                // 소아/유아 판단
                                if (optionName[1].IndexOf("소아") > 0)
                                {
                                    ageText = "소아";
                                }
                                else if (optionName[1].IndexOf("유아") > 0)
                                {
                                    ageText = "유아";
                                }

                                // 인원수 판단
                                if (optionName[1].IndexOf("1인") > 0)
                                {
                                    peopleCount = 1;
                                }
                                else if (optionName[1].IndexOf("2인") > 0)
                                {
                                    peopleCount = 2;
                                }
                                else if (optionName[1].IndexOf("3인") > 0)
                                {
                                    peopleCount = 3;
                                }
                                else if (optionName[1].IndexOf("4인") > 0)
                                {
                                    peopleCount = 4;
                                }
                                else if (optionName[1].IndexOf("5인") > 0)
                                {
                                    peopleCount = 5;
                                }
                                else if (optionName[1].IndexOf("6인") > 0)
                                {
                                    peopleCount = 6;
                                }
                                else if (optionName[1].IndexOf("7인") > 0)
                                {
                                    peopleCount = 7;
                                }
                                else if (optionName[1].IndexOf("8인") > 0)
                                {
                                    peopleCount = 8;
                                }
                                else if (optionName[1].IndexOf("9인") > 0)
                                {
                                    peopleCount = 9;
                                }

                                if (ageText.Equals("소아"))
                                {
                                    numberOfChild = peopleCount;
                                }
                                else if (ageText.Equals("유아"))
                                {
                                    numberOfInfant = peopleCount;
                                }
                            }
                        }
                        else if (PRDT_NM.Equals("프레지던트크루즈"))  // [12%할인] 하롱베이 1박2일 크루즈[총주문 191 건] 사용수량 [총86 건]
                        {
                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            // [구분^출발일^기준 : 02-01. [하롱베이 크루즈] 프리미어 발코니 1박2일,^2019-07-31^1) 성인 (2인1실/1인요금) : 1개] 
                            // [구분^출발일^기준 : 03-01 [추가옵션] 바디 마사지 60분,^01,02옵션 구매시에만 추가 가능^성인 : 1개] 
                            // [구분^출발일^기준 : 01-01. [하롱베이 크루즈] 앰버서더 발코니 1박2일,^ 2019-06-25 ^ 3) 1인 추가(성인/ 소아 동일) : 1개] 
                            // [구분^출발일^기준 : 03-01 [추가옵션] 바디 마사지 60분,^ 01, 02옵션 구매시에만 추가 가능 ^ 성인 : 1개]
                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            if (OPTN_NM.IndexOf("하롱베이 크루즈") > 0)
                            {
                                // 옵션명을 ^ 문자열로 분리 
                                // 분리결과: "[구분", "출발일", "기준 : 02-01. [하롱베이 크루즈] 프리미어 발코니 1박2일", "2019-07-31", "1) 성인 (2인1실/1인요금) : 1개]
                                // 분리결과: "[구분", "출발일", "기준 : 01-01. [하롱베이 크루즈] 앰버서더 발코니 1박2일,", "2019-06-25", "3) 1인 추가(성인/소아 동일) : 1개]
                                optionName = OPTN_NM.Split(new char[] { '^' });

                                // 상품등급 판단
                                string productGradeName1 = optionName[2];
                                if (productGradeName1.IndexOf("프리미어 발코니") > 0)
                                {
                                    productGradeName = "PREMIER";
                                }
                                else if (productGradeName1.IndexOf("앰버서더 발코니") > 0)
                                {
                                    productGradeName = "AMBASSADOR";
                                }

                                mainProductExists = true;

                                vOReservationDetail.DPTR_DT = optionName[3].Substring(0,10);

                                // 성인판단-소아 또는 유아 추가 등 다른 예외가 있는지 확인 필요
                                if (optionName[4].IndexOf("성인/소아 동일") > 0)
                                {
                                    ageText = "성인";
                                    numberOfAdult = 1;
                                }
                                else if (optionName[4].IndexOf("성인") > 0)
                                {
                                    ageText = "성인";
                                    numberOfAdult = 1;
                                }
                            }
                            else if (OPTN_NM.IndexOf("추가옵션") > 0)
                            {
                                // 2인1실이면 0.5로 계산하고 추가옵션은 옵션내역에 insert처리
                                // 구분^출발일^기준 : 03-01 [추가옵션] 바디 마사지 60분,^01,02옵션 구매시에만 추가 가능^성인 : 1개]
                                // [구분^출발일 : 02-2. [추가옵션] 유아 1인 추가^2019-05-25 : 1개] 
                                // 분리결과: "[구분^출발일 : 02-2. [추가옵션" + " 유아 1인 추가^2019-05-25 : 1개" + " "
                                optionName = OPTN_NM.Split(new char[] { ']' });

                                // 3번째:  바디 마사지 60분,^01,02옵션 구매시에만 추가 가능^성인 : 1개
                                optionName1 = optionName[1].Split(new char[] { ',' });

                                // 옵션명 추출 (바디 마사지 60분)
                                realOptionName = optionName1[0].Trim();

                                // 건수 추출 (^01,02옵션 구매시에만 추가 가능^성인 : 1개 => "^01,02옵션 구매시에만 추가 가능^성인 " , "1개"
                                optionName2 = optionName1[2].Split(new char[] { ':' });
                                optionCountText1 = optionName2[1].Trim();
                                optionCountText2 = optionCountText1.Replace("개", "");
                                optionCount = Int16.Parse(optionCountText2);

                                // 성인/소아/유아 구분 판단
                                if (OPTN_NM.Contains("성인") == true)
                                {
                                    listReservationOptionAdultDivisionCode.Add("1");          // 성인
                                }
                                else if (OPTN_NM.Contains("소아") == true)
                                {
                                    listReservationOptionAdultDivisionCode.Add("2");          // 성인
                                }
                                else if (OPTN_NM.Contains("유아") == true)
                                {
                                    listReservationOptionAdultDivisionCode.Add("3");          // 성인
                                }

                                // 예약옵션내역 저장을 위한 리스트 처리
                                // 옵션명이 동일하면 인원수만 누적. 통화코드는 판매통화코드를 적용
                                if (listReservationOptionName.Count > 0)
                                {
                                    int itemIndex = listReservationOptionName.IndexOf(realOptionName);
                                    if (itemIndex > 0)
                                    {
                                        listReservationOptionPeopleCount[itemIndex] = listReservationOptionPeopleCount[itemIndex] + optionCount;
                                    } else
                                    {
                                        listReservationOptionName.Add(realOptionName);                 // 옵션명
                                        listReservationOptionAmount.Add(double.Parse(PRCS_AMT));       // 옵션가격
                                        listReservationOptionPeopleCount.Add(optionCount);             // 옵션인원수
                                    }
                                }
                                else
                                {
                                    listReservationOptionName.Add(realOptionName);                     // 옵션명
                                    listReservationOptionAmount.Add(double.Parse(PRCS_AMT));           // 옵션가격
                                    listReservationOptionPeopleCount.Add(optionCount);                 // 옵션인원수
                                }
                            }
                        }
                        else if (PRDT_NM.Equals("통킨쇼"))  // 베트남 하노이 통킨쇼 입장권[총주문 468 건] 미사용수량 [총393 건]
                        {
                            if (OPTN_NM.IndexOf("통킨쇼") > 0)
                            {
                                //--------------------------------------------------------------------------------------------------------------------------------------------------
                                // 옵션명: [구분^출발일^호텔 : 01-02. [하노이] 통킨쇼 실버석 (셔틀포함)^2019-09-12^성인 : 1개] 
                                // 옵션명을 ^ 문자열로 분리 
                                // 분리결과: "구분" + "출발일" + "호텔 : 01-02. [하노이] 통킨쇼 실버석 (셔틀포함)" + "2019-09-12" + "성인 : 1개]"
                                //--------------------------------------------------------------------------------------------------------------------------------------------------
                                // (뉴딜)옵션명: [구분^날짜를 선택하세요^성인/소아 : 01-01. [하노이] 통킨쇼 실버석 (셔틀포함)^2019-10-17^성인 : 1개] 
                                // 옵션명을 ^ 문자열로 분리 
                                // 분리결과: "구분" + "날짜를 선택하세요" + "성인/소아 : 01-01. [하노이] 통킨쇼 실버석 (셔틀포함) + "2019-10-17" + "성인 : 1개]"
                                //--------------------------------------------------------------------------------------------------------------------------------------------------
                                optionName = OPTN_NM.Split(new char[] { '^' });

                                // 상품등급 판단
                                string productGradeName1 = optionName[2];
                                if (productGradeName1.Contains("실버") == true)
                                {
                                    productGradeName = "SILVER";
                                }
                                else if (productGradeName1.Contains("골드") == true)
                                {
                                    productGradeName = "GOLD";
                                }

                                mainProductExists = true;

                                vOReservationDetail.DPTR_DT = optionName[3];

                                // 건수 추출 ("성인 : 1개]" ==> "성인" + 1개")
                                optionName2 = optionName[4].Split(new char[] { ':' });
                                optionCountText1 = optionName2[1].Trim();
                                optionCountText1 = optionCountText1.Replace("개", "");
                                optionCountText1 = optionCountText1.Replace("]", "");
                                optionCount = Int16.Parse(optionCountText1);

                                // 성인판단-소아 또는 유아 추가 등 다른 예외가 있는지 확인 필요
                                ageFullText = optionName[4].Trim();

                                if (ageFullText.Contains("성인"))
                                {
                                    ageText = "성인";
                                    numberOfAdult = optionCount;
                                }
                                else if (ageFullText.Contains("소아"))
                                {
                                    ageText = "소아";
                                    numberOfChild = optionCount;
                                }
                                else if (ageFullText.Contains("유아"))
                                {
                                    ageText = "유아";
                                    numberOfInfant = optionCount;
                                }
                            }
                        }
                        break;
                        // End of 위메프
                } // End of switch

                // 옵션정보를 고객요청사항에 포함
                if (CUST_RQST_CNTS != "") vOReservationDetail.CUST_RQST_CNTS = CUST_RQST_CNTS;
                if (realOptionInfo != "") vOReservationDetail.CUST_RQST_CNTS = vOReservationDetail.CUST_RQST_CNTS + Environment.NewLine + realOptionInfo.Trim();
                if (ageFullText != "") vOReservationDetail.CUST_RQST_CNTS = vOReservationDetail.CUST_RQST_CNTS + Environment.NewLine + ageFullText.Trim();
                vOReservationDetail.CUST_RQST_CNTS.Trim();

                //---------------------------------------------------------------------------------------------------------------------------------------
                // 성인소아 가격 판단
                //---------------------------------------------------------------------------------------------------------------------------------------
                if (ageText.Equals("성인"))
                {
                    vOReservationDetail.ADLT_NBR = vOReservationDetail.ADLT_NBR + numberOfAdult;
                    vOReservationDetail.ADLT_SALE_PRCE = Double.Parse(PRCS_AMT);
                } else if (ageText.Equals("소아"))
                {
                    vOReservationDetail.CHLD_NBR = vOReservationDetail.CHLD_NBR + numberOfChild;
                    vOReservationDetail.CHLD_SALE_PRCE = Double.Parse(PRCS_AMT);
                }
                else if (ageText.Equals("유아"))
                {
                    vOReservationDetail.INFN_NBR = vOReservationDetail.INFN_NBR + numberOfInfant;
                    vOReservationDetail.INFN_SALE_PRCE = Double.Parse(PRCS_AMT);
                }

                //---------------------------------------------------------------------------------------------------------------------------------------
                // 상품정보를 검색하여 판매통화코드를 Set (나머지 배열이 동일한 값이므로 한번만 실행)
                //---------------------------------------------------------------------------------------------------------------------------------------
                if (vOReservationDetail.PRDT_GRAD_CD == "" || vOReservationDetail.SALE_CUR_CD == "")
                {
                    var searchResult = searchProductInfo(vOReservationDetail.PRDT_CNMB, productGradeName, vOReservationDetail.DPTR_DT);
                    vOReservationDetail.PRDT_GRAD_CD = searchResult.Item1;                                               // 상품등급코드
                    vOReservationDetail.SALE_CUR_CD = searchResult.Item2;                                                // 판매통화코드
                    procFirst = false;

                    if (vOReservationDetail.PRDT_GRAD_CD == "" || vOReservationDetail.SALE_CUR_CD == "")
                    {
                        mainProductExists = false;
                    }
                }
            } // End of Loop

            if (mainProductExists == false)
            {
                _userMessageText.Add("주문내용에 상품정보가 없습니다. 옵션주문만 존재할 수도 있습니다." + " 주문번호: " + vOReservationDetail.ORDR_NO + " 예약자: " + ORDE_CUST_NM + " 상품: " + PRDT_NM + " 등급: " + productGradeName);
                return false;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 총판매금액 계산
            //---------------------------------------------------------------------------------------------------------------------------------------
            double adultSaleAmount = Math.Round(vOReservationDetail.ADLT_SALE_PRCE * vOReservationDetail.ADLT_NBR);
            double childSaleAmount = Math.Round(vOReservationDetail.CHLD_SALE_PRCE * vOReservationDetail.CHLD_NBR);
            double infantSaleAmount = Math.Round(vOReservationDetail.INFN_SALE_PRCE * vOReservationDetail.INFN_NBR);

            // 외화총판매금액
            vOReservationDetail.TOT_SALE_AMT = vOReservationDetail.TOT_SALE_AMT +
                                               adultSaleAmount +
                                               childSaleAmount +
                                               infantSaleAmount;
            // 소셜업체는 원화판매
            vOReservationDetail.WON_TOT_SALE_AMT = vOReservationDetail.TOT_SALE_AMT;                             // 원화총판매금액

            vOReservationDetail.SALE_SUM_AMT = vOReservationDetail.TOT_SALE_AMT;                                 // 판매합계금액 (옵션금액이 없으므로 총판매금액과 동일)
            vOReservationDetail.UNAM_BAL = vOReservationDetail.TOT_SALE_AMT;                                     // 미수잔액 (등록시에는 전부 미수금)

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 고객검색 및 신규등록
            //---------------------------------------------------------------------------------------------------------------------------------------
            vOReservationDetail.CUST_NO = searchCustomerInfo(ORDE_CUST_NM, RPRS_ENG_NM, ORDE_CTIF_PHNE_NO, ORDE_EMAL_ADDR);
            vOReservationDetail.CUST_NM = ORDE_CUST_NM;
            if (vOReservationDetail.CUST_NO == "")
            {
                _userMessageText.Add("고객 신규 등록 중에 오류가 발생했습니다. 운영담당자에게 연락하세요." + " 주문번호: " + vOReservationDetail.ORDR_NO + " 예약자: " + ORDE_CUST_NM);
                return false;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 예약기본 테이블 신규 등록
            //---------------------------------------------------------------------------------------------------------------------------------------
            if (mainProductExists == true)
            {
                reservationNumber = insertReservationMaster();

                if (String.IsNullOrEmpty(reservationNumber))
                {
                    _userMessageText.Add("예약기본정보 등록 중에 오류가 발생했습니다. 운영담당자에게 연락하세요." + " 주문번호: " + vOReservationDetail.ORDR_NO + " 예약자: " + ORDE_CUST_NM);
                    // 예약건수는 예약일자별로 99개를 초과할 수 없도록 예외로직 추가
                    _overflowReservationSequenceNo = true;
                    return false;
                }
            }


            //---------------------------------------------------------------------------------------------------------------------------------------
            // 예약판매가격내역 신규 등록
            //---------------------------------------------------------------------------------------------------------------------------------------
            if (vOReservationDetail.ADLT_NBR > 0 && vOReservationDetail.ADLT_SALE_PRCE > 0)
            {
                listReservationAdultDivisionCode.Add("1");          // 성인
                listReservationSalePriceName.Add("성인 구매가");    // 성인구매가
                listReservationSalePriceAmount.Add(vOReservationDetail.ADLT_SALE_PRCE);
                listReservationSalePricePeopleCount.Add(vOReservationDetail.ADLT_NBR);
            }
            if (vOReservationDetail.CHLD_NBR > 0 && vOReservationDetail.CHLD_SALE_PRCE >= 0)
            {
                listReservationAdultDivisionCode.Add("2");          // 소아
                listReservationSalePriceName.Add("소아 구매가");    // 소아구매가
                listReservationSalePriceAmount.Add(vOReservationDetail.CHLD_SALE_PRCE);
                listReservationSalePricePeopleCount.Add(vOReservationDetail.CHLD_NBR);
            }
            if (vOReservationDetail.INFN_NBR > 0 && vOReservationDetail.INFN_SALE_PRCE >= 0)
            {
                listReservationAdultDivisionCode.Add("3");          // 유아
                listReservationSalePriceName.Add("유아 구매가");    // 유아구매가
                listReservationSalePriceAmount.Add(vOReservationDetail.INFN_SALE_PRCE);
                listReservationSalePricePeopleCount.Add(vOReservationDetail.INFN_NBR);
            }


            if (listReservationAdultDivisionCode.Count > 0)
            {
                if (insertReservationSalePriceInfo(reservationNumber, listReservationAdultDivisionCode, listReservationSalePriceName, listReservationSalePriceAmount, listReservationSalePricePeopleCount) == false)
                {
                    _userMessageText.Add("예약판매가격내역 등록 중에 오류가 발생했습니다. 운영담당자에게 연락하세요." + " 주문번호: " + vOReservationDetail.ORDR_NO + " 예약자: " + ORDE_CUST_NM);
                    return false;
                }
            }

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 예약옵션내역 신규 등록
            //---------------------------------------------------------------------------------------------------------------------------------------
            if (listReservationOptionName.Count > 0)
            {
                if (insertReservationOptionInfo(reservationNumber, 
                                                vOReservationDetail.SALE_CUR_CD,
                                                listReservationOptionAdultDivisionCode,
                                                listReservationOptionName, 
                                                listReservationOptionAmount, 
                                                listReservationOptionPeopleCount) == false)
                {
                    _userMessageText.Add("예약옵션내역 등록 중에 오류가 발생했습니다. 운영담당자에게 연락하세요." + " 주문번호: " + vOReservationDetail.ORDR_NO + " 예약자: " + ORDE_CUST_NM);
                    return false;
                }
            }

            //---------------------------------------------------------------------------------------------------------------------------------------
            // 구매자목록에 예약전환 완료 플래그 설정
            //---------------------------------------------------------------------------------------------------------------------------------------
            if (updatePurchaseListInfo(PRDT_CNMB, vOReservationDetail.PRCS_DTM, CMPN_NO, ORDR_NO, ORDE_CUST_NM, ORDE_CTIF_PHNE_NO, ORDE_EMAL_ADDR, reservationNumber) == false)
            {
                _userMessageText.Add("구매자파일 갱신 중에 오류가 발생했습니다. 운영담당자에게 연락하세요." + " 주문번호: " + vOReservationDetail.ORDR_NO + " 예약자: " + ORDE_CUST_NM);
                return false;
            }

            return true;
        }

        //================================================================================================================================================
        // 상품일련번호와 상품등급명으로 상품정보를 검색하고 판매통화코드와 상품등급코드를 VO에 세팅
        //================================================================================================================================================
        private Tuple<string, string> searchProductInfo(string PRDT_CNMB, string productGradeName, string departureDate)
        {
            string productGradeCode = "";
            string saleCurrencyCode = "";
            string currencyCode = "KRW";

            //string query = string.Format("CALL SelectProductDetailItemByProductNoAndGradeName ({0},'{1}','{2}','{3}','{4}')", PRDT_CNMB, productGradeName, currencyCode, departureDate, departureDate);
            string query = string.Format("CALL SelectProductDetailItemByProductNoAndGradeName ({0},'{1}')", PRDT_CNMB, productGradeName);
            DataSet dataSet = DbHelper.SelectQuery(query);

            // 상품을 찾지 못했을 경우 Exception 처리를 어떻게 할까?
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                _userMessageText.Add("상품정보가 존재하지 않습니다. 주문번호: " + vOReservationDetail.ORDR_NO + " 예약자: " + vOReservationDetail.CUST_NM + " 상품: " + vOReservationDetail.PRDT_CNMB + " 등급: " + productGradeName);
            }
            else
            {
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                productGradeCode = dataRow["PRDT_GRAD_CD"].ToString();                                   // 상품등급코드
                saleCurrencyCode = "KRW";
                // saleCurrencyCode = dataRow["CUR_CD"].ToString();                                        // 판매통화코드
                /**
                 *    19. 10. 23(수) 배장훈
                 *    상품상세정보의 가격정보가 책정이 안되어있는경우
                 *    구매자엑셀파일에서의 예약전환이 이루어지지 않는 문제 발생 (SelectProductDetailItemByProductNoAndGradeName)
                 *    TB_PRDT_DTLS_PRCE_L(상품가격) 테이블은 조인에서 제외
                 * **/
            }

            return new Tuple<string, string>(productGradeCode, saleCurrencyCode);
        }


        //================================================================================================================================================
        // 고객명, 연락처, 이메일로 동일 고객 검색
        //================================================================================================================================================
        private string searchCustomerInfo(string customerName, string customerEngName, string phoneNumber, string emailAddress)
        {
            string customerNo = "";

            // 이메일주소에 '@'만 있으면 공백으로 무시 처리
            if (emailAddress.Equals("@")) emailAddress = "";

            string query = string.Format("CALL SelectCustInfoByPersonalInfo ('{0}','{1}','{2}')", customerName, phoneNumber, emailAddress);

            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                customerNo = insertCustomerInfo(customerName, customerEngName, phoneNumber, emailAddress);
                return customerNo;
            }
            else
            {
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                return dataRow["CUST_NO"].ToString();
            }
        }

        //================================================================================================================================================
        // 고객명, 연락처, 이메일로 고객 신규 등록
        //================================================================================================================================================
        private string insertCustomerInfo(string customerName, string customerEngName, string phoneNumber, string emailAddress)
        {
            string customerNo = "";
            string homePostNumber = "";
            string homeAddress = "";
            string homeDetailAddress = "";
            string officePostNumber = "";
            string officeAddress = "";
            string officeDetailAddress = "";
            string memo = "";

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            // 개인인지 법인인지 판단 (법인을 판단하는 문자열 지속 추가 필요)
            string personalCorporationDivision = "";
            string[] str = new string[12];
            str[0] = "(주)";
            str[1] = "회사";
            str[2] = "사무소";
            str[3] = "법인";
            str[4] = "은행";
            str[5] = "학원";
            str[6] = "법무사";
            str[7] = "회계사";
            str[8] = "세무사";
            str[9] = "관세사";
            str[10] = "병원";
            str[11] = "의원";

            int indexValue = 0;

            for (int kk = 0; kk < str.Length; kk++)
            {
                indexValue = customerName.IndexOf(str[kk]);

                if (indexValue > 0) break;
            }

            if (indexValue == -1)
                personalCorporationDivision = "1";        // 개인
            else
                personalCorporationDivision = "2";        // 법인

            // sql 실행 및 고객번호 채번 결과 리턴
            string query = string.Format("CALL InsertCustInfoItemFromPurchaseFile ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                customerName,
                customerEngName,
                personalCorporationDivision,
                phoneNumber,
                emailAddress,
                officePostNumber,
                officeAddress,
                officeDetailAddress,
                homePostNumber,
                homeAddress,
                homeDetailAddress,
                memo,
                Global.loginInfo.ACNT_ID
            );

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal != -1)
                customerNo = queryResultArray[0];

            return customerNo;
        }

        //================================================================================================================================================
        // 예약기본 VO 객체 초기화
        //================================================================================================================================================
        private void clearVOReservationDetail()
        {
            vOReservationDetail.RSVT_NO = "";                                       // 예약번호
            vOReservationDetail.RSVT_DT = DateTime.Now.ToString("yyyy-MM-dd");      // 예약일자
            vOReservationDetail.ORDR_NO = "";                                // 주문번호
            vOReservationDetail.CNFM_NO = "";                                // CFM번호
            vOReservationDetail.PRDT_CNMB = "";                              // 상품일련번호
            vOReservationDetail.PRDT_GRAD_CD = "";                           // 상품등급코드
            vOReservationDetail.CUST_NO = "";                                // 고객번호
            vOReservationDetail.CUST_NM = "";                                // 고객명
            vOReservationDetail.TKTR_CMPN_NO = "";                           // 모객업체번호
            vOReservationDetail.RPRS_ENG_NM = "";                            // 대표자영문명
            vOReservationDetail.ORDE_CTIF_PHNE_NO = "";                      // 주문자연락처전화번호
            vOReservationDetail.ORDE_EMAL_ADDR = "";                         // 주문자이메일주소
            vOReservationDetail.RPRS_EMAL_ADDR = "";                         // 대표자이메일주소
            vOReservationDetail.AGE = 0;                                     // 연령
            vOReservationDetail.ADLT_SALE_PRCE = 0;                          // 성인판매가격
            vOReservationDetail.CHLD_SALE_PRCE = 0;                          // 소아판매가격
            vOReservationDetail.INFN_SALE_PRCE = 0;                          // 유아판매가격
            vOReservationDetail.TOT_SALE_AMT = 0;                            // 외화총판매금액=판매합계금액+옵션합계금액
            vOReservationDetail.WON_TOT_SALE_AMT = 0;                        // 원화총판매금액=판매합계금액+옵션합계금액
            vOReservationDetail.ADLT_NBR = 0;                                // 성인수
            vOReservationDetail.CHLD_NBR = 0;                                // 소아수
            vOReservationDetail.INFN_NBR = 0;                                // 유아수
            vOReservationDetail.FREE_TCKT_NBR = 0;                           // 프리티켓수
            vOReservationDetail.DPTR_DT = "";                                // 도착일자
            vOReservationDetail.LGMT_DAYS = 0;                               // 숙박일수
            vOReservationDetail.TOT_TRIP_DAYS = 0;                           // 총여행일수
            vOReservationDetail.SALE_SUM_AMT = 0;                            // 판매합계금액
            vOReservationDetail.OPTN_SUM_AMT = 0;                            // 옵션합계금액
            vOReservationDetail.CSPR_SUM_AMT = 0;                            // 원가합계금액
            vOReservationDetail.SALE_CUR_CD = "";                            // 판매통화코드
            vOReservationDetail.RSVT_AMT = 0;                                // 예약금액
            vOReservationDetail.PRPY_AMT = 0;                                // 선금금액
            vOReservationDetail.TOT_RECT_AMT = 0;                            // 총입금금액
            vOReservationDetail.MDPY_AMT = 0;                                // 중도금금액
            vOReservationDetail.UNAM_BAL = 0;                                // 미수금잔액
            vOReservationDetail.DSCT_AMT = 0;                                // 할인금액
            vOReservationDetail.WON_PYMT_FEE = 0;                            // 원화지급수수료
            vOReservationDetail.STEM_WON_AMT = 0;                            // 가정산원화금액
            vOReservationDetail.STMT_YN = "N";                               // 정산여부
            vOReservationDetail.STMT_WON_AMT = 0;                            // 정산원화금액
            vOReservationDetail.PRCS_DTM = "";                               // 구매일시
            vOReservationDetail.SHTL_ICUD_EN = "N";                          // 셔틀포함여부
            vOReservationDetail.RPSB_EMPL_NO = Global.loginInfo.EMPL_NO;     // 담당직원번호
            vOReservationDetail.CUSL_EMPL_NO = "";                           // 상담직원번호
            vOReservationDetail.LST_CHCK_STTS_CD = "";                       // 명단확인상태코드
            vOReservationDetail.PSPT_CHCK_STTS_CD = "";                      // 여권확인상태코드
            vOReservationDetail.ARGM_CHCK_STTS_CD = "";                      // 수배확인상태코드
            vOReservationDetail.VOCH_CHCK_STTS_CD = "";                      // 바우처확인상태코드
            vOReservationDetail.AVAT_CHCK_STTS_CD = "";                      // 항공확인상태코드
            vOReservationDetail.ISRC_CHCK_STTS_CD = "";                      // 보험확인상태코드
            vOReservationDetail.PRSN_CHCK_STTS_CD = "";                      // 개인확인상태코드
            vOReservationDetail.cutOffListStatusName = "";                   // 명단확인상태명
            vOReservationDetail.cutOffPassportStatusName = "";               // 여권확인상태코드
            vOReservationDetail.cutOffArrangementStatusName = "";            // 수배확인상태코드
            vOReservationDetail.cutOffVoucherStatusName = "";                // 바우처확인상태코드
            vOReservationDetail.cutOffAirlineStatusName = "";                // 항공확인상태코드
            vOReservationDetail.cutOffInsuranceStatusName = "";              // 보험확인상태코드
            vOReservationDetail.cutOffPersonalInfoStatusName = "";           // 개인확인상태코드
            vOReservationDetail.LST_CHCK_DTM = "";                           // 명단확인일시
            vOReservationDetail.PSPT_CHCK_DTM = "";                          // 여권확인일시
            vOReservationDetail.ARGM_CHCK_DTM = "";                          // 수배확인일시
            vOReservationDetail.VOCH_CHCK_DTM = "";                          // 바우처확인일시
            vOReservationDetail.AVAT_CHCK_DTM = "";                          // 항공확인일시
            vOReservationDetail.ISRC_CHCK_DTM = "";                          // 보험확인일시
            vOReservationDetail.PRSN_CHCK_DTM = "";                          // 개인확인일시
            vOReservationDetail.RSVT_STTS_CD = "1";                          // 예약상태코드 (1: 미확정)
            vOReservationDetail.TCKT_STTS_CD = "";                           // 티켓상태코드
            vOReservationDetail.QUOT_CNMB = "0";                             // 견적일련번호
            vOReservationDetail.DEL_YN = "N";                                // 삭제여부
            vOReservationDetail.OPTN_NM_1 = "";                              // 옵션명1
            vOReservationDetail.OPTN_NM_2 = "";                              // 옵션명2
            vOReservationDetail.OPTN_NM_3 = "";                              // 옵션명3
            vOReservationDetail.OPTN_NM_4 = "";                              // 옵션명4
            vOReservationDetail.OPTN_NM_5 = "";                              // 옵션명5
            vOReservationDetail.CUST_RQST_CNTS = "";                         // 고객요청내용
            vOReservationDetail.INTR_MEMO_CNTS = "";                         // 내부메모내용
            vOReservationDetail.REMK_CNTS = "";                              // 비고내용
            vOReservationDetail.FRST_RGST_DTM = "";                          // 최초등록일시
            vOReservationDetail.FRST_RGTR_ID = "";                           // 최초등록자ID
            vOReservationDetail.FINL_MDFC_DTM = "";                          // 최종변경일시
            vOReservationDetail.FINL_MDFR_ID = "";                           // 최종변경자ID
        }


        //================================================================================================================================================
        // 예약기본 테이블 신규 생성
        //================================================================================================================================================
        private string insertReservationMaster()
        {
            string reservationNumber = "";

            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

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

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (!String.IsNullOrEmpty(queryResultArray?[0].ToString()))
            {
                reservationNumber = queryResultArray[0].ToString();

                if  (reservationNumber.IndexOf("ERROR") > 0)
                {
                    return  null;
                } else
                {
                    return reservationNumber;
                }
            }
            else
            {
                return null;
            }
        }

        //================================================================================================================================================
        // 예약판매가격내역 신규 등록
        //================================================================================================================================================
        private bool insertReservationSalePriceInfo
        (
            string reservationNumber, 
            List<string> reservationAdultDivisionCode, 
            List<string> listReservationSalePriceName, 
            List<double> listReservationSalePriceAmount, 
            List<int> listReservationSalePricePeopleCount
        )
        {
            string[] queryStringArray = new string[listReservationSalePriceName.Count];           // sql 배열
            string[] queryResultArray = new string[listReservationSalePriceName.Count];           // 건별 sql 처리 결과 리턴 배열

            // [판매가] INSERT 쿼리를 배열에 저장
            for (int ii = 0; ii < listReservationSalePriceName.Count; ii++)
            {
                string RSVT_NO = reservationNumber;
                string CNMB = "0";
                string ADLT_DVSN_CD = reservationAdultDivisionCode[ii].ToString();
                string SALE_PRCE_NM = listReservationSalePriceName[ii].ToString();
                double SALE_UTPR = listReservationSalePriceAmount[ii];
                int NMPS_NBR = listReservationSalePricePeopleCount[ii];
                string SALE_CUR_CD = "KRW";                                                       // 소셜판매 건은 원화 고정
                Int32 WON_SALE_UTPR = Convert.ToInt32(Math.Round(SALE_UTPR));                     // 소셜판매 건은 원화판매임
                double APLY_EXRT = 0;

                string query = string.Format("CALL InsertRsvtSalePriceItem ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                               RSVT_NO,
                               SALE_PRCE_NM,
                               ADLT_DVSN_CD,
                               SALE_CUR_CD,
                               SALE_UTPR,
                               WON_SALE_UTPR,
                               NMPS_NBR,
                               APLY_EXRT,
                               Global.loginInfo.ACNT_ID);

                queryStringArray[ii] = query;
            }

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
                return false;
            else
                return true;
        }

        //================================================================================================================================================
        // 예약옵션내역 신규 등록 (예: 위메프 프레지던트크루즈 바디 마사지 등) => 예약판매가격내역에 insert
        //================================================================================================================================================
        private bool insertReservationOptionInfo(string reservationNumber, 
                                                 string currencyCode, 
                                                 List<string> listReservationOptionAdultDivisionCode,
                                                 List<string> reservationOptionName, 
                                                 List<double> reservationOptionAmount, 
                                                 List<int> reservationPeopleCount)
        {
            string[] queryStringArray = new string[reservationOptionName.Count];           // sql 배열
            string[] queryResultArray = new string[reservationOptionName.Count];           // 건별 sql 처리 결과 리턴 배열

            // INSERT 쿼리를 배열에 저장
            for (int ii = 0; ii < reservationOptionName.Count; ii++)
            {
                string RSVT_NO = reservationNumber;
                string CNMB = "0";
                string ADLT_DVSN_CD = listReservationOptionAdultDivisionCode[ii].ToString();
                string SALE_PRCE_NM = reservationOptionName[ii].ToString();
                double SALE_UTPR = reservationOptionAmount[ii];
                int NMPS_NBR = reservationPeopleCount[ii];

                string SALE_CUR_CD = "KRW";                                                                                  // 소셜판매 건은 원화 고정
                Int32 WON_SALE_UTPR = Convert.ToInt32(Math.Round(SALE_UTPR));                     // 소셜판매 건은 원화판매임
                double APLY_EXRT = 0;

                string query = string.Format("CALL InsertRsvtSalePriceItem ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                               RSVT_NO,
                               SALE_PRCE_NM,
                               ADLT_DVSN_CD,
                               SALE_CUR_CD,
                               SALE_UTPR,
                               WON_SALE_UTPR,
                               NMPS_NBR,
                               APLY_EXRT,
                               Global.loginInfo.ACNT_ID);

                queryStringArray[ii] = query;
            }

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
                return false;
            else
                return true;
        }

        //================================================================================================================================================
        // 구매자목록 테이블에 예약등록여부와 예약번호 갱신
        //================================================================================================================================================
        private bool updatePurchaseListInfo(string PRDT_CNMB, string PRCS_DT, string CMPN_NO, string ORDR_NO, string ORDE_CUST_NM, string ORDE_CTIF_PHNE_NO, string ORDE_EMAL_ADDR, string reservationNumber)
        {
            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            string RSVT_RGST_YN = "Y";                           // 예약등록여부

            string query = string.Format("CALL UpdatePurchaserListItem ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                PRDT_CNMB,
                PRCS_DT,
                CMPN_NO,
                ORDR_NO,
                ORDE_CUST_NM,
                ORDE_CTIF_PHNE_NO,
                ORDE_EMAL_ADDR,
                reservationNumber,
                RSVT_RGST_YN,
                Global.loginInfo.ACNT_ID
            );

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                return false;
            }

            return true;
        }

        //================================================================================================================================================
        // 처리 중에 발생한 예외사항을 사용자에게 팝업으로 안내
        //================================================================================================================================================
        private void showUserMessage()
        {
            string resultMessage = "[처리결과] 예약전환 오류로 다음 내용은 처리되지 않았습니다." + "\r\n";

            for (int ii = 0; ii < _userMessageText.Count; ii++)
            {
                resultMessage = resultMessage + _userMessageText[ii].ToString() + "\r\n";
            }

            PopUpResultMessage form = new PopUpResultMessage();
            form.setUserMessage(resultMessage);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
    }
}
 