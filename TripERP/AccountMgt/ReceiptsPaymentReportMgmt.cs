using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;

namespace TripERP.AccountMgt
{
    //***************************
    // 20191001 - 박현호 작업 완료
    //***************************  

    public partial class ReceiptsPaymentReportMgmt : Form
    {
        //---------------------------------------------------------
        // 전월 이월 입금,출금,잔액 합계 (원화,외화) 변수
        //---------------------------------------------------------
        double SUM_PREV_MONTH_DEPOSIT_WON = 0.0;        
        double SUM_PREV_MONTH_OUT_WON = 0.0;        
        double SUM_PREV_MONTH_REMAIN_WON = 0.0;        


        //-----------------------------------------------------------------------------------------------
        // 전월이월에서 서로 다른 "외화의 전표 연산을 위해" 전월이월잔액금을 통화별로 보관 
        //-----------------------------------------------------------------------------------------------
        List<Dictionary<string, List<double>>> prevMonthFRCR_BAL_DATA = new List<Dictionary<string, List<double>>>();

        public ReceiptsPaymentReportMgmt()
        {
            InitializeComponent();
        }

        private void ReceiptsPaymentReportMgmt_Load(object sender, EventArgs e)
        {
            reportViewerLoad();                                                                 // reportViewer Load
        }

        //========================================================================================================================
        // reportViewer Load  동작 Method -> reportViewer 속성값 설정!        
        //========================================================================================================================
        public void reportViewerLoad()
        {
            //-------------------------------------------------------------------------------------
            // 구분 ComboBox Item Setting
            //-------------------------------------------------------------------------------------
            this.cb_wonFrcr.Items.Add(new ComboBoxItem("원화", "KRW"));
            this.cb_wonFrcr.Items.Add(new ComboBoxItem("외화", "FRCR"));
            this.cb_wonFrcr.SelectedIndex = 0;

            //-------------------------------------------------------------------------------------
            // ReportViewer 초기 Setting
            //-------------------------------------------------------------------------------------
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TripERP.AccountMgt.ReceiptsPaymentReport.rdlc";      // 보고서와 ReportViewer 연결
            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;                  // Report Mode 설정 (현재 Local)
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);                // reportViewer 의 Layout 설정 (현재 인쇄Layoout)
            this.reportViewer1.Size = new Size(this.groupBox2.Width - 20, this.groupBox2.Height - 50);                 // reportViewer Size 조절 (현재 WindowForm 크기에 맞게)       
            this.reportViewer1.Location = new Point(this.groupBox2.Location.X + 10, this.groupBox2.Location.Y + 40);
            this.reportViewer1.MouseWheel += new MouseEventHandler(zoomInOutMouseWheel);    // ReportViewer Zoom InOUt 동작 단축키 관련 EventHandler (MouseWheel 동작 관련)
            this.reportViewer1.ShowZoomControl = true;

            //-------------------------------------------------------------------------------------
            // 보통예금출납장 초기 Loading 시 날짜값 설정
            //-------------------------------------------------------------------------------------
            var parameter = new ReportParameter[2];
            parameter[0] = new ReportParameter("DateFrom", dtp_searchFrom.Text.ToString().Substring(0, 12));
            parameter[1] = new ReportParameter("DateTo", dtp_searchTo.Text.ToString().Substring(0, 12));
            this.reportViewer1.LocalReport.SetParameters(parameter);

            PageSettings pg = new PageSettings();
            pg.Margins.Top = 40;
            pg.Margins.Left = 25;
            pg.Margins.Right = 40;
            pg.Margins.Bottom = 40;
            pg.Landscape = true;                                                                // 용지 세로,가로 Default 값 설정 (True : 가로 / False : 세로)            
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();


            //-------------------------------------------------------------------------------------------------
            // Form Load 시 reportViewer 출력을 위한 DataSource 설정
            //-------------------------------------------------------------------------------------------------
            DataTable dt = new DataTable();
            ReportDataSource rDS = new ReportDataSource("DataSet1", dt);
            ReportDataSource rDS2 = new ReportDataSource("DataSet2", dt);
            ReportDataSource rDS3 = new ReportDataSource("DataSet3", dt);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rDS);
            this.reportViewer1.LocalReport.DataSources.Add(rDS2);
            this.reportViewer1.LocalReport.DataSources.Add(rDS3);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();


            //DataRow[] settlementDataArr = getSettlementSateData();      // DB 에서 Data 가져오는 Method
            //dataRemake(settlementDataArr);                                       // 가져온 Data ReportViewer Table 에 출력하는 Method
        }












        //========================================================================================================================
        // Ctrl + MouseWheel 동작시 보고서 배율 확대   
        //========================================================================================================================
        public void zoomInOutMouseWheel(object sender, MouseEventArgs e)
        {
            Keys key = ModifierKeys;                                                    // 보조키 정보를 변수화
            ReportViewer reportViewer = (ReportViewer)sender;           // Event 발생 주체 변수화
            reportViewer.ZoomMode = ZoomMode.Percent;                   // reportViewer ZoomMode 를 Percent 형식으로 지정
            string pressedKey = key.ToString();                                     // ModifierKey 정보를 변수에 담아놓음                        

            if (pressedKey.Equals("Control"))                                       // 현재 누르고 있는 ModifierKey 값이 Control 이라면
            {
                string wheellDelta = e.Delta.ToString();                        // Wheel 상향값 하향값 판단을 위해 Wheel 회전방향과 회전수 변수화                
                if (wheellDelta.Equals("120"))                                      // Wheel 회전 방향이 상향값이라면
                {
                    if (reportViewer.ZoomPercent < 200)                         // ZoomPercent 가 200% 이하의 값일때
                    {
                        reportViewer.ZoomPercent += 10;                         // ZoomPercent 10 증가!
                    }
                }
                else      // Wheel 회전 방향이 하향값이라면      
                {
                    if (reportViewer.ZoomPercent > 20)      // ZoomPercent 값이 20 이상일때만
                    {
                        reportViewer.ZoomPercent -= 10;         // ZoomPercent 값을 10 감소
                    }
                }
                reportViewer.RefreshReport();                   // Report 를 Refresh 하여 Zoom 변경 정보를 출력되게함 (Page 변경과는 무관)
            }
        }










        //======================================================================================================================
        // 검색 버튼 동작 Event Method
        //======================================================================================================================
        private void searchAccountCodeListButton_Click(object sender, EventArgs e)
        {
            selectData();
        }

        //-----------------------------------------------------------
        // 검색 버튼 Click 시 실행 Method
        //-----------------------------------------------------------
        private void selectData()
        {
            string searchFrom = dtp_searchFrom.Value.ToString().Substring(0, 11).Trim();
            string searchTo = dtp_searchTo.Value.ToString().Substring(0, 11).Trim();
            double SUM_DEPOSIT_MONTH_WON = 0.0;                                             // 입금액 합계 변수 (월별, 원화)
            double SUM_DEPOSIT_MONTH_FRCR = 0.0;                                             // 입금액 합계 변수 (월별, 외화)
            double SUM_OUT_MONTH_WON = 0.0;                                                    // 출금액 합계 변수 (월별, 원화)
            double SUM_OUT_MONTH_FRCR = 0.0;                                                    // 출금액 합계 변수 (월별, 외화)
            double SUM_REMAIN_MONTH_WON = 0.0;                                              // 잔액 합계 변수 (월별, 원화)
            double SUM_REMAIN_MONTH_FRCR = 0.0;                                              // 잔액 합계 변수 (월별, 외화)
            double DEPOSIT_WON_AMT = 0.0;                                                         // 입금 (해당월 입금액)               --> row 1개 값
            double DEPOSIT_FRCR_AMT = 0.0;                                                          // 입금 외화                            --> row 1개 값
            double OUT_WON_AMT = 0.0;                                                                // 출금 (해당월 출금액)                --> row 1개 값
            double OUT_FRCR_AMT = 0.0;                                                                  // 출금 (외화)                          --> row 1개 값
            double REMAIN_WON_AMT = 0.0;                                                 // 원화잔액 (잔액 + 입금액 - 출금액)    --> row 1개 값
            double REMAIN_FRCR_AMT = 0.0;                                                // 외화잔액 (잔액 + 입금액 - 출금액)    --> row 1개 값
            ComboBoxItem selectedDivision = (ComboBoxItem)cb_wonFrcr.SelectedItem;                                  // 원화 외화 구분
            string division = selectedDivision.Value.ToString().Trim();
            string CUR_CD = "";
            string prev_CUR_CD = "";                                                                      // 전표이력 Data 읽을시 외화 통화별로 전표이력 내역 (입금, 출금, 잔액) 계산을 다르게 해주기 위한 메모리 변수 --> 이전에 나온 CUR_CD 를 보관
            List<string> history_Receipts_CurrencyCode = new List<string>();        // 전표이력 읽었을때 나온 CUR_CD 보관 List  --> 외화 누계 정보에 이용

            List<string> CUR_CD_List = getAllRegistedCurrencyCode();                  // 현재 DB에 등록되어있는 모든 통화코드를 담은 List
            if (CUR_CD_List.Count == 0)
            {
                MessageBox.Show("등록되어있는 통화코드가 없습니다.");
            }


            List<Dictionary<string, List<double>>> FRCR_COUNT_DATA_List = createDictionaryForFRCR(CUR_CD_List);                 //  외화별 합계계산을 다르게 하기 위해 존재하는 Dictionary List

            
            
            //-----------------------------------------------------------------------------------------------------
            // 검색 일자 유효성 검사후 시작일이 목표일보다 이후 일자일 경우 경고 및 조회 동작 수행 중단
            //-----------------------------------------------------------------------------------------------------
            if (DateTime.Parse(searchFrom) > DateTime.Parse(searchTo))
            {
                MessageBox.Show("조회 일자 시작일이 목표일보다 이후 일자입니다.");
                return;
            }

            //-----------------------------------------------------------------------------------------------------------
            // 전월의 전표내역 정보 가져오기 및 Parameter 로 Setting
            //-----------------------------------------------------------------------------------------------------------
            DataTable selectPrevMonthDataResult = selectPrevMonthData(searchFrom, searchTo, division);      // 전월 입금, 출금, 잔액 누적 소계 정보 가져오기         

            var parameter = new ReportParameter[2];                                                                                  // reportViewer 의 Parameter 개수만큼 배열의 길이 갯수 지정

            //----------------------------------------------------------------------------------
            // 조회 년, 월 정보 reportViewer 에 Setting 하기
            //----------------------------------------------------------------------------------            
            parameter[0] = new ReportParameter("DateFrom", dtp_searchFrom.Text.ToString().Substring(0, 13));
            parameter[1] = new ReportParameter("DateTo", dtp_searchTo.Text.ToString().Substring(0, 13));


            //-----------------------------------------------------------------------------------------------------------
            // 해당월의 전표내역 정보 가져오기
            //-----------------------------------------------------------------------------------------------------------  
            // 가져온 전표내역을 ReportViewer 에 출력하기 위해 DataSet 과 대응시킬 DataTable 선언 및 속성 추가
            DataTable dt = new DataTable();
            dt.Columns.Add("월일");
            dt.Columns.Add("통화");
            dt.Columns.Add("환율");
            dt.Columns.Add("적요");
            dt.Columns.Add("입금원화");
            dt.Columns.Add("입금외화");
            dt.Columns.Add("출금원화");
            dt.Columns.Add("출금외화");
            dt.Columns.Add("잔액원화");
            dt.Columns.Add("잔액외화");

            // 가져온 전표내역의 월별, 누계 Data 를 담을 DataTable
            DataTable dt_monthAndAll = new DataTable();
            dt_monthAndAll.Columns.Add("통화");
            dt_monthAndAll.Columns.Add("월별입금원화합계");
            dt_monthAndAll.Columns.Add("월별입금외화합계");
            dt_monthAndAll.Columns.Add("월별출금원화합계");
            dt_monthAndAll.Columns.Add("월별출금외화합계");
            dt_monthAndAll.Columns.Add("월별잔액원화합계");
            dt_monthAndAll.Columns.Add("월별잔액외화합계");
            dt_monthAndAll.Columns.Add("누계입금원화");
            dt_monthAndAll.Columns.Add("누계입금외화");
            dt_monthAndAll.Columns.Add("누계출금원화");
            dt_monthAndAll.Columns.Add("누계출금외화");
            dt_monthAndAll.Columns.Add("누계잔액원화");
            dt_monthAndAll.Columns.Add("누계잔액외화");


            //-----------------------------------------------------------------------------------------------------
            // DataTable 을 ReportDataSource 객체화 한다.. 그래야 ReportViewer 에 적용가능
            // 현재 전표내용 조회 결과가 존재할떄와 존재하지 않을시를 구분시 사용하기 위해 현재 줄로 이동함
            //-----------------------------------------------------------------------------------------------------
            ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);                                 // 전표 내역 담을 Table 을 
            ReportDataSource dataSource2 = null;                                                                                    // 전월 이월 Data 의 ReportViewer 의 DataSource 화
            ReportDataSource dataSource3 = new ReportDataSource("DataSet3", dt_monthAndAll);            // 월별 합계, 누계 Data 를 ReportViewer 의 DataSource 화

            if (selectPrevMonthDataResult == null)
            {
                dataSource2 = new ReportDataSource("DataSet2", new DataTable());
            }
            else
            {
                dataSource2 = new ReportDataSource("DataSet2", selectPrevMonthDataResult);
            }
           

            //-----------------------------------------------------------------------------------------------------
            // 전표내용 가져오는 Procedure 선언 및 실행
            //-----------------------------------------------------------------------------------------------------
            string query = string.Format("CALL SelectReceiptsPaymentData('{0}','{1}','{2}')", searchFrom, searchTo, division);
            DataSet ds = DbHelper.SelectQuery(query);
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                //-----------------------------------------------------------------------------------------------------
                // 해당월 입,출금 내역 없을 경우 조회 시작일과 목표일만 reportViewer 매개변수 처리
                //-----------------------------------------------------------------------------------------------------
                //MessageBox.Show("조회 결과가 존재하지 않습니다.", "경고");

                var parameter_mid = new ReportParameter[2];

                parameter_mid[0] = new ReportParameter("DateFrom", dtp_searchFrom.Text.ToString().Substring(0, 13));
                parameter_mid[1] = new ReportParameter("DateTo", dtp_searchTo.Text.ToString().Substring(0, 13));

                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(dataSource);
                this.reportViewer1.LocalReport.DataSources.Add(dataSource2);
                this.reportViewer1.LocalReport.DataSources.Add(dataSource3);
                this.reportViewer1.LocalReport.SetParameters(parameter_mid);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
                return;
            }


            //-------------------------------------------------------------------------------
            // DB 에서 꺼낸 각 월별 입출금 내역을 가져와 DataTable 의 행으로 넣기
            //-------------------------------------------------------------------------------            
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string SLIP_DT = row["SLIP_DT"].ToString().Trim().Substring(0, 10);                                                  // 월/일                
                CUR_CD = row["CUR_CD"].ToString().Trim();                                                                                   // 통화코드
                if (!history_Receipts_CurrencyCode.Contains(CUR_CD)) { 
                    history_Receipts_CurrencyCode.Add(CUR_CD);
                }
                double TRAN_EXRT = double.Parse(row["TRAN_EXRT"].ToString().Trim());                    // 환율
                string REMK_CNTS = row["REMK_CNTS"].ToString().Trim();                                          // 적요
                string SLIP_NO = row["SLIP_NO"].ToString().Trim();                                                    // 전표번호
                string SLIP_CNMB = row["SLIP_CNMB"].ToString().Trim();                                            // 전표일련번호
                string ACNT_TIT_CD = row["ACNT_TIT_CD"].ToString().Trim();                                     // 계정과목코드
                string DBCR_DVSN_CD = row["DBCR_DVSN_CD"].ToString().Trim();                             // 차대구분코드
                string CASH_TRNS_DVSN_CD = row["CASH_TRNS_DVSN_CD"].ToString().Trim();         // 현금대체구분코드                

                string CUST_NO = row["CUST_NO"].ToString().Trim();                                                   // 고객번호
                string ACCT_NO = row["ACCT_NO"].ToString().Trim();                                                   // 계좌번호
                string RCKO_DT = row["RCKO_DT"].ToString().Trim();                                                    // 기산일자
                string DPWH_CNMB = row["DPWH_CNMB"].ToString().Trim();                                         // 입출금일련번호
                string RPSB_EMPL_NO = row["RPSB_EMPL_NO"].ToString().Trim();                                // 담당직원번호
                string RSVT_NO = row["RSVT_NO"].ToString().Trim();                                                  // 예약번호

                string SLIP_WON_AMT = "";
                string SLIP_FRCR_AMT = "";


                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                // 통화코드별로 전표금액 값을 다르게 가져옴
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //**************************************** 원화 일 때 ****************************************
                if (CUR_CD.Equals("KRW"))
                {
                    SLIP_WON_AMT = row["SLIP_WON_AMT"].ToString().Trim();                                       // 원화전표금액
                    SLIP_FRCR_AMT = "";
                    //--------------------------------------------------------------------------------
                    // 조회한 Data 를 기반으로 입금,출금 구분
                    //--------------------------------------------------------------------------------                
                    if (DBCR_DVSN_CD == "1")        // 차변 (입금)
                    {
                        DEPOSIT_WON_AMT = double.Parse(SLIP_WON_AMT);
                        OUT_WON_AMT = 0.0;
                    }
                    else if (DBCR_DVSN_CD == "2")    //대변 (출금)
                    {
                        DEPOSIT_WON_AMT = 0.0;
                        OUT_WON_AMT = double.Parse(SLIP_WON_AMT);
                    }

                    //-------------------------------------------------------------------------------------------------------------
                    // 동적으로 구성되는 reportViewer 의 row 중 잔액 속성은 첫번째 줄만 전월이월 잔액내용을 참조하여 계산,
                    // 이후에는 계산된 내용을 참조하여 입,출금 판별하여 누적 계산 진행
                    //-------------------------------------------------------------------------------------------------------------
                    if (i == 0)
                    {
                        REMAIN_WON_AMT = SUM_PREV_MONTH_REMAIN_WON + DEPOSIT_WON_AMT - OUT_WON_AMT;                        
                    }
                    else
                    {
                        REMAIN_WON_AMT = REMAIN_WON_AMT + DEPOSIT_WON_AMT - OUT_WON_AMT;                        
                    }
                }
                else
                {
                    //**************************************** 외화 일 때 ****************************************
                    SLIP_FRCR_AMT = row["SLIP_FRCR_AMT"].ToString().Trim();                                      // 전표외화금액
                    SLIP_WON_AMT = row["SLIP_WON_AMT"].ToString().Trim();                                       // 전표원화금액

                    //--------------------------------------------------------------------------------
                    // 조회한 Data 를 기반으로 입금,출금 구분
                    //--------------------------------------------------------------------------------                
                    if (DBCR_DVSN_CD == "1")        // 차변 (입금)
                    {
                        DEPOSIT_FRCR_AMT = double.Parse(SLIP_FRCR_AMT);                        
                        DEPOSIT_WON_AMT = double.Parse(SLIP_WON_AMT);
                        OUT_FRCR_AMT = 0.0;
                        OUT_WON_AMT = 0.0;
                    }
                    else if (DBCR_DVSN_CD == "2")    //대변 (출금)
                    {
                        DEPOSIT_FRCR_AMT = 0.0;
                        DEPOSIT_WON_AMT = 0.0;
                        OUT_FRCR_AMT = double.Parse(SLIP_FRCR_AMT);
                        OUT_WON_AMT = double.Parse(SLIP_WON_AMT);
                    }

                    //-------------------------------------------------------------------------------------------------------------
                    // 동적으로 구성되는 reportViewer 의 row 중 잔액 속성은 첫번째 줄만 전월이월 잔액내용을 참조하여 계산,
                    // 이후에는 계산된 내용을 참조하여 입,출금 판별하여 누적 계산 진행
                    //-------------------------------------------------------------------------------------------------------------
                    if (i == 0)
                    {
                        if (prevMonthFRCR_BAL_DATA.Count == 0)
                        {
                            REMAIN_WON_AMT = REMAIN_WON_AMT + DEPOSIT_WON_AMT - OUT_WON_AMT;
                            REMAIN_FRCR_AMT = REMAIN_FRCR_AMT + DEPOSIT_FRCR_AMT - OUT_FRCR_AMT;
                        }
                        else
                        {
                            for (int a = 0; a < prevMonthFRCR_BAL_DATA.Count; a++)
                            {
                                //-----------------------------------------------------------------------------------------------------------------------------------------
                                // 외화잔액 List 에서 꺼댄 Dictionary 의 key 값이 현재 CUR_CD 와 같다면 dictionary 에 담겨있는 잔액정보를 이용하여 잔액 도출
                                //-----------------------------------------------------------------------------------------------------------------------------------------
                                Dictionary<string, List<double>> dictionary = prevMonthFRCR_BAL_DATA[a];
                                if (dictionary.Keys.Equals(CUR_CD))
                                {
                                    List<double> FRCR_REMAIN_BAL = new List<double>();                                                          // 전월 이월 원화잔액과 외화잔액 합계를 담을 List
                                    dictionary.TryGetValue(CUR_CD, out FRCR_REMAIN_BAL);                                                        // Dictionary 에서 전월이월 원화잔액, 외화잔액을 담은 List 를 꺼냄 (0 : 원화 잔액 / 1: 외화 잔액)
                                    REMAIN_WON_AMT = FRCR_REMAIN_BAL[0] + DEPOSIT_WON_AMT - OUT_WON_AMT;            // 원화잔액 연산                          
                                    REMAIN_FRCR_AMT = FRCR_REMAIN_BAL[1] + DEPOSIT_FRCR_AMT - OUT_FRCR_AMT;          // 외화잔액 연산           
                                }
                            }
                        }
                    }
                    else
                    {                        
                        if (!CUR_CD.Equals(prev_CUR_CD))
                        {
                            if(prevMonthFRCR_BAL_DATA.Count == 0)
                            {
                                REMAIN_WON_AMT = 0.0;
                                REMAIN_FRCR_AMT = 0.0;
                                REMAIN_WON_AMT = REMAIN_WON_AMT + DEPOSIT_WON_AMT - OUT_WON_AMT;
                                REMAIN_FRCR_AMT = REMAIN_FRCR_AMT + DEPOSIT_FRCR_AMT - OUT_FRCR_AMT;
                            }
                            else
                            {
                                for (int a = 0; a < prevMonthFRCR_BAL_DATA.Count; a++)
                                {
                                    //-----------------------------------------------------------------W------------------------------------------------------------------------
                                    // 외화잔액 List 에서 꺼댄 Dictionary 의 key 값이 현재 CUR_CD 와 같다면 dictionary 에 담겨있는 잔액정보를 이용하여 잔액 도출
                                    //-----------------------------------------------------------------------------------------------------------------------------------------
                                    Dictionary<string, List<double>> dictionary = prevMonthFRCR_BAL_DATA[a];
                                    if (dictionary.Keys.Equals(CUR_CD))
                                    {
                                        List<double> FRCR_REMAIN_BAL = new List<double>();                                                          // 전월 이월 원화잔액과 외화잔액 합계를 담을 List
                                        dictionary.TryGetValue(CUR_CD, out FRCR_REMAIN_BAL);                                                        // Dictionary 에서 전월이월 원화잔액, 외화잔액을 담은 List 를 꺼냄 (0 : 원화 잔액 / 1: 외화 잔액)
                                        REMAIN_WON_AMT = FRCR_REMAIN_BAL[0] + DEPOSIT_WON_AMT - OUT_WON_AMT;            // 원화잔액 연산                          
                                        REMAIN_FRCR_AMT = FRCR_REMAIN_BAL[1] + DEPOSIT_FRCR_AMT - OUT_FRCR_AMT;          // 외화잔액 연산           
                                    }
                                }
                            }

                        }
                        else
                        {
                            REMAIN_WON_AMT = REMAIN_WON_AMT + DEPOSIT_WON_AMT - OUT_WON_AMT;
                            REMAIN_FRCR_AMT = REMAIN_FRCR_AMT + DEPOSIT_FRCR_AMT - OUT_FRCR_AMT;
                        }                     
                    }
                }


                

                //-------------------------------------------------------------------------------------------------------------------
                // 원화, 외화 구분하여 원화일때는 외화 Column 에 공백, 외화일때는 Data 를 넣어준다.
                //-------------------------------------------------------------------------------------------------------------------
                if (CUR_CD.Equals("KRW"))
                {                    
                    dt.Rows.Add(SLIP_DT,
                                        CUR_CD,
                                        "",
                                        REMK_CNTS,
                                        Utils.SetComma(DEPOSIT_WON_AMT.ToString().Trim()),
                                        "",
                                        Utils.SetComma(OUT_WON_AMT.ToString().Trim()),
                                        "",
                                        Utils.SetComma(REMAIN_WON_AMT.ToString().Trim()),
                                        ""
                                        );

                    //-------------------------------------------------------------------
                    // 원화일때 누계 진행
                    //-------------------------------------------------------------------
                    SUM_DEPOSIT_MONTH_WON += DEPOSIT_WON_AMT;
                    SUM_OUT_MONTH_WON += OUT_WON_AMT;
                    SUM_REMAIN_MONTH_WON = SUM_DEPOSIT_MONTH_WON - SUM_OUT_MONTH_WON;
                }
                else
                {
                    dt.Rows.Add(SLIP_DT,
                                      CUR_CD,
                                      TRAN_EXRT,
                                      REMK_CNTS,
                                      Utils.SetComma(DEPOSIT_WON_AMT.ToString().Trim()),
                                      Utils.SetComma(DEPOSIT_FRCR_AMT),
                                      Utils.SetComma(OUT_WON_AMT.ToString().Trim()),
                                      Utils.SetComma(OUT_FRCR_AMT),
                                      Utils.SetComma(REMAIN_WON_AMT.ToString().Trim()),
                                      Utils.SetComma(REMAIN_FRCR_AMT)
                                      );
               
                    //----------------------------------------------------------------------------------
                    // 외화통화코드 구분하여 월별 합계 금액 따로 누적하기
                    //----------------------------------------------------------------------------------
                    for (int h = 0; h<CUR_CD_List.Count; h++)
                    {
                        string compareCurrencyCode = CUR_CD_List[h].ToString().Trim();
                        if (compareCurrencyCode.Equals(CUR_CD))                                                                                                             // DB 에 등록되어있는 통화코드인지 먼저 확인
                        {
                            for(int j=0; j<FRCR_COUNT_DATA_List.Count; j++)
                            {
                                //------------------------------------------------------------------------------------------
                                // Map 에 해당 CUR_CD 를 Key 로 갖는 List<double> 을 가져와 연산 Data 를 넣는다.
                                // 통화코드는 순차적으로 나오지 않으므로 그때 그때 합산 처리한다.
                                //------------------------------------------------------------------------------------------
                                Dictionary<string, List<double>> dictionary = FRCR_COUNT_DATA_List[j];                                
                                if (dictionary.ContainsKey(CUR_CD))
                                {
                                    List<double> list = new List<double>();
                                    dictionary.TryGetValue(CUR_CD, out list);
                                    if (list.Count == 0)
                                    {
                                        //------------------------------------------------------------------------------------------
                                        // 해당 CUR_CD 의 LIST 의 길이가 0 이면 Data 를 넣어줘야함
                                        //------------------------------------------------------------------------------------------
                                        list.Add(DEPOSIT_WON_AMT);                                                      // 월별 입금 원화                                        
                                        list.Add(DEPOSIT_FRCR_AMT);                                                     // 월별 입금 외화
                                        list.Add(OUT_WON_AMT);                                                             // 월별 출금 원화
                                        list.Add(OUT_FRCR_AMT);                                                            // 월별 출금 외화
                                        list.Add(DEPOSIT_WON_AMT - OUT_WON_AMT);                          // 월별 잔액 원화
                                        list.Add(DEPOSIT_FRCR_AMT - OUT_FRCR_AMT);                         // 월별 잔액 외화
                                    }
                                    else
                                    {
                                        //------------------------------------------------------------------------------------------
                                        // 해당 CUR_CD 의 LIST 길이가 0이 아니면 DATA 를 누적해줘야함
                                        //------------------------------------------------------------------------------------------
                                        list[0] += DEPOSIT_WON_AMT;                             // 월별 입금 원화
                                        list[1] += DEPOSIT_FRCR_AMT;                          // 월별 입금 외화
                                        list[2] += OUT_WON_AMT;                                 // 월별 출금 원화
                                        list[3] += OUT_FRCR_AMT;                                 // 월별 출금 외화
                                        list[4] = list[0] - list[2];                                      // 월별 잔액 원화
                                        list[5] = list[1] - list[3];                                        // 월별 잔액 외화
                                    }

                                    dictionary.Remove(CUR_CD);
                                    dictionary.Add(CUR_CD, list);
                                    FRCR_COUNT_DATA_List[j] = dictionary;
                                }
                            }
                        }
                    }
                }
                prev_CUR_CD = CUR_CD;                       //  FOR 문을 한번씩 순회할때마다 이전 통화코드 기억 변수에 값을 대입 (이후 For 문을 시작하여 전표내역 출력시 통화코드가 변했다면 별도계산)
            }


            //----------------------------------------------------------------------------------------------------------------
            // 넣으면서 각 입금, 출금 내역의 합을 구해 DataTable 로 넣어주기 (월별, 누계)
            //----------------------------------------------------------------------------------------------------------------
            //*****************************************월별 입금, 출금, 잔액 계산*****************************************
            string comma_SUM_DEPOSIT_MONTH_WON = Utils.SetComma(SUM_DEPOSIT_MONTH_WON.ToString());                                      // 당월 입금 금액 합계 (원화)
            string comma_SUM_DEPOSIT_MONTH_FRCR = Utils.SetComma(SUM_DEPOSIT_MONTH_FRCR);                                                   // 당월 입금 금액 합계 (외화)
            string comma_SUM_OUT_MONTH_WON = Utils.SetComma(SUM_OUT_MONTH_WON.ToString());                                                    // 당월 출금 금액 합계 (원화)
            string comma_SUM_OUT_MONTH_FRCR = Utils.SetComma(SUM_OUT_MONTH_FRCR);                                                                   // 당월 출금 금액 합계 (외화)
            string comma_SUM_REAMIN_MONTH_WON = Utils.SetComma(SUM_REMAIN_MONTH_WON.ToString());                                        // 당월 잔액 = 당월 입금금액 합계 - 당월 출금금액 합계 (원화)
            string comma_SUM_REAMIN_MONTH_FRCR = Utils.SetComma(SUM_REMAIN_MONTH_FRCR);                                                     // 당월 잔액 = 당월 입금금액 합계 - 당월 출금금액 합계 (외화)


            //*****************************************누계 입금, 출금, 잔액 계산*****************************************  
            string CurrencyCode_monthAndAll = "";                                                                     // 통화기호 (월별, 누계)
            double accumulate_SUM_DEPOSIT_WON = 0.0;                                                        // 누계 입금액 = 전월 총입금액 + 당월 총입금액 (원화)
            double accumulate_SUM_DEPOSIT_FRCR = 0.0;                                                       // 누계 입금액 = 전월 총입금액 + 당월 총입금액 (외화)
            double accumulate_SUM_OUT_WON = 0.0;                                                               // 누계 출금액 = 전월 총출금액 + 당월 총출금액 (원화)
            double accumulate_SUM_OUT_FRCR = 0.0;                                                              // 누계 출금액 = 전월 총출금액 + 당월 총출금액 (외화)
            double accumulate_SUM_REMAIN_WON = 0.0;                                                         // 누계 잔액 = 누계 입금액 - 누계 출금액 (원화)
            double accumulate_SUM_REMAIN_FRCR = 0.0;                                                        // 누계 잔액 = 누계 입금액 - 누계 출금액 (외화)
            
           

                

            //-------------------------------------------------------------------------------------------------------------------
            // 원화, 외화 구분하여 원화일때는 외화 Column 에 공백, 외화일때는 Data 를 넣어준다.
            //-------------------------------------------------------------------------------------------------------------------
            if (CUR_CD.Equals("KRW"))
            {
                accumulate_SUM_DEPOSIT_WON = SUM_PREV_MONTH_DEPOSIT_WON + SUM_DEPOSIT_MONTH_WON;                                                    // 누계 입금액 = 전월 총입금액 + 당월 총입금액 (원화)                
                accumulate_SUM_OUT_WON = SUM_PREV_MONTH_OUT_WON + SUM_OUT_MONTH_WON;                                                                        // 누계 출금액 = 전월 총출금액 + 당월 총출금액 (원화)                
                accumulate_SUM_REMAIN_WON = accumulate_SUM_DEPOSIT_WON - accumulate_SUM_OUT_WON;                                                         // 누계 잔액 = 누계 입금액 - 누계 출금액 (원화)                

                dt_monthAndAll.Rows.Add(CUR_CD, 
                                                        comma_SUM_DEPOSIT_MONTH_WON,                                      // 당월 입금 금액 합계  Parameter Setting (원화)
                                                        "",                                                                                           // 당월 입금 금액 합계  Parameter Setting (외화)
                                                        comma_SUM_OUT_MONTH_WON,                                            // 당월 출금 금액 합계  Parameter Setting (원화)
                                                        "",                                                                                           // 당월 출금 금액 합계  Parameter Setting (외화)
                                                        comma_SUM_REAMIN_MONTH_WON,                                      // 당월 잔액  Parameter Setting (원화)
                                                        "",                                                                                           // 당월 잔액  Parameter Setting (원화)
                                                        Utils.SetComma((accumulate_SUM_DEPOSIT_WON).ToString()),                            // 누계 입금 금액  Parameter Settring (원화)
                                                        "",                                                                                                                         // 누계 입금 금액  Parameter Settring (외화)
                                                        Utils.SetComma(accumulate_SUM_OUT_WON.ToString()),                                      // 누계 출금 금액  Parameter Settring (원화)
                                                        "",                                                                                                                         // 누계 출금 금액  Parameter Settring (외화)
                                                        Utils.SetComma(accumulate_SUM_REMAIN_WON.ToString()),                                   // 누계 잔액  Parameter Settring (원화)
                                                        ""                                                                                                                          // 누계 잔액  Parameter Settring (외화)
                                                         );                                                                                                                                      
            }
            else
            {
                /*
               * 전표정보가 외화일때 월별, 누계 계산을 위해 만들어 놓았던 List 에 들어있는 Dictionary 의 Key 값을 DB 에 등록되어있는 전체 통화 코드랑 비교 
               */
     
                for (int j = 0; j < history_Receipts_CurrencyCode.Count; j++)
                {                    
                    string PREV_HISTORY_CUR_CD = history_Receipts_CurrencyCode[j];
                    
                    //--------------------------------------------------------------------------------------------------------------------------
                    // 해당 통화코드의 해당월 합계정보를 구함
                    //--------------------------------------------------------------------------------------------------------------------------
                    for(int e=0; e< FRCR_COUNT_DATA_List.Count; e++) { 
                        Dictionary<string, List<double>> sum_monthAndAll_dic = FRCR_COUNT_DATA_List[e];
                        if (sum_monthAndAll_dic.ContainsKey(PREV_HISTORY_CUR_CD))
                        {
                            List<double> sum_monthAndAll_list = new List<double>();
                            sum_monthAndAll_dic.TryGetValue(PREV_HISTORY_CUR_CD, out sum_monthAndAll_list);

                            CurrencyCode_monthAndAll = PREV_HISTORY_CUR_CD;
                            comma_SUM_DEPOSIT_MONTH_WON = Utils.SetComma(sum_monthAndAll_list[0].ToString().Trim());
                            comma_SUM_DEPOSIT_MONTH_FRCR = Utils.SetComma(sum_monthAndAll_list[1]);
                            comma_SUM_OUT_MONTH_WON = Utils.SetComma(sum_monthAndAll_list[2].ToString().Trim());
                            comma_SUM_OUT_MONTH_FRCR = Utils.SetComma(sum_monthAndAll_list[3]);
                            comma_SUM_REAMIN_MONTH_WON = Utils.SetComma((sum_monthAndAll_list[0] - sum_monthAndAll_list[2]).ToString().Trim());
                            comma_SUM_REAMIN_MONTH_FRCR = Utils.SetComma(sum_monthAndAll_list[1] - sum_monthAndAll_list[3]);

                            //--------------------------------------------------------------------------------------------------------------------------
                            // 해당 통화코드의 전월이월정보를 가져와 누계값구함
                            //--------------------------------------------------------------------------------------------------------------------------
                            for (int r = 0; r < prevMonthFRCR_BAL_DATA.Count; r++)
                            {
                                Dictionary<string, List<double>> prevMonthDataDic = prevMonthFRCR_BAL_DATA[r];
                                if (prevMonthDataDic.ContainsKey(PREV_HISTORY_CUR_CD))
                                {
                                    List<double> list_For_accumulate = new List<double>();
                                    prevMonthDataDic.TryGetValue(PREV_HISTORY_CUR_CD, out list_For_accumulate);
                                    accumulate_SUM_DEPOSIT_WON = list_For_accumulate[0] + sum_monthAndAll_list[0];
                                    accumulate_SUM_DEPOSIT_FRCR = list_For_accumulate[1] + sum_monthAndAll_list[1];
                                    accumulate_SUM_OUT_WON = list_For_accumulate[2] + sum_monthAndAll_list[2];
                                    accumulate_SUM_OUT_FRCR = list_For_accumulate[3] + sum_monthAndAll_list[3];
                                    accumulate_SUM_REMAIN_WON = (list_For_accumulate[0] + sum_monthAndAll_list[0]) - (list_For_accumulate[2] + sum_monthAndAll_list[2]);
                                    accumulate_SUM_REMAIN_FRCR = (list_For_accumulate[1] + sum_monthAndAll_list[1]) - (list_For_accumulate[3] + sum_monthAndAll_list[3]);
                                }
                            }

                            // 통화, 월별입금원화합계, 월별입금외화합계, 월별출금원화합계, 월별출금외화합계, 월별잔액원화, 월별잔액외화, 누계입금원화, 누계입금외화, 누계출금원화, 누계출금외화, 누계잔액원화
                            dt_monthAndAll.Rows.Add(CurrencyCode_monthAndAll,
                                                                    comma_SUM_DEPOSIT_MONTH_WON,
                                                                    comma_SUM_DEPOSIT_MONTH_FRCR,
                                                                    comma_SUM_OUT_MONTH_WON,
                                                                    comma_SUM_OUT_MONTH_FRCR,
                                                                    comma_SUM_REAMIN_MONTH_WON,
                                                                    comma_SUM_REAMIN_MONTH_FRCR,
                                                                    Utils.SetComma(accumulate_SUM_DEPOSIT_WON.ToString().Trim()),
                                                                    Utils.SetComma(accumulate_SUM_DEPOSIT_FRCR),
                                                                    Utils.SetComma(accumulate_SUM_OUT_WON.ToString().Trim()),
                                                                    Utils.SetComma(accumulate_SUM_OUT_FRCR),
                                                                    Utils.SetComma(accumulate_SUM_REMAIN_WON.ToString().Trim()),
                                                                    Utils.SetComma(accumulate_SUM_REMAIN_FRCR)
                                                                    );                            
                            break;
                        }
                    }
                }
                
            }
            

            //----------------------------------------------------------------------------------
            // 설정한 Parameter 와 DataTable(DataSource) reportViewer 에 Setting 하기
            //----------------------------------------------------------------------------------
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.LocalReport.DataSources.Add(dataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(dataSource3);
            this.reportViewer1.LocalReport.SetParameters(parameter);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }





        //======================================================================================================================
        // 전월 누적 입금, 출금, 잔액 소계 가져옴
        //======================================================================================================================
        private DataTable selectPrevMonthData(string searchFromDate, string searchToDate, string division)
        {
            //-------------------------------------------------------------------
            // 검색버튼 누를시에 값이 계속 누적되는것을 막기위한 초기화
            //-------------------------------------------------------------------
            SUM_PREV_MONTH_DEPOSIT_WON = 0.0;            
            SUM_PREV_MONTH_OUT_WON = 0.0;            
            SUM_PREV_MONTH_REMAIN_WON = 0.0;            


            string searchFrom = searchFromDate;
            string searchTo = searchToDate;
            string theDivision = division;
            string CUR_CD = "";
            string query = string.Format("CALL SelectPrevMonthData('{0}','{1}')", searchFrom, theDivision);
            DataSet ds = DbHelper.SelectQuery(query);
            int rowCount = ds.Tables[0].Rows.Count;
            DataTable dt = null;

            if (rowCount == 0)
            {
                MessageBox.Show("전월 이월 정보가 없습니다.");

                return null;
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("통화");
                dt.Columns.Add("입금원화");
                dt.Columns.Add("입금외화");
                dt.Columns.Add("출금원화");
                dt.Columns.Add("출금외화");
                dt.Columns.Add("잔액원화");
                dt.Columns.Add("잔액외화");


                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];

                    CUR_CD = row["CUR_CD"].ToString().Trim();
                    double SUM_PREV_MONTH_DEPOSIT_WON_AMT = double.Parse(row["SUM_PREV_MONTH_DEPOSIT_WON_AMT"].ToString().Trim());
                    double SUM_PREV_MONTH_DEPOSIT_FRCR_AMT = double.Parse(row["SUM_PREV_MONTH_DEPOSIT_FRCR_AMT"].ToString().Trim());
                    double SUM_PREV_MONTH_OUT_WON_AMT = double.Parse(row["SUM_PREV_MONTH_OUT_WON_AMT"].ToString().Trim());
                    double SUM_PREV_MONTH_OUT_FRCR_AMT = double.Parse(row["SUM_PREV_MONTH_OUT_FRCR_AMT"].ToString().Trim());
                    double SUM_PREV_MONTH_REMAIN_WON_AMT = double.Parse(row["PREV_MONTH_REMAIN_WON_BAL"].ToString().Trim());
                    double SUM_PREV_MONTH_REMAIN_FRCR_AMT = double.Parse(row["PREV_MONTH_REMAIN_FRCR_BAL"].ToString().Trim());

                    if (CUR_CD.Equals("KRW"))
                    {
                        //-----------------------------------------------------------------------------------------------------------------------------
                        // 원화일 경우 Row 에 Data 를 넣어주면서 기간내 전표처리 연산에 사용될 deposit, out, remain 값을 멤버 변수화해서 저장
                        //-----------------------------------------------------------------------------------------------------------------------------
                        dt.Rows.Add(CUR_CD,
                                            Utils.SetComma(SUM_PREV_MONTH_DEPOSIT_WON_AMT.ToString()),
                                            "",
                                            Utils.SetComma(SUM_PREV_MONTH_OUT_WON_AMT.ToString()),
                                            "",
                                            Utils.SetComma(SUM_PREV_MONTH_REMAIN_WON_AMT.ToString()),
                                            ""
                                            );
                        
                        SUM_PREV_MONTH_DEPOSIT_WON = SUM_PREV_MONTH_DEPOSIT_WON_AMT;                        
                        SUM_PREV_MONTH_OUT_WON = SUM_PREV_MONTH_OUT_WON_AMT;
                        SUM_PREV_MONTH_REMAIN_WON = SUM_PREV_MONTH_REMAIN_WON_AMT;

                    }
                    else
                    {
                        if (CUR_CD != "")
                        {
                            dt.Rows.Add(CUR_CD,
                                                Utils.SetComma(SUM_PREV_MONTH_DEPOSIT_WON_AMT.ToString()),
                                                Utils.SetComma(SUM_PREV_MONTH_DEPOSIT_FRCR_AMT),
                                                Utils.SetComma(SUM_PREV_MONTH_OUT_WON_AMT.ToString()),
                                                Utils.SetComma(SUM_PREV_MONTH_OUT_FRCR_AMT),
                                                Utils.SetComma(SUM_PREV_MONTH_REMAIN_WON_AMT.ToString()),
                                                Utils.SetComma(SUM_PREV_MONTH_REMAIN_FRCR_AMT)
                                                );

                            //---------------------------------------------------------------------------------------
                            // 출납장의 원화, 외화별 연산 잔액 및 누계 잔액 표기를 위해 외화 잔액부 Data 따로 보관
                            //---------------------------------------------------------------------------------------
                            Dictionary<string, List<double>> prevMonthbalanceData = new Dictionary<string, List<double>>();
                            List<double> prevMonthWonFrcrBal = new List<double>();
                            prevMonthWonFrcrBal.Add(SUM_PREV_MONTH_DEPOSIT_WON_AMT);                                         // 전월 이월 입금 합계 원화금액
                            prevMonthWonFrcrBal.Add(SUM_PREV_MONTH_DEPOSIT_FRCR_AMT);                                         // 전월 이월 입금 합계 외화금액
                            prevMonthWonFrcrBal.Add(SUM_PREV_MONTH_OUT_WON_AMT);                                                 // 전월 이월 출금 합계 원화금액    
                            prevMonthWonFrcrBal.Add(SUM_PREV_MONTH_OUT_FRCR_AMT);                                               // 전월 이월 출금 합계 외화금액                            
                            prevMonthWonFrcrBal.Add(SUM_PREV_MONTH_REMAIN_WON_AMT);                                         // 전월 이월 잔액 원화금액
                            prevMonthWonFrcrBal.Add(SUM_PREV_MONTH_REMAIN_FRCR_AMT);                                        // 전월 이월 잔액 외화금액
                            prevMonthbalanceData.Add(CUR_CD, prevMonthWonFrcrBal);
                            prevMonthFRCR_BAL_DATA.Add(prevMonthbalanceData);
                        }
                        else
                        {
                            dt.Rows.Add(CUR_CD,
                                                "",
                                                "",
                                                "",
                                                "",
                                                "",
                                                ""
                                                );
                        }
                    }
                }
            }

            return dt;
        }









        //======================================================================================================================
        // DB 에 등록되어있는 통화코드 모두 불러오기
        //======================================================================================================================
        private List<string> getAllRegistedCurrencyCode()
        {
            List<string> CUR_CD_List = new List<string>();
            string getCurrencyCodeQuery = "SELECT CUR_CD FROM TB_CUR_L";
            DataSet CurrencyCodesDataSet = DbHelper.SelectQuery(getCurrencyCodeQuery);
            int registedCurrencyCodeCount = CurrencyCodesDataSet.Tables[0].Rows.Count;
            if (registedCurrencyCodeCount == 0)
            {
                MessageBox.Show("등록된 통화코드가 존재하지 않습니다.");
            }
            else
            {
                for (int i = 0; i < registedCurrencyCodeCount; i++)
                {
                    DataRow row = CurrencyCodesDataSet.Tables[0].Rows[i];
                    CUR_CD_List.Add(row["CUR_CD"].ToString().Trim());
                }
            }

            return CUR_CD_List;
        }










        //======================================================================================================================
        // DB 에 등록되어있는 통화코드의 합계정보를 담을  Map 과 그 Map 을 담을 List 만듦
        //======================================================================================================================
        private List<Dictionary<string, List<double>>> createDictionaryForFRCR(List<string> CUR_CD_List)
        {
            List<Dictionary<string, List<double>>> FRCR_COUNT_DATA_List = new List<Dictionary<string, List<double>>>();

            for (int i=0; i<CUR_CD_List.Count; i++)
            {
                string CUR_CD = CUR_CD_List[i].ToString().Trim();
                Dictionary<string, List<double>> FRCR_SUM_DIC = new Dictionary<string, List<double>>();
                List<double> FRCR_SUM_LIST = new List<double>();
                FRCR_SUM_DIC.Add(CUR_CD, FRCR_SUM_LIST);
                FRCR_COUNT_DATA_List.Add(FRCR_SUM_DIC);
            }        
            

            return FRCR_COUNT_DATA_List;
        }









        //======================================================================================================================
        // 닫기 버튼 Click 동작
        //======================================================================================================================
        private void closeFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}