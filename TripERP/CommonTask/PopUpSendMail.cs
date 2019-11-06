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

using System.Net;
using System.Net.Mail;

namespace TripERP.CommonTask
{
    public partial class PopUpSendMail : Form
    {
        enum eEmailtemplateListDataGridView { CHAR_MSG_NO, CHAR_MSG_TITLE_NM, CHAR_MSG_CNTS };

        private string _cutOffType = "";            // PopUpCheckCutOff에서 넘기는 예약진행구분코드 08/08(문서발송관리내역 --> 저장이력/발송이력 분리)
        private string _customerNo = "";            // 고객번호
        private string _reservationNo = "";         // 예약번호
        private string _emailAddr = "";             // 이메일주소
        private string _receiverCellPhoneNo = "";   // 핸드폰번호
        private string _employeeNum = "";       // 직원번호

        private bool _EDITED = false;


        private string _filePath = "";                  // 파일경로

        public PopUpSendMail()
        {
            InitializeComponent();
        }

        private void PopUpSendMail_Load(object sender, EventArgs e)
        {
            // 배장훈 07/31 ~
            InitControls();
        }

        // 배장훈 07/31 ~
        private void InitControls()
        {
            // 입력박스 초기화
            ResetInputFormField();

            // 콤보박스 초기화
            ResetComboBox();

            // DataGridView 스타일 초기화
            InitDataGridView();
        }

        // 배장훈 07/31 ~ 입력폼 초기화
        private void ResetInputFormField()
        {
            SearchTemplateContentsTextBox.Text = "";
            FileNameTextBox.Text = "";

            EmailTemplateListDataGridView.Rows.Clear();
        }

        // 콤보박스 초기화
        private void ResetComboBox()
        {
            SearchTemplateTitleComboBox.Items.Clear();
            SearchTemplateTitleComboBox.Text = "";

            //List<CommonCodeItem> list = Global.GetCommonCodeList("CHAR_MSG_DVSN_CD");

            //for (int li = 0; li < list.Count; li++)
            //{
            //    if (!list[li].Desc.Equals("이메일"))
            //        break;
            //    string value = list[li].Value.ToString();
            //    string desc = list[li].Desc;

            //    ComboBoxItem item = new ComboBoxItem(desc, value);

            //    SearchTemplateTitleComboBox.Items.Add(item);
            //}

            //if (SearchTemplateTitleComboBox.Items.Count > 0)
            //    SearchTemplateTitleComboBox.SelectedIndex = 0;

            string query = "";
            DataSet dataSet;

            query = "CALL SelectTemplateTitle ('3', '3')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("템플릿 메시지 제목을 가져올 수 없습니다.");
                return;
            }

            SearchTemplateTitleComboBox.Items.Add(new ComboBoxItem("전체", ""));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CHAR_MSG_DVSN_CD = dataRow["CHAR_MSG_DVSN_CD"].ToString();
                string CHAR_MSG_TITL_NM = dataRow["CHAR_MSG_TITL_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CHAR_MSG_TITL_NM, CHAR_MSG_DVSN_CD);
                SearchTemplateTitleComboBox.Items.Add(item);
            }
            if (SearchTemplateTitleComboBox.Items.Count > 0)
                SearchTemplateTitleComboBox.SelectedIndex = 0;

            searchEmailTemplateList();
        }

        // 데이터 그리드뷰 초기화
        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = EmailTemplateListDataGridView;
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

        // 템플릿 검색 ~ 배장훈
        private void searchCostPriceListButton_Click(object sender, EventArgs e)
        {
            searchEmailTemplateList();
        }

        private void searchEmailTemplateList()
        {
            EmailTemplateListDataGridView.Rows.Clear();

            string CHAR_MSG_TITL_NM = SearchTemplateTitleComboBox.Text;                                    // 문자메시지제목명
            string CHAR_MSG_CNTS = SearchTemplateContentsTextBox.Text.Trim();                               // 문자메시지내용
            string CHAR_MSG_NO = "";

            if (CHAR_MSG_TITL_NM.Equals("전체"))
                CHAR_MSG_TITL_NM = "";

            string query = string.Format("CALL SelectEMailSendTemplateList ('{0}', '{1}')", CHAR_MSG_TITL_NM, CHAR_MSG_CNTS);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("문자메시지 템플릿정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                CHAR_MSG_NO = datarow["CHAR_MSG_NO"].ToString();
                CHAR_MSG_TITL_NM = datarow["CHAR_MSG_TITL_NM"].ToString();
                CHAR_MSG_CNTS = datarow["CHAR_MSG_CNTS"].ToString();

                EmailTemplateListDataGridView.Rows.Add(CHAR_MSG_NO, CHAR_MSG_TITL_NM, CHAR_MSG_CNTS);
            }

            EmailTemplateListDataGridView.ClearSelection();
        }

        private void EmailTemplateListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmailTemplateListDataGridView.SelectedRows.Count == 0)
                return;

            string CHAR_MSG_NO = EmailTemplateListDataGridView.SelectedRows[0].Cells[(int)eEmailtemplateListDataGridView.CHAR_MSG_NO].Value.ToString();
            string CHAR_MSG_TITL_NM = EmailTemplateListDataGridView.SelectedRows[0].Cells[(int)eEmailtemplateListDataGridView.CHAR_MSG_TITLE_NM].Value.ToString();
            string CHAR_MSG_CNTS = EmailTemplateListDataGridView.SelectedRows[0].Cells[(int)eEmailtemplateListDataGridView.CHAR_MSG_CNTS].Value.ToString();

            txt_mail_subject.Text = CHAR_MSG_TITL_NM;
            txt_mail_body.Text = CHAR_MSG_CNTS;
        }

        private void btn_Mail_Send_2_Click(object sender, EventArgs e)
        {
            Mail_send_MailMessage();
        }

        private void Mail_send_MailMessage()
        {
            MessageBox.Show(Keys.Enter.ToString());
            // Gmail SMTP : smtp.gmail.com / Port 587 
            // 보안 수준이 낮은 앱 허용: 사용 (https://myaccount.google.com/lesssecureapps)

            // 입력값 유효성 검증
            if (!CheckRequireItems())
                return;

            string myString= txt_mail_body.Text.Trim();
            myString = myString.Replace(System.Environment.NewLine, "<br>");

            MailMessage message = new MailMessage();
            message.From = new MailAddress(txt_mail_sender.Text.Trim(), txt_mail_sender_name.Text.Trim(), System.Text.Encoding.UTF8);

            message.To.Add(txt_mail_receiver.Text.Trim());

            //---------------------------------------
            if (txt_mail_receiver_cc.Text.Trim().Length != 0)
            {
                MailAddress cc = new MailAddress(txt_mail_receiver_cc.Text.Trim());
                message.CC.Add(cc);             // 참조
                message.Bcc.Add(cc);            // 숨은참조
            }

            //---------------------------------------
            message.Subject = txt_mail_subject.Text.Trim();
            message.SubjectEncoding = UTF8Encoding.UTF8;
            //---------------------------------------
            message.Body = myString;       //  "메일 내용입니다. - _Attachment.jpg를 첨부 (특수문자)";
            message.BodyEncoding = UTF8Encoding.UTF8;
            //---------------------------------------
            message.IsBodyHtml = true;                                          // 메일형식지정HTML
            message.Priority = MailPriority.High;                               // 중요도 높음
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;        // 메일 배달 실패시 알림

            // 이메일 첨부 배장훈 0808 START ~
            // 파일첨부 예외처리
            if (!_filePath.Equals(""))
            {
                Attachment attFile = new Attachment(_filePath);
                message.Attachments.Add(attFile);
            }
            
            // ~ END

            //---------------------------------------
            SmtpClient client = new SmtpClient();
            //client.Host = txt_SMTP.Text.Trim();                     // SMTP(발송)서버 도메인
            //client.Port = Convert.ToInt16(UpDn_port.Value);         // 587, SMTP서버 포트
            client.Host = "smtp.gmail.com";                     // SMTP(발송)서버 도메인
            client.Port = 587;         // 587, SMTP서버 포트
            client.EnableSsl = true;                                // SSL 사용
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            //보내는 사람 메일 서버접속계정, 암호, Anonymous이용시 생략
            //client.Credentials = new System.Net.NetworkCredential(txt_mail_id.Text.Trim(), txt_mail_pw.Text.Trim());    
            client.Credentials = new System.Net.NetworkCredential(txt_mail_sender.Text.Trim(), txt_mail_pw.Text.Trim());

            try
            {
                client.Send(message);
                message.Dispose();
                SaveDocumentHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString(), "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 발송이력 저장
        private void SaveDocumentHistory()
        {
            // 문서발송일련번호 (쿼리내에서 max 하고 있음)
            string ACPT_NO = "";
            string RSVT_NO = _reservationNo;                                // 예약번호
            string SMS_SND_YN = "N";                                        // SMS전송여부
            string LMS_SND_YN = "N";                                        // LMS전송여부
            string EMAL_SND_YN = "Y";                                       // 이메일발송여부
            string RSVT_PRGS_DVSN_CD = "";                                  // 예약진행구분코드 
            string CUST_NO = _customerNo;                                   // 고객번호
            string SND_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  // 전송일자
            string CELL_PHNE_NO = _receiverCellPhoneNo;                     // 휴대전화번호
            string FAX_NO = "";                                             // 팩스번호
            string EMAL_ADDR = txt_mail_receiver.Text.Trim();                                  // 이메일주소
            string DOCU_SND_STTS_CD = "100";                                  // 문서발송상태코드(팝빌)
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;
            string RPSB_EMPL_NO = _employeeNum;                                       // 직원번호
            
            // 예약번호가 빈경우 수기로 입력한 상태
            string query = "";
            string DRCT_SND_CD = "0";

            if (RPSB_EMPL_NO.Equals(""))
                DRCT_SND_CD = "1";

            if (RSVT_NO.Equals(""))
            {
                // 예약정보 검색
                string IN_EMAL_ADDR = txt_mail_receiver.Text.Trim();

                query = string.Format("CALL SelectCustomerNumByCellPhone ('{0}', 'EMAIL')", IN_EMAL_ADDR);
                DataSet dataSet = DbHelper.SelectQuery(query);
                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    CUST_NO = "0";
                    //MessageBox.Show("예약번호를 가져올 수 없습니다.");
                    // return;
                }
                else
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    CUST_NO = dataRow["CUST_NO"].ToString();
                }
            }

            if (_cutOffType == "LST_CHCK_STTS_CD")
                RSVT_PRGS_DVSN_CD = "명단확인";
            else if (_cutOffType == "ARGM_CHCK_STTS_CD")
                RSVT_PRGS_DVSN_CD = "수배확인";
            else if (_cutOffType == "PSPT_CHCK_STTS_CD")
                RSVT_PRGS_DVSN_CD = "여권확인";
            else if (_cutOffType == "ISRC_CHCK_STTS_CD")
                RSVT_PRGS_DVSN_CD = "항공확인";
            else if (_cutOffType == "AVAT_CHCK_STTS_CD")
                RSVT_PRGS_DVSN_CD = "보험확인";
            else if (_cutOffType == "PRSN_CHCK_STTS_CD")
                RSVT_PRGS_DVSN_CD = "개인확인";

            

            query = string.Format("CALL InsertDocuSndItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
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
            if (retVal == -1)
            {
                MessageBox.Show("발송이력을 저장할 수 없습니다.");
            }
            else
            {
                MessageBox.Show("발송이력을 저장했습니다.");
                // this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }

        private bool CheckRequireItems()
        {
            // txt_mail_sender txt_mail_pw

            if (txt_mail_sender.Text.ToString().Equals(""))
            {
                MessageBox.Show("발신자 이메일주소는 필수 입력항목입니다.");
                txt_mail_sender.Focus();
                return false;
            }

            if (txt_mail_pw.Text.ToString().Equals(""))
            {
                MessageBox.Show("발신자 이메일주소의 비밀번호는 필수 입력항목입니다.");
                txt_mail_pw.Focus();
                return false;
            }

            if (txt_mail_receiver.Text.ToString().Equals(""))
            {
                MessageBox.Show("수신자 이메일주소는 필수 입력항목입니다.");
                txt_mail_receiver.Focus();
                return false;
            }

            if (txt_mail_subject.Text.ToString().Equals(""))
            {
                MessageBox.Show("메일 제목은 필수 입력항목입니다.");
                txt_mail_subject.Focus();
                return false;
            }

            if (txt_mail_body.Text.ToString().Equals(""))
            {
                MessageBox.Show("메일 내용은 필수 입력항목입니다.");
                txt_mail_body.Focus();
                return false;
            }
            return true;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 받는 주소명에 엔터키를 누르면 예약목록 검색 창을 띄워 고객을 선택하도록 함 ~ 배장훈
        private void txt_mail_receiver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchReservationCustomerForm();
            }
        }

        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            SearchReservationCustomerForm();
        }

        private void SearchReservationCustomerForm()
        {
            PopUpSearchReservationInfo form = new PopUpSearchReservationInfo();

            // form.SetCustomerName(txt_mail_receiver.Text.Trim());

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            // 선택된 이메일주소 화면에 표시
            txt_mail_receiver.Text = form.GetReservationEmailAddress();
            _reservationNo = form.GetReservationNumber();
            _customerNo = form.GetCustomerNumber();
            _receiverCellPhoneNo = form.GetReservationPhoneNumber();
            _emailAddr = form.GetReservationEmailAddress();
        }

        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FileSearchButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = ConstDefine.sapSourceDir;
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "파일 선택";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            FileNameTextBox.Text = openFileDialog.SafeFileName;
            _filePath = openFileDialog.FileName;
        }

        private void FileDeleteButton_Click(object sender, EventArgs e)
        {
            FileNameTextBox.Text = "";
            _filePath = "";
        }

        public void SetCutOffType(string IN_CUTOFF_TYPE)
        {
            _EDITED = true;
            _cutOffType = IN_CUTOFF_TYPE;
        }

        public void SetReservationNo(string IN_RESERVATION_NO)
        {
            _reservationNo = IN_RESERVATION_NO;
            // ReservationNumberTextBox.Text = _reservationNo;
        }

        public void SetCustomerNumber(string IN_CUSTOMER_NO)
        {
            _customerNo = IN_CUSTOMER_NO;
        }

        public void SetEmailAddress(string IN_EMAIL_ADDR)
        {
            _emailAddr = IN_EMAIL_ADDR;
            txt_mail_receiver.Text = _emailAddr;
        }

        public void SetReceiverCellPhoneNo(string receiverCellPhoneNo)
        {
            _receiverCellPhoneNo = receiverCellPhoneNo;
        }

        public void SetEmployeeNumber(string IN_EMPLOYEE_NUM)
        {
            _employeeNum = IN_EMPLOYEE_NUM;
        }

        private void txt_mail_receiver_cc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_mail_receiver_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class Mail    
    {

    }

}
