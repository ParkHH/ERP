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
    public partial class OptionMgt : Form
    {
        private string _registrationConsecutiveNumber { get; set; }
        private string _beforeApplyLaunchDate { get; set; }
        private string _beforeApplyEndDate { get; set; }
        private string _optionName { get; set; }

        enum eCostPriceListDataGridView
        {
            PRDT_CNMB,
            PRDT_NM,
            PRDT_GRAD_CD,
            PRDT_GRAD_NM,
            OPTN_CNMB,
            PRDT_OPTN_NM,
            CUR_CD,
            CUR_NM,
            PRDT_OPTN_SALE_AMT,
            APLY_LNCH_DT,
            APLY_END_DT,
            USE_YN_NM,
            USE_YN,
            RGST_CNMB
        };

        public OptionMgt()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void OptionMgt_Load(object sender, EventArgs e)
        {
            InitControls();
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
            DataGridView dataGridView1 = OptionListDataGridView;
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

            OptionNo.Text = "";
            OptionNameTextBox.Text = "";
            OptionAmountTextBox.Text = "";
            ApplyLaunchDateTimePicker.Text = "";
            ApplyEndDateTimePicker.Text = "";

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
        }

        // 목록조회버튼 클릭

        private void searchCostPriceListButton_Click_1(object sender, EventArgs e)
        {
            searchOptionList();
        }

        // 상품옵션내역 목록조회
        private void searchOptionList()
        {
            OptionListDataGridView.Rows.Clear();

            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(SearchProductNameComboBox);
            string PRDT_NM = "";
            string PRDT_GRAD_CD = Utils.GetSelectedComboBoxItemValue(SearchProductGradeNameComboBox);
            string PRDT_GRAD_NM = "";
            string OPTN_CNMB = "";                                             // 옵션일련번호
            string PRDT_OPTN_NM = "";                                          // 옵션명
            string CUR_CD = "";                                                // 통화코드
            string CUR_NM = "";                                                // 통화명
            string PRDT_OPTN_SALE_AMT = "";                                    // 옵션금액
            string APLY_LNCH_DT = "";                                          // 적용개시일
            string APLY_END_DT = "";                                           // 적용종료일
            string USE_YN_NM = "";                                             // 사용여부명
            string USE_YN = "";                                                // 사용여부
            string RGST_CNMB = "";                                             // 등록일련번호

            string query = string.Format("CALL SelectOptionList ('{0}', '{1}')", PRDT_CNMB, PRDT_GRAD_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품옵션내역정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {

                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();
                PRDT_NM = datarow["PRDT_NM"].ToString();
                PRDT_GRAD_CD = datarow["PRDT_GRAD_CD"].ToString();
                PRDT_GRAD_NM = datarow["PRDT_GRAD_NM"].ToString();
                OPTN_CNMB = datarow["OPTN_CNMB"].ToString();
                PRDT_OPTN_NM = datarow["PRDT_OPTN_NM"].ToString();
                CUR_CD = datarow["CUR_CD"].ToString();
                CUR_NM = datarow["CUR_NM"].ToString();
                PRDT_OPTN_SALE_AMT = datarow["PRDT_OPTN_SALE_AMT"].ToString();
                APLY_LNCH_DT = datarow["APLY_LNCH_DT"].ToString().Substring(0, 10);
                APLY_END_DT = datarow["APLY_END_DT"].ToString().Substring(0, 10);
                USE_YN_NM = datarow["USE_YN_NM"].ToString();
                USE_YN = datarow["USE_YN"].ToString();
                RGST_CNMB = datarow["RGST_CNMB"].ToString();

                OptionListDataGridView.Rows.Add(PRDT_CNMB, PRDT_NM, PRDT_GRAD_CD, PRDT_GRAD_NM, OPTN_CNMB, PRDT_OPTN_NM, CUR_CD, CUR_NM, PRDT_OPTN_SALE_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN_NM, USE_YN, RGST_CNMB);
            }
            // 목록조회 후 그리드 선택 해제
            OptionListDataGridView.ClearSelection();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);             // 상품일련번호
            string PRDT_GRAD_CD = Utils.GetSelectedComboBoxItemValue(ProductGradeCodeComboBox);     // 상품등급코드;
            string OPTN_CNMB = OptionNo.Text.Trim();                                                // 옵션일련번호
            string PRDT_OPTN_NM = OptionNameTextBox.Text.Trim();                                    // 옵션명
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);               // 통화코드
            string PRDT_OPTN_SALE_AMT = OptionAmountTextBox.Text.Trim();                            // 옵션금액
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

            if (CUR_CD == "")
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

            string OPTN_CNMB = OptionNo.Text.Trim();                                             // 원가일련번호
            if (OPTN_CNMB == "")
            {
                MessageBox.Show("옵션번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteOptionInfo ('{0}', '{1}', '{2}', '{3}')", PRDT_CNMB, PRDT_GRAD_CD, OPTN_CNMB, _registrationConsecutiveNumber);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show(" 옵션정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                searchOptionList();
                MessageBox.Show("옵션정보를 삭제했습니다.");
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        // 폼 닫기
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);             // 상품일련번호
            string PRDT_GRAD_CD = Utils.GetSelectedComboBoxItemValue(ProductGradeCodeComboBox);     // 상품등급코드;
            string OPTN_CNMB = OptionNo.Text.Trim();                                                // 옵션일련번호
            string PRDT_OPTN_NM = OptionNameTextBox.Text.Trim();                                    // 옵션명
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(CurrencyCodeComboBox);               // 통화코드
            string PRDT_OPTN_SALE_AMT = OptionAmountTextBox.Text.Trim();                            // 옵션금액
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Text.Substring(0, 10);                  // 적용개시일자
            string APLY_END_DT = ApplyEndDateTimePicker.Text.Substring(0, 10);                      // 적용종료일자
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                      // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            // 옵션번호가 없으면 MAX+1로 채번하고 Insert처리
            if (OPTN_CNMB == "" && (_registrationConsecutiveNumber == "" || _registrationConsecutiveNumber == null))
            {
                query = string.Format("CALL InsertOptionInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                            PRDT_CNMB, PRDT_GRAD_CD, OPTN_CNMB, PRDT_OPTN_NM, CUR_CD, PRDT_OPTN_SALE_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
            }
            else
            {
                // 수배처, 적용개시일, 적용종료일이 동일할 경우 Update
                if (APLY_LNCH_DT == _beforeApplyLaunchDate && APLY_END_DT == _beforeApplyEndDate && USE_YN == "Y" && PRDT_OPTN_NM == _optionName)
                {
                    query = string.Format("CALL UpdateOptionInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')",
                            PRDT_CNMB, PRDT_GRAD_CD, OPTN_CNMB, _registrationConsecutiveNumber,  PRDT_OPTN_NM, CUR_CD, PRDT_OPTN_SALE_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
                }
                else
                {
                    query = string.Format("CALL InsertOptionInfo ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                            PRDT_CNMB, PRDT_GRAD_CD, OPTN_CNMB, PRDT_OPTN_NM, CUR_CD, PRDT_OPTN_SALE_AMT, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
                }
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("상품옵션정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                searchOptionList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("상품옵션정보를 저장했습니다.");
            }

            // 저장 후 입력폼 초기화
            ResetInputFormField();
        }

        // 초기화버튼 클릭
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 그리드 행 클릭
        private void OptionListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OptionListDataGridView.SelectedRows.Count == 0)
                return;

            string PRDT_CNMB = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_CNMB].Value.ToString();
            string PRDT_NM = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_NM].Value.ToString();
            string PRDT_GRAD_CD = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_GRAD_CD].Value.ToString();
            string PRDT_GRAD_NM = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_GRAD_NM].Value.ToString();
            string OPTN_CNMB = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.OPTN_CNMB].Value.ToString();
            string PRDT_OPTN_NM = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_OPTN_NM].Value.ToString();
            string CUR_CD = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.CUR_CD].Value.ToString();
            string CUR_NM = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.CUR_NM].Value.ToString();
            string PRDT_OPTN_SALE_AMT = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.PRDT_OPTN_SALE_AMT].Value.ToString();
            string APLY_LNCH_DT = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.APLY_LNCH_DT].Value.ToString();
            string APLY_END_DT = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.APLY_END_DT].Value.ToString();
            string USE_YN_NM = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.USE_YN_NM].Value.ToString();
            string USE_YN = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.USE_YN].Value.ToString();
            string RGST_CNMB = OptionListDataGridView.SelectedRows[0].Cells[(int)eCostPriceListDataGridView.RGST_CNMB].Value.ToString();

            _registrationConsecutiveNumber = RGST_CNMB;
            _beforeApplyEndDate = APLY_END_DT;
            _beforeApplyLaunchDate = APLY_LNCH_DT;
            _optionName = PRDT_OPTN_NM;

            Utils.SelectComboBoxItemByValue(ProductCodeComboBox, PRDT_CNMB);          // 상품일련번호
            Utils.SelectComboBoxItemByValue(ProductGradeCodeComboBox, PRDT_GRAD_CD);  // 상품등급코드
            OptionNo.Text = OPTN_CNMB;                                                // 원가번호
            OptionNameTextBox.Text = PRDT_OPTN_NM;                                    // 원가명
            Utils.SelectComboBoxItemByValue(CurrencyCodeComboBox, CUR_CD);            // 통화코드
            OptionAmountTextBox.Text = PRDT_OPTN_SALE_AMT;                            // 성인외화원가금액
            ApplyLaunchDateTimePicker.Text = APLY_LNCH_DT;                            // 적용개시일
            ApplyEndDateTimePicker.Text = APLY_END_DT;                                // 적용종료일
            Utils.SelectComboBoxItemByValue(UseYnComboBox, USE_YN);                   // 사용여부
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
                MessageBox.Show("선택한 상품번호에 대한 상품등급정보를 가져올 수 없습니다.");
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

        // 
        private void SearchProductNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(SearchProductNameComboBox);  // 상품일련번호

            SearchProductGradeNameComboBox.Items.Clear();
            string query = string.Format("CALL SelectProductGradeCodeByProductNo ('{0}')", PRDT_CNMB);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("선택한 상품번호에 대한 상품등급정보를 가져올 수 없습니다.");
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
    }
}
