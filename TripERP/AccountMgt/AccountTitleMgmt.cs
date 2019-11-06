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

namespace TripERP.AccountMgt
{
    public partial class AccountTitleMgmt : Form
    {
        enum eAccountTitleListDataGridView
        {
            ACNT_TIT_CD,                       // 계정과목코드
            ACNT_TIT_NM,                       // 계정과목명
            ACNT_TYP_CD,                       // 계정유형코드
            ACNT_TYP_NM,                       // 계정유형명
            SCRN_SORT_ORD,                     // 화면정렬순서
            USE_YN,                            // 사용여부
            USE_YN_NM,                         // 사용여부명
            DBCR_DVSN_CD,                      // 차대구분코드
            DBCR_DVSN_NM                       // 차대구분명
        };

        public AccountTitleMgmt()
        {
            InitializeComponent();
        }

        //==============================================================================================================================================
        // 폼로딩 초기화
        //==============================================================================================================================================
        private void AccountTitleMgmt_Load(object sender, EventArgs e)
        {
            loadCommonCodeComboBox();
            searchAccountCodeList();
        }

        //==============================================================================================================================================
        // 공통코드 콤보박스 설정
        //==============================================================================================================================================
        private void loadCommonCodeComboBox()
        {
            // 사용여부 콤보박스는 못불러와서 따로 설정 -- 배장훈 09/18
            string query = "CALL SelectCommoncodeList ('YN')";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0) {
                MessageBox.Show("사용여부 공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows) {
                string USE_YN = dataRow["CD_VLID_VAL"].ToString();
                string USE_YN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(USE_YN_NM, USE_YN);
                useYnComboBox.Items.Add(item);
            }

            string[] groupNameArray = { "ACNT_TYP_CD", "DBCR_DVSN_CD"};

            ComboBox[] comboBoxArray = { accountTypeComboBox, debitCreditComboBox, useYnComboBox };

            for (int gi = 0; gi < groupNameArray.Length; gi++)
            {
                if (comboBoxArray[gi].Items.Count > 0) comboBoxArray[gi].Items.Clear();

                List<CommonCodeItem> list = Global.GetCommonCodeList(groupNameArray[gi]);

                for (int li = 0; li < list.Count; li++)
                {
                    string value = list[li].Value.ToString();
                    string desc = list[li].Desc;

                    ComboBoxItem item = new ComboBoxItem(desc, value);

                    comboBoxArray[gi].Items.Add(item);
                }

                if (comboBoxArray[gi].Items.Count > 0)
                    comboBoxArray[gi].SelectedIndex = -1;
            }
        }






        //=========================================================================================================================================================================
        // 그리드 초기화
        //=========================================================================================================================================================================
        private void InitDataGridView()
        {
            DataGridView dataGridView1 = accountTitleListDataGridView;
            dataGridView1.DoubleBuffered(true);
        }





        //==============================================================================================================================================
        // 검색버튼 클릭
        //==============================================================================================================================================
        private void searchAccountCodeListButton_Click(object sender, EventArgs e)
        {
            searchAccountCodeList();
        }

        //-------------------------------------------------------------------------------------------------------------------------
        // 계정과목 목록조회 (검색 버튼 동작 Method)
        //-------------------------------------------------------------------------------------------------------------------------
        private void searchAccountCodeList()
        {
            accountTitleListDataGridView.Rows.Clear();

            string ACNT_TIT_CD = "";                                                                      // 계정과목코드
            string ACNT_TIT_NM = searchAccountTitleNameTextBox.Text.Trim();                               // 계정과목명
            string ACNT_TYP_CD = "";                                                                      // 계정유형코드
            string ACNT_TYP_NM = "";                                                                      // 계정유형명
            string DBCR_DVSN_CD = "";                                                                     // 차대구분코드
            string DBCR_DVSN_NM = "";                                                                     // 차대구분명
            string SCRN_SORT_ORD = "";                                                                    // 화면정렬순서
            string USE_YN = "";                                                                           // 사용여부
            string USE_YN_NM = "";                                                                        // 사용여부명

            string query = string.Format("CALL SelectAccountTitleList ('{0}')", ACNT_TIT_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("계정과목기본정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {

                ACNT_TIT_CD = datarow["ACNT_TIT_CD"].ToString();
                ACNT_TIT_NM = datarow["ACNT_TIT_NM"].ToString();
                ACNT_TYP_CD = datarow["ACNT_TYP_CD"].ToString();
                ACNT_TYP_NM = datarow["ACNT_TYP_NM"].ToString();
                DBCR_DVSN_CD = datarow["DBCR_DVSN_CD"].ToString();
                DBCR_DVSN_NM = datarow["DBCR_DVSN_NM"].ToString();
                SCRN_SORT_ORD = datarow["SCRN_SORT_ORD"].ToString();
                USE_YN = datarow["USE_YN"].ToString();
                USE_YN_NM = datarow["USE_YN_NM"].ToString();

                accountTitleListDataGridView.Rows.Add
                (
                    ACNT_TIT_CD,                       // 계정과목코드
                    ACNT_TIT_NM,                       // 계정과목명
                    ACNT_TYP_CD,                       // 계정유형코드
                    ACNT_TYP_NM,                       // 계정유형명
                    SCRN_SORT_ORD,                     // 화면정렬순서
                    USE_YN,                            // 사용여부
                    USE_YN_NM                          // 사용여부명
                );
            }

            accountTitleListDataGridView.ClearSelection();
        }








        //==============================================================================================================================================
        // 그리드 행 클릭
        //==============================================================================================================================================
        private void accountTitleListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (accountTitleListDataGridView.SelectedRows.Count == 0)
                return;

            string ACNT_TIT_CD = accountTitleListDataGridView.SelectedRows[0].Cells[(int)eAccountTitleListDataGridView.ACNT_TIT_CD].Value.ToString();
            string ACNT_TIT_NM = accountTitleListDataGridView.SelectedRows[0].Cells[(int)eAccountTitleListDataGridView.ACNT_TIT_NM].Value.ToString();
            string ACNT_TYP_CD = accountTitleListDataGridView.SelectedRows[0].Cells[(int)eAccountTitleListDataGridView.ACNT_TYP_CD].Value.ToString();
            string SCRN_SORT_ORD = accountTitleListDataGridView.SelectedRows[0].Cells[(int)eAccountTitleListDataGridView.SCRN_SORT_ORD].Value.ToString();
            string USE_YN = accountTitleListDataGridView.SelectedRows[0].Cells[(int)eAccountTitleListDataGridView.USE_YN].Value.ToString();

            accountTitleCodeTextBox.Text = ACNT_TIT_CD;                                         // 계정과목코드
            accountTitleCodeTextBox.ReadOnly = true;
            accountTitleNameTextBox.Text = ACNT_TIT_NM;                                         // 계정과목코드
            Utils.SelectComboBoxItemByValue(accountTypeComboBox, ACNT_TYP_CD);                  // 계정유형코드
            screenSortOrderTextBox.Text = SCRN_SORT_ORD;                                        // 화면정렬순서
            Utils.SelectComboBoxItemByValue(useYnComboBox, USE_YN);                             // 사용여부
        }







        //==============================================================================================================================================
        // 계정과목명 키 입력 이벤트
        //==============================================================================================================================================
        private void searchAccountTitleNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchAccountCodeList();
            }
        }






        //==============================================================================================================================================
        // 초기화버튼 Event Method
        //==============================================================================================================================================
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInput();
        }

        //-----------------------------------------------------------------
        // 초기화 버튼 동작 Method
        //-----------------------------------------------------------------
        private void ResetInput() {
            accountTitleCodeTextBox.Text = "";
            accountTitleCodeTextBox.ReadOnly = false;
            accountTitleNameTextBox.Text = "";
            accountTypeComboBox.SelectedIndex = -1;
            debitCreditComboBox.SelectedIndex = -1;
            screenSortOrderTextBox.Text = "";
            useYnComboBox.SelectedIndex = -1;
        }







        //==============================================================================================================================================
        // 저장 버튼 클릭 EventMethod
        //==============================================================================================================================================        
        private void saveButton_Click(object sender, EventArgs e) {
            saveAccountTitle();
        }

        //--------------------------------------------------------------------------------------------------------------------
        // 저장 버튼 동작 Method
        //--------------------------------------------------------------------------------------------------------------------
        public void saveAccountTitle()
        {
            string ACNT_TIT_CD = accountTitleCodeTextBox.Text.Trim();                                     // 계정과목코드
            string ACNT_TIT_NM = accountTitleNameTextBox.Text.Trim();                               // 계정과목명
            string ACNT_TYP_CD = Utils.GetSelectedComboBoxItemValue(accountTypeComboBox);                 // 계정유형코드
            string ACNT_TYP_NM = Utils.GetSelectedComboBoxItemText(accountTypeComboBox);                  // 계정유형명
            //string DBCR_DVSN_CD = Utils.GetSelectedComboBoxItemValue(debitCreditComboBox);                // 차대구분코드
            //string DBCR_DVSN_NM = Utils.GetSelectedComboBoxItemText(debitCreditComboBox);                 // 차대구분명
            string SCRN_SORT_ORD = screenSortOrderTextBox.Text.Trim();                                   // 화면정렬순서
            string USE_YN = Utils.GetSelectedComboBoxItemValue(useYnComboBox);                           // 사용여부
            string USE_YN_NM = Utils.GetSelectedComboBoxItemText(useYnComboBox);                         // 사용여부명

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            string MDFR_ID = Global.loginInfo.ACNT_ID;                         // 최초+최종 변경자ID
            string query = "";

            // 계정과목코드 null일시 Insert
            if (ACNT_TIT_CD.Equals(""))
            {
                query = string.Format("CALL InsertAccountTitleInfo ('{0}','{1}','{2}','{3}','{4}','{5}')",
                    ACNT_TIT_CD, ACNT_TIT_NM, ACNT_TYP_CD, SCRN_SORT_ORD, USE_YN, MDFR_ID);
            }
            // 계정과목코드 not null일시 Update
            if (!ACNT_TIT_CD.Equals(""))
            {
                query = string.Format("CALL UpdateAccountTitleInfo ('{0}','{1}','{2}','{3}','{4}','{5}')",
                    ACNT_TIT_CD, ACNT_TIT_NM, ACNT_TYP_CD, SCRN_SORT_ORD, USE_YN, MDFR_ID);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("계정과목코드를 저장할 수 없습니다.");
                return;
            }
            else
            {
                MessageBox.Show("계정과목코드를 저장했습니다.");
            }

            searchAccountCodeList(); // 등록 후 그리드를 최신상태로 Refresh
            ResetInput();
        }








        //==============================================================================================================================================
        // 삭제 버튼 클릭 EventMethod
        //==============================================================================================================================================
        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteAccountTitle();
        }

        //--------------------------------------------------------------------------------------------------------------------
        // 삭제 버튼 동작 Method
        //--------------------------------------------------------------------------------------------------------------------
        public void deleteAccountTitle()
        {
            string ACNT_TIT_CD = accountTitleCodeTextBox.Text.Trim();                                     // 계정과목코드

            if (ACNT_TIT_CD.Equals(""))
            {
                MessageBox.Show("계정과목쿠드는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요");
                accountTitleCodeTextBox.Focus();
                return;
            }

            if (MessageBox.Show("계정과목코드를 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteCurrentAccountTitleInfo ('{0}')", ACNT_TIT_CD);

            int retVal = DbHelper.ExecuteNonQuery(query);

            if (retVal == -1)
            {
                MessageBox.Show("계정과목코드를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                searchAccountCodeList();
                MessageBox.Show("계정과목코드를 삭제했습니다.");
            }
            ResetInput();
        }







        //=================================================================================================================================
        // 폼 닫기.
        //=================================================================================================================================
        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }







        //=================================================================================================================================
        // 입력값 유효성 검증 Method
        //=================================================================================================================================
        private bool CheckRequireItems() {
            string ACNT_TIT_CD = accountTitleCodeTextBox.Text.Trim();                                     // 계정과목코드
            string ACNT_TIT_NM = accountTitleNameTextBox.Text.Trim();                               // 계정과목명
            string ACNT_TYP_NM = Utils.GetSelectedComboBoxItemText(accountTypeComboBox);                  // 계정유형명
            //string DBCR_DVSN_CD = Utils.GetSelectedComboBoxItemValue(debitCreditComboBox);                // 차대구분코드
            //string DBCR_DVSN_NM = Utils.GetSelectedComboBoxItemText(debitCreditComboBox);                 // 차대구분명
            string SCRN_SORT_ORD = screenSortOrderTextBox.Text.Trim();                                   // 화면정렬순서
            string USE_YN_NM = Utils.GetSelectedComboBoxItemText(useYnComboBox);                         // 사용여부명

            if (ACNT_TIT_CD == "") {
                MessageBox.Show("계정과목코드는 필수 입력항목입니다.");
                accountTitleCodeTextBox.Focus();
                return false;
            }

            if (ACNT_TIT_NM == "") {
                MessageBox.Show("계좌번호는 필수 입력항목입니다.");
                searchAccountTitleNameTextBox.Focus();
                return false;
            }

            if (ACNT_TYP_NM == "") {
                MessageBox.Show("계정유형은 필수 입력항목입니다.");
                accountTypeComboBox.Focus();
                return false;
            }

            if (SCRN_SORT_ORD == "") {
                MessageBox.Show("화면정렬순서는 필수 입력항목입니다.");
                screenSortOrderTextBox.Focus();
                return false;
            }

            if (USE_YN_NM == "") {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
                useYnComboBox.Focus();
                return false;
            }

            return true;
        }
    }
}
