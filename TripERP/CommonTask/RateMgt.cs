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

namespace TripERP.CommonTask
{
    public partial class RateMgt : Form
    {
        enum eRateListDataGridView { PRDT_NM, CMPN_NM, RATE_TYPE, RATE, APLY_LNCH_DT, APLY_END_DT, USE_YN_NM, USE_YN, PRDT_CNMB, CMPN_NO, RATE_CD };

        public RateMgt()
        {
            InitializeComponent();
        }

        // 폼 닫기
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void InitControls()
        {
            // 상품코드
            ProductCodeComboBox.Items.Clear();
            SearchProductCodeComboBox.Items.Clear();

            string query = "CALL SelectPrdtList";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0) {
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
                SearchProductCodeComboBox.Items.Add(item);
            }

            ProductCodeComboBox.SelectedIndex = -1;
            SearchProductCodeComboBox.SelectedIndex = -1;

            // 업체명
            SearchCooperativeCompanyNoComboBox.Items.Clear();
            CooperativeCompanyNoComboBox.Items.Clear();

            string COOP_CMPN_DVSN_CD = "10";

            query = string.Format("CALL SelectCoopCmpnList('{0}', '{1}')", COOP_CMPN_DVSN_CD, ' ');
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("협럭업체기본정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 업체번호와 업체명을 콤보에 설정
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);
                CooperativeCompanyNoComboBox.Items.Add(item);
                SearchCooperativeCompanyNoComboBox.Items.Add(item);
            }

            CooperativeCompanyNoComboBox.SelectedIndex = -1;
            SearchCooperativeCompanyNoComboBox.SelectedIndex = -1;

            // 요율구분
            RateCodeComboBox.Items.Clear();

            query = "CALL SelectCommonCodeList('RATE_CD')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공통코드유효값 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 업체번호와 업체명을 콤보에 설정
                string CMPN_NO = dataRow["CD_VLID_VAL"].ToString();
                string CMPN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);
                RateCodeComboBox.Items.Add(item);
            }

            RateCodeComboBox.SelectedIndex = -1;

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
            }

            UseYnComboBox.SelectedIndex = 0;

            // 그리드 스타일 초기화
            InitDataGridView();

            SearchRateList();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = RateListDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        private void SearchRateList()
        {
            RateListDataGridView.Rows.Clear();

            //{ PRDT_NM, CMPN_NM, RATE_TYPE, RATE, APLY_LNCH_DT, APLY_END_DT, USE_YN_NM, USE_YN, PRDT_CNMB, CMPN_NO, RATE_CD }

            string PRDT_NM = SearchProductCodeComboBox.Text.Trim();
            string CMPN_NM = SearchCooperativeCompanyNoComboBox.Text.Trim();
            string RATE_TYPE = "";
            string RATE = "";
            string APLY_LNCH_DT = "";
            string APLY_END_DT = "";
            string USE_YN_NM = "";
            string USE_YN = "";
            string PRDT_CNMB = "";
            string CMPN_NO = "";
            string RATE_CD = "";

            string query = string.Format("CALL SelectRateList ('{0}', '{1}')", PRDT_NM, CMPN_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("요율정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                PRDT_NM = datarow["PRDT_NM"].ToString();
                CMPN_NM = datarow["CMPN_NM"].ToString();
                RATE_TYPE = datarow["RATE_TYPE"].ToString();
                RATE = datarow["RATE"].ToString();
                APLY_LNCH_DT = datarow["APLY_LNCH_DT"].ToString().Substring(0, 10);
                APLY_END_DT = datarow["APLY_END_DT"].ToString().Substring(0, 10);
                USE_YN_NM = datarow["USE_YN_NM"].ToString();
                USE_YN = datarow["USE_YN"].ToString();
                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();
                CMPN_NO = datarow["CMPN_NO"].ToString();
                RATE_CD = datarow["RATE_CD"].ToString();

                RateListDataGridView.Rows.Add(PRDT_NM, CMPN_NM, RATE_TYPE, RATE, APLY_LNCH_DT, APLY_END_DT, USE_YN_NM, USE_YN, PRDT_CNMB, CMPN_NO, RATE_CD);
            }

            RateListDataGridView.ClearSelection();
        }

        // 검색버튼 클릭
        private void searchRateListButton_Click(object sender, EventArgs e)
        {
            SearchRateList();
        }

        // 그리드 행 클릭
        private void RateListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RateListDataGridView.SelectedRows.Count == 0)
                return;

            string PRDT_NM = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.PRDT_NM].Value.ToString();
            string CMPN_NM = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.CMPN_NM].Value.ToString();
            string RATE_TYPE = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.RATE_TYPE].Value.ToString();
            string RATE = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.RATE].Value.ToString();
            string APLY_LNCH_DT = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_LNCH_DT].Value.ToString();
            string APLY_END_DT = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.APLY_END_DT].Value.ToString();
            string USE_YN_NM = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.USE_YN_NM].Value.ToString();
            string USE_YN = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.USE_YN].Value.ToString();
            string RATE_CD = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.RATE_CD].Value.ToString();
            string PRDT_CNMB = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.PRDT_CNMB].Value.ToString();
            string CMPN_NO = RateListDataGridView.SelectedRows[0].Cells[(int)eRateListDataGridView.CMPN_NO].Value.ToString();

            Utils.SelectComboBoxItemByValue(ProductCodeComboBox, PRDT_CNMB);         // 상품일련번호
            Utils.SelectComboBoxItemByValue(CooperativeCompanyNoComboBox, CMPN_NO);  // 업체번호
            Utils.SelectComboBoxItemByValue(RateCodeComboBox, RATE_CD);              // 요율코드

            RateTextBox.Text = RATE;
            ApplyLaunchDateTimePicker.Text = APLY_LNCH_DT;
            ApplyEndDateTimePicker.Text = APLY_END_DT;

            Utils.SelectComboBoxItemByValue(UseYnComboBox, USE_YN);

        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ProductCodeComboBox.SelectedIndex = -1;
            CooperativeCompanyNoComboBox.SelectedIndex = -1;
            RateCodeComboBox.SelectedIndex = -1;

            RateTextBox.Text = "";

            ApplyLaunchDateTimePicker.Value = DateTime.Now;
            ApplyEndDateTimePicker.Value = DateTime.Now;

            UseYnComboBox.SelectedIndex = -1;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveRate();
        }

        private void SaveRate()
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);         // 상품일련번호
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyNoComboBox);  // 업체번호
            string RATE_CD = Utils.GetSelectedComboBoxItemValue(RateCodeComboBox);              // 요율코드
            string RATE = RateTextBox.Text.Trim();                                              // 요율
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Value.ToString("yyyy-MM-dd");       // 적용개시일
            string APLY_END_DT = ApplyEndDateTimePicker.Value.ToString("yyyy-MM-dd");           // 적용종료일
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                  // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                     // 최초등록자ID

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            string query = string.Format("CALL InsertRateItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                PRDT_CNMB, CMPN_NO, RATE_CD, RATE, APLY_LNCH_DT, APLY_END_DT, USE_YN, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("요율정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchRateList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("요율정보를 저장했습니다.");
            }
        }

        private bool CheckRequireItems()
        {
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);         // 상품일련번호
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyNoComboBox);  // 업체번호
            string RATE_CD = Utils.GetSelectedComboBoxItemValue(RateCodeComboBox);              // 요율코드
            string RATE = RateTextBox.ToString();                                               // 요율
            string APLY_LNCH_DT = ApplyLaunchDateTimePicker.Value.ToString("yyyy-MM-dd");       // 적용개시일
            string APLY_END_DT = ApplyEndDateTimePicker.Value.ToString("yyyy-MM-dd");           // 적용종료일
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                  // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                     // 최초등록자ID

            if (PRDT_CNMB == "")
            {
                MessageBox.Show("상품번호를 선택하지 않았습니다.");
                ProductCodeComboBox.Focus();
                return false;
            }

            if (CMPN_NO == "")
            {
                MessageBox.Show("업체번호를 선택하지 않았습니다.");
                CooperativeCompanyNoComboBox.Focus();
                return false;
            }

            if (RATE_CD == "")
            {
                MessageBox.Show("요율구분을 선택하지 않았습니다.");
                RateCodeComboBox.Focus();
                return false;
            }

            if (RATE == "")
            {
                MessageBox.Show("요율은 필수 입력항목입니다.");
                RateTextBox.Focus();
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

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
                UseYnComboBox.Focus();
                return false;
            }

            return true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(ProductCodeComboBox);         // 상품일련번호
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyNoComboBox);  // 업체번호
            string RATE_CD = Utils.GetSelectedComboBoxItemValue(RateCodeComboBox);              // 요율코드

            string query = string.Format("CALL DeleteRateItem ('{0}','{1}','{2}')", PRDT_CNMB, CMPN_NO, RATE_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("요율정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchRateList();
                MessageBox.Show("선택한 항목을 삭제했습니다.");
            }
        }

        private void RateMgmt_Load(object sender, EventArgs e)
        {
            InitControls();
        }
    }
}
