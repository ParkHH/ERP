using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using TripERP.Common;

namespace TripERP.ReservationMgt
{
    public partial class PurchaseListMgt : Form
    {
        private string _companyNumber = "";
        private string _companyName = "";
        private string _productNubmer = "";
        private string _productName = "";

        // 엑셀 데이터의 자료 표준단어와 위치정보 보관용
        string[,] _excelItemArray;                                                            // 엑셀자료구조정보 배열
        private int _excelItemCount = 0;                                                      // 엑셀자료항목 수
        private int _excelStartRowNumer = 0;                                                  // 실데이터가 위치한 행값

        private string _fullFileName = "";                                                    // 엑셀 파일명(경로포함)
        private string _fileName = "";                                                        // 엑셀 파일명
        private string _filePath = "";                                                        // 엑셀 경로명

        public PurchaseListMgt()
        {
            InitializeComponent();

            ExcelImporter.ex_ver();
        }

        // 폼 로딩 초기화
        private void PurchaseListMgt_Load(object sender, EventArgs e)
        {
            this.Controls.Add(Pan_read_msg);
            Pan_read_msg.Location = new Point((this.Width - Pan_read_msg.Width) / 2,
                                              (this.Height - Pan_read_msg.Height) / 2);

            InitControls();

            this.Controls.Add(Pan_read_msg);
            saveButton.Enabled = false;
            //convertReservationStateButton.Enabled = false;
            Pan_read_msg.Visible = false;

            // 파일 업로드 하지 않고 예약 전환시도를 할 경우를 방지하기 위해 예약전환 버튼을 Enabled = false 처리
            convertReservationStateButton.Enabled = false;
        }

        // 초기화
        private void InitControls()
        {
            // 그리드 스타일 초기화
            InitDataGridView();

            InitComboBox();
        }

        // 그리드 스타일 초기화
        private void InitDataGridView()
        {
            DataGridView dataGridView1 = UploadHistoryDataGridView2;

            dataGridView1.DoubleBuffered(true);
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.DoubleBuffered(true);

            DataGridView dataGridView2 = PurchaseListDataGridView2;

            dataGridView2.DoubleBuffered(true);
            //dataGridView2.RowHeadersVisible = false;
            //dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView2.MultiSelect = false;
            //dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView2.EnableHeadersVisualStyles = false;
            //dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView2.RowHeadersVisible = false;
            //dataGridView2.DoubleBuffered(true);
        }

        // 콤보박스 초기화
        private void InitComboBox()
        {
            // 상품명
            SearchProductListComboBox.Items.Clear();
            SearchProductListComboBox.Text = "전체";

            string query = "CALL SelectPrdtListForPurchaseFileProc";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품기본정보를 가져올 수 없습니다.");
                return;
            }

            SearchProductListComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 상품일련번호와 상품명을 콤보에 설정
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_NM, PRDT_CNMB);
                SearchProductListComboBox.Items.Add(item);
                productNameComboBox.Items.Add(item);
            }

            SearchProductListComboBox.SelectedIndex = -1;

            // 모객업체
            SearchCooperativeCompanyComboBox.Items.Clear();
            SearchCooperativeCompanyComboBox.Text = "전체";

            string COOP_CMPN_DVSN_CD = "10";                                                                  // 모객업체
            query = string.Format("CALL SelectCoopCmpnList ('{0}', '{1}')", COOP_CMPN_DVSN_CD, ' ');
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("모객업체 정보를 가져올 수 없습니다.");
                return;
            }

            SearchCooperativeCompanyComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                SearchCooperativeCompanyComboBox.Items.Add(item);
                CooperativeCompanyComboBox.Items.Add(item);
            }

            SearchCooperativeCompanyComboBox.SelectedItem = -1;
            CooperativeCompanyComboBox.SelectedItem = -1;

        }

        // 폼 닫기
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //=============================================================================================================================
        // Excel File Open Button Click 
        //=============================================================================================================================
        private void fileOpenButton_Click(object sender, EventArgs e)
        {
            // 그리드 내용 초기화
            PurchaseListDataGridView2.Rows.Clear();

            string[,] gridItem;

            saveButton.Enabled = false;

            if (productNameComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품을 선택하세요");
                productNameComboBox.Focus();
                return;
            }
            if (CooperativeCompanyComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("모객업체를 선택하세요");
                CooperativeCompanyComboBox.Focus();
                return;
            }

            _companyNumber = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyComboBox);        // 모객업체번호
            _companyName = CooperativeCompanyComboBox.Text.Trim();
            _productNubmer = Utils.GetSelectedComboBoxItemValue(productNameComboBox);               // 상품일련번호
            _productName = productNameComboBox.Text.Trim();

            // 파일 오픈
            OpenFileDialog open_diag = new OpenFileDialog();
            open_diag.Title = "구매자목록 파일 가져오기";

            //open_diag.Filter = "xls 파일|*.xls|xlsx 파일|*.xlsx|모든 파일|*.*";
            open_diag.Filter = "xlsx 파일|*.xlsx|모든 파일|*.*";

            if (open_diag.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            _fileName = open_diag.SafeFileName;
            _fullFileName = open_diag.FileName;
            _filePath = _fullFileName.Replace(_fileName, "");

            filePathNameTextBox.Text = _fileName + "\\" + _filePath;

            // 툴팁에 전체 파일명을 표시
            toolTip1.SetToolTip(filePathNameTextBox, _fullFileName);

            Pan_read_msg.Visible = true;
            Pan_read_msg.BringToFront();
            Pan_read_msg.Refresh();

            // 엑셀파일구조기본 테이블에서 파일정보를 획득
            if (getExcelFileInfo() == false)
            {
                Pan_read_msg.Visible = false;
                saveButton.Enabled = false;
                return;
            }

            //----------------------------------------------------------------------------------------------------------------------------------
            // 엑셀 파일을 읽어 배열에 저장
            //----------------------------------------------------------------------------------------------------------------------------------
            Pan_read_msg.Visible = false;
            saveButton.Enabled = true;
            //PurchaseListDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            Excel.Application exlApp = new Excel.Application();
            Excel.Workbook exlWorkbook = exlApp.Workbooks.Open(_fullFileName);
            Excel.Worksheet exlWorksheet = exlWorkbook.Sheets[1];

            try
            {
                Excel.Range rng = exlWorksheet.UsedRange;

                exlApp.Visible = false;

                int totalColumns = exlWorksheet.UsedRange.Cells.Columns.Count + 1;

                Object[,] data = rng.Value;

                int rowCount = data.GetLength(0);
                int colCount = data.GetLength(1);

                int colPos = 0;
                string synonymName = "";
                string standardTermName = "";

                // 배열의 행개수 결정 (엑셀 행의 수 - 시작행)
                int gridItemSize = rowCount - _excelStartRowNumer + 1; 

                gridItem = new string[gridItemSize, 17];

                // 배열에 저장할 행의 첨자
                int savedRowNumber = 0;
                string dealName = "";
                bool validFile = false;

                //----------------------------------------------------------------------------------------------------------------------------------
                // 첫행을 읽고 파일구조를 체크하여 원하는 상품과 모객업체가 아니면 처리를 중단
                //----------------------------------------------------------------------------------------------------------------------------------
                if (_companyName.Equals("티몬"))
                {
                    // 티몬은 4번째 행의 10번째 셀이 딜명
                    dealName = data?[_excelStartRowNumer, 10].ToString();
                    dealName = dealName.Replace(" ", "");

                    if (_productName.Equals("통킨쇼"))
                    {
                        if (dealName.IndexOf("하노이통킨쇼") > 0) validFile = true;
                    }
                    else if (_productName.Equals("프레지던트크루즈"))
                    {
                        if (dealName.IndexOf("프레지던트크루즈") > 0) validFile = true;
                    }
                    else if (_productName.Equals("럭셔리 데이 크루즈"))
                    {
                        if (dealName.IndexOf("럭셔리데이크루즈") > 0) validFile = true;
                    }
                }
                else if (_companyName.Equals("위메프"))
                {
                    // 위메프는 엑셀 1행 1열이 딜명
                    dealName = data?[1, 1].ToString();
                    dealName = dealName.Replace(" ", "");

                    if (_productName.Equals("통킨쇼"))
                    {
                        if (dealName.IndexOf("하노이통킨쇼") > 0) validFile = true;
                    }
                    else if (_productName.Equals("프레지던트크루즈"))
                    {
                        if (dealName.IndexOf("1박2일크루즈") > 0) validFile = true;
                    }
                    else if (_productName.Equals("럭셔리 데이 크루즈"))
                    {
                        if (dealName.IndexOf("럭셔리데이크루즈") > 0) validFile = true;
                    }
                }

                if (validFile == false)
                {
                    MessageBox.Show("엑셀파일의 내용이 맞지 않습니다. 선택하신 파일, 모객업체, 상품이 맞는지 확인하세요.");
                    return;
                }

                //---------------------------------------------------------------------------------------------------------------------
                // 엑셀의 행 개수만큼 loop 처리
                //---------------------------------------------------------------------------------------------------------------------
                //for (int rowPos = _excelStartRowNumer; rowPos < exlWorksheet.UsedRange.Cells.Rows.Count + 1; rowPos++)
                for (int rowPos = _excelStartRowNumer; rowPos < rowCount + 1; rowPos++)
                {
                    // 엑셀자료구조정보 배열 수만큼 loop 처리
                    for (int ii = 0; ii < _excelItemCount; ii++)
                    {
                        standardTermName = _excelItemArray[ii, 0];
                        colPos = Int16.Parse(_excelItemArray[ii, 1]);
                        synonymName = _excelItemArray[ii, 2];

                        try
                        {
                            if (!String.IsNullOrEmpty(data?[rowPos, colPos].ToString()))
                            {
                                gridItem[savedRowNumber, ii] = data?[rowPos, colPos].ToString();
                            }
                        }
                        catch (NullReferenceException)
                        {
                            gridItem[savedRowNumber, ii] = "";
                        }
                    }

                    savedRowNumber++;
                }

                exlWorkbook.Close(true);
                exlApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("엑셀 업로드 중 오류가 발생했습니다. 운영담당자에게 연락하세요. " + ex.ToString());
                throw new Exception(ex.ToString());
            }
            finally
            {
                // Clean up
                ReleaseExcelObject(exlWorksheet);
                ReleaseExcelObject(exlWorkbook);
                ReleaseExcelObject(exlApp);
            }

            // 모객업체에 따라 배열을 그리드에 출력
            if (_companyName.Equals("티몬"))
            {
                putArrayToGridForTIMON(gridItem);
            }
            else if (_companyName.Equals("위메프"))
            {
                putArrayToGridForWEMAF(gridItem);
            }

            PurchaseListDataGridView2.ClearSelection();

            return;
        }

        //======================================================================================================================================
        // 엑셀을 저장한 배열을 그리드에 출력 (티몬용)
        //======================================================================================================================================
        private void putArrayToGridForTIMON(string[,] gridItem)
        {
            //----------------------------------------------------------------------------------------------------------------------------------
            // 배열을 그리드에 출력
            //----------------------------------------------------------------------------------------------------------------------------------
            string PROC_STTS_NM = "읽음";                   // 처리상태 (읽음, 등록, 기등록 구분)
            string ORDR_NO = "";                            // 주문번호
            string TCKT_NO = "";                            // 티켓번호
            string ORDE_CUST_NM = "";                       // 주문자고객명
            string ORDE_CTIF_PHNE_NO = "";                  // 주문자연락처전화번호
            string ORDE_EMAL_ADDR = "";                     // 주문자이메일주소
            string PRCS_AMT = "";                           // 구매금액
            string DSCT_AMT = "";                           // 할인금액
            string PRCS_DT = "";                            // 구매일자
            string OPTN_NM = "";                            // 옵션명
            string RPRS_ENG_NM = "";                        // 대표자영문명
            string AGE_CNTS = "";                           // 연령내용
            string REMK_CNTS = "";                          // 비고내용

            int gridItemCount = gridItem.GetLength(0);      // 출력할 그리드 행의 개수

            for (int ii = 0; ii < gridItemCount; ii++)
            {
                if (!String.IsNullOrEmpty(gridItem[ii,0]))
                {
                        if (_productName.Equals("통킨쇼"))
                        {
                            ORDR_NO = gridItem[ii, 0];
                            TCKT_NO = gridItem[ii, 1];
                            ORDE_CUST_NM = gridItem[ii, 2];
                            ORDE_CTIF_PHNE_NO = gridItem[ii, 3];
                            OPTN_NM = gridItem[ii, 4];
                            PRCS_AMT = Utils.SetComma(gridItem[ii, 5]);
                            DSCT_AMT = Utils.SetComma(gridItem[ii, 6]);
                            PRCS_DT = gridItem[ii, 7].Substring(0, 10);
                            RPRS_ENG_NM = gridItem[ii, 8];
                            AGE_CNTS = gridItem[ii, 9];
                            ORDE_EMAL_ADDR = gridItem[ii, 10];
                            REMK_CNTS = gridItem[ii, 11];
                        }
                        else if (_productName.Equals("프레지던트크루즈"))
                        {
                            ORDR_NO = gridItem[ii, 0];
                            TCKT_NO = gridItem[ii, 1];
                            ORDE_CUST_NM = gridItem[ii, 2];
                            OPTN_NM = gridItem[ii, 3];
                            PRCS_AMT = Utils.SetComma(gridItem[ii, 4]);
                            DSCT_AMT = Utils.SetComma(gridItem[ii, 5]);
                            PRCS_DT = gridItem[ii, 6].Substring(0, 10);
                            RPRS_ENG_NM = gridItem[ii, 7];
                            ORDE_CTIF_PHNE_NO = gridItem[ii, 8];
                            ORDE_EMAL_ADDR = gridItem[ii, 9];
                            AGE_CNTS = gridItem[ii, 10];
                            REMK_CNTS = gridItem[ii, 11];
                        }
                        else if (_productName.Equals("럭셔리 데이 크루즈"))
                        {
                            ORDR_NO = gridItem[ii, 0];
                            TCKT_NO = gridItem[ii, 1];
                            ORDE_CUST_NM = gridItem[ii, 2];
                            ORDE_CTIF_PHNE_NO = gridItem[ii, 3];
                            ORDE_EMAL_ADDR = gridItem[ii, 4];
                            OPTN_NM = gridItem[ii, 5];
                            PRCS_AMT = Utils.SetComma(gridItem[ii, 6]);
                            DSCT_AMT = Utils.SetComma(gridItem[ii, 7]);
                            PRCS_DT = gridItem[ii, 8].Substring(0, 10);
                            RPRS_ENG_NM = gridItem[ii, 9];
                            AGE_CNTS = gridItem[ii, 10];
                            REMK_CNTS = gridItem[ii, 11];
                        }

                        PurchaseListDataGridView2.Rows.Add
                        (
                            PROC_STTS_NM,
                            _companyNumber,
                            _companyName,
                            _productNubmer,
                            _productName,
                            ORDR_NO,
                            TCKT_NO,
                            ORDE_CUST_NM,
                            RPRS_ENG_NM,
                            ORDE_CTIF_PHNE_NO,
                            ORDE_EMAL_ADDR,
                            PRCS_AMT,
                            DSCT_AMT,
                            PRCS_DT,
                            OPTN_NM,
                            AGE_CNTS,
                            REMK_CNTS
                        );
                }
                else
                {
                    break;
                }
            }
        }

        //======================================================================================================================================
        // 엑셀을 저장한 배열을 그리드에 출력 (위메프용)
        //======================================================================================================================================
        private void putArrayToGridForWEMAF(string[,] gridItem)
        {
            //----------------------------------------------------------------------------------------------------------------------------------
            // 배열을 그리드에 출력
            //----------------------------------------------------------------------------------------------------------------------------------
            string PROC_STTS_NM = "읽음";                   // 처리상태 (읽음, 등록, 기등록 구분)
            string ORDR_NO = "";                            // 주문번호
            string TCKT_NO = "";                            // 티켓번호
            string ORDE_CUST_NM = "";                       // 주문자고객명
            string ORDE_CTIF_PHNE_NO = "";                  // 주문자연락처전화번호
            string ORDE_EMAL_ADDR = "";                     // 주문자이메일주소
            string PRCS_AMT = "";                           // 구매금액
            string DSCT_AMT = "";                           // 할인금액
            string PRCS_DT = "";                            // 구매일자
            string OPTN_NM = "";                            // 옵션명
            string RPRS_ENG_NM = "";                        // 대표자영문명
            string AGE_CNTS = "";                           // 연령내용
            string REMK_CNTS = "";                          // 비고내용

            int gridItemCount = gridItem.GetLength(0);      // 출력할 그리드 행의 개수

            for (int ii = 0; ii < gridItemCount; ii++)
            {
                if (!String.IsNullOrEmpty(gridItem[ii, 0]))
                {
                    ORDR_NO = gridItem[ii, 0];
                    ORDE_CUST_NM = gridItem[ii, 1];
                    ORDE_CTIF_PHNE_NO = gridItem[ii, 2];
                    ORDE_EMAL_ADDR = gridItem[ii, 3];
                    PRCS_DT = gridItem[ii, 4].Substring(0, 10);
                    TCKT_NO = gridItem[ii, 5];
                    OPTN_NM = gridItem[ii, 6];
                    PRCS_AMT = Utils.SetComma(gridItem[ii, 7]);
                    REMK_CNTS = gridItem[ii, 8];

                    PurchaseListDataGridView2.Rows.Add
                    (
                        PROC_STTS_NM,
                        _companyNumber,
                        _companyName,
                        _productNubmer,
                        _productName,
                        ORDR_NO,
                        TCKT_NO,
                        ORDE_CUST_NM,
                        RPRS_ENG_NM,
                        ORDE_CTIF_PHNE_NO,
                        ORDE_EMAL_ADDR,
                        PRCS_AMT,
                        DSCT_AMT,
                        PRCS_DT,
                        OPTN_NM,
                        AGE_CNTS,
                        REMK_CNTS
                    );
                }
                else
                    {
                        break;
                    }
                }
        }

        //======================================================================================================================================
        // 엑셀파일구조기본 테이블에서 파일정보를 획득
        //======================================================================================================================================
        private bool getExcelFileInfo()
        {
            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyComboBox);             // 모객업체
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(productNameComboBox);                       // 상품일련번호

            string DATA_COL_NO = "";                                                                          // 데이터 열번호
            string STD_TRMY_NM = "";                                                                          // 표준용어명
            string ALOP_SYNO_NM = "";                                                                         // 이음동의어명

            //----------------------------------------------------------------------------------------------------------------------------------
            // 모객업체의 상품 구매목록 파일 정보를 검색하여 행 위치값을 결정
            //----------------------------------------------------------------------------------------------------------------------------------
            string query = string.Format("CALL SelectExcelFileStructureBasicItem ( '{0}', {1} )", TKTR_CMPN_NO, PRDT_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);

            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            if (dataRowList.Count == 0)
            {
                MessageBox.Show("선택하신 모객업체의 상품에 대한 엑셀기본정보가 없습니다. 운영담당자에게 연락하세요.");
                return false;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];
            _excelStartRowNumer = Int16.Parse(dataRow["DATA_ROW_NO"].ToString());                         // 실데이터가 위치한 행값

            //----------------------------------------------------------------------------------------------------------------------------------
            // 엑셀자료항목별 위치값과 이음동의어를 검색하여 배열에 저장
            //----------------------------------------------------------------------------------------------------------------------------------
            query = string.Format("CALL SelectExcelFileStructureInfo ( '{0}', {1} )", TKTR_CMPN_NO, PRDT_CNMB);
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("엑셀파일구조정보를 가져올 수 없습니다.");
                return false;
            }

            _excelItemCount = dataSet.Tables[0].Rows.Count;

            int xx = 0;

            this._excelItemArray = new string[_excelItemCount, 3];

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                DATA_COL_NO = datarow["DATA_COL_NO"].ToString();
                STD_TRMY_NM = datarow["STD_TRMY_NM"].ToString();
                ALOP_SYNO_NM = datarow["ALOP_SYNO_NM"].ToString();

                _excelItemArray[xx, 0] = STD_TRMY_NM;
                _excelItemArray[xx, 1] = DATA_COL_NO;
                _excelItemArray[xx, 2] = ALOP_SYNO_NM;
                xx++;
            }

            return true;
        }

        // 구매자목록 업로드 목록 그리드초기화
        private void resetPurchaseListDataGridView2()
        {
            PurchaseListDataGridView2.Rows.Clear();
        }

        // 엑셀 초기화
        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        // 이력 검색 Button Click Event --> 190801 박현호
        //=============================================================================================================================
        private void searchUploadHistoryListButton_Click(object sender, EventArgs e)
        {
            getUploadHistory();
        }

        // 이력 검색 Method --> 190801 박현호
        public void getUploadHistory()
        {
            string PRDT_CMNB = "";
            string TKTR_CMPN_NO = "";
            if (!SearchProductListComboBox.Text.Trim().Equals("전체"))
            {
                ComboBoxItem productItem = (ComboBoxItem)SearchProductListComboBox.SelectedItem;
                PRDT_CMNB = productItem.Value.ToString();
            }
            if (!SearchCooperativeCompanyComboBox.Text.Equals("전체"))
            {
                ComboBoxItem companyItem = (ComboBoxItem)SearchCooperativeCompanyComboBox.SelectedItem;
                TKTR_CMPN_NO = companyItem.Value.ToString();
            }

            string TSK_DTM_FROM = UploadDateFromDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");
            string TSK_DTM_TO = UploadDateEndDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");

            string getUploadHistoryProcedure = string.Format("CALL SelectFileUploadHistory('{0}','{1}','{2}','{3}')", PRDT_CMNB, TKTR_CMPN_NO, TSK_DTM_FROM, TSK_DTM_TO);
            DataSet data = DbHelper.SelectQuery(getUploadHistoryProcedure);
            DataRowCollection rows = data.Tables[0].Rows;

            MessageBox.Show(rows.Count + " 건의 Data 가 조회되었습니다.");

            //ExcelImporter.UploadHistoryDataGridView.Rows.Clear();
            UploadHistoryDataGridView2.Rows.Clear();

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i];
                int TSK_CNMB = int.Parse(row["TSK_CNMB"].ToString());
                string TSK_DTM = row["TSK_DTM"].ToString();
                string TK_CMPN_NM = row["CMPN_NM"].ToString();
                string PRDT_NM = row["PRDT_NM"].ToString();
                string FILE_NM = row["FILE_NM"].ToString();

                UploadHistoryDataGridView2.Rows.Add(TSK_CNMB, TSK_DTM, TK_CMPN_NM, PRDT_NM, FILE_NM);
            }
        }

        //=============================================================================================================================
        // 저장 Button 눌렀을경우      --> 190731 박현호
        //=============================================================================================================================
        private void saveButton_Click(object sender, EventArgs e)
        {
            bool result = insertPurchaseListData();
            convertReservationStateButton.Enabled = true;
        }


        //=============================================================================================================================
        //구매자 목록 File Upload 이력, Data DB Insert 하기!      --> 190801 박현호
        //=============================================================================================================================
        public bool insertPurchaseListData()
        {
            int insertCount = 0;
            int existCount = 0;

            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyComboBox);        // 모객업체번호
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(productNameComboBox);             // 상품일련번호
            string PRDT_NM = Utils.GetSelectedComboBoxItemText(productNameComboBox);                // 상품명
            string ORDR_NO = "";                                                                    // 주문번호
            string TCKT_NO = "";                                                                    // 티켓번호
            string ORDE_CUST_NM = "";                                                               // 주문자고객명
            string ORDE_CTIF_PHNE_NO = "";                                                          // 주문자연락처전화번호
            string ORDE_EMAL_ADDR = "";                                                             // 주문자이메일주소
            double PRCS_AMT = 0;                                                                    // 구매금액
            double DSCT_AMT = 0;                                                                    // 할인금액
            string PRCS_DT = "";                                                                    // 구매일자
            string OPTN_NM = "";                                                                    // 옵션명
            string RPRS_ENG_NM = "";                                                                // 대표자영문명
            string AGE_CNTS = "";                                                                   // 연령내용
            string REMK_CNTS = "";                                                                  // 비고내용
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            string query = "";

            string RESULT_CODE = "";

            string[] queryStringArray = new string[PurchaseListDataGridView2.Rows.Count];           // 트랜잭션 처리를 위한 query 배열
            string[] queryResultArray = new string[PurchaseListDataGridView2.Rows.Count];           // 건별 sql 처리 결과 리턴 배열

            // 그리드를 한행씩 읽어서 구매목록 테이블에 Insert하고 그리드의 상태를 변경
            for (int gridRow = 0; gridRow < PurchaseListDataGridView2.Rows.Count; gridRow++)
            {
                PRCS_DT = PurchaseListDataGridView2["PRCS_DT", gridRow].Value.ToString();                                               // 구매일자
                ORDR_NO = PurchaseListDataGridView2["ORDR_NO", gridRow].Value.ToString();                                             // 주문번호
                TCKT_NO = PurchaseListDataGridView2["TCKT_NO", gridRow].Value.ToString();                                               // 티켓번호
                ORDE_CUST_NM = PurchaseListDataGridView2["ORDE_CUST_NM", gridRow].Value.ToString();                          // 구매자고객명

                try
                {
                    ORDE_CTIF_PHNE_NO = PurchaseListDataGridView2["ORDE_CTIF_PHNE_NO", gridRow].Value.ToString().Trim();          // 구매자연락처전화번호
                } catch (System.NullReferenceException)
                {
                    ORDE_CTIF_PHNE_NO = "";
                }

                try
                {
                    ORDE_EMAL_ADDR = PurchaseListDataGridView2["ORDE_EMAL_ADDR", gridRow].Value.ToString();               // 구매자이메일주소
                } catch (System.NullReferenceException)
                {
                    ORDE_EMAL_ADDR = "";
                }

                OPTN_NM = PurchaseListDataGridView2["OPTN_NM", gridRow].Value.ToString();                             // 옵션명
                PRCS_AMT = Utils.GetDoubleValue(PurchaseListDataGridView2["PRCS_AMT", gridRow].Value.ToString());     // 구매금액
                DSCT_AMT = Utils.GetDoubleValue(PurchaseListDataGridView2["DSCT_AMT", gridRow].Value.ToString());     // 할인금액

                // 모객업체에 따라 저장 컬럼값 예외 처리
                if (_companyName.Equals("티몬"))
                {
                    if (PurchaseListDataGridView2["RPRS_ENG_NM", gridRow].Value == null)
                    {
                        RPRS_ENG_NM = "";
                    }
                    else
                    {
                        RPRS_ENG_NM = PurchaseListDataGridView2["RPRS_ENG_NM", gridRow].Value.ToString();                 // 대표자영문명
                    }

                    if (PRDT_NM.Equals("프레지던트크루즈") || PRDT_NM.Equals("통킨쇼"))
                    {
                        AGE_CNTS = PurchaseListDataGridView2["AGE_CNTS", gridRow].Value.ToString();                           // 연령내용
                        REMK_CNTS = PurchaseListDataGridView2["REMK_CNTS", gridRow].Value.ToString();                         // 비고내용
                    }
                    else
                    {
                        AGE_CNTS = "";                                                                                        // 연령내용
                        REMK_CNTS = "";
                    }
                }
                else if (_companyName.Equals("위메프"))
                {
                    if (PRDT_NM.Equals("럭셔리 데이 크루즈"))
                    {
                        AGE_CNTS = "";                                                                                        // 연령내용
                        REMK_CNTS = PurchaseListDataGridView2["REMK_CNTS", gridRow].Value.ToString();                         // 비고내용
                    }
                    else if (PRDT_NM.Equals("프레지던트크루즈"))
                    {
                        AGE_CNTS = "";
                        REMK_CNTS = PurchaseListDataGridView2["REMK_CNTS", gridRow].Value.ToString();                         // 비고내용
                    }
                    else if (PRDT_NM.Equals("통킨쇼"))
                    {
                        AGE_CNTS = "";
                        REMK_CNTS = PurchaseListDataGridView2["REMK_CNTS", gridRow].Value.ToString();                         // 비고내용
                    }
                }


                /*
                //------------------------------------------------------------------------------------------------------------------
                // 고객 등록 정보 암호화     --> 191024 박현호
                //------------------------------------------------------------------------------------------------------------------
                ORDE_CUST_NM = EncryptMgt.Encrypt(ORDE_CUST_NM, EncryptMgt.aesEncryptKey);
                ORDE_CTIF_PHNE_NO = EncryptMgt.Encrypt(ORDE_CTIF_PHNE_NO, EncryptMgt.aesEncryptKey);
                ORDE_EMAL_ADDR = EncryptMgt.Encrypt(ORDE_EMAL_ADDR, EncryptMgt.aesEncryptKey);
                RPRS_ENG_NM = EncryptMgt.Encrypt(RPRS_ENG_NM, EncryptMgt.aesEncryptKey);
                */


                query = string.Format("CALL InsertPurchaserListItem ('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')",
                    PRCS_DT,
                    PRDT_CNMB,
                    CMPN_NO,
                    ORDR_NO,
                    TCKT_NO,
                    ORDE_CUST_NM,
                    ORDE_CTIF_PHNE_NO,
                    ORDE_EMAL_ADDR,
                    OPTN_NM,
                    PRCS_AMT,
                    DSCT_AMT,
                    RPRS_ENG_NM,
                    AGE_CNTS,
                    REMK_CNTS,
                    FRST_RGTR_ID
                );

                // 처리할 sql을 배열에 저장
                queryStringArray[gridRow] = query;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // SQL 일괄처리 후 리턴 값을 받아 그리드에 행의 처리 결과를 반영
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            // 최종 처리한 SQL에 오류가 있으면 에러 처리
            if (retVal != 0)
            {
                MessageBox.Show("구매자파일 등록 중에 오류가 발생했습니다. 운영담당자에게 연락하세요.");
                return false;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 구매자목록의 처리 결과를 그리드에 반영
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            for (int jj = 0; jj < queryResultArray.Length; jj++)
            {
                RESULT_CODE = queryResultArray[jj];

                // 기 등록건
                if (RESULT_CODE.Equals("EXISTS"))
                {
                    //PurchaseListDataGridView2.SelectedRows[gridRow].Cells["PROC_STTS_NM"].Value = "중복";
                    PurchaseListDataGridView2["PROC_STTS_NM", jj].Value = "중복";

                    //optionDataGridView["OPTN_CNMB", _clickOptionRow].Value = OPTN_CNMB;

                    existCount++;
                }
                else if (RESULT_CODE.Equals("INSERT"))
                {
                    //PurchaseListDataGridView2.SelectedRows[gridRow].Cells["PROC_STTS_NM"].Value = "등록";
                    PurchaseListDataGridView2["PROC_STTS_NM", jj].Value = "등록";
                    insertCount++;
                }
            }

            PurchaseListDataGridView2.Refresh();

            if (insertCount > 0)
            {
                insertFileUploadRecordData();
                MessageBox.Show("구매자목록을 등록했습니다. [신규등록] " + insertCount + "건   [중복] " + existCount + "건");
                return true;
            }
            else
            {
                MessageBox.Show("중복데이터로 등록 대상이 없습니다. [신규등록] " + insertCount + "건   [중복] " + existCount + "건");
                return true;
            }
        }

        //=============================================================================================================================
        // 파일 업로드 이력 Data Insert Method             --> 190731 박현호
        //=============================================================================================================================
        private void insertFileUploadRecordData()
        {
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyComboBox);        // 모객업체번호
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(productNameComboBox);             // 상품일련번호
            string FILE_NM = _fileName;                                                             // 파일명
            string FILE_PATH_NM = "";                                                               // 파일 경로명
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자아이디

            string query = string.Format("CALL InsertPurchaseFileUploadHistory('{0}','{1}','{2}','{3}','{4}')", CMPN_NO, PRDT_CNMB, FILE_NM, FILE_PATH_NM, FRST_RGTR_ID);

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("구매자목록파일 이력정보를 저장할 수 없습니다.");
                return;
            }
        }

        private void CooperativeCompanyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _companyNumber = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyComboBox);             // 모객업체
            _companyName = CooperativeCompanyComboBox.Text.Trim();
        }

        private void productNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _productNubmer = Utils.GetSelectedComboBoxItemValue(productNameComboBox);               // 상품일련번호
            _productName = productNameComboBox.Text.Trim();
        }

        //=============================================================================================================================
        // 예약 전환 버튼 Event    --> 190802 박현호
        //=============================================================================================================================
        private void convertReservationStateButton_Click(object sender, EventArgs e)
        {
            PopUpInsertReservationFromPurchaseList form = new PopUpInsertReservationFromPurchaseList();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        //======================================================================================================================================
        // 초기화 버튼 클릭
        //======================================================================================================================================
        private void resetButton_Click(object sender, EventArgs e)
        {
            CooperativeCompanyComboBox.SelectedIndex = -1;
            productNameComboBox.SelectedIndex = -1;
            filePathNameTextBox.Text = "";
        }
    }
}
