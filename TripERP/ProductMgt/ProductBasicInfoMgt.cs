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
    public partial class ProductBasicInfoMgt : Form
    {
        enum eRateListDataGridView
        {
            PRDT_CNMB,
            PRDT_NM,
            PRDT_CTGR_NM,
            PRDT_CTGR_CD,
            APLY_NATN_NM,
            APLY_NATN_CD,
            APLY_NATN_REGN_NM,
            APLY_NATN_REGN_CD,
            APLY_LNCH_DT,
            APLY_END_DT,
            USE_YN_NM,
            USE_YN,
            CNSM_FILE_APLY_YN_NM,
            CNSM_FILE_APLY_YN,
            LST_CHCK_NEED_YN,
            PSPT_CHCK_NEED_YN,
            ARGM_CHCK_NEED_YN,
            VOCH_CHCK_NEED_YN,
            AVAT_CHCK_NEED_YN,
            ISRC_CHCK_NEED_YN,
            PRSN_CHCK_NEED_YN
        };
                                    
        public ProductBasicInfoMgt()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void ProductBasicInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
        }

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
                ProductCategoryCodeComboBox.Items.Add(item);
            }

            SearchProductCategoryCodeComboBox.SelectedIndex = -1;

            // 사용여부
            UseYnComboBox.Items.Clear();

            query = "CALL SelectCommoncodeList ('YN')";
            dataSet = DbHelper.SelectQuery(query);
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
                purchaseFileApplyYnComboBox.Items.Add(item);
            }

            UseYnComboBox.SelectedIndex = 0;
            purchaseFileApplyYnComboBox.SelectedIndex = -1;

            // 국가코드 (거래가능한 국가코드만 검색)

            ApplyNationCodeComboBox.Items.Clear();

            query = "CALL SelectTransactionPossibleNationList";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string APLY_NATN_CD = dataRow["NATN_CD"].ToString();
                string APLY_NATN_NM = dataRow["KOR_NATN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(APLY_NATN_NM, APLY_NATN_CD);
                ApplyNationCodeComboBox.Items.Add(item);
            }

            ApplyNationCodeComboBox.SelectedIndex = 0;

            // 그리드 스타일 초기화
            InitDataGridView();

            SearchProductInfo();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
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

        // 검색버튼 클릭
        private void searchProductListButton_Click(object sender, EventArgs e)
        {
            SearchProductInfo();
        }

        // 상품기본정보 목록조회
        private void SearchProductInfo()
        {
            ProductListDataGridView.Rows.Clear();

            string PRDT_CNMB = "";
            string PRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(SearchProductCategoryCodeComboBox);
            string PRDT_CTGR_NM = "";
            string PRDT_NM = SearchProductNameTextBox.Text.Trim();
            string APLY_NATN_CD = "";
            string APLY_NATN_NM = "";
            string APLY_NATN_REGN_CD = "";
            string APLY_NATN_REGN_NM = "";
            string APLY_LNCH_DT = "";
            string APLY_END_DT = "";

            Boolean LST_CHCK_NEED_YN = false;
            Boolean PSPT_CHCK_NEED_YN = false;
            Boolean ARGM_CHCK_NEED_YN = false;
            Boolean VOCH_CHCK_NEED_YN = false;
            Boolean AVAT_CHCK_NEED_YN = false;
            Boolean ISRC_CHCK_NEED_YN = false;
            Boolean PRSN_CHCK_NEED_YN = false;

            string USE_YN = "";
            string USE_YN_NM = "";

            string CNSM_FILE_APLY_YN = "";
            string CNSM_FILE_APLY_YN_NM = "";

            string query = string.Format("CALL SelectProductList ('{0}', '{1}')", PRDT_NM, PRDT_CTGR_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품기본정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();                         // 상품일련번호
                PRDT_NM = datarow["PRDT_NM"].ToString();                             // 상품명
                PRDT_CTGR_NM = datarow["PRDT_CTGR_NM"].ToString();                   // 상품카테고리명
                PRDT_CTGR_CD = datarow["PRDT_CTGR_CD"].ToString();                   // 상품카테고리코드
                APLY_NATN_CD = datarow["APLY_NATN_CD"].ToString();
                APLY_NATN_NM = datarow["APLY_NATN_NM"].ToString();
                APLY_NATN_REGN_CD = datarow["APLY_NATN_REGN_CD"].ToString();
                APLY_NATN_REGN_NM = datarow["APLY_NATN_REGN_NM"].ToString();

                APLY_LNCH_DT = datarow["APLY_LNCH_DT"].ToString().Substring(0, 10);
                APLY_END_DT = datarow["APLY_END_DT"].ToString().Substring(0, 10);

                if(datarow["LST_CHCK_NEED_YN"].ToString().Equals("Y")) 
                {
                    LST_CHCK_NEED_YN = true;
                }
                else
                {
                    LST_CHCK_NEED_YN = false;
                }
                if (datarow["PSPT_CHCK_NEED_YN"].ToString().Equals("Y"))
                {
                    PSPT_CHCK_NEED_YN = true;
                }
                else
                {
                    PSPT_CHCK_NEED_YN = false;
                }
                if (datarow["ARGM_CHCK_NEED_YN"].ToString().Equals("Y"))
                {
                    ARGM_CHCK_NEED_YN = true;
                }
                else
                {
                    ARGM_CHCK_NEED_YN = false;
                }
                if (datarow["VOCH_CHCK_NEED_YN"].ToString().Equals("Y"))
                {
                    VOCH_CHCK_NEED_YN = true;
                }
                else
                {
                    VOCH_CHCK_NEED_YN = false;
                }
                if (datarow["AVAT_CHCK_NEED_YN"].ToString().Equals("Y"))
                {
                    AVAT_CHCK_NEED_YN = true;
                }
                else
                {
                    AVAT_CHCK_NEED_YN = false;
                }
                if (datarow["ISRC_CHCK_NEED_YN"].ToString().Equals("Y"))
                {
                    ISRC_CHCK_NEED_YN = true;
                }
                else
                {
                    ISRC_CHCK_NEED_YN = false;
                }
                if (datarow["PRSN_CHCK_NEED_YN"].ToString().Equals("Y"))
                {
                    PRSN_CHCK_NEED_YN = true;
                }
                else
                {
                    PRSN_CHCK_NEED_YN = false;
                }

                USE_YN = datarow["USE_YN"].ToString();
                USE_YN_NM = datarow["USE_YN_NM"].ToString();
                //USE_YN_NM = Global.GetCommonCodeDesc("YN", USE_YN);                         // 사용여부


                CNSM_FILE_APLY_YN = datarow["CNSM_FILE_APLY_YN"].ToString();
                CNSM_FILE_APLY_YN_NM = datarow["CNSM_FILE_APLY_YN_NM"].ToString();
                // CNSM_FILE_APLY_YN_NM = Global.GetCommonCodeDesc("YN", CNSM_FILE_APLY_YN);   // 구매자파일적용여부

                ProductListDataGridView.Rows.Add
                (
                    PRDT_CNMB, 
                    PRDT_NM, 
                    PRDT_CTGR_NM, 
                    PRDT_CTGR_CD, 
                    APLY_NATN_NM, 
                    APLY_NATN_CD, 
                    APLY_NATN_REGN_NM, 
                    APLY_NATN_REGN_CD, 
                    APLY_LNCH_DT, 
                    APLY_END_DT,
                    USE_YN_NM,
                    USE_YN,
                    CNSM_FILE_APLY_YN_NM,
                    CNSM_FILE_APLY_YN,
                    LST_CHCK_NEED_YN,
                    PSPT_CHCK_NEED_YN,
                    ARGM_CHCK_NEED_YN,
                    VOCH_CHCK_NEED_YN,
                    AVAT_CHCK_NEED_YN,
                    ISRC_CHCK_NEED_YN,
                    PRSN_CHCK_NEED_YN
                );
            }

            ProductListDataGridView.ClearSelection();

        }

        // 그리드 행 클릭
        private void ProductListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProductListDataGridView.SelectedRows.Count == 0)
                return;

            string PRDT_CNMB = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.PRDT_CNMB].Value.ToString();
            string PRDT_NM = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.PRDT_NM].Value.ToString();
            string PRDT_CTGR_NM = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.PRDT_CTGR_NM].Value.ToString();
            string PRDT_CTGR_CD = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.PRDT_CTGR_CD].Value.ToString();
            string APLY_NATN_NM = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_NATN_NM].Value.ToString();
            string APLY_NATN_CD = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_NATN_CD].Value.ToString();
            string APLY_NATN_REGN_NM = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_NATN_REGN_NM].Value.ToString();
            string APLY_NATN_REGN_CD = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_NATN_REGN_CD].Value.ToString();
            string APLY_LNCH_DT = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_LNCH_DT].Value.ToString();
            string APLY_END_DT = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_END_DT].Value.ToString();
            string USE_YN_NM = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.USE_YN_NM].Value.ToString();
            string CNSM_FILE_APLY_YN_NM = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.CNSM_FILE_APLY_YN_NM].Value.ToString();

            bool LST_CHCK_NEED_YN = Convert.ToBoolean(ProductListDataGridView.SelectedRows[0].Cells["LST_CHCK_NEED_YN"].Value);
            bool PSPT_CHCK_NEED_YN = Convert.ToBoolean(ProductListDataGridView.SelectedRows[0].Cells["PSPT_CHCK_NEED_YN"].Value);
            bool ARGM_CHCK_NEED_YN = Convert.ToBoolean(ProductListDataGridView.SelectedRows[0].Cells["ARGM_CHCK_NEED_YN"].Value);
            bool VOCH_CHCK_NEED_YN = Convert.ToBoolean(ProductListDataGridView.SelectedRows[0].Cells["VOCH_CHCK_NEED_YN"].Value);
            bool ISRC_CHCK_NEED_YN = Convert.ToBoolean(ProductListDataGridView.SelectedRows[0].Cells["ISRC_CHCK_NEED_YN"].Value);
            bool AVAT_CHCK_NEED_YN = Convert.ToBoolean(ProductListDataGridView.SelectedRows[0].Cells["AVAT_CHCK_NEED_YN"].Value);
            bool PRSN_CHCK_NEED_YN = Convert.ToBoolean(ProductListDataGridView.SelectedRows[0].Cells["PRSN_CHCK_NEED_YN"].Value);

            string USE_YN = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.USE_YN].Value.ToString();
            string CNSM_FILE_APLY_YN = ProductListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.CNSM_FILE_APLY_YN].Value.ToString();

            listNameCheckBox.Checked = false;
            passportCheckBox.Checked = false;
            arragementCheckBox.Checked = false;
            voucherCheckBox.Checked = false;
            airlineCheckBox.Checked = false;
            insuranceCheckBox.Checked = false;
            personalInformationCheckBox.Checked = false;

            ProductNoTextBox.Text = PRDT_CNMB;
            Utils.SelectComboBoxItemByValue(ProductCategoryCodeComboBox, PRDT_CTGR_CD);      // 상품카테고리코드

            ProductNameTextBox.Text = PRDT_NM;                                               // 상품명
            Utils.SelectComboBoxItemByValue(UseYnComboBox, USE_YN);                          // 사용여부
            Utils.SelectComboBoxItemByValue(purchaseFileApplyYnComboBox, CNSM_FILE_APLY_YN); // 구매자파일적용여부
            Utils.SelectComboBoxItemByValue(ApplyNationCodeComboBox, APLY_NATN_CD);          // 적용국가코드

            ApplyRegionCodeComboBox.Items.Clear();

            string query = string.Format("CALL SelectRegionInfo ('{0}')", APLY_NATN_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가목록정보를 가져올 수 없습니다.");
                return;
            }            

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 국가코드와 국가명을 콤보에 설정
                string NATN_CD = dataRow["NATN_REGN_CD"].ToString();
                string KOR_NATN_NM = dataRow["NATN_REGN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(KOR_NATN_NM, NATN_CD);
                ApplyRegionCodeComboBox.Items.Add(item);                
            }

            ApplyRegionCodeComboBox.Text = APLY_NATN_REGN_NM;          

            ApplyLaunchDateTimePicker.Text = APLY_LNCH_DT;
            ApplyEndDateTimePicker.Text = APLY_END_DT;
            

            if (LST_CHCK_NEED_YN == true)
            {
                listNameCheckBox.Checked = true;
            }

            if (PSPT_CHCK_NEED_YN == true)
            {
                passportCheckBox.Checked = true;
            }

            if (ARGM_CHCK_NEED_YN == true)
            {
                arragementCheckBox.Checked = true;
            }

            if (VOCH_CHCK_NEED_YN == true)
            {
                voucherCheckBox.Checked = true;
            }

            if (ISRC_CHCK_NEED_YN == true)
            {
                insuranceCheckBox.Checked = true;
            }

            if (AVAT_CHCK_NEED_YN == true)
            {
                airlineCheckBox.Checked = true;
            }

            if (PRSN_CHCK_NEED_YN == true)
            {
                personalInformationCheckBox.Checked = true;
            }
        }

        // 폼 닫기
        private void btnCloseForm_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveProductInfo();
        }

        // 상품기본정보 저장
        private void SaveProductInfo()
        {
            string PRDT_CNMB = ProductNoTextBox.Text.Trim();                                          // 상품일련번호
            string PRDT_NM = ProductNameTextBox.Text.Trim();                                          // 상품명
            string PRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(ProductCategoryCodeComboBox);    // 상품카테고리코드
            string APLY_NATN_CD = Utils.GetSelectedComboBoxItemValue(ApplyNationCodeComboBox);        // 적용국가코드
            string APLY_NATN_REGN_CD = Utils.GetSelectedComboBoxItemValue(ApplyRegionCodeComboBox);   // 적용국가지역코드

            string LST_CHCK_NEED_YN = "N";
            string PSPT_CHCK_NEED_YN = "N";
            string ARGM_CHCK_NEED_YN = "N";
            string VOCH_CHCK_NEED_YN = "N";
            string ISRC_CHCK_NEED_YN = "N";
            string AVAT_CHCK_NEED_YN = "N";
            string PRSN_CHCK_NEED_YN = "N";

            if (listNameCheckBox.Checked == true) LST_CHCK_NEED_YN = "Y";
            if (passportCheckBox.Checked == true) PSPT_CHCK_NEED_YN = "Y";
            if (arragementCheckBox.Checked == true) ARGM_CHCK_NEED_YN = "Y";
            if (voucherCheckBox.Checked == true) VOCH_CHCK_NEED_YN = "Y";
            if (insuranceCheckBox.Checked == true) ISRC_CHCK_NEED_YN = "Y";
            if (airlineCheckBox.Checked == true) AVAT_CHCK_NEED_YN = "Y";
            if (personalInformationCheckBox.Checked == true) PRSN_CHCK_NEED_YN = "Y";

            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Value.ToString("yyyy-MM-dd");               // 적용개시일
            string APLY_END_DT = ApplyEndDateTimePicker.Value.ToString("yyyy-MM-dd");                   // 적용종료일
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                          // 사용여부
            string CNSM_FILE_APLY_YN = Utils.GetSelectedComboBoxItemValue(purchaseFileApplyYnComboBox); // 구매자파일적용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                             // 최초등록자ID
            string FINL_MDFR_ID = Global.loginInfo.ACNT_ID;                                             // 최종변경자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            // 상품일련번호가 입력되었으면 Update를, 입력되지 않았으면 Insert로 처리 (Insert시에는 상품일련번호를 자동 채번)
            if (PRDT_CNMB !="")
            {
                query = string.Format("CALL UpdateProductInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
                    PRDT_CNMB, 
                    PRDT_NM, 
                    PRDT_CTGR_CD, 
                    APLY_NATN_CD, 
                    APLY_NATN_REGN_CD,
                    LST_CHCK_NEED_YN,
                    PSPT_CHCK_NEED_YN,
                    ARGM_CHCK_NEED_YN,
                    VOCH_CHCK_NEED_YN,
                    ISRC_CHCK_NEED_YN,
                    AVAT_CHCK_NEED_YN,
                    PRSN_CHCK_NEED_YN,
                    APLY_LNCH_DT, 
                    APLY_END_DT, 
                    USE_YN,
                    CNSM_FILE_APLY_YN,
                    FINL_MDFR_ID
                );
            }
            else
            {
                query = string.Format("CALL InsertProductInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    PRDT_NM, 
                    PRDT_CTGR_CD, 
                    APLY_NATN_CD, 
                    APLY_NATN_REGN_CD,
                    LST_CHCK_NEED_YN,
                    PSPT_CHCK_NEED_YN,
                    ARGM_CHCK_NEED_YN,
                    VOCH_CHCK_NEED_YN,
                    ISRC_CHCK_NEED_YN,
                    AVAT_CHCK_NEED_YN,
                    PRSN_CHCK_NEED_YN,
                    APLY_LNCH_DT, 
                    APLY_END_DT, 
                    USE_YN,
                    CNSM_FILE_APLY_YN,
                    FRST_RGTR_ID);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("상품기본정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchProductInfo();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("상품기본정보를 저장했습니다.");
            }

        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string PRDT_CNMB = ProductNoTextBox.Text.Trim();                                             // 상품일련번호
            string PRDT_NM = ProductNameTextBox.Text.Trim();                                             // 상품명
            string PRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(ProductCategoryCodeComboBox);       // 상품카테고리코드
            string APLY_NATN_CD = Utils.GetSelectedComboBoxItemValue(ApplyNationCodeComboBox);           // 적용국가코드
            string APLY_NATN_REGN_CD = Utils.GetSelectedComboBoxItemValue(ApplyRegionCodeComboBox);      // 적용국가지역코드
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Value.ToString("yyyy-MM-dd");                // 적용개시일
            string APLY_END_DT = ApplyEndDateTimePicker.Value.ToString("yyyy-MM-dd");                    // 적용종료일
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                           // 사용여부
            string CNSM_FILE_APLY_YN = Utils.GetSelectedComboBoxItemValue(purchaseFileApplyYnComboBox);  // 구매자파일적용여부

            if (PRDT_NM == "")
            {
                MessageBox.Show("상품명은 필수 입력항목입니다.");
                ProductNameTextBox.Focus();
                return false;
            }

            if (PRDT_CTGR_CD == "")
            {
                MessageBox.Show("상품카테고리는 필수 입력항목입니다.");
                ProductCategoryCodeComboBox.Focus();
                return false;
            }
            /*
            if (APLY_NATN_CD == "")
            {
                MessageBox.Show("적용국가는 필수 입력항목입니다.");
                ApplyNationCodeComboBox.Focus();
                return false;
            }

            if (APLY_NATN_REGN_CD == "")
            {
                MessageBox.Show("적용지역은 필수 입력항목입니다.");
                ApplyRegionCodeComboBox.Focus();
                return false;
            }
            */
            if (APLY_LNCH_DT == "")
            {
                MessageBox.Show("적용개시일은 필수 입력항목입니다.");
                ApplyLaunchDateTimePicker.Focus();
                return false;
            }

            if (APLY_END_DT == "")
            {
                MessageBox.Show("적용개시일은 필수 입력항목입니다.");
                ApplyEndDateTimePicker.Focus();
                return false;
            }

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
                UseYnComboBox.Focus();
                return false;
            }

            if (CNSM_FILE_APLY_YN == "")
            {
                MessageBox.Show("구매자파일대상여부는 필수 입력항목입니다.");
                UseYnComboBox.Focus();
                return false;
            }

            return true;
        }

        // 국가코드 선택이 변경되면 국가에 해당되는 지역코드를 콤보박에스 설정
        private void ApplyNationCodeComboBox_TextChanged(object sender, EventArgs e)
        {
            setNationRegionList(Utils.GetSelectedComboBoxItemValue(ApplyNationCodeComboBox));
        }

        // 국가지역코드 콤보박스 세팅
        private void setNationRegionList(string NATN_CD)
        {
            // 국가코드
            ApplyRegionCodeComboBox.Items.Clear();

            string query = string.Format("CALL SelectNationRegionListByNationCode('{0}')", NATN_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가지역정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 국가지역목록 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string APLY_NATN_REGN_CD = dataRow["NATN_REGN_CD"].ToString();
                string APLY_NATN_REGN_NM = dataRow["NATN_REGN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(APLY_NATN_REGN_NM, APLY_NATN_REGN_CD);
                ApplyRegionCodeComboBox.Items.Add(item);
            }

            ApplyRegionCodeComboBox.SelectedIndex = -1;
        }

        // 삭제버튼 클릭
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string PRDT_CNMB = ProductNoTextBox.Text.Trim();         // 상품일련번호

            if (PRDT_CNMB == "")
            {
                MessageBox.Show("상품번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                ProductNoTextBox.Focus();
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteProductInfo ('{0}')", PRDT_CNMB);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show(" 상품기본정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchProductInfo();
                MessageBox.Show("상품기본정보를 삭제했습니다.");
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ProductNoTextBox.Text = "";
            ProductNameTextBox.Text = "";
            ProductCategoryCodeComboBox.SelectedIndex = -1;
            UseYnComboBox.SelectedIndex = -1;
            ApplyNationCodeComboBox.SelectedIndex = -1;
            ApplyRegionCodeComboBox.SelectedIndex = -1;


            listNameCheckBox.Checked = false;
            passportCheckBox.Checked = false;
            arragementCheckBox.Checked = false;
            voucherCheckBox.Checked = false;
            airlineCheckBox.Checked = false;
            insuranceCheckBox.Checked = false;
            personalInformationCheckBox.Checked = false;

        }

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
    }
}
