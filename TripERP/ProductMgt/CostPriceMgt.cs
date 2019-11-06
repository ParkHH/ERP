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
    public partial class CostPriceMgt : Form
    {
        private string _registrationConsecutiveNumber { get; set; }
        private string _beforeApplyLaunchDate { get; set; }
        private string _beforeApplyEndDate { get; set; }
        private string _beforeArrangementCompanyNumber { get; set; }
        private string _costPriceName { get; set; }

        enum eCostPriceListDataGridView
        {
            PRDT_CNMB,
            PRDT_NM,
            PRDT_GRAD_CD,
            PRDT_GRAD_NM,
            CSPR_CNMB,
            CSPR_NM,
            ARPL_CMPN_NO,
            ARPL_CMPN_NM,
            CSPR_CUR_CD,
            CSPR_CUR_NM,
            ADLT_FRCR_CSPR_AMT,
            CHLD_FRCR_CSPR_AMT,
            INFN_FRCR_CSPR_AMT,
            APLY_LNCH_DT,
            APLY_END_DT,
            USE_YN_NM,
            USE_YN,
            RGST_CNMB
        };

        public CostPriceMgt()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void CostPriceMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            searchCostPriceList();                  // Form Load 시 내용 바로 출력
        }

        // 초기화
        private void InitControls()
        {

            // 폼 입력필드 초기화
            ResetInputFormField();

            // 그리드 스타일 초기화
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView1 = CostPriceListDataGridView;
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

        // 입력폼 초기화
        private void ResetInputFormField()
        {
            ProductGradeCodeComboBox.Items.Clear();
            ProductGradeCodeComboBox.Text = "";

            CostPriceNo.Text = "";
            CostPriceNameTextBox.Text = "";
            AdultCostPriceTextBox.Text = "";
            ChildCostPriceTextBox.Text = "";
            InfantCostPriceTextBox.Text = "";
            ApplyLaunchDateTimePicker.Text = "";
            ApplyEndDateTimePicker.Text = "";

            _registrationConsecutiveNumber = "";
            _beforeApplyEndDate = "";
            _beforeApplyLaunchDate = "";
            _beforeArrangementCompanyNumber = "";
            _costPriceName = "";

            // 사용여부
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

            // 상품명
            ProductCodeComboBox.Items.Clear();
            ProductCodeComboBox.Text = "";

            query = "CALL SelectPrdtList";
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
                ProductCodeComboBox.Items.Add(item);
                SearchProductNameComboBox.Items.Add(item);
            }

            ProductCodeComboBox.SelectedIndex = -1;

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

            // 수배처 콤보박스 초기화
            CompanyNoComboBox.Items.Clear();
            CompanyNoComboBox.Text = "";

            query = "CALL SelectDestinationCompanyList()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("수배처 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                CompanyNoComboBox.Items.Add(item);
            }

            CompanyNoComboBox.SelectedIndex = -1;
        }

        // 목록조회버튼 클릭
        private void searchCostPriceListButton_Click(object sender, EventArgs e)
        {
            searchCostPriceList();
        }

        // 상품원가내역 목록조회
        private void searchCostPriceList()
        {
            CostPriceListDataGridView.Rows.Clear();

            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(SearchProductNameComboBox);            
            string PRDT_NM = "";
            string PRDT_GRAD_CD = Utils.GetSelectedComboBoxItemValue(SearchProductGradeNameComboBox);
            string PRDT_GRAD_NM = "";
            string CSPR_CNMB = "";                                             // 원가일련번호
            string CSPR_NM = "";                                               // 원가명
            string ARPL_CMPN_NO = "";                                          // 수배처업체번호
            string ARPL_CMPN_NM = "";                                          // 수배처업체명
            string CSPR_CUR_CD = "";                                           // 원가통화코드
            string CSPR_CUR_NM = "";                                           // 원가통화명
            string ADLT_FRCR_CSPR_AMT = "";                                    // 성인외화원가금액
            string CHLD_FRCR_CSPR_AMT = "";                                    // 소아외화원가금액
            string INFN_FRCR_CSPR_AMT = "";                                    // 유아외화원가금액
            string APLY_LNCH_DT = "";                                          // 적용개시일
            string APLY_END_DT = "";                                           // 적용종료일
            string USE_YN_NM = "";                                             // 사용여부명
            string USE_YN = "";                                                // 사용여부
            string RGST_CNMB = "";                                             // 등록일련번호

            string query = string.Format("CALL SelectCostPriceList ('{0}', '{1}')", PRDT_CNMB, PRDT_GRAD_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품원가내역정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();
                PRDT_NM = datarow["PRDT_NM"].ToString();
                PRDT_GRAD_CD = datarow["PRDT_GRAD_CD"].ToString();
                PRDT_GRAD_NM = datarow["PRDT_GRAD_NM"].ToString();
                CSPR_CNMB = datarow["CSPR_CNMB"].ToString();
                CSPR_NM = datarow["CSPR_NM"].ToString();
                ARPL_CMPN_NO = datarow["ARPL_CMPN_NO"].ToString();
                ARPL_CMPN_NM = datarow["ARPL_CMPN_NM"].ToString();
                CSPR_CUR_CD = datarow["CSPR_CUR_CD"].ToString();
                CSPR_CUR_NM = datarow["CSPR_CUR_NM"].ToString();
                ADLT_FRCR_CSPR_AMT = datarow["ADLT_FRCR_CSPR_AMT"].ToString();
                CHLD_FRCR_CSPR_AMT = datarow["CHLD_FRCR_CSPR_AMT"].ToString();
                INFN_FRCR_CSPR_AMT = datarow["INFN_FRCR_CSPR_AMT"].ToString();
                APLY_LNCH_DT = datarow["APLY_LNCH_DT"].ToString().Substring(0, 10);
                APLY_END_DT = datarow["APLY_END_DT"].ToString().Substring(0, 10);
                USE_YN_NM = datarow["USE_YN_NM"].ToString();
                USE_YN = datarow["USE_YN"].ToString();
                RGST_CNMB = datarow["RGST_CNMB"].ToString();

                CostPriceListDataGridView.Rows.Add
                (
                    PRDT_CNMB, 
                    PRDT_NM, 
                    PRDT_GRAD_CD, 
                    PRDT_GRAD_NM, 
                    CSPR_CNMB, 
                    CSPR_NM, 
                    ARPL_CMPN_NO, 
                    ARPL_CMPN_NM, 
                    CSPR_CUR_CD, 
                    CSPR_CUR_NM, 
                    double.Parse(ADLT_FRCR_CSPR_AMT), 
                    double.Parse(CHLD_FRCR_CSPR_AMT), 
                    double.Parse(INFN_FRCR_CSPR_AMT), 
                    APLY_LNCH_DT, 
                    APLY_END_DT, 
                    USE_YN_NM, 
                    USE_YN, 
                    RGST_CNMB
                );
            }

            CostPriceListDataGridView.ClearSelection();
        }

        // 그리드 행 클릭
        private void CostPriceListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CostPriceListDataGridView.SelectedRows.Count == 0)
                return;

            string PRDT_CNMB = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_CNMB].Value.ToString();
            string PRDT_NM = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_NM].Value.ToString();
            string PRDT_GRAD_CD = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_GRAD_CD].Value.ToString();
            string PRDT_GRAD_NM = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_GRAD_NM].Value.ToString();
            string CSPR_CNMB = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.CSPR_CNMB].Value.ToString();
            string CSPR_NM = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.CSPR_NM].Value.ToString();
            string ARPL_CMPN_NO = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.ARPL_CMPN_NO].Value.ToString();
            string ARPL_CMPN_NM = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.ARPL_CMPN_NM].Value.ToString();
            string CSPR_CUR_CD = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.CSPR_CUR_CD].Value.ToString();
            string CSPR_CUR_NM = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.CSPR_CUR_NM].Value.ToString();
            string ADLT_FRCR_CSPR_AMT = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.ADLT_FRCR_CSPR_AMT].Value.ToString();
            string CHLD_FRCR_CSPR_AMT = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.CHLD_FRCR_CSPR_AMT].Value.ToString();
            string INFN_FRCR_CSPR_AMT = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.INFN_FRCR_CSPR_AMT].Value.ToString();
            string APLY_LNCH_DT = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.APLY_LNCH_DT].Value.ToString();
            string APLY_END_DT = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.APLY_END_DT].Value.ToString();
            string USE_YN_NM = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.USE_YN_NM].Value.ToString();
            string USE_YN = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.USE_YN].Value.ToString();
            string RGST_CNMB = CostPriceListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.RGST_CNMB].Value.ToString();

            _registrationConsecutiveNumber = RGST_CNMB;
            _beforeApplyEndDate = APLY_END_DT;
            _beforeApplyLaunchDate = APLY_LNCH_DT;
            _beforeArrangementCompanyNumber = ARPL_CMPN_NO;
            _costPriceName = CSPR_NM;

            Utils.SelectComboBoxItemByValue(ProductCodeComboBox, PRDT_CNMB);             // 상품일련번호
            Utils.SelectComboBoxItemByValue(ProductGradeCodeComboBox, PRDT_GRAD_CD);     // 상품등급코드
            CostPriceNo.Text = CSPR_CNMB;                                                // 원가번호
            CostPriceNameTextBox.Text = CSPR_NM;                                         // 원가명
            Utils.SelectComboBoxItemByValue(CompanyNoComboBox, ARPL_CMPN_NO);            // 수배처업체명
            Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CSPR_CUR_CD);          // 통화코드
            AdultCostPriceTextBox.Text = ADLT_FRCR_CSPR_AMT;                             // 성인외화원가금액
            ChildCostPriceTextBox.Text = CHLD_FRCR_CSPR_AMT;                             // 소아외화원가금액
            InfantCostPriceTextBox.Text = INFN_FRCR_CSPR_AMT;                            // 유아외화원가금액
            ApplyLaunchDateTimePicker.Text = APLY_LNCH_DT;                               // 적용개시일
            ApplyEndDateTimePicker.Text = APLY_END_DT;                                   // 적용종료일
            Utils.SelectComboBoxItemByValue(UseYnComboBox, USE_YN);                      // 사용여부
        }

        // 상품명이 바뀌면 해당되는 상품등급을 콤보에 설정
        private void ProductCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);  // 상품일련번호

            ProductGradeCodeComboBox.Items.Clear();
            string query = string.Format("CALL SelectProductGradeCodeByProductNo ('{0}')", PRDT_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("선택한 상품번호에 대한 상품카테고리정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 상품카테고리코드와 상품카테고리명을 콤보에 설정
                string PRDT_GRAD_CD = dataRow["PRDT_GRAD_CD"].ToString();
                string PRDT_GRAD_NM = dataRow["PRDT_GRAD_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_GRAD_NM, PRDT_GRAD_CD);
                ProductGradeCodeComboBox.Items.Add(item);
            }

            ProductGradeCodeComboBox.SelectedIndex = -1;
        }

        // 초기화버튼 클릭
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);             // 상품일련번호
            string PRDT_GRAD_CD = Utils.GetSelectedComboBoxItemValue(ProductGradeCodeComboBox);     // 상품등급코드;
            string CSPR_CNMB = CostPriceNo.Text.Trim();                                             // 원가일련번호
            string CSPR_NM = CostPriceNameTextBox.Text.Trim();                                      // 원가명
            string ARPL_CMPN_NO = Utils.GetSelectedComboBoxItemValue(CompanyNoComboBox);            // 수배처업체번호
            string CSPR_CUR_CD =  Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);         // 원가통화코드

            if (AdultCostPriceTextBox.Text.Trim() == "") AdultCostPriceTextBox.Text = "0";
            if (ChildCostPriceTextBox.Text.Trim() == "") ChildCostPriceTextBox.Text = "0";
            if (InfantCostPriceTextBox.Text.Trim() == "") InfantCostPriceTextBox.Text = "0";

            string ADLT_FRCR_CSPR_AMT = AdultCostPriceTextBox.Text.Trim();                          // 성인외화원가금액
            string CHLD_FRCR_CSPR_AMT = ChildCostPriceTextBox.Text.Trim();                          // 소아외화원가금액
            string INFN_FRCR_CSPR_AMT = InfantCostPriceTextBox.Text.Trim();                         // 유아외화원가금액
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                  // 적용개시일자
            string APLY_END_DT = ApplyEndDateTimePicker.Text.Substring(0, 10);                      // 적용종료일자
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                      // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            // 등급코드가 없으면 MAX+1로 채번하고 Insert처리
            if (CSPR_CNMB == "" && (_registrationConsecutiveNumber == "" || _registrationConsecutiveNumber == null))
            {
                query = string.Format("CALL SelectCostPriceByDate ('{0}', '{1}', '{2}', '{3}', '{4}')",
                        PRDT_CNMB, PRDT_GRAD_CD, APLY_LNCH_DT, APLY_END_DT, CSPR_CNMB);
                DataSet dataSet = DbHelper.SelectQuery(query);

                /**
                 * [ 등록 성공 ]
                 * 새로 등록하는 원가정보가 기존의 동일한 상품일련번호, 등급코드의 원가정보의 개시일자와 겹치지 않는 경우
                 * */
                if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
                {
                    query = string.Format("CALL InsertCostPriceInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                            PRDT_CNMB, PRDT_GRAD_CD, CSPR_CNMB, CSPR_NM, ARPL_CMPN_NO, CSPR_CUR_CD, ADLT_FRCR_CSPR_AMT, CHLD_FRCR_CSPR_AMT, INFN_FRCR_CSPR_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
                }
                /**
                 * [ 등록 실패 ]
                 * 새로 등록하는 원가정보가 기존의 동일한 상품일련번호, 등급코드의 원가정보의 개시일자와 겹치는 경우
                 * */
                else
                {
                    MessageBox.Show("동일한 상품명, 등급의 원가정보와 적용개시일자가 겹칩니다.\n적용개시일자를 다르게 설정해주세요.");
                    return;
                }

            }
            /**
             * DataGridView에서 선택후 작업을 하는 경우.
             * */
            else
            {
                // 수배처, 적용개시일, 적용종료일이 동일할 경우 Update
                //if (APLY_LNCH_DT == _beforeApplyLaunchDate && APLY_END_DT == _beforeApplyEndDate && USE_YN == "Y" && ARPL_CMPN_NO == _beforeArrangementCompanyNumber && CSPR_NM == _costPriceName)
                query = string.Format("CALL SelectCostPriceByDate ('{0}', '{1}', '{2}', '{3}', '{4}')",
                        PRDT_CNMB, PRDT_GRAD_CD, APLY_LNCH_DT, APLY_END_DT, CSPR_CNMB);
                DataSet dataSet = DbHelper.SelectQuery(query);

                /**
                 * [ 등록 성공 ]
                 * 새로 등록하는 원가정보가 기존의 동일한 상품일련번호, 등급코드의 원가정보의 개시일자와 겹치지 않는 경우
                 * */
                if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
                {
                    query = string.Format("CALL UpdateCostPriceInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')",
                            PRDT_CNMB, PRDT_GRAD_CD, CSPR_CNMB, _registrationConsecutiveNumber, CSPR_NM, ARPL_CMPN_NO, CSPR_CUR_CD, ADLT_FRCR_CSPR_AMT, CHLD_FRCR_CSPR_AMT, INFN_FRCR_CSPR_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
                }
                /**
                 * [ 등록 실패 ]
                 * 새로 등록하는 원가정보가 기존의 동일한 상품일련번호, 등급코드의 원가정보의 개시일자와 겹치는 경우
                 * */
                else
                {
                    MessageBox.Show("동일한 상품명, 등급의 원가정보와 적용개시일자가 겹칩니다.\n적용개시일자를 다르게 설정해주세요.");
                    return;
                }

                
                //if (APLY_LNCH_DT == _beforeApplyLaunchDate && APLY_END_DT == _beforeApplyEndDate && USE_YN == "Y" && ARPL_CMPN_NO == _beforeArrangementCompanyNumber && CSPR_NM == _costPriceName)
                //{
                    
                //}
                //else
                //{
                //    query = string.Format("CALL InsertCostPriceInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                //            PRDT_CNMB, PRDT_GRAD_CD, CSPR_CNMB, CSPR_NM, ARPL_CMPN_NO, CSPR_CUR_CD, ADLT_FRCR_CSPR_AMT, CHLD_FRCR_CSPR_AMT, INFN_FRCR_CSPR_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
                //}
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("원가정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                searchCostPriceList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("원가정보를 저장했습니다.");
            }

            // 저장 후 입력폼 초기화
            ResetInputFormField();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);             // 상품일련번호
            string PRDT_GRAD_CD = Utils.GetSelectedComboBoxItemValue(ProductGradeCodeComboBox);     // 상품등급코드;
            string CSPR_CNMB = CostPriceNo.Text.Trim();                                             // 원가일련번호
            string CSPR_NM = CostPriceNameTextBox.Text.Trim();                                      // 원가명
            string ARPL_CMPM_NO = Utils.GetSelectedComboBoxItemValue(CompanyNoComboBox);            // 수배처업체번호
            string CSPR_CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);          // 원가통화코드
            string ADLT_FRCR_CSPR_AMT = AdultCostPriceTextBox.Text.Trim();                          // 성인외화원가금액
            string CHLD_FRCR_CSPR_AMT = ChildCostPriceTextBox.Text.Trim();                          // 소아외화원가금액
            string INFN_FRCR_CSPR_AMT = InfantCostPriceTextBox.Text.Trim();                         // 유아외화원가금액
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                  // 적용개시일자
            string APLY_END_DT = ApplyEndDateTimePicker.Text.Substring(0, 10);                      // 적용종료일자
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                      // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            if (PRDT_CNMB == "")
            {
                MessageBox.Show("상품을 선택하세요.");
                ProductCodeComboBox.Focus();
                return false;
            }

            if (PRDT_GRAD_CD == "")
            {
                MessageBox.Show("상품등급을 선택하세요.");
                ProductGradeCodeComboBox.Focus();
                return false;
            }

            if (CSPR_CUR_CD == "")
            {
                MessageBox.Show("통화코드를 선택하세요.");
                CurrencyCodeComboBox.Focus();
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

            if (ApplyEndDateTimePicker.Value < ApplyLaunchDateTimePicker.Value)
            {
                MessageBox.Show("적용종료일이 적용개시일보다 작습니다. 일자를 확인하세요.");
                ApplyEndDateTimePicker.Focus();
                return false;
            }

            if (ARPL_CMPM_NO == "")
            {
                MessageBox.Show("수배처를 선택하세요.");
                CompanyNoComboBox.Focus();
                return false;
            }

            return true;
        }

        // 삭제버튼 클릭
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);  // 상품일련번호
            if (PRDT_CNMB == "")
            {
                MessageBox.Show("상품명은 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                ProductCodeComboBox.Focus();
                return;
            }

            string PRDT_GRAD_CD = Utils.GetSelectedComboBoxItemValue(ProductGradeCodeComboBox);  // 상품일련번호
            if (PRDT_GRAD_CD == "")
            {
                MessageBox.Show("상품등급은 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                ProductGradeCodeComboBox.Focus();
                return;
            }

            string CSPR_CNMB = CostPriceNo.Text.Trim();                                  // 원가일련번호
            if (CSPR_CNMB == "")
            {
                MessageBox.Show("원가번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                ProductGradeCodeComboBox.Focus();
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteCostPriceInfo ('{0}', '{1}', '{2}', '{3}')", PRDT_CNMB, PRDT_GRAD_CD, CSPR_CNMB, _registrationConsecutiveNumber);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show(" 원가정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                searchCostPriceList();
                MessageBox.Show("원가정보를 삭제했습니다.");
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        // 폼 닫기
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 검색 상품명이 바뀌면 상품등급을 세팅
        private void SearchProductNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(SearchProductNameComboBox);  // 상품일련번호

            SearchProductGradeNameComboBox.Items.Clear();
            string query = string.Format("CALL SelectProductGradeCodeByProductNo ('{0}')", PRDT_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("선택한 상품번호에 대한 상품카테고리정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 상품카테고리코드와 상품카테고리명을 콤보에 설정
                string PRDT_GRAD_CD = dataRow["PRDT_GRAD_CD"].ToString();
                string PRDT_GRAD_NM = dataRow["PRDT_GRAD_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_GRAD_NM, PRDT_GRAD_CD);
                SearchProductGradeNameComboBox.Items.Add(item);
            }

            SearchProductGradeNameComboBox.SelectedIndex = -1;
        }

        /**
         * 19. 10. 31(목) - 배장훈
         * 원가의 경우, 수정하는경우 원가명과 등급 Disabled처리
         * */
        private void CostPriceNo_TextChanged(object sender, EventArgs e)
        {
            /**
             * 원가번호 텍스트박스가 비어있지 않은 경우(그리드뷰 선택시)
             * */
            if (!CostPriceNo.Text.Equals(""))
            {
                ProductCodeComboBox.Enabled = false;
                ProductGradeCodeComboBox.Enabled = false;
            }
            /**
             * 원가번호 텍스트박스가 비어있는 경우(초기화 선택시)
             * */
            else
            {
                ProductCodeComboBox.Enabled = true;
                ProductGradeCodeComboBox.Enabled = true;
            }
        }
    }
}
