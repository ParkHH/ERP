using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;

namespace TripERP.Invoice
{
    public partial class InvoiceManageForm : Form
    {
        enum eForeignExchangeRateDataGridView { NTFC_DT, CUR_CD, BASI_EXRT, STMT_EXRT, INVC_EXRT };

        string PrevCUR_CD = "";                            // getExrt 에서 사용하는 Memory 변수
        private string _exchangeRateSaveMode = "ADD";      // 환율 등록 처리 구분 (신규/갱신)

        // 환율처리 VO
        private string VoNTFC_DT { get; set; }
        private string VoCUR_CD { get; set; }
        private string VoBASI_EXRT { get; set; }
        private string VoSTMT_EXRT { get; set; }
        private string VoINVC_EXRT { get; set; }

        public InvoiceManageForm()
        {
            this.KeyPreview = true;                 // Form 에 KeyEvent 를 주기 위해 설정하는 변수
            InitializeComponent();
        }


        // Form Load 시 바로 수행        --> 190821 박현호
        //=================================================================================================================
        private void InvoiceCreateForm_Load(object sender, EventArgs e)
        {            
            setItemsToSearchArrangementPlaceCompanyComboBox();          // 모객업체 불러오기
            setItemsToSearchProductListComboBox();                                  // 상품 불러오기

            setCurrencyCodeComboBox();
            resetInputField();
            initializeExchangeRateVO();                                                  // 환율 VO 초기화
            SearchForeignExchageRateList();                                         // 당일자 인보이스 환율 검색
        }
        //=================================================================================================================






        private void initializeExchangeRateVO()
        {
            VoNTFC_DT = "";
            VoCUR_CD = "";
            VoBASI_EXRT = "";
            VoSTMT_EXRT = "";
            VoINVC_EXRT = "";
        }







        // Form 닫기  --> 190821 박현호
        //=================================================================================================================
        private void closeFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //=================================================================================================================







            
        // 모객업체 전체 불러오기  --> 20190821 - 박현호 (작업진행)
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









        // 상품 전체 불러오기 --> 20190821 - 박현호 (작업진행)
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









        // 통화코드 콤보박스 설정
        //=================================================================================================================
        private void setCurrencyCodeComboBox()
        {
            currencyCodeComboBox.Items.Clear();
            currencyCodeComboBox.Items.Clear();

            string query = "CALL SelectCurList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("통화코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CUR_CD = dataRow["CUR_CD"].ToString();
                string CUR_NM = dataRow["CUR_NM"].ToString();
                ComboBoxItem item = null;

                if (!CUR_CD.Equals("KRW")) { 
                    item = new ComboBoxItem(CUR_NM, CUR_CD);

                    currencyCodeComboBox.Items.Add(item);
                }
            }
        }
        //=================================================================================================================









        // 입력필드 초기화
        //=================================================================================================================
        private void resetInputField()
        {
            currencyCodeComboBox.SelectedIndex = 0;
            invoiceExrtTextBox.Text = "0";
        }
        //=================================================================================================================









        // 검색버튼 눌렀을 경우  --> 190821 박현호
        //=================================================================================================================
        // 검색버튼 눌렀을 경우 GROUP START POINT
        private void searchSettlementTargetListButton_Click(object sender, EventArgs e)
        {
            selectInvoiceTargetSummary();
            selectInvoiceTargetDetail();
        }


        //--------------------------------------------------------
        // 검색조건에 해당하는 예약건 가져오기 (상세)
        //--------------------------------------------------------
        public void selectInvoiceTargetDetail()
        {
            invoiceTargetDetailDataGridView.Rows.Clear();   // 채우기 전에 한번 비우기

            string message = "[Invoice]";
            ComboBoxItem companyItem = (ComboBoxItem)searchArrangementPlaceCompanyComboBox.SelectedItem;
            ComboBoxItem productItem = (ComboBoxItem)searchProductListComboBox.SelectedItem;

            string IN_CMPN_NO = companyItem.Value.ToString().Trim();
            string IN_PRDT_CNMB = productItem.Value.ToString().Trim();
            string IN_DPTR_FROM_DT = searchStartDatePicker.Text.ToString().Trim();
            string IN_DPTR_TO_DT = searchEndDatePicker.Text.ToString().Trim();
            string INVC_EXRT = "";                      

            string selectInvoiceTarget = string.Format("CALL SelectInvoiceForDetail('{0}','{1}','{2}','{3}','{4}')", IN_CMPN_NO, IN_PRDT_CNMB, IN_DPTR_FROM_DT, IN_DPTR_TO_DT, ' ');
            DataSet ds = DbHelper.SelectQuery(selectInvoiceTarget);
            int rowCount = ds.Tables[0].Rows.Count;
            if(rowCount == 0)
            {
                message += " 조회 결과가 존재하지 않습니다.";
                MessageBox.Show(message);
            }
            else
            {
                //message +=" "+ rowCount + " 건의 Data 가 조회 되었습니다.";
        
                // 검색 결과 하단 Grid 예 표현하기
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    string RSVT_NO = row["RSVT_NO"].ToString().Trim();
                    string DPTR_DT = row["DPTR_DT"].ToString().Trim().Substring(0,10);
                    string TOT_SALE_AMT = row["TOT_SALE_AMT"].ToString().Trim();
                    string PRDT_CNMB = row["PRDT_CNMB"].ToString().Trim();
                    string PRDT_NM = row["PRDT_NM"].ToString().Trim();
                    string CUR_SYBL = row["CUR_SYBL"].ToString().Trim();
                    string CUR_CD = row["CUR_CD"].ToString().Trim();
                    INVC_EXRT = row["INVC_EXRT"].ToString().Trim();
                    string INVC_AMT = row["INVC_AMT"].ToString().Trim().Substring(0, row["INVC_AMT"].ToString().Trim().LastIndexOf("."));
                    string CMPN_NO = row["CMPN_NO"].ToString().Trim();
                    string CMPN_NM = row["CMPN_NM"].ToString().Trim();
                    string EXRT = "";
                    string INVC_YN = row["INVC_YN"].ToString().Trim();
                    if (INVC_YN.Equals("Y"))
                    {
                        INVC_YN = "예";
                    }
                    else
                    {
                        INVC_YN = "아니오";
                    }
                    string[] rowDataArrForDetail = null;

                    if (INVC_EXRT.Equals("1.00"))
                    {
                        EXRT = "";
                        rowDataArrForDetail = new string[] { "false", RSVT_NO, PRDT_NM, CMPN_NM, CUR_SYBL + " (" + CUR_CD + ")", Utils.SetComma(TOT_SALE_AMT), EXRT, DPTR_DT, Utils.SetComma(INVC_AMT), "", CMPN_NO, PRDT_CNMB, CUR_CD, INVC_YN };
                    }
                    else
                    {
                        EXRT = INVC_EXRT;
                        rowDataArrForDetail = new string[] { "false", RSVT_NO, PRDT_NM, CMPN_NM, CUR_SYBL + " (" + CUR_CD + ")", Utils.SetComma(TOT_SALE_AMT), EXRT, DPTR_DT, Utils.SetComma(INVC_AMT), "", CMPN_NO, PRDT_CNMB, CUR_CD, INVC_YN };
                    }
                    invoiceTargetDetailDataGridView.Rows.Add(rowDataArrForDetail);
                }
                                               
                invoiceTargetDetailDataGridView.ClearSelection();
            }
            
        }




        //---------------------------------------------------------------------------------
        // 검색조건에 해당하는 예약건 가져오기 (요약)   --> 190821 박현호        
        //---------------------------------------------------------------------------------
        public void selectInvoiceTargetSummary()
        {
            invoiceTargetSummaryDataGridView.Rows.Clear();  // 채우기 전에 한번 비우기

            string message = "[Invoice]";
            ComboBoxItem companyItem = (ComboBoxItem)searchArrangementPlaceCompanyComboBox.SelectedItem;
            ComboBoxItem productItem = (ComboBoxItem)searchProductListComboBox.SelectedItem;

            string IN_CMPN_NO = companyItem.Value.ToString().Trim();
            string IN_PRDT_CNMB = productItem.Value.ToString().Trim();
            string IN_DPTR_FROM_DT = searchStartDatePicker.Text.ToString().Trim();
            string IN_DPTR_TO_DT = searchEndDatePicker.Text.ToString().Trim();

            string SelectInvoiceForSummary = string.Format("CALL SelectInvoiceForSummary('{0}','{1}','{2}','{3}')", IN_CMPN_NO, IN_PRDT_CNMB, IN_DPTR_FROM_DT, IN_DPTR_TO_DT);
            DataSet ds = DbHelper.SelectQuery(SelectInvoiceForSummary);
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                message += " 조회 결과가 존재하지 않습니다.";
            }
            else
            {
                message +=" "+rowCount + " 건의 Data 가 조회 되었습니다.";

                for (int i = 0; i < rowCount; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    string CMPN_NO = row["CMPN_NO"].ToString().Trim();
                    string CMPN_NM = row["CMPN_NM"].ToString().Trim();
                    string COUNT_CMPN = row["COUNT_CMPN"].ToString().Trim();

                    string[] rowDataForSummary = { (i + 1).ToString(), CMPN_NM, COUNT_CMPN };

                    invoiceTargetSummaryDataGridView.Rows.Add(rowDataForSummary);
                }
            }

            invoiceTargetSummaryDataGridView.ClearSelection();
            //MessageBox.Show(message);
        }
        // 검색버튼 눌렀을 경우 GROUP END POINT
        //=================================================================================================================










        // checkbox Control (invoice 대상 상세 내역 )   --> 190822 박현호
        //=================================================================================================================
        private void invoiceTargetDetailDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (invoiceTargetDetailDataGridView.SelectedCells.Count == 0)
            {
                return;
            }

            DataGridView gridView = (DataGridView)sender;
            if (gridView.SelectedCells[0] == invoiceTargetDetailDataGridView.Rows[invoiceTargetDetailDataGridView.SelectedCells[0].RowIndex].Cells["checkbox"])
            {
                bool checkedState = Boolean.Parse(gridView.SelectedCells[0].Value.ToString());
                gridView.SelectedCells[0].Value = !checkedState;
            }
        }
        //=================================================================================================================







        // 선택 Column 에 전체선택 CheckBox 추가 (invoice 대상 상세)   --> 190822 박현호
        //=================================================================================================================       
        // 전체선택 CheckBox GROUP START POINT
        private void invoiceTargetDetailDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            paintCheckBox(sender, e);
        }

        //----------------------------------------------------------
        // 전체선택 CheckBox 그리기
        //----------------------------------------------------------
        public void paintCheckBox(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);
                Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                int nChkBoxWidth = 15;
                int nChkBoxHeight = 15;
                int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
                int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

                pt.X += offsetx;
                pt.Y += offsety;

                CheckBox cb = new CheckBox();
                cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                cb.Location = pt;
                cb.CheckedChanged += new EventHandler(gvSheetListCheckBox_CheckedChanged);
                ((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }

        //----------------------------------------------------------
        // 전체선택 CheckBox Click 시
        //----------------------------------------------------------
        private void gvSheetListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in invoiceTargetDetailDataGridView.Rows)
            {
                r.Cells["checkbox"].Value = ((CheckBox)sender).Checked;
            }
        }
        // 전체선택 CheckBox GROUP END POINT
        //=================================================================================================================










        // 기준 환율 외 환율적용 CheckBox 동작     --> 190822 박현호
        //=================================================================================================================
        // 환율적용 CheckBox GROUP START POINT
        private void applyExrtCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            activateExrtItems();
        }

        public void activateExrtItems()
        {
            curCodeCombobox.Enabled = !curCodeCombobox.Enabled;
            foreignExchangeRateTextBox.Enabled = !foreignExchangeRateTextBox.Enabled;

            addComboBoxItems();
        }

        //----------------------------------------------------------
        // 환율 적용 화폐코드 ComboBox 채우기
        //----------------------------------------------------------
        public void addComboBoxItems()
        {
            string query = "SELECT CUR_CD, CUR_SYBL FROM TB_CUR_L";
            DataSet ds = DbHelper.SelectQuery(query);
            int rowCount = ds.Tables[0].Rows.Count;
            if(rowCount == 0)
            {
                MessageBox.Show("화폐 등록 정보가 존재하지 않습니다.", "경고");
                return;
            }

            curCodeCombobox.Items.Clear();

            for (int i=0; i<rowCount; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string CUR_CD = row["CUR_CD"].ToString().Trim();
                string CUR_SYBL = row["CUR_SYBL"].ToString().Trim();
                if (!CUR_CD.Equals("KRW"))
                {
                    curCodeCombobox.Items.Add(new ComboBoxItem(CUR_SYBL + " (" + CUR_CD + ")", CUR_CD));
                }
            }

            curCodeCombobox.SelectedIndex = 4;
        }


        /*----------------------------------------------------------
        환율 입력 TextBox 에서 Enter 를 누를 경우

        1. 입력한 Key 값이 Enter 라면 동작을 수행
        2. 이때 입력된 Text 가 없다면 환율값 입력 요구
        3. 입력한 값이 숫자라면 적용 Method 호출
        ----------------------------------------------------------*/
        private void foreignExchangeRateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string CUR_CODE = "";
            string CUR_SYBL = "";
            string exrtValueStr = "";
            if(e.KeyCode == Keys.Enter)
            {
                // 입력 환율값이 있는지 확인
                if (tb.Text.Length == 0)
                {
                    MessageBox.Show("적용 환율값을 입력해주세요");
                    return;
                }       
                
                // 입력 환율값 숫자값인지 확인
                try { 
                    int.Parse(tb.Text).ToString();
                }
                catch
                {
                    MessageBox.Show("환율값은 숫자만 입력할 수 있습니다.");
                    return;
                }
                
                //환율 적용 Parameter 값 가져오기!
                ComboBoxItem curItems = (ComboBoxItem)curCodeCombobox.SelectedItem;
                CUR_CODE = curItems.Value.ToString().Trim();
                CUR_SYBL = curItems.Text.Trim();                
                exrtValueStr = tb.Text.Trim();                

                applyNewExrt(CUR_CODE, CUR_SYBL, exrtValueStr); // invoice 상세 DataGridView 다시 그리기!
            }
        }



        /*
        ----------------------------------------------------------
        입력한 환율 값 적용!

        1. invoice 상세 DataGridView 에 출력된 Row 의 개수를 가져옴
        2. Row 의 개수만큼 반복
        3. Row 하나를 꺼내고 통화기호에 해당하는 Cell 의 값을 불러와 매개변수로 받은 통화기호와 비교
        4. 같다면 매개변수로 받은 입력 환율값으로 계산 진행
        5. DataGridView 갱신
        ---------------------------------------------------------- */

        public void applyNewExrt(string IN_CUR_CODE, string IN_CUR_SYBL, string IN_exrtValue)
        {
            int invoiceDetailRows = invoiceTargetDetailDataGridView.Rows.Count;         
            for(int i=0; i<invoiceDetailRows; i++)                                                        
            {
                DataGridViewRow row = invoiceTargetDetailDataGridView.Rows[i];          
                string CUR_SYBL = row.Cells[4].Value.ToString().Trim();   
                if (CUR_SYBL.Equals(IN_CUR_SYBL))  
                {                    
                    row.Cells[6].Value = IN_exrtValue;
                    row.Cells[8].Value = Utils.SetComma(int.Parse((int.Parse(Utils.removeComma(row.Cells[5].Value.ToString().Trim())) * double.Parse(IN_exrtValue)).ToString().Trim()));
                }
            }

            invoiceTargetDetailDataGridView.Update();
        }
        // 환율적용 CheckBox GROUP END POINT
        //=================================================================================================================









        // Invoice 생성 Button Click      --> 190822 박현호
        //=======================================================================================================================
        // Invoice 생성 GROUP START POINT
        private void executeButton_Click(object sender, EventArgs e)
        {
            createInvoiceDocument();            
        }

        /*
        ----------------------------------------------------------------------------
        Invoice 생성
        1. invoice 대상 Data 판별 (DataGridView 의 checkbox 상태가 True 인것)
        2. 같은 모객업체 끼리 Invoice 양식 Data 로 사용
        ----------------------------------------------------------------------------*/
        public void createInvoiceDocument()
        {                   
            List<string[]> rowDataList = new List<string[]>();
            List<string> queryList = new List<string>();
            int rowCount = invoiceTargetDetailDataGridView.Rows.Count;
            string message = "";
            string IN_INVC_CNMB = "";
            int trueCount = 0;

            // 체크된 Row 들의 Data 를 배열에 담아 List 에 Add
            for (int i=0; i<rowCount; i++)
            {
                DataGridViewRow row = invoiceTargetDetailDataGridView.Rows[i];
                string checkedState = row.Cells[0].Value.ToString().Trim();
                //MessageBox.Show(checkedState);
                if (checkedState.Equals("True"))
                {
                    trueCount++;
                    int columnCount = row.Cells.Count;                                        
                    string[] rowData = new string[columnCount-1];
                    for (int j=1; j<columnCount; j++)
                    {
                        rowData[j-1] = row.Cells[j].Value.ToString().Trim();
                        //MessageBox.Show(rowData[j-1]);
                    }
                    rowDataList.Add(rowData);
                }                
            }

            // check 된 checkBox 없을시 경고
            if (trueCount == 0)
            {
                MessageBox.Show("Invoice 를 생성할 예약건을 선택하세요.", "경고");
                return;
            }


            IN_INVC_CNMB = createInvoiceCNMB();     // Invocie 일련번호 채번
            int existedInvoiceCount = 0;

            for (int i=0; i<rowDataList.Count; i++)
            {                
                // DB 에 Data 저장!!!!!!!!!!!
                string[] rowDataArr = rowDataList[i];                
                // Invoice 가 이미 생성된 건이라면 건너뜀
                string INVC_YN = rowDataArr[12].ToString().Trim();
                if (INVC_YN.Equals("예"))
                {
                    existedInvoiceCount++;
                    continue;
                }

                string RSVT_NO = rowDataArr[0].ToString().Trim();            
                string PRDT_CNMB = rowDataArr[10].ToString().Trim();
                string CUR_CD = rowDataArr[11].ToString().Trim();
                string TKTR_CMPN_NO = rowDataArr[9].ToString().Trim();
                string UNAM_BAL = rowDataArr[4].ToString().Trim();
                string BASI_EXRT = rowDataArr[5].ToString().Trim();

                if (!validateExrt(BASI_EXRT))        // 환율값 유효성 검증 (숫자값이 아니라면 Invoice 생성 차단)
                {
                    MessageBox.Show("기준 환율값이 올바르지 않습니다!", "경고");
                    return;
                }
                else
                {
                    if (CUR_CD.Equals("KRW"))
                    {
                        BASI_EXRT = "1.0";
                    }                    
                }

                string CHNGE_AMT = rowDataArr[7].ToString().Trim();
                string DPTR_DT = rowDataArr[6].ToString().Trim();
                string REMK_CNTS = rowDataArr[8].ToString().Trim();
                string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

                string insertInvoice = string.Format("CALL InsertInvoice('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", RSVT_NO, IN_INVC_CNMB, PRDT_CNMB, CUR_CD, TKTR_CMPN_NO, Utils.removeComma(UNAM_BAL), BASI_EXRT, Utils.removeComma(CHNGE_AMT), DPTR_DT, REMK_CNTS, FRST_RGTR_ID);
                queryList.Add(insertInvoice);
                int CNMB = int.Parse(IN_INVC_CNMB);
                IN_INVC_CNMB = (++CNMB).ToString().Trim();
            }
           
            int insertTransactionResult = DbHelper.ExecuteNonQueryWithTransaction(queryList.ToArray());     // Procedure Collection 을 하나의 Transaction 에서 수행
            if(insertTransactionResult != -1)
            {
                message = "Invoice 생성 완료 (성공 : "+ queryList.Count + " / 선택 전체 : "+ trueCount + ")";
                selectInvoiceTargetDetail();        // Invoice 대상 상세목록 갱신!
                selectInvoiceTargetSummary();   // Invoice 대상 요약목록 갱신!!
            }
            else
            {
                if (existedInvoiceCount > 0)
                {
                    message = "이미 Invoice 가 생성된 Data 입니다.";
                }
                else
                {
                    message = "Invoice 생성 실패!\n관리자에게 문의하세요!";
                }                
            }

            MessageBox.Show(message);
  
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




        //-----------------------------------------------------
        // 환율값 유효성 검증
        // - 환율값이 숫자가 아니라면 Invoice 생성 차단!!
        //-----------------------------------------------------
        public bool validateExrt(string IN_BASI_EXRT)
        {
            bool validateResult = false;
            try
            {
                double exrt = double.Parse(IN_BASI_EXRT);                
                if(exrt.GetType().ToString().Equals("System.Int%") || exrt.GetType().ToString().Equals("System.Double"))
                {
                    validateResult = true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                if (IN_BASI_EXRT.Equals(""))
                {
                    validateResult = true;                    
                }
            }

            return validateResult;
        }
        // Invoice 생성 GROUP END POINT
        //=======================================================================================================================









        // Invoice 생성 취소        --> 190823 박현호
        //=======================================================================================================================
        // Invoice 생성 취소 GROUP START POINT
        private void bt_invoicdCreateCancel_Click(object sender, EventArgs e)
        {
            invoiceCreateCancel();
        }


        //-----------------------------------------------------------
        // check 된 Data 의 생성여부 판단 및 취소 진행
        //-----------------------------------------------------------
        public void invoiceCreateCancel()
        {
            int cancelCompleteCount = 0;
            string RSVT_NO = "";
            int checkedCount = 0;
            int notSuitableDataCount = 0;
            int rowCount = invoiceTargetDetailDataGridView.Rows.Count;
            for(int i=0; i<rowCount; i++)
            {
                DataGridViewRow row = invoiceTargetDetailDataGridView.Rows[i];
                string checkedState = row.Cells[0].Value.ToString().Trim();                
                if (checkedState.Equals("True"))
                {
                    checkedCount++;
                    
                    string INVC_YN = row.Cells[13].Value.ToString().Trim();
                    if (INVC_YN.Equals("예"))
                    {
                        RSVT_NO = row.Cells[1].Value.ToString().Trim();
                        string invoiceCancelQuery = "DELETE FROM TB_INVC_D WHERE RSVT_NO='" + RSVT_NO + "'";
                        string updateInvoiceCreateStateQuery = "UPDATE TB_RSVT_M SET INVC_YN='N' WHERE RSVT_NO='"+RSVT_NO+"'";
                        int transactionResult = DbHelper.ExecuteNonQueryWithTransaction(new string[] { invoiceCancelQuery, updateInvoiceCreateStateQuery });
                        if (transactionResult != -1)
                        {
                            cancelCompleteCount++;
                        }
                    }
                    else
                    {
                        notSuitableDataCount++;
                    }
                }          
            }
            if (checkedCount == 0)
            {
                MessageBox.Show("Data 를 선택해주세요.", "경고");
            }
            else
            {
                if (cancelCompleteCount > 0)
                {
                    MessageBox.Show("취소 작업 완료 (성공 : "+cancelCompleteCount+" / 선택 전체 : "+checkedCount.ToString().Trim()+")");
                    selectInvoiceTargetDetail();        // Invoice 대상 상세목록 갱신!
                    selectInvoiceTargetSummary();   // Invoice 대상 요약목록 갱신!!
                }
                else
                {
                    if (notSuitableDataCount == checkedCount)
                    {
                        MessageBox.Show("Invoice 가 생성되지 않은 Data 입니다.");
                    }
                    else
                    {
                        MessageBox.Show("취소 작업 실패\n관리자에게 문의하세요!", "경고");
                    }                                   
                }              
            }            
        }
        // Invoice 생성 취소 GROUP END POINT
        //=======================================================================================================================











        // 엑셀 다운로드      --> 190823 박현호
        //=======================================================================================================================
        private void excelDownloadButton_Click(object sender, EventArgs e)
        {
            excelDownload(sender, e);
        }

        // 엑셀다운로드 버튼 --> 190730 박현호
        public void excelDownload(object sender, EventArgs e)
        {
            // 그리드의 내용을 엑셀로 내보내기 한다.     
            SaveFileDialog saveFileDialog = new SaveFileDialog();                                                   // File 저장 대화상자 객체
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";                                                         // 저장할 File 형식 지정
            saveFileDialog.Title = "파일 내보내기";                                                                           // 대화상자 Title
            saveFileDialog.ShowDialog();                                                                                        // 대화상자 출력!

            if (saveFileDialog.FileName.Trim() == "")                                                                       // 지정한 FileName 이 없다면 진행 중단
                return;

            this.Cursor = Cursors.WaitCursor;                                                                                 // 마우스의 Cursor 모양을 대기모양으로 변경

            string filePath = saveFileDialog.FileName.Trim();                                                           // 입력한 File 저장 이름의 공백을 제거하여 보관
            string fileDirPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));      // 저장할 File 의 경로 위치를 보관 (DirectorySeparatorChar : Directory 구분 기호를 제공)
            if (Directory.Exists(fileDirPath))          // 저장할 경로값이 유효하다면
            {
                if (true == ExcelHelper.ExportExcel(filePath, invoiceTargetDetailDataGridView))                // ExcelFile 저장을 시작 (매개변수로 File 이름과, DataGridView 객체를 갖음)
                    MessageBox.Show(string.Format("{0}\r\n파일을 저장했습니다.", filePath));                // 성공시 저장 완료 MessageBox 출력
                else
                    MessageBox.Show("파일을 저장 할 수 없습니다.");                                                       // 실패시 저장 실패 MessageBox 출력
            }
            else
            {
                MessageBox.Show("잘못된 저장 경로입니다.");                                                               // 경로 유효성 없음 안내 출력
            }

            this.Cursor = Cursors.Default;                                                                                         // 마우스 커서 모양을 기본으로 원복
        }









        //===============================================================================================
        // 환율 입력 초기화 버튼 클릭
        //===============================================================================================
        private void resetExchangeRateButton_Click(object sender, EventArgs e)
        {
            resetInputField();
        }








        //===============================================================================================
        // 환율 조회
        //===============================================================================================
        private void SearchForeignExchageRateList()
        {
            foreignExchangeRateDataGridView.Rows.Clear();

            string NTFC_DT = notifyDateTimePicker.Value.ToString("yyyy-MM-dd");
            string CUR_CD = "";
            string query = string.Format("CALL SelectExrtNtfcList ('{0}', '{1}')", NTFC_DT, CUR_CD);

            string BASI_EXRT = "";
            string STMT_EXRT = "";
            string INVC_EXRT = "";

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("환율고시내역을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                NTFC_DT = dataRow["NTFC_DT"].ToString().Substring(0, 10);
                CUR_CD = dataRow["CUR_CD"].ToString();
                BASI_EXRT = dataRow["BASI_EXRT"].ToString();
                STMT_EXRT = dataRow["STMT_EXRT"].ToString();
                INVC_EXRT = dataRow["INVC_EXRT"].ToString();

                foreignExchangeRateDataGridView.Rows.Add(NTFC_DT, CUR_CD, BASI_EXRT, STMT_EXRT, INVC_EXRT);
            }

            foreignExchangeRateDataGridView.ClearSelection();
        }




        //============================================================================================
        // 환율 저장버튼 클릭
        //============================================================================================
        private void saveExchangeRateButton_Click(object sender, EventArgs e)
        {
                AddExrtNtfcItem();
        }

        //---------------------------------------------------------------------------------------------------------------------
        // 환율 등록 동작 Method
        //---------------------------------------------------------------------------------------------------------------------
        private void AddExrtNtfcItem()
        {
            VoNTFC_DT = notifyDateTimePicker.Value.ToString("yyyy-MM-dd");
            VoCUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);
            VoINVC_EXRT = invoiceExrtTextBox.Text.Trim();
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 기 등록환율 정보 검색
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------
            string query = string.Format("CALL SelectExrtNtfcItem ( '{0}', '{1}' )", VoNTFC_DT, VoCUR_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("해당 정보로 기존에 등록된 환율정보가 없습니다.\n환율정보를 등록합니다.");
                VoBASI_EXRT = "0";
                VoSTMT_EXRT = "0";
                query = string.Format("CALL InsertExrtNtfcItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", VoNTFC_DT, VoCUR_CD, VoBASI_EXRT, VoSTMT_EXRT, VoINVC_EXRT, FRST_RGTR_ID);
            }
            else
            {
                MessageBox.Show("해당 정보로 기존에 등록된 환율정보가 있습니다.\n기존 정보를 수정합니다.");
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                int rowCount = dataSet.Tables[0].Rows.Count;
                VoBASI_EXRT = dataRow["BASI_EXRT"].ToString().Trim();
                //VoSTMT_EXRT = dataRow["STMT_EXRT"].ToString().Trim();
                VoSTMT_EXRT = invoiceExrtTextBox.Text.Trim();
                query = string.Format("CALL UpdateExrtNtfcItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", VoNTFC_DT, VoCUR_CD, VoBASI_EXRT, VoSTMT_EXRT, VoINVC_EXRT, FRST_RGTR_ID);                
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("환율을 저장할 수 없습니다.");
                return;
            }

            SearchForeignExchageRateList();                 // 환율 목록 GridView reload
            selectInvoiceTargetDetail();                          // 인보이스 상세 대상 목록 GridView reload
        }




        //========================================================================================
        // 환율 삭제버튼 클릭
        //========================================================================================
        private void deleteExchangeRateButton_Click(object sender, EventArgs e)
        {
            deleteINVC_EXRT();
        }

        //------------------------------------------------------------------------------------------------
        // 환율 정보 삭제 동작
        //------------------------------------------------------------------------------------------------
        private void deleteINVC_EXRT()
        {
            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string NTFC_DT = notifyDateTimePicker.Value.ToString("yyyy-MM-dd");
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);

            string query = string.Format("CALL DeleteExrtNtfcItem ('{0}', '{1}')", NTFC_DT, CUR_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("환율을 삭제할 수 없습니다.");
                return;
            }
            SearchForeignExchageRateList();                     // 환율목록 GridView Reload
            selectInvoiceTargetDetail();                          // 인보이스 상세 대상 목록 GridView reload
        }







        //=================================================================================================
        // 환율정보 DataGridView 의 Cell Click Event Method
        //=================================================================================================
        private void foreignExchangeRateDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickTheINVC_EXRTCell();
        }

        //------------------------------------------------------------------------------------------------
        // 환율 정보 표시 DataGridView Cell Click 시 동작 Method
        //------------------------------------------------------------------------------------------------
        private void clickTheINVC_EXRTCell()
        {
            if (foreignExchangeRateDataGridView.SelectedRows.Count == 0)
                return;

            string NTFC_DT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.NTFC_DT].Value.ToString();
            string CUR_CD = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.CUR_CD].Value.ToString();
            string BASI_EXRT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.BASI_EXRT].Value.ToString();
            string STMT_EXRT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.STMT_EXRT].Value.ToString();
            string INVC_EXRT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.INVC_EXRT].Value.ToString();

            notifyDateTimePicker.Value = Utils.GetDateTimeFormatFromString(NTFC_DT);
            Utils.SelectComboBoxItemByValue(currencyCodeComboBox, CUR_CD);
            VoBASI_EXRT = BASI_EXRT;
            VoSTMT_EXRT = STMT_EXRT;
            VoINVC_EXRT = INVC_EXRT;
            invoiceExrtTextBox.Text = INVC_EXRT;
        }






        //=================================================================================================
        // Form KeyDown Event Method
        //=================================================================================================
        private void InvoiceManageForm_KeyDown(object sender, KeyEventArgs e)
        {
            downKeys(e);
        }

        //--------------------------------------------------------------------
        // keyPress 동작 Method
        //--------------------------------------------------------------------
        public void downKeys(KeyEventArgs e)
        {            
            if (e.KeyCode.ToString().Equals("F5"))
            {
                //MessageBox.Show("F5 번을 누르셨네요!");
                invoiceTargetDetailDataGridView.Rows.Clear();           // 인보이스 대상 상세 목록 초기화
                invoiceTargetSummaryDataGridView.Rows.Clear();      // 인보이사 대상 요약  목록초기화
            }
        }
              
    }
}
