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
using System.IO; 

namespace TripERP.ReservationMgt
{

    public partial class ReservationList : Form
    {
        public enum eReservationDataGridView
        {
            RSVT_DT,
            RSVT_NO,
            DPTR_DT,
            CUST_NM,
            CMPN_NM,
            PRDT_NM,
            number,
            reservationStatus,
            SALE_CUR_CD,
            TOT_SALE_AMT,
            TOT_RECT_AMT,
            ARGM_CHCK_STTS_CD,
            LST_CHCK_STTS_CD,
            PSPT_CHCK_STTS_CD,
            VOCH_CHCK_STTS_CD,
            ISRC_CHCK_STTS_CD,
            AVAT_CHCK_STTS_CD,
            PRSN_CHCK_STTS_CD,
            RSVT_STTS_CD,
            CUST_NO,
            EMPL_NM,
        }

        private string _PRDT_NM = "";    // 상품명
        private string _CMPN_NM = "";    // 모객업체
        private string _EMPL_NM = "";    // 담당자
        private string _STTS_CT = "";    // 예약상태
        private string _DPTR_DT = "";    // 출발일자
        private string _RSVT_DT = "";    // 예약일자
        private string _RSVT_NO = "";    // 예약번호
        private string _TM_CNT;          // 팀수
        private string _saveMode = "";   // 수정모드

        public ReservationList()
        {
            InitializeComponent();
        }

        private void ReservationList_Load(object sender, EventArgs e)
        {
            InitControls();

            // 대시보드에서 타고 들어온 경우
            if (_saveMode == Global.SAVEMODE_UPDATE && _RSVT_DT != "")
            {
                productComboBox.Text = _PRDT_NM;
                cooperativeCompanyComboBox.Text = _CMPN_NM;
                employeeComboBox.Text = _EMPL_NM;
                // 대시보드는 확정목록만 가져오기 때문에 고정함.
                reservationStatusComboBox.Text = "확정";

                startReservationDateTimePicker.Text = _RSVT_DT;
                endReservationDateTimePicker.Text = _RSVT_DT;
                startDepartureDateTimePicker.Text = _DPTR_DT;
                endDepartureDateTimePicker.Text = _DPTR_DT;

                searchReservationList();
            }
        }

        private void InitControls()
        {
            // 상품 콤보박스 아이템 로드
            LoadProductComboBoxItems();

            // 모객업체 콤보박스 아이템 로드
            LoadCooperativeCompanyComboBoxItems();

            // 담당자 콤보박스 아이템 로드
            LoadEmployeeComboBoxItems();

            // 예약상태 콤보박스 아이템 로드
            LoadReservationStatusComboBoxItems();

            // 검색기준 콤보박스 설정
            LoadSearchDateConditionComboBoxItem();

            // DataGridView 스타일 초기화
            InitDataGridView();
        }

        private void LoadProductComboBoxItems()
        {
            productComboBox.Items.Clear();

            string query = "CALL SelectPrdtList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품 정보를 가져올 수 없습니다.");
                return;
            }

            productComboBox.Items.Add(new ComboBoxItem("전체", -1));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_NM, PRDT_CNMB);

                productComboBox.Items.Add(item);
            }

            if(productComboBox.Items.Count > 0)
                productComboBox.SelectedIndex = 0;
        }

        private void LoadCooperativeCompanyComboBoxItems()
        {
            cooperativeCompanyComboBox.Items.Clear();

            // 모객매체
            //string CNSM_FILE_APLY_YN = "10";
            string query = string.Format("CALL SelectCoopCmpnList ('{0}', '{1}')", ' ', ' ');

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("모객업체 정보를 가져올 수 없습니다.");
                return;
            }

            cooperativeCompanyComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                cooperativeCompanyComboBox.Items.Add(item);
            }

            if (cooperativeCompanyComboBox.Items.Count > 0)
                cooperativeCompanyComboBox.SelectedIndex = 0;
        }

        private void LoadEmployeeComboBoxItems()
        {
            employeeComboBox.Items.Clear();

            string query = "CALL SelectEmplList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("담당자 정보를 가져올 수 없습니다.");
                return;
            }

            employeeComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string EMPL_NO = dataRow["EMPL_NO"].ToString();
                string EMPL_NM = dataRow["EMPL_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(EMPL_NM, EMPL_NO);

                employeeComboBox.Items.Add(item);
            }

            if (employeeComboBox.Items.Count > 0)
                employeeComboBox.SelectedIndex = 0;
        }

        private void LoadReservationStatusComboBoxItems()
        {
            reservationStatusComboBox.Items.Clear();

            List<CommonCodeItem> list = Global.GetCommonCodeList("RSVT_STTS_CD");

            reservationStatusComboBox.Items.Add(new ComboBoxItem("전체", ""));
            for (int i=0; i< list.Count; i++)
            {
                string value = list[i].Value.ToString();
                string desc = list[i].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                reservationStatusComboBox.Items.Add(item);
            }

            if (reservationStatusComboBox.Items.Count > 0)
                reservationStatusComboBox.SelectedIndex = 0;
        }

        //=========================================================================================================================================================================
        // 검색기준 콤보박스 초기화
        //=========================================================================================================================================================================
        private void LoadSearchDateConditionComboBoxItem()
        {
            searchDateConditionComboBox.Items.Clear();
            searchDateConditionComboBox.Items.Add(new ComboBoxItem("예약일", "1"));
            searchDateConditionComboBox.Items.Add(new ComboBoxItem("출발일", "2"));
            searchDateConditionComboBox.Items.Add(new ComboBoxItem("예약+출발", "3"));

            // 출발일 기준으로 검색하도록 기본 설정하고 출발일은 Disable처리
            searchDateConditionComboBox.SelectedIndex = 0;

            startDepartureDateTimePicker.Enabled = false;
            endDepartureDateTimePicker.Enabled = false;

            // 예약일 초기화
            startReservationDateTimePicker.Value = DateTime.Now.AddDays(-60);
            endReservationDateTimePicker.Value = DateTime.Now;

            // 출발일 초기화
            startDepartureDateTimePicker.Value = DateTime.Now.AddDays(-30);
            endDepartureDateTimePicker.Value = DateTime.Now.AddDays(90);
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView1 = reservationDataGridView;
            dataGridView1.DoubleBuffered(true);

            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
        }

        // 검색버튼 클릭
        private void searchReservationListButton_Click(object sender, EventArgs e)
        {
            searchReservationList();
        }

        private void searchReservationList()
        {
            reservationDataGridView.SuspendLayout();

            reservationDataGridView.Rows.Clear();

            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(productComboBox);
            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(cooperativeCompanyComboBox);
            string RPSB_EMPL_NO = Utils.GetSelectedComboBoxItemValue(employeeComboBox);
            string RSVT_STTS_CD = Utils.GetSelectedComboBoxItemValue(reservationStatusComboBox);                   // 예약상태코드

            string START_DPTR_DT = "";
            string END_DPTR_DT = "";
            string START_RSVT_DT = "";
            string END_RSVT_DT = "";
            string CUST_NM = bookerNameTextBox.Text.Trim();

            // 검색기준에 따라 일자 검색조건을 다르게 설정
            if (searchDateConditionComboBox.SelectedIndex == 0)
            {
                // 예약일 기준
                START_RSVT_DT = startReservationDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");                // 예약일자From
                END_RSVT_DT = endReservationDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");                    // 예약일자To
            }
            else if (searchDateConditionComboBox.SelectedIndex == 1)
            {
                // 출발일 기준
                START_DPTR_DT = startDepartureDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");                  // 출발일자From
                END_DPTR_DT = endDepartureDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");                      // 출발일자To
            }
            else if (searchDateConditionComboBox.SelectedIndex == 2)
            {
                // 예약일+출발일 기준
                START_RSVT_DT = startReservationDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");                // 예약일자From
                END_RSVT_DT = endReservationDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");                    // 예약일자To

                START_DPTR_DT = startDepartureDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");                  // 출발일자From
                END_DPTR_DT = endDepartureDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");                      // 출발일자To
            }

            string query = string.Format("CALL SelectRsvtList ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
                PRDT_CNMB, TKTR_CMPN_NO, RPSB_EMPL_NO, RSVT_STTS_CD, START_DPTR_DT, END_DPTR_DT, START_RSVT_DT, END_RSVT_DT, CUST_NM);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약목록을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string RSVT_DT = dataRow["RSVT_DT"].ToString().Substring(0, 10);  // 예약일
                string RSVT_NO = dataRow["RSVT_NO"].ToString();                   // 예약번호
                string DPTR_DT = dataRow["DPTR_DT"].ToString().Substring(0, 10);  // 출발일
                CUST_NM = dataRow["CUST_NM"].ToString();                   // 고객명
                string CUST_NO = dataRow["CUST_NO"].ToString();                   // 고객번호
                string CMPN_NM = dataRow["CMPN_NM"].ToString();                   // 모객업체
                string PRDT_NM = dataRow["PRDT_NM"].ToString();                   // 상품명
                string ADLT_NBR = dataRow["ADLT_NBR"].ToString();                 // 성인수                
                string CHLD_NBR = dataRow["CHLD_NBR"].ToString();                 // 소아수
                string INFN_NBR = dataRow["INFN_NBR"].ToString();                 // 유아수
                RSVT_STTS_CD = dataRow["RSVT_STTS_CD"].ToString();                // 예약상태     
                //string ADLT_SALE_PRCE = dataRow["ADLT_SALE_PRCE"].ToString();   // 성인판매가격
                //string CHLD_SALE_PRCE = dataRow["CHLD_SALE_PRCE"].ToString();   // 소아판매가격
                //string INFN_SALE_PRCE = dataRow["INFN_SALE_PRCE"].ToString();                   // 유아판매가격
                string SALE_CUR_CD = Utils.SetComma(dataRow["SALE_CUR_CD"].ToString());           // 판매통화코드
                string TOT_SALE_AMT = Utils.SetComma(dataRow["TOT_SALE_AMT"].ToString());         // 총판매액
                string TOT_RECT_AMT = Utils.SetComma(dataRow["TOT_RECT_AMT"].ToString());         // 총입금금액

                /*
                //---------------------------------------------------------------------------------
                // 암호화 내용 복호화 진행
                //---------------------------------------------------------------------------------
                CUST_NM = EncryptMgt.Decrypt(CUST_NM, EncryptMgt.aesEncryptKey);                                
                */

                /*
                string LST_CHCK_STTS_CD = Global.GetCommonCodeDesc("LST_CHCK_STTS_CD", dataRow["LST_CHCK_STTS_CD"].ToString());     // 명단
                string PSPT_CHCK_STTS_CD = Global.GetCommonCodeDesc("PSPT_CHCK_STTS_CD", dataRow["PSPT_CHCK_STTS_CD"].ToString());  // 여권
                string ARGM_CHCK_STTS_CD = Global.GetCommonCodeDesc("ARGM_CHCK_STTS_CD", dataRow["ARGM_CHCK_STTS_CD"].ToString());  // 수배 
                string VOCH_CHCK_STTS_CD = Global.GetCommonCodeDesc("VOCH_CHCK_STTS_CD", dataRow["VOCH_CHCK_STTS_CD"].ToString());  // 바우처
                string ISRC_CHCK_STTS_CD = Global.GetCommonCodeDesc("ISRC_CHCK_STTS_CD", dataRow["ISRC_CHCK_STTS_CD"].ToString());  // 보험
                string AVAT_CHCK_STTS_CD = Global.GetCommonCodeDesc("AVAT_CHCK_STTS_CD", dataRow["AVAT_CHCK_STTS_CD"].ToString());  // 항공
                string PRSN_CHCK_STTS_CD = Global.GetCommonCodeDesc("PRSN_CHCK_STTS_CD", dataRow["PRSN_CHCK_STTS_CD"].ToString());  // 개인
                */

                string ARGM_CHCK_STTS_CD = dataRow["ARGM_CHCK_STTS_CD"].ToString();  // 수배 
                string LST_CHCK_STTS_CD = dataRow["LST_CHCK_STTS_CD"].ToString();    // 명단
                string PSPT_CHCK_STTS_CD = dataRow["PSPT_CHCK_STTS_CD"].ToString();  // 여권
                string VOCH_CHCK_STTS_CD = dataRow["VOCH_CHCK_STTS_CD"].ToString();  // 바우처
                string ISRC_CHCK_STTS_CD = dataRow["ISRC_CHCK_STTS_CD"].ToString();  // 보험
                string AVAT_CHCK_STTS_CD = dataRow["AVAT_CHCK_STTS_CD"].ToString();  // 항공
                string PRSN_CHCK_STTS_CD = dataRow["PRSN_CHCK_STTS_CD"].ToString();  // 개인

                if (ARGM_CHCK_STTS_CD.Equals("2"))
                    ARGM_CHCK_STTS_CD = "●";
                else
                    ARGM_CHCK_STTS_CD = "";

                if (LST_CHCK_STTS_CD.Equals("2"))
                    LST_CHCK_STTS_CD = "●";
                else
                    LST_CHCK_STTS_CD = "";

                if (PSPT_CHCK_STTS_CD.Equals("2"))
                    PSPT_CHCK_STTS_CD = "●";
                else
                    PSPT_CHCK_STTS_CD = "";

                if (VOCH_CHCK_STTS_CD.Equals("2"))
                    VOCH_CHCK_STTS_CD = "●";
                else
                    VOCH_CHCK_STTS_CD = "";

                if (ISRC_CHCK_STTS_CD.Equals("2"))
                    ISRC_CHCK_STTS_CD = "●";
                else
                    ISRC_CHCK_STTS_CD = "";

                if (AVAT_CHCK_STTS_CD.Equals("2"))
                    AVAT_CHCK_STTS_CD = "●";
                else
                    AVAT_CHCK_STTS_CD = "";

                if (PRSN_CHCK_STTS_CD.Equals("2"))
                    PRSN_CHCK_STTS_CD = "●";
                else
                    PRSN_CHCK_STTS_CD = "";

                string EMPL_NM = dataRow["EMPL_NM"].ToString();                                                                     // 담당자

                int number = Int32.Parse(ADLT_NBR) + Int32.Parse(CHLD_NBR) + Int32.Parse(INFN_NBR);
                string reservationStatus = Global.GetCommonCodeDesc("RSVT_STTS_CD", RSVT_STTS_CD);
                //Double price = Double.Parse(ADLT_SALE_PRCE) + Double.Parse(CHLD_SALE_PRCE) + Double.Parse(INFN_SALE_PRCE);
                //reservationDataGridView.Rows.Add(false,
                reservationDataGridView.Rows.Add
                (
                    RSVT_DT,
                    RSVT_NO,
                    DPTR_DT,
                    CUST_NM,
                    CMPN_NM,
                    PRDT_NM,
                    number,
                    reservationStatus,
                    SALE_CUR_CD,
                    double.Parse(TOT_SALE_AMT),
                    double.Parse(TOT_RECT_AMT),
                    ARGM_CHCK_STTS_CD,
                    LST_CHCK_STTS_CD,
                    PSPT_CHCK_STTS_CD,
                    VOCH_CHCK_STTS_CD,
                    ISRC_CHCK_STTS_CD,
                    AVAT_CHCK_STTS_CD,
                    PRSN_CHCK_STTS_CD,
                    RSVT_STTS_CD,
                    CUST_NO,
                    EMPL_NM
                    );
            }

            reservationDataGridView.ClearSelection();
            reservationDataGridView.ResumeLayout();
        }

        private void openReservationDetailMgtButton_Click(object sender, EventArgs e)
        {
            OpenReservationDetailMgtForm();
        }



        //==================================================================================================================
        // 예약목록 Cell 을 Double Click 했을때 동작
        //==================================================================================================================
        private void reservationDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenReservationDetailMgtForm();
        }

        private void OpenReservationDetailMgtForm()
        {
            if (reservationDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("예약 항목을 선택해 주십시오.");
                return;
            }

            string reservationNumber = reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.RSVT_NO].Value.ToString();

            // 폼이 실행중인지 확인하여 중복실행되지 않도록 처리
            ReservationDetailMgt frm = (ReservationDetailMgt) Utils.isFormActivated("ReservationDetailMgt");
            if (frm == null)
            {
                ReservationDetailInfoMgt form = new ReservationDetailInfoMgt();
                form.MdiParent = Global.mainForm;
                form.Text = "예약상세관리";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.SetReservationNumber(reservationNumber);
                form.SetSaveMode(Global.SAVEMODE_UPDATE);

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();
            } else
            {
                frm.SetReservationNumber(reservationNumber);
                frm.SetSaveMode(Global.SAVEMODE_UPDATE);
                frm.SetReservationDataGridView(reservationDataGridView);
                frm.redrawReservationDetailInfo();
                frm.Activate();
            }

            // 예약상세화면 닫기 후 그리드 Refresh
            searchReservationList();
        }

        private void restoreReservationButton_Click(object sender, EventArgs e)
        {
            if (reservationDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("예약 항목을 선택해 주십시오.");
                return;
            }

            int reservationStatusCode = Int32.Parse(reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.RSVT_STTS_CD].Value.ToString());
            if(reservationStatusCode != (int)Global.eReservationStatus.canceled)
            {
                MessageBox.Show("취소 상태가 아닙니다.");
                return;
            }

            string reservationNumber = reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.RSVT_NO].Value.ToString();
            int retVal = DbHelper.ExecuteNonQuery(string.Format("CALL UpdateRsvtSttsCdItem ('{0}', '{1}')", reservationNumber, (int)Global.eReservationStatus.notYetDecided));
            if(retVal != -1)
            {

                reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.reservationStatus].Value = "미확정";
                reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.RSVT_STTS_CD].Value = (int)Global.eReservationStatus.notYetDecided;
                MessageBox.Show("예약 상태를 미확정으로 변경했습니다.");
            }
            else
            {
                MessageBox.Show("예약 상태를 변경할 수 없습니다.");
            }
        }

        private void cancelReservationButton_Click(object sender, EventArgs e)
        {
            if (reservationDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("예약 항목을 선택해 주십시오.");
                return;
            }

            int reservationStatusCode = Int32.Parse(reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.RSVT_STTS_CD].Value.ToString());
            if (reservationStatusCode == (int)Global.eReservationStatus.canceled)
            {
                MessageBox.Show("이미 취소된 상태입니다");
                return;
            }

            string reservationNumber = reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.RSVT_NO].Value.ToString();
            int retVal = DbHelper.ExecuteNonQuery(string.Format("CALL UpdateRsvtSttsCdItem ('{0}', '{1}')", reservationNumber, (int)Global.eReservationStatus.canceled));
            if (retVal != -1)
            {

                reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.reservationStatus].Value = "취소";
                reservationDataGridView.SelectedRows[0].Cells[(int)eReservationDataGridView.RSVT_STTS_CD].Value = (int)Global.eReservationStatus.canceled;
                MessageBox.Show("예약 상태를 취소로 변경했습니다.");
            }
            else
            {
                MessageBox.Show("예약 상태를 변경할 수 없습니다.");
            }
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

            reservationDataGridView.SuspendLayout();

            reservationDataGridView.SelectAll();

            if (Directory.Exists(fileDirPath))
            {
                //if (true == ExcelHelper.ExportExcel(filePath, reservationDataGridView))
                if (ExcelHelper.gridToExcel(filePath, reservationDataGridView) == true)
                    MessageBox.Show(string.Format("{0}\r\n파일을 저장했습니다.", filePath));
                else
                    MessageBox.Show("파일을 저장 할 수 없습니다."); 
            }
            else
            {
                MessageBox.Show("잘못된 저장 경로입니다."); 
            }

            this.Cursor = Cursors.Default;
            reservationDataGridView.ClearSelection();

            reservationDataGridView.ResumeLayout();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setProductName(string PRDT_NM)
        {
            _PRDT_NM = PRDT_NM;
        }

        public void setCompanyName(string CMPN_NM)
        {
            _CMPN_NM = CMPN_NM;
        }

        public void setEmployeeName(string EMPL_NM)
        {
            _EMPL_NM = EMPL_NM;
        }

        public void setReservationStatusCode(string STTS_CT)
        {
            _STTS_CT = STTS_CT;
        }

        public void setDepartDate(string DPTR_DT)
        {
            _DPTR_DT = DPTR_DT;
        }

        public void setReservationDate(string RSVT_DT)
        {
            _RSVT_DT = RSVT_DT;
        }

        public void setReserationNumber(string RSVT_NO)
        {
            _RSVT_NO = RSVT_NO;
        }

        public void setTeamCount(string TM_CNT)
        {
            _TM_CNT = TM_CNT;
        }

        public void setSaveMode(string saveMode)
        {
            _saveMode = saveMode;
        }

        // 폼이 활성화되면 목록을 자동 검색
        private void ReservationList_Activated(object sender, EventArgs e)
        {
            searchReservationList();
        }

        //=========================================================================================================================================================================
        // 검색기준이 바뀌면 일자 검색 조건을 재조정
        //=========================================================================================================================================================================
        private void searchDateConditionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchDateConditionComboBox.SelectedIndex == -1) return;

            if (searchDateConditionComboBox.SelectedIndex == 0)
            {
                // 예약일 검색
                startReservationDateTimePicker.Enabled = true;
                endReservationDateTimePicker.Enabled = true;

                startReservationDateTimePicker.Focus();

                startDepartureDateTimePicker.Enabled = false;
                endDepartureDateTimePicker.Enabled = false;
            } else if (searchDateConditionComboBox.SelectedIndex == 1)
            {
                // 출발일 검색
                startReservationDateTimePicker.Enabled = false;
                endReservationDateTimePicker.Enabled = false;

                startDepartureDateTimePicker.Enabled = true;
                endDepartureDateTimePicker.Enabled = true;

                startDepartureDateTimePicker.Focus();
            } else if (searchDateConditionComboBox.SelectedIndex == 2)
            {
                // 예약일+출발일 검색
                startReservationDateTimePicker.Enabled = true;
                endReservationDateTimePicker.Enabled = true;

                startDepartureDateTimePicker.Enabled = true;
                endDepartureDateTimePicker.Enabled = true;

                startDepartureDateTimePicker.Focus();
            }
        }

        private void reservationDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenReservationDetailMgtForm();
            }
        }

        // 폼 키보드 이벤트 처리
        private void ReservationList_KeyDown(object sender, KeyEventArgs e)
        {
            // 엔터키를 누르면 검색
            if (e.KeyCode == Keys.Enter)
            {
                searchReservationList();
            }
        }

        private void bookerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 엔터키를 누르면 검색
            if (e.KeyCode == Keys.Enter)
            {
                searchReservationList();
            }
        }
    }
}