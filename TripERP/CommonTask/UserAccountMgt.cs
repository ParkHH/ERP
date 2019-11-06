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
    public partial class UserAccountMgt : Form
    {
        string _tableSequenceNumber = "";                                  // 테이블 순번

        enum eUserAccountdataGridView
        {
            TABL_CNMB,       // 테이블 순번
            CMPN_NO,         // 업체번호
            CMPN_NM,         // 회사명
            EMPL_NO,         // 직원번호
            EMPL_NM,         // 직원명
            PSTN_NM,         // 직위명
            ACNT_ID,         // 계정ID
            ACNT_PW,         // 계정PW
            CNNC_PRMI_YN,    // 접속허용여부
            CNNC_PRMI_YN_NM, // 접속허용여부명
            HLOF_DVSN_CD,    // 재직구분코드
            HLOF_DVSN_NM,    // 재직구분코드명
            SCRN_SORT_ORD,    // 화면정렬순서
            MAC                         // MAC 주소
        }

        public UserAccountMgt()
        {
            InitializeComponent();
        }

        private void UserAccountMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchUserAccountList();
        }

        //=========================================================================================================================================================================
        // 초기화
        //=========================================================================================================================================================================
        private void InitControls()
        {
            loadComboBoxItem();
            resetInputFormField();
            InitDataGridView();        // 그리드 스타일 초기화
        }

        //=========================================================================================================================================================================
        // 콤보박스 설정
        //=========================================================================================================================================================================
        private void loadComboBoxItem()
        {
            string query = "";
            DataSet dataSet;
            
            // 회사명, 사용자명, 부서명 목록 삭제
            searchCompanyNameComboBox.Items.Clear();
            requestCompanyNameComboBox.Items.Clear();
            requestEmployeeNameTextBox.Items.Clear();
            accessAllowComboBox.Items.Clear();

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 회사명 콤보박스 설정
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            string COOP_CMPN_DVSN_CD = "01";                      // 본사
            query = string.Format("CALL SelectCompanyNameList ('{0}')", COOP_CMPN_DVSN_CD);
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("회사명 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 회사명 정보를 콤보에 설정
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                searchCompanyNameComboBox.Items.Add(item);
                requestCompanyNameComboBox.Items.Add(item);
            }

            //if (searchCompanyNameComboBox.Items.Count >= 0) searchCompanyNameComboBox.SelectedIndex = 0;
            searchCompanyNameComboBox.SelectedIndex = 0;
            requestCompanyNameComboBox.SelectedIndex = 0;

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 여부 콤보박스 설정
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            query = "CALL SelectCommoncodeList ('YN')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string USE_YN = dataRow["CD_VLID_VAL"].ToString();
                string USE_YN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(USE_YN_NM, USE_YN);
                accessAllowComboBox.Items.Add(item);
            }

            // 직원명 콤보박스 설정
            loadEmployeeComboBox();
        }

        //=========================================================================================================================================================================
        // 직원명 콤보박스 설정
        //=========================================================================================================================================================================
        private void loadEmployeeComboBox()
        {
            string query = "CALL SelectEmplList ()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("담당자 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string EMPL_NO = dataRow["EMPL_NO"].ToString();
                string EMPL_NM = dataRow["EMPL_NM"].ToString();

                /*
                //-----------------------------------------------------------------------------------------------
                // 복호화진행        --> 191024 박현호
                //-----------------------------------------------------------------------------------------------
                EMPL_NM = EncryptMgt.Decrypt(EMPL_NM, EncryptMgt.aesEncryptKey);
                */

                ComboBoxItem item = new ComboBoxItem(EMPL_NM, EMPL_NO);

                requestEmployeeNameTextBox.Items.Add(item);
            }

            requestEmployeeNameTextBox.SelectedIndex = -1;
        }


        //=========================================================================================================================================================================
        // 그리드 초기화
        //=========================================================================================================================================================================
        private void InitDataGridView()
        {
            DataGridView dataGridView = userAccountdataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;
            //dataGridView.RowTemplate.Resizable = false;
        }

        //=========================================================================================================================================================================
        // 초기화 버튼 클릭
        //=========================================================================================================================================================================
        private void InitializeButton_Click(object sender, EventArgs e)
        {
            resetInputFormField();
        }

        //=========================================================================================================================================================================
        // 입력필드 초기화
        //=========================================================================================================================================================================
        private void resetInputFormField()
        {
            _tableSequenceNumber = "";                           // 테이블순번
            //searchCompanyNameComboBox.Text = "";                 // 검색 회사명
            searchEmployeeNameTextBox.Text = "";                 // 검색 직원명
            searchAccountIdTextbox.Text = "";                    // 검색용 ID
            //requestCompanyNameComboBox.SelectedIndex = -1;       // 회사
            requestEmployeeNameTextBox.SelectedIndex = -1;       // 직원명
            requestPositionNameTextBox.Text = "";                // 직위명
            accountIdTextBox.Text = "";                          // 계정ID
            accessAllowComboBox.SelectedIndex = -1;              // 접속허용여부
            screenOrderTextBox.Text = "";                        // 화면정렬순서
        }

        //=========================================================================================================================================================================
        // 검색버튼 클릭
        //=========================================================================================================================================================================
        private void searchUserAccountButton_Click(object sender, EventArgs e)
        {
            SearchUserAccountList();
        }

        //=========================================================================================================================================================================
        // 계정정보 검색
        //=========================================================================================================================================================================
        private void SearchUserAccountList()
        {
            userAccountdataGridView.Rows.Clear();

            string TABL_CNMB;                                                                 // 테이블 순번
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(searchCompanyNameComboBox);   // 업체번호
            string CMPN_NM = "";                                                              // 업체명
            string EMPL_NO = "";                                                              // 직원번호
            string EMPL_NM = searchEmployeeNameTextBox.Text.Trim();                           // 직원명
            string PSTN_NM = "";                                                              // 직위
            string ACNT_ID = searchAccountIdTextbox.Text.Trim();                              // 계정ID
            string ACNT_PW = "";                                                              // 계정PW
            string CNNC_PRMI_YN = "";                                                         // 접속허용여부
            string CNNC_PRMI_YN_NM = "";                                                      // 접속허용여부명 (예/아니오)
            string HLOF_DVSN_CD = "";                                                         // 재직구분코드
            string HLOF_DVSN_NM = "";                                                         // 재직구분코드명
            string SCRN_SORT_ORD = "";                                                        // 화면정렬순서
            string MAC = "";

            string query = string.Format("CALL SelectUserAccounttList ('{0}', '{1}', '{2}')", CMPN_NO, EMPL_NM, ACNT_ID);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용자 계정정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                TABL_CNMB = datarow["TABL_CNMB"].ToString();
                CMPN_NO = datarow["CMPN_NO"].ToString();
                CMPN_NM = datarow["CMPN_NM"].ToString();
                ACNT_ID = datarow["ACNT_ID"].ToString();
                EMPL_NO = datarow["EMPL_NO"].ToString();
                EMPL_NM = datarow["EMPL_NM"].ToString();
                PSTN_NM = datarow["PSTN_NM"].ToString();
                ACNT_PW = datarow["ACNT_PW"].ToString();
                CNNC_PRMI_YN = datarow["CNNC_PRMI_YN"].ToString();
                CNNC_PRMI_YN_NM = datarow["CNNC_PRMI_YN_NM"].ToString();
                HLOF_DVSN_CD = datarow["HLOF_DVSN_CD"].ToString();
                HLOF_DVSN_NM = datarow["HLOF_DVSN_NM"].ToString();
                SCRN_SORT_ORD = datarow["SCRN_SORT_ORD"].ToString();
                MAC = datarow["MAC"].ToString().Trim();

                /*
                //-------------------------------------------------------------------------------------------
                // 암호화 내용 복호화 진행  --> 191024 박현호
                //-------------------------------------------------------------------------------------------
                EMPL_NM = EncryptMgt.Decrypt(EMPL_NM, EncryptMgt.aesEncryptKey);                        // 직원명
                PSTN_NM = EncryptMgt.Decrypt(PSTN_NM, EncryptMgt.aesEncryptKey);                        // 직위
                */

                userAccountdataGridView.Rows.Add(TABL_CNMB, CMPN_NO, CMPN_NM, EMPL_NO, EMPL_NM, PSTN_NM, ACNT_ID, ACNT_PW, CNNC_PRMI_YN, CNNC_PRMI_YN_NM, HLOF_DVSN_CD, HLOF_DVSN_NM, SCRN_SORT_ORD, MAC);
            }
            userAccountdataGridView.ClearSelection();
        }

        //=========================================================================================================================================================================
        // 그리드 행 클릭
        //=========================================================================================================================================================================
        private void UserAccountListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (userAccountdataGridView.SelectedRows.Count == 0)
                return;

            string TABL_CNMB = "";
            string CMPN_NO = "";
            string CMPN_NM = "";
            string ACNT_ID = "";
            string EMPL_NO = "";
            string EMPL_NM = "";
            string PSTN_NM = "";
            string ACNT_PW = "";
            string CNNC_PRMI_YN = "";
            string CNNC_PRMI_YN_NM = "";
            string HLOF_DVSN_NM = "";
            string SCRN_SORT_ORD = "";
            string MAC = "";

            TABL_CNMB = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.TABL_CNMB].Value.ToString();
            CMPN_NO = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.CMPN_NO].Value.ToString();
            CMPN_NM = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.CMPN_NM].Value.ToString();
            ACNT_ID = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.ACNT_ID].Value.ToString();
            EMPL_NO = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.EMPL_NO].Value.ToString();
            EMPL_NM = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.EMPL_NM].Value.ToString();
            PSTN_NM = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.PSTN_NM].Value.ToString();
            ACNT_PW = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.ACNT_PW].Value.ToString();
            CNNC_PRMI_YN = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.CNNC_PRMI_YN].Value.ToString();
            CNNC_PRMI_YN_NM = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.CNNC_PRMI_YN_NM].Value.ToString();
            HLOF_DVSN_NM = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.HLOF_DVSN_NM].Value.ToString();
            SCRN_SORT_ORD = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.SCRN_SORT_ORD].Value.ToString();
            MAC = userAccountdataGridView.SelectedRows[0].Cells[(int)eUserAccountdataGridView.MAC].Value.ToString(); 

            _tableSequenceNumber = TABL_CNMB;
            Utils.SelectComboBoxItemByValue(requestCompanyNameComboBox, CMPN_NO);               // 회사(업체번호)
            Utils.SelectComboBoxItemByValue(requestEmployeeNameTextBox, EMPL_NO);               // 직원명
            requestPositionNameTextBox.Text = PSTN_NM;                                          // 직위
            accountIdTextBox.Text = ACNT_ID;                                                    // ID
            Utils.SelectComboBoxItemByValue(requestEmployeeNameTextBox, EMPL_NO);               // 직원명
            Utils.SelectComboBoxItemByValue(accessAllowComboBox, CNNC_PRMI_YN);                 // 접속허용여부
            tb_MAC.Text = MAC;
        }

        //=========================================================================================================================================================================
        // 폼 닫기버튼 클릭
        //=========================================================================================================================================================================
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //=========================================================================================================================================================================
        // 삭제버튼 클릭
        //=========================================================================================================================================================================
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_tableSequenceNumber == "")
            {
                MessageBox.Show("목록에서 삭제대상 계정을 선택하세요.");
                userAccountdataGridView.Focus();
                return;
            }

            string query = string.Format("CALL DeleteUserAccountInfo ('{0}')", _tableSequenceNumber);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("사용자 계정을 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchUserAccountList();
            }

            // 삭제 후 입력폼 초기화
            resetInputFormField();
        }

        //=========================================================================================================================================================================
        // 키보드로 그리드 행 이동
        //=========================================================================================================================================================================
        private void UserAccountdataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = userAccountdataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && userAccountdataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (userAccountdataGridView.SelectedRows.Count == 0)
                return;

            string TABL_CNMB = "";
            string CMPN_NO = "";
            string CMPN_NM = "";
            string ACNT_ID = "";
            string EMPL_NO = "";
            string EMPL_NM = "";
            string PSTN_NM = "";
            string ACNT_PW = "";
            string CNNC_PRMI_YN = "";
            string CNNC_PRMI_YN_NM = "";
            string HLOF_DVSN_NM = "";
            string SCRN_SORT_ORD = "";
            string MAC = "";

            TABL_CNMB = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.TABL_CNMB].Value.ToString();
            CMPN_NO = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.CMPN_NO].Value.ToString();
            CMPN_NM = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.CMPN_NM].Value.ToString();
            ACNT_ID = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.ACNT_ID].Value.ToString();
            EMPL_NO = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.EMPL_NO].Value.ToString();
            EMPL_NM = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.EMPL_NM].Value.ToString();
            PSTN_NM = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.PSTN_NM].Value.ToString();
            ACNT_PW = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.ACNT_PW].Value.ToString();
            CNNC_PRMI_YN = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.CNNC_PRMI_YN].Value.ToString();
            CNNC_PRMI_YN_NM = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.CNNC_PRMI_YN_NM].Value.ToString();
            HLOF_DVSN_NM = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.HLOF_DVSN_NM].Value.ToString();
            SCRN_SORT_ORD = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.SCRN_SORT_ORD].Value.ToString();
            MAC = userAccountdataGridView.Rows[rowIndex].Cells[(int)eUserAccountdataGridView.MAC].Value.ToString();

            _tableSequenceNumber = TABL_CNMB;
            Utils.SelectComboBoxItemByValue(requestCompanyNameComboBox, CMPN_NO);               // 회사(업체번호)
            Utils.SelectComboBoxItemByValue(requestEmployeeNameTextBox, EMPL_NO);               // 직원명
            requestPositionNameTextBox.Text = PSTN_NM;                                          // 직위
            accountIdTextBox.Text = ACNT_ID;                                                    // ID
            Utils.SelectComboBoxItemByValue(requestEmployeeNameTextBox, EMPL_NO);               // 직원명
            Utils.SelectComboBoxItemByValue(accessAllowComboBox, CNNC_PRMI_YN);                 // 접속허용여부
            tb_MAC.Text = MAC;
        }

        //=========================================================================================================================================================================
        // 저장버튼 클릭
        //=========================================================================================================================================================================
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveUserAccountList();
        }

        //=========================================================================================================================================================================
        // 계정 정보 저장
        //=========================================================================================================================================================================
        private void SaveUserAccountList()
        {
            string TABL_CNMB = _tableSequenceNumber;                                                                                 // 테이블 순번
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(requestCompanyNameComboBox);            // 회사명
            string EMPL_NO = Utils.GetSelectedComboBoxItemValue(requestEmployeeNameTextBox);                 // 직원명
            string ACNT_ID = accountIdTextBox.Text.Trim();                                                                          // 계정ID
            string ACNT_PW = "";                                                                                                                // 비밀번호
            string CNNC_PRMI_YN = Utils.GetSelectedComboBoxItemValue(accessAllowComboBox);          // 접속허용여부
            string SCRN_SORT_ORD = screenOrderTextBox.Text.Trim();                                                      // 화면정렬순서            
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                                                     // 최초등록자ID
            string MAC = tb_MAC.Text.Trim();


            //------------------------------------------------------------------------
            // ID 유효성 검사
            //------------------------------------------------------------------------
            bool validateResult = vaildateLoginValue(ACNT_ID);
            if (!validateResult)
            {                
                return;
            }


            //-------------------------------------------------------------------------
            // MAC 주소 유효성 검사
            //-------------------------------------------------------------------------
            bool result = checkMAC(MAC);
            if (!result)
            {
                return;
            }

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;

            // 테이블 순번이 입력되지 않을 경우
            if (TABL_CNMB == "" || TABL_CNMB == null)
            {
                // 초기패스워드 설정
                string initializedPassword = ACNT_ID + DateTime.Now.ToString("yyyyMMdd");
                ACNT_PW = generatePasswordHash(initializedPassword);

                query = string.Format("CALL InsertUserAccountInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}','{7}')",
                        CMPN_NO, EMPL_NO, ACNT_ID, ACNT_PW, CNNC_PRMI_YN, SCRN_SORT_ORD, FRST_RGTR_ID, MAC);
            }
            else
            {
                query = string.Format("CALL UpdateUserAccountInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}','{8}')",
                        TABL_CNMB, CMPN_NO, EMPL_NO, ACNT_ID, ACNT_PW, CNNC_PRMI_YN, SCRN_SORT_ORD, FRST_RGTR_ID, MAC);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("사용자 계정 정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchUserAccountList();                                                       // 사용자 계정 등록 후 그리드를 최신상태로 Refresh
            }

            SearchUserAccountList();
            resetInputFormField();
        }

        //=========================================================================================================================================================================
        // 입력값 유효성 검증
        //=========================================================================================================================================================================
        private bool CheckRequireItems()
        {
            string TABL_CNMB = _tableSequenceNumber;                                                // 테이블 순번
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(requestCompanyNameComboBox);        // 회사명
            string EMPL_NO = Utils.GetSelectedComboBoxItemValue(requestEmployeeNameTextBox);        // 직원명
            string ACNT_ID = accountIdTextBox.Text.Trim();                                          // 계정ID
            string CNNC_PRMI_YN = Utils.GetSelectedComboBoxItemValue(accessAllowComboBox);          // 접속허용여부
            string SCRN_SORT_ORD = screenOrderTextBox.Text.Trim();                                  // 화면정렬순서            
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            if (CMPN_NO == "")
            {
                MessageBox.Show("회사명은 필수 입력항목입니다.");
                requestCompanyNameComboBox.Focus();
                return false;
            }

            if (EMPL_NO == "")
            {
                MessageBox.Show("직원명은 필수 입력항목입니다.");
                requestEmployeeNameTextBox.Focus();
                return false;
            }            

            if (ACNT_ID == "")
            {
                MessageBox.Show("계정ID는 필수 입력항목입니다.");
                accountIdTextBox.Focus();
                return false;
            }

            if (CNNC_PRMI_YN == "")
            {
                MessageBox.Show("접속허용 구분은 필수 입력항목입니다.");
                accessAllowComboBox.Focus();
                return false;
            }

            if (SCRN_SORT_ORD == "")
            {
                MessageBox.Show("화면정렬순서는 필수 입력항목입니다.");
                screenOrderTextBox.Focus();
                return false;
            }

            return true;
        }

        //=========================================================================================================================================================================
        // 패스워드 초기화
        //=========================================================================================================================================================================
        private void PasswordInitializeButton_Click(object sender, EventArgs e)
        {
            string ACNT_ID = accountIdTextBox.Text.Trim();
            string initializedPassword = ACNT_ID + DateTime.Now.ToString("yyyyMMdd");
            string ACNT_PW = generatePasswordHash(initializedPassword);

            string query = string.Format("CALL UpdateUserAccountPassword ('{0}', '{1}', '{2}', '{3}')", _tableSequenceNumber, ACNT_ID, ACNT_PW, Global.loginInfo.ACNT_ID);
            DataSet dataSet = DbHelper.SelectQuery(query);

            MessageBox.Show("비밀번호를 초기화 하였습니다.");

            SearchUserAccountList();
            resetInputFormField();
        }

        //=========================================================================================================================================================================
        // 패스워드 변경
        //=========================================================================================================================================================================
        private void PasswordSavebutton_Click(object sender, EventArgs e)
        {
            string ACNT_ID = accountIdTextBox.Text.Trim();            
            string input_pw = accountPasswordTextBox.Text.Trim();
            string ACNT_PW = generatePasswordHash(input_pw);

            string query = string.Format("CALL UpdateUserAccountPassword ('{0}', '{1}', '{2}', '{3}')", _tableSequenceNumber, ACNT_ID, ACNT_PW, Global.loginInfo.ACNT_ID);
            DataSet dataSet = DbHelper.SelectQuery(query);

            MessageBox.Show("비밀번호를 저장 하였습니다.");

            SearchUserAccountList();
            resetInputFormField();
        }

        //=========================================================================================================================================================================
        // 패스워드 해시 생성
        //=========================================================================================================================================================================
        private string generatePasswordHash(string passwordString)
        {
            return HashGenerate.GenerateMySQLHash(passwordString);
        }





        //=========================================================================================================================================================================
        // MAC 주소값 유효성 검사  --> 190918 박현호
        //=========================================================================================================================================================================
        public bool checkMAC(string MAC)
        {
            string[] MACStrArr = MAC.Split('-');
            string MACNum = "";
            bool result = false;

            if(MACStrArr.Length != 6)
            {
                MessageBox.Show("- 이외의 특수문자는 입력할 수 없습니다.");
                return result;
            }

            for(int i=0; i<MACStrArr.Length; i++)
            {
                MACNum += MACStrArr[i];
            }

            if (MACNum.Length != 12)
            {
                MessageBox.Show("MAC 주소값은 12자리 입니다.");
                return result;
            }
            result = true;

            return result;
        }




        //==============================================================================================================================
        // 직원 이름 선택시 직위를 자동으로 Setting 하게함               --> 191024 박현호
        //==============================================================================================================================
        private void requestEmployeeNameTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPSTN_NM();
        }

        //--------------------------------------------------------------------------------------------------------------------
        // 직원명 ComboBox Index 변경시 동작하는 Method               --> 191024 박현호
        //--------------------------------------------------------------------------------------------------------------------
        public void setPSTN_NM()
        {
            try
            { 
                ComboBoxItem item = (ComboBoxItem)requestEmployeeNameTextBox.SelectedItem;
                string EMPL_NO = item.Value.ToString().Trim();
                string EMPL_NM = requestEmployeeNameTextBox.Text.Trim();

                /*
                //DB 에 암호화되어 등록되어있으므로 암호화 해서 Parameter 로 넣어줌
                EMPL_NM = EncryptMgt.Encrypt(EMPL_NM, EncryptMgt.aesEncryptKey);
                */

                string query = string.Format("CALL SelectEmplListCondition('{0}','{1}')", EMPL_NO, EMPL_NM);
                DataSet ds = DbHelper.SelectQuery(query);
                int rowCount = ds.Tables[0].Rows.Count;
                if (rowCount == 0)
                {
                    MessageBox.Show("해당 직원명으로 검색된 직위가 없습니다.\n직원 등록 Data 를 확인하세요.");
                    return;
                }
                DataRow row = ds.Tables[0].Rows[0];
                string PSTN_NM = row["PSTN_NM"].ToString().Trim();

                 /*
                // 복호화
                PSTN_NM = EncryptMgt.Decrypt(PSTN_NM, EncryptMgt.aesEncryptKey);
                */

                requestPositionNameTextBox.Text = PSTN_NM;
            }
            catch
            {

            }
        }





        //=======================================================================================================
        // 로그인 입력값 유효성 검사           --> 191105 박현호
        //=======================================================================================================
        private bool vaildateLoginValue(string in_id)
        {
            bool result = false;

            string[] charSpecial = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+", "\'", "\"", "`", "/", "<", ">", ".", ",", "\\", "]", "[", "{", "}" };
            for (int i = 0; i < charSpecial.Length; i++)
            {
                string charSome = charSpecial[i];
                if (in_id.Equals(charSome) || in_id.Contains(charSome))
                {
                    MessageBox.Show("입력한 ID 값이 올바르지 않습니다.\nID 에 특수문자를 사용할 수 없습니다.");
                    return result;
                }
            }

            result = true;

            return result;
        }
    }
}