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

namespace TripERP.ReservationMgt
{
    public partial class PopUpSearchReservationInfo : Form
    {
        enum eCustomerDataGridView
        {
            CUST_NO,
            CUST_NM,
            RSVT_NO,
            RSVT_DT,
            PRDT_CNMB,
            PRDT_GRAD_CD,
            PRDT_NM,
            DPTR_DT,
            CELL_PHNE_NO,
            RSVT_STTS_CD,
            EMAL_ADDR
        };

        private string _ReservationNumber = "";
        private string _ReservationPhoneNumber = "";
        private string _ReservationCustomerName = "";
        private string _ReservationEmailAddress = "";
        private string _ReservationProgressCode = "";
        private string _CustomerNumber = "";

        public PopUpSearchReservationInfo()
        {
            InitializeComponent();
        }

        private void PopUpSearchReservationInfo_Load(object sender, EventArgs e)
        {
            InitDataGridView();

            // 예약자명이 입력되어 있으면 예약자명이 같은 고객목록을 조회
            if (customerNameTextBox.Text != "")
            {
                searchReservationListByCustomerName();
            }

        }


        private void InitDataGridView()
        {
            DataGridView dataGridView1 = customerDataGridView;
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

        // 예약자명으로 예약목록 검색
        private void searchReservationListByCustomerName()
        {
            customerDataGridView.Rows.Clear();

            string CUST_NO = "";
            string CUST_NM = customerNameTextBox.Text.Trim();
            string RSVT_NO = "";
            string RSVT_DT = "";
            string PRDT_CNMB = "";
            string PRDT_GRAD_CD = "";
            string PRDT_NM = "";
            string DPTR_DT = "";

            string query = string.Format("CALL SelectReservationListByCustomerName ('{0}')", CUST_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약자명으로 예약목록을 검색할 수 없습니다.");
                return;
            }

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약자명에 해당하는 _예약목록이 존재하지 않습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                CUST_NO = dataRow["CUST_NO"].ToString();
                CUST_NM = dataRow["CUST_NM"].ToString();
                RSVT_NO = dataRow["RSVT_NO"].ToString();
                RSVT_DT = dataRow["RSVT_DT"].ToString().Substring(0,10);
                PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                PRDT_GRAD_CD = dataRow["PRDT_GRAD_CD"].ToString();
                PRDT_NM = dataRow["PRDT_NM"].ToString();
                DPTR_DT = dataRow["DPTR_DT"].ToString().Substring(0,10);
                _ReservationPhoneNumber = dataRow["CELL_PHNE_NO"].ToString();
                _ReservationCustomerName = dataRow["CUST_NM"].ToString();
                _ReservationEmailAddress = dataRow["EMAL_ADDR"].ToString();
                _ReservationProgressCode = dataRow["RSVT_STTS_CD"].ToString();

                customerDataGridView.Rows.Add
                (
                    CUST_NO,
                    CUST_NM,
                    RSVT_NO,
                    RSVT_DT,
                    PRDT_CNMB,
                    PRDT_GRAD_CD,
                    PRDT_NM,
                    DPTR_DT,
                    _ReservationPhoneNumber,
                    _ReservationProgressCode,
                    _ReservationEmailAddress
                );
            }
        }

        public void SetCustomerName(string customerName)
        {
            customerNameTextBox.Text = customerName;
        }

        public string GetReservationNumber()
        {
            return _ReservationNumber;
        }

        public string GetReservationPhoneNumber()
        {
            return _ReservationPhoneNumber;
        }

        public string GetReceiverName()
        {
            return _ReservationCustomerName;
        }

        public string GetReservationEmailAddress()
        {
            return _ReservationEmailAddress;
        }

        public string GetReservationProgress()
        {
            return _ReservationProgressCode;
        }

        public string GetCustomerNumber()
        {
            return _CustomerNumber;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (customerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("고객을 선택해 주십시오.");
                return;
            }

            returnReservationInfoSearchResult();
        }

        private void returnReservationInfoSearchResult()
        {
            _ReservationNumber = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.RSVT_NO].Value.ToString();
            _ReservationCustomerName = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_NM].Value.ToString();
            _ReservationPhoneNumber = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CELL_PHNE_NO].Value.ToString();
            _CustomerNumber = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.CUST_NO].Value.ToString();
            _ReservationProgressCode = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.RSVT_STTS_CD].Value.ToString();
            _ReservationEmailAddress = customerDataGridView.SelectedRows[0].Cells[(int)eCustomerDataGridView.EMAL_ADDR].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // 폼 닫기
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 그리드 행을 두번 클릭하면 팝업 호출 창으로 검색값 전달 후 폼을 닫음
        private void customerDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            returnReservationInfoSearchResult();
        }

        // 검색버튼 클릭
        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            searchReservationListByCustomerName();
        }
    }
}
