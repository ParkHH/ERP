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

namespace TripERP.CustomerMgt
{
    public partial class PopUpCheckSameCustomer : Form
    {
        enum eCustomerDataGridView { CUST_NO, CUST_NM, CUST_ENG_NM, BIRTH, SEX_DVSN_CD, SEX_DVSN_NM, PRSN_CORP_DVSN_CD, PRSN_CORP_DVSN_NM, CELL_PHNE_NO, EMAL_ADDR };

        public string _customerNumber { get; set; }
        public string _customerName { get; set; }
        public string _customerEngName { get; set; }
        public string _birthDay { get; set; }
        public string _sexDivisionCode { get; set; }
        public string _personCorporationDivisionCode { get; set; }

        //=========================================================================================================================================================================
        // 폼 초기화
        //=========================================================================================================================================================================
        public PopUpCheckSameCustomer()
        {
            InitializeComponent();
        }

        //=========================================================================================================================================================================
        // 폼 로드
        //=========================================================================================================================================================================
        private void PopUpCheckSameCustomer_Load(object sender, EventArgs e)
        {
            InitDataGridView();

            loadCommonComboBox();

            userGuideLabel.Text = "동일 고객으로 추정되는 고객목록이 있습니다. \r\n 목록에서 대상자를 선택하세요.";

            customerNameTextBox.Text = _customerName;
            passengerBirthDateTextBox.Text = _birthDay;

            if (_sexDivisionCode != "") Utils.SelectComboBoxItemByValue(passengerSexDivisionComboBox, _sexDivisionCode);
            if (_personCorporationDivisionCode != "") Utils.SelectComboBoxItemByValue(personalCorporationDivisionComboBox, _personCorporationDivisionCode);

            // 고객명이 입력되어 있으면 고객명이 같은 고객목록을 조회
            if (_customerName != "" && _birthDay != "" && _sexDivisionCode != "" && _personCorporationDivisionCode != "")
            {
                searchCustomerList();
            }
        }

        //=========================================================================================================================================================================
        // 그리드 초기화
        //=========================================================================================================================================================================
        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = customerDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
        }

        //=========================================================================================================================================================================
        // 콤보박스 세팅
        //=========================================================================================================================================================================
        private void loadCommonComboBox()
        {
            string[] groupNameArray = { "SEX_DVSN_CD", "PRSN_CORP_DVSN_CD" };

            ComboBox[] comboBoxArray = { passengerSexDivisionComboBox, personalCorporationDivisionComboBox };

            for (int gi = 0; gi < groupNameArray.Length; gi++)
            {
                comboBoxArray[gi].Items.Clear();

                List<CommonCodeItem> list = Global.GetCommonCodeList(groupNameArray[gi]);

                for (int li = 0; li < list.Count; li++)
                {
                    string value = list[li].Value.ToString();
                    string desc = list[li].Desc;

                    ComboBoxItem item = new ComboBoxItem(desc, value);

                    comboBoxArray[gi].Items.Add(item);
                }

                if (comboBoxArray[gi].Items.Count > 0) comboBoxArray[gi].SelectedIndex = -1;
            }
        }

        //=========================================================================================================================================================================
        // 검색버튼 클릭
        //=========================================================================================================================================================================
        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            searchCustomerList();
        }

        //=========================================================================================================================================================================
        // 고객목록 검색
        //=========================================================================================================================================================================
        private void searchCustomerList()
        {
            customerDataGridView.Rows.Clear();

            string CUST_NO = "";
            string CUST_NM = customerNameTextBox.Text.Trim();
            string CUST_ENG_NM = "";
            string BIRTH = passengerBirthDateTextBox.Text.Trim();
            string SEX_DVSN_CD = Utils.GetSelectedComboBoxItemValue(passengerSexDivisionComboBox);
            string SEX_DVSN_NM = "";
            string PRSN_CORP_DVSN_CD = Utils.GetSelectedComboBoxItemValue(personalCorporationDivisionComboBox);
            string PRSN_CORP_DVSN_NM = "";
            string CELL_PHNE_NO = "";
            string EMAL_ADDR = "";

            string query = string.Format("CALL SelectCheckSameCustomerInfo ('{0}', '{1}', '{2}', '{3}')", CUST_NM, BIRTH, SEX_DVSN_CD, PRSN_CORP_DVSN_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("고객 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                CUST_NO = dataRow["CUST_NO"].ToString();
                CUST_NM = dataRow["CUST_NM"].ToString();
                CUST_ENG_NM = dataRow["CUST_ENG_NM"].ToString();
                BIRTH = dataRow["BIRTH"].ToString();

                if (BIRTH != "") BIRTH = BIRTH.Substring(0, 10);

                SEX_DVSN_CD = dataRow["SEX_DVSN_CD"].ToString();
                SEX_DVSN_NM = dataRow["SEX_DVSN_NM"].ToString();
                PRSN_CORP_DVSN_CD = dataRow["PRSN_CORP_DVSN_CD"].ToString();
                PRSN_CORP_DVSN_NM = dataRow["PRSN_CORP_DVSN_NM"].ToString();
                CELL_PHNE_NO = dataRow["CELL_PHNE_NO"].ToString();
                EMAL_ADDR = dataRow["EMAL_ADDR"].ToString();

                customerDataGridView.Rows.Add(CUST_NO, CUST_NM, CUST_ENG_NM, BIRTH, SEX_DVSN_CD, SEX_DVSN_NM, PRSN_CORP_DVSN_CD, PRSN_CORP_DVSN_NM, CELL_PHNE_NO, EMAL_ADDR);
            }

            customerDataGridView.ClearSelection();
        }

        //=========================================================================================================================================================================
        //  그리드 더블클릭하여 고객 선택값을 호출창으로 리턴
        //=========================================================================================================================================================================
        private void customerDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("고객을 선택해 주십시오.");
                return;
            }

            returnCustomerDataGridViewChoiceRow();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //=========================================================================================================================================================================
        // 선택 버튼 클릭
        //=========================================================================================================================================================================
        private void selectButton_Click(object sender, EventArgs e)
        {
            if (customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("고객을 선택해 주십시오.");
                return;
            }

            returnCustomerDataGridViewChoiceRow();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //=========================================================================================================================================================================
        // 호출창으로 리턴할 고객정보 값 세팅
        //=========================================================================================================================================================================
        private void returnCustomerDataGridViewChoiceRow()
        {
            _customerNumber = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_NO].Value.ToString();
            _customerName = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_NM].Value.ToString();
            _customerEngName = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_ENG_NM].Value.ToString();
            _birthDay = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.BIRTH].Value.ToString();
            _sexDivisionCode = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.SEX_DVSN_CD].Value.ToString();
            _personCorporationDivisionCode = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.PRSN_CORP_DVSN_CD].Value.ToString();
        }

        //=========================================================================================================================================================================
        // 폼닫기
        //=========================================================================================================================================================================
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //=========================================================================================================================================================================
        //  getter/setter
        //=========================================================================================================================================================================
        public string getCustomerNumber()
        {
            return _customerNumber;
        }

        public string getCustomerName()
        {
            return _customerName;
        }

        public string getCustomerEngName()
        {
            return _customerEngName;
        }

        public string getBirthDay()
        {
            return _birthDay;
        }

        public string getSexDivisionCode()
        {
            return _sexDivisionCode;
        }

        public string getPersonCorporationDivisionCode()
        {
            return _personCorporationDivisionCode;
        }

        public void setCustomerNumber(string customerNumber)
        {
            _customerNumber = customerNumber;
        }

        public void setCustomerName(string customerName)
        {
            _customerName = customerName;
        }

        public void setCustomerEngName(string customerEngName)
        {
            _customerEngName = customerEngName;
        }

        public void setBirthDay(string birthDay)
        {
            _birthDay = birthDay;
        }

        public void setSexDivisionCode(string sexDivisionCode)
        {
            _sexDivisionCode = sexDivisionCode;
        }

        public void setPersonCorporationDivisionCode(string personCorporationDivisionCode)
        {
            _personCorporationDivisionCode = personCorporationDivisionCode;
        }
    }
}
