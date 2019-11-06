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
    public partial class CurrencyCodeMgt : Form
    {
        enum eCurrencyCodeListDataGridView { CUR_CD, CUR_NM, CUR_SYBL, USE_YN, SCRN_SORT_ORD };
        private string _saveMode = "ADD"; // Test Description

        public CurrencyCodeMgt()
        {
            InitializeComponent();
        }

        private void CurrencyCodeMgmt_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            // 사용여부
            useYnComboBox.Items.Clear();

            string query = "CALL SelectCommoncodeList ('YN')";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string USE_YN = dataRow["CD_VLID_VAL"].ToString();
                string USE_YN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(USE_YN_NM, USE_YN);
                useYnComboBox.Items.Add(item);
            }

            useYnComboBox.SelectedIndex = 0;

            // DataGridView 스타일 초기화
            InitDataGridView();

            ResetButtonStatus(true);

            SearchCurrencyCodeList();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = currencyCodeListDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            useYnComboBox.SelectedIndex = 0;
            currencyCodeTextBox.Text = "";
            currencyNameTextBox.Text = "";
            currencySymbolTextBox.Text = "";
            screenSortOrderTextBox.Text = "";

            ResetButtonStatus(true);
        }

        private void ResetButtonStatus(bool insertMode = true)
        {
            deleteButton.Enabled = !insertMode;

            saveButton.Text = insertMode == true ? "저장" : "수정";
            _saveMode = insertMode == true ? "ADD" : "UPDATE";
            //useYnComboBox.Enabled = insertMode;
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_saveMode == "ADD")
                AddCurrencyCode();
            else
                UpdateCurrencyCode();
        }

        private void AddCurrencyCode()
        {
            string CUR_CD = currencyCodeTextBox.Text.Trim();
            string CUR_NM = currencyNameTextBox.Text.Trim();
            string CUR_SYBL = currencySymbolTextBox.Text.Trim();
            string USE_YN = Utils.GetSelectedComboBoxItemValue(useYnComboBox);
            string SCRN_SORT_ORD = screenSortOrderTextBox.Text.Trim();
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            if (CheckRequireItems() == false)
                return;

            string query = string.Format("CALL InsertCurrencyCodeItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                CUR_CD, CUR_NM, CUR_SYBL, SCRN_SORT_ORD, USE_YN, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("통화코드를 등록할 수 없습니다.");
                return;
            }
            else
            {
                SearchCurrencyCodeList();
                MessageBox.Show("통화코드를 등록했습니다.");
                ResetButtonStatus(true);
            }
        }

        // 통화목록 테이블 UPDATE
        private void UpdateCurrencyCode()
        {
            string CUR_CD = currencyCodeTextBox.Text.Trim();
            string CUR_NM = currencyNameTextBox.Text.Trim();
            string CUR_SYBL = currencySymbolTextBox.Text.Trim();
            string USE_YN = Utils.GetSelectedComboBoxItemValue(useYnComboBox);
            string SCRN_SORT_ORD = screenSortOrderTextBox.Text.Trim();
            string FINL_MDFR_ID = Global.loginInfo.ACNT_ID;

            if (CheckRequireItems() == false)
                return;

            string query = string.Format("CALL UpdateCurrencyCodeItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                CUR_CD, CUR_NM, CUR_SYBL,SCRN_SORT_ORD, USE_YN, FINL_MDFR_ID);

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("통화코드를 수정할 수 없습니다.");
                return;
            }
            else
            {
                SearchCurrencyCodeList();
                MessageBox.Show("통화코드를 수정했습니다.");
                ResetButtonStatus(true);
            }
        }

        private void searchCurrencyCodeListButton_Click(object sender, EventArgs e)
        {
            SearchCurrencyCodeList();
        }

        // 통화목록 테이블 SELECT
        private void SearchCurrencyCodeList()
        {
            currencyCodeListDataGridView.Rows.Clear();

            string CUR_CD = searchCurrencyCodeTextBox.Text.Trim();
            string CUR_NM = searchCurrencyNameTextBox.Text.Trim();
            string CUR_SYBL = "";
            string USE_YN = "";
            string SCRN_SORT_ORD = screenSortOrderTextBox.Text.Trim();

            string query = string.Format("CALL SelectCurrencyCodeList ('{0}', '{1}')", CUR_CD, CUR_NM);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("통화목록을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                CUR_CD = dataRow["CUR_CD"].ToString();
                CUR_NM = dataRow["CUR_NM"].ToString();
                CUR_SYBL = dataRow["CUR_SYBL"].ToString();
                SCRN_SORT_ORD = dataRow["SCRN_SORT_ORD"].ToString();
                USE_YN = dataRow["CD_VLID_VAL_DESC"].ToString();

                currencyCodeListDataGridView.Rows.Add(CUR_CD, CUR_NM, CUR_SYBL, USE_YN, SCRN_SORT_ORD);
            }

            currencyCodeListDataGridView.ClearSelection();
        }

        private bool CheckRequireItems()
        {
            string CUR_CD = currencyCodeTextBox.Text.Trim();
            string CUR_NM = currencyNameTextBox.Text.Trim();
            string CUR_SYBL = currencySymbolTextBox.Text.Trim();
            string USE_YN = Utils.GetSelectedComboBoxItemValue(useYnComboBox);
            string SCRN_SORT_ORD = screenSortOrderTextBox.Text.Trim();
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            if (CUR_CD == "")
            {
                MessageBox.Show("통화코드는 필수 입력항목입니다.");
                currencyCodeTextBox.Focus();
                return false;
            }

            if (CUR_NM == "")
            {
                MessageBox.Show("통화명은 필수 입력항목입니다.");
                currencyNameTextBox.Focus();
                return false;
            }

            if (SCRN_SORT_ORD == "")
            {
                MessageBox.Show("화면정렬순서는 필수 입력항목입니다.");
                screenSortOrderTextBox.Focus();
                return false;
            }

            return true;
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void currencyCodeListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (currencyCodeListDataGridView.SelectedRows.Count == 0)
                return;

            string CUR_CD = currencyCodeListDataGridView.SelectedRows[0].Cells[(int)eCurrencyCodeListDataGridView.CUR_CD].Value.ToString();
            string CUR_NM = currencyCodeListDataGridView.SelectedRows[0].Cells[(int)eCurrencyCodeListDataGridView.CUR_NM].Value.ToString();
            string CUR_SYBL = currencyCodeListDataGridView.SelectedRows[0].Cells[(int)eCurrencyCodeListDataGridView.CUR_SYBL].Value.ToString();
            string USE_YN = currencyCodeListDataGridView.SelectedRows[0].Cells[(int)eCurrencyCodeListDataGridView.USE_YN].Value.ToString();
            string SCRN_SORT_ORD = currencyCodeListDataGridView.SelectedRows[0].Cells[(int)eCurrencyCodeListDataGridView.SCRN_SORT_ORD].Value.ToString();

            currencyCodeTextBox.Text = CUR_CD;
            currencyNameTextBox.Text = CUR_NM;
            currencySymbolTextBox.Text = CUR_SYBL;
            Utils.SelectComboBoxItemByValue(useYnComboBox, USE_YN);
            screenSortOrderTextBox.Text = SCRN_SORT_ORD;

            ResetButtonStatus(false);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string CUR_CD = currencyCodeTextBox.Text;

            string query = string.Format("CALL DeleteCurrencyCodeItem ('{0}')", CUR_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("통화코드를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                currencyCodeListDataGridView.Rows.RemoveAt(currencyCodeListDataGridView.SelectedRows[0].Index);
                MessageBox.Show("선택한 항목을 삭제했습니다.");
                ResetButtonStatus(true);
            }
        }

        // 통화명 입력 후 엔터키로 검색
        private void searchCurrencyNameTextBox_Enter(object sender, EventArgs e)
        {
            SearchCurrencyCodeList();
        }
    }
}
