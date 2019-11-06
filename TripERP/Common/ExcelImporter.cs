using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Win32;

namespace TripERP.Common
{
    class ExcelImporter
    {

        public static bool excel_install_stat = false; /// Excel 설치 유무
        public static string excel_not_install = "excel not install".ToUpper();

        internal static DataGridView UploadHistoryDataGridView;
        internal static DataGridViewTextBoxColumn[] excel_file_col;
        public static string[] excel_file_header = { "번호", "처리일자", "모객업체", "상품명", "파일명" };


        internal static DataGridView PurchaseListDataGridView;
        internal static DataGridViewTextBoxColumn[] excel_contents_col;
        public static string[] excel_contents_header = {"번호", "업체번호", "업체명", "상품명", "티켓번호",
                                                        "주문번호", "구매일자", "이름 (주문자)","연락처(주문자)", "이메일(주문자)", "구매금액" };


        // Excel Column 명 Dictionary
        public Dictionary<string, List<string>> excelColumnDictionary = ExcelColumnDictionary.initializingExcelColumnDic();


        public static int rows;
        public static int cols;

        // 파일목록
        internal static List<string> ListT_TB_CNSM_FILE_L = new List<string>();
        // 구매자목록
        internal static List<List<string>> ListT_TB_CNSM_L = new List<List<string>>();

        ///// GRID 생성
        public static void grid_create(Control _Contol1, Control _Contol2)
        {
            if (_Contol1 != null)
            {
                UploadHistoryDataGridView = new DataGridView();
                UploadHistoryDataGridView.Name = "UploadHistoryDataGridView";
                UploadHistoryDataGridView.Font = new Font("맑은 고딕", 12, System.Drawing.FontStyle.Regular);
                UploadHistoryDataGridView.BackgroundColor = Color.Gray;
                UploadHistoryDataGridView.MultiSelect = true;
                UploadHistoryDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                UploadHistoryDataGridView.ScrollBars = ScrollBars.Both;
                UploadHistoryDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                UploadHistoryDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                UploadHistoryDataGridView.GridColor = Color.Black;
                UploadHistoryDataGridView.RowHeadersVisible = false;
                UploadHistoryDataGridView.BackColor = Color.Black;
                UploadHistoryDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
                UploadHistoryDataGridView.MultiSelect = false;
                _Contol1.Controls.Add(UploadHistoryDataGridView);
                UploadHistoryDataGridView.Height = _Contol1.Height - 70;
                UploadHistoryDataGridView.Dock = DockStyle.Bottom;
                UploadHistoryDataGridView.BringToFront();
            }
            //-------------------------------
            if(_Contol2 != null)
            { 
                PurchaseListDataGridView = new DataGridView();
                PurchaseListDataGridView.Name = "PurchaseListDataGridView";
                PurchaseListDataGridView.Font = new Font("맑은 고딕", 12, System.Drawing.FontStyle.Regular);
                PurchaseListDataGridView.BackgroundColor = Color.Gray;
                PurchaseListDataGridView.MultiSelect = true;
                PurchaseListDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                PurchaseListDataGridView.ScrollBars = ScrollBars.Both;
                PurchaseListDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                PurchaseListDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                PurchaseListDataGridView.GridColor = Color.Black;
                PurchaseListDataGridView.RowHeadersVisible = false;
                PurchaseListDataGridView.BackColor = Color.Black;
                PurchaseListDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                PurchaseListDataGridView.MultiSelect = false;
                _Contol2.Controls.Add(PurchaseListDataGridView);
                PurchaseListDataGridView.Height = _Contol2.Height-30;
                PurchaseListDataGridView.Dock = DockStyle.Bottom;
                PurchaseListDataGridView.BringToFront();
            }
            //-------------------------------
        }



        ///// 헤더 문자열을 GRID 헤더로 생성
        public static void grid_create_header()
        {
            if(UploadHistoryDataGridView != null)
            {
                UploadHistoryDataGridView.BorderStyle = BorderStyle.None;
                UploadHistoryDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                UploadHistoryDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                UploadHistoryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                UploadHistoryDataGridView.ColumnHeadersHeight = 25;

                UploadHistoryDataGridView.EnableHeadersVisualStyles = false;
                UploadHistoryDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                UploadHistoryDataGridView.Columns.Clear();
                cols = excel_file_header.Count();
                excel_file_col = new DataGridViewTextBoxColumn[cols + 1];
                for (int i = 0; i < cols; i++)
                {
                    excel_file_col[i] = new DataGridViewTextBoxColumn();
                    UploadHistoryDataGridView.Columns.Add(excel_file_col[i]);
                    UploadHistoryDataGridView.Columns[i].Name = i.ToString();
                    UploadHistoryDataGridView.Columns[i].HeaderText = excel_file_header[i].Trim();


                    switch (i)
                    {
                        case 0:
                            UploadHistoryDataGridView.Columns[i].Width = 50;
                            UploadHistoryDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case 4:
                            UploadHistoryDataGridView.Columns[i].Width = 700;
                            UploadHistoryDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            break;
                        default:
                            UploadHistoryDataGridView.Columns[i].Width = 220;
                            UploadHistoryDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                    }
                    /// COLUMN SIZE 조정---------------------------------
                    UploadHistoryDataGridView.Columns[i].Resizable = DataGridViewTriState.True;
                    UploadHistoryDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    UploadHistoryDataGridView.Columns[i].ReadOnly = true;
                }
                UploadHistoryDataGridView.AllowUserToAddRows = false;
                UploadHistoryDataGridView.Rows.Clear();
            }
            
            //-------------------------------------------------------------
            if(PurchaseListDataGridView != null)
            {
                PurchaseListDataGridView.BorderStyle = BorderStyle.None;
                PurchaseListDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                PurchaseListDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PurchaseListDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                PurchaseListDataGridView.ColumnHeadersHeight = 25;

                PurchaseListDataGridView.EnableHeadersVisualStyles = false;
                PurchaseListDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                PurchaseListDataGridView.Columns.Clear();
                cols = excel_contents_header.Count();
                excel_contents_col = new DataGridViewTextBoxColumn[cols + 1];
                for (int i = 0; i < cols; i++)
                {
                    excel_contents_col[i] = new DataGridViewTextBoxColumn();
                    PurchaseListDataGridView.Columns.Add(excel_contents_col[i]);
                    PurchaseListDataGridView.Columns[i].Width = 120;
                    PurchaseListDataGridView.Columns[i].Name = i.ToString();
                    PurchaseListDataGridView.Columns[i].HeaderText = excel_contents_header[i].Trim();
                    switch (i)
                    {
                        case 0:
                            PurchaseListDataGridView.Columns[i].Width = 50;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case 1:
                            PurchaseListDataGridView.Columns[i].Width = 80;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case 4:
                            PurchaseListDataGridView.Columns[i].Width = 160;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case 6:
                            PurchaseListDataGridView.Columns[i].Width = 180;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case 7:
                            PurchaseListDataGridView.Columns[i].Width = 120;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case 8:
                            PurchaseListDataGridView.Columns[i].Width = 200;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case 9:
                            PurchaseListDataGridView.Columns[i].Width = 170;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            break;
                        case 10:
                            PurchaseListDataGridView.Columns[i].Width = 120;
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        default:
                            PurchaseListDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            break;
                    }
                    /// COLUMN SIZE 조정---------------------------------
                    PurchaseListDataGridView.Columns[i].Resizable = DataGridViewTriState.True;
                    PurchaseListDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    PurchaseListDataGridView.Columns[i].ReadOnly = true;
                }
                PurchaseListDataGridView.AllowUserToAddRows = false;
                PurchaseListDataGridView.Rows.Clear();
            }
        }
 




        ///// 엑셀 문서를 GRID로 삽입
        public static void grid_value_move(string _filename, DataGridView _grid_table,
                                           ProgressBar _progress,
                                           string _Prod_name, string _Prod_code,
                                           string _Comp_name, string _Comp_code)
        {

            ListT_TB_CNSM_L.Clear();
            
            _grid_table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;               
            Excel.Application exlApp = new Excel.Application();
            Excel.Workbook exlWorkbook = exlApp.Workbooks.Open(_filename);
            Excel._Worksheet exlWorksheet = exlWorkbook.Sheets[1];
            //Excel.Range exlRange = exlWorksheet.UsedRange;

            try
            {
                // 속도 향상 Test!!!    --> 190805 박현호            
                Excel.Range exlRange = exlWorksheet.get_Range("A1", "W188000");
                object[,] values = (object[,])exlRange.Value2;
                exlApp.Visible = false;

                int row_count = exlRange.Rows.Count;
                int col_count = exlRange.Columns.Count;

                _progress.Maximum = (row_count - 1);
                _progress.Minimum = 0;



                // Excel 문서에서 Column 을 읽고 해당 Colum 에서의 Cell Data 가 무엇인지 판별하는 Logic 을 짜야함!! (190806 - 박현호) 
                //***********************************************************************************************
                seperateColumns(exlRange, row_count, col_count);
                //***********************************************************************************************


                // Data 의 시작점에서 끝지점을 찾는 Logic
                //***********************************************************************************************
                int srt_row = 4;                                                    // Excel File 에서 Data 가 시작되는 Row 의 Index
                int count = 4;                                                      // Data 가 존재하는 Row 의 개수를 구하기 위한 count 변수

                while (true)                                                         // 행의 첫번째 Cell 값이 null 일경우 까지 계속 반복
                {
                    if (exlRange.Cells[count, 1].Value2 == null)      // null 값이 나온다면 행의 개수 row_count 의 개수를 count - 1 값으로 변경
                    {
                        //MessageBox.Show("다음 Row 의 값은 없습니다. row 의 개수는 " + (count-srt_row) + " 개 입니다.");
                        row_count = count - 1;
                        break;
                    }
                    count++;                // null 값이 나오지 않을 경우에는 Count 를 증가시킴.
                }
                //***********************************************************************************************

                //  Debug.Print(exlRange.Cells[0, 13].Value2);


                // GridView Cell 내용 채우기
                //************************************************************************************************************************************
                for (int i = srt_row; i <= row_count - 0; i++)
                {
                    _grid_table.Rows.Add(1);
                    _grid_table.Rows[i - srt_row].Cells[0].Value = (i - srt_row + 1).ToString();    // 번호
                    _grid_table.Rows[i - srt_row].Cells[1].Value = _Comp_code;                      // 업체번호
                    _grid_table.Rows[i - srt_row].Cells[2].Value = _Comp_name;                      // 업체코드
                    _grid_table.Rows[i - srt_row].Cells[3].Value = _Prod_name;                      // 상품명

                    //--------------------------------------------------------------------------------------------------
                    ListT_TB_CNSM_L.Add(new List<string>());


                    //                                                Excel Range           .Substring(0,12)                  
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 13].Value2.ToString());      // (13-00) 구매일자
                    ListT_TB_CNSM_L[i - srt_row].Add(_Prod_code);                                               // (99-01) 상품코드
                    ListT_TB_CNSM_L[i - srt_row].Add(_Comp_code);                                           // (99-02) 업체코드
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 1].Value2.ToString());       // (01-03) 주문번호
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 2].Value2.ToString());       // (02-04) 티켓번호
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 3].Value2.ToString());       // (03-05) 주문자고객명
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 4].Value2.ToString());       // (04-06) 주문자연락처 
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 5].Value2.ToString());       // (05-07) 주문자이메일주소
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 6].Value2.ToString());       // (06-08) 선물대상자고객명
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 7].Value2.ToString());       // (07-09) 선물대상자연락처전화번호
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 8].Value2.ToString());       // (08-10) 선물대상자이메일주소
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 11].Value2.ToString());       // (09-11) 옵션명
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 12].Value2.ToString());      // (10-12) 구매금액
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 13].Value2.ToString());      // (11-13) 할인금액
                    ListT_TB_CNSM_L[i - srt_row].Add("N");                                                          // (12-14) 티켓상태
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 18].Value2.ToString());      // (13-15) 구매일시
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 20].Value2.ToString());      // (14-16) 추가문구내용
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 21].Value2.ToString());      // (15-17) 옵션1          -> 출발일자 인것 같음
                    string option2 = exlRange.Cells[i, 22].Value2.ToString();
                    ListT_TB_CNSM_L[i - srt_row].Add(option2);                                                     // (16-18) 옵션2          -> 쇼 등급, 셔틀포함 여부 들어있음
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 23].Value2.ToString());      // (17-19) 옵션3          -> 성인, 소아, 유아 구분                 
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 24].Value2.ToString());      // (18-20) 옵션4
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 25].Value2.ToString());      // (19-21) 옵션5
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 26].Value2.ToString());      // (20-22) 대표자영문명
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 27].Value2.ToString());      // (22-23) 연령
                    ListT_TB_CNSM_L[i - srt_row].Add(exlRange.Cells[i, 28].Value2.ToString());      // (23-24) 대표자이메일주소
                    ListT_TB_CNSM_L[i - srt_row].Add("-");                                          // (99-25) 셔틀포함유무
                    ListT_TB_CNSM_L[i - srt_row].Add("-");                                          // (99-26) 비고내용
                    ListT_TB_CNSM_L[i - srt_row].Add("N");                                          // (99-27) 예약등록여부
                    ListT_TB_CNSM_L[i - srt_row].Add(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")); // (99-28) 최초등록일시
                    ListT_TB_CNSM_L[i - srt_row].Add(Global.loginInfo.ACNT_ID);                                          // (99-29) _FRST_RGTR_ID
                    ListT_TB_CNSM_L[i - srt_row].Add(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")); // (99-30) _FINL_MDFC_DTM timestamp
                    ListT_TB_CNSM_L[i - srt_row].Add(Global.loginInfo.ACNT_ID);                                          // (99-31) _FINL_MDFR_ID

                   /* for (int j = 1; j <= col_count; j++)
                    {
                        if (exlRange.Cells[i, j] != null && exlRange.Cells[i, j].Value2 != null)
                        {*/
                            try
                            {
                                //switch (j)          // Col No
                                //{
                                    //case 1:         // 주문번호
                                        _grid_table.Rows[i - srt_row].Cells[5].Value = ListT_TB_CNSM_L[i - srt_row][3];
                                       // break;
                                    //case 2:         // 티켓번호
                                        _grid_table.Rows[i - srt_row].Cells[4].Value = ListT_TB_CNSM_L[i - srt_row][4];
                                        //break;
                                    //case 3:         // 이름 (주문자)
                                        _grid_table.Rows[i - srt_row].Cells[7].Value = ListT_TB_CNSM_L[i - srt_row][5];
                                       // break;
                                    //case 4:         // 연락처 (주문자)
                                        _grid_table.Rows[i - srt_row].Cells[8].Value = ListT_TB_CNSM_L[i - srt_row][6];
                                       // break;
                                    //case 5:         // 이메일 (주문자)
                                        _grid_table.Rows[i - srt_row].Cells[9].Value = ListT_TB_CNSM_L[i - srt_row][7];
                                       // break;
                                    //case 10:         // 구매금액
                                        _grid_table.Rows[i - srt_row].Cells[10].Value = Utils.SetComma(ListT_TB_CNSM_L[i - srt_row][12]);
                                       // break;
                                    //case 13:         // 구매일자
                                        _grid_table.Rows[i - srt_row].Cells[6].Value = ListT_TB_CNSM_L[i - srt_row][0];
                                       // break;
                                //}
                                //  _grid_table.Rows[i - srt_row].Cells[j - 1].Value = exlRange.Cells[i, j].Value2.ToString();
                            }
                            catch (Exception ex)
                            {
                                Debug.Print("DBGV Invoke : " + ex.ToString());
                            }
                        /*}
                        else
                        {
                            Debug.Print("");
                        }
                    }*/
                    _progress.Value += _progress.Maximum / i;
                }
                _grid_table.Rows[0].Cells[0].Selected = false;                
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Marshal.ReleaseComObject(exlRange);
                Marshal.ReleaseComObject(exlWorksheet);

                exlWorkbook.Close();
                Marshal.ReleaseComObject(exlWorkbook);

                exlApp.Quit();
                Marshal.ReleaseComObject(exlApp);
                exlApp = null;
                //************************************************************************************************************************************
            }
            catch (Exception e)
            {
                if(e.Message.Equals("예외가 발생한 HRESULT: 0x800A03EC")) { 
                    MessageBox.Show("*.xls 가 아닌 *.xlsx 확장자(통합문서) Excel File 을 선택해주세요");
                }
            } 
        }



        public static string excel_ver_app()
        {
            string @return = string.Empty;
            Excel.Application excelApp = new Excel.Application();
            try
            {
                // excelApp = Marshal.GetActiveObject("Excel.Application");
                // excelApp = Excel
            }
            catch (Exception)
            {
                @return = excel_not_install;    // 설치되지 않았으므로 Exception 처리
                return @return;
            }

            if (excelApp != null)
            {
                try
                {
                    string ver = string.Empty;
                    switch (excelApp.Version)
                    {
                        case "7.0":
                            ver = "95";
                            break;
                        case "8.0":
                            ver = "97";
                            break;
                        case "9.0":
                            ver = "2000";
                            break;
                        case "10.0":
                            ver = "2002";
                            break;
                        case "11.0":
                            ver = "2003";
                            break;
                        case "12.0":
                            ver = "2007";
                            break;
                        case "14.0":
                            ver = "2010";
                            break;
                        case "15.0":
                            ver = "2013";
                            break;
                        case "16.0":
                            ver = "2016 or 2019";
                            break;
                        default: /// = "Higher than the 2013 version"
                            ver = excelApp.Version.ToUpper();
                            break;
                    }
                    excelApp.Quit();
                    excelApp = null;
                    @return = ver;
                }
                catch (Exception ex)
                {
                    @return = excel_not_install;
                }
            }
            else
            {
                @return = excel_not_install;
            }
            return @return;
        }

        private const string REGISTRY_EXCEL_KEY = @"Excel.Application";
        public static void ex_ver()
        {
            RegistryKey rkHKCR = Registry.ClassesRoot;
            RegistryKey rkExcelKey = rkHKCR.OpenSubKey(REGISTRY_EXCEL_KEY);
            bool bExcelInstalled = (rkExcelKey == null ? false : true);

            //SubKey 값으로 CurVer 까지 확인한다면, Excel 의 버전
            if (bExcelInstalled)
            {
                // Installed
            }
            else
            {
                // Not Installed
            }
        }


        // Excel 의 Header Cell 을 읽고 사전으로 판별!        --> 190806 박현호
        //=====================================================================================
        public static void seperateColumns(Excel.Range exlRange, int row_count, int col_count)
        {
            Dictionary<string, List<string>> excelColumnDictionary = ExcelColumnDictionary.initializingExcelColumnDic();
            int headerStartRow = findHeaderStartPoint(exlRange, row_count, col_count);
            for(int i=0; i<col_count; i++)
            {
                string headerName = exlRange.Cells[headerStartRow, i].Value2.ToString();
                
            }
        }
        //=====================================================================================


        // Excel 의 Header 의 시작점을 파악      --> 190806 박현호
        //======================================================================================
        public static int findHeaderStartPoint(Excel.Range exlRange, int row_count, int col_count)
        {
            Dictionary<string, List<string>> excelColumnDictionary = ExcelColumnDictionary.initializingExcelColumnDic();
            List<string> dictionacryKeys = ExcelColumnDictionary.getKeys();
            int headerStartRow = 0;

            for (int i = 1; i <= row_count; i++)
            {
                for (int j = 0; j < dictionacryKeys.Count; j++)
                {
                    List<string> values = excelColumnDictionary[dictionacryKeys[j]];

                    if (values.Contains(exlRange.Cells[i, 1].Value2.ToString()))
                    {
                        MessageBox.Show("본 Column 의 Title 은 " + dictionacryKeys[j]);
                        headerStartRow = i;
                        break;
                    }
                }
            }
            return headerStartRow;
        }
        //======================================================================================
    }






}
