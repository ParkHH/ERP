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

namespace TripERP.ProductMgt
{
    public partial class ProductDetailInfoMgt : Form
    {
        private string _registrationConsecutiveNumber { get; set; }
        private string _beforeApplyLaunchDate { get; set; }
        private string _beforeApplyEndDate { get; set; }
        private string _beforeProductName { get; set; }

        enum eProductDetailListDataGridView
        {
            PRDT_CNMB,
            PRDT_NM,
            PRDT_GRAD_CD,
            PRDT_GRAD_NM,
            PRDT_CTGR_CD,
            PRDT_CTGR_NM,
            CUR_CD,
            CUR_NM,
            ADLT_SALE_PRCE,
            CHLD_SALE_PRCE,
            INFN_SALE_PRCE,
            APLY_LNCH_DT,
            APLY_END_DT,
            USE_YN_NM,
            USE_YN,
            RGST_CNMB
        };

        public ProductDetailInfoMgt()
        {
            InitializeComponent();
        }

      






        //=================================================================================================
        // 폼 로딩 초기화 Event Method
        //=================================================================================================
        private void ProductDetailInfoMgt_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;        
            InitControls();
            searchProductList();                    // Form Load 시 바로 검색 (내용 출력)
        }

        //------------------------------------------------------------------------------------------
        // 초기화 동작 Method
        //------------------------------------------------------------------------------------------
        private void InitControls()
        {
            // 검색용 상품카테고리코드
            SearchProductCategoryCodeComboBox.Items.Clear();

            string query = "CALL SelectAllProductCategoryCodeList";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품카테고리정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 상품일련번호와 상품명을 콤보에 설정
                string PRDT_CTGR_CD = dataRow["PRDT_CTGR_CD"].ToString();
                string PRDT_CTGR_NM = dataRow["PRDT_CTGR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_CTGR_NM, PRDT_CTGR_CD);
                SearchProductCategoryCodeComboBox.Items.Add(item);
            }

            SearchProductCategoryCodeComboBox.SelectedIndex = -1;

            // 폼 입력필드 초기화
            ResetInputFormField();

            // 그리드 스타일 초기화
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView1 = ProductListDataGridView;
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











        //=================================================================================================
        // 검색버튼 클릭 Event Method
        //=================================================================================================
        private void searchProductListButton_Click(object sender, EventArgs e)
        {            
            searchProductList();
        }

        //------------------------------------------------------------------------------------------
        // 상품상세목록 조회
        //------------------------------------------------------------------------------------------
        private void searchProductList()
        {
            ProductListDataGridView.Rows.Clear();           // 상품 목록 Clear
            productPriceDetailGridView.Rows.Clear();        // 상품 가격 목록 Clear

            // 상품상세리스트 테이블 관련 Column
            string PRDT_CNMB = "";                                                                                              // 상품일련번호
            string PRDT_NM = SearchProductNameTextBox.Text.Trim();                                          // 상품명
            string PRDT_GRAD_CD = "";                                                                                       // 상품등급코드
            string PRDT_GRAD_NM = SearchProductGradeNameTextBox.Text.Trim();                     // 상품등급명
            string PRDT_CTGR_NM = "";                                                                                       // 상품카테고리명
            string PRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(SearchProductCategoryCodeComboBox);    // 상품카테고리코드
            string USE_YN_NM = "";                                                                                           // 사용여부명(공통코드)
            string USE_YN = "";                                                                                                 // 사용여부
            string RGST_CNMB = "";                                                                                          // 등록일련번호      

            string query = string.Format("CALL SelectProductDetailList ('{0}', '{1}', '{2}')", PRDT_NM, PRDT_GRAD_NM, PRDT_CTGR_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품상세목록정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {

                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();
                PRDT_NM = datarow["PRDT_NM"].ToString();
                PRDT_GRAD_CD = datarow["PRDT_GRAD_CD"].ToString();
                PRDT_GRAD_NM = datarow["PRDT_GRAD_NM"].ToString();
                PRDT_CTGR_NM = datarow["PRDT_CTGR_NM"].ToString();
                PRDT_CTGR_CD = datarow["PRDT_CTGR_CD"].ToString();                
                USE_YN_NM = datarow["USE_YN_NM"].ToString();
                USE_YN = datarow["USE_YN"].ToString();                
                RGST_CNMB = datarow["RGST_CNMB"].ToString();        // 상품 가격정보에 대한 등록 일련번호

                ProductListDataGridView.Rows.Add
                (                    
                    PRDT_CNMB, 
                    PRDT_NM, 
                    PRDT_GRAD_CD, 
                    PRDT_GRAD_NM, 
                    PRDT_CTGR_CD, 
                    PRDT_CTGR_NM, 
                    USE_YN_NM, 
                    USE_YN,
                    RGST_CNMB
                );
            }

            ProductListDataGridView.ClearSelection();

        }











        //=========================================================================================================
        // 상품 상세 정보 그리드 행 클릭 Event Method              --> 190927 박현호 수정
        //=========================================================================================================
        private void ProductListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectProductPriceInfo();
        }

        //--------------------------------------------------------------------------------------------------------------
        // 행 Click 시 동작 Method
        //--------------------------------------------------------------------------------------------------------------
        private void selectProductPriceInfo()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // 상품 상세 정보 그리드 행 Click 시 선택한 상품에 대한 판매가 정보를 입력할 수 있도록   enabled true 처리
            //-------------------------------------------------------------------------------------------------------------------
            AdultSalePriceTextBox.Enabled = true;
            ChildSalePriceTextBox.Enabled = true;
            InfantSalePriceTextBox.Enabled = true;
            ApplyLaunchDateTimePicker.Enabled = true;
            ApplyEndDateTimePicker.Enabled = true;            
            UseYnComboBox.Enabled = true;
            CurrencyCodeComboBox.Enabled = true;

            //상품조회결과 Cell Click 했을경우 입력 Component 들을 초기화
            productPriceDetailGridView.Rows.Clear();
            _beforeApplyLaunchDate = "";
            _beforeApplyEndDate = "";
            AdultSalePriceTextBox.Text = "";                                 // 성인판매가
            ChildSalePriceTextBox.Text = "";                                 // 소아판매가
            InfantSalePriceTextBox.Text = "";                                // 유아판매가
            cb_ProductUseYn.SelectedIndex = -1;                         // 상품 상세 사용 여부
            UseYnComboBox.SelectedIndex = -1;                         // 상품 가격 상세 사용여부
            ApplyLaunchDateTimePicker.Text = "";                   // 적용개시일
            ApplyEndDateTimePicker.Text = "";                         // 적용종료일
            CurrencyCodeComboBox.SelectedIndex =  -1;                             // 통화코드

            if (ProductListDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
                

            string PRDT_CNMB = ProductListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            string PRDT_NM = ProductListDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            string PRDT_GRAD_CD = ProductListDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            string PRDT_GRAD_NM = ProductListDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            string PRDT_CTGR_CD = ProductListDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            string PRDT_CTGR_NM = ProductListDataGridView.SelectedRows[0].Cells[5].Value.ToString();       
            string USE_YN = ProductListDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            _beforeProductName = PRDT_NM;
            _registrationConsecutiveNumber = ProductListDataGridView.SelectedRows[0].Cells[8].Value.ToString();       // 등록일련번호

            // 행 선택 시 선택 Cell 상품 상세 정보를 Component 에 Setting
            Utils.SelectComboBoxItemByValue(ProductNameComboBox, PRDT_CNMB);                             // 상품일련번호
            ProductGradeCodeTextBox.Text = PRDT_GRAD_CD;                                                                 // 상품등급코드
            ProductGradeNameTextBox.Text = PRDT_GRAD_NM;                                                                // 상품등급명
            Utils.SelectComboBoxItemByValue(ProductCategoryNameComboBox, PRDT_CTGR_NM);             // 상품카테고리코드
            Utils.SelectComboBoxItemByText(cb_ProductUseYn, USE_YN);                                                // 상품 상세 사용여부


            string query = string.Format("CALL SelectProductPriceDetail({0},{1})", PRDT_CNMB, PRDT_GRAD_CD);
            DataSet ds = DbHelper.SelectQuery(query);
            
            int rowCount = ds.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                //MessageBox.Show("해당 상품에 등록된 판매가 정보가 존재하지 않습니다.");                                
                return;
            }

            

            DataRowCollection rowArr = ds.Tables[0].Rows;
            for (int i = 0; i < rowArr.Count; i++) {
                DataRow row = rowArr[i];

                string CUR_CD = row["CUR_CD"].ToString().Trim();
                string ADLT_SALE_PRCE = row["ADLT_SALE_PRCE"].ToString().Trim();
                string CHLD_SALE_PRCE = row["CHLD_SALE_PRCE"].ToString().Trim();
                string INFN_SALE_PRCE = row["INFN_SALE_PRCE"].ToString().Trim();
                string APLY_LNCH_DT = row["APLY_LNCH_DT"].ToString().Trim().Substring(0,10);
                string APLY_END_DT = row["APLY_END_DT"].ToString().Trim().Substring(0, 10);
                string PRDT_PRCE_USE_YN = row["PRDT_PRCE_USE_YN"].ToString().Trim();
                string USE_YN_NM = "";
                string RGST_CNMB = row["RGST_CNMB"].ToString().Trim();
                if (PRDT_PRCE_USE_YN.Equals("Y"))
                {
                    USE_YN_NM = "예";
                }
                else
                {
                    USE_YN_NM = "아니요";
                }

                if (CUR_CD.Equals("KRW"))
                {
                    ADLT_SALE_PRCE = Utils.SetComma(ADLT_SALE_PRCE.Substring(0, ADLT_SALE_PRCE.IndexOf(".")));
                    CHLD_SALE_PRCE = Utils.SetComma(CHLD_SALE_PRCE.Substring(0, CHLD_SALE_PRCE.IndexOf(".")));
                    INFN_SALE_PRCE = Utils.SetComma(INFN_SALE_PRCE.Substring(0, INFN_SALE_PRCE.IndexOf(".")));
                }

                productPriceDetailGridView.Rows.Add(RGST_CNMB, CUR_CD, ADLT_SALE_PRCE, CHLD_SALE_PRCE, INFN_SALE_PRCE, APLY_LNCH_DT, APLY_END_DT, USE_YN_NM);
                productPriceDetailGridView.ClearSelection();
            }
        }












        //==================================================================================================================
        // 상품상세가격 DataGridView Cell Click Event Method              --> 190927 박현호 수정
        //==================================================================================================================
        private void productPriceDetailGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            setProductPriceData();
        }

        //--------------------------------------------------------------------------------------------------------
        // 상품 상세 가격 정보 DataGridView 에서 선택한 row 의 값을 Componet 에 setting
        //--------------------------------------------------------------------------------------------------------
        private void setProductPriceData()
        {
            if(ProductListDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("상품 등급을 먼저 선택하세요.");
                productPriceDetailGridView.ClearSelection();
                return;
            }
            else
            {
                if(productPriceDetailGridView.SelectedRows.Count == 0)
                {
                    productPriceDetailGridView.ClearSelection();
                    return;
                }
                DataGridViewRow selectedRow = productPriceDetailGridView.SelectedRows[0];
                string CUR_CD = selectedRow.Cells[1].Value.ToString();
                string ADLT_SALE_PRCE = selectedRow.Cells[2].Value.ToString();
                string CHLD_SALE_PRCE = selectedRow.Cells[3].Value.ToString();
                string INFN_SALE_PRCE = selectedRow.Cells[4].Value.ToString();
                string APLY_LNCH_DT = selectedRow.Cells[5].Value.ToString().Trim().Substring(0, 10);
                string APLY_END_DT = selectedRow.Cells[6].Value.ToString().Trim().Substring(0, 10);
                string PRDT_PRCE_USE_YN = selectedRow.Cells[7].Value.ToString().Trim();
                string USE_YN = "";
                if (PRDT_PRCE_USE_YN.Equals("예"))
                {
                    USE_YN = "Y";
                }
                else
                {
                    USE_YN = "N";
                }

                _beforeApplyLaunchDate = APLY_LNCH_DT;
                _beforeApplyEndDate = APLY_END_DT;

                AdultSalePriceTextBox.Text = ADLT_SALE_PRCE;                                 // 성인판매가
                ChildSalePriceTextBox.Text = CHLD_SALE_PRCE;                                 // 소아판매가
                InfantSalePriceTextBox.Text = INFN_SALE_PRCE;                                // 유아판매가
                Utils.SelectComboBoxItemByValue(UseYnComboBox, USE_YN);            // 사용여부
                ApplyLaunchDateTimePicker.Text = APLY_LNCH_DT;                             // 적용개시일
                ApplyEndDateTimePicker.Text = APLY_END_DT;                                   // 적용종료일
                Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD);               // 통화코드
            }           
        }












        //=========================================================================================================
        // 저장버튼 클릭 Event Method              --> 190927 박현호 수정
        //=========================================================================================================
        private void saveButton_Click(object sender, EventArgs e)
        {
            saveProductDetailInfo();
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        // 상품 상세 저장 동작 Method
        // 1. 상품을 등록할것인지.. 수정할 것인지...
        // 2. 상품에 대한 가격 정보를 저장할 것인지..
        //--------------------------------------------------------------------------------------------------------------------------------
        private void saveProductDetailInfo()
        {
            // ================ TB_PRDT_DTLS_L 에 주입될 Data ================
            string PRDT_NM = Utils.GetSelectedComboBoxItemText(ProductNameComboBox);                             // 상품명
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductNameComboBox);                         // 상품일련번호
            string PRDT_GRAD_CD = ProductGradeCodeTextBox.Text.Trim();                                                       // 상품등급코드
            string PRDT_GRAD_NM = ProductGradeNameTextBox.Text.Trim();                                                       // 상품등급명
            string PRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(ProductCategoryNameComboBox);      // 상품카테고리코드    
            string PRDT_USE_YN = Utils.GetSelectedComboBoxItemValue(cb_ProductUseYn);                               // 상품 상세 사용 여부 

            // ================ TB_PRDT_PRCE_DTLS_L 에 주입될 Data ================
            string ADLT_SALE_PRCE = Utils.removeComma(AdultSalePriceTextBox.Text);                                                      // 성인판매가    
            string CHLD_SALE_PRCE = Utils.removeComma(ChildSalePriceTextBox.Text);                                                       // 소아판매가
            string INFN_SALE_PRCE = Utils.removeComma(InfantSalePriceTextBox.Text);                                                    // 유아판매가
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                                                       // 상품 가격 상세 사용여부
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);                                           // 통화코드
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                                                      // 적용개시일자
            string APLY_END_DT = ApplyEndDateTimePicker.Text.Substring(0, 10);                                                              // 적용종료일자


            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID            
            string query = "";                                                                                          // 수행할 Query 변수
            string queryState = "";                                                                                   // 상품, 상품가격 동작 구분을 위한 변수


            //*****************************************************************************
            // 상품 목록에서 선택한 상품이 있다면 상품 가격정보의 등록,수정 또는 상품 정보의 수정인지 판단 
            //*****************************************************************************
            if (ProductListDataGridView.SelectedRows.Count != 0)
            {

                //*****************************************************************************
                // 상품 가격정보 목록에서 선택한 Row 가 존재한다면 --> 가격정보 수정
                //*****************************************************************************
                if (productPriceDetailGridView.SelectedRows.Count != 0)
                {
                    MessageBox.Show("상품 가격정보를 수정합니다.");

                    // 상품 가격정보 입력값 유효성 검증
                    if (CheckRequireItemsProductPrice() == false)
                    {
                        return;
                    }

                    queryState = "PPU";              // Product Price UPDATE
                    query = string.Format("CALL UpdateProductDetailPrice('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",  PRDT_CNMB,
                                                                                                                                                                                             PRDT_GRAD_CD,
                                                                                                                                                                                             CUR_CD,
                                                                                                                                                                                             ADLT_SALE_PRCE,
                                                                                                                                                                                             CHLD_SALE_PRCE,
                                                                                                                                                                                             INFN_SALE_PRCE,
                                                                                                                                                                                             USE_YN,
                                                                                                                                                                                             APLY_LNCH_DT,
                                                                                                                                                                                             APLY_END_DT,
                                                                                                                                                                                             FRST_RGTR_ID
                                                                                                                                                                                             );
                }
                //*****************************************************************************
                //  상품 가격정보 목록에서 선택한 Row 가 없다면 -->  새로운 가격정보 등록 or 상품정보 수정
                //*****************************************************************************
                else
                {
                    // 가격 상세 입력란에 값이 입력되어 있다면 가격정보를 등록하는것     
                    if (AdultSalePriceTextBox.Text != "" || ChildSalePriceTextBox.Text !="" || InfantSalePriceTextBox.Text != "")
                    {
                        MessageBox.Show("새로운 상품 가격 정보를 등록합니다.");

                        // 상품 가격정보 입력값 유효성 검증
                        if (CheckRequireItemsProductPrice() == false)
                        {
                            return;
                        }

                        queryState = "PPI";              // Product Price Insert
                        query = string.Format("CALL InsertProductDetailPrice('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",   PRDT_CNMB,
                                                                                                                                                                                                PRDT_GRAD_CD,
                                                                                                                                                                                                CUR_CD,
                                                                                                                                                                                                ADLT_SALE_PRCE,
                                                                                                                                                                                                CHLD_SALE_PRCE,
                                                                                                                                                                                                INFN_SALE_PRCE,
                                                                                                                                                                                                USE_YN,
                                                                                                                                                                                                APLY_LNCH_DT,
                                                                                                                                                                                                APLY_END_DT,
                                                                                                                                                                                                FRST_RGTR_ID
                                                                                                                                                                                                  );
                    }
                    else     //가격을 입력하지 않았다면 상품정보를 수정하는것
                    {
                        MessageBox.Show("선택한 상품 정보를 수정합니다.");

                        // 상품정보 입력값 유효성 검증
                        if (CheckRequireItemsProduct() == false)
                        {
                            return;
                        }
                        string new_PRDT_NM = ProductNameComboBox.Text.Trim();
                        queryState = "PIU";             // Product Info Update
                        query = string.Format("CALL UpdateProductDetailInfo('{0}','{1}','{2}','{3}','{4}','{5}')",   PRDT_CNMB,
                                                                                                                                                                PRDT_GRAD_CD,
                                                                                                                                                                PRDT_GRAD_NM,
                                                                                                                                                                PRDT_CTGR_CD,
                                                                                                                                                                PRDT_USE_YN,
                                                                                                                                                                FRST_RGTR_ID                                                                                                                                                                        
                                                                                                                                                                );
                    }
                }
            }
            //*****************************************************************************
            // 상품 목록에서 선택한 Row 가 없다면 새로운 상품 정보를 등록
            //*****************************************************************************
            else
            {
                MessageBox.Show("새로운 상품 정보를 등록합니다.");

                List<string> queries = new List<string>();
                string query1 = "";
                string query2 = "";

                // 상품정보 입력값 유효성 검증
                if (CheckRequireItemsProduct() == false)
                {
                    return;
                }                

                queryState = "PII";              // Product Info Insert
                query1 = string.Format("CALL InsertProductDetailInfo('{0}','{1}','{2}','{3}','{4}','{5}')",     PRDT_CNMB,
                                                                                                                                                          PRDT_GRAD_CD,
                                                                                                                                                          PRDT_GRAD_NM,
                                                                                                                                                          PRDT_CTGR_CD,
                                                                                                                                                          PRDT_USE_YN,
                                                                                                                                                          FRST_RGTR_ID
                                                                                                                                                          );

                queries.Add(query1);

                // 상품 정보만 입력해서 등록을 할 수도 있지만 가격정보도 동시에 입력하여 상품정보와 가격정보를 같이 등록할 수도 있다.
                // 성인, 소아, 유가 가격 입력정보에 값이 하나라도 들어있다면 가격정보도 등록해준다.
                if (ADLT_SALE_PRCE != "" || CHLD_SALE_PRCE != "" || INFN_SALE_PRCE != "")
                {
                    // 상품 가격정보 입력값 유효성 검증
                    if (CheckRequireItemsProductPrice() == false)
                    {
                        return;
                    }
                    query2 = string.Format("CALL InsertProductDetailPrice('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",     PRDT_CNMB,
                                                                                                                                                                                                PRDT_GRAD_CD,
                                                                                                                                                                                                CUR_CD,
                                                                                                                                                                                                ADLT_SALE_PRCE,
                                                                                                                                                                                                CHLD_SALE_PRCE,
                                                                                                                                                                                                INFN_SALE_PRCE,
                                                                                                                                                                                                USE_YN,
                                                                                                                                                                                                APLY_LNCH_DT,
                                                                                                                                                                                                APLY_END_DT,
                                                                                                                                                                                                FRST_RGTR_ID
                                                                                                                                                                                                );
                    queries.Add(query2);
                }
               
               int result = DbHelper.ExecuteNonQueryWithTransaction(queries.ToArray());
               if(result < 0)
                {
                    MessageBox.Show("상품 정보 입력에 실패");                    
                }
                else
                {
                    MessageBox.Show("상품 정보 입력 성공");
                    searchProductList();                // 선택 상품 상세정보 GridView Refresh
                    selectProductPriceInfo();       // 선택상품 가격정보 GridView Refresh                                                    
                    ResetInputFormField();          // 저장후 입력 폼 초기화
                }
                return;                 // 새로운 상품정보 입력 후에는 본 줄 아래부분의 Code 는 실행되면 안됨 따라서 return
            }        
                                   
            
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                string message = "";
                if (queryState.Equals("PII") || queryState.Equals("PIU")) {
                    message = "상품상세정보를 저장할 수 없습니다.";                    
                }
                else
                {
                    // 상품 가격정보가 저장될 수 없는 이유는 같은 통화코드에서 등록되어있는 적용기간에 등록하려는 내용의 적용개시일이 포함될때이다.
                    // StoredProcedure 내에서 처리 (InsertProductDetailPrice())
                    message = "상품 가격 정보를 저장 할 수 없습니다.\n등록되어있는 적용기간에 등록하려는 내용의 적용개시일이 포함되는지\n확인해주세요.";                    
                }

                MessageBox.Show(message);
                return;
            }
            else
            {
                string message = "";
                if (queryState.Equals("PII") || queryState.Equals("PIU"))
                {
                    message = "상품상세정보를 저장했습니다.";
                    searchProductList();                // 선택 상품 상세정보 GridView Refresh
                }
                else
                {
                    message = "상품 가격 정보를 저장했습니다.";
                    selectProductPriceInfo();       // 선택상품 가격정보 GridView Refresh
                }

                MessageBox.Show(message);
                
            }

            // 저장후 입력 폼 초기화
            ResetInputFormField();            
        }   













        //=========================================================================================
        // 상품 가격정보 등록시 유효성 Check               --> 190927 박현호
        //=========================================================================================
        private bool CheckRequireItemsProductPrice()
        {
            string ADLT_SALE_PRCE = AdultSalePriceTextBox.Text.Trim();                              // 성인판매가격
            string CHLD_SALE_PRCE = ChildSalePriceTextBox.Text.Trim();                              // 소아판매가격
            string INFN_SALE_PRCE = InfantSalePriceTextBox.Text.Trim();                             // 유아판매가격
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);               // 통화코드
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                      // 사용여부
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                  // 적용개시일자
            string APLY_END_DT = ApplyEndDateTimePicker.Text.Substring(0, 10);                      // 적용종료일자
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            if (CUR_CD == "")
            {
                MessageBox.Show("통화코드는 필수 입력항목입니다.");
                CurrencyCodeComboBox.Focus();
                return false;
            }

            if (ADLT_SALE_PRCE == "")
            {
                MessageBox.Show("성인판매가는 필수 입력항목입니다.");
                AdultSalePriceTextBox.Focus();
                return false;
            }

            if (CHLD_SALE_PRCE == "")
            {
                MessageBox.Show("소아판매가는 필수 입력항목입니다.");
                ChildSalePriceTextBox.Focus();
                return false;
            }

            if (INFN_SALE_PRCE == "")
            {
                MessageBox.Show("유아판매가는 필수 입력항목입니다.");
                InfantSalePriceTextBox.Focus();
                return false;
            }

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
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





        //=========================================================================================
        // 상품등록시 유효성 Check              --> 190927 박현호
        //=========================================================================================
        private bool CheckRequireItemsProduct()
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductNameComboBox);             // 상품일련번호
            string PRDT_GRAD_CD = ProductGradeCodeTextBox.Text.Trim();                              // 상품등급코드
            string PRDT_GRAD_NM = ProductGradeNameTextBox.Text.Trim();                              // 상품등급명
            string PRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(ProductCategoryNameComboBox);  // 상품카테고리코드
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            if (PRDT_GRAD_NM == "")
            {
                MessageBox.Show("상품등급명은 필수 입력항목입니다.");
                ProductGradeNameTextBox.Focus();
                return false;
            }
           
            return true;
        }





        //==========================================================================================================
        // 초기화버튼 클릭 Event Method
        //==========================================================================================================
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        //---------------------------------------------------------------------------------
        // 입력폼 초기화 동작 Method
        //---------------------------------------------------------------------------------
        private void ResetInputFormField()
        {
            /*
             * 현재 GridView 에 선택된 Row 가 있는지 판단하여 CRUD 를 진행한다. 초기화버튼을 누른다는것은 새로운 상품상세정보를 등록하는 행위로 볼 수 있다.    
             * 따라서 초기화 버튼을 누를때 각 GridView 의 선택을 Clear 해줌으로써 새로운 정보를 등록하는 상태로 Setting 해준다
             */
            ProductListDataGridView.ClearSelection();           // 상품목록 선택 해제
            productPriceDetailGridView.ClearSelection();        // 상품 가격 목록 선택 해제           
            ProductCategoryNameComboBox.Items.Clear();
            ProductCategoryNameComboBox.Text = "";
            
            ProductGradeCodeTextBox.Text = "";
            ProductGradeNameTextBox.Text = "";
            AdultSalePriceTextBox.Text = "";
            ChildSalePriceTextBox.Text = "";
            InfantSalePriceTextBox.Text = "";
            ApplyLaunchDateTimePicker.Text = "";
            ApplyEndDateTimePicker.Text = "";

            if (ProductListDataGridView.SelectedRows.Count == 0)
            {
                AdultSalePriceTextBox.Enabled = false;
                ChildSalePriceTextBox.Enabled = false;
                InfantSalePriceTextBox.Enabled = false;
                ApplyLaunchDateTimePicker.Enabled = false;
                ApplyEndDateTimePicker.Enabled = false;                
                UseYnComboBox.Enabled = false;
                CurrencyCodeComboBox.Enabled = false;
            }
            else
            {
                AdultSalePriceTextBox.Enabled = true;
                ChildSalePriceTextBox.Enabled = true;
                InfantSalePriceTextBox.Enabled = true;
                ApplyLaunchDateTimePicker.Enabled = true;
                ApplyEndDateTimePicker.Enabled = true;                
                UseYnComboBox.Enabled = true;
                CurrencyCodeComboBox.Enabled = true;
            }           
            

            // 상품 상세 사용여부
            cb_ProductUseYn.Items.Clear();
            cb_ProductUseYn.Text = "";

            // 상품 가격 상세 사용여부
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
                cb_ProductUseYn.Items.Add(item);        // 상품 상세 사용여부
                UseYnComboBox.Items.Add(item);          // 상품 가격 상세 사용여부
                
            }

            cb_ProductUseYn.SelectedIndex = -1;
            UseYnComboBox.SelectedIndex = -1;

            // 상품명
            ProductNameComboBox.Items.Clear();
            ProductNameComboBox.Text = "";

            query = "CALL SelectAllProductList";
            dataSet = DbHelper.SelectQuery(query);
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

            // 통화코드 콤보박스 초기화
            CurrencyCodeComboBox.Items.Clear();
            CurrencyCodeComboBox.Text = "";

            query = "CALL SelectCurList ()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("통화코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CUR_CD = dataRow["CUR_CD"].ToString();
                string CUR_NM = dataRow["CUR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CUR_NM, CUR_CD);

                CurrencyCodeComboBox.Items.Add(item);
            }

            CurrencyCodeComboBox.SelectedIndex = -1;
        }







        //=========================================================================================
        // 폼 닫기
        //=========================================================================================
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }







        //=========================================================================================
        // 삭제버튼 클릭
        //=========================================================================================
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(ProductListDataGridView.SelectedRows.Count > 0)
            {
 
                MessageBox.Show("상품 등급정보 및 상세 가격정보를 미사용 처리합니다.");
                string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductNameComboBox);                 // 상품일련번호
                string PRDT_GRAD_CD = ProductGradeCodeTextBox.Text.Trim();                                                  // 상품등급코드

                if (PRDT_CNMB == "")
                {
                    MessageBox.Show("상품번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                    ProductNameComboBox.Focus();
                    return;
                }
                
                if (PRDT_GRAD_CD == "")
                {
                    MessageBox.Show("상품등급코드는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                    return;
                }

                if (MessageBox.Show("선택한 항목을 미사용 처리하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string query = string.Format("CALL DeleteProductDetailInfo ('{0}', '{1}', '{2}')", PRDT_CNMB, PRDT_GRAD_CD, _registrationConsecutiveNumber);
                int retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal == -1)
                {
                    MessageBox.Show(" 상품상세정보를 미사용 처리 할 수 없습니다. 운영담당자에게 연락하세요.");
                    return;
                }
                else
                {
                    searchProductList();
                    MessageBox.Show("상품 등급 및 가격정보를 미사용 처리를 완료했습니다.");
                }

                // 삭제후 입력 폼 초기화
                ResetInputFormField();


            }
        }








        //=========================================================================================
        // 상품명 ComboBox 선택 변경이 Event Method
        //=========================================================================================
        private void ProductNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductNameComboBox);  // 상품일련번호

            ProductCategoryNameComboBox.Items.Clear();
            string query = string.Format("CALL SelectProductCategoryInfoByProductNo ('{0}')", PRDT_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("선택한 상품번호에 대한 상품카테고리정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 상품카테고리코드와 상품카테고리명을 콤보에 설정
                string PRDT_CTGR_CD = dataRow["PRDT_CTGR_CD"].ToString();
                string PRDT_CTGR_NM = dataRow["PRDT_CTGR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_CTGR_NM, PRDT_CTGR_CD);
                ProductCategoryNameComboBox.Items.Add(item);
            }

            ProductCategoryNameComboBox.SelectedIndex = 0;
        }








        //=========================================================================================
        // 엑셀 내보내기
        //=========================================================================================
        private void exportExcelButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "파일 내보내기";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Trim() == "")
                return;

            this.Cursor = Cursors.WaitCursor;

            string filePath = saveFileDialog.FileName.Trim();
            string fileDirPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));

            ProductListDataGridView.SuspendLayout();

            ProductListDataGridView.SelectAll();

            if (Directory.Exists(fileDirPath))
            {
                //if (true == ExcelHelper.ExportExcel(filePath, reservationDataGridView))
                if (ExcelHelper.gridToExcel(filePath, ProductListDataGridView) == true)
                    MessageBox.Show(string.Format("{0}\r\n파일을 저장했습니다.", filePath));
                else
                    MessageBox.Show("파일을 저장 할 수 없습니다.");
            }
            else
            {
                MessageBox.Show("잘못된 저장 경로입니다.");
            }

            this.Cursor = Cursors.Default;
            ProductListDataGridView.ClearSelection();

            ProductListDataGridView.ResumeLayout();

        }






        //=======================================================================================
        // Form 에서 특정 버튼 눌렀을 경우 동작 설정 Event Method              --> 190927 박현호
        //=======================================================================================
        private void ProductDetailInfoMgt_KeyDown(object sender, KeyEventArgs e)
        {            
            String keyCode =  e.KeyCode.ToString();
            keyAction(keyCode);      
        }

        //-------------------------------------------------------------------
        // 입력 Key 에 따른 동작 Method
        //-------------------------------------------------------------------
        private void keyAction(String keyCode)
        {            
            // 1. ESC 버튼 눌럿을 경우 동작
            if(keyCode == Keys.Escape.ToString())
            {                
                ProductListDataGridView.ClearSelection();
                productPriceDetailGridView.ClearSelection();
                ResetInputFormField();
            }
        }
    }
}