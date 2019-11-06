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
    public partial class UnpaidReportForm : Form
    {
        public UnpaidReportForm()
        {
            InitializeComponent();
        }

        private void UnpaidReportForm_Load(object sender, EventArgs e)
        {
            setItemsToSearchArrangementPlaceCompanyComboBox();      // 수배처 목록 불러오기
            setItemsToSearchProductListComboBox();                              // 상품 목록 불러오기   
            reportViewerLoad();                                                                 // reportViewer Load       

            //-----------------------------------------------------------------------------------------------------------------------------
            // 보고서 첫  Load 화면 출력 본 과정 무시시 첫 화면 경고 Page 출력됨
            //-----------------------------------------------------------------------------------------------------------------------------
            DataRow[] unpaidDataArr = getUnpaidSateData();      // DB 에서 검색조건에 해당하는 Data 가져오기!
            dataRemake(unpaidDataArr);                                            // 보고서에 Data 내용 출력하기!         
        }


        // reportViewer Load    --> 190812 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=========================================================
        public void reportViewerLoad()
        {
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TripERP.Report.UnpaidState.rdlc";      // 보고서와 ReportViewer 연결
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
            pg.Landscape = true;                                                                // 용지 세로,가로 Default 값 설정 (True : 가로 / False : 세로)            
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();
        }
        //=========================================================







        // 검색버튼 눌렀을 경우!!  --> 190813 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=========================================================
        private void searchUnpaidTargetListButton_Click(object sender, EventArgs e)
        {
            DataRow[] unpaidDataArr = getUnpaidSateData();      // DB 에서 검색조건에 해당하는 Data 가져오기!
            dataRemake(unpaidDataArr);                                            // 보고서에 Data 내용 출력하기!                                                  
        }
        //=========================================================







        // DB 에서 미지급현황 Data 가져오기 --> 190812 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=================================================
        public DataRow[] getUnpaidSateData()
        {
            // 검색조건값 가져오기
            //----------------------------------------------------------------------------------------------------------------------------------------
            ComboBoxItem CMPN_SELECTED_ITEM = (ComboBoxItem)searchArrangementPlaceCompanyComboBox.SelectedItem;
            ComboBoxItem PRDT_SELECTED_ITEM = (ComboBoxItem)searchProductListComboBox.SelectedItem;
            string CMPN_NO = CMPN_SELECTED_ITEM.Value.ToString().Trim();
            string PRDT_CNMB = PRDT_SELECTED_ITEM.Value.ToString().Trim();
            string TRAN_FROM_DT = searchStartDatePicker.Value.ToString("yyyy-MM-dd");
            string TRAN_TO_DT = searchEndDatePicker.Value.ToString("yyyy-MM-dd");
            //----------------------------------------------------------------------------------------------------------------------------------------

            string SelectAllForUnpaidReport = string.Format("CALL SelectAllForUnpaidReport('{0}','{1}','{2}','{3}')", CMPN_NO, PRDT_CNMB, TRAN_FROM_DT, TRAN_TO_DT);
            DataSet unpaidDatas = DbHelper.SelectQuery(SelectAllForUnpaidReport);
            int dataCount = unpaidDatas.Tables[0].Rows.Count;
            DataRow[] unpaidDataArr = new DataRow[dataCount];

            for (int i = 0; i < dataCount; i++)
            {
                unpaidDataArr[i] = unpaidDatas.Tables[0].Rows[i];
            }

            return unpaidDataArr;
        }
        //=================================================




        // 가져온 미지급내역 Data 사용 가능하게 가공 및 보고서에 출력   --> 190812 박현호
        // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //=================================================
        public void dataRemake(DataRow[] unpaidDataArr)
        {
            DataRow[] datas = unpaidDataArr;
            int seq = datas.Length;
            int count = 1;
            int SUM_STEM_UNPA_WON_AMT = 0;            
            int SUM_STMT_UNPA_WON_AMT = 0;            
            int SUM_ERDF_PRLS_AMT = 0;


            DataTable dt = new DataTable();
            dt.Columns.Add("No");
            dt.Columns.Add("예약번호");
            dt.Columns.Add("고객명");
            dt.Columns.Add("상품명");
            dt.Columns.Add("수배처");              
            dt.Columns.Add("가정산기준환율");
            dt.Columns.Add("가정산미지급원화금액");
            dt.Columns.Add("가정산미지급외화금액");
            dt.Columns.Add("정산환율");
            dt.Columns.Add("정산원화금액");
            dt.Columns.Add("예상환차손익금액");
            dt.Columns.Add("계정과목");
            dt.Columns.Add("거래일자");

            for (int i = 0; i < unpaidDataArr.Length; i++)
            {
                DataRow unpaidData = unpaidDataArr[i];
                string RSVT_NO = unpaidData["RSVT_NO"].ToString().Trim();                                                                                // 예약번호                
                string CUST_NM = unpaidData["CUST_NM"].ToString().Trim();                                                                               // 고객명(성명)     
                string PRDT_NM = unpaidData["PRDT_NM"].ToString().Trim();                                                                               // 상품명
                string CMPN_NM = unpaidData["CMPN_NM"].ToString().Trim();                                                                             // 수배처명  
                string STEM_BASI_EXRT = unpaidData["STEM_BASI_EXRT"].ToString().Trim();                                                     // 가정산환율
                string STEM_UNPA_WON_AMT = unpaidData["STEM_UNPA_WON_AMT"].ToString().Trim();                                    // 가정산미지급원화금액
                string STEM_UNPA_FRCR_AMT = unpaidData["STEM_UNPA_FRCR_AMT"].ToString().Trim();                                 // 가정산미지급외화금액
                string STMT_BASI_EXRT = unpaidData["STMT_BASI_EXRT"].ToString().Trim();                                                   // 정산환율
                string STMT_UNPA_WON_AMT = unpaidData["STMT_UNPA_WON_AMT"].ToString().Trim();                                   // 정산원화금액     
                string ERDF_PRLS_AMT = unpaidData["ERDF_PRLS_AMT"].ToString().Trim();                                                      // 예상환차손익금액
                string ACNT_TIT_NM = unpaidData["ACNT_TIT_NM"].ToString().Trim();                                                             // 계정과목       
                string TRAN_DT = unpaidData["TRAN_DT"].ToString().Trim().Substring(0, 10);                                                                           // 거래일자
                string CUR_SYBL = unpaidData["CUR_SYBL"].ToString().Trim();                                                                       // 통화 코드

                // 소계표현을 위한 금액 누적
                //-------------------------------------------------------------------------------------------------------------------
                SUM_STEM_UNPA_WON_AMT += int.Parse(STEM_UNPA_WON_AMT);                    // 가정산미지급원화금액 소계                
                SUM_STMT_UNPA_WON_AMT += int.Parse(STMT_UNPA_WON_AMT);              // 정산원화금액 소계
                SUM_ERDF_PRLS_AMT += int.Parse(ERDF_PRLS_AMT);                                   // 예상환차손익금 소계                       
                //-------------------------------------------------------------------------------------------------------------------

                dt.Rows.Add(new object[] {
                    count++.ToString(),
                    RSVT_NO,
                    CUST_NM,
                    PRDT_NM,
                    CMPN_NM,
                    STEM_BASI_EXRT,
                    "￦"+Utils.SetComma(STEM_UNPA_WON_AMT),
                    CUR_SYBL+Utils.SetComma(STEM_UNPA_FRCR_AMT),
                    STMT_BASI_EXRT,
                    "￦"+Utils.SetComma(STMT_UNPA_WON_AMT),                    
                    "￦"+Utils.SetComma(ERDF_PRLS_AMT),
                    ACNT_TIT_NM,
                    TRAN_DT
                });
            }

            // 소계 Data DataTable 에 추가 (ReportViewer 매개변수 활용)
            //------------------------------------------------------------------------------------------------------------------------------------------------------
            var reportParameter = new ReportParameter[5];
            reportParameter[0] = new ReportParameter("SUM_STMT_UNPA_WON_AMT", "￦" + Utils.SetComma(SUM_STMT_UNPA_WON_AMT));
            reportParameter[1] = new ReportParameter("SUM_STEM_UNPA_WON_AMT", "￦" + Utils.SetComma(SUM_STEM_UNPA_WON_AMT));                          
            reportParameter[2] = new ReportParameter("PRINT_DATE", "*출력일자 : " + DateTime.Now.ToString().Trim());
            reportParameter[3] = new ReportParameter("PrintDoID", "*보고자 : " + setPrintDateAndPrintDoID());
            reportParameter[4] = new ReportParameter("SUM_ERDF_PRLS_AMT", "￦" + Utils.SetComma(SUM_ERDF_PRLS_AMT));

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
            string getUserName = "SELECT EMPL_NM FROM TB_EMPL_M WHERE EMPL_NO = (" +
                "SELECT EMPL_NO FROM TB_ACNT_INFO_M WHERE ACNT_ID='" + ACNT_ID + "')";
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



        // 수배처 전체 불러오기  --> 20190816 - 박현호
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




        // 상품 전체 불러오기 --> 20190816 - 박현호
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






        // 닫기 Button 눌렀을 경우 동작  --> 190816 박현호
        //=====================================================
        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //=====================================================





        // Ctrl + MouseWheel 동작시 보고서 배율 확대      --> 190816 박현호
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
    }
}
