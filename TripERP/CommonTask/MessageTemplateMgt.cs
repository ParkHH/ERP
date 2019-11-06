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
    public partial class MessageTemplateMgt : Form
    {
        enum eMessageTemplateDataGridView { CHAR_MSG_NO, CHAR_MSG_DVSN_CD, CHAR_MSG_DVSN_NM, CHAR_MSG_TITLE_NM, CHAR_MSG_CNTS, FRST_RGST_DTM, FRST_RGTR_ID, FINL_MDFC_DTM, FINL_MDFR_ID };

        string CHAR_MSG_NO = "";
        string CHAR_MSG_DVSN_CD = "";
        string CHAR_MSG_DVSN_NM = "";
        string CHAR_MSG_TITL_NM = "";
        string CHAR_MSG_CNTS = "";

        public MessageTemplateMgt()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void MessageTemplateMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            searchMessageTemplateList();
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

            MessageNo.Text = "";
            MessageTitleTextBox.Text = "";
            MessageContentTextBox.Text = "";

            MessageTemplateDataGridView.Rows.Clear();
        }

        private void ResetComboBox()
        {
            // 문자 템플릿 타입 콤보 설정
            SelectTemplateTypeComboBox.Items.Clear();
            SearchMessageTitleComboBox.Items.Clear();
            MessageDivisionNoComboBox.Items.Clear();

            List<CommonCodeItem> list = Global.GetCommonCodeList("CHAR_MSG_DVSN_CD");

            SelectTemplateTypeComboBox.Items.Add(new ComboBoxItem("전체", ""));
            SearchMessageTitleComboBox.Items.Add(new ComboBoxItem("전체", ""));
            for (int li = 0; li < list.Count; li++)
            {
                string value = list[li].Value.ToString();
                string desc = list[li].Desc;

                ComboBoxItem item = new ComboBoxItem(desc, value);

                SelectTemplateTypeComboBox.Items.Add(item);
                MessageDivisionNoComboBox.Items.Add(item);
            }

            if (SelectTemplateTypeComboBox.Items.Count > 0)
            {
                SelectTemplateTypeComboBox.SelectedIndex = 0;
            }

            if (MessageDivisionNoComboBox.Items.Count > 0)
            {
                MessageDivisionNoComboBox.Text = "";
            }

            string query = "";
            DataSet dataSet;
            query = "CALL SelectAllMessageTitle ()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("템플릿 메시지 제목을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CHAR_MSG_DVSN_CD = dataRow["CHAR_MSG_DVSN_CD"].ToString();
                string CHAR_MSG_TITL_NM = dataRow["CHAR_MSG_TITL_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CHAR_MSG_TITL_NM, CHAR_MSG_DVSN_CD);
                SearchMessageTitleComboBox.Items.Add(item);
            }

            if (SearchMessageTitleComboBox.Items.Count > 0)
                SearchMessageTitleComboBox.SelectedIndex = 0;
        }

        // 데이터 그리드뷰 초기화
        private void InitDataGridView()
        {
        }

        // 목록조회버튼 클릭
        private void searchMessageTemplateButton_Click(object sender, EventArgs e)
        {
            searchMessageTemplateList();
        }

        // 문자메시지 템플릿 목록 조회
        private void searchMessageTemplateList()
        {
            MessageTemplateDataGridView.Rows.Clear();

            CHAR_MSG_DVSN_CD = Utils.GetSelectedComboBoxItemValue(SelectTemplateTypeComboBox);     // 문자템플릿구분(SMS, LMS, 이메일)

            CHAR_MSG_TITL_NM = SearchMessageTitleComboBox.Text;                                    // 문자메시지제목명
            CHAR_MSG_CNTS = SearchMessageContentTextBox.Text.Trim();                               // 문자메시지내용

            if (CHAR_MSG_TITL_NM.Equals("전체"))
                CHAR_MSG_TITL_NM = "";
            if (CHAR_MSG_CNTS.Equals("전체"))
                CHAR_MSG_CNTS = "";

            string query = string.Format("CALL SelectCharTemplateList ('{0}', '{1}', '{2}')", CHAR_MSG_DVSN_CD, CHAR_MSG_TITL_NM, CHAR_MSG_CNTS);
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

                MessageTemplateDataGridView.Rows.Add(CHAR_MSG_NO, CHAR_MSG_DVSN_CD, CHAR_MSG_DVSN_NM, CHAR_MSG_TITL_NM, CHAR_MSG_CNTS);
            }

            MessageTemplateDataGridView.ClearSelection();

        }

        // 그리드 행 클릭
        private void MessageTemplateDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageTemplateDataGridView.SelectedRows.Count == 0)
                return;

            CHAR_MSG_NO = MessageTemplateDataGridView.SelectedRows[0].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_NO].Value.ToString();
            CHAR_MSG_DVSN_CD = MessageTemplateDataGridView.SelectedRows[0].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_DVSN_CD].Value.ToString();
            CHAR_MSG_DVSN_NM = MessageTemplateDataGridView.SelectedRows[0].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_DVSN_NM].Value.ToString();
            CHAR_MSG_TITL_NM = MessageTemplateDataGridView.SelectedRows[0].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_TITLE_NM].Value.ToString();
            CHAR_MSG_CNTS = MessageTemplateDataGridView.SelectedRows[0].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_CNTS].Value.ToString();

            MessageTemplateDataGridViewChoice();
        }

        private void MessageTemplateDataGridViewChoice()
        {
            MessageNo.Text = CHAR_MSG_NO;

            Utils.SelectComboBoxItemByValue(MessageDivisionNoComboBox, CHAR_MSG_DVSN_CD);  // 문자메시지구분코드 콤보박스

            MessageTitleTextBox.Text = CHAR_MSG_TITL_NM;
            MessageContentTextBox.Text = CHAR_MSG_CNTS;
        }

        // 초기화 버튼 클릭
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
            ResetComboBox();
            searchMessageTemplateList();
        }

        // 저장 버튼 클릭
        private void saveButton_Click(object sender, EventArgs e)
        {
            Boolean updateFlag = false;

            string CHAR_MSG_NO = MessageNo.Text.Trim();                                               // 문자메시지번호
            string CHAR_MSG_DVSN_CD = Utils.GetSelectedComboBoxItemValue(MessageDivisionNoComboBox);  // 문자메시지구분번호
            string CHAR_MSG_TITL_NM = MessageTitleTextBox.Text.Trim();                                // 문자메시지제목
            string CHAR_MSG_CNTS = MessageContentTextBox.Text.Trim();                                 // 문자메시지내용
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                           // 최초등록자ID
            string FINL_RGTR_ID = Global.loginInfo.ACNT_ID;                                           // 최종변경자ID

            string query = "";
            int _msgNo = 0;

            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;

            // 문자번호가 입력되지 않으면 MAX+1로 채번하고 Insert처리
            if (CHAR_MSG_NO == "" || CHAR_MSG_NO == null)
            {
                query = "CALL SelectMaxMessageTemplateNo";
                DataSet dataSet = DbHelper.SelectQuery(query);
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBox.Show("문자메시지번호를 가져올 수 없습니다.");
                    return;
                }

                DataRow dataRow = dataSet.Tables[0].Rows[0];
                CHAR_MSG_NO = dataRow["CHAR_MSG_NO"].ToString(); // 문자메시지번호

                // 문자번호가 없으면 1로 설정하고 있으면 +1 증가
                if (CHAR_MSG_NO == "0")
                {
                    CHAR_MSG_NO = "1";
                }
                else
                {
                    _msgNo = Convert.ToInt16(CHAR_MSG_NO);
                    _msgNo = _msgNo + 1;
                    CHAR_MSG_NO = Convert.ToString(_msgNo);
                }

                query = string.Format("CALL InsertMessageTemplate ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    CHAR_MSG_NO, CHAR_MSG_DVSN_CD, CHAR_MSG_TITL_NM, CHAR_MSG_CNTS, FRST_RGTR_ID);

            }
            else
            {
                _msgNo = Convert.ToInt16(CHAR_MSG_NO);
                CHAR_MSG_NO = Convert.ToString(_msgNo);

                query = string.Format("CALL UpdateMessageTemplate ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    CHAR_MSG_NO, CHAR_MSG_DVSN_CD, CHAR_MSG_TITL_NM, CHAR_MSG_CNTS, FINL_RGTR_ID);

                updateFlag = true;
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("문자메시지 템플릿을 저장할 수 없습니다.");
                return;
            }
            else
            {
                // 저장 후 입력폼 초기화
                ResetInputFormField();

                // 등록 후 그리드를 최신상태로 Refresh
                searchMessageTemplateList();

                if (updateFlag)
                {
                    MessageBox.Show("문자메시지 템플릿을 수정했습니다.");
                }
                else
                {
                    
                    MessageBox.Show("문자메시지 템플릿을 저장했습니다.");
                }
                
            }
        }

        // 삭제 버튼 클릭
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string CHAR_MSG_NO = MessageNo.Text.Trim();

            if (CHAR_MSG_NO == "")
            {
                MessageBox.Show("문자메시지번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요.");
                return;
            }

            string query = string.Format("CALL DeleteMessageTemplate ('{0}')", CHAR_MSG_NO);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("문자메시지템플릿을 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                MessageBox.Show("문자메시지템플릿을 삭제했습니다.");
                searchMessageTemplateList();
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
            ResetComboBox();
            searchMessageTemplateList();
        }

        // 닫기 버튼 클릭
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string CHAR_MSG_DVSN_CD = Utils.GetSelectedComboBoxItemValue(MessageDivisionNoComboBox);
            string CHAR_MSG_TITL_NM = MessageTitleTextBox.Text.Trim();
            string CHAR_MSG_CNTS = MessageContentTextBox.Text.Trim();

            if (CHAR_MSG_DVSN_CD == "")
            {
                MessageBox.Show("구분번호는 필수 입력항목입니다.");
                MessageDivisionNoComboBox.Focus();
                return false;
            }

            if (CHAR_MSG_TITL_NM == "")
            {
                MessageBox.Show("문자메시지명은 필수 입력항목입니다.");
                MessageTitleTextBox.Focus();
                return false;
            }

            if (CHAR_MSG_CNTS == "")
            {
                MessageBox.Show("문자메시지내용은 필수 입력항목입니다.");
                MessageContentTextBox.Focus();
                return false;
            }

            return true;
        }
        
        // SMS, LMS, 이메일 등 템플릿 타입에 따라 문자 템플릿 내용의 텍스트박스 크기를 조정
        private void setContentsTextBoxByType()
        {
            string templateType = Utils.GetSelectedComboBoxItemValue(MessageDivisionNoComboBox);

            //MessageContentTextBox.Location = 147;

            if (templateType.Equals("1"))
            {
                MessageContentTextBox.Width = 372;
                MessageContentTextBox.Height = 210;
                MessageContentTextBox.MaxLength = 45;
            }
            else if (templateType.Equals("2"))
            {
                MessageContentTextBox.Width = 556;
                MessageContentTextBox.Height = 210;
                MessageContentTextBox.MaxLength = 31762;
            }
            else if (templateType.Equals("3"))
            {
                MessageContentTextBox.Width = 556;
                MessageContentTextBox.Height = 240;
                MessageContentTextBox.MaxLength = 31762;
            }
        }

        private void MessageTemplateDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (MessageTemplateDataGridView.CurrentRow != null)
            {
                // 현재 Row의 인덱스 
                int rowIndex = MessageTemplateDataGridView.CurrentRow.Index;
                // 첫번째 인덱스에서 ↑ 누르는거 방지
                if (e.KeyCode == Keys.Up && rowIndex == 0) return;
                // 마지막 인덱스에서 ↓ 누르는거 방지
                if (e.KeyCode == Keys.Down && MessageTemplateDataGridView.Rows.Count == rowIndex + 1) return;

                if (e.KeyCode.Equals(Keys.Up))
                {
                    CHAR_MSG_NO = MessageTemplateDataGridView.Rows[rowIndex - 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_NO].Value.ToString();
                    CHAR_MSG_DVSN_CD = MessageTemplateDataGridView.Rows[rowIndex - 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_DVSN_CD].Value.ToString();
                    CHAR_MSG_TITL_NM = MessageTemplateDataGridView.Rows[rowIndex - 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_TITLE_NM].Value.ToString();
                    CHAR_MSG_CNTS = MessageTemplateDataGridView.Rows[rowIndex - 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_CNTS].Value.ToString();
                }
                else if (e.KeyCode.Equals(Keys.Down))
                {
                    CHAR_MSG_NO = MessageTemplateDataGridView.Rows[rowIndex + 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_NO].Value.ToString();
                    CHAR_MSG_DVSN_CD = MessageTemplateDataGridView.Rows[rowIndex + 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_DVSN_CD].Value.ToString();
                    CHAR_MSG_TITL_NM = MessageTemplateDataGridView.Rows[rowIndex + 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_TITLE_NM].Value.ToString();
                    CHAR_MSG_CNTS = MessageTemplateDataGridView.Rows[rowIndex + 1].Cells[(int)eMessageTemplateDataGridView.CHAR_MSG_CNTS].Value.ToString();
                }

                MessageTemplateDataGridViewChoice();
            }
        }

        // SMS, LMS, 이메일 등 템플릿 타입에 따라 문자 템플릿 내용의 텍스트박스 크기를 조정
        private void MessageDivisionNoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setContentsTextBoxByType();
        }
    }
}
