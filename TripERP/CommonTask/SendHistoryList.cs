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
using Popbill.Message;

namespace TripERP.CommonTask
{
    public partial class SendHistoryList : Form
    {
        public enum eSendHistoryListDataGridView
        {
            CHK_YN,
            DOCU_SND_CNMB,
            ACPT_NO,
            RSVT_NO,
            SMS_SND_YN,
            LMS_SND_YN,
            EMAIL_SND_YN,
            RSVT_PRGS_DVSN_CD,
            DOCU_SND_STTS,
            SND_DTM,
            CELL_PHNE_NO,
            EMAL_ADDR,
            FAX_NO,
            EMPL_NM,
            CUST_NM,
            DOCU_SND_STTS_CD
        };

        private MessageService messageService;

        private string strCorpNum = "2088700871";
        private string strUserId = "hanih77";           // 팝빌회원ID
        private string LinkID = "GBRIDGE";              // 링크아이디
        private string SecretKey = "WWVXq4omBaaw9pPoHn+lUV/a94WAIJupAURLBuKYaoc=";  //비밀키
        private string result_code = "";
        private string log_sms = "";
        private string log_lms = "";

        bool CHK_YN = false;
        private string DOCU_SND_CNMB {get; set;}
        private string ACPT_NO {get; set;}
        private string RSVT_NO {get; set;}
        private string SMS_SND_YN {get; set;}
        private string LMS_SND_YN {get; set;}
        private string EMAIL_SND_YN {get; set;}
        private string RSVT_PRGS_DVSN_CD {get; set;}
        private string SND_DTM {get; set;}
        private string CELL_PHNE_NO {get; set;}
        private string EMAL_ADDR {get; set;}
        private string FAX_NO {get; set;}
        private string DOCU_SND_STTS {get; set;}
        private string DOCU_SND_STTS_CD {get; set;}
        private string DOCU_SND_STTS_DVSN {get; set;}
        private string EMPL_NM {get; set;}
        private string CUST_NM {get; set;}

        // 발송매체코드 배열(DataGridView의 발송매체코드 1 --> SMS)
        public string[] ArraySendMediaType;

        // 예약진행상태코드 배열(DataGridView의 예약진행상태코드 1 --> 등록완료)
        public string[] ArrayReservationProgressType;


        public SendHistoryList()
        {
            InitializeComponent();
            // 문자 서비스 모듈 초기화
            messageService = new MessageService(LinkID, SecretKey);
            // 연동환경 설정값, true(개발용), false(상업용)
            messageService.IsTest = true;
        }

        private void SendHistoryList_Load(object sender, EventArgs e)
        {
            InitControls();
            searchSendHistoryList();                    // form load 시 내용 출력
        }

        // 초기화
        private void InitControls()
        {
            // 폼 입력필드 초기화
            ResetInputFormField();

            // 콤보박스 초기화
            ResetComboBox();

            // DataGridView 스타일 초기화
            InitDataGridView();
        }

        // 입력폼 초기화
        private void ResetInputFormField()
        {
            SendHistoryCNMBTextbox.Text = "";
            ReservationNoTextbox.Text = "";
            AcceptNumberTextBox.Text = "";
            SMSYNTextBox.Text = "";
            LMSYNTextBox.Text = "";
            EMAILYNTextBox.Text = "";
            CustNoTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            FaxNoTextBox.Text = "";
            BankRPTVEmailTextBox.Text = "";

            SendDateTimePicker.Value = DateTime.Now;

            StartSendDateTimePicker.Value = DateTime.Now.AddDays(-7);
        }

        // 콤보박스 초기화
        private void ResetComboBox()
        {
            // 필드 초기화
            SearchSendMediaComboBox.Items.Clear();
            SearchReservationProgressStateComboBox.Items.Clear();

            ReservationProgressStateComboBox.Items.Clear();
            BankRPTVEmailComboBox.Items.Clear();
            ReservationProgComboBox.Items.Clear();

            SearchSendMediaComboBox.Items.Add(new ComboBoxItem("전체", ""));
            SearchReservationProgressStateComboBox.Items.Add(new ComboBoxItem("전체", ""));

            // 발송매체 콤보박스 아이템 로드
            LoadSendMediaComboBoxItmes();
            // 예약진행상태 콤보박스 아이템 로드
            LoadReservationProgressStateItems();
            // 문서발송상태 콤보박스 아이템 로드
            LoadDocumentSendStateItems();
        }

        // 데이터 그리드뷰 초기화
        private void InitDataGridView()
        {
            DataGridView dataGridView1 = SendHistoryDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            //dataGridView1.RowHeadersVisible = false;
            dataGridView1.DoubleBuffered(true);
        }

        // 발송이력조회 버튼 클릭
        private void SearchSendHistory_Click(object sender, EventArgs e)
        {
            searchSendHistoryList();
        }

        private void searchSendHistoryList()
        {
            SendHistoryDataGridView.Rows.Clear();

            string SendMediaType = Utils.GetSelectedComboBoxItemText(SearchSendMediaComboBox);
            string ReservationProgressType = Utils.GetSelectedComboBoxItemText(SearchReservationProgressStateComboBox);

            string START_SEND_DT = StartSendDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");
            string END_SEND_DT = EndSendDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");

            string query = string.Format("CALL SelectSendHistoryList ('{0}', '{1}', '{2}', '{3}')",
                                           SendMediaType, ReservationProgressType, START_SEND_DT, END_SEND_DT);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("문자발송내역을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                DOCU_SND_CNMB = dataRow["DOCU_SND_CNMB"].ToString();
                ACPT_NO = dataRow["ACPT_NO"].ToString();
                RSVT_NO = dataRow["RSVT_NO"].ToString();
                SMS_SND_YN = dataRow["SMS_SND_YN"].ToString();
                LMS_SND_YN = dataRow["LMS_SND_YN"].ToString();
                EMAIL_SND_YN = dataRow["EMAL_SND_YN"].ToString();
                RSVT_PRGS_DVSN_CD = dataRow["RSVT_PRGS_DVSN_CD"].ToString();
                SND_DTM = dataRow["SND_DTM"].ToString();
                CELL_PHNE_NO =dataRow["CELL_PHNE_NO"].ToString();
                EMAL_ADDR = dataRow["EMAL_ADDR"].ToString();
                FAX_NO = dataRow["FAX_NO"].ToString();
                DOCU_SND_STTS_CD = dataRow["DOCU_SND_STTS_CD"].ToString();
                EMPL_NM = dataRow["EMPL_NM"].ToString();
                CUST_NM = dataRow["CUST_NM"].ToString();

                if (SMS_SND_YN.Equals("Y"))
                    SMS_SND_YN = "●";
                else
                    SMS_SND_YN = "";
                if (LMS_SND_YN.Equals("Y"))
                    LMS_SND_YN = "●";
                else
                    LMS_SND_YN = "";
                if (EMAIL_SND_YN.Equals("Y"))
                    EMAIL_SND_YN = "●";
                else
                    EMAIL_SND_YN = "";

                string DOCU_SND_STTS = "";

                if (DOCU_SND_STTS_CD.Substring(0, 1).Equals("1"))
                    DOCU_SND_STTS = "발송 성공";
                else
                {
                    DOCU_SND_STTS = "발송 실패";
                    
                }

                SendHistoryDataGridView.Rows.Add(CHK_YN,
                                                                DOCU_SND_CNMB,
                                                                ACPT_NO,
                                                                RSVT_NO,
                                                                SMS_SND_YN,
                                                                LMS_SND_YN,
                                                                EMAIL_SND_YN,
                                                                RSVT_PRGS_DVSN_CD,
                                                                DOCU_SND_STTS,
                                                                SND_DTM,
                                                                CELL_PHNE_NO,
                                                                EMAL_ADDR,
                                                                FAX_NO,
                                                                EMPL_NM,
                                                                CUST_NM,
                                                                DOCU_SND_STTS_CD
                                                 );
            }
            SendHistoryDataGridView.ClearSelection();
        }

        // 그리드 방향키 컨트롤
        private void SendHistoryDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스 
            int rowIndex = SendHistoryDataGridView.CurrentRow.Index;
            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0) return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && SendHistoryDataGridView.Rows.Count == rowIndex + 1) return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && SendHistoryDataGridView.Rows.Count == rowIndex + 1) return;

            if (e.KeyCode.Equals(Keys.Up))
            {
                DOCU_SND_CNMB = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_CNMB].Value.ToString();
                ACPT_NO = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.ACPT_NO].Value.ToString();
                RSVT_NO = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.RSVT_NO].Value.ToString();
                SMS_SND_YN = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.SMS_SND_YN].Value.ToString();
                LMS_SND_YN = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.LMS_SND_YN].Value.ToString();
                EMAIL_SND_YN = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.EMAIL_SND_YN].Value.ToString();
                RSVT_PRGS_DVSN_CD = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.RSVT_PRGS_DVSN_CD].Value.ToString();
                DOCU_SND_STTS = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_STTS].Value.ToString();
                SND_DTM = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.SND_DTM].Value.ToString();
                CELL_PHNE_NO = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.CELL_PHNE_NO].Value.ToString();
                FAX_NO = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.FAX_NO].Value.ToString();
                EMAL_ADDR = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.EMAL_ADDR].Value.ToString();
                EMPL_NM = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.EMPL_NM].Value.ToString();
                CUST_NM = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.CUST_NM].Value.ToString();
                DOCU_SND_STTS_CD = SendHistoryDataGridView.Rows[rowIndex - 1].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_STTS_CD].Value.ToString();
            }
            else if (e.KeyCode.Equals(Keys.Down))
            {
                DOCU_SND_CNMB = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_CNMB].Value.ToString();
                ACPT_NO = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.ACPT_NO].Value.ToString();
                RSVT_NO = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.RSVT_NO].Value.ToString();
                SMS_SND_YN = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.SMS_SND_YN].Value.ToString();
                LMS_SND_YN = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.LMS_SND_YN].Value.ToString();
                EMAIL_SND_YN = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.EMAIL_SND_YN].Value.ToString();
                RSVT_PRGS_DVSN_CD = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.RSVT_PRGS_DVSN_CD].Value.ToString();
                DOCU_SND_STTS = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_STTS].Value.ToString();
                SND_DTM = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.SND_DTM].Value.ToString();
                CELL_PHNE_NO = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.CELL_PHNE_NO].Value.ToString();
                FAX_NO = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.FAX_NO].Value.ToString();
                EMAL_ADDR = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.EMAL_ADDR].Value.ToString();
                EMPL_NM = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.EMPL_NM].Value.ToString();
                CUST_NM = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.CUST_NM].Value.ToString();
                DOCU_SND_STTS_CD = SendHistoryDataGridView.Rows[rowIndex + 1].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_STTS_CD].Value.ToString();
            }
            CheckO();
            SendHistoryDataGridViewChoice();
        }

        // 그리드 행 클릭
        private void SendHistoryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SendHistoryDataGridView.SelectedRows.Count == 0)
                return;

            DOCU_SND_CNMB = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_CNMB].Value.ToString();
            ACPT_NO = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.ACPT_NO].Value.ToString();
            RSVT_NO = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.RSVT_NO].Value.ToString();
            SMS_SND_YN = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.SMS_SND_YN].Value.ToString();
            LMS_SND_YN = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.LMS_SND_YN].Value.ToString();
            EMAIL_SND_YN = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.EMAIL_SND_YN].Value.ToString();
            RSVT_PRGS_DVSN_CD = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.RSVT_PRGS_DVSN_CD].Value.ToString();
            DOCU_SND_STTS = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_STTS].Value.ToString();
            SND_DTM = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.SND_DTM].Value.ToString();
            CELL_PHNE_NO = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.CELL_PHNE_NO].Value.ToString();
            FAX_NO = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.FAX_NO].Value.ToString();
            EMAL_ADDR = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.EMAL_ADDR].Value.ToString();
            EMPL_NM = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.EMPL_NM].Value.ToString();
            CUST_NM = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.CUST_NM].Value.ToString();
            DOCU_SND_STTS_CD = SendHistoryDataGridView.SelectedRows[0].Cells[(int)eSendHistoryListDataGridView.DOCU_SND_STTS_CD].Value.ToString();

            CheckO();

            SendHistoryDataGridViewChoice();

            try
            {
                if (SendHistoryDataGridView.CurrentCell != null)
                {
                    if (SendHistoryDataGridView[0, SendHistoryDataGridView.CurrentCell.RowIndex].Value.Equals(true))
                        SendHistoryDataGridView[0, SendHistoryDataGridView.CurrentCell.RowIndex].Value = false;
                    else
                        SendHistoryDataGridView[0, SendHistoryDataGridView.CurrentCell.RowIndex].Value = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendHistoryDataGridViewChoice()
        {
            ReservationNoTextbox.Text = RSVT_NO;
            ReservationProgComboBox.Text = RSVT_PRGS_DVSN_CD;

            if (RSVT_PRGS_DVSN_CD.Equals(""))
            {
                ReservationProgComboBox.Text = "";
            }
            else
            {
                Utils.SelectComboBoxItemByText(ReservationProgComboBox, RSVT_PRGS_DVSN_CD);
            }

            
            AcceptNumberTextBox.Text = ACPT_NO;
            SMSYNTextBox.Text = SMS_SND_YN;
            LMSYNTextBox.Text = LMS_SND_YN;
            EMAILYNTextBox.Text = EMAIL_SND_YN;
            SendDateTimePicker.Text = SND_DTM;
            CustNoTextBox.Text = CUST_NM;
            PhoneNumberTextBox.Text = CELL_PHNE_NO;
            FaxNoTextBox.Text = FAX_NO;
            
            SendHistoryCNMBTextbox.Text = DOCU_SND_CNMB;

            if (DOCU_SND_STTS_CD.Substring(0, 1).Equals("1"))
                Utils.SelectComboBoxItemByText(ReservationProgressStateComboBox, "발송 성공");
            else
                Utils.SelectComboBoxItemByText(ReservationProgressStateComboBox, "발송 실패");

            if (EMAL_ADDR != "")
            {
                string[] split_email = EMAL_ADDR.Split('@');
                BankRPTVEmailTextBox.Text = split_email[0];
                BankRPTVEmailComboBox.Text = split_email[1];
            }
        }

        // 재전송
        private void ReSendButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 재전송하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            for (int i = 0; i < SendHistoryDataGridView.RowCount; i++)
            {
                if (SendHistoryDataGridView[0, i].Value.Equals(true))
                {
                    string ACPT_NO = SendHistoryDataGridView[2, i].Value.ToString();
                    MessageBox.Show(SendHistoryDataGridView[10, i].Value.ToString());
                    if (ACPT_NO == "")
                        return;

                    List<MessageResult> ResultList = messageService.GetMessageResult(strCorpNum, ACPT_NO);

                    for (int j = 0; j < ResultList.Count; j++)
                    {
                        String senderNum = ResultList[j].senderName;
                        String receiver = ResultList[j].receiveNum;
                        MessageBox.Show(receiver);
                        String receiverName = ResultList[j].receiveName;
                        String subject = ResultList[j].subject;
                        String content = ResultList[j].content;
                        String requestNum = "";
                        Boolean adsYN = false;

                        try
                        {
                            string receiptNum = messageService.SendSMS(strCorpNum, senderNum, receiver,
                                receiverName, content, getReserveDT(), strUserId, requestNum, adsYN);

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

                    // string msg = SendHistoryDataGridView[].Value.ToString(); // columIndex, rowIndex
                    // MessageBox.Show(msg);
                    //count = count + 1;
                }

                //    if (SearchEmployeeNamecomboBox.Equals(null))
                //    {
                //        MessageBox.Show(string.Format("임직원을 선택하셔야합니다."));
                //        return;
                //    }
                //    else
                //    {
                //        EMPL_NO = Utils.GetSelectedComboBoxItemValue(SearchEmployeeNamecomboBox);          // 사번
                //    }

                //    string query = "";

                //    query = string.Format("CALL InsertEmployeeMenuInfoItem ('{0}', '{1}', '{2}')", SCRN_CD, EMPL_NO, FRST_RGTR_ID);
                //    retVal = DbHelper.ExecuteNonQuery(query);
                //}

                //MessageBox.Show(string.Format("총 {0} 건 저장", count));
                //SearchEmployeeMenuCodeList();                            // 등록 후 그리드를 최신상태로 Refresh

                //return;
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
                    result_code = Convert.ToString(ResultList[i].result);
                }
            }
            catch (Popbill.PopbillException ex)
            {
                MessageBox.Show(this, "응답코드(code) : " + ex.code.ToString() + "\r\n" +
                                "응답메시지(message) : " + ex.Message, "문자 전송상태 확인");
            }
            return result_code;
        }

        // 발송이력 저장
        private void SaveDocumentHistory(string IN_DOCU_CD)
        {
            //// 문서발송일련번호 (쿼리내에서 max 하고 있음)
            //string RSVT_NO = ReservationNumberTextBox.Text.Trim();          // 예약번호
            //string ACPT_NO = txt_ReceiptNum_search.Text.Trim();                        // 접수번호
            //string SMS_SND_YN = "N";                                        // SMS전송여부
            //string LMS_SND_YN = "N";                                        // LMS전송여부
            //string EMAL_SND_YN = "N";                                       // 이메일발송여부
            //string RSVT_PRGS_DVSN_CD = "";                                  // 예약진행구분코드 
            //string CUST_NO = _customerNo;                                   // 고객번호
            //string SND_DTM = "";                                            // 전송일자
            //string CELL_PHNE_NO = rxCellPhoneNoTextBox.Text.Trim();         // 휴대전화번호
            //string FAX_NO = "";                                             // 팩스번호
            //string EMAL_ADDR = _emailAddr;                                  // 이메일주소
            //string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;
            //string DOCU_SND_STTS_CD = "";                          // 문서발송상태코드(팝빌)

            //if (txt_ReserveDT.Text.Equals(""))
            //    SND_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   // 발송일시
            //else
            //    SND_DTM = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   // 일단 고정

            //// SMS, LMS 분류
            //switch (IN_DOCU_CD)
            //{
            //    case "1":   // SMS
            //        SMS_SND_YN = "Y";
            //        break;
            //    case "2":   // LMS
            //        LMS_SND_YN = "Y";
            //        break;
            //    case "3":   // EMAIL
            //        EMAL_SND_YN = "Y";
            //        break;
            //}

            //// 예약진행구분코드
            //if (_cutOffType == "LST_CHCK_STTS_CD")
            //    RSVT_PRGS_DVSN_CD = "명단확인";
            //else if (_cutOffType == "ARGM_CHCK_STTS_CD")
            //    RSVT_PRGS_DVSN_CD = "수배확인";
            //else if (_cutOffType == "PSPT_CHCK_STTS_CD")
            //    RSVT_PRGS_DVSN_CD = "여권확인";
            //else if (_cutOffType == "ISRC_CHCK_STTS_CD")
            //    RSVT_PRGS_DVSN_CD = "항공확인";
            //else if (_cutOffType == "AVAT_CHCK_STTS_CD")
            //    RSVT_PRGS_DVSN_CD = "보험확인";
            //else if (_cutOffType == "PRSN_CHCK_STTS_CD")
            //    RSVT_PRGS_DVSN_CD = "개인확인";

            //// 문서발송상태코드 
            //if (result_code.Equals("100")) // 발송 성공
            //    DOCU_SND_STTS_CD = "1";
            //else                           // 발송 실패
            //{
            //    if (result_code.Equals(""))
            //        DOCU_SND_STTS_CD = "0"; // 미분류 오류
            //    else if (result_code.Substring(0, 1).Equals("2"))
            //        DOCU_SND_STTS_CD = "2"; // 메세지 형식 오류 
            //    else if (result_code.Substring(0, 1).Equals("3"))
            //        DOCU_SND_STTS_CD = "3"; // 발신 오류
            //    else if (result_code.Substring(0, 1).Equals("4"))
            //        DOCU_SND_STTS_CD = "4"; // 수신 및 착신 오류
            //    else if (result_code.Substring(0, 1).Equals("5"))
            //        DOCU_SND_STTS_CD = "5"; // 이동통신사 오류
            //    else if (result_code.Substring(0, 1).Equals("8"))
            //        DOCU_SND_STTS_CD = "8"; // 수신 거부
            //    else if (result_code.Substring(0, 1).Equals("9"))
            //        DOCU_SND_STTS_CD = "9"; // 기타 오류

            //}

            //string query = string.Format("CALL InsertDocuSndItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
            //    RSVT_NO,
            //    ACPT_NO,
            //    SMS_SND_YN,
            //    LMS_SND_YN,
            //    EMAL_SND_YN,
            //    RSVT_PRGS_DVSN_CD,
            //    CUST_NO,
            //    SND_DTM,
            //    CELL_PHNE_NO,
            //    FAX_NO,
            //    EMAL_ADDR,
            //    FRST_RGTR_ID,
            //    DOCU_SND_STTS_CD
            //);

            //long retVal = DbHelper.ExecuteScalar(query);
            //if (retVal == -1)
            //    MessageBox.Show("발송이력을 저장할 수 없습니다.");
            //else
            //    MessageBox.Show("발송이력을 저장했습니다.");
        }

        private DateTime? getReserveDT()
        {
            DateTime? reserveDT = null;
            //if (String.IsNullOrEmpty(txt_ReserveDT.Text) == false)
            //{
            //    reserveDT = DateTime.ParseExact(txt_ReserveDT.Text, "yyyyMMddHHmmss",
            //        System.Globalization.CultureInfo.InvariantCulture);
            //}

            return reserveDT;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void LoadDocumentTypeComboBoxItems()
        //{
        //    string query = "CALL SelectCommonCodeList ('DOCU_CD')";
        //    DataSet dataSet = DbHelper.SelectQuery(query);
        //    if (dataSet == null || dataSet.Tables.Count == 0)
        //    {
        //        MessageBox.Show("문서코드를 목록을 가져올 수 없습니다.");
        //        return;
        //    }

        //    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        //    {
        //        string CD_VLID_VAL_CODE = dataRow["CD_VLID_VAL"].ToString();
        //        string CD_VLID_VAL_VALUE = dataRow["CD_VLID_VAL_DESC"].ToString();

        //        ComboBoxItem DOCU_ITEM = new ComboBoxItem(CD_VLID_VAL_VALUE, CD_VLID_VAL_CODE);

        //        SearchDocumentTypeComboBox.Items.Add(DOCU_ITEM);
        //    }
        //}

        private void LoadSendMediaComboBoxItmes()
        {
            //string query = "CALL SelectCommonCodeList ('SND_MDIA_CD')";
            //DataSet dataSet = DbHelper.SelectQuery(query);
            //if (dataSet == null || dataSet.Tables.Count == 0)
            //{
            //    MessageBox.Show("발송매체 목록을 가져올 수 없습니다.");
            //    return;
            //}

            //int cnt = dataSet.Tables[0].Rows.Count;
            //foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            //{
            //    string CD_VLID_VAL_CODE = dataRow["CD_VLID_VAL"].ToString();
            //    string CD_VLID_VAL_VALUE = dataRow["CD_VLID_VAL_DESC"].ToString();

            //    ComboBoxItem SEND_MEDIA_ITEM = new ComboBoxItem(CD_VLID_VAL_VALUE, CD_VLID_VAL_CODE);

            //    SearchSendMediaComboBox.Items.Add(SEND_MEDIA_ITEM);
            //}

            //if (SearchSendMediaComboBox.Items.Count > 0)
            //    SearchSendMediaComboBox.SelectedIndex = 0;

            SearchSendMediaComboBox.Items.Add(new ComboBoxItem("SMS", 0));
            SearchSendMediaComboBox.Items.Add(new ComboBoxItem("LMS", 1));
            SearchSendMediaComboBox.Items.Add(new ComboBoxItem("이메일", 2));

            if (SearchSendMediaComboBox.Items.Count > 0)
                SearchSendMediaComboBox.SelectedIndex = 0;
        }

        private void LoadReservationProgressStateItems()
        {
            string query = "CALL SelectCommonCodeList ('RSVT_PRGS_DVSN_CD')";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약진행구분코드 목록을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CD_VLID_VAL_CODE = dataRow["CD_VLID_VAL"].ToString();
                string CD_VLID_VAL_VALUE = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem RSVR_PRGS_ITEM = new ComboBoxItem(CD_VLID_VAL_VALUE, CD_VLID_VAL_CODE);

                ReservationProgComboBox.Items.Add(RSVR_PRGS_ITEM);
            }

            if (ReservationProgComboBox.Items.Count > 0)
                ReservationProgComboBox.Text = "";
        }

        private void LoadDocumentSendStateItems()
        {
            SearchReservationProgressStateComboBox.Items.Add(new ComboBoxItem("발송 성공", 0));
            SearchReservationProgressStateComboBox.Items.Add(new ComboBoxItem("발송 실패", 1));

            ReservationProgressStateComboBox.Items.Add(new ComboBoxItem("발송 성공", 0));
            ReservationProgressStateComboBox.Items.Add(new ComboBoxItem("발송 실패", 0));

            if (SearchReservationProgressStateComboBox.Items.Count > 0)
                SearchReservationProgressStateComboBox.SelectedIndex = 0;

            if (ReservationProgressStateComboBox.Items.Count > 0)
                ReservationProgressStateComboBox.Text = "";
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

            SendHistoryDataGridView.SelectAll();

            if (Directory.Exists(fileDirPath))
            {
                //if (true == ExcelHelper.ExportExcel(filePath, reservationDataGridView))
                if (ExcelHelper.gridToExcel(filePath, SendHistoryDataGridView) == true)
                    MessageBox.Show(string.Format("{0}\r\n파일을 저장했습니다.", filePath));
                else
                    MessageBox.Show("파일을 저장 할 수 없습니다.");
            }
            else
            {
                MessageBox.Show("잘못된 저장 경로입니다.");
            }

            this.Cursor = Cursors.Default;
        }

        private void CheckO()
        {
            if (SMS_SND_YN.Equals("●"))
                SMS_SND_YN = "Y";
            else
                SMS_SND_YN = "N";
            if (LMS_SND_YN.Equals("●"))
                LMS_SND_YN = "Y";
            else
                LMS_SND_YN = "N";
            if (EMAIL_SND_YN.Equals("●"))
                EMAIL_SND_YN = "Y";
            else
                EMAIL_SND_YN = "N";
        }

        //// 목록 전체선택 체크박스 생성
        //private void SendHistoryDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.ColumnIndex == 0 && e.RowIndex == -1)
        //    {
        //        e.PaintBackground(e.ClipBounds, false);

        //        Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

        //        int nChkBoxWidth = 15;
        //        int nChkBoxHeight = 15;
        //        int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
        //        int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

        //        pt.X += offsetx;
        //        pt.Y += offsety;

        //        CheckBox cb = new CheckBox();
        //        cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
        //        cb.Location = pt;
        //        cb.CheckedChanged += new EventHandler(gvSheetListCheckBox_CheckedChanged);

        //        ((DataGridView)sender).Controls.Add(cb);

        //        e.Handled = true;
        //    }
        //}

        //// 목록 Checkbox 전체선택
        //private void gvSheetListCheckBox_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox cb = (CheckBox)sender;
        //    DataGridView gridView = (DataGridView)cb.Parent;
        //    int rowCount = 0;
        //    foreach (DataGridViewRow r in gridView.Rows)
        //    {
        //        if (r.Cells[0].OwningColumn.Name.Equals("colCheck"))
        //        {
        //            r.Cells["colCheck"].Value = cb.Checked;
        //            rowCount = rowCount + 1;
        //        }

        //        else if (r.Cells[0].OwningColumn.Name.Equals("colCheck2"))
        //        {
        //            r.Cells["colCheck2"].Value = cb.Checked;
        //            rowCount = rowCount + 1;
        //        }

        //    }
        //}

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
    }
}
