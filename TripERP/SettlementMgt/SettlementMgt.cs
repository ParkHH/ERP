using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using TripERP.Common;
using TripERP.SettlementMgt;
using TripERP.Login;

using MySql.Data.MySqlClient;




namespace TripERP.SettlementMgt
{
    public partial class SettlementMgt : Form
    {
        public SettlementMgt()
        {
            InitializeComponent();
        }


        SettlementProgressBar progressBarForm = null;


        // 해당 PageLoad 시 수행되야할 기본 작업 --> 20190723 - 박현호 (작업 진행)
        //================================================================================================================
        private void SettlementMgt_Load(object sender, EventArgs e)
        {
            InitDataGridView();
            setItemsToSearchTicketterCompanyComboBox();                         // 모객업체 전체 목록 가져오기.
            setItemsToSearchArrangementPlaceCompanyComboBox();                  // 수배처 전체 목록 가져오기.
            setItemsToSearchProductListComboBox();                              // 상품 전체 목록 가져오기.
        }
        //================================================================================================================


        //================================================================================================================
        // 그리드 스타일 초기화
        //================================================================================================================
        private void InitDataGridView()
        {
            DataGridView dataGridView = unpaidSummaryListDataGridView;
            //dataGridView.RowHeadersVisible = false;
            //dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView.MultiSelect = false;
            //dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView.EnableHeadersVisualStyles = false;
            //dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView.RowHeadersVisible = false;
            dataGridView.DoubleBuffered(true);

            dataGridView = unpaidDetailListDataGridView1;
            //dataGridView.RowHeadersVisible = false;
            //dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView.MultiSelect = false;
            //dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView.EnableHeadersVisualStyles = false;
            //dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView.RowHeadersVisible = false;
            dataGridView.DoubleBuffered(true);
        }



        // 정산실행버튼 클릭 --> 20190724 - 박현호 (작업 진행)
        //================================================================================================================
        private void executeButton_Click(object sender, EventArgs e)
        {
            //대체 일괄처리 (거래처 및 모객업체에 지급해야 하는 비용을 지급처리한다)
            //버튼이 클릭되면 진행률(Progresive Bar)를 표시한다.

            //Begin Transaction 선언
            //1. 사전 유효성 검증
            //1.1. 정산기준환율(txtForeignExchangeRate)이 입력되지 않은 경우 "정산기준환율을 입력하세요" msgbox


            if (foreignExchangeRateTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("정산기준환율은 필수 입력사항입니다.");
                return;
            }  


            //2. 대체 일괄 정산처리 (Loop 처리)
            if (foreignExchangeRateTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("정산환율은 필수 입력 사항입니다.");
                return;
            }

            if (MessageBox.Show("정산을 진행하시겠습니까?", "정산실행", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            showProgressBar();
            SettlementTransaction();
        }
        //================================================================================================================









        // 검색버튼 클릭  --> 20190723 - 박현호 (작업진행)
        //================================================================================================================
        private void searchSettlementTargetListButton_Click(object sender, EventArgs e)
        {
            searchDo();
        }


        public void searchDo()
        {
            if (foreignExchangeRateTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("정산환율은 필수 입력항목입니다.");
                foreignExchangeRateTextBox.Focus();
                return;
            }

            // 검색 조건 Data 변수화 (정산대상상세, 정산대상요약 공통)
            string IN_TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(searchTicketterCompanyComboBox);
            string IN_ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(searchCooperativeCompanyComboBox);
            string IN_PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(searchProductListComboBox);

            string IN_START_DPTR_DT = searchStartDatePicker.Text.Trim();
            string IN_END_DPTR_DT = searchEndDatePicker.Text.Trim();
            double IN_BASI_EXRT = 0.0;
            IN_BASI_EXRT = double.Parse(foreignExchangeRateTextBox.Text.Trim());

            setUnpaidDetailListDataGridView(IN_TKTR_CMPN_NO, IN_ARPL_CMPN_NO, IN_PRDT_CNMB, IN_START_DPTR_DT, IN_END_DPTR_DT, IN_BASI_EXRT);             // 정산대상상세 목록에 검색결과 출력
            setUnpaidSummaryListDataGridView(IN_TKTR_CMPN_NO, IN_ARPL_CMPN_NO, IN_PRDT_CNMB, IN_START_DPTR_DT, IN_END_DPTR_DT, IN_BASI_EXRT);            // 정산대상요약 목록에 검색결과 출력
        }
        //================================================================================================================









        // 환율입력창에서 Enter 눌렀을 경우            --> 190826 박현호  
        //================================================================================================================
        private void foreignExchangeRateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            validateExchangeValue(sender, e);
        }



        // 입력한 환율값 유효성 검증
        public void validateExchangeValue(object sender, KeyEventArgs e)
        {
            string insertExchangeValue = foreignExchangeRateTextBox.Text.Trim();
            try
            {                
                if (e.KeyCode == Keys.Enter)
                {
                    double exchangeValue = double.Parse(insertExchangeValue);     // 입력받은 값을 double 로 형변환 하여 입력한 값이 숫자인지 문자인지 판별
                    searchDo();
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show("기준환율값은 숫자만 입력할 수 있습니다.", "경고");          // 문자일경우 Exception 발생하여 경고 문구 띄움
            }     
        }
        //================================================================================================================









       
        // 정산대상상세 Page Grid 출력 Method --> 20190723 - 박현호 (작업진행)
        //================================================================================================================
        public void setUnpaidDetailListDataGridView(string IN_TKTR_CPNY_NO, string IN_ARPL_CMPN_NO, string IN_PRDT_CMNB, string IN_START_DPTR_DT, string IN_END_DPTR_DT, double IN_BASI_EXRT)
        {
            unpaidDetailListDataGridView1.Rows.Clear();

            // Stored Procedure 호출 (정산대상상세)
            string query2 = string.Format("CALL SelectUnpaidListForSettlementDetail('{0}','{1}','{2}','{3}','{4}','{5}')", IN_TKTR_CPNY_NO, IN_ARPL_CMPN_NO, IN_PRDT_CMNB, IN_START_DPTR_DT, IN_END_DPTR_DT, IN_BASI_EXRT);

            DataSet dataSet = DbHelper.SelectQuery(query2);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;
      
            MessageBox.Show(dataRowList.Count+" 건의 Data 가 조회되었습니다.");
           
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string RSVT_NO = dataRow["RSVT_NO"].ToString();                                                                         // 예약번호
                string DPTR_DT = dataRow["DPTR_DT"].ToString().Substring(0,10);                                                         // 출발일
                string CUST_NM = dataRow["CUST_NM"].ToString();                                                                         // 고객명
                int UNPA_CNMB = int.Parse(dataRow["UNPA_CNMB"].ToString());                                                             // 미지급일련번호
                string PRDT_NM = dataRow["PRDT_NM"].ToString();                                                                         // 상품명
                string TKTR_CMPN_NO = dataRow["TKTR_CMPN_NO"].ToString();                                                               // 모객업체!
                string TKTR_CMPN_NM = dataRow["TKTR_CMPN_NM"].ToString();                                                               // 모객업체!
                string ARPL_CMPN_NO = dataRow["ARPL_CMPN_NO"].ToString();                                                               // 수배처업체번호
                string ARPL_CMPN_NM = dataRow["ARPL_CMPN_NM"].ToString();                                                               // 수배처업체명
                string STMT_UNPA_WON_AMT = Utils.SetComma(dataRow["STMT_UNPA_WON_AMT"].ToString());                                     // 정산미지급원화금액
                string CUR_CD = dataRow["CUR_CD"].ToString();                                                                           // 통화코드
                double STMT_BASI_EXRT = double.Parse(dataRow["STMT_BASI_EXRT"].ToString());                                             // 정산환율
                string STEM_UNPA_FRCR_AMT = Utils.SetComma(dataRow["STEM_UNPA_FRCR_AMT"].ToString());                                   // 가정산미지급외화금액
                string PROFIT_LOSS = Utils.SetComma(dataRow["PROFIT_LOSS"].ToString());                                                 // 환차손익
                string STEM_UNPA_WON_AMT = Utils.SetComma(dataRow["STEM_UNPA_WON_AMT"].ToString());                                     // 가정산미지급원화금액
                double STEM_BASI_EXRT = double.Parse(dataRow["STEM_BASI_EXRT"].ToString());                                             // 가정산환율
                string REMK_CNTS = dataRow["REMK_CNTS"].ToString();

                unpaidDetailListDataGridView1.Rows.Add(false, RSVT_NO, DPTR_DT, CUST_NM, PRDT_NM, TKTR_CMPN_NM, ARPL_CMPN_NM, STMT_UNPA_WON_AMT, CUR_CD, STMT_BASI_EXRT, STEM_UNPA_FRCR_AMT, PROFIT_LOSS, STEM_UNPA_WON_AMT, STEM_BASI_EXRT, REMK_CNTS, UNPA_CNMB, ARPL_CMPN_NO);
    
            }


            unpaidDetailListDataGridView1.ClearSelection();
        }
        //================================================================================================================





        // 정산대상요약 Page Grid 출력  Method --> 20190723 - 박현호 (작업진행)
        //================================================================================================================
        public void setUnpaidSummaryListDataGridView(string IN_TKTR_CPNY_NO, string IN_ARPL_CMPN_NO, string IN_PRDT_CMNB, string IN_START_DPTR_DT, string IN_END_DPTR_DT,  double IN_BASI_EXRT)
        {
            unpaidSummaryListDataGridView.Rows.Clear();

            string query = string.Format("CALL SelectUnpaidListForSettlementSummary('{0}','{1}','{2}','{3}','{4}','{5}')", IN_TKTR_CPNY_NO, IN_ARPL_CMPN_NO, IN_PRDT_CMNB, IN_START_DPTR_DT, IN_END_DPTR_DT, IN_BASI_EXRT);

            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;
            for (int i = 0; i < dataRowList.Count; i++) {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string TKTR_CMPN_NM = dataRow["TKTR_CMPN_NM"].ToString();
                string ARPL_CMPN_NM = dataRow["ARPL_CMPN_NM"].ToString();
                int RSVT_COUNT = int.Parse(dataRow["RSVT_COUNT"].ToString());
                string SUM_SETTLE_WON_AMT = Utils.SetComma(dataRow["SETTLE_WON_AMT"].ToString());
                string SUM_STEM_UNPA_FRCR_AMT = Utils.SetComma(dataRow["STEM_UNPA_FRCR_AMT"].ToString());
                string CUR_CD = dataRow["CUR_CD"].ToString();
                double BASI_EXRT = double.Parse(dataRow["BASI_EXRT"].ToString());
                string SUM_STEM_UNPA_WON_AMT = Utils.SetComma(dataRow["STEM_UNPA_WON_AMT"].ToString());
                string SUM_PREDICT_PROFIT_LOSS = Utils.SetComma(dataRow["PREDICT_PROFIT_LOSS"].ToString());

                unpaidSummaryListDataGridView.Rows.Add(TKTR_CMPN_NM, ARPL_CMPN_NM, RSVT_COUNT, SUM_SETTLE_WON_AMT, SUM_STEM_UNPA_FRCR_AMT, CUR_CD, BASI_EXRT, SUM_STEM_UNPA_WON_AMT, SUM_PREDICT_PROFIT_LOSS);
            }

            unpaidSummaryListDataGridView.ClearSelection();
        }
        //================================================================================================================





        // 닫기 Button
        //================================================================================================================
        private void closeFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //================================================================================================================






        // 체크박스 관련 설정 --> 20190723 - 박현호 (작업진행)
        //================================================================================================================
        private void checkstateChange(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;
            int rowCount = unpaidDetailListDataGridView1.Rows.Count;
            if(rowCount == 0)
            {
                return;
            }
           
            string st = ((DataGridView)sender).CurrentCell.Value.ToString().Trim();
            if (st.Equals("True") || st.Equals("False"))
            {
                ((DataGridView)sender).CurrentCell.Value = !bool.Parse(st);
            }
            
        }

        //================================================================================================================
        // 모객업체 검색 콤보박스 초기화
        //================================================================================================================
        private void setItemsToSearchTicketterCompanyComboBox()
        {
            searchTicketterCompanyComboBox.Items.Clear();
            searchTicketterCompanyComboBox.Text = "";
            string[] COOP_CMPN_DVSN_CDArr = { "10", "11" };

            searchTicketterCompanyComboBox.Items.Add(new ComboBoxItem("전체", ' '));
            // 모객업체
            for (int i = 0; i < COOP_CMPN_DVSN_CDArr.Length; i++)
            {
                string CNSM_FILE_APLY_YN = COOP_CMPN_DVSN_CDArr[i];
                string query = string.Format("CALL SelectCoopCmpnList ('{0}', '{1}')", CNSM_FILE_APLY_YN, ' ');
                DataSet dataSet = DbHelper.SelectQuery(query);
                DataRowCollection dataRowList = dataSet.Tables[0].Rows;
                for (int j = 0; j < dataRowList.Count; j++)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[j];
                    string CMPN_NO = dataRow["CMPN_NO"].ToString();
                    string CMPN_NM = dataRow["CMPN_NM"].ToString();

                    searchTicketterCompanyComboBox.Items.Add(new ComboBoxItem(CMPN_NM, CMPN_NO));
                }
            }

            if (searchTicketterCompanyComboBox.Items.Count > 0)
                searchTicketterCompanyComboBox.SelectedIndex = 0;
        }


        //================================================================================================================
        // 수배처 전체 불러오기
        //================================================================================================================
        public void setItemsToSearchArrangementPlaceCompanyComboBox() {
            searchCooperativeCompanyComboBox.Items.Clear();
            searchCooperativeCompanyComboBox.Text = "";
            string[] COOP_CMPN_DVSN_CDArr = { "30" };

            searchCooperativeCompanyComboBox.Items.Add(new ComboBoxItem("전체",' '));
            // 수배처
            for (int i=0; i< COOP_CMPN_DVSN_CDArr.Length; i++) { 
                string CNSM_FILE_APLY_YN = COOP_CMPN_DVSN_CDArr[i];
                string query = string.Format("CALL SelectCoopCmpnList ('{0}', '{1}')", CNSM_FILE_APLY_YN, ' ');
                DataSet dataSet = DbHelper.SelectQuery(query);
                DataRowCollection dataRowList = dataSet.Tables[0].Rows;
                for(int j=0; j< dataRowList.Count; j++)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[j];
                    string CMPN_NO = dataRow["CMPN_NO"].ToString();
                    string CMPN_NM = dataRow["CMPN_NM"].ToString();

                    searchCooperativeCompanyComboBox.Items.Add(new ComboBoxItem(CMPN_NM, CMPN_NO));
                }
            }

            if (searchCooperativeCompanyComboBox.Items.Count > 0)
                searchCooperativeCompanyComboBox.SelectedIndex = 0;
        }







        // 상품 전체 불러오기 --> 20190723 - 박현호 (작업진행)
        //================================================================================================================
        public void setItemsToSearchProductListComboBox()
        {
            searchProductListComboBox.Items.Clear();
            searchProductListComboBox.Text = "";

            searchProductListComboBox.Items.Add(new ComboBoxItem("전체", ' '));
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
        //================================================================================================================







        // ProgressBar 출력 --> 20190724 - 박현호 (작업 진행)
        //================================================================================================================
        public void showProgressBar()
        {
             progressBarForm = new SettlementProgressBar(this);
            //progressBarForm.Parent = this;
            progressBarForm.StartPosition = FormStartPosition.CenterScreen;
            progressBarForm.Show();
        }
        //================================================================================================================






        // 정산작업 Transaction --> 20190724 - 박현호 (작업 진행)
        //================================================================================================================
        public void SettlementTransaction()
        {
            try
            {
                //▶SelectUnpaidListForSettlementDetail Procedure 를 실행하기 위해 필요한 변수 Setting
                //=======================================================================================
                ComboBoxItem ARPL_CPNY_ComboBoxItem = (ComboBoxItem)searchTicketterCompanyComboBox.SelectedItem;
                ComboBoxItem IN_PRDT_ComboBoxItem = (ComboBoxItem)searchProductListComboBox.SelectedItem;
                string IN_ARPL_CPNY_NO = "";

                if (ARPL_CPNY_ComboBoxItem != null)
                {
                        IN_ARPL_CPNY_NO = ARPL_CPNY_ComboBoxItem.Value.ToString();
                }

                string IN_PRDT_CNMB = "";

                if (IN_PRDT_ComboBoxItem != null)
                {
                    IN_PRDT_CNMB = IN_PRDT_ComboBoxItem.Value.ToString();
                }

                string IN_START_DPTR_DT = searchStartDatePicker.Text.Trim();
                string IN_END_DPTR_DT = searchEndDatePicker.Text.Trim();
                double IN_BASI_EXRT = 0.0;

                if (foreignExchangeRateTextBox.Text.Trim().Equals(""))
                {
                    MessageBox.Show("화면 하단 정산환율값 입력란에 환율 값을 입력하세요");
                    return;
                }
                else
                {
                    IN_BASI_EXRT = double.Parse(foreignExchangeRateTextBox.Text.Trim());
                }

                progressBarForm.progressBar.Value += 30;
                //=======================================================================================





                //▶ 정산처리내역 등록 Procedure 실행
                //=======================================================================================
                try
                {
                    // Detail DataGridView 의 ARPL_CMPN_NO Column 의 값을 가져온다.
                    string[] ARPL_CMPN_NOArr = null;
                    int rowCount = unpaidDetailListDataGridView1.Rows.Count;
                    if(rowCount == 0)
                    {
                        MessageBox.Show("정산 실행 대상 Data 가 존재하지 않습니다.");
                        return;
                    }

                    ARPL_CMPN_NOArr = new string[rowCount];
                    for (int i=0; i<rowCount; i++)
                    {
                        DataGridViewRow row = unpaidDetailListDataGridView1.Rows[i];
                        ARPL_CMPN_NOArr[i] = row.Cells[16].ToString().Trim();
                    }

                    string procedureForUnpaidToPaid = string.Format("CALL SettlementBatchProc('{0}','{1}','{2}'"/*,'{3}'", '{4}'"*/+ ")", /*, IN_PRDT_CNMB, */IN_START_DPTR_DT, IN_END_DPTR_DT, IN_BASI_EXRT);                   
                    int result = DbHelper.ExecuteNonQuery(procedureForUnpaidToPaid);
                    if (result == 0)
                    {
                        progressBarForm.progressBar.Value += 30;
                        progressBarForm.progressBar.Value += 10;
                        MessageBox.Show("정산처리진행을 완료하였습니다!");
                        unpaidSummaryListDataGridView.Rows.Clear();
                        unpaidDetailListDataGridView1.Rows.Clear();
                        foreignExchangeRateTextBox.ResetText();                        
                    }
                    else
                    {
                        MessageBox.Show("System 오류\n정산 작업에 실패하였습니다. \n관리자에게 문의하세요!");
                    }
                }catch(MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                //=======================================================================================

            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
   
            }
            finally
            {
                progressBarForm.Close();
            }
        }
        //================================================================================================================






        // 예약목록 상태조회 RSVT_NO Data 변형 Method
        //================================================================================================================
        public string make_RSVT_NO(string[] date) {
            string RSVT_NO = null;
            for (int i = 0; i < date.Length; i++)
            {
                RSVT_NO += date[i];
            }

            return RSVT_NO += "01"; 
        }
        //================================================================================================================








        // 엑셀다운로드 버튼 --> 190730 박현호
        //================================================================================================================
        private void excelDownloadButton_Click(object sender, EventArgs e)
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
                if (true == ExcelHelper.ExportExcel(filePath, unpaidDetailListDataGridView1))                // ExcelFile 저장을 시작 (매개변수로 File 이름과, DataGridView 객체를 갖음)
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
        //================================================================================================================






        // 인쇄버튼 클릭
        private void printOutButton_Click(object sender, EventArgs e)
        {
            //그리드의 내용을 인쇄한다.
            //타이틀: 정산대상목록
            //기준일자: 당일
            //정산대상기간: 출발일자FROM~TO
            //합계출력 필요

            PrintPreviewDialog printPreview = new PrintPreviewDialog();
            this.Cursor = Cursors.WaitCursor;
            printPreview.Width = this.Width / 3;
            printPreview.Height = this.Height*2/3;
            
            printPreview.ShowDialog();

        }








        // 전체선택 CheckBox EventHandler Method  --> 190730 박현호
        //================================================================================================================
        private void gvSheetListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in unpaidDetailListDataGridView1.Rows)
            {
                r.Cells["chkBox"].Value = ((CheckBox)sender).Checked;
            }
        }

        private void unpaidDetailListDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void unpaidDetailListDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //================================================================================================================       
    }
}
