using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;
using TripERP.Data;

namespace TripERP.ProductMgt
{
    public partial class ProductPromotionMgt : Form
    {
        public ProductPromotionMgt()
        {
            InitializeComponent();
        }

        bool flagInsertOrUpdate = false;
        private enum eProductPromotionListDataGridView {PRDT_PRMO_CD, PRMO_NM, PRDT_CNMB, PRDT_NM, ADLT_PRMO_DSCT_RT, CHLD_PRMO_DSCT_RT, INFN_PRMO_DSCT_RT, ADLT_PRMO_DSCT_AMT, CHLD_PRMO_DSCT_AMT, INFN_PRMO_DSCT_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN}
        Pagination pagination = null;       // Paging 처리 관련 설정 Class

        // 프로모션 관리 Page Load 되었을때 동작!!     - 박현호
        //*************************************************************************************************************************************************
        private void ProductPromotionMgt_Load(object sender, EventArgs e)
        {   
            // 모든 상품명 가져오는 Method 호출!
            bringProductAll();
           
            // 사용여부 한글 속성 한글화
            bringKoreanYN();

            pagination = new Pagination(promotionListDataGridView);
            pagination.DataGridViewHeaderSetting();
            //getPromotionList();
        }
        //*************************************************************************************************************************************************






        // Paging Button Click EventHandler Method    - 박현호
        //*************************************************************************************************************************************************
        private void toolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton ToolStripButton = ((ToolStripButton)sender);

                //Determining the current page
                if (ToolStripButton == btnBackward)
                    pagination.CurrentPage--;
                else if (ToolStripButton == btnForward)
                    pagination.CurrentPage++;
                else if (ToolStripButton == btnLast)
                    pagination.CurrentPage = pagination.PagesCount;
                else if (ToolStripButton == btnFirst)
                    pagination.CurrentPage = 1;
                else
                    pagination.CurrentPage = Convert.ToInt32(ToolStripButton.Text, CultureInfo.InvariantCulture);

                if (pagination.CurrentPage < 1)
                    pagination.CurrentPage = 1;
                else if (pagination.CurrentPage > pagination.PagesCount)
                    pagination.CurrentPage = pagination.PagesCount;

                //Rebind the Datagridview with the data.
                pagination.RebindGridForPageChange();

                //Change the pagiantions buttons according to page number
                pagination.RefreshPagination(btnFirst, btnBackward, btnForward, btnLast, toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5);
            }
            catch (Exception) { }
        }
        //*************************************************************************************************************************************************





        // GridView Data 출력!!!   - 박현호
        //*************************************************************************************************************************************************
        private void getPromotionList()
        {
            //프로모션 정보를 가져오는 Method 호출!!
            pagination.Baselist = bringPromotionAll();
            pagination.CurrentPage = 1;
            pagination.PagesCount = Convert.ToInt32(Math.Ceiling(pagination.Baselist.Count * 1.0 / pagination.PageRows));

            // 페이징 관련 설정 
            pagination.RefreshPagination(btnFirst, btnBackward, btnForward, btnLast, toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5);
            pagination.RebindGridForPageChange();
        }
        //*************************************************************************************************************************************************







        // USE_YN 한글값 가져오기    - 박현호
        //*************************************************************************************************************************************************
        private void bringKoreanYN()
        {
            UseYnComboBox.Items.Clear();
            UseYnComboBox.Text = "";

            string query = "CALL SelectCommoncodeList ('YN')";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용여부 공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string USE_YN = dataRow["CD_VLID_VAL"].ToString();
                string USE_YN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(USE_YN_NM, USE_YN);
                UseYnComboBox.Items.Add(item);
            }

            UseYnComboBox.SelectedIndex = -1;
        }
        //*************************************************************************************************************************************************







        // 모든 상품명 가져오기   - 박현호
        //*************************************************************************************************************************************************
        private void bringProductAll()
        {
            // 상품명
            ProductNameComboBox.Items.Clear();
            ProductNameComboBox.Text = "";

            string query = "CALL SelectAllProductList";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품기본정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 상품일련번호와 상품명을 콤보에 설정
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_NM, PRDT_CNMB);
                ProductNameComboBox.Items.Add(item);
            }

            ProductNameComboBox.SelectedIndex = -1;
        }
        //*************************************************************************************************************************************************







        // 프로모션 전체 목록들 가져와 Table 에 출력!   - 박현호
        //*************************************************************************************************************************************************
        private BindingList<Promotion> bringPromotionAll() {
            BindingList<Promotion> promotionList = new BindingList<Promotion>();
            string PRMO_NM = SearchPromotionNameTextBox.Text.Trim();
            string query = string.Format("CALL SelectPromotionList ('{0}')", PRMO_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                /***********************************************************************************
                // Paging 처리 적용 이전 Code
                // 모든 프로모션 정보를 Table 에서 꺼냄
                string PRDT_PRMO_CD = dataRow["PRDT_PRMO_CD"].ToString();
                
                if (dataRow["PRMO_NM"].ToString() != "")
                {
                    PRMO_NM = dataRow["PRMO_NM"].ToString();
                }
                else
                {
                    PRMO_NM = "-";
                }
                string ADLT_PRMO_DSCT_RT = dataRow["ADLT_PRMO_DSCT_RT"].ToString();
                string CHLD_PRMO_DSCT_RT = dataRow["CHLD_PRMO_DSCT_RT"].ToString();
                string INFN_PRMO_DSCT_RT = dataRow["INFN_PRMO_DSCT_RT"].ToString();
                string ADLT_PRMO_DSCT_AMT = Utils.SetComma(dataRow["ADLT_PRMO_DSCT_AMT"].ToString());
                string CHLD_PRMO_DSCT_AMT = Utils.SetComma(dataRow["CHLD_PRMO_DSCT_AMT"].ToString());
                string INFN_PRMO_DSCT_AMT = Utils.SetComma(dataRow["INFN_PRMO_DSCT_AMT"].ToString());
                string APLY_LNCH_DT = dataRow["APLY_LNCH_DT"].ToString();
                string APLY_END_DT = dataRow["APLY_END_DT"].ToString();
                string USE_YN = "";
                if (dataRow["USE_YN"].ToString() == "Y")
                {
                    USE_YN = "예";C:\Users\ParkHyeonho\Desktop\work\IDE\workspace\visualStudio_workspace\TripERP\ProductMgt\ProductPromotionMgt.cs
                }
                else
                {
                    USE_YN = "아니요";
                }
                ***********************************************************************************/

                try {
                    string selectProductNamequery = "SELECT PRDT_NM FROM TB_PRDT_M WHERE PRDT_CNMB=" + PRDT_CNMB;
                    DataSet dataSet2 = DbHelper.SelectQuery(selectProductNamequery);
                    DataRow dataRow2 = dataSet2.Tables[0].Rows[0];
                    //string PRDT_NM = dataRow2["PRDT_NM"].ToString();
                    string USE_YN = dataRow["USE_YN"].ToString();                                               // 사용여부
                    if (USE_YN.Trim().Equals("Y"))
                    {
                        USE_YN = "예";
                    }
                    else
                    {
                        USE_YN = "아니요";
                    }

                    promotionList.Add(new Promotion()
                    {
                        PRDT_PRMO_CD = dataRow["PRDT_PRMO_CD"].ToString(),
                        PRDT_CNMB = dataRow["PRDT_CNMB"].ToString(),
                        PRMO_NM = dataRow["PRMO_NM"].ToString(),
                        ADLT_PRMO_DSCT_RT = dataRow["ADLT_PRMO_DSCT_RT"].ToString(),
                        CHLD_PRMO_DSCT_RT = dataRow["CHLD_PRMO_DSCT_RT"].ToString(),
                        INFN_PRMO_DSCT_RT = dataRow["INFN_PRMO_DSCT_RT"].ToString(),
                        ADLT_PRMO_DSCT_AMT = Utils.SetComma(dataRow["ADLT_PRMO_DSCT_AMT"].ToString()),
                        CHLD_PRMO_DSCT_AMT = Utils.SetComma(dataRow["CHLD_PRMO_DSCT_AMT"].ToString()),
                        INFN_PRMO_DSCT_AMT = Utils.SetComma(dataRow["INFN_PRMO_DSCT_AMT"].ToString()),
                        APLY_LNCH_DT = dataRow["APLY_LNCH_DT"].ToString().Substring(0, 10),
                        APLY_END_DT = dataRow["APLY_END_DT"].ToString().Substring(0, 10),
                        USE_YN = USE_YN,
                        PRDT_NM = dataRow2["PRDT_NM"].ToString()
                    });

                    //promotionListDataGridView.Rows.Add(PRDT_PRMO_CD, PRMO_NM, PRDT_CNMB, PRDT_NM, ADLT_PRMO_DSCT_RT, CHLD_PRMO_DSCT_RT, INFN_PRMO_DSCT_RT, ADLT_PRMO_DSCT_AMT, CHLD_PRMO_DSCT_AMT, INFN_PRMO_DSCT_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN);
                    //promotionListDataGridView.ClearSelection(); 

                } catch (Exception e) {
                    Console.WriteLine("ProductPromotionMgt : bringPromotionAll() : 등록되지 않은 상품일련번호의 상품이 존재합니다.\n" + e.Message);
                }
            }
            return promotionList;
        }
        //**********************************************************************************************************************************************************







        // 초기화, 저장, 삭제, 닫기 Button Click Event Handler    - 박현호
        //**********************************************************************************************************************************************************
        public void ClickedButton(object sender, EventArgs e)
        {
            // 검색버튼 눌렀을때 동작   - 박현호
            //=================================
            if (sender == searchPromotionListButton) {
                promotionListDataGridView.Rows.Clear();
                //bringPromotionAll();
                getPromotionList();
            }
            //=================================

            // 초기화 Button Click 시 동작!!   - 박현호
            //=================================
            if (sender == resetButton)
            {
                 clearDetail();
            }
            //=================================

            // 저장 Button Click 시 Insert, Update 동작!!   - 박현호
            //===============================================================================
            else if (sender == saveButton)
            {
                if (!flagInsertOrUpdate)
                {
                    // Insert
                    if (MessageBox.Show("Data 를 등록하시겠습니까?", "프로모션 Data 등록", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                    // 입력값 유효성 검증
                    if (CheckRequireItems(flagInsertOrUpdate) == false)
                    {
                        return;
                    }

                    string query = "SELECT PRDT_PRMO_CD FROM TB_PRDT_PRMO_BASC_M ORDER BY PRDT_PRMO_CD DESC LIMIT 1";
                    DataSet dataSet = DbHelper.SelectQuery(query);
                    if (dataSet.Tables[0].Rows.Count == 0)
                    {
                        // 처음 등록하는 Promotion 일때 프로모션 Code 지정해주어야함
                    }
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    int PRDT_PRMO_CD = int.Parse(dataRow["PRDT_PRMO_CD"].ToString()) + 1;                           // 프로모션 코드
                    int PRDT_CNMB = int.Parse(productConsecutiveNumberTextBox.Text.Trim());
                    string PRMO_NM = promotionNameTextBox.Text.Trim();                                                           // 프로모션 이름
                    double ADLT_PRMO_DSCT_RT = double.Parse(adultPromotionRateTextBox.Text.Trim());             // 성인할인율
                    double CHLD_PRMO_DSCT_RT = double.Parse(childPromotionRateTextBox.Text.Trim());             // 소아 할인율
                    double INFN_PRMO_DSCT_RT = double.Parse(infantPromotionRateTextBox.Text.Trim());            // 유아할인율
                    double ADLT_PRMO_DSCT_AMT = double.Parse(adultPromotionAmountTextBox.Text.Trim());      // 성인할인금액
                    double CHLD_PRMO_DSCT_AMT = double.Parse(childPromotionAmountTextBox.Text.Trim());      // 소아할인금액
                    double INFN_PRMO_DSCT_AMT = double.Parse(infantPromotionAmountTextBox.Text.Trim());     // 유아할인금액
                    string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Trim().Substring(0,10);                       // 적용개시일자
                    string APLY_END_DT = ApplyEndDateTimePicker.Text.Trim().Substring(0, 10);                            // 적용종료일자
                    string USE_YN = "";                                                                                                                 // 사용여부
                    if (UseYnComboBox.Text.Trim() == "예")
                    {
                        USE_YN = "Y";
                    }
                    else
                    {
                        USE_YN = "N";
                    }
                    string FRST_RGTR_ID = Global.loginInfo.ACNT_ID.Trim();                                                   // 최초등록자ID


                    string insertQuery = string.Format("CALL InsertPromotion('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", PRDT_PRMO_CD, PRDT_CNMB, PRMO_NM, ADLT_PRMO_DSCT_RT, CHLD_PRMO_DSCT_RT, INFN_PRMO_DSCT_RT, ADLT_PRMO_DSCT_AMT, CHLD_PRMO_DSCT_AMT, INFN_PRMO_DSCT_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
                    int insertResult = DbHelper.ExecuteNonQuery(insertQuery);
                    if (insertResult != -1)
                    {
                        promotionListDataGridView.Rows.Clear();
                        clearDetail();
                        //bringPromotionAll();
                        getPromotionList();
                    }
                    else
                    {
                        MessageBox.Show("프로모션 상품 등록에 실패하였습니다.");
                    }
                }
                else
                {
                    // Update 동작!!
                    if (MessageBox.Show("Data 를 수정하시겠습니까?", "프로모션 Data 수정", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                    // 입력값 유효성 검증   
                    if (CheckRequireItems(flagInsertOrUpdate) == false)
                    {
                        return;
                    }
                    int PRDT_PRMO_CD = int.Parse(promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.PRDT_PRMO_CD].Value.ToString());                                        // 프로모션 코드
                    int PRDT_CNMB = int.Parse(productConsecutiveNumberTextBox.Text.Trim());                           // 상품일련번호
                    int BEFORE_PRDT_CNMB = int.Parse(promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.PRDT_CNMB].Value.ToString());
                    string PRMO_NM = promotionNameTextBox.Text.Trim();                                                           // 프로모션 이름
                    double ADLT_PRMO_DSCT_RT = double.Parse(adultPromotionRateTextBox.Text.Trim());             // 성인할인율
                    double CHLD_PRMO_DSCT_RT = double.Parse(childPromotionRateTextBox.Text.Trim());             // 소아 할인율
                    double INFN_PRMO_DSCT_RT = double.Parse(infantPromotionRateTextBox.Text.Trim());            // 유아할인율
                    double ADLT_PRMO_DSCT_AMT = double.Parse(adultPromotionAmountTextBox.Text.Trim());      // 성인할인금액
                    double CHLD_PRMO_DSCT_AMT = double.Parse(childPromotionAmountTextBox.Text.Trim());      // 소아할인금액
                    double INFN_PRMO_DSCT_AMT = double.Parse(infantPromotionAmountTextBox.Text.Trim());     // 유아할인금액
                    string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Trim();                                                              // 적용개시일자
                    string APLY_END_DT = ApplyEndDateTimePicker.Text.Trim();                                                                   // 적용종료일자
                    string USE_YN = "";                                                                                                                 // 사용여부
                    if (UseYnComboBox.Text.Trim() == "예")
                    {
                        USE_YN = "Y";
                    }
                    else
                    {
                        USE_YN = "N";
                    }
                    string FINL_MDFR_ID = Global.loginInfo.ACNT_ID.Trim();                                                   // 최종변경자ID


                    string updateQuery = string.Format("CALL UpdatePromotionInfo('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", PRDT_CNMB, PRMO_NM, ADLT_PRMO_DSCT_RT, CHLD_PRMO_DSCT_RT, INFN_PRMO_DSCT_RT, ADLT_PRMO_DSCT_AMT, CHLD_PRMO_DSCT_AMT, INFN_PRMO_DSCT_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FINL_MDFR_ID, PRDT_PRMO_CD, BEFORE_PRDT_CNMB);
                    /*
                    "UPDATE TB_PRDT_PRMO_BASC_M SET " +
                        "PRDT_CNMB=" + PRDT_CNMB + ", PRMO_NM='" + PRMO_NM + "', ADLT_PRMO_DSCT_RT=" + ADLT_PRMO_DSCT_RT + ", CHLD_PRMO_DSCT_RT=" + CHLD_PRMO_DSCT_RT +
                        ", INFN_PRMO_DSCT_RT=" + INFN_PRMO_DSCT_RT + ", ADLT_PRMO_DSCT_AMT=" + ADLT_PRMO_DSCT_AMT + ", CHLD_PRMO_DSCT_AMT=" + CHLD_PRMO_DSCT_AMT +
                        ", INFN_PRMO_DSCT_AMT=" + INFN_PRMO_DSCT_AMT + ", APLY_LNCH_DT='" + APLY_LNCH_DT + "', APLY_END_DT='" + APLY_END_DT + "', USE_YN='" + USE_YN + "', FINL_MDFR_ID='" + FINL_MDFR_ID +
                        "' WHERE PRDT_PRMO_CD=" + PRDT_PRMO_CD + " AND PRDT_CNMB=" + BEFORE_PRDT_CNMB;
                   */
                    int result = DbHelper.ExecuteNonQuery(updateQuery);
                    if (result != -1)
                    {
                        MessageBox.Show("Data 수정 성공");
                        promotionListDataGridView.Rows.Clear();
                        clearDetail();
                        //bringPromotionAll();
                        getPromotionList();
                    }
                    else
                    {
                        MessageBox.Show("Data 수정 실패, 관리자에게 문의하세요.");
                        changeInsertAndUpdateFlag();
                    }

                }
             //===============================================================================
            }


            // 삭제 Button Click 시 Delete 동작!!    - 박현호
            //===============================================================================
            else if (sender == deleteButton)
            {
                int PRDT_PRMO_CD = int.Parse(promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.PRDT_PRMO_CD].Value.ToString());
                int PRDT_CNMB = int.Parse(productConsecutiveNumberTextBox.Text.Trim());
                string PRMO_NM = promotionNameTextBox.Text.Trim();
                string deleteQuery = string.Format("CALL DeletePromotionInfo('{0}','{1}')", PRDT_PRMO_CD, PRDT_CNMB);
                    /*
                    "DELETE FROM TB_PRDT_PRMO_BASC_M WHERE PRDT_PRMO_CD=" + PRDT_PRMO_CD + " AND PRDT_CNMB=" + PRDT_CNMB;
                    */
                if (MessageBox.Show("===================\n▶프로모션 코드 : " + PRDT_PRMO_CD + " \n▶일련번호 : " + PRDT_CNMB + "\n===================\n삭제하시겠습니까?", "프로모션 Data 삭제", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                int result = DbHelper.ExecuteNonQuery(deleteQuery);
                if (result != -1)
                {
                    MessageBox.Show("프로모션 Data 삭제 완료");
                    promotionListDataGridView.Rows.Clear();
                    clearDetail();
                    //bringPromotionAll();
                    getPromotionList();
                }
                else
                {
                    MessageBox.Show("프로모션 Data 삭제 실패, 관리자에게 문의하세요.");
                    changeInsertAndUpdateFlag();
                }
            }
            //===============================================================================

            // 닫기 Button Click 시 창 닫는 동작!
            //===============================================================================
            else if (sender == btnCloseForm)
            {
                this.Close();
            }
            //===============================================================================
        }
        //**********************************************************************************************************************************************************


        // Table 의 행을 Click 했을 경우 동작   - 박현호
        // 상세보기 항목에 Text 를 채움 
        //***********************************************************************************************************************************
        private void promotionListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            changeInsertAndUpdateFlag();
            if (promotionListDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            string PRDT_PRMO_CD = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.PRDT_PRMO_CD].Value.ToString();
            string PRDT_CNMB = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.PRDT_CNMB].Value.ToString();
            string PRMO_NM = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.PRMO_NM].Value.ToString();
            string PRDT_NM = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.PRDT_NM].Value.ToString();
            string ADLT_PRMO_DSCT_RT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.ADLT_PRMO_DSCT_RT].Value.ToString();
            string CHLD_PRMO_DSCT_RT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.CHLD_PRMO_DSCT_RT].Value.ToString();
            string INFN_PRMO_DSCT_RT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.INFN_PRMO_DSCT_RT].Value.ToString();
            string ADLT_PRMO_DSCT_AMT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.ADLT_PRMO_DSCT_AMT].Value.ToString();
            string CHLD_PRMO_DSCT_AMT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.CHLD_PRMO_DSCT_AMT].Value.ToString();
            string INFN_PRMO_DSCT_AMT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.INFN_PRMO_DSCT_AMT].Value.ToString();
            string APLY_LNCH_DT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.APLY_LNCH_DT].Value.ToString();
            string APLY_END_DT = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.APLY_END_DT].Value.ToString();
            string USE_YN = promotionListDataGridView.SelectedRows[0].Cells[(int)eProductPromotionListDataGridView.USE_YN].Value.ToString();

            //promotionCodeTextBox.Text = PRDT_PRMO_CD;
            productConsecutiveNumberTextBox.Text = PRDT_CNMB;
            promotionNameTextBox.Text = PRMO_NM;
            ProductNameComboBox.Text = PRDT_NM;
            adultPromotionRateTextBox.Text = ADLT_PRMO_DSCT_RT;
            childPromotionRateTextBox.Text = CHLD_PRMO_DSCT_RT;
            infantPromotionRateTextBox.Text = INFN_PRMO_DSCT_RT;
            adultPromotionAmountTextBox.Text = ADLT_PRMO_DSCT_AMT;
            childPromotionAmountTextBox.Text = CHLD_PRMO_DSCT_AMT;
            infantPromotionAmountTextBox.Text = INFN_PRMO_DSCT_AMT;
            ApplyLaunchDateTimePicker.Text = APLY_LNCH_DT;
            ApplyEndDateTimePicker.Text = APLY_END_DT;
            UseYnComboBox.Text = USE_YN;
        }
        //***********************************************************************************************************************************


        // 입력값 유효성 검증 - 박현호
        //***********************************************************************************************************************************
        private bool CheckRequireItems(bool flagInsertOrUpdate)
        {
            //string PRDT_PRMO_CD = promotionCodeTextBox.Text.Trim();                                               // 프로모션 코드
            string PRDT_CNMB = productConsecutiveNumberTextBox.Text.Trim();                                     // 상품일련번호 
            string PRMO_NM = promotionNameTextBox.Text.Trim();                                                           // 프로모션 이름
            string ADLT_PRMO_DSCT_RT = adultPromotionRateTextBox.Text.Trim();                                // 성인할인율
            string CHLD_PRMO_DSCT_RT = childPromotionRateTextBox.Text.Trim();                               // 소아 할인율
            string INFN_PRMO_DSCT_RT = infantPromotionRateTextBox.Text.Trim();                           // 유아할인율
            string ADLT_PRMO_DSCT_AMT = adultPromotionAmountTextBox.Text.Trim();                    // 성인할인금액
            string CHLD_PRMO_DSCT_AMT =childPromotionAmountTextBox.Text.Trim();                     // 소아할인금액
            string INFN_PRMO_DSCT_AMT = infantPromotionAmountTextBox.Text.Trim();                    // 유아할인금액
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Trim();                                        // 적용개시일자
            string APLY_END_DT = ApplyEndDateTimePicker.Text.Trim();                                              // 적용종료일자
            string USE_YN = infantPromotionAmountTextBox.Text.Trim();                                               // 사용여부
            /*
            if (PRDT_PRMO_CD == "")
            {
                MessageBox.Show("프로모션 Code 를 선택하세요.");
                promotionCodeTextBox.Focus();
                return false;
            }
            */

       
            if (PRDT_CNMB == "")
            {
                MessageBox.Show("상품 일련번호는 필수값 입니다.");
                return false;
            }
         

            if (PRMO_NM == "")
            {
                MessageBox.Show("프로모션 명을 입력하세요.");
                promotionNameTextBox.Focus();
                return false;
            }

            if (ADLT_PRMO_DSCT_RT == "")
            {
                MessageBox.Show("성인 할인율을 입력하세요.");
                adultPromotionRateTextBox.Focus();
                return false;
            }
            if (CHLD_PRMO_DSCT_RT == "")
            {
                MessageBox.Show("소아 할인율을 입력하세요.");
                childPromotionRateTextBox.Focus();
                return false;
            }
            if (INFN_PRMO_DSCT_RT == "")
            {
                MessageBox.Show("유아 할인율을 입력하세요.");
                infantPromotionRateTextBox.Focus();
                return false;
            }
            if (ADLT_PRMO_DSCT_AMT == "")
            {
                MessageBox.Show("성인 할인금액을 입력하세요.");
                adultPromotionAmountTextBox.Focus();
                return false;
            }
            if (CHLD_PRMO_DSCT_AMT == "")
            {
                MessageBox.Show("소아 할인금액을 입력하세요.");
                childPromotionAmountTextBox.Focus();
                return false;
            }
            if (INFN_PRMO_DSCT_AMT == "")
            {
                MessageBox.Show("유아 할인금액을 입력하세요.");
                infantPromotionAmountTextBox.Focus();
                return false;
            }

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부를 선택하세요.");
                UseYnComboBox.Focus();
                return false;
            }

            if (APLY_LNCH_DT == "")
            {
                MessageBox.Show("적용개시일은 필수 입력항목입니다.");
                ApplyLaunchDateTimePicker.Focus();
                return false;
            }

            if (APLY_END_DT == "")
            {
                MessageBox.Show("적용종료일은 필수 입력항목입니다.");
                ApplyEndDateTimePicker.Focus();
                return false;
            }

            return true;
        }
        //***********************************************************************************************************************************





        // 상세보기창 초기화!!! - 박현호
        //********************************************************************
        private void clearDetail()
        {
            //promotionCodeTextBox.ResetText();
            promotionNameTextBox.ResetText();
            productConsecutiveNumberTextBox.ResetText();
            ProductNameComboBox.ResetText();
            ApplyLaunchDateTimePicker.ResetText();
            ApplyEndDateTimePicker.ResetText();
            UseYnComboBox.ResetText();
            adultPromotionRateTextBox.ResetText();
            childPromotionRateTextBox.ResetText();
            infantPromotionRateTextBox.ResetText();
            adultPromotionAmountTextBox.ResetText();
            childPromotionAmountTextBox.ResetText();
            infantPromotionAmountTextBox.ResetText();
            if (flagInsertOrUpdate)
            {
                changeInsertAndUpdateFlag();
            }
        }
        //********************************************************************





        private void ProductNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductNameComboBox);
            productConsecutiveNumberTextBox.Text = PRDT_CNMB;
        }





        // Insert Update 구분 Flag 변경 Method - 박현호
        //********************************************************************
        private void changeInsertAndUpdateFlag()
        {
            flagInsertOrUpdate = !flagInsertOrUpdate;
        }
        //********************************************************************




        // 할인율, 할인금액 TextBox Enable 처리 Mehtod  --> 190821 박현호
        //=======================================================================================================================================================
        public void promotionTextBoxEnableControl(object sender, EventArgs e)
        {
            object[] allTextBox = { adultPromotionRateTextBox, childPromotionRateTextBox, infantPromotionRateTextBox, adultPromotionAmountTextBox, childPromotionAmountTextBox, infantPromotionAmountTextBox };
            object[] rateTextBox = { adultPromotionRateTextBox, childPromotionRateTextBox, infantPromotionRateTextBox };
            object[] amountTextBox = { adultPromotionAmountTextBox, childPromotionAmountTextBox , infantPromotionAmountTextBox };
           
            for(int i=0; i<allTextBox.Length; i++)
            {
                TextBox tb1 = (TextBox)sender;
                if(tb1 == allTextBox[i])
                {
                    if (i > 2)
                    {
                        int j = 3;

                        TextBox tb = (TextBox)rateTextBox[i-j];
                        if (tb1.Text.Length > 0)
                        {
                            tb.Enabled = false;
                        }
                        else
                        {
                            tb.Enabled = true;
                        }                        
                    }
                    else
                    {
                        TextBox tb = (TextBox)amountTextBox[i];
                        if (tb1.Text.Length > 0)
                        {
                            tb.Enabled = false;
                        }
                        else
                        {
                            tb.Enabled = true;
                        }
                    }

                    break;
                }
            }
            //=======================================================================================================================================================
        }
    }
}
