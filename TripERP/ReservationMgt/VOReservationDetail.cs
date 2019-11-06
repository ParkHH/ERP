using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripERP.ReservationMgt
{
    public class VOReservationDetail
    {
        
        public string RSVT_NO { get; set; }                       // 예약번호2019-08-06 hih
        public string RSVT_DT { get; set; }                       // 예약일자
        public string ORDR_NO { get; set; }                       // 주문번호
        public string CNFM_NO { get; set; }                       // CFM번호
        public string PRDT_CNMB { get; set; }                     // 상품일련번호
        public string PRDT_GRAD_CD { get; set; }                  // 상품등급코드
        public string CUST_NO { get; set; }                       // 고객번호
        public string CUST_NM { get; set; }                       // 고객명
        public string TKTR_CMPN_NO { get; set; }                  // 모객업체번호
        public string RPRS_ENG_NM { get; set; }                   // 대표자영문명
        public string ORDE_CTIF_PHNE_NO { get; set; }             // 주문자연락처전화번호
        public string ORDE_EMAL_ADDR { get; set; }                // 주문자이메일주소
        public string RPRS_EMAL_ADDR { get; set; }                // 대표자이메일주소
        public int AGE { get; set; }                              // 연령
        public double ADLT_SALE_PRCE { get; set; }                // 성인판매가격
        public double CHLD_SALE_PRCE { get; set; }                // 소아판매가격
        public double INFN_SALE_PRCE { get; set; }                // 유아판매가격
        public double TOT_SALE_AMT { get; set; }                  // 총판매외화금액=판매합계금액+옵션합계금액
        public double WON_TOT_SALE_AMT { get; set; }              // 총판매원화금액=판매합계금액+옵션합계금액
        public int ADLT_NBR { get; set; }                         // 성인수
        public int CHLD_NBR { get; set; }                         // 소아수
        public int INFN_NBR { get; set; }                         // 유아수
        public int FREE_TCKT_NBR { get; set; }                    // 프리티켓수
        public string DPTR_DT { get; set; }                       // 도착일자
        public int LGMT_DAYS { get; set; }                        // 숙박일수
        public int TOT_TRIP_DAYS { get; set; }                    // 총여행일수
        public double SALE_SUM_AMT { get; set; }                  // 판매합계금액
        public double OPTN_SUM_AMT { get; set; }                  // 옵션합계금액
        public double CSPR_SUM_AMT { get; set; }                  // 원가합계금액
        public string SALE_CUR_CD { get; set; }                   // 판매통화코드
        public double RSVT_AMT { get; set; }                      // 예약금액
        public double PRPY_AMT { get; set; }                      // 선금금액
        public double TOT_RECT_AMT { get; set; }                  // 총입금금액
        public double MDPY_AMT { get; set; }                      // 중도금금액
        public double UNAM_BAL { get; set; }                      // 미수금잔액
        public double DSCT_AMT { get; set; }                      // 할인금액
        public double WON_PYMT_FEE { get; set; }                  // 원화지급수수료
        public double STEM_WON_AMT { get; set; }                  // 가정산원화금액
        public string STMT_YN { get; set; }                       // 정산여부
        public double STMT_WON_AMT { get; set; }                  // 정산원화금액
        public string PRCS_DTM { get; set; }                      // 구매일시
        public string SHTL_ICUD_EN { get; set; }                  // 셔틀포함여부
        public string RPSB_EMPL_NO { get; set; }                  // 담당직원번호
        public string CUSL_EMPL_NO { get; set; }                  // 상담직원번호
        public string LST_CHCK_STTS_CD { get; set; }              // 명단확인상태코드
        public string PSPT_CHCK_STTS_CD { get; set; }             // 여권확인상태코드
        public string ARGM_CHCK_STTS_CD { get; set; }             // 수배확인상태코드
        public string VOCH_CHCK_STTS_CD { get; set; }             // 바우처확인상태코드
        public string AVAT_CHCK_STTS_CD { get; set; }             // 항공확인상태코드
        public string ISRC_CHCK_STTS_CD { get; set; }             // 보험확인상태코드
        public string PRSN_CHCK_STTS_CD { get; set; }             // 개인확인상태코드
        public string cutOffListStatusName { get; set; }          // 명단확인상태명
        public string cutOffPassportStatusName { get; set; }      // 여권확인상태코드
        public string cutOffArrangementStatusName { get; set; }   // 수배확인상태코드
        public string cutOffVoucherStatusName { get; set; }       // 바우처확인상태코드
        public string cutOffAirlineStatusName { get; set; }       // 항공확인상태코드
        public string cutOffInsuranceStatusName { get; set; }     // 보험확인상태코드
        public string cutOffPersonalInfoStatusName { get; set; }  // 개인확인상태코드
        public string LST_CHCK_DTM { get; set; }                  // 명단확인일시
        public string PSPT_CHCK_DTM { get; set; }                 // 여권확인일시
        public string ARGM_CHCK_DTM { get; set; }                 // 수배확인일시
        public string VOCH_CHCK_DTM { get; set; }                 // 바우처확인일시
        public string AVAT_CHCK_DTM { get; set; }                 // 항공확인일시
        public string ISRC_CHCK_DTM { get; set; }                 // 보험확인일시
        public string PRSN_CHCK_DTM { get; set; }                 // 개인확인일시
        public string RSVT_STTS_CD { get; set; }                  // 예약상태코드
        public string TCKT_STTS_CD { get; set; }                  // 티켓상태코드
        public string QUOT_CNMB { get; set; }                     // 견적일련번호
        public string DEL_YN { get; set; }                        // 삭제여부
        public string OPTN_NM_1 { get; set; }                     // 옵션명1
        public string OPTN_NM_2 { get; set; }                     // 옵션명2
        public string OPTN_NM_3 { get; set; }                     // 옵션명3
        public string OPTN_NM_4 { get; set; }                     // 옵션명4
        public string OPTN_NM_5 { get; set; }                     // 옵션명5
        public string CUST_RQST_CNTS { get; set; }                // 고객요청내용
        public string INTR_MEMO_CNTS { get; set; }                // 내부메모내용
        public string REMK_CNTS { get; set; }                     // 비고내용
        public string COOP_CMPN_DVSN_CD { get; set; }             // 협력업체구분코드
        public string FRST_RGST_DTM { get; set; }                 // 최초등록일시
        public string FRST_RGTR_ID { get; set; }                  // 최초등록자ID
        public string FINL_MDFC_DTM { get; set; }                 // 최종변경일시
        public string FINL_MDFR_ID { get; set; }                  // 최종변경자ID
    }


}
