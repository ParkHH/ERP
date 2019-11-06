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
using System.Threading;

using Popbill.Message;
using System.IO;
using System.Text.RegularExpressions;

namespace TripERP.CommonTask
{
    public partial class PopUpSendSMS : Form
    {
        enum eSMStemplateListDataGridView { CHAR_MSG_NO, CHAR_MSG_DVSN_CD, CHAR_MSG_DVSN_NM, CHAR_MSG_TITLE_NM, CHAR_MSG_CNTS };

        private string CHAR_MSG_NO = "";
        private string CHAR_MSG_DVSN_CD = "";
        private string CHAR_MSG_DVSN_NM = "";
        private string CHAR_MSG_TITL_NM = "";
        private string CHAR_MSG_CNTS = "";
       
        private MessageService messageService;

        private string strCorpNum = "2088700871";       // 사업자등록번호
        private string strUserId = "hanih77";           // 팝빌회원ID
        private string LinkID = "GBRIDGE";              // 링크아이디
        private string SecretKey = "WWVXq4omBaaw9pPoHn+lUV/a94WAIJupAURLBuKYaoc=";  //비밀키
        private const string CRLF = "\r\n";

        private string _receiverName = "";
        private string _receiverCellPhoneNo = "";

        private string log_sms = "";
        private string log_lms = "";

        private ListView lv_string = new ListView();
        private string[] txt_string = {"subject(메시지 제목) ", "content(메시지 내용)",         "sendNum(발신번호)",
                                       "senderName(발신자명) ", "receiveNum(수신번호)",         "receiveName(수신자명)",
                                       "receiptDT(접수시간)  ", "sendDT(전송일시)",             "resultDT(전송결과 수신시간)",
                                       "reserveDT(예약일시)  ", "state(전송 상태코드)",         "result(전송 결과코드)",
                                       "type(메시지 타입)    ", "tranNet(전송처리 이동통신사명)","receiptNum(접수번호)",   "requestNum(요청번호)" };

        private string _cutOffType = ""; // PopUpCheckCutOff에서 넘기는 예약진행구분코드 08/08(문서발송관리내역 --> 저장이력/발송이력 분리)
        private string _customerNo = "";
        private string _reservationStatement = "";
        private string _reservationNo = "";
        private string _emailAddr = "";
        private string _messageType = "";
        private string state_code;     // 문서발송상태코드
        private string result_code;    // 문서발송상태코드
        private string receiptNum = ""; // 접수번호
        private string _employeeNum = ""; // 직원번호

        private bool _EDITED = false;
        
        public PopUpSendSMS()
        {
            InitializeComponent();
            // 문자 서비스 모듈 초기화
            messageService = new MessageService(LinkID, SecretKey);
            // 연동환경 설정값, true(개발용), false(상업용)
            messageService.IsTest = true;
        }

        private void PopUpSendSMS_Load(object sender, EventArgs e)
        {
            // 에어발생

            //lbl_strCorpNum.Text = strCorpNum;
            //lbl_strUserId.Text = strUserId;
            //lbl_LinkID.Text = LinkID;
            //lbl_SecretKey.Text = SecretKey;

            // listview_string_init();

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

            // DataGridView스타일 초기화
            InitDataGridView();

            searchMessageTemplateList();

        }

        // 배장훈 07/31 ~ 입력폼 초기화
        private void ResetInputFormField()
        {
            if (!_EDITED)
            {
                SearchTemplateContentsTextBox.Text = "";
                ReservationNumberTextBox.Text = "";
            }

            SMStemplateListDataGridView.Rows.Clear();
        }

        private void ResetComboBox()
        {
            SearchTemplateTypeComboBox.Items.Clear();
            SearchTemplateTypeComboBox.Text = "";
            SearchTemplateTitleComboBox.Items.Clear();
            SearchTemplateTitleComboBox.Text = "";

            List<CommonCodeItem> list = Global.GetCommonCodeList("CHAR_MSG_DVSN_CD");

            SearchTemplateTypeComboBox.Items.Add(new ComboBoxItem("전체", ""));
            for (int li = 0; li < list.Count; li++)
            {
                if (list[li].Desc.Equals("이메일"))
                    break;
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                SearchTemplateTypeComboBox.Items.Add(item);
            }

            if (SearchTemplateTypeComboBox.Items.Count > 0)
                SearchTemplateTypeComboBox.SelectedIndex = 0;
            if (_EDITED)
            {
                if (_messageType.Equals("1"))
                    SearchTemplateTypeComboBox.SelectedIndex = 1;
                else
                    SearchTemplateTypeComboBox.SelectedIndex = 2;
            }
                

            string query = "";
            DataSet dataSet;

            query = "CALL SelectTemplateTitle ('1', '2')";
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


            if (_EDITED)
                searchMessageTemplateList();
        }

        // 데이터그리드뷰 초기화
        private void InitDataGridView()
        {
            DataGridView dataGridView1 = SMStemplateListDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 254);
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
        }

        private void searchSMSTemplate()
        {
            SMStemplateListDataGridView.Rows.Clear();

            string CHAR_MSG_NO = "";
            string CHAR_MSG_TITL_NM = SearchTemplateContentsTextBox.Text.Trim();

            string query = string.Format("CALL SelectCharTemplateList ( '{0}', '{1}' )", "1", CHAR_MSG_TITL_NM);  // "1" SMS,  "2" LMS
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("문자템플릿정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                CHAR_MSG_NO = Utils.GetString(dataRow["CHAR_MSG_NO"]);              // 문자메시지번호
                CHAR_MSG_TITL_NM = Utils.GetString(dataRow["CHAR_MSG_TITL_NM"]);    // 문자메시지제목

                SMStemplateListDataGridView.Rows.Add(CHAR_MSG_NO, CHAR_MSG_TITL_NM);
            }

            SMStemplateListDataGridView.ClearSelection();
        }


        public void SetReceiverName(string receiverName)
        {
            _receiverName = receiverName;
            receiverNameTextBox.Text = receiverName;
        }

        public void SetReceiverCellPhoneNo(string receiverCellPhoneNo)
        {
            _receiverCellPhoneNo = receiverCellPhoneNo;
            rxCellPhoneNoTextBox.Text = receiverCellPhoneNo;
        }

        // 폼 닫기
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 템플릿 검색 ~ 배장훈
        private void searchCostPriceListButton_Click(object sender, EventArgs e)
        {
            searchMessageTemplateList();
        }

        // 문자메시지 템플릿 목록 조회 ~ 배장훈
        private void searchMessageTemplateList()
        {
            SMStemplateListDataGridView.Rows.Clear();

            string CHAR_MSG_DVSN_CD = Utils.GetSelectedComboBoxItemValue(SearchTemplateTypeComboBox);     // 문자템플릿구분(SMS, LMS, 이메일)
            string CHAR_MSG_TITL_NM = SearchTemplateTitleComboBox.Text;                                    // 문자메시지제목명
            string CHAR_MSG_CNTS = SearchTemplateContentsTextBox.Text.Trim();                               // 문자메시지내용
            string CHAR_MSG_NO = "";
            string CHAR_MSG_DVSN_NM = "";

            if (CHAR_MSG_TITL_NM.Equals("전체"))
                CHAR_MSG_TITL_NM = "";

            string query = string.Format("CALL SelectMessageSendTemplateList ('{0}', '{1}', '{2}')", CHAR_MSG_DVSN_CD, CHAR_MSG_TITL_NM, CHAR_MSG_CNTS);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("문자메시지 템플릿정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                CHAR_MSG_NO = datarow["CHAR_MSG_NO"].ToString();
                CHAR_MSG_DVSN_CD = datarow["CHAR_MSG_DVSN_CD"].ToString();
                CHAR_MSG_DVSN_NM = datarow["CHAR_MSG_DVSN_NM"].ToString();
                CHAR_MSG_TITL_NM = datarow["CHAR_MSG_TITL_NM"].ToString();
                CHAR_MSG_CNTS = datarow["CHAR_MSG_CNTS"].ToString();

                SMStemplateListDataGridView.Rows.Add(CHAR_MSG_NO, CHAR_MSG_DVSN_CD, CHAR_MSG_DVSN_NM, CHAR_MSG_TITL_NM, CHAR_MSG_CNTS);
            }

            SMStemplateListDataGridView.ClearSelection();
        }

        // 템플릿 그리드 클릭 ~ 배장훈
        private void SMStemplateListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SMStemplateListDataGridView.SelectedRows.Count == 0)
                return;

            CHAR_MSG_NO = SMStemplateListDataGridView.SelectedRows[0].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_NO].Value.ToString();
            CHAR_MSG_DVSN_CD = SMStemplateListDataGridView.SelectedRows[0].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_DVSN_CD].Value.ToString();
            CHAR_MSG_DVSN_NM = SMStemplateListDataGridView.SelectedRows[0].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_DVSN_NM].Value.ToString();
            CHAR_MSG_TITL_NM = SMStemplateListDataGridView.SelectedRows[0].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_TITLE_NM].Value.ToString();
            CHAR_MSG_CNTS = SMStemplateListDataGridView.SelectedRows[0].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_CNTS].Value.ToString();

            SMStemplateDataGridViewChoice();
        }

        private void SMStemplateDataGridViewChoice()
        {
            if (CHAR_MSG_DVSN_CD.Equals("1"))
                rTxt_sms_contents.Text = CHAR_MSG_TITL_NM + "\n" + CHAR_MSG_CNTS;
            else
            {
                txt_lms_subject.Text = CHAR_MSG_TITL_NM;
                rTxt_lms_contents.Text = CHAR_MSG_CNTS;
            }  // ReservationNumberTextBox
        }
        // 예약자명에 엔터키를 누르면 예약목록 검색 창을 띄워 고객을 선택하도록 함 ~ 배장훈
        private void receiverNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchReservationCustomerForm();
            }
        }

        // 버튼 클릭으로도 예약자 목록 띄우게
        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            SearchReservationCustomerForm();
        }

        //
        private void SearchReservationCustomerForm()
        {
            PopUpSearchReservationInfo form = new PopUpSearchReservationInfo();

            // form.SetCustomerName(receiverNameTextBox.Text.Trim());

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            // 선택된 예약번호를 화면에 표시
            ReservationNumberTextBox.Text = form.GetReservationNumber();
            rxCellPhoneNoTextBox.Text = form.GetReservationPhoneNumber();
            receiverNameTextBox.Text = form.GetReceiverName();
            _emailAddr = form.GetReservationEmailAddress();
            _customerNo = form.GetCustomerNumber();
            _reservationStatement = form.GetReservationProgress();

            //ReservationProgressComboBox.Items.Clear();
            //List<CommonCodeItem> list = Global.GetCommonCodeList("RSVT_STTS_CD");
            //for (int i = 0; i < list.Count; i++)
            //{
            //    string value = list[i].Value.ToString();
            //    string desc = list[i].Desc;
            //    ComboBoxItem item = new ComboBoxItem(desc, value);
            //    ReservationProgressComboBox.Items.Add(item);
            //}
            
            //if (ReservationProgressComboBox.Items.Count > 0)
            //    ReservationProgressComboBox.SelectedIndex = Convert.ToInt16(form.GetReservationProgress());

            // 예약정보를 화면에 표시
            // SetReservationInfoToForm();
        }

        #region

        private void btn_tx_sms_Click(object sender, EventArgs e)
        {
            String senderNum = txCellPhoneNoTextBox.Text.Trim();    // 발신번호 
            String receiver = rxCellPhoneNoTextBox.Text.Trim();     // 수신번호
            String receiverName = receiverNameTextBox.Text.Trim();  // 수신자명 
            String contents = rTxt_sms_contents.Text.Trim() + "-" + DateTime.Now.ToLongDateString();
            String requestNum = "";
            Boolean adsYN = false;                                  // 광고문자여부 (기본값 false)

            log_sms = Application.StartupPath + "\\Log\\sms-" + DateTime.Now.ToString("yyyyMMdd").ToString() + ".txt";

            // 입력값 유효성 검증
            if (!CheckRequireItems("1"))
                return;

            try
            {
                string receiptNum = messageService.SendSMS(strCorpNum, senderNum, receiver,
                                    receiverName, contents, getReserveDT(), strUserId, requestNum, adsYN);
                MessageBox.Show("접수번호 : " + receiptNum, "단문(SMS) 전송");
                txt_ReceiptNum_search.Text = receiptNum;

                result_code = SearchReceiptResult(receiptNum);

                log_save_system(log_sms, receiptNum);
               
                SaveDocumentHistory("1"); // SMS
            }
            catch (Popbill.PopbillException ex)
            {
                MessageBox.Show(this, "응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                     "응답메시지(message) : " + ex.Message, "단문(SMS) 전송");
            }
        }

        private void btn_tx_lms_Click(object sender, EventArgs e)
        {
            String senderNum = txCellPhoneNoTextBox.Text.Trim();    // 발신번호 
            String receiver = rxCellPhoneNoTextBox.Text.Trim();     // 수신번호
            String receiverName = receiverNameTextBox.Text.Trim();  // 수신자명 
            String subject = txt_lms_subject.Text.Trim();           // LMS문자제목
            String contents = rTxt_lms_contents.Text.Trim() + "-" + DateTime.Now.ToLongDateString();
            String requestNum = "";
            Boolean adsYN = false;

            if (!CheckRequireItems("2"))
                return;
            try
            {
                string receiptNum = messageService.SendLMS(strCorpNum, senderNum, receiver,
                                    receiverName, subject, contents, getReserveDT(), strUserId, requestNum, adsYN);
                MessageBox.Show(this, "접수번호 : " + receiptNum, "LMS(장문) 전송");
                txt_ReceiptNum_search.Text = receiptNum;

                result_code = SearchReceiptResult(receiptNum);

                log_save_system(log_lms, receiptNum);

                SaveDocumentHistory("2"); // LMS
            }
            catch (Popbill.PopbillException ex)
            {
                MessageBox.Show(this, "응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "LMS(장문) 전송");
            }
        }

        private DateTime? getReserveDT()
        {
            DateTime? reserveDT = null;
            if (String.IsNullOrEmpty(txt_ReserveDT.Text) == false)
            {
                reserveDT = DateTime.ParseExact(txt_ReserveDT.Text, "yyyyMMddHHmmss",
                    System.Globalization.CultureInfo.InvariantCulture);
            }

            return reserveDT;
        }

        // 발송이력 저장
        private void SaveDocumentHistory(string IN_DOCU_CD)
        {
            // 문서발송일련번호 (쿼리내에서 max 하고 있음)
            string RSVT_NO = ReservationNumberTextBox.Text.Trim();          // 예약번호
            string ACPT_NO = txt_ReceiptNum_search.Text.Trim();                        // 접수번호
            string SMS_SND_YN = "N";                                        // SMS전송여부
            string LMS_SND_YN = "N";                                        // LMS전송여부
            string EMAL_SND_YN = "N";                                       // 이메일발송여부
            string RSVT_PRGS_DVSN_CD = "";                                  // 예약진행구분코드 
            string CUST_NO = _customerNo;                                   // 고객번호
            string SND_DTM = "";                                            // 전송일자
            string CELL_PHNE_NO = rxCellPhoneNoTextBox.Text.Trim();         // 휴대전화번호
            string FAX_NO = "";                                             // 팩스번호
            string EMAL_ADDR = _emailAddr;                                  // 이메일주소
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;     
            string DOCU_SND_STTS_CD = "";                          // 문서발송상태코드(팝빌)
            string RPSB_EMPL_NO = _employeeNum;                 // 직원번호

            // 예약번호가 빈경우 수기로 입력한 상태
            string query = "";
            string DRCT_SND_CD = "0";

            if (RPSB_EMPL_NO.Equals(""))
                DRCT_SND_CD = "1";

            if (RSVT_NO.Equals(""))
            {
                // 예약정보 검색
                string CUST_NM = receiverNameTextBox.Text.Trim();

                query = string.Format("CALL SelectCustomerNumByCellPhone ('{0}', 'LMS')", CELL_PHNE_NO);
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
                

            if(txt_ReserveDT.Text.Equals(""))
                SND_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   // 발송일시
            else
                SND_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   // 일단 고정

            // SMS, LMS 분류
            switch (IN_DOCU_CD)
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

            // 예약진행구분코드
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

            // 문서발송상태코드 


            if (result_code.Equals("100")) // 발송 성공
                DOCU_SND_STTS_CD = "1";
            else                           // 발송 실패
            {
                if (result_code.Equals(""))
                    DOCU_SND_STTS_CD = "0"; // 미분류 오류
                else if (result_code.Substring(0, 1).Equals("2"))
                    DOCU_SND_STTS_CD = "2"; // 메세지 형식 오류 
                else if (result_code.Substring(0, 1).Equals("3"))
                    DOCU_SND_STTS_CD = "3"; // 발신 오류
                else if (result_code.Substring(0, 1).Equals("4"))
                    DOCU_SND_STTS_CD = "4"; // 수신 및 착신 오류
                else if (result_code.Substring(0, 1).Equals("5"))
                    DOCU_SND_STTS_CD = "5"; // 이동통신사 오류
                else if (result_code.Substring(0, 1).Equals("8"))
                    DOCU_SND_STTS_CD = "8"; // 수신 거부
                else if (result_code.Substring(0, 1).Equals("9"))
                    DOCU_SND_STTS_CD = "9"; // 기타 오류
                
            }

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

        private void log_save_system(string _filename, string _msg)
        {
            try
            {
                FileInfo File_Info = new FileInfo(_filename);
                if (File_Info.Exists == false)
                {
                    StreamWriter SaveSm_new = new StreamWriter(_filename, true, System.Text.Encoding.Default);
                    SaveSm_new.Flush();
                    SaveSm_new.Close();
                }

                StreamWriter SaveSm = new StreamWriter(_filename, true, System.Text.Encoding.Default);
                SaveSm.Write(_msg + System.Environment.NewLine);
                SaveSm.Flush();
                SaveSm.Close();
            }
            catch (Exception ex)
            {
            }


        }

        private void trace_log(int _type, string _str)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            try
            {
                str.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").PadRight(20, ' ') + _str);

                // tem.tab_tr_gr_lbl[tr][gr].Text = new string(' ', 6) + new_form.txt_gr_name.Text.Trim();

            }
            catch (Exception ex)
            {
            }
            str = null;
        }

        private bool CheckRequireItems(string IN_SMS_SND_YN)
        {
            if (rxCellPhoneNoTextBox.Text.ToString().Equals(""))
            {
                MessageBox.Show("수신번호는 필수 입력항목입니다.");
                rxCellPhoneNoTextBox.Focus();
                return false;
            }

            if (rTxt_sms_contents.Text.ToString().Equals("") && IN_SMS_SND_YN.Equals("1"))
            {
                MessageBox.Show("메세지 내용은 필수 입력항목입니다.");
                rTxt_sms_contents.Focus();
                return false;
            }

            if (rTxt_lms_contents.Text.ToString().Equals("") && IN_SMS_SND_YN.Equals("2"))
            {
                MessageBox.Show("메세지 내용은 필수 입력항목입니다.");
                rTxt_lms_contents.Focus();
                return false;
            }

            return true;
        }


        #endregion



        private void SMStemplateListDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

            //if (e.Data.GetDataPresent(typeof(DataGrid)))
            //{ e.Effect = DragDropEffects.Copy; }
            //else
            //{ e.Effect = DragDropEffects.None; }
        }



        public static void folder_create()
        {
            if (Directory.Exists(Application.StartupPath + "\\Log") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Log");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SearchReceiptNumResult(txt_ReceiptNum_search.Text.Trim());
        }

        private void SearchReceiptNumResult(string IN_RECEIPT_NUM)
        {
            if (IN_RECEIPT_NUM.Equals(""))
            {
                MessageBox.Show(this, "접수번호가 비어있습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lv_string.Items.Clear();
            try
            {
                //List<MessageResult> ResultList = messageService.GetMessageResult(strCorpNum, txt_ReceiptNum_search_search.Text.Trim());
                List<MessageResult> ResultList = messageService.GetMessageResult(strCorpNum, IN_RECEIPT_NUM);

                string rowStr = "subject(메시지 제목) | content(메시지 내용) | sendNum(발신번호) | senderName(발신자명) | receiveNum(수신번호) | receiveName(수신자명) |" +
                                "receiptDT(접수시간) | sendDT(전송일시) | resultDT(전송결과 수신시간) | reserveDT(예약일시) | state(전송 상태코드) | result(전송 결과코드) |" +
                                "type(메시지 타입) | tranNet(전송처리 이동통신사명) | receiptNum(접수번호) | requestNum(요청번호)";

                lv_string.EndUpdate();

                for (int i = 0; i < ResultList.Count; i++)
                {
                    lv_string.Items.Add(string.Empty);
                    lv_string.Items[i].Font = new Font("Tahoma", 8.5F, FontStyle.Regular);
                    if (i % 2 == 0)
                    { lv_string.Items[i].BackColor = Color.Snow; }
                    else
                    { lv_string.Items[i].BackColor = Color.White; }

                    lv_string.Items[i].SubItems.Add(ResultList[i].subject);
                    lv_string.Items[i].SubItems.Add(ResultList[i].content);
                    lv_string.Items[i].SubItems.Add(ResultList[i].sendNum);

                    lv_string.Items[i].SubItems.Add(ResultList[i].senderName);
                    lv_string.Items[i].SubItems.Add(ResultList[i].receiveNum);
                    lv_string.Items[i].SubItems.Add(ResultList[i].receiveName);
                    lv_string.Items[i].SubItems.Add(ResultList[i].receiptDT);
                    lv_string.Items[i].SubItems.Add(ResultList[i].sendDT);
                    lv_string.Items[i].SubItems.Add(ResultList[i].resultDT);
                    lv_string.Items[i].SubItems.Add(ResultList[i].reserveDT);
                    lv_string.Items[i].SubItems.Add(Convert.ToString(ResultList[i].state));
                    lv_string.Items[i].SubItems.Add(Convert.ToString(ResultList[i].result));
                    
                    lv_string.Items[i].SubItems.Add(ResultList[i].type);
                    lv_string.Items[i].SubItems.Add(ResultList[i].tranNet);
                    lv_string.Items[i].SubItems.Add(ResultList[i].receiptNum);
                    lv_string.Items[i].SubItems.Add(ResultList[i].requestNum);
                }
                lv_string.EndUpdate();
            }
            catch (Popbill.PopbillException ex)
            {
                MessageBox.Show(this, "응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
        }

        private string SearchReceiptResult(string IN_RECEIPT_NUM)
        {
            //if (txt_ReceiptNum_search_search.Text.Trim() == "")
            if (IN_RECEIPT_NUM.Equals(""))
            {
                MessageBox.Show(this, "접수번호가 비어있습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "접수번호가 비어있습니다.";
            }

            try
            {
                List<MessageResult> ResultList = messageService.GetMessageResult(strCorpNum, IN_RECEIPT_NUM);

                for (int i = 0; i < ResultList.Count; i++)
                {
                    state_code = Convert.ToString(ResultList[i].state);
                    result_code = Convert.ToString(ResultList[i].result);
                }

                //MessageBox.Show("state_code : " + state_code + "\nresult_code : " + result_code);
            }
            catch (Popbill.PopbillException ex)
            {
                MessageBox.Show(this, "응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
            return result_code;
        }


        private void listview_string_init()
        {
            this.Controls.Add(lv_string);
            lv_string.Height = 150;
            lv_string.Dock = DockStyle.Bottom;
            lv_string.Activation = ItemActivation.Standard;
            lv_string.Font = new Font("Tahoma", 11.0F, FontStyle.Regular);
            lv_string.AllowColumnReorder = false;
            lv_string.BorderStyle = BorderStyle.FixedSingle;
            lv_string.UseCompatibleStateImageBehavior = true;

            lv_string.GridLines = true;
            lv_string.FullRowSelect = true;
            lv_string.View = View.Details;
            lv_string.BackColor = Color.White;
            lv_string.ForeColor = Color.Black;
            lv_string.HotTracking = false;
            lv_string.HoverSelection = false;
            lv_string.HideSelection = false;

            lv_string.Columns.Add(string.Empty, string.Empty, 0, HorizontalAlignment.Left, 0);
            for (int i = 0; i < txt_string.Length; i++)
            {
                lv_string.Columns.Add(string.Empty, txt_string[i].Trim(), 200, HorizontalAlignment.Left, 0);
            }
            lv_string.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lv_string.Visible = true;


        }

        private void lbl_ReceiptNum_search_Click(object sender, EventArgs e)
        {
            txt_ReceiptNum_search.Text = "019073014000000001";

            // 019073014000000001
            // 019073014000000031
            // 019073014000000026
        }

        private void lbl_ReceiptNum_search_DoubleClick(object sender, EventArgs e)
        {
            txt_ReceiptNum_search.Text = "019073014000000026";
        }

        private void lbl_ReserveDT_Click(object sender, EventArgs e)
        {
            
        }

        public void SetCutOffType(string IN_CUTOFF_TYPE)
        {
            _EDITED = true;
            _cutOffType = IN_CUTOFF_TYPE;
        }

        public void SetReservationNo(string IN_RESERVATION_NO)
        {
            _reservationNo = IN_RESERVATION_NO;
            ReservationNumberTextBox.Text = _reservationNo;
        }

        public void SetCustomerNumber(string IN_CUSTOMER_NO)
        {
            _customerNo = IN_CUSTOMER_NO;
        }

        public void SetEmailAddress(string IN_EMAIL_ADDR)
        {
            _emailAddr = IN_EMAIL_ADDR;
        }

        public void SetMessageType(string IN_MSG_TYPE)
        {
            _messageType = IN_MSG_TYPE;
        }

        public void SetEmployeeNumber(string IN_EMPLOYEE_NUM)
        {
            _employeeNum = IN_EMPLOYEE_NUM;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txt_ReserveDT_Click(object sender, EventArgs e)
        {
            txt_ReserveDT.Text = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
        }

        private void SMStemplateListDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (SMStemplateListDataGridView.CurrentRow != null)
            {
                // 현재 Row의 인덱스 
                int rowIndex = SMStemplateListDataGridView.CurrentRow.Index;
                // 첫번째 인덱스에서 ↑ 누르는거 방지
                if (e.KeyCode == Keys.Up && rowIndex == 0) return;
                // 마지막 인덱스에서 ↓ 누르는거 방지
                if (e.KeyCode == Keys.Down && SMStemplateListDataGridView.Rows.Count == rowIndex + 1) return;

                if (e.KeyCode.Equals(Keys.Up))
                {
                    CHAR_MSG_NO = SMStemplateListDataGridView.Rows[rowIndex - 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_NO].Value.ToString();
                    CHAR_MSG_DVSN_CD = SMStemplateListDataGridView.Rows[rowIndex - 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_DVSN_CD].Value.ToString();
                    CHAR_MSG_TITL_NM = SMStemplateListDataGridView.Rows[rowIndex - 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_TITLE_NM].Value.ToString();
                    CHAR_MSG_CNTS = SMStemplateListDataGridView.Rows[rowIndex - 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_CNTS].Value.ToString();
                }
                else if (e.KeyCode.Equals(Keys.Down))
                {
                    CHAR_MSG_NO = SMStemplateListDataGridView.Rows[rowIndex + 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_NO].Value.ToString();
                    CHAR_MSG_DVSN_CD = SMStemplateListDataGridView.Rows[rowIndex + 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_DVSN_CD].Value.ToString();
                    CHAR_MSG_TITL_NM = SMStemplateListDataGridView.Rows[rowIndex + 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_TITLE_NM].Value.ToString();
                    CHAR_MSG_CNTS = SMStemplateListDataGridView.Rows[rowIndex + 1].Cells[(int)eSMStemplateListDataGridView.CHAR_MSG_CNTS].Value.ToString();
                }

                SMStemplateDataGridViewChoice();
            }
        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            SMStemplateListDataGridView.Theme = (Guna.UI.WinForms.GunaDataGridViewPresetThemes)gunaComboBox1.SelectedIndex;
        }

        private void btnCloseForm_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void SmsResetBtn_Click(object sender, EventArgs e) {
            rTxt_sms_contents.Text = "";
        }

        private void LmsResetBtn_Click(object sender, EventArgs e) {
            txt_lms_subject.Text = "";
            rTxt_lms_contents.Text = "";
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
