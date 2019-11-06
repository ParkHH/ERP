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

namespace TripERP.ProductMgt
{
    public partial class PopUpAddProductInfo : Form
    {
        public string _productNumber = "";
        public string _productGradeCode = "";

        private string VoPRDT_CNMB { get; set; }
        private string VoPRDT_GRAD_CD { get; set; }
        private string VoPRDT_CTGR_CD { get; set; }
        private string VoPRDT_NM { get; set; }
        private string VoPRDT_GRAD_NM { get; set; }
        private string VoSALE_CUR_CD { get; set; }
        private string VoADLT_SALE_PRCE { get; set; }
        private string VoCHLD_SALE_PRCE { get; set; }
        private string VoINFN_SALE_PRCE { get; set; }
        private string VoAPLY_NATN_CD { get; set; }
        private string VoAPLY_NATN_REGN_CD { get; set; }
        private string VoAPLY_LNCH_DT { get; set; }
        private string VoAPLY_END_DT { get; set; }
        private string VoLST_CHCK_NEED_YN { get; set; }
        private string VoPSPT_CHCK_NEED_YN { get; set; }
        private string VoARGM_CHCK_NEED_YN { get; set; }
        private string VoVOCH_CHCK_NEED_YN { get; set; }
        private string VoAVAT_CHCK_NEED_YN { get; set; }
        private string VoISRC_CHCK_NEED_YN { get; set; }
        private string VoPRSN_CHCK_NEED_YN { get; set; }
        private string VoUSE_YN { get; set; }
        private string VoCNSM_FILE_APLY_YN { get; set; }
        private string VoFRST_RGTR_ID { get; set; }

        public PopUpAddProductInfo()
        {
            InitializeComponent();
        }

        private void PopUpAddProductInfo_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        //=========================================================================================================================================================================
        // 폼 초기화
        //=========================================================================================================================================================================
        private void InitControls()
        {
            // 상품카테고리 콤보박스 설정
            LoadProductCategoryCodeComboBoxItems();

            // 국가코드 콤보박스 설정
            LoadApplyNationCodeComboBoxItems();

            // 통화코드 콤보박스 설정
            LoadCurrencyCodeComboBoxItems();
        }

        //=========================================================================================================================================================================
        // 상품카테고리 콤보박스 아이템 로드
        //=========================================================================================================================================================================
        private void LoadProductCategoryCodeComboBoxItems()
        {
            // 검색용 상품카테고리코드
            ProductCategoryCodeComboBox.Items.Clear();

            string query = "CALL SelectAllProductCategoryCodeList";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품카테고리정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string PRDT_CTGR_CD = dataRow["PRDT_CTGR_CD"].ToString();
                string PRDT_CTGR_NM = dataRow["PRDT_CTGR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_CTGR_NM, PRDT_CTGR_CD);
                ProductCategoryCodeComboBox.Items.Add(item);
            }

            ProductCategoryCodeComboBox.SelectedIndex = -1;
        }

        //=========================================================================================================================================================================
        // 국가코드 콤보박스 아이템 로드
        //=========================================================================================================================================================================
        private void LoadApplyNationCodeComboBoxItems()
        {
            // 국가코드 (거래가능한 국가코드만 검색)
            ApplyNationCodeComboBox.Items.Clear();

            string query = "CALL SelectTransactionPossibleNationList";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string APLY_NATN_CD = dataRow["NATN_CD"].ToString();
                string APLY_NATN_NM = dataRow["KOR_NATN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(APLY_NATN_NM, APLY_NATN_CD);
                ApplyNationCodeComboBox.Items.Add(item);
            }

            ApplyNationCodeComboBox.SelectedIndex = -1;
        }

        //=========================================================================================================================================================================
        // 통화코드 콤보박스 아이템 로드
        //=========================================================================================================================================================================
        private void LoadCurrencyCodeComboBoxItems()
        {
            CurrencyCodeComboBox.Items.Clear();

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
                string CUR_SYBL = dataRow["CUR_SYBL"].ToString();

                ComboBoxItem item = new ComboBoxItem(string.Format("{0}({1})", CUR_NM, CUR_SYBL), CUR_CD);

                CurrencyCodeComboBox.Items.Add(item);      // 판매통화코드
            }

            if (CurrencyCodeComboBox.Items.Count > 0)
            {
                CurrencyCodeComboBox.SelectedIndex = -1;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetVO();
            resetInputFormField();
        }

        private void resetVO()
        {
            VoPRDT_CNMB = "";
            VoPRDT_GRAD_CD = "";
            VoPRDT_CTGR_CD = "";
            VoPRDT_NM = "";
            VoPRDT_GRAD_NM = "";
            VoSALE_CUR_CD = "";
            VoADLT_SALE_PRCE = "";
            VoCHLD_SALE_PRCE = "";
            VoINFN_SALE_PRCE = "";
            VoAPLY_NATN_CD = "";
            VoAPLY_NATN_REGN_CD = "";
            VoAPLY_LNCH_DT = "";
            VoAPLY_END_DT = "";
            VoLST_CHCK_NEED_YN = "";
            VoPSPT_CHCK_NEED_YN = "";
            VoARGM_CHCK_NEED_YN = "";
            VoVOCH_CHCK_NEED_YN = "";
            VoAVAT_CHCK_NEED_YN = "";
            VoISRC_CHCK_NEED_YN = "";
            VoPRSN_CHCK_NEED_YN = "";
            VoUSE_YN = "";
            VoCNSM_FILE_APLY_YN = "";
            VoAPLY_LNCH_DT = "";
            VoAPLY_END_DT = "";
            VoFRST_RGTR_ID = Global.loginInfo.ACNT_ID;
        }

        //=========================================================================================================================================================================
        // 초기화버튼 클릭
        //=========================================================================================================================================================================
        private void resetInputFormField()
        {
            ProductNameTextBox.Text = "";
            ProductGradeNameTextBox.Text = "";
            ProductCategoryCodeComboBox.SelectedIndex = -1;
            ApplyNationCodeComboBox.SelectedIndex = -1;
            ApplyRegionCodeComboBox.SelectedIndex = -1;
            CurrencyCodeComboBox.SelectedIndex = -1;

            listNameCheckBox.Checked = false;
            passportCheckBox.Checked = false;
            arragementCheckBox.Checked = false;
            voucherCheckBox.Checked = false;
            airlineCheckBox.Checked = false;
            insuranceCheckBox.Checked = false;
            personalInformationCheckBox.Checked = false;
        }

        //=========================================================================================================================================================================
        // 저장버튼 클릭
        //=========================================================================================================================================================================
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (CheckRequireItems() == false) return;

            int retVal = 0;
            string query = "";
            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열

            VoPRDT_NM = ProductNameTextBox.Text.Trim();
            VoPRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(ProductCategoryCodeComboBox);
            VoAPLY_NATN_CD = Utils.GetSelectedComboBoxItemValue(ApplyNationCodeComboBox);
            VoAPLY_NATN_REGN_CD = Utils.GetSelectedComboBoxItemValue(ApplyRegionCodeComboBox);

            if (listNameCheckBox.Checked == true)
                VoLST_CHCK_NEED_YN = "Y";
            else
                VoLST_CHCK_NEED_YN = "N";

            if (passportCheckBox.Checked == true)
                VoPSPT_CHCK_NEED_YN = "Y";
            else
                VoPSPT_CHCK_NEED_YN = "N";

            if (arragementCheckBox.Checked == true)
                VoARGM_CHCK_NEED_YN = "Y";
            else
                VoARGM_CHCK_NEED_YN = "N";

            if (voucherCheckBox.Checked == true)
                VoVOCH_CHCK_NEED_YN = "Y";
            else
                VoVOCH_CHCK_NEED_YN = "N";

            if (insuranceCheckBox.Checked == true)
                VoISRC_CHCK_NEED_YN = "Y";
            else
                VoISRC_CHCK_NEED_YN = "N";

            if (airlineCheckBox.Checked == true)
                VoAVAT_CHCK_NEED_YN = "Y";
            else
                VoAVAT_CHCK_NEED_YN = "N";

            if (personalInformationCheckBox.Checked == true)
                VoPRSN_CHCK_NEED_YN = "Y";
            else
                VoPRSN_CHCK_NEED_YN = "N";

            VoAPLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                  // 적용개시일자
            VoAPLY_END_DT = ApplyEndDateTimePicker.Text.Substring(0, 10);                      // 적용종료일자

            VoUSE_YN = "Y";
            VoCNSM_FILE_APLY_YN = "N";
            VoFRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 상품기본정보 생성
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            {
                query = string.Format("CALL InsertProductInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    VoPRDT_NM,
                    VoPRDT_CTGR_CD,
                    VoAPLY_NATN_CD,
                    VoAPLY_NATN_REGN_CD,
                    VoLST_CHCK_NEED_YN,
                    VoPSPT_CHCK_NEED_YN,
                    VoARGM_CHCK_NEED_YN,
                    VoVOCH_CHCK_NEED_YN,
                    VoISRC_CHCK_NEED_YN,
                    VoAVAT_CHCK_NEED_YN,
                    VoPRSN_CHCK_NEED_YN,
                    VoAPLY_LNCH_DT,
                    VoAPLY_END_DT,
                    VoUSE_YN,
                    VoCNSM_FILE_APLY_YN,
                    VoFRST_RGTR_ID);
            }

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            VoPRDT_CNMB = queryResultArray[0];

            if (retVal == -1 || VoPRDT_CNMB.Equals(""))
            {
                MessageBox.Show("상품기본정보를 저장할 수 없습니다.");
                return;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 상품상세목록 생성
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            {
                query = string.Format("CALL InsertProductDetailInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                        VoPRDT_CNMB,
                        VoPRDT_GRAD_CD,
                        VoPRDT_GRAD_NM,
                        VoPRDT_CTGR_CD,
                        VoUSE_YN,
                        VoFRST_RGTR_ID
                );
            }

            queryStringArray[0] = query;

            queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("상품상세목록을 저장할 수 없습니다.");
                return;
            }

            VoPRDT_GRAD_CD = queryResultArray[0];

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 상품가격정보 생성
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            {
                query = string.Format("CALL InsertProductDetailPrice('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                        VoPRDT_CNMB,
                        VoPRDT_GRAD_CD,
                        VoSALE_CUR_CD,
                        VoADLT_SALE_PRCE,
                        VoCHLD_SALE_PRCE,
                        VoINFN_SALE_PRCE,
                        VoUSE_YN,
                        VoAPLY_LNCH_DT,
                        VoAPLY_END_DT,
                        VoFRST_RGTR_ID
                );
            }

            queryStringArray[0] = query;

            queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            retVal = queryResult.Item1;

            if (retVal == -1)
            {
                MessageBox.Show("상품가격정보를 저장할 수 없습니다.");
                return;
            }

            this.Close();
        }

        //=========================================================================================================================================================================
        // 입력값 유효성 검증
        //=========================================================================================================================================================================
        private bool CheckRequireItems()
        {
            if (ProductCategoryCodeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품카테고리를 선택하세요.");
                ProductCategoryCodeComboBox.Focus();
                return false;
            }

            if (ProductNameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("상품명은 필수 입력항목입니다.");
                ProductNameTextBox.Focus();
                return false;
            }

            if (ProductGradeNameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("상품등급명은 필수 입력항목입니다.");
                ProductGradeNameTextBox.Focus();
                return false;
            }

            if (ApplyNationCodeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("적용국가를 선택하세요.");
                ApplyNationCodeComboBox.Focus();
                return false;
            }
            /*
            if (ApplyRegionCodeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("적용지역을 선택하세요.");
                ApplyRegionCodeComboBox.Focus();
                return false;
            }
            */
            if (CurrencyCodeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("통화코드를 선택하세요.");
                CurrencyCodeComboBox.Focus();
                return false;
            }

            if (AdultSalePriceTextBox.Text.Trim().Equals("")) AdultSalePriceTextBox.Text = "0";
            if (ChildSalePriceTextBox.Text.Trim().Equals("")) ChildSalePriceTextBox.Text = "0";
            if (InfantSalePriceTextBox.Text.Trim().Equals("")) InfantSalePriceTextBox.Text = "0";

            VoPRDT_CTGR_CD = Utils.GetSelectedComboBoxItemValue(ProductCategoryCodeComboBox);       // 상품카테고리코드
            VoPRDT_NM = ProductNameTextBox.Text.Trim();                                             // 상품명
            VoPRDT_GRAD_NM = ProductGradeNameTextBox.Text.Trim();                                   // 상품명
            VoSALE_CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);               // 판매통화코드
            VoADLT_SALE_PRCE = AdultSalePriceTextBox.Text.Trim();
            if (VoADLT_SALE_PRCE.Equals("")) VoADLT_SALE_PRCE = "0";
            VoCHLD_SALE_PRCE = ChildSalePriceTextBox.Text.Trim();
            if (VoCHLD_SALE_PRCE.Equals("")) VoCHLD_SALE_PRCE = "0";
            VoINFN_SALE_PRCE = InfantSalePriceTextBox.Text.Trim();
            if (VoINFN_SALE_PRCE.Equals("")) VoINFN_SALE_PRCE = "0";
            VoAPLY_NATN_CD = Utils.GetSelectedComboBoxItemValue(ApplyNationCodeComboBox);           // 적용국가코드
            VoAPLY_NATN_REGN_CD = Utils.GetSelectedComboBoxItemValue(ApplyRegionCodeComboBox);      // 적용국가지역코드
            VoAPLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                         // 적용개시일자
            VoAPLY_END_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                          // 적용종료일자
            // 적용종료일
            VoUSE_YN = "Y";                                                                                                                       // 사용여부
            VoCNSM_FILE_APLY_YN = "N";                                                              // 구매자파일적용여부

            if (listNameCheckBox.Checked == true)
                VoLST_CHCK_NEED_YN = "Y";
            else
                VoLST_CHCK_NEED_YN = "N";

            if (passportCheckBox.Checked == true)
                VoPSPT_CHCK_NEED_YN = "Y";
            else
                VoPSPT_CHCK_NEED_YN = "N";

            if (arragementCheckBox.Checked == true)
                VoARGM_CHCK_NEED_YN = "Y";
            else
                VoARGM_CHCK_NEED_YN = "N";

            if (voucherCheckBox.Checked == true)
                VoVOCH_CHCK_NEED_YN = "Y";
            else
                VoVOCH_CHCK_NEED_YN = "N";

            if (insuranceCheckBox.Checked == true)
                VoISRC_CHCK_NEED_YN = "Y";
            else
                VoISRC_CHCK_NEED_YN = "N";

            if (airlineCheckBox.Checked == true)
                VoAVAT_CHCK_NEED_YN = "Y";
            else
                VoAVAT_CHCK_NEED_YN = "N";

            if (personalInformationCheckBox.Checked == true)
                VoPRSN_CHCK_NEED_YN = "Y";
            else
                VoPRSN_CHCK_NEED_YN = "N";

            return true;
        }

        public string GetProductNumber()
        {
            return VoPRDT_CNMB;
        }

        public string GetProductGradeCode()
        {
            return VoPRDT_GRAD_CD;
        }

        //=========================================================================================================================================================================
        // 국가코드가 변경되면 지역코드를 검색하여 콤보박스에 설정
        //=========================================================================================================================================================================
        private void ApplyRegionCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setNationRegionList(Utils.GetSelectedComboBoxItemValue(ApplyNationCodeComboBox));
        }

        //=========================================================================================================================================================================
        // 국가지역코드 콤보박스 세팅
        //=========================================================================================================================================================================
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

        //=========================================================================================================================================================================
        // 폼을 닫고 상품일련번호와 상품등급코드값을 리턴
        //=========================================================================================================================================================================
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplyNationCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setNationRegionList(Utils.GetSelectedComboBoxItemValue(ApplyNationCodeComboBox));
        }
    }
}
