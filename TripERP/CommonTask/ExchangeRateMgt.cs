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
    public partial class ExchangeRateMgt : Form
    {
        enum eForeignExchangeRateDataGridView { NTFC_DT, CUR_CD, BASI_EXRT, STMT_EXRT, INVC_EXRT };
        
        public ExchangeRateMgt()
        {
            InitializeComponent();
        }

        private void ForeignExchangeRateMgmt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchForeignExchageRateList();         // form load 시 환율내역 바로 출력
        }

        private void InitControls()
        {
            ////
            // 통화코드 콤보박스 초기화
            searchCurrencyCodeComboBox.Items.Clear();
            currencyCodeComboBox.Items.Clear();

            string query = "CALL SelectCurList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("통화코드 정보를 가져올 수 없습니다.");
                return;
            }

            searchCurrencyCodeComboBox.Items.Add(new ComboBoxItem("전체", ""));

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CUR_CD = dataRow["CUR_CD"].ToString();
                string CUR_NM = dataRow["CUR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CUR_NM, CUR_CD);

                searchCurrencyCodeComboBox.Items.Add(item);
                currencyCodeComboBox.Items.Add(item);
            }

            if(searchCurrencyCodeComboBox.Items.Count > 0)
                searchCurrencyCodeComboBox.SelectedIndex = 0;
            if (currencyCodeComboBox.Items.Count > 0)
                currencyCodeComboBox.SelectedIndex = 1;
            ////

            ////
            // 고시일자 설정
            searchNotifyDateTimePicker.Value = DateTime.Now;
            notifyDateTimePicker.Value = searchNotifyDateTimePicker.Value;
            ////

            // DataGridView 스타일 초기화
            InitDataGridView();

            SearchForeignExchageRateList();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = foreignExchangeRateDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        private void searchForeignExchageRateListButton_Click(object sender, EventArgs e)
        {
            SearchForeignExchageRateList();
        }

        private void SearchForeignExchageRateList()
        {
            foreignExchangeRateDataGridView.Rows.Clear();

            string NTFC_DT = searchNotifyDateTimePicker.Value.ToString("yyyy-MM-dd");
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(searchCurrencyCodeComboBox);
            string query = string.Format("CALL SelectExrtNtfcList ('{0}', '{1}')", NTFC_DT, CUR_CD);

            string BASI_EXRT = "";
            string STMT_EXRT = "";
            string INVC_EXRT = "";

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("환율고시내역을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                NTFC_DT = dataRow["NTFC_DT"].ToString().Substring(0, 10);
                CUR_CD = dataRow["CUR_CD"].ToString();
                BASI_EXRT = dataRow["BASI_EXRT"].ToString();
                STMT_EXRT = dataRow["STMT_EXRT"].ToString();
                INVC_EXRT = dataRow["INVC_EXRT"].ToString();

                foreignExchangeRateDataGridView.Rows.Add(NTFC_DT, CUR_CD, BASI_EXRT, STMT_EXRT, INVC_EXRT);
            }

            foreignExchangeRateDataGridView.ClearSelection();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            AddExrtNtfcItem();
        }

        private void AddExrtNtfcItem()
        {
            string NTFC_DT = notifyDateTimePicker.Value.ToString("yyyy-MM-dd");
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);
            string BASI_EXRT = basiExrtTextBox.Text.Trim();
            string STMT_EXRT = settlementExrtTextBox.Text.Trim();
            string INVC_EXRT = invoiceExrtTextBox.Text.Trim();
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            if (CheckRequireItems() == false)
                return;

            string query = string.Format("CALL InsertExrtNtfcItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                NTFC_DT, CUR_CD, BASI_EXRT, STMT_EXRT, INVC_EXRT, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("환율을 저장할 수 없습니다.");
                return;
            }
            SearchForeignExchageRateList();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return; 

            string NTFC_DT = notifyDateTimePicker.Value.ToString("yyyy-MM-dd");
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);

            string query = string.Format("CALL DeleteExrtNtfcItem ('{0}', '{1}')", NTFC_DT, CUR_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("환율을 삭제할 수 없습니다.");
                return;
            }
            SearchForeignExchageRateList();
        }

        private bool CheckRequireItems()
        {
            string NTFC_DT = notifyDateTimePicker.Value.ToString("yyyy-MM-dd");
            string CUR_CD = Utils.GetSelectedComboBoxItemValue(currencyCodeComboBox);
            string BASI_EXRT = basiExrtTextBox.Text.Trim();

            if (NTFC_DT == "")
            {
                MessageBox.Show("고시일자를 선택해 주십시오.");
                notifyDateTimePicker.Focus();
                return false; 
            }

            if (CUR_CD == "")
            {
                MessageBox.Show("통화코드를 선택해 주십시오.");
                currencyCodeComboBox.Focus();
                return false;
            }

            if (BASI_EXRT == "")
            {
                MessageBox.Show("기준환율을 입력해 주십시오.");
                basiExrtTextBox.Focus();
                return false;
            }

            return true;
        }

        private void foreignExchangeRateDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            notifyDateTimePicker.Value = DateTime.Now;
            if (currencyCodeComboBox.Items.Count > 0)
                currencyCodeComboBox.SelectedIndex = 0;
            basiExrtTextBox.Text = "";
            settlementExrtTextBox.Text = "";
            invoiceExrtTextBox.Text = "";
            foreignExchangeRateDataGridView.ClearSelection();
        }

        private void foreignExchangeRateDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (foreignExchangeRateDataGridView.SelectedRows.Count == 0)
                return;

            string NTFC_DT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.NTFC_DT].Value.ToString();
            string CUR_CD = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.CUR_CD].Value.ToString();
            string BASI_EXRT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.BASI_EXRT].Value.ToString();
            string STMT_EXRT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.STMT_EXRT].Value.ToString();
            string INVC_EXRT = foreignExchangeRateDataGridView.SelectedRows[0].Cells[(int)eForeignExchangeRateDataGridView.INVC_EXRT].Value.ToString();

            notifyDateTimePicker.Value = Utils.GetDateTimeFormatFromString(NTFC_DT);
            Utils.SelectComboBoxItemByValue(currencyCodeComboBox, CUR_CD);
            basiExrtTextBox.Text = BASI_EXRT;
            settlementExrtTextBox.Text = STMT_EXRT;
            invoiceExrtTextBox.Text = INVC_EXRT;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
