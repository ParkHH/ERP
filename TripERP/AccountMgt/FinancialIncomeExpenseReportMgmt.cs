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

namespace TripERP.Report
{
    public partial class FinancialIncomeExpenseReportMgmt : Form
    {

        // 보통예금 외화별 합계 도출하는데 필요한 List
        List<Dictionary<string, List<double>>> FRCR_SUM_LIST = new List<Dictionary<string, List<double>>>();

        // Dictionary 의 key 값을 확인하기 위해 출력되는 외화코드를 담아놓는 List
        List<string> FRCR_CUR_CD_LIST = new List<string>();

        public FinancialIncomeExpenseReportMgmt()
        {
            InitializeComponent();
            //
        }

        //=============================================================
        // Form Load EventMethod
        //=============================================================
        private void FinancialIncomeExpenseReportMgmt_Load(object sender, EventArgs e)
        {
            reportViewerLoad();                                                                 // reportViewer Load       
        }

        //----------------------------------------------------------------------------------------------------
        // reportViewer Load  동작 Method -> reportViewer 속성값 설정!        
        //----------------------------------------------------------------------------------------------------
        public void reportViewerLoad()
        {
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TripERP.AccountMgt.FinancialIncomeExpense.rdlc";      // 보고서와 ReportViewer 연결
            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;                  // Report Mode 설정 (현재 Local)
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);                // reportViewer 의 Layout 설정 (현재 인쇄Layoout)
            this.reportViewer1.Size = new Size(this.groupBox2.Width - 20, this.groupBox2.Height - 50);                 // reportViewer Size 조절 (현재 WindowForm 크기에 맞게)       
            this.reportViewer1.Location = new Point(this.groupBox2.Location.X + 10, this.groupBox2.Location.Y + 40);
            this.reportViewer1.MouseWheel += new MouseEventHandler(zoomInOutMouseWheel);    // ReportViewer Zoom InOUt 동작 단축키 관련 EventHandler (MouseWheel 동작 관련)
            this.reportViewer1.ShowZoomControl = true;

            PageSettings pg = new PageSettings();
            pg.Margins.Top = 10;
            pg.Margins.Left = 10;
            pg.Margins.Right = 10;
            pg.Margins.Bottom = 10;
            pg.Landscape = false;                                                                // 용지 세로,가로 Default 값 설정 (True : 가로 / False : 세로)            
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();

            DataTable dt = new DataTable();
            ReportDataSource rDS = new ReportDataSource("DataSet1", dt);
            ReportDataSource rDS2 = new ReportDataSource("DataSet2", dt);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rDS);
            this.reportViewer1.LocalReport.DataSources.Add(rDS2);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();


            //DataRow[] settlementDataArr = getSettlementSateData();      // DB 에서 Data 가져오는 Method
            //dataRemake(settlementDataArr);                                       // 가져온 Data ReportViewer Table 에 출력하는 Method
        }







        //=============================================================
        // Ctrl + MouseWheel 동작시 보고서 배율 확대   
        //=============================================================
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
        // 검색 버튼 Click 동작 Event Method
        //======================================================================================================================
        private void searchAccountCodeListButton_Click(object sender, EventArgs e)
        {
            searchFinancialIncomeExpenseData();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 일일자금수지일보 Data Select        
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void searchFinancialIncomeExpenseData()
        {
            /*
             * ********************************
             *  >> 가져와야하는 정보
             * ********************************
             *     1.보통예금 정보
             *          1) 전일잔액
             *          2) 입금액 (당일)
             *          3) 출금액 (당일)
             *          4) 금일 잔액
             *          5) 외화 잔액 (USD, EUR)
             *          6) 예금 합계 (전일잔액 합계, 당일 임금액 합계, 당일 출금액 합계, 금일잔액합계, 외화잔액합계(USD, EUR)
             *          
             *      2. 기타제예금 정보
             *          1) 제예금 (전일잔액, 당일입금액, 당일출금액, 금일잔액)
             *          2) 예수금 (전일잔액, 당일입금액, 당일출금액, 금일잔액)
             *          3) 기타제예금 합계 (전일잔액 합계, 당일 입금액 합계, 당일 출금액 합계, 금일 잔액 합계)
             */

            selectNormalAccountInfo();              // 보통 예금 정보 select 및 reportViewer 에 Insert
            selectOtherAccountInfo();               // 기타 제예금 정보 SELECT 및 PARAMETER SETTING
        }




        //======================================================================================================================
        // 보통예금 정보 SELECT
        //======================================================================================================================
        private void selectNormalAccountInfo()
        {
            FRCR_CUR_CD_LIST.Clear();
            FRCR_SUM_LIST.Clear();

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 1. 보통예금 정보 Select
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 원화
            double SUM_PREV_DAY_BAL = 0.0;
            double SUM_DEPOSIT_AMT = 0.0;
            double SUM_OUT_AMT = 0.0;
            double SUM_DALY_REMAIN_AMT = 0.0;

            // 외화
            double SUM_PREV_DAY_FRCR_BAL = 0.0;
            double SUM_FRCR_DEPOSIT_AMT = 0.0;
            double SUM_FRCR_OUT_AMT = 0.0;
            double SUM_FRCR_REMAIN_AMT = 0.0;
            List<double> SUM_FRCR_DATA_LIST = null;
            Dictionary<string, List<double>> FRCR_SUM_DATA_LIST = null;

            // FOR 문 순환시 이전 통화코드와 변경사항 있는지 판단을 위한 메모리 변수 선언
            string PREV_CUR_CD = "";
            

            //************************************************
            // DB Select 정보와 대응되는 DataTable 을 선언
            //************************************************
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("보통예금은행명");
            dt1.Columns.Add("계좌구분");
            dt1.Columns.Add("통화코드");
            dt1.Columns.Add("전일잔액");            
            dt1.Columns.Add("입금");
            dt1.Columns.Add("출금");
            dt1.Columns.Add("금일잔액");

            ReportDataSource NormalAccountDataSource = new ReportDataSource("DataSet1", dt1);

            //************************************************
            // Query 선언 및 실행
            //************************************************
            string query = string.Format("CALL SelectFinancialIncomExpenseNormalAccountInfo('{0}')", dateTimePicker1.Value.ToShortDateString().Substring(0, 10));
            DataSet ds = DbHelper.SelectQuery(query);
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                MessageBox.Show("보통예금 조회 내역이 존재하지 않습니다.");
                return;
            }

            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string ACCOUNT_NAME = row["BANK_NM"].ToString().Trim();
                string ACCOUNT_TEXT = row["ACCT_NM"].ToString().Trim();            
                string CUR_CD = row["CUR_CD"].ToString().Trim();                
                double PREV_DAY_BAL = 0.0;
                double DEPOSIT_AMT = 0.0;
                double OUT_AMT = 0.0;
                double DALY_REMAIN_AMT = 0.0;
                //double USD = 0.0;
                //double EUR = 0.0;
                if (CUR_CD.Equals("KRW"))
                {
                    PREV_DAY_BAL = double.Parse(row["PREV_DAY_WON_BAL"].ToString().Trim());
                    DEPOSIT_AMT = double.Parse(row["DALY_SUM_DEPOSIT_WON_AMT"].ToString().Trim());
                    OUT_AMT = double.Parse(row["DALY_SUM_OUT_WON_AMT"].ToString().Trim());
                    DALY_REMAIN_AMT = PREV_DAY_BAL + DEPOSIT_AMT - OUT_AMT;

                    //*********************************************
                    // 각 행에 출력될 내용에서 보통예금 합계금액을 구함 (원화)
                    //*********************************************
                    SUM_PREV_DAY_BAL += PREV_DAY_BAL;
                    SUM_DEPOSIT_AMT += DEPOSIT_AMT;
                    SUM_OUT_AMT += OUT_AMT;
                    SUM_DALY_REMAIN_AMT += DALY_REMAIN_AMT;

                    dt1.Rows.Add( ACCOUNT_NAME, 
                                           ACCOUNT_TEXT, 
                                           CUR_CD, 
                                           Utils.SetComma(PREV_DAY_BAL.ToString()), 
                                           Utils.SetComma(DEPOSIT_AMT.ToString()), 
                                           Utils.SetComma(OUT_AMT.ToString()), 
                                           Utils.SetComma(DALY_REMAIN_AMT.ToString()));
                }
                else
                {
                    // 외화코드에 관련한 전일금액, 금일입금액, 금일출금액, 금일잔액 누적값을 담은 List 내부 Dictionary key 값과 비교하기 위해 외화코드의 Key 값을 List 에 담아놓음
                    FRCR_CUR_CD_LIST.Add(CUR_CD);

                    PREV_DAY_BAL = double.Parse(row["PREV_DAY_FRCR_BAL"].ToString().Trim());
                    DEPOSIT_AMT = double.Parse(row["DALY_SUM_DEPOSIT_FRCR_AMT"].ToString().Trim());
                    OUT_AMT = double.Parse(row["DALY_SUM_OUT_FRCR_AMT"].ToString().Trim());
                    DALY_REMAIN_AMT = PREV_DAY_BAL + DEPOSIT_AMT - OUT_AMT;
                    SUM_FRCR_DATA_LIST = new List<double>();
                    FRCR_SUM_DATA_LIST = new Dictionary<string, List<double>>();

                    //**************************************************
                    // 각 행에 출력될 내용에서 보통예금 합계금액을 구함 (외화 구분)
                    //**************************************************
                    if (!CUR_CD.Equals(PREV_CUR_CD))
                    {
                        
                        if (i == 0)
                        {
                            SUM_PREV_DAY_FRCR_BAL += PREV_DAY_BAL;
                            SUM_FRCR_DEPOSIT_AMT += DEPOSIT_AMT;
                            SUM_FRCR_OUT_AMT += OUT_AMT;
                            SUM_FRCR_REMAIN_AMT += DALY_REMAIN_AMT;                            
                        }
                        else
                        {
                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            // 새로 출력한 외화코드가 이전 외화코드와 다르다면 기존에 누적되던 변수값을 List 에 담아 Dictionary 에 이전 외화코드를 Key 값으로 저장
                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            SUM_FRCR_DATA_LIST.Add(SUM_PREV_DAY_FRCR_BAL);                  // 전일 잔액
                            SUM_FRCR_DATA_LIST.Add(SUM_FRCR_DEPOSIT_AMT);                   // 금일 입금액 합계
                            SUM_FRCR_DATA_LIST.Add(SUM_FRCR_OUT_AMT);                           // 금일 출금액 합계
                            SUM_FRCR_DATA_LIST.Add(SUM_FRCR_REMAIN_AMT);                    // 금일 잔액
                            
                            FRCR_SUM_DATA_LIST.Add(PREV_CUR_CD, SUM_FRCR_DATA_LIST);
                            FRCR_SUM_LIST.Add(FRCR_SUM_DATA_LIST);

                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            // 이후 누적 변수 초기화하고 새로 누적 시작
                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            SUM_PREV_DAY_FRCR_BAL = 0.0;
                            SUM_FRCR_DEPOSIT_AMT = 0.0;
                            SUM_FRCR_OUT_AMT = 0.0;
                            SUM_FRCR_REMAIN_AMT = 0.0;

                            SUM_PREV_DAY_FRCR_BAL += PREV_DAY_BAL;
                            SUM_FRCR_DEPOSIT_AMT += DEPOSIT_AMT;
                            SUM_FRCR_OUT_AMT += OUT_AMT;
                            SUM_FRCR_REMAIN_AMT += DALY_REMAIN_AMT;
                            
                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            // For 문의 마지막 순환에서는 다음에 비교할 외화코드가 없으므로 바로 넣어줌
                            //--------------------------------------------------------------------------------------------------------------------------------------------------
                            if (i== rowCount - 1)
                            {
                                // Dictionary 와 List 를 새로 생성해서 넣어주지 않으면 For문의 마지막 순환에서 FRCR_SUM_LIST 의 마지막 2개 Index 에 각각 2개의 Data 가 들어감
                                Dictionary<string, List<double>> FRCR_SUM_DATA_LIST2 = new Dictionary<string, List<double>>();
                                List<double> SUM_FRCR_DATA_LIST2 = new List<double>();
                                SUM_FRCR_DATA_LIST2.Add(SUM_PREV_DAY_FRCR_BAL);                  // 전일 잔액
                                SUM_FRCR_DATA_LIST2.Add(SUM_FRCR_DEPOSIT_AMT);                   // 금일 입금액 합계
                                SUM_FRCR_DATA_LIST2.Add(SUM_FRCR_OUT_AMT);                           // 금일 출금액 합계
                                SUM_FRCR_DATA_LIST2.Add(SUM_FRCR_REMAIN_AMT);                    // 금일 잔액

                                FRCR_SUM_DATA_LIST2.Add(CUR_CD, SUM_FRCR_DATA_LIST2);
                                FRCR_SUM_LIST.Add(FRCR_SUM_DATA_LIST2);
                            }
                        }                                                                                                             
                    }
                    else
                    {
                        SUM_PREV_DAY_FRCR_BAL += PREV_DAY_BAL;
                        SUM_FRCR_DEPOSIT_AMT += DEPOSIT_AMT;
                        SUM_FRCR_OUT_AMT += OUT_AMT;
                        SUM_FRCR_REMAIN_AMT += DALY_REMAIN_AMT;
                    }

                    

                    dt1.Rows.Add(   ACCOUNT_NAME, 
                                            ACCOUNT_TEXT, 
                                            CUR_CD, 
                                            Utils.SetComma(PREV_DAY_BAL), 
                                            Utils.SetComma(DEPOSIT_AMT), 
                                            Utils.SetComma(OUT_AMT), 
                                            Utils.SetComma(DALY_REMAIN_AMT));

                    // 출력되는 통화코드의 변화를 파악하기 위해 메모리 변수에 출력되는 통화코드를 대입
                    PREV_CUR_CD = CUR_CD;
                }                
            }

            //*****************************************
            // parameter 에 구한 합계 정보 Setting
            //*****************************************
            //----------------------------------------------
            // 원화정보는 별도로 Parameter 처리!
            //----------------------------------------------
            var parameter = new ReportParameter[4];

            parameter[0] = new ReportParameter("SUM_PREV_DAY_BAL", Utils.SetComma(SUM_PREV_DAY_BAL.ToString()));
            parameter[1] = new ReportParameter("SUM_DEPOSIT_AMT", Utils.SetComma(SUM_DEPOSIT_AMT.ToString()));
            parameter[2] = new ReportParameter("SUM_OUT_AMT", Utils.SetComma(SUM_OUT_AMT.ToString()));
            parameter[3] = new ReportParameter("SUM_DALY_REMAIN_AMT", Utils.SetComma(SUM_DALY_REMAIN_AMT.ToString()));

            //---------------------------------------------------
            // 외화정보는 DataTable 을 이용하여 동적 처리!
            //---------------------------------------------------
            DataTable dt2 = new DataTable();            
            dt2.Columns.Add("외화코드");
            dt2.Columns.Add("전일금액합계");
            dt2.Columns.Add("금일입금액합계");
            dt2.Columns.Add("금일출금액합계");
            dt2.Columns.Add("금일잔액합계");

            ReportDataSource dataSourceForSumForeignCurrencyCode = new ReportDataSource("DataSet2", dt2);        

            for (int j = 0; j < FRCR_CUR_CD_LIST.Count; j++)
            {
                string FRCR_CUR_CD = FRCR_CUR_CD_LIST[j];
                for (int i = 0; i < FRCR_SUM_LIST.Count; i++)
                {
                    Dictionary<string, List<double>> dic = FRCR_SUM_LIST[i];
                    if (dic.ContainsKey(FRCR_CUR_CD))
                    {
                        List<double> frcr_sum_data_list = null;
                        dic.TryGetValue(FRCR_CUR_CD, out frcr_sum_data_list);

                        string sum_prev_day_frcr_bal = Utils.SetComma(frcr_sum_data_list[0]);
                        string sum_frcr_deposit_amt = Utils.SetComma(frcr_sum_data_list[1]);
                        string sum_frcr_out_amt = Utils.SetComma(frcr_sum_data_list[2]);
                        string sum_frcr_remain_amt = Utils.SetComma(frcr_sum_data_list[3]);

                        dt2.Rows.Add(   FRCR_CUR_CD, 
                                                sum_prev_day_frcr_bal, 
                                                sum_frcr_deposit_amt, 
                                                sum_frcr_out_amt, 
                                                sum_frcr_remain_amt);

                        break;
                    }

                }
            }


            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(NormalAccountDataSource);
            this.reportViewer1.LocalReport.DataSources.Add(dataSourceForSumForeignCurrencyCode);
            this.reportViewer1.LocalReport.SetParameters(parameter);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }





        //======================================================================================================================
        // 기타 제예금 정보 및 메모 정보 SELECT
        //======================================================================================================================
        private void selectOtherAccountInfo()
        {
            //-------------------------------------------------------------------
            // Procedure  호출!
            //-------------------------------------------------------------------
            string query = string.Format("CALL SelectOtherAccountInfoForFinancialIncomExpense('{0}')", dateTimePicker1.Value.ToShortDateString().Substring(0,10));
            DataSet ds = DbHelper.SelectQuery(query);
            int rowCount = ds.Tables[0].Rows.Count;
            if(rowCount == 0)
            {
                /*
                 * rowCount 가 0 일 경우는 해당 기준일자에 저장된 기타제예금 정보가 존재하지 않는것
                 * 삭제 버튼을 눌러 Data 를 삭제하여 해당일자에 Data 가 없는경우를 대비하여 기타제예금과 관련된 Parameter 의 값들을 비워준다.
                 * 비워주지 않으면 reportViewer 에 setting 된 Parameter 가 그대로 잔존하여 reportViewer 를 refresh 했을 경우에도 값이 출력된다.
                 */
                //MessageBox.Show("해당일자에 조회된 기타제예금 정보가 존재하지 않습니다.");
                var reportParameter = new ReportParameter[16];
                reportParameter[0] = new ReportParameter("OTHER_BANK_NAME", "");
                reportParameter[1] = new ReportParameter("OTHER_PREV_REMAIN_AMT_1", "");
                reportParameter[2] = new ReportParameter("OTHER_DEPOSIT_AMT_1", "");
                reportParameter[3] = new ReportParameter("OTHER_OUT_AMT_1", "");
                reportParameter[4] = new ReportParameter("OTHER_REMAIN_AMT_1", "");
                reportParameter[5] = new ReportParameter("OTHER_BANK_NAME", "");
                reportParameter[6] = new ReportParameter("OTHER_PREV_REMAIN_AMT_2", "");
                reportParameter[7] = new ReportParameter("OTHER_DEPOSIT_AMT_2", "");
                reportParameter[8] = new ReportParameter("OTHER_OUT_AMT_2", "");
                reportParameter[9] = new ReportParameter("OTHER_REMAIN_AMT_2", "");
                reportParameter[10] = new ReportParameter("SUM_PREV_OTHER_AMT", "");
                reportParameter[11] = new ReportParameter("SUN_OTHER_DEPOSIT_AMT", "");
                reportParameter[12] = new ReportParameter("SUM_OTHER_OUT_AMT", "");
                reportParameter[13] = new ReportParameter("SUM_OTHER_CURRENT_AMT", "");
                reportParameter[14] = new ReportParameter("MEMO_DEPOSIT", "");
                reportParameter[15] = new ReportParameter("MEMO_OUT", "");

                this.reportViewer1.LocalReport.SetParameters(reportParameter);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            else
            {
                //-------------------------------------------------------------------
                //  제예금, 예수금 조회결과의 공통값 담을 변수 설정
                //-------------------------------------------------------------------   
                string BANK_NM = "";
                string RECT_REMK = "";
                string WHDR_REMK = "";

                //-------------------------------------------------------------------
                //  제예금, 예수금 조회값을 담을 변수 설정 (합계 금액 구하는데 이용)
                //-------------------------------------------------------------------                
                double PRDY_BAL = 0.0;
                double RECT_AMT = 0.0;
                double WHDR_AMT = 0.0;
                double TDY_BAL = 0.0;

                double PRDY_BAL2 = 0.0;
                double RECT_AMT2 = 0.0;
                double WHDR_AMT2 = 0.0;
                double TDY_BAL2 = 0.0;

                //-----------------------------------------------------------------------------
                // 조회 결과의 행을 꺼내 내부 Data 를 parameter 화
                //-----------------------------------------------------------------------------
                for (int i=0; i<rowCount; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];

                    string DVSN_CD = row["DEPOSIT_DVSN_CD"].ToString().Trim();
                    RECT_REMK = row["RECT_REMK"].ToString().Trim();
                    WHDR_REMK = row["WHDR_REMK"].ToString().Trim();


                    //-----------------------------------------------------------------------------
                    // 제예금, 예수금 구분코드에 따라 다르게 동작
                    // >> 제예금일 경우 위 변수중 제예금에 해당하는 변수에 결과값 넣기
                    // >> 예수금도 마찬가지
                    //-----------------------------------------------------------------------------
                    if (DVSN_CD.Equals("1"))
                    {
                        var reportParameter = new ReportParameter[11];

                        BANK_NM = row["BANK_NM"].ToString();
                        PRDY_BAL = double.Parse(row["PRDY_BAL"].ToString());
                        RECT_AMT = double.Parse(row["RECT_AMT"].ToString());
                        WHDR_AMT = double.Parse(row["WHDR_AMT"].ToString());
                        TDY_BAL = double.Parse(row["TDY_BAL"].ToString());

                        reportParameter[0] = new ReportParameter("OTHER_BANK_NAME", BANK_NM);
                        reportParameter[1] = new ReportParameter("OTHER_PREV_REMAIN_AMT_1", Utils.SetComma(PRDY_BAL.ToString()));
                        reportParameter[2] = new ReportParameter("OTHER_DEPOSIT_AMT_1", Utils.SetComma(RECT_AMT.ToString()));
                        reportParameter[3] = new ReportParameter("OTHER_OUT_AMT_1", Utils.SetComma(WHDR_AMT.ToString()));
                        reportParameter[4] = new ReportParameter("OTHER_REMAIN_AMT_1", Utils.SetComma(TDY_BAL.ToString()));

                        //-----------------------------------------------------------------------------
                        // 합계 Data 를 구하고 paramter setting
                        // 조회 Data 중 공통부분도 이곳에서 parameter 화 한다.
                        //-----------------------------------------------------------------------------
                        reportParameter[5] = new ReportParameter("SUM_PREV_OTHER_AMT", Utils.SetComma((PRDY_BAL + PRDY_BAL2).ToString()));
                        reportParameter[6] = new ReportParameter("SUN_OTHER_DEPOSIT_AMT", Utils.SetComma((RECT_AMT + RECT_AMT2).ToString()));
                        reportParameter[7] = new ReportParameter("SUM_OTHER_OUT_AMT", Utils.SetComma((WHDR_AMT + WHDR_AMT2).ToString()));
                        reportParameter[8] = new ReportParameter("SUM_OTHER_CURRENT_AMT", Utils.SetComma((TDY_BAL + TDY_BAL2).ToString()));
                        reportParameter[9] = new ReportParameter("MEMO_DEPOSIT", RECT_REMK);
                        reportParameter[10] = new ReportParameter("MEMO_OUT", WHDR_REMK);

                        //-----------------------------------------------------------------------------
                        // parameter  setting 을 완료한 후  reportViewer 새로고침
                        //-----------------------------------------------------------------------------
                        this.reportViewer1.LocalReport.SetParameters(reportParameter);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                    }
                    else
                    {
                        var reportParameter = new ReportParameter[11];

                        BANK_NM = row["BANK_NM"].ToString();
                        PRDY_BAL2 = double.Parse(row["PRDY_BAL"].ToString());
                        RECT_AMT2 = double.Parse(row["RECT_AMT"].ToString());
                        WHDR_AMT2 = double.Parse(row["WHDR_AMT"].ToString());
                        TDY_BAL2 = double.Parse(row["TDY_BAL"].ToString());

                        reportParameter[0] = new ReportParameter("OTHER_BANK_NAME", BANK_NM);
                        reportParameter[1] = new ReportParameter("OTHER_PREV_REMAIN_AMT_2", Utils.SetComma(PRDY_BAL2.ToString()));
                        reportParameter[2] = new ReportParameter("OTHER_DEPOSIT_AMT_2", Utils.SetComma(RECT_AMT2.ToString()));
                        reportParameter[3] = new ReportParameter("OTHER_OUT_AMT_2", Utils.SetComma(WHDR_AMT2.ToString()));
                        reportParameter[4] = new ReportParameter("OTHER_REMAIN_AMT_2", Utils.SetComma(TDY_BAL2.ToString()));

                        //-----------------------------------------------------------------------------
                        // 합계 Data 를 구하고 paramter setting
                        // 조회 Data 중 공통부분도 이곳에서 parameter 화 한다.
                        //-----------------------------------------------------------------------------
                        reportParameter[5] = new ReportParameter("SUM_PREV_OTHER_AMT", Utils.SetComma((PRDY_BAL + PRDY_BAL2).ToString()));
                        reportParameter[6] = new ReportParameter("SUN_OTHER_DEPOSIT_AMT", Utils.SetComma((RECT_AMT + RECT_AMT2).ToString()));
                        reportParameter[7] = new ReportParameter("SUM_OTHER_OUT_AMT", Utils.SetComma((WHDR_AMT + WHDR_AMT2).ToString()));
                        reportParameter[8] = new ReportParameter("SUM_OTHER_CURRENT_AMT", Utils.SetComma((TDY_BAL + TDY_BAL2).ToString()));
                        reportParameter[9] = new ReportParameter("MEMO_DEPOSIT", RECT_REMK);
                        reportParameter[10] = new ReportParameter("MEMO_OUT", WHDR_REMK);

                        //-----------------------------------------------------------------------------
                        // parameter  setting 을 완료한 후  reportViewer 새로고침
                        //-----------------------------------------------------------------------------
                        this.reportViewer1.LocalReport.SetParameters(reportParameter);
                        this.reportViewer1.LocalReport.Refresh();
                        this.reportViewer1.RefreshReport();
                    }                 
                }                                           
            }          
        }






        //======================================================================================================================
        // 닫기 버튼 Click 동작
        //======================================================================================================================
        private void closeFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }









        //======================================================================================================================
        // 적용 버튼 Click 동작
        //======================================================================================================================
        private void bt_apply_Click(object sender, EventArgs e)
        {
            applyData();
            searchFinancialIncomeExpenseData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 적용 버튼 동작 Method
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void applyData()
        {            
            var reportParameter = new ReportParameter[15];

            //----------------------
            // 입력값 가져오기
            //----------------------
            string other_bank_name = tb_other_bank_name.Text.Trim();                           // 기재 제예금, 예수금 은행명         
            string other_prev_remain_amt = tb_other_prev_remain_amt.Text.Trim();        // 제예금 입금액
            string other_deposit_amt = tb_other_deposit_amt.Text.Trim();                        // 제예금 입금액
            string other_out_amt = tb_other_out_amt.Text.Trim();                                    // 제예금 출금액
            string other_remain_amt = tb_other_remain_amt.Text.Trim();                          // 제예금 금일 잔액

            string other_prev_remain_amt2 = tb_other_prev_remain_amt2.Text.Trim();  // 예수금 전일잔액
            string other_deposit_amt2 = tb_other_deposit_amt2.Text.Trim();                  // 예수금 입금액
            string other_out_amt2 = tb_other_out_amt2.Text.Trim();                              // 예수금 출금액
            string other_remain_amt2 = tb_other_remain_amt2.Text.Trim();                    // 예수금 금일잔액

            string memo_deposit = tb_deposit_memo.Text.Trim();                                  // 비고 (입금)
            string memo_out = tb_out_memo.Text.Trim();                                              // 비고 (출금)

            //-----------------------------------
            // List 에 담아 유효성 Check 진행
            //-----------------------------------
            List<string> inputDataList = new List<string>();            
            inputDataList.Add(other_prev_remain_amt);
            inputDataList.Add(other_deposit_amt);
            inputDataList.Add(other_out_amt);
            inputDataList.Add(other_remain_amt);
            inputDataList.Add(other_prev_remain_amt2);
            inputDataList.Add(other_deposit_amt2);
            inputDataList.Add(other_out_amt2);
            inputDataList.Add(other_remain_amt2);

            //-----------------------------------
            // 입력값 유효성 검사
            //-----------------------------------
            bool result = validateInputData(inputDataList);

            if (!result)
            {
                return;
            }

            //-----------------------------------------------------------
            // 입력내용을 DB 에 저장!
            //-----------------------------------------------------------
            string RGST_DT = dateTimePicker1.Value.ToShortDateString().Substring(0, 10);            // 등록일자 (조회일자)
            string ACNT_ID = Global.loginInfo.ACNT_ID;

            registData(RGST_DT,
                            other_bank_name, 
                            other_prev_remain_amt, 
                            other_deposit_amt, 
                            other_out_amt, 
                            other_remain_amt, 
                            other_prev_remain_amt2, 
                            other_deposit_amt2, 
                            other_out_amt2, 
                            other_remain_amt2, 
                            memo_deposit, 
                            memo_out,
                            ACNT_ID);
        }




        //======================================================================================================================
        // 기타 제예금 입력내용을 DB 에 저장하는 Method
        //======================================================================================================================
        private void registData(string RGST_DT,
                                           string other_bank_name,
                                           string other_prev_remain_amt,
                                           string other_deposit_amt,
                                           string other_out_amt,
                                           string other_remain_amt,
                                           string other_prev_remain_amt2,
                                           string other_deposit_amt2,
                                           string other_out_amt2,
                                           string other_remain_amt2,
                                           string memo_deposit,
                                           string memo_out,
                                           string ACNT_ID)
        {
            string query = string.Format("CALL InsertUpdateOtherAccountInfoForFinancialImcomExpense('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",     RGST_DT,       
                                                                                                                                                                                                                                                                                other_bank_name,
                                                                                                                                                                                                                                                                                other_prev_remain_amt,
                                                                                                                                                                                                                                                                                other_deposit_amt,
                                                                                                                                                                                                                                                                                other_out_amt,
                                                                                                                                                                                                                                                                                other_remain_amt,
                                                                                                                                                                                                                                                                                other_prev_remain_amt2,
                                                                                                                                                                                                                                                                                other_deposit_amt2,
                                                                                                                                                                                                                                                                                other_out_amt2,
                                                                                                                                                                                                                                                                                other_remain_amt2,
                                                                                                                                                                                                                                                                                memo_deposit,
                                                                                                                                                                                                                                                                                memo_out,
                                                                                                                                                                                                                                                                                ACNT_ID
                                                                                                                                                                                                                                                                        );
            int result = DbHelper.ExecuteNonQuery(query);
            string message = "";

            if (result < 0)
            {
                message = "Data 등록에 실패";
            }
            else
            {
                message = "Data 등록 성공";
            }
            //MessageBox.Show(message);
        }







        //======================================================================================================================
        // 입력값 유효성 검사 Method
        //======================================================================================================================
        private bool validateInputData(List<string> inputDataList)
        {
            bool resultFlag = false;

            if(inputDataList[0] == "")
            {                
                return true;
            }

            //---------------------------------------------------------------------------
            // 매개변수 List 에서 string 을 하나씩 꺼내 int.Parse() 동작 수행
            // 만약 문자가 들어있다면 Exception 이 발생할것임...
            //---------------------------------------------------------------------------
            try
            {
                for (int i = 0; i < inputDataList.Count; i++)
                {
                    string inputData = inputDataList[i];
                    if(inputData == "")
                    {
                        inputData = "0";
                    }
                    int.Parse(inputData);

                }

                resultFlag = true;
            }
            catch
            {
                MessageBox.Show("기타제예금 항목에는 숫자만 입력할 수 있습니다.");                
            }

            return resultFlag;
        }










        //======================================================================================================================
        // 초기화 버튼 Click 동작
        //======================================================================================================================
        private void button6_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 초기화 버튼 동작 Method
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void resetAll()
        {
            tb_other_bank_name.ResetText();                 // 은행명
            tb_other_prev_remain_amt.ResetText();       // 제예금 전일 잔액
            tb_other_deposit_amt.ResetText();               // 제예금 입금금액
            tb_other_out_amt.ResetText();                       // 제예금 출금금액
            tb_other_remain_amt.ResetText();                // 제예금 잔액
            tb_other_prev_remain_amt2.ResetText();      // 예수금 전일 잔액
            tb_other_deposit_amt2.ResetText();              // 예수금 입금금액
            tb_other_out_amt2.ResetText();                  // 예수금 출금금액
            tb_other_remain_amt2.ResetText();            // 예수금 잔액

            tb_deposit_memo.ResetText();                // 비고 (입금)
            tb_out_memo.ResetText();                      // 비고 (출금)
        }







        //======================================================================================================================
        // 삭제 버튼 Click 동작
        //======================================================================================================================
        private void bt_deleteOtherAccountInfo_Click(object sender, EventArgs e)
        {
            deleteOtherAccountInfo();           // 기타제예금 정보 DELETE
            selectOtherAccountInfo();           // 기타제예금 정보 SELECT
        }

        //------------------------------------------------------------------------------------
        // 삭제버튼 동작 Method
        //------------------------------------------------------------------------------------
        private void deleteOtherAccountInfo()
        {
            string message = "";
            string BASI_DT = dateTimePicker1.Value.ToShortDateString().Trim();
            string query = string.Format("CALL DeleteOtherAccountInfo('{0}')", BASI_DT);
            int result = DbHelper.ExecuteNonQuery(query);
            if(result < 0)
            {
                message = "기타 제예금 정보 삭제 실패";
            }
            else
            {
                message = "기타 제예금 정보 삭제 성공";
            }
            //MessageBox.Show(message);
        }
    }
}