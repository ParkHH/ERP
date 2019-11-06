using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;

namespace TripERP.Report
{
    public partial class PaymentReportForm : Form
    {
        public PaymentReportForm()
        {
            InitializeComponent();
        }

        private void PaymentReportForm_Load(object sender, EventArgs e)
        {
            setItemsToSearchArrangementPlaceCompanyComboBox();      // 수배처 목록 불러오기
            setItemsToSearchProductListComboBox();                              // 상품 목록 불러오기
            reportViewerLoad();                                                                 // reportViewer Load            
        }


        // reportViewer Load    --> 190812 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=========================================================
        public void reportViewerLoad()
        {
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TripERP.Report.SettlementState.rdlc";      // 보고서와 ReportViewer 연결
            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;                  // Report Mode 설정 (현재 Local)
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);                // reportViewer 의 Layout 설정 (현재 인쇄Layoout)
            this.reportViewer1.Size = new Size(this.groupBox2.Width - 20, this.groupBox2.Height - 50);                 // reportViewer Size 조절 (현재 WindowForm 크기에 맞게)       
            this.reportViewer1.Location = new Point(this.groupBox2.Location.X + 10, this.groupBox2.Location.Y + 40);

            PageSettings pg = new PageSettings();
            pg.Margins.Top = 10;
            pg.Margins.Left = 10;
            pg.Margins.Right = 10;
            pg.Margins.Bottom = 10;
            pg.Landscape = true;                                                                // 용지 세로,가로 Default 값 설정 (True : 가로 / False : 세로)            
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();

            //DataRow[] settlementDataArr = getSettlementSateData();      // DB 에서 Data 가져오는 Method
            //dataRemake(settlementDataArr);                                       // 가져온 Data ReportViewer Table 에 출력하는 Method
        }
        //=========================================================







        // 검색버튼 눌렀을 경우!!  --> 190813 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=========================================================
        private void searchPaymentTargetListButton_Click(object sender, EventArgs e)
        {
            DataRow[] settlementDataArr = getSettlementSateData();      // DB 에서 검색조건에 해당하는 Data 가져오기!
            dataRemake(settlementDataArr);                                            // 보고서에 Data 내용 출력하기!                    
        }
        //=========================================================







        // DB 에서 정산현황 Data 가져오기 --> 190812 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=================================================
        public DataRow[] getSettlementSateData()
        {
            // 검색조건값 가져오기
            //----------------------------------------------------------------------------------------------------------------------------------------
            ComboBoxItem CMPN_SELECTED_ITEM = (ComboBoxItem)searchArrangementPlaceCompanyComboBox.SelectedItem;
            ComboBoxItem PRDT_SELECTED_ITEM = (ComboBoxItem)searchProductListComboBox.SelectedItem;
            string CMPN_NO = CMPN_SELECTED_ITEM.Value.ToString().Trim();
            string PRDT_CNMB = PRDT_SELECTED_ITEM.Value.ToString().Trim();
            string STMT_FROM_DT = searchStartDatePicker.Value.ToString("yyyy-MM-dd");
            string STMT_TO_DT = searchEndDatePicker.Value.ToString("yyyy-MM-dd");
            //----------------------------------------------------------------------------------------------------------------------------------------

            string SelectAllForSettlementReport = string.Format("CALL SelectAllForSettlementReport('{0}','{1}','{2}','{3}')", CMPN_NO, PRDT_CNMB, STMT_FROM_DT, STMT_TO_DT);
            DataSet settlementDatas = DbHelper.SelectQuery(SelectAllForSettlementReport);
            int dataCount = settlementDatas.Tables[0].Rows.Count;
            DataRow[] settlementDataArr = new DataRow[dataCount];

            for (int i = 0; i < dataCount; i++)
            {
                settlementDataArr[i] = settlementDatas.Tables[0].Rows[i];
            }

            return settlementDataArr;
        }
        //=================================================




        // 가져온 정산내역 Data 사용 가능하게 가공 및 보고서에 출력   --> 190812 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=================================================
        public void dataRemake(DataRow[] settlementDataArr)
        {
            DataRow[] datas = settlementDataArr;
            int seq = datas.Length;
            int count = 1;
            int SUM_TOT_SALE_WON_AMT = 0;
            //int SUM_TOT_SALE_FOREIGN_AMT = 0;
            int SUM_STEM_UNPA_WON_AMT = 0;
            //int SUM_STEM_UNPA_FRCR_AMT = 0;
            int SUM_WON_PYMT_FEE = 0;
            int SUM_PROFIT = 0;
            string SUM_PROFIT_RATE = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("No");
            dt.Columns.Add("출발일");
            dt.Columns.Add("성명");
            dt.Columns.Add("인원수량");
            dt.Columns.Add("수배처");
            dt.Columns.Add("상품명");
            dt.Columns.Add("원화판매가");
            dt.Columns.Add("외화판매가");
            dt.Columns.Add("원화현지지급금");
            dt.Columns.Add("외화현지지급금");
            dt.Columns.Add("원화수수료");
            dt.Columns.Add("GB수익금");
            dt.Columns.Add("GB수익률");
            dt.Columns.Add("정산일");

            for (int i = 0; i < settlementDataArr.Length; i++)
            {
                DataRow settlementData = settlementDataArr[i];
                string DPTR_DT = settlementData["DPTR_DT"].ToString().Trim().Substring(0, 10);                                                      // 출발일                
                string CUST_NM = settlementData["CUST_NM"].ToString().Trim();                                                                            // 고객명(성명)
                string PEOPLE_COUNT = settlementData["PEOPLE_COUNT"].ToString().Trim();                                                             // 인원, 수량
                string CMPN_NM = settlementData["CMPN_NM"].ToString().Trim();                                                                               // 모객업체명
                string PRDT_NM = settlementData["PRDT_NM"].ToString().Trim();                                                                                  // 상품명
                string TOT_SALE_WON_AMT = settlementData["TOT_SALE_WON_AMT"].ToString().Trim();                                             // 원화판매가
                string TOT_SALE_FOREIGN_AMT = settlementData["TOT_SALE_FOREIGN_AMT"].ToString().Trim();                                // 외화판매가
                string STEM_UNPA_WON_AMT = settlementData["STEM_UNPA_WON_AMT"].ToString().Trim();                                     // 원화현지미지급금
                string STEM_UNPA_FRCR_AMT = settlementData["STEM_UNPA_FRCR_AMT"].ToString().Trim();                                  // 외화현지미지급금
                string WON_PYMT_FEE = settlementData["WON_PYMT_FEE"].ToString().Trim();                                                      // 수수료
                string PROFIT = settlementData["PROFIT"].ToString().Trim();                                                                              // GB 수익금
                string PROFIT_RATE = settlementData["PROFIT_RATE"].ToString().Trim();                                                             // GB 수익률
                string CUR_SYBL = settlementData["CUR_SYBL"].ToString().Trim();                                                                     // 통화 코드
                string STMT_DT = settlementData["STMT_DTM"].ToString().Trim();                                                                      // 정산일

                // 소계표현을 위한 금액 누적
                //-------------------------------------------------------------------------------------------------------------------
                SUM_TOT_SALE_WON_AMT += int.Parse(TOT_SALE_WON_AMT);                    // 원화판매가
                                                                                        //SUM_TOT_SALE_FOREIGN_AMT += int.Parse(TOT_SALE_FOREIGN_AMT);      // 외화판매가
                SUM_STEM_UNPA_WON_AMT += int.Parse(STEM_UNPA_WON_AMT);              // 원화현지미지급금
                                                                                    //SUM_STEM_UNPA_FRCR_AMT += int.Parse(STEM_UNPA_FRCR_AMT);          // 외화현지미지급금
                SUM_WON_PYMT_FEE += int.Parse(WON_PYMT_FEE);                                    // 수수료
                SUM_PROFIT += int.Parse(PROFIT);                                                               // GB 수익금
                SUM_PROFIT_RATE = (SUM_PROFIT / SUM_TOT_SALE_WON_AMT * 100).ToString().Trim();            // GB 수익률
                                                                                                            //-------------------------------------------------------------------------------------------------------------------

                dt.Rows.Add(new object[] {
                count++.ToString(),
                DPTR_DT,
                CUST_NM,
                PEOPLE_COUNT,
                CMPN_NM,
                PRDT_NM,
                "￦"+Utils.SetComma(TOT_SALE_WON_AMT),
                CUR_SYBL+Utils.SetComma(TOT_SALE_FOREIGN_AMT),
                "￦"+Utils.SetComma(STEM_UNPA_WON_AMT),
                CUR_SYBL+Utils.SetComma(STEM_UNPA_FRCR_AMT),
                "￦"+Utils.SetComma(WON_PYMT_FEE),
                Utils.SetComma(PROFIT),
                PROFIT_RATE+"%",
                STMT_DT
            });
            }

            // 소계 Data DataTable 에 추가 (ReportViewer 매개변수 활용)
            //------------------------------------------------------------------------------------------------------------------------------------------------------
            var reportParameter = new ReportParameter[7];
            reportParameter[0] = new ReportParameter("SUM_TOT_SALE_WON_AMT", "￦" + Utils.SetComma(SUM_TOT_SALE_WON_AMT));
            reportParameter[1] = new ReportParameter("SUM_STEM_UNPA_WON_AMT", "￦" + Utils.SetComma(SUM_STEM_UNPA_WON_AMT));
            reportParameter[2] = new ReportParameter("SUM_WON_PYMT_FEE", "￦" + Utils.SetComma(SUM_WON_PYMT_FEE));
            reportParameter[3] = new ReportParameter("SUM_PROFIT", Utils.SetComma(SUM_PROFIT));
            reportParameter[4] = new ReportParameter("SUM_PROFIT_RATE", SUM_PROFIT_RATE + "%");
            reportParameter[5] = new ReportParameter("PRINT_DATE", "*출력일자 : " + DateTime.Now.ToString().Trim());
            reportParameter[6] = new ReportParameter("PrintDoID", "*보고자 : " + setPrintDateAndPrintDoID());

            reportViewer1.LocalReport.SetParameters(reportParameter);
            //------------------------------------------------------------------------------------------------------------------------------------------------------

            reportViewer1.PageCountMode = PageCountMode.Actual;

            ReportDataSource rDS = new ReportDataSource("DataSet1", dt);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rDS);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        //=================================================



        // 출력 일자 및 출력자 아이디 보고서에 표현하기
        //=================================================
        public string setPrintDateAndPrintDoID()
        {
            string ACNT_ID = Global.loginInfo.ACNT_ID;
            string userName = "";
            string getUserName = "SELECT * FROM TB_ACNT_INFO_M WHERE ACNT_ID=" + ACNT_ID;
            DataSet dataSet = DbHelper.SelectQuery(getUserName);
            DataRowCollection dataCollection = dataSet.Tables[0].Rows;
            if (dataCollection.Count == 0)
            {
                userName = "stranger";
            }
            else
            {
                for (int i = 0; i < dataCollection.Count; i++)
                {
                    DataRow data = dataSet.Tables[0].Rows[i];
                    userName = data["EMPL_NM"].ToString().Trim();
                }
            }

            return userName;
        }
        //=================================================



        // 수배처 전체 불러오기  --> 20190813 - 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=================================================
        public void setItemsToSearchArrangementPlaceCompanyComboBox()
        {
            searchArrangementPlaceCompanyComboBox.Items.Clear();
            searchArrangementPlaceCompanyComboBox.Text = "";
            searchArrangementPlaceCompanyComboBox.Items.Add(new ComboBoxItem("전체", "-"));

            string query = string.Format("CALL SelectCoopCmpnList('{0}', '{1}')", ' ', ' ');
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
        //=================================================




        // 상품 전체 불러오기 --> 20190813 - 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=================================================
        public void setItemsToSearchProductListComboBox()
        {
            searchProductListComboBox.Items.Clear();
            searchProductListComboBox.Text = "";
            searchProductListComboBox.Items.Add(new ComboBoxItem("전체", "-"));

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
        //=================================================






        // 닫기 Button 눌렀을 경우 동작
        //=====================================================
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }      
        //=====================================================
    }
}
