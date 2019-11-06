using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;

namespace TripERP.SettlementMgt
{
    public partial class SettlementCancellationMgmt : Form
    {
        public SettlementCancellationMgmt()
        {
            InitializeComponent();
        }

        // 해당 Page 가 Load 되었을때 수행해야할 기초 작업
        private void SettlementCancellationMgmt_Load(object sender, EventArgs e)
        {
            // 그리드 스타일 초기화
            InitDataGridView();

            // 수배처 불러오기
            setItemsToSearchArrangementPlaceCompanyComboBox();

            // 상품 불러오기
            setItemsToSearchProductListComboBox();

            // form load 시 내역 바로 출력
            findAllSettlementList();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView1 = grdSettlementList;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
            dataGridView1.DoubleBuffered(true);
        }


        //==============================================================================================================
        // 모객업체 전체 불러오기  --> 20190726 - 박현호 (작업진행)
        //==============================================================================================================
        public void setItemsToSearchArrangementPlaceCompanyComboBox()
        {
            cboSearchArrangementPlaceCompany.Items.Clear();
            cboSearchArrangementPlaceCompany.Text = "";
            string[] COOP_CMPN_DVSN_CDArr = { "10", "11" };
            
            for(int i=0; i< COOP_CMPN_DVSN_CDArr.Length; i++) { 
                string COOP_CMPN_DVSN_CD = COOP_CMPN_DVSN_CDArr[i];
                string query = string.Format("CALL SelectCoopCmpnList ('{0}', '{1}')", COOP_CMPN_DVSN_CD, ' ');
                DataSet dataSet = DbHelper.SelectQuery(query);
                DataRowCollection dataRowList = dataSet.Tables[0].Rows;

                cboSearchArrangementPlaceCompany.Items.Add(new ComboBoxItem("전체", ' '));
                for (int j = 0; j< dataRowList.Count; j++)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[j];
                    string CMPN_NO = dataRow["CMPN_NO"].ToString();
                    string CMPN_NM = dataRow["CMPN_NM"].ToString();

                    cboSearchArrangementPlaceCompany.Items.Add(new ComboBoxItem(CMPN_NM, CMPN_NO));
                }
            }

            cboSearchArrangementPlaceCompany.SelectedIndex = 0;
        }


        //==============================================================================================================
        // 상품 전체 불러오기  --> 20190726 - 박현호 (작업진행)
        //==============================================================================================================
        public void setItemsToSearchProductListComboBox()
        {
            cboSearchProductList.Items.Clear();
            cboSearchProductList.Text = "";

            string query = string.Format("CALL SelectPrdtList");
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            cboSearchProductList.Items.Add(new ComboBoxItem("전체", ' '));
            for (int i = 0; i < dataRowList.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();
                cboSearchProductList.Items.Add(new ComboBoxItem(PRDT_NM, PRDT_CNMB));
            }

            cboSearchProductList.SelectedIndex = 0;
        }







        //==============================================================================================================
        // 검색 버튼 눌렀을때 동작   --> 20190726 - 박현호 (작업진행)
        //==============================================================================================================
        private void SelectSettlementListButton_Click(object sender, EventArgs e)
        {
            findAllSettlementList();
        }           

        // 정산내역 불러오기 --> 20190729 - 박현호 (작업진행)
        public void findAllSettlementList() {
            grdSettlementList.Rows.Clear();
            
            // 검색에 필요한 조건들을 변수화            
            string selectedSettlementState = cboSearchProcessDivision.Text.ToString().Trim();
            ComboBoxItem company = (ComboBoxItem)cboSearchArrangementPlaceCompany.SelectedItem;
            ComboBoxItem product = (ComboBoxItem)cboSearchProductList.SelectedItem;

            string IN_STMT_STTS_CD = "";
            string IN_CMPN_NO = "";
            string IN_PRDT_CNMB = "";

            if (selectedSettlementState.Equals("정산완료"))
            {
                IN_STMT_STTS_CD = "0";
            }
            else if (selectedSettlementState.Equals("정산취소"))
            {
                IN_STMT_STTS_CD = "2";
            }
            else if(selectedSettlementState.Equals("전체"))
            {
                IN_STMT_STTS_CD = "";
            }

            if (company != null)
            {
                IN_CMPN_NO = company.Value.ToString();
            }

            if (product != null)
            {
                IN_PRDT_CNMB = product.Value.ToString();
            }
            string IN_RSVT_NO = txtSearchReservationNo.Text.Trim();
            string IN_CUS_NM = txtSearchBookerName.Text.Trim();
            string settlement_from_date = dateSearchSettlementtFromDate.Value.ToString("yyyy-MM-dd 00:00:00");
            string settlement_to_date = dateSearchSettlementtToDate.Value.ToString("yyyy-MM-dd 23:59:59");            

            string settlementProcedure = string.Format("CALL SelectSettlementForSettlementCancel('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", IN_STMT_STTS_CD, IN_CMPN_NO, IN_PRDT_CNMB, IN_RSVT_NO, IN_CUS_NM, settlement_from_date, settlement_to_date);

            DataSet resultData = DbHelper.SelectQuery(settlementProcedure);
            DataRowCollection settlementList = resultData.Tables[0].Rows;

            if(settlementList.Count == 0)
            {
                MessageBox.Show("조회 Data 가 존재하지 않습니다.");
                return;
            }
            

            for (int i = 0; i < settlementList.Count; i++)
            {
                DataRow data = settlementList[i];
                string RSVT_NO = data["RSVT_NO"].ToString().Trim();                                                                                         // 예약번호
                string STMT_CNMB = data["STMT_CNMB"].ToString().Trim();                                                                              // 정산번호
                string UNPA_CNMB = data["UNPA_CNMB"].ToString().Trim();                                                                              // 미지급번호
                string STMT_DTM = data["STMT_DTM"].ToString().Trim();                                                                                   // 정산일시
                string TKTR_CMPN_NM = data["TKTR_CMPN_NM"].ToString().Trim();                                                                    // 모객업체
                string ARPL_CMPN_NM = data["ARPL_CMPN_NM"].ToString().Trim();                                                                    // 수배처
                string ACNT_TIT_NM = data["ACNT_TIT_NM"].ToString().Trim();                                                                         // 계정과목
                string STMT_UNPA_WON_AMT = Utils.SetComma(data["STMT_UNPA_WON_AMT"].ToString().Trim());                  // 정산 원화금액 (정산금액)
                string ERDF_PRLS_AMT = Utils.SetComma(data["ERDF_PRLS_AMT"].ToString().Trim());                                     // 환차손익
                string PRDT_NM = data["PRDT_NM"].ToString().Trim();                                                                                     // 상품명
                string CUST_NM = data["CUST_NM"].ToString().Trim();                                                                                      // 예약자
                string DPTR_DT = data["DPTR_DT"].ToString().Trim();                                                                                      //출발일

                string STMT_STTS_CD = data["STMT_STTS_CD"].ToString().Trim();                                                                   // 정산상태코드
                string STEM_BASI_EXRT = data["STEM_BASI_EXRT"].ToString().Trim();                                                           // 가정산환율
                string STEM_UNPA_WON_AMT = Utils.SetComma(data["STEM_UNPA_WON_AMT"].ToString().Trim());               // 가정산원화금액
                string STEM_UNPA_FRCR_AMT =Utils.SetComma(data["STEM_UNPA_FRCR_AMT"].ToString().Trim());               // 가정산외화금액
                string CUR_CD = data["CUR_CD"].ToString().Trim();                                                                                       // 통화코드
                string REMK_CNTS = data["REMK_CNTS"].ToString().Trim();                                                                             // 비고

                grdSettlementList.Rows.Add(false, RSVT_NO, STMT_CNMB, UNPA_CNMB, STMT_DTM, TKTR_CMPN_NM, ARPL_CMPN_NM, ACNT_TIT_NM, STMT_UNPA_WON_AMT, ERDF_PRLS_AMT, PRDT_NM, CUST_NM, DPTR_DT, STMT_STTS_CD, STEM_BASI_EXRT, STEM_UNPA_WON_AMT, STEM_UNPA_FRCR_AMT, CUR_CD, REMK_CNTS);

                if (i % 2 == 1)
                {
                    grdSettlementList.Rows[i].DefaultCellStyle.BackColor = Color.LemonChiffon;
                }
                grdSettlementList.ClearSelection();
                clearDetailBox();
            }
        }



        //==============================================================================================================
        // Grid 의 cell 을 Click 했을때
        //==============================================================================================================
        private void grdSettlementList_CellClick(object sender, DataGridViewCellEventArgs e)
            {  
                showClickedRowInfo(sender);
            }

        // Grid 에 출력된 Row 를 Click 햇을경우에 대한 Event 동작 Method  --> 20190728 - 박현호 (작업진행)
        public void showClickedRowInfo(object sender) {
            // 미조회시 RowHeader 눌렀을시 Exception 발생 방지
            if (grdSettlementList.SelectedCells.Count == 0)
            {
                return;
            }


            // Cell 안의 CheckBox 를 Click 했을경우 동작
           DataGridView gridView = (DataGridView)sender;
           if(gridView.SelectedCells[0] == grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["checkbox"])
           {
               //MessageBox.Show("체크박스가 있는 Cell 을 클릭하셨네요!!");
               bool checkedState = (bool)gridView.SelectedCells[0].Value;
               gridView.SelectedCells[0].Value = !checkedState;
           }
            
            string RSVT_NO = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["RSVT_NO"].Value.ToString().Trim();                                        // 예약번호
            string STMT_CNMB = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STMT_CNMB"].Value.ToString().Trim();                                // 정산번호
            string UNPA_CNMB = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["UNPA_CNMB"].Value.ToString().Trim();                                // 미지급번호
            string STMT_DTM = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STMT_DTM"].Value.ToString().Trim();                                    // 정산일시
            string TKTR_CMPN_NM = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["TKTR_CMPN_NM"].Value.ToString().Trim();                // 모객업체
            string ARPL_CMPN_NM = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["ARPL_CMPN_NM"].Value.ToString().Trim();                                       // 수배처 
            string ACNT_TIT_NM = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["ACNT_TIT_NM"].Value.ToString().Trim();                          // 계정과목
            string STMT_UNPA_WON_AMT = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STMT_UNPA_WON_AMT"].Value.ToString().Trim();         // 정산 원화금액 (정산금액)
            string ERDF_PRLS_AMT = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["ERDF_PRLS_AMT"].Value.ToString().Trim();                         // 환차손익
            string PRDT_NM = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["PRDT_NM"].Value.ToString().Trim();                                             // 상품명
            string CUST_NM = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["CUST_NM"].Value.ToString().Trim();                                         // 예약자
            string DPTR_DT = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["DPTR_DT"].Value.ToString().Trim();                                             //출발일

            string STMT_STTS_CD = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STMT_STTS_CD"].Value.ToString().Trim();                                        // 정산상태코드
            string STEM_BASI_EXRT = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STEM_BASI_EXRT"].Value.ToString().Trim();                                // 가정산환율
            string STEM_UNPA_WON_AMT = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STEM_UNPA_WON_AMT"].Value.ToString().Trim();               // 가정산원화금액
            string STEM_UNPA_FRCR_AMT = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STEM_UNPA_FRCR_AMT"].Value.ToString().Trim();               // 가정산외화금액
            string CUR_CD = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["CUR_CD"].Value.ToString().Trim();                                                             // 통화코드
            string REMK_CNTS = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["REMK_CNTS"].Value.ToString().Trim();                                               // 비고


            txtReservationNo.Text = RSVT_NO;                                                        // 예약번호
            TKTR_CMPN_NM_tb.Text = TKTR_CMPN_NM;
            txtArrangementPlaceCompanyName.Text = ARPL_CMPN_NM;                           // 수배처
            txtSettlementAmount.Text = STMT_UNPA_WON_AMT;                           // 정산원화금액                                                   
            if (STMT_STTS_CD.Equals("0"))                        // 정산상태코드
            {
                cboSettlementStatusCode.SelectedIndex = 0;
            }
            else
            {
                cboSettlementStatusCode.SelectedIndex = 1;
            }
            txtBookerName.Text = CUST_NM;                                                           // 고객명, 예약자명
            txtCurrencyCode.Text = CUR_CD;                                                          // 통화코드
            txtProfitLoss.Text = ERDF_PRLS_AMT;                                                      // 환차손익
            txtProductName.Text = PRDT_NM;                                                               // 상품명
            txtSettlementEstimationExchangeRate.Text = STEM_BASI_EXRT;                     // 가정산환율
            cboAccountCode.Text = ACNT_TIT_NM;                                                           //계정과목
            txtSettlementNo.Text = STMT_CNMB;                                                            // 정산일련번호
            txtSettlementEstimationWonAmount.Text = STEM_UNPA_WON_AMT;            // 가정산원화금액
            txtSettlementTimestamp.Text = STMT_DTM;                                                 // 정산일시
            txtUnpaidNo.Text = UNPA_CNMB;                                                               // 미지급번호
            txtSettlementEstimationForeignAmount.Text = STEM_UNPA_FRCR_AMT;     // 가정산외화금액
            txtSettlementMemo.Text = REMK_CNTS;                                                 // 비고

        }




        //==============================================================================================================
        // 상세보기 창 모두 초기화  --> 20190728 - 박현호 (작업진행)
        //==============================================================================================================
        public void clearDetailBox() {
            txtReservationNo.ResetText();                                                        // 예약번호
            TKTR_CMPN_NM_tb.ResetText();                                                    // 모객업체
            txtArrangementPlaceCompanyName.ResetText();                           // 수배처
            txtSettlementAmount.ResetText();                                                // 정산원화금액                                                   
            cboSettlementStatusCode.SelectedIndex = -1;                             // 정산상태코드
            txtBookerName.ResetText();                                                          // 고객명, 예약자명
            txtCurrencyCode.ResetText();                                                        // 통화코드
            txtProfitLoss.ResetText();                                                              // 환차손익
            txtProductName.ResetText();                                                         // 상품명
            txtSettlementEstimationExchangeRate.ResetText();                         // 가정산환율
            cboAccountCode.ResetText();                                                         //계정과목
            txtSettlementNo.ResetText();                                                        // 정산일련번호
            txtSettlementEstimationWonAmount.ResetText();                            // 가정산원화금액
            txtSettlementTimestamp.ResetText();                                             // 정산일시
            txtUnpaidNo.ResetText();                                                               // 미지급번호
            txtSettlementEstimationForeignAmount.ResetText();                       // 가정산외화금액
            txtSettlementMemo.ResetText();                                                  // 비고
        }





        //==============================================================================================================
        // 키보드 방향키 눌러서 Grid Row 선택 변경시 Detail 표시
        //==============================================================================================================
        private void grdSettlementList_KeyDown(object sender, KeyEventArgs e)
        {
            // 키보드 입력값
            string key = e.KeyData.ToString();

            // 현재 Row의 인덱스 
            int rowIndex = grdSettlementList.CurrentRow.Index;

            // 1번째 인덱스에서 ↑ 누르는거 방지
            if (rowIndex == 0 && key.Equals("Up"))
            {
                return;
            }

            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (grdSettlementList.Rows.Count == rowIndex + 1 && key.Equals("Down"))
            {
                return;
            }

            if (key.Equals("Up"))
            {
                showClickedRowInfo(sender);
            }

            else if (key.Equals("Down"))
            {
                showClickedRowInfo(sender);
            }
        }



        //==============================================================================================================
        // 저장 버튼 눌렀을때
        //==============================================================================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveSettlementInfo();
        }

        // 정산 내역 수정사항 반영
        public void saveSettlementInfo()
        {
            if(MessageBox.Show("수정하시겠습니까??", "정산처리 Data 수정", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            char settlementStateNum = ' ';
            String settlementStatusCodeItem =  (String)cboSettlementStatusCode.SelectedItem;
            if(settlementStatusCodeItem == null)
            {
                MessageBox.Show("저장할 Data 가 선택하세요.");

                return;
            }
            string settlementStateSelectedText = cboSettlementStatusCode.SelectedItem.ToString().Trim();
            if (settlementStateSelectedText.Equals("정산취소")){
                settlementStateNum = '2';
            }
            else
            {
                settlementStateNum = '0';
            }
            string RSVT_NO = grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["RSVT_NO"].Value.ToString().Trim();
            int STMT_CNMB = int.Parse(grdSettlementList.Rows[grdSettlementList.SelectedCells[0].RowIndex].Cells["STMT_CNMB"].Value.ToString().Trim());
            string REMK_CNTS = txtSettlementMemo.Text.ToString().Trim();
            string ACNT_ID = Global.loginInfo.ACNT_ID.ToString().Trim();

            string settlementUpdateProcedure = string.Format("CALL UpdateSettlementInfo('{0}','{1}','{2}','{3}','{4}')", RSVT_NO, STMT_CNMB, ACNT_ID, REMK_CNTS, settlementStateNum);
            int settlementUpdateResult = DbHelper.ExecuteNonQuery(settlementUpdateProcedure);

            string message = "";
            if (settlementUpdateResult == 0)
            {
                message = "정산내역 수정 완료!";
            }
            else
            {
                message = "정산내역 수정 실패! \n 관리자에게 문의하세요.";
            }
            MessageBox.Show(message);
            findAllSettlementList();
        }



        //==============================================================================================================
        // 정산취소 버튼 눌렀을때
        //==============================================================================================================
        private void btnCancelSettlement_Click(object sender, EventArgs e)
        {
            updateSettlementCancel();
        }

        // 정산 취소처리 진행 Method
        public void updateSettlementCancel()
        {
            if (MessageBox.Show("정산취소를 진행하시겠습니까??", "정산 처리 취소", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            int settlementCount = grdSettlementList.Rows.Count;
            int checkCount = 0;
            int resultCount = 0;

            for (int i = 0; i < settlementCount; i++)
            {
                DataGridViewRow row = grdSettlementList.Rows[i];
                string checkYN = row.Cells["checkbox"].Value.ToString().Trim();
                if (checkYN.Equals("True"))
                {
                    checkCount++;
                    string RSVT_NO = row.Cells["RSVT_NO"].Value.ToString().Trim();
                    int STMT_CNMB = int.Parse(row.Cells["STMT_CNMB"].Value.ToString().Trim());
                    string ACNT_ID = Global.loginInfo.ACNT_ID.Trim();

                    string settlementCancelProcedure = string.Format("CALL SettlementCancelBatchProc('{0}','{1}','{2}')", RSVT_NO, STMT_CNMB, ACNT_ID);
                    int settlementCancelResult = DbHelper.ExecuteNonQuery(settlementCancelProcedure);
                    if (settlementCancelResult == 0)
                    {
                        resultCount++;
                    }
                }
            }

            // check 된 Data 가 존재하는지 확인
            if(checkCount == 0)
            {
                MessageBox.Show("정산 취소 작업을 진행할 Data 를 선택하세요!");
                return;
            }


            if (checkCount == resultCount)
            {
                MessageBox.Show("정산취소 작업을 완료하였습니다.");
                findAllSettlementList();
            }
            else
            {
                MessageBox.Show(checkCount - resultCount + " 건의 정산취소 작업을 실패하였습니다 \n 관리자에게 문의하세요!");
            }
        }


        //==============================================================================================================
        // 닫기 버튼 눌렀을때 동작
        //==============================================================================================================
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // DataGridView Header 에 전체선택 기능을 위해 CheckBox 그리기  --> 190729 - 박현호 (작업진행)
        private void grdSettlementList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdSettlementListCheckBox_CheckedChanged);

                ((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }


        //==============================================================================================================
        // 전체선택,해제 효과 기능 Method --> 190729-박현호(작업진행)
        //==============================================================================================================
        private void grdSettlementListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in grdSettlementList.Rows)
            {
                r.Cells["checkbox"].Value = ((CheckBox)sender).Checked;
            }
        }



        //==============================================================================================================
        // Enter Key 누를시 검색!        --> 190823 박현호
        //==============================================================================================================
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                findAllSettlementList();
            }
            
        }




        //==============================================================================================================
        // 화면 전체 초기화    --> 190823 박현호
        //==============================================================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            resetPage();  
        }

        // 초기화
        public void resetPage()
        {
            grdSettlementList.Rows.Clear();
            cboSearchProcessDivision.SelectedIndex = 0;
            cboSearchArrangementPlaceCompany.SelectedIndex = 0;
            cboSearchProductList.SelectedIndex = 0;
            txtSearchReservationNo.ResetText();
            txtSearchBookerName.ResetText();
            txtReservationNo.ResetText();
            txtBookerName.ResetText();
            txtProductName.ResetText();
            txtSettlementNo.ResetText();
            txtUnpaidNo.ResetText();
            txtArrangementPlaceCompanyName.ResetText();
            txtCurrencyCode.ResetText();
            txtSettlementEstimationExchangeRate.ResetText();
            txtSettlementEstimationWonAmount.ResetText();
            txtSettlementEstimationForeignAmount.ResetText();
            txtSettlementAmount.ResetText();
            txtProfitLoss.ResetText();
            cboAccountCode.ResetText();
            txtSettlementTimestamp.ResetText();
            cboSettlementStatusCode.ResetText();
            txtSettlementMemo.ResetText();
        }        
    }
}