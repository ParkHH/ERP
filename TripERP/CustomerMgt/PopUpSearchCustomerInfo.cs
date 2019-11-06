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
using TripERP.ReservationMgt;

namespace TripERP.CustomerMgt
{
    public partial class PopUpSearchCustomerInfo : Form
    {
        enum eCustomerDataGridView { CUST_NO = 0, CUST_NM, CUST_ENG_NM, CELL_PHNE_NO, EMAL_ADDR };
        private string _customerNumber = ""; 
        private string _customerName = "";
        private string _customerEngName = "";
        private string _emailId = "";
        private string _emailDomain = "";
        private string _customerCellPhoneNo = "";

        public PopUpSearchCustomerInfo()
        {
            InitializeComponent();
        }

        private void PopUpSearchCustomerInfo_Load(object sender, EventArgs e)
        {
            InitDataGridView(); 

            // 고객명이 입력되어 있으면 고객명이 같은 고객목록을 조회
            if (customerNameTextBox.Text != "")
            {
                searchCustomerList();
            }
        }

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
            //dataGridView1.DoubleBuffered(true);
        }

        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            searchCustomerList();
        }

        // 고객목록 검색
        private void searchCustomerList()
        {
            customerDataGridView.Rows.Clear();

            string CUST_NM = customerNameTextBox.Text.Trim();
            string CELL_PHNE_NO = cellPhoneTextBox.Text.Trim();
            string EMAL_ADDR = emailAddressTextBox.Text.Trim();
            string CUST_ENG_NM = "";

            string query = string.Format("CALL SelectCustInfoList ('{0}', '{1}', '{2}')",
                CUST_NM, CELL_PHNE_NO, EMAL_ADDR);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("고객 정보를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                if (DialogResult.Yes == MessageBox.Show("고객정보가 없습니다. 새로운 고객을 등록하시겠습니까?", "", MessageBoxButtons.YesNo))
                {
                    OpenAddCustomerForm();
                }
            }
            else
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    string CUST_NO = dataRow["CUST_NO"].ToString();
                    CUST_NM = dataRow["CUST_NM"].ToString();
                    CUST_ENG_NM = dataRow["CUST_ENG_NM"].ToString();
                    CELL_PHNE_NO = dataRow["CELL_PHNE_NO"].ToString();
                    EMAL_ADDR = dataRow["EMAL_ADDR"].ToString();

                    customerDataGridView.Rows.Add(CUST_NO, CUST_NM, CUST_ENG_NM, CELL_PHNE_NO, EMAL_ADDR);
                }
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if(customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("고객을 선택해 주십시오.");
                return; 
            }

            returnCustomerDataGridViewChoiceRow();

            this.DialogResult = DialogResult.OK;
            this.Close(); 
        }

        // 호출창으로 리턴할 고객정보 값 세팅
        private void returnCustomerDataGridViewChoiceRow()
        {
            _customerNumber = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_NO].Value.ToString();
            _customerName = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_NM].Value.ToString();
            _customerEngName = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_ENG_NM].Value.ToString();
            _customerCellPhoneNo = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CELL_PHNE_NO].Value.ToString();

            string emailAddress = "";
            _emailId = "";
            _emailDomain = "";

            // 이메일주소
            if (customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.EMAL_ADDR].Value != null)
            {
                emailAddress = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.EMAL_ADDR].Value.ToString();
            }

            if (emailAddress != "")
            {
                string[] emailAddressSplit;
                emailAddressSplit = emailAddress.Split('@');

                _emailId = emailAddressSplit[0].ToString();
                _emailDomain = emailAddressSplit[1].ToString();
            }
        }



        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            OpenAddCustomerForm(); 
        }

        private void OpenAddCustomerForm()
        {
            PopUpAddCustomerInfo form = new PopUpAddCustomerInfo(this);
            form.StartPosition = FormStartPosition.CenterParent;

            // 고객명을 호출창에 전달
            form.SetCustomerName(customerNameTextBox.Text.Trim());
            // 휴대폰번호를 호출창에 전달
            form.SetBookerCellPhoneNo(cellPhoneTextBox.Text.Trim());
            // 이메일주소를 호출창에 전달
            form.SetBookerEmailAddress(emailAddressTextBox.Text.Trim());

            form.ShowDialog(); 
        }

        public string GetCustomerNumber()
        {
            return _customerNumber; 
        }

        public string GetCustomerName()
        {
            return _customerName; 
        }

        public string GetCustomerEngName()
        {
            return _customerEngName;
        }

        public string GetCustomerCellPhoneNo()
        {
            return _customerCellPhoneNo;
        }

        public string GetEmailId()
        {
            return _emailId;
        }

        public string GetEmailDomainName()
        {
            return _emailDomain;
        }

        // 호출창에서 설정한 고객명을 입력검색 고객명에 Setting
        public void SetCustomerName(string customerName)
        {
            customerNameTextBox.Text = customerName;
        }

        // 셀을 더블클릭하면 호출 창으로 선택값을 리턴
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

        // 고객명을 입력하고 엔터키를 누르면 검색 시작
        private void customerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchCustomerList();
            }
        }

        // 그리드에서 행을 선택하고 엔터를 누르면 호출 창으로 고객정보를 리턴
        private void customerDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
        }
    }
}
