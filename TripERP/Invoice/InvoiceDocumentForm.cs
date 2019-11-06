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

namespace TripERP.Invoice
{
    public partial class InvoiceDocumentForm : Form
    {
        public InvoiceDocumentForm()
        {
            InitializeComponent();
        }

        // Form Load 시 수행 --> 190822 박현호
        //=================================================================================================================
        private void InvoiceDocumentForm_Load(object sender, EventArgs e)
        {
            setItemsToSearchArrangementPlaceCompanyComboBox();
            setItemsToSearchProductListComboBox();
            InsertItemForAccount();

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TripERP.Invoice.InvoiceDocumentForm.rdlc";        // 보고서와 ReportViewer 연결
            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;                             // Report Mode 설정 (현재 Local)
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);                          // reportViewer 의 Layout 설정 (현재 인쇄Layoout)
            this.reportViewer1.Size = new Size(this.groupBox2.Width - 20, this.groupBox2.Height - 50);                          // reportViewer Size 조절 (현재 WindowForm 크기에 맞게)       
            this.reportViewer1.Location = new Point(this.groupBox2.Location.X + 10, this.groupBox2.Location.Y + 40);
            this.reportViewer1.MouseWheel += new MouseEventHandler(zoomInOutMouseWheel);                                    // ReportViewer Zoom InOUt 동작 단축키 관련 EventHandler (MouseWheel 동작 관련)
            this.reportViewer1.ShowZoomControl = true;
            this.reportViewer1.ZoomPercent = 90;

            PageSettings pg = new PageSettings();        
            pg.Margins.Top = 40;
            pg.Margins.Left = 40;
            pg.Margins.Right = 40;
            pg.Margins.Bottom = 40;              
            pg.Landscape = true;                                                                // 용지 세로,가로 Default 값 설정 (True : 가로 / False : 세로)           


            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();
            selectInvoiceInfo("", "", "", "", "");
        }
        //=================================================================================================================




        // 닫기 버튼    --> 190823 박현호
        //=================================================================================================================
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //=================================================================================================================




        // 모객업체 전체 불러오기  --> 20190822 - 박현호 (작업진행)
        //=================================================================================================================
        public void setItemsToSearchArrangementPlaceCompanyComboBox()
        {
            searchArrangementPlaceCompanyComboBox.Items.Clear();
            searchArrangementPlaceCompanyComboBox.Text = "";
            searchArrangementPlaceCompanyComboBox.Items.Add(new ComboBoxItem("전체", " "));

            // 수배처
            string CNSM_FILE_APLY_YN = "11";
            string CNSM_FILE_APLY_YN2 = "01";

            string query = string.Format("CALL SelectCoopCmpnList ('{0}','{1}')", CNSM_FILE_APLY_YN, CNSM_FILE_APLY_YN2);
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;
            for (int i = 0; i < dataRowList.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                searchArrangementPlaceCompanyComboBox.Items.Add(new ComboBoxItem(CMPN_NM, CMPN_NO));
            }
            searchArrangementPlaceCompanyComboBox.SelectedIndex = 0;
        }
        //=================================================================================================================





        // 상품 전체 불러오기 --> 20190822 - 박현호 (작업진행)
        //=================================================================================================================
        public void setItemsToSearchProductListComboBox()
        {
            searchProductListComboBox.Items.Clear();
            searchProductListComboBox.Text = "";
            searchProductListComboBox.Items.Add(new ComboBoxItem("전체", " "));

            string query = string.Format("CALL SelectPrdtList");
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            for (int i = 0; i < dataRowList.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();
                searchProductListComboBox.Items.Add(new ComboBoxItem(PRDT_NM, PRDT_CNMB));
            }
            searchProductListComboBox.SelectedIndex = 0;
        }
        //=================================================================================================================




        // Invoice 생성 정보 Select     --> 190826 박현호
        //=================================================================================================================
        public void selectInvoiceInfo()
        {
            var reportParameters = new ReportParameter[12];

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 본사 정보 가져오기 (Invoice 머리글 정보)
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            string COOP_CMPN_DVSN_CD = "01";
            string ACNT_ID = Global.loginInfo.ACNT_ID.Trim();
            string selectInvoiceHeadInfo = string.Format("CALL SelectInvoiceHeadInfoForDocument('{0}', '{1}')", COOP_CMPN_DVSN_CD, ACNT_ID);
            DataSet ds = DbHelper.SelectQuery(selectInvoiceHeadInfo);
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                MessageBox.Show("Invoice 머리글 정보 가져오기 실패!\n관리자에게 문의하세요!");
            }
            else
            {
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    reportParameters[0] = new ReportParameter("CMPN_NM", row[0].ToString().Trim());
                    reportParameters[1] = new ReportParameter("CMNY_ADDR1", row[1].ToString().Trim());
                    reportParameters[2] = new ReportParameter("CMNY_ADDR2", row[2].ToString().Trim());
                    reportParameters[3] = new ReportParameter("CMNY_POST_NO", row[3].ToString().Trim());
                    reportParameters[4] = new ReportParameter("RPRS_OFFC_PHNE_NO", row[4].ToString().Trim());
                    reportParameters[5] = new ReportParameter("RPRS_FAX_NO", row[5].ToString().Trim());
                    reportParameters[6] = new ReportParameter("PSTN_NM", row[6].ToString().Trim());
                    reportParameters[7] = new ReportParameter("EMPL_NM", row[7].ToString().Trim());
                    reportParameters[8] = new ReportParameter("EMAL_ADDR", row[8].ToString().Trim());
                    reportParameters[9] = new ReportParameter("INDU_RGST_NO", row[9].ToString().Trim());
                    reportParameters[10] = new ReportParameter("RPRS_NM", row[10].ToString().Trim());
                    reportParameters[11] = new ReportParameter("DATE", DateTime.Now.ToString());
                }
            }
      
            reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        //=================================================================================================================





        // Ctrl + MouseWheel 동작시 보고서 배율 확대      --> 190822 박현호
        //=====================================================
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
        //=====================================================







        // 입력란 item setting         --> 박현호
        //=====================================================
        public void InsertItemForAccount()
        {            
            cb_account_won.Items.Add(new ComboBoxItem("--- 선택 ----", 0));
            cb_account_foreign.Items.Add(new ComboBoxItem("--- 선택 ----", 0));

            string query = string.Format("CALL SelectAllFinancialOrganizations()");
            DataSet ds = DbHelper.SelectQuery(query);
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                MessageBox.Show("등록된 은행정보가 존재하지 않습니다.");
                return;
            }
            for(int i=0; i<rowCount; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string FNCL_ORGN_CD = row["FNCL_ORGN_CD"].ToString().Trim();
                string FNCL_ORGN_NM = row["FNCL_ORGN_NM"].ToString().Trim();
                string FNCL_ORGN_DVSN_CD = row["FNCL_ORGN_DVSN_CD"].ToString().Trim();

                cb_account_won.Items.Add(new ComboBoxItem(FNCL_ORGN_NM, FNCL_ORGN_CD));
                cb_account_foreign.Items.Add(new ComboBoxItem(FNCL_ORGN_NM, FNCL_ORGN_CD));
            }

            cb_account_won.SelectedIndex = 0;
            cb_account_foreign.SelectedIndex = 0;
        }
        //=====================================================





        // 초기화 버튼       --> 190826 박현호
        //=====================================================
        private void button6_Click(object sender, EventArgs e)
        {
            clearBox();
            applyData();
        }

        public void clearBox()
        {
            cb_account_won.SelectedIndex = 0;
            cb_account_foreign.SelectedIndex = 0;
            tb_won_account.ResetText();
            tb_foreign_account.ResetText();
            tb_year.ResetText();
            tb_month.ResetText();
            tb_date.ResetText();
        }
        //=====================================================





       






        // >>>>>>>>>> Overloading <<<<<<<<<<<<
        // Invoice 생성 정보 Select, 적용버튼 눌러서 Parameter 전달시     --> 190826 박현호
        //=================================================================================================================
        public void selectInvoiceInfo(string bank_won, string account_won, string bank_foreign, string account_foreign, string pay_end_date)
        {
            var reportParameters = new ReportParameter[21];

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 본사 정보 가져오기 (Invoice 머리글 정보)
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            string COOP_CMPN_DVSN_CD = "01";                                // 지브리지 업체 구분 코드 (01)
            string ACNT_ID = Global.loginInfo.ACNT_ID.Trim();
            string selectInvoiceHeadInfo = string.Format("CALL SelectInvoiceHeadInfoForDocument('{0}', '{1}')", COOP_CMPN_DVSN_CD, ACNT_ID);
            DataSet ds = DbHelper.SelectQuery(selectInvoiceHeadInfo);
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                MessageBox.Show("Invoice 머리글 정보 가져오기 실패!\n관리자에게 문의하세요!");
            }
            else
            {
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    reportParameters[0] = new ReportParameter("CMPN_NM", row[0].ToString().Trim());
                    reportParameters[1] = new ReportParameter("CMNY_ADDR1", row[1].ToString().Trim());
                    reportParameters[2] = new ReportParameter("CMNY_ADDR2", row[2].ToString().Trim());
                    reportParameters[3] = new ReportParameter("CMNY_POST_NO", row[3].ToString().Trim());
                    reportParameters[4] = new ReportParameter("RPRS_OFFC_PHNE_NO", row[4].ToString().Trim());
                    reportParameters[5] = new ReportParameter("RPRS_FAX_NO", row[5].ToString().Trim());
                    reportParameters[6] = new ReportParameter("PSTN_NM", row[6].ToString().Trim());
                    reportParameters[7] = new ReportParameter("EMPL_NM", row[7].ToString().Trim());
                    reportParameters[8] = new ReportParameter("EMAL_ADDR", row[8].ToString().Trim());
                    reportParameters[9] = new ReportParameter("INDU_RGST_NO", row[9].ToString().Trim());
                    reportParameters[10] = new ReportParameter("RPRS_NM", row[10].ToString().Trim());
                    reportParameters[11] = new ReportParameter("DATE", DateTime.Now.ToString());

                    //------------------------------------------------------------------------------------------------------------
                    // 원화계좌은행 Default
                    //------------------------------------------------------------------------------------------------------------
                    if (bank_won == "" || bank_won.Equals("--- 선택 ----"))
                    {
                        reportParameters[12] = new ReportParameter("BANK_WON", "국민은행");
                    }
                    else
                    {
                        reportParameters[12] = new ReportParameter("BANK_WON", bank_won);
                    }

                    //------------------------------------------------------------------------------------------------------------
                    // 원화 계좌 Default
                    //------------------------------------------------------------------------------------------------------------
                    if (account_won == "")
                    {
                        reportParameters[13] = new ReportParameter("ACCOUNT_WON", "763601-04-154658");
                    }
                    else
                    {
                        if (account_won.Contains('/') || account_won.Contains('*') || account_won.Contains('+') || account_foreign.Contains(')') || account_won.Contains('(') || account_won.Contains('&') || account_won.Contains('^') || account_won.Contains('%') || account_won.Contains('$') || account_won.Contains('#') || account_won.Contains('@') || account_won.Contains('!') || account_won.Contains('~') || account_won.Contains("'") || account_won.Contains("\"") || account_won.Contains("_") || account_won.Contains("="))
                        {
                            MessageBox.Show("- 이외의 특수문자는 입력 할 수 없습니다.", "경고");
                            return;
                        }
                        reportParameters[13] = new ReportParameter("ACCOUNT_WON", account_won);
                    }

                    //------------------------------------------------------------------------------------------------------------
                    // 외화계좌 은행 Default
                    //------------------------------------------------------------------------------------------------------------
                    if (bank_foreign == "" || bank_foreign.Equals("--- 선택 ----"))
                    {
                        reportParameters[14] = new ReportParameter("BANK_FOREIN", "국민은행");
                    }
                    else
                    {
                        reportParameters[14] = new ReportParameter("BANK_FOREIN", bank_foreign);
                    }

                    //------------------------------------------------------------------------------------------------------------
                    // 외화계좌 Default
                    //------------------------------------------------------------------------------------------------------------
                    if (account_foreign == "")
                    {
                        reportParameters[15] = new ReportParameter("ACCOUNT_FOREIN", "763668-11-003753");
                    }
                    else
                    {
                        if (account_foreign.Contains('/') || account_foreign.Contains('*') || account_foreign.Contains('+') || account_foreign.Contains(')') || account_foreign.Contains('(') || account_foreign.Contains('&') || account_foreign.Contains('^') || account_foreign.Contains('%') || account_foreign.Contains('$') || account_foreign.Contains('#') || account_foreign.Contains('@') || account_foreign.Contains('!') || account_foreign.Contains('~') || account_foreign.Contains("'") || account_foreign.Contains("\"") || account_foreign.Contains("_") || account_foreign.Contains("="))
                        {
                            MessageBox.Show("- 이외의 특수문자는 입력 할 수 없습니다.", "경고");
                            return;
                        }
                        reportParameters[15] = new ReportParameter("ACCOUNT_FOREIN", account_foreign);
                    }

                    //------------------------------------------------------------------------------------------------------------
                    // 납기일 Default
                    //------------------------------------------------------------------------------------------------------------
                    if (pay_end_date == "" || pay_end_date.Equals("--"))
                    {
                        reportParameters[16] = new ReportParameter("PAY_END_DATE", DateTime.Now.ToString().Substring(0,10));
                    }
                    else
                    {
                        try
                        {
                            if (DateTime.Parse(pay_end_date) <= DateTime.Now)
                            {
                                MessageBox.Show("납기일은 금일보다 이후 값을 입력해야합니다.", "경고");
                                return;
                            }
                            reportParameters[16] = new ReportParameter("PAY_END_DATE", pay_end_date);
                        }catch(Exception e)
                        {
                            MessageBox.Show("적용할 Data 값이 유효하지 않습니다.", "경고");
                            return;
                        }
                    }
                    
                }
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Inovice 청구 내역 정보 가져오기 (Invoice 본문 정보)
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            searchInvoiceData(reportParameters);
        }
        //=================================================================================================================






        // Enterkey 눌렀을때 Parameter 확인해서 적용하기
        //=================================================================================================================
        private void enterKeyPressed(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                applyData();
            }
        }
        //=================================================================================================================





        //=====================================================================================================================
        // 적용버튼 --> 190826 박현호
        //=====================================================================================================================
        private void button7_Click(object sender, EventArgs e)
        {
            applyData();
        }

        //-----------------------------------------------------------------------------------------
        // 입력 Parameter 변수화 및 적용
        //-----------------------------------------------------------------------------------------
        public void applyData()
        {
            string bank_won = cb_account_won.SelectedItem.ToString().Trim();
            string bank_foreign = cb_account_foreign.SelectedItem.ToString().Trim();
            string account_won = tb_won_account.Text.Trim();
            string account_foreign = tb_foreign_account.Text.Trim();
            string year = tb_year.Text.Trim();
            string month = tb_month.Text.Trim();
            string date = tb_date.Text.Trim();
            string pay_end_date = year + "-" + month + "-" + date;
            selectInvoiceInfo(bank_won, account_won, bank_foreign, account_foreign, pay_end_date);
        }




        // 검색버튼 눌렀을때 동작!!
        //=================================================================================================================
        private void searchSettlementTargetListButton_Click(object sender, EventArgs e)
        {
            ReportParameter[] reportParameters = new ReportParameter[4];
            searchInvoiceData(reportParameters);
        }

        //--------------------------------------
        // 검색 동작
        //--------------------------------------
        public void searchInvoiceData(ReportParameter[] reportParameters)
        {
            int reportParameterStartIndex = 0;

            for(int i=0; i<reportParameters.Length; i++)
            {
                if (reportParameters[i] == null)
                {
                    reportParameterStartIndex = i;                    
                    break;
                }
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Inovice 청구 내역 정보 가져오기 (Invoice 본문 정보)
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 1) DataTable 준비
            DataTable dt = new DataTable();
            dt.Columns.Add("날짜");
            dt.Columns.Add("예약자명");
            dt.Columns.Add("내용");
            dt.Columns.Add("구분");
            dt.Columns.Add("단가");
            dt.Columns.Add("인원");
            dt.Columns.Add("판매가");
            dt.Columns.Add("합계");
            dt.Columns.Add("비고");
            dt.Columns.Add("기준환율");
            dt.Columns.Add("예약번호");

            // 2) DataTable 에 담을 Data 조회 및 Table 채우기
            ComboBoxItem companyItem = (ComboBoxItem)searchArrangementPlaceCompanyComboBox.SelectedItem;
            ComboBoxItem productItem = (ComboBoxItem)searchProductListComboBox.SelectedItem;
            string TKTR_CMPN_NO = companyItem.Value.ToString().Trim();
            string PRDT_CNMB = productItem.Value.ToString().Trim();
            string DPTR_FROM_DT = searchStartDatePicker.Text.Trim();
            string DPTR_TO_DT = searchEndDatePicker.Text.Trim();

            string selectInvoiceBodyInfo = string.Format("CALL SelectInvoiceBodyInfoForDocument('{0}','{1}','{2}','{3}')", TKTR_CMPN_NO, PRDT_CNMB, DPTR_FROM_DT, DPTR_TO_DT);

            DataSet ds = DbHelper.SelectQuery(selectInvoiceBodyInfo);
            int rowCount = ds.Tables[0].Rows.Count;
            double SUM_AMT_WON = 0;
            double SUM_AMT_FREN = 0;
            string FREN_CUR_SYBL = "";
            string PREV_CUST_NM = "";
            int count = 0;
            double FREN_EXRT = 0.0;
            List<string> SALE_AMT_LIST = new List<string>();

            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string date = row[0].ToString().Trim();                         // 날짜
                string CUST_NM = row[1].ToString().Trim();                  // 예약자명
                if (PREV_CUST_NM.Equals(""))
                {
                    PREV_CUST_NM = CUST_NM;
                }
                string PRDT_NM = row[2].ToString().Trim();                  // 상품명
                string ADLT_DVSN_CD = row[6].ToString().Trim();        // 성인 구분 코드
                string SALE_PRCE_NM = "";                                          //판매가격명 (DS 의 구분에 들어갈것)     
                if (ADLT_DVSN_CD.Equals("1"))
                {
                    SALE_PRCE_NM = "성인";
                }
                else if (ADLT_DVSN_CD.Equals("2"))
                {
                    SALE_PRCE_NM = "소아";
                }
                else
                {
                    SALE_PRCE_NM = "유아";
                }
                string SALE_UTPR = row[7].ToString().Trim();                                     // 단가
                string NMPS_NBR = row[8].ToString().Trim();                                    // 인원
                string SALE_AMT = row[9].ToString().Trim();                                    // 판매금액            
                string CUR_CD = row[10].ToString().Trim();                                      // 통화코드
                string CUR_SYBL = row[11].ToString().Trim();                                        // 통화 기호
                string BASI_EXRT = "";                                                                      // 기준환율
                string RSVT_NO = row[13].ToString().Trim();                                     // 예약번호

                //-----------------------------------------------------------
                // 판매 총액 집계! (원화, 외화)
                //-----------------------------------------------------------
                if (CUR_CD.Equals("KRW"))
                {
                    SUM_AMT_WON += double.Parse(SALE_AMT);
                    SALE_UTPR = Utils.SetComma(double.Parse(SALE_UTPR));                    
                }
                else
                {
                    BASI_EXRT = row[4].ToString().Trim();
                    FREN_EXRT = double.Parse(BASI_EXRT);                    
                    SUM_AMT_FREN += double.Parse(SALE_AMT);
                    SALE_UTPR = Utils.SetComma(double.Parse(SALE_UTPR));                    
                    FREN_CUR_SYBL = CUR_SYBL;
                }



                //------------------------------------------------------------------------------
                // 예약자 별 구매건 처리
                //------------------------------------------------------------------------------
                if (PREV_CUST_NM.Equals(CUST_NM))
                {                    
                    count++;
                    //************************************************************
                    // 이전 Recode 와 같은 CUST_NM 을 갖는 Recode 들의 인원 정보를 출력
                    //      - 증가시킨 count 와 for 문의 지역변수 i 가 0이 아닐경우 아래 동작 수행
                    //          -> 이 조건이 이전 Recode 와 같은 CUST_NM 을 갖는 Recode 의 조건 
                    //************************************************************
                    if (count > 0 && i!=0)
                    {
                        //통화코드가 한화가 아닐경우 SUM_AMT 의 집계는 분리되서 이루어져야함
                        if (CUR_SYBL.Equals(FREN_CUR_SYBL))
                        {

                        }
                        
                        SALE_AMT_LIST.Add(SALE_AMT);
                        string SUM_SALE_AMT_STR = "";

                        //*********************************************************
                        // 마지막 Recode 가 이전 Recode 와 같은 CUST_NM 을 갖고 있다면...
                        //*********************************************************
                        if (i == rowCount - 1)
                        {                            
                            double SUM_SALE_AMT = 0.0;
                            for (int k = 0; k < SALE_AMT_LIST.Count; k++)
                            {
                                SUM_SALE_AMT += double.Parse(SALE_AMT_LIST[k]);
                            }
                            SUM_SALE_AMT_STR = CUR_SYBL+Utils.SetComma(SUM_SALE_AMT);
                        }

                        dt.Rows.Add(new object[]{
                               "",
                               "",
                               PRDT_NM,
                               SALE_PRCE_NM,
                               CUR_SYBL+SALE_UTPR,
                               NMPS_NBR,
                               CUR_SYBL+Utils.SetComma(double.Parse(SALE_AMT)),
                               SUM_SALE_AMT_STR,
                               null,
                               BASI_EXRT,
                               RSVT_NO
                             });
                        
                    }                   
                    else
                    {
                        //*****************************************
                        // 조회한 DataSet 에서 첫 Recode 의 인원 정보를 출력
                        //*****************************************
                        SALE_AMT_LIST.Add(SALE_AMT);                                        // 현재 판매가 정보를 List 에 넣어놓음
                            dt.Rows.Add(new object[]{                                                   // DataTable 에 Data 를 채워넣어줌
                               date.Substring(0,10),                                                        // 출발일자
                               CUST_NM,                                                                         // 예약자명
                               PRDT_NM,                                                                         // 상품명
                               SALE_PRCE_NM,                                                                // 성인,소아,유아 구분
                               CUR_SYBL+SALE_UTPR,                                                      // 판매 단가
                               NMPS_NBR,                                                                        // 인원수
                               CUR_SYBL+Utils.SetComma(double.Parse(SALE_AMT)),     // 현재 판매가 정보를 ',' 처리해서 넣엉줌
                               "",                                                                                    // 합계정보는 다음데이터가 이전데이터의 예약자명과 다를때 주입해줌
                               null,                                                                                // 비고
                               BASI_EXRT,                                                                       // 기준환율
                               RSVT_NO                                                                            // 예약번호   (동명이인 있을시 예약번호로 구분)
                             });
                     }
                }
                else
                {
                    //**************************************************************
                    // 이전 Data 와 다른 CUST_NM 을 갖는 Recode 가 나왔을경우 동작
                    //**************************************************************
                    double SUM_SALE_AMT = 0.0;                                                                              // 담아놓은 판매가의 합산 Data 를 누적하기 위한 변수
                    for(int a=0; a< SALE_AMT_LIST.Count; a++)                                                       // 판매가 정보를 담아 놓은 List 길이만큼 반복
                    {                   
                        SUM_SALE_AMT += double.Parse(SALE_AMT_LIST[a]);                                    // 들어있는 판매가 정보를 누적                        
                    }
                    DataRow prevRow = dt.Rows[i - 1];                                                                                                   // 이전 DataRow 를 불러온다
                    prevRow[7] = prevRow[6].ToString().Substring(0, 1) + Utils.SetComma(SUM_SALE_AMT);                  // 이전 DataRow 의 합계 Column 에 판매가 합산 Data 주입
                    SALE_AMT_LIST.Clear();                                                                                                                  // 다음 판매가 합계정보를 집계하기 위해 List 를 비움
                    count = 0;                                                                                                                                      // Count 초기화
                    string SUM_AMT = "";
                    if(i== rowCount - 1)
                    {
                        SUM_AMT = CUR_SYBL+Utils.SetComma(double.Parse(SALE_AMT));
                    }
                    dt.Rows.Add(new object[]{
                       date.Substring(0,10),
                       CUST_NM,
                       PRDT_NM,
                       SALE_PRCE_NM,
                       CUR_SYBL+SALE_UTPR,
                       NMPS_NBR,
                       CUR_SYBL+Utils.SetComma(double.Parse(SALE_AMT)),
                       SUM_AMT,
                       null,
                       BASI_EXRT,
                       RSVT_NO
                     });
                    PREV_CUST_NM = CUST_NM;
                    SALE_AMT_LIST.Add(SALE_AMT);
                }              
            }
        
            reportParameters[reportParameterStartIndex] = new ReportParameter("SUM_AMT_WON", "￦"+Utils.SetComma(SUM_AMT_WON));                                                  // 원화총액
            reportParameters[reportParameterStartIndex+1] = new ReportParameter("SUM_AMT_FREN", FREN_CUR_SYBL + Utils.SetComma(SUM_AMT_FREN));                      // 외화 총액
            reportParameters[reportParameterStartIndex+2] = new ReportParameter("CUR_SYBL", FREN_CUR_SYBL.ToString().Trim());                                                           // 통화기호
            reportParameters[reportParameterStartIndex+3] = new ReportParameter("EXRT_CHANGE", "￦" + Utils.SetComma(FREN_EXRT * SUM_AMT_FREN));    // 외화환전금액

            ReportDataSource rDS = new ReportDataSource("DataSet1", dt);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rDS);
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();

        }
        //=================================================================================================================
    }
}
