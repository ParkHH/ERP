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
using TripERP.CommonTask;
using TripERP.CustomerMgt;

namespace TripERP.ReservationMgt
{
    public partial class PopUpCheckCutOff : Form
    {
        private string _reservationNumber = "";
        private string _partnerCustomerNumber = "";
        private string _bookerName = "";
        private string _cellPhoneNumber = "";
        private string _emailAddress = "";
        private string _cutOffType = "";
        private string _cutOffStatusCode = "";
        private string _cutOffStatusName = "";
        private string _fileName = "";
        private string _customerNumber = "";
        private string _customerName = "";
        private string _employeeNumber = "";
        private string _employeeName = "";
        private string _sendMediaCode = "";
        private DataGridView _cutOffMgtMemoDataGridView = null; 

        public PopUpCheckCutOff()
        {
            InitializeComponent();
        }

        private void PopUpCheckCutOff_Load(object sender, EventArgs e)
        {
            reservationNumberTextBox.Text = _reservationNumber;
            bookerNameTextBox.Text = _bookerName;
            cellPhoneNumberTextBox.Text = _cellPhoneNumber;
            emailAddressTextBox.Text = _emailAddress;

            if (_cutOffType == "LST_CHCK_STTS_CD")
                progressStatusTextBox.Text = "명단확인";
            else if (_cutOffType == "ARGM_CHCK_STTS_CD")
                progressStatusTextBox.Text = "수배확인";
            else if (_cutOffType == "PSPT_CHCK_STTS_CD")
                progressStatusTextBox.Text = "여권확인";
            else if (_cutOffType == "ISRC_CHCK_STTS_CD")
                progressStatusTextBox.Text = "항공확인";
            else if (_cutOffType == "AVAT_CHCK_STTS_CD")
                progressStatusTextBox.Text = "보험확인";
            else if (_cutOffType == "PRSN_CHCK_STTS_CD")
                progressStatusTextBox.Text = "개인확인";

            progressStatusComboBox.Items.Clear();

            if (_cutOffStatusCode == "")
            {
                List<CommonCodeItem> list = Global.GetCommonCodeList(_cutOffType);
                for (int i = 0; i < list.Count; i++)
                {
                    string value = list[i].Value.ToString();
                    string desc = list[i].Desc;
                    ComboBoxItem item = new ComboBoxItem(desc, value);
                    progressStatusComboBox.Items.Add(item);
                }

                if (progressStatusComboBox.Items.Count > 0)
                    progressStatusComboBox.SelectedIndex = 0;
            }
            else
            {
                progressStatusComboBox.Items.Add(new ComboBoxItem(_cutOffStatusName, _cutOffStatusCode));
            }

            if (progressStatusComboBox.Items.Count > 0)
                progressStatusComboBox.SelectedIndex = 0;

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 그리드 스타일 초기화
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            InitDataGridView();

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 공통코드 콤보박스 초기화
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            setOtherComboBox();

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 예약동반자목록 조회
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            searchPassengerList();

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 폼 입력필드 초기화
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            resetPassengerInputForm();
        }

        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = passengerListDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ////dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            //dataGridView1.RowHeadersVisible = false;
        }

        public void SetReservationNumber(string reservationNumber)
        {
            _reservationNumber = reservationNumber;
        }
        public void SetBookerName(string bookerName)
        {
            _bookerName = bookerName;
        }
        public void SetCellPhoneNumber(string cellPhoneNumber)
        {
            _cellPhoneNumber = cellPhoneNumber;
        }
        public void SetEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
        }
        public void SetCutOffType(string cutOffType)
        {
            _cutOffType = cutOffType;
        }
        public void SetCutOffStatusCode(string cutOffStatusCode)
        {
            _cutOffStatusCode = cutOffStatusCode;
        }
        public void SetCutOffStatusName(string cutOffStatusName)
        {
            _cutOffStatusName = cutOffStatusName;
        }
        public void SetCustomerNumber(string customerNumber)
        {
            _customerNumber = customerNumber;
        }
        public void SetCustomerName(string customerName)
        {
            _customerName = customerName;
        }
        public void SetEmployeeNumber(string employeeNumber)
        {
            _employeeNumber = employeeNumber; 
        }
        public void SetEmployeeName(string employeeName)
        {
            _employeeName = employeeName;
        }
        public void SetCutOffMgtMemoDataGridView(DataGridView cutOffMgtMemoDataGridView)
        {
            _cutOffMgtMemoDataGridView = cutOffMgtMemoDataGridView; 
        }


        private void selectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = ConstDefine.sapSourceDir;
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "파일 선택";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            filePathTextBox.Text = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
        }
        
        // SMS 문자 전송
        private void sendSmsButton_Click(object sender, EventArgs e)
        {
            if (cellPhoneNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("받는 사람 번호가 없습니다.");
                return; 
            }
            _sendMediaCode = "1";      // SMS
            openSMSLMSForm("1");
        }

        private void sendLmsButton_Click(object sender, EventArgs e)
        {
            if (cellPhoneNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("받는사람 번호가 없습니다.");
                return;
            }
            _sendMediaCode = "2";      // LMS
            openSMSLMSForm("2");
        }

        private void openSMSLMSForm(string IN_MSG_TYPE)
        {
            if (cellPhoneNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("받는 사람 번호가 없습니다.");
                return;
            }

            PopUpSendSMS form = new PopUpSendSMS();

            form.SetReceiverName(bookerNameTextBox.Text.Trim());
            form.SetReceiverCellPhoneNo(cellPhoneNumberTextBox.Text.Trim());
            form.SetCutOffType(_cutOffType);
            form.SetReservationNo(_reservationNumber);
            form.SetCustomerNumber(_customerNumber);
            form.SetEmailAddress(_emailAddress);
            form.SetMessageType(IN_MSG_TYPE);
            form.SetEmployeeNumber(_employeeNumber);

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void sendEmailButton_Click(object sender, EventArgs e)
        {
            if (emailAddressTextBox.Text.Trim() == "")
            {
                MessageBox.Show("받는사람 주소가 없습니다.");
                return;
            }
            if(IsValidEmailAddress(emailAddressTextBox.Text.Trim()) == true)
            {
                PopUpSendMail form = new PopUpSendMail();

                form.SetCutOffType(_cutOffType);
                form.SetReservationNo(_reservationNumber);
                form.SetCustomerNumber(_customerNumber);
                form.SetEmailAddress(_emailAddress);
                form.SetReceiverCellPhoneNo(_cellPhoneNumber);
                form.SetEmployeeNumber(_employeeNumber);

                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("email 주소가 올바르지 않습니다.");
                return;
            }

            //_sendMediaCode = "3";      // EMAIL
        }

        private bool IsValidEmailAddress(string emailAddress)
        {
            bool isValid = true;

            // 일단 기본적인 형식만 검증한다. 
            string [] emailTemp = emailAddress.Split('@'); 
            if (emailTemp.Length != 2 || emailTemp[0] == "" || emailTemp[1] == "")
                isValid = false;

            string[] domainTemp = emailTemp[1].Split('.');
            if (domainTemp.Length < 2)
                isValid = false;

            for (int i = 0; i < domainTemp.Length; i++)
            {
                if(domainTemp[i] == "")
                {
                    isValid = false;
                    break; 
                }
            }

            return isValid; 
        }

        // 저장버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            if(progressStatusComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("진행상태를 선택해 주십시오.");
                return;
            }

            // 문서발송일련번호 (쿼리내에서 max 하고 있음)
            string RSVT_NO = _reservationNumber;
            string DOCU_CD = "111"; // 문서코드
            string SMS_SND_YN = "N";
            string LMS_SND_YN = "N";
            string EMAL_SND_YN = "N";

            //string SND_MDIA_CD = Global.eSendMediaCode.email.ToString(); // 발송매체코드
            string RSVT_PRGS_DVSN_CD = (progressStatusComboBox.SelectedItem as ComboBoxItem).Value.ToString(); // 예약진행구분코드 

            switch (_sendMediaCode)
            {
                case "1":   // SMS
                        SMS_SND_YN = "Y";
                        break;
                case "2":   // LMS
                        LMS_SND_YN = "Y";
                        break;
                case "3":   // EMAIL
                        EMAL_SND_YN = "Y";
                        break;
            }

            string CUST_NO = _customerNumber;                                // 고객번호
            string SND_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   // 발송일시
            string CELL_PHNE_NO = cellPhoneNumberTextBox.Text.Trim();        // 휴대전화번호
            string FAX_NO = "";                                              // 팩스번호
            string EMAL_ADDR = emailAddressTextBox.Text.Trim();              // 이메일주소
            string POST_NO = "";                                             // 우편번호
            string BASC_ADDR = "";                                           // 기본주소
            string DTLS_MAIN = "";                                           // 상세주소
            string FILE_PATH_NM = "";                                        // 파일경로명
            string FILE_NM = _fileName;                                      // 파일명
            string RPSB_EMPL_NO = _employeeNumber;                           // 담당직원번호
            string FRST_RGST_DTM = SND_DTM; 
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;
            string RSVT_PRGS_DVSN_NAME = Global.GetCommonCodeDesc("RSVT_PRGS_DVSN_CD", RSVT_PRGS_DVSN_CD);
            string DOCU_SND_STTS_CD = "1";

            string ACPT_NO = "";                                   // 접수번호는 SMS,LMS 발송시만 UPDATE
            string DRCT_SND_CD = "0";                           // 

            string query = string.Format("CALL InsertDocuSndItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                RSVT_NO,
                ACPT_NO,
                RPSB_EMPL_NO,
                SMS_SND_YN,
                LMS_SND_YN,
                EMAL_SND_YN,
                RSVT_PRGS_DVSN_CD,
                CUST_NO,
                SND_DTM,
                CELL_PHNE_NO,
                FAX_NO,
                EMAL_ADDR,
                FRST_RGTR_ID,
                DOCU_SND_STTS_CD,
                DRCT_SND_CD
            );

            long retVal = DbHelper.ExecuteScalar(query); 
            if(retVal == -1)
            {
                MessageBox.Show("예약진행관리를 저장할 수 없습니다.");
            }
            else
            {
                _cutOffMgtMemoDataGridView.Rows.Add(FRST_RGST_DTM.Substring(0, 10), _customerName, RSVT_PRGS_DVSN_NAME, FILE_NM, _employeeName);

                MessageBox.Show("예약진행관리를 저장했습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void searchPassengerList()
        {
            passengerListDataGridView.Rows.Clear();

            string PRTN_CUST_NO = "";
            string CUST_NM = "";
            string CUST_ENG_NM = "";
            string BIRTH = "";
            string SEX_DVSN_CD = "";
            string SEX_DVSN_NM = "";
            string PRSN_CORP_DVSN_CD = "";
            string PRSN_CORP_DVSN_NM = "";

            string query = string.Format("CALL SelectAllReservationPartnerList ('{0}')", _reservationNumber);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약동반자 정보를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    PRTN_CUST_NO = dataRow["PRTN_CUST_NO"].ToString();
                    CUST_NM = dataRow["CUST_NM"].ToString();
                    CUST_ENG_NM = dataRow["CUST_ENG_NM"].ToString();
                    SEX_DVSN_CD = dataRow["SEX_DVSN_CD"].ToString();
                    SEX_DVSN_NM = dataRow["SEX_DVSN_NM"].ToString();
                    BIRTH = dataRow["BIRTH"].ToString().Substring(0,8);
                    PRSN_CORP_DVSN_CD = dataRow["PRSN_CORP_DVSN_CD"].ToString();
                    PRSN_CORP_DVSN_NM = dataRow["PRSN_CORP_DVSN_NM"].ToString();

                    passengerListDataGridView.Rows.Add(PRTN_CUST_NO, CUST_NM, CUST_ENG_NM, BIRTH, SEX_DVSN_CD, SEX_DVSN_NM, PRSN_CORP_DVSN_CD, PRSN_CORP_DVSN_NM);
                }
            }

            _partnerCustomerNumber = "";

        }

        //======================================================================================================================================================================
        // 동반자 고객 저장버튼 클릭
        //======================================================================================================================================================================
        private void savePassengerListButton_Click(object sender, EventArgs e)
        {
            string RSVT_NO = reservationNumberTextBox.Text.Trim();
            string PRTN_CUST_NO = _partnerCustomerNumber;
            string CUST_NM = passengerNameTextBox.Text.Trim();
            string CUST_ENG_NM = passengerEngNameTextBox.Text.Trim();

            string BIRTH = string.Format("{0:yyyy/MM/dd}", passengerBirthDateTextBox.Text.Trim());  // 생년월일
            if (Utils.isYYYYMMDD(BIRTH) == false)
            {
                MessageBox.Show("생년월일을 YYYYMMDD형식으로 입력하세요.");
                passengerBirthDateTextBox.SelectAll();
                passengerBirthDateTextBox.Focus();

                return;
            }

            string SEX_DVSN_CD = Utils.GetSelectedComboBoxItemValue(passengerSexDivisionComboBox);
            string PRSN_CORP_DVSN_CD = Utils.GetSelectedComboBoxItemValue(personalCorporationDivisionComboBox);
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            string customerName = "";
            string customerEngName = "";
            string birthDate = "";
            string sexDivisionCode = "";
            string personCoporporationDivisionCode = "";
            string cellPhoneNumber = "";
            string emailAddress = "";

            string query = "";
            string[] queryStringArray = new string[1];           // sql 배열
            string[] queryResultArray = new string[1];           // 건별 sql 처리 결과 리턴 배열
            int returnRowCount = 0;

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 고객명, 생년월일, 성별, 휴대폰번호, 이메일주소가 동일한 고객이 있으면 내용 체크하여 갱신
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            query = string.Format("CALL SelectCheckSameCustomerInfo ('{0}', '{1}', '{2}', '{3}')", CUST_NM, BIRTH, SEX_DVSN_CD, PRSN_CORP_DVSN_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("동일 고객 판단을 위한 고객정보 검색에 실패했습니다.");
                return;
            }

            // 동일고객 확인 및 선택 팝업 호출
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                callPopUpCheckSameCustomer(CUST_NM, BIRTH, SEX_DVSN_CD, PRSN_CORP_DVSN_CD);
                return;
            }

            if (_partnerCustomerNumber == "")
            {
                query = string.Format("CALL InsertCustomerAndRsvtPartnerItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                       RSVT_NO, CUST_NM, CUST_ENG_NM, BIRTH, SEX_DVSN_CD, PRSN_CORP_DVSN_CD, FRST_RGTR_ID);
            }
            else
            {
                // 동반자내역은 있는 건으로, 고객정보만 갱신
                query = string.Format("CALL UpdateCustInfoItemForReservationPartner ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                       PRTN_CUST_NO, CUST_NM, CUST_ENG_NM, BIRTH, SEX_DVSN_CD, PRSN_CORP_DVSN_CD, FRST_RGTR_ID);
            }

            queryStringArray[0] = query;

            var queryResult = DbHelper.ExecuteScalarAndReturnByWithTransaction(queryStringArray);
            int retVal = queryResult.Item1;
            queryResultArray = queryResult.Item2;

            if (retVal == -1)
            {
                MessageBox.Show("예약동반자 정보를 저장할 수 없습니다.");
                return;
            }

            resetPassengerInputForm();
            searchPassengerList();
        }

        //======================================================================================================================================================================
        // 고객명, 생년월일, 성별, 휴대폰번호, 이메일주소가 동일한 고객이 있으면 저장을 생략
        //======================================================================================================================================================================
        private void callPopUpCheckSameCustomer(string CUST_NM, string BIRTH, string SEX_DVSN_CD, string PRSN_CORP_DVSN_CD)
        {
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 동일고객 검색 팝업 창 기동
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            PopUpCheckSameCustomer form = new PopUpCheckSameCustomer();
            form.setCustomerName(CUST_NM);
            form.setBirthDay(BIRTH);
            form.setSexDivisionCode(SEX_DVSN_CD);
            form.setPersonCorporationDivisionCode(PRSN_CORP_DVSN_CD);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            string customerNumber = form.getCustomerNumber();

            string query = string.Format("CALL InsertRsvtPartnerItem ('{0}', '{1}', '{2}')", _reservationNumber, customerNumber, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("예약동반자명단을 등록하지 못했습니다. 운영담당자에게 연락하세요.");
                return;
            }

            resetPassengerInputForm();
            searchPassengerList();
        }

        //======================================================================================================================================================================
        // 동반자 고객목록 그리드 행 클릭
        //======================================================================================================================================================================
        private void passengerListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (passengerListDataGridView.SelectedRows.Count == 0)
                return;

            _partnerCustomerNumber = passengerListDataGridView.SelectedRows[0].Cells["PRTN_CUST_NO"].Value.ToString();
            string CUST_NM = passengerListDataGridView.SelectedRows[0].Cells["CUST_NM"].Value.ToString();
            string CUST_ENG_NM = passengerListDataGridView.SelectedRows[0].Cells["CUST_ENG_NM"].Value.ToString();
            string BIRTH = passengerListDataGridView.SelectedRows[0].Cells["BIRTH"].Value.ToString();
            string SEX_DVSN_CD = passengerListDataGridView.SelectedRows[0].Cells["SEX_DVSN_CD"].Value.ToString();
            string PRSN_CORP_DVSN_CD = passengerListDataGridView.SelectedRows[0].Cells["PRSN_CORP_DVSN_CD"].Value.ToString();

            passengerNameTextBox.Text = CUST_NM;
            passengerEngNameTextBox.Text = CUST_ENG_NM;
            passengerBirthDateTextBox.Text = BIRTH;
            Utils.SelectComboBoxItemByValue(passengerSexDivisionComboBox, SEX_DVSN_CD);
            Utils.SelectComboBoxItemByValue(personalCorporationDivisionComboBox, PRSN_CORP_DVSN_CD);
        }

        //======================================================================================================================================================================
        // 동반자 입력폼 초기화
        //======================================================================================================================================================================
        private void resetPassengerInputForm()
        {
            passengerNameTextBox.Text = "";
            passengerEngNameTextBox.Text = "";
            personalCorporationDivisionComboBox.SelectedIndex = 0;
            passengerBirthDateTextBox.Text = "";
            passengerSexDivisionComboBox.SelectedIndex = -1;
        }



        private void close1Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 개인법인구분코드, 성별구분코드 콤보박스 설정
        private void setOtherComboBox()
        {
            string[] groupNameArray = { "PRSN_CORP_DVSN_CD", "SEX_DVSN_CD" };

            ComboBox[] comboBoxArray = { personalCorporationDivisionComboBox, passengerSexDivisionComboBox };

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

        //======================================================================================================================================================================
        // 동반자 삭제버튼 클릭
        //======================================================================================================================================================================
        private void deletePassengerListButton_Click(object sender, EventArgs e)
        {
            if (_reservationNumber == "")
            {
                MessageBox.Show("예약번호가 지정되지 않았습니다. 예약번호를 확인하세요.");
                return;
            }

            if (_partnerCustomerNumber == "")
            {
                MessageBox.Show("삭제하고자 하는 동반자가 선택되지 않았습니다. 목록에서 삭제대상 동반자를 선택하세요.");
                passengerListDataGridView.Focus();
                return;
            }

            string query = string.Format("CALL DeleteRsvtPartnerItem ('{0}', '{1}')", _reservationNumber, _partnerCustomerNumber);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("예약동반자 정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                resetPassengerInputForm();
                searchPassengerList();
            }
        }

        private void passengerBirthDateTextBox_Click(object sender, EventArgs e)
        {
            if (passengerBirthDateTextBox.Text.Trim().Length == 0) passengerBirthDateTextBox.Text = "YYYYMMDD";
            this.passengerBirthDateTextBox.SelectAll();
        }

        private void passengerBirthDateTextBox_Leave(object sender, EventArgs e)
        {
            if (passengerBirthDateTextBox.Text.Trim().Equals("YYYYMMDD")) passengerBirthDateTextBox.Text = "";
        }

        //======================================================================================================================================================================
        // 동반자목록 그리드 키보드 선택
        //======================================================================================================================================================================
        private void passengerListDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = passengerListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0) return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && passengerListDataGridView.Rows.Count == rowIndex + 1) return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (passengerListDataGridView.SelectedRows.Count == 0)
                return;

            _partnerCustomerNumber = passengerListDataGridView.Rows[rowIndex].Cells["PRTN_CUST_NO"].Value.ToString();
            string CUST_NM = passengerListDataGridView.Rows[rowIndex].Cells["CUST_NM"].Value.ToString();
            string CUST_ENG_NM = passengerListDataGridView.Rows[rowIndex].Cells["CUST_ENG_NM"].Value.ToString();
            string BIRTH = passengerListDataGridView.Rows[rowIndex].Cells["BIRTH"].Value.ToString();
            string SEX_DVSN_CD = passengerListDataGridView.Rows[rowIndex].Cells["SEX_DVSN_CD"].Value.ToString();
            string PRSN_CORP_DVSN_CD = passengerListDataGridView.Rows[rowIndex].Cells["PRSN_CORP_DVSN_CD"].Value.ToString();

            passengerNameTextBox.Text = CUST_NM;
            passengerEngNameTextBox.Text = CUST_ENG_NM;
            passengerBirthDateTextBox.Text = BIRTH;
            Utils.SelectComboBoxItemByValue(passengerSexDivisionComboBox, SEX_DVSN_CD);
            Utils.SelectComboBoxItemByValue(personalCorporationDivisionComboBox, PRSN_CORP_DVSN_CD);
        }

        //======================================================================================================================================================================
        // 동반자목록 입력 초기화버튼 클릭
        //======================================================================================================================================================================
        private void resetButton_Click(object sender, EventArgs e)
        {
            resetPassengerInputForm();
        }
    }
}
