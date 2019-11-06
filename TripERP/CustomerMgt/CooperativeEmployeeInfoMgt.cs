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

namespace TripERP.CustomerMgt
{
    public partial class CooperativeEmployeeInfoMgt : Form
    {
        enum eCooperativeEmployeeListDataGridView
        {            
            CMPN_NM,                     // 회사명
            RPTV_EMPL_CNMB,              // 직원일련번호
            RPTV_NM,                     // 직원명
            DEPT_NM,                     // 부서명
            PSTN_NM,                     // 직위명
            RPSB_BIZ_CNTS,               // 담당업무내용
            RPTV_OFFC_PHNE_NO,           // 담당자 직장전화번호
            RPTV_OFFC_EXT_NO,            // 담당자 직장내선번호
            RPTV_CELL_PHNE_NO,           // 담당자 휴대전화번호
            RPTV_FAX_NO,                 // 담당자 팩스번호
            RPTV_EMAL_ADDR               // 담당자 이메일주소                        
        };

        string CMPN_NO;                     // 회사번호
        string CMPN_NM;                     // 회사명
        string RPTV_EMPL_CNMB;              // 직원일련번호
        string RPTV_NM;                     // 직원명
        string DEPT_NM;                     // 부서명
        string PSTN_NM;                     // 직위명
        string RPSB_BIZ_CNTS;               // 담당업무내용
        string RPTV_OFFC_PHNE_NO;           // 담당자 직장전화번호
        string RPTV_OFFC_EXT_NO;            // 담당자 직장내선번호
        string RPTV_CELL_PHNE_NO;           // 담당자 휴대전화번호
        string RPTV_EMAL_ADDR;              // 담당자 이메일주소
        string RPTV_FAX_NO;                 // 담당자 팩스번호
        string FRST_RGTR_ID;                // 최초등록ID

        public CooperativeEmployeeInfoMgt()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void CooperativeEmployeeInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchCooperativeEmployeeList();                // form load 시 내용 바로 출력
        }

        // 초기화
        private void InitControls()
        {
            string query = "";
            DataSet dataSet;

            // 회사명, 직원명, 업무내용
            SearchCooperativeNamecomboBox.Text.Trim();
            SearchCooperativeEmployeeComboBox.Text.Trim();
            SearchRpsbBizTextBox.Text.Trim();

            // 회사명, 회사코드 적재
            query = "CALL SelectCompanyNameCodeList()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("회사명 정보를 가져올 수 없습니다.");
                return;
            }
            
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);
                SearchCooperativeNamecomboBox.Items.Add(item);
            }

            SearchCooperativeNamecomboBox.SelectedIndex = -1;

            // 직원명, 직원코드 적재
            query = "CALL SelectCooperativeEmployeeNameCodeList()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("협력업체 직원명 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string RPTV_NM = dataRow["RPTV_NM"].ToString();
                SearchCooperativeEmployeeComboBox.Items.Add(RPTV_NM);
            }

            SearchCooperativeEmployeeComboBox.SelectedIndex = -1;            

            // 폼 입력필드 초기화
            ResetInputFormField();

            // 그리드 스타일 초기화
            InitDataGridView();
        }

        // 폼 입력필드 초기화
        private void ResetInputFormField()
        {
            CooperativeCompanyNameMgtComboBox.Text = "";        // 회사명
            RptvEmplCnmbTextBox.Text = "";                      // 직원 일련번호 조회
            InputCooperativeEmployeeNameTextBox.Text = "";      // 고객구분 조회
            InputCooperativeDepartmentMgtTextBox.Text = "";     // 부서명
            InputEmployeePostiontionNameTextBox.Text = "";      // 직위명

            RpsbBizCntsTextBox.Text = "";                       // 담당업무
            OfficePhoneNumberTextBox.Text = "";                 // 직장전화번호
            OfficePhoneExtNumberTextBox.Text = "";              // 직장내선번호
            EmployeeCellphoneTextBox.Text = "";                 // 휴대전화번호
            OfficeFaxTextBox.Text = "";                         // 팩스번호

            CooperativeEmployeetxtEmailAddr.Text = "";                            // 이메일 앞부분 입력
            CooperativeEmployeeemailAddr2ComboBox.Text = "";                       // 이메일 뒷부분 입력
            CooperativeEmployeeemailAddr2ComboBox.SelectedIndex = -1;              // 이메일 뒷부분 입력 초기화

            CooperativeEmployeeemailAddr2TextBox.Text = "";                        // 이메일 직접입력 초기화
            CooperativeEmployeeemailAddr2TextBox.Visible = false;

            CooperativeEmployeeemailAddr2ComboBox.Items.Clear();
            CooperativeCompanyNameMgtComboBox.Items.Clear();


            string query = "";
            DataSet dataSet;

            // 회사명, 회사코드 적재
            query = "CALL SelectCompanyNameCodeList()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("회사명 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);
                CooperativeCompanyNameMgtComboBox.Items.Add(item);
            }

            query = "CALL SelectCommonCodeList('EMAL_DOMN_ADDR')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("이메일 정보를 가져올 수 없습니다.");
                return;
            }

            // 이메일 주소 @뒤 리스트
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string CUST_EMAL_CD = dataRow["CD_VLID_VAL"].ToString();
                string CUST_EMAL_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(CUST_EMAL_NM, CUST_EMAL_CD);
                CooperativeEmployeeemailAddr2ComboBox.Items.Add(item);
            }

            CooperativeEmployeeemailAddr2ComboBox.SelectedIndex = -1;
            CooperativeCompanyNameMgtComboBox.SelectedIndex = -1;
        }

        private void btnInitializeCooperativeEmployeeInfo_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }


        private void InitDataGridView()
        {
            DataGridView dataGridView = CooperativeEmployeeDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        private void btnClosePopUp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 협력업체 직원목록 조회버튼 클릭
        private void searchCooperativeEmployeeListButton_Click(object sender, EventArgs e)
        {
            SearchCooperativeEmployeeList();
        }

        private void SearchCooperativeEmployeeList()
        {
            CooperativeEmployeeDataGridView.Rows.Clear();

            string CMPN_NO;                     // 회사번호
            string CMPN_NM;                     // 회사명
            string RPTV_EMPL_CNMB;              // 직원일련번호
            string RPTV_NM;                     // 직원명
            string DEPT_NM;                     // 부서명
            string PSTN_NM;                     // 직위명
            string RPSB_BIZ_CNTS;               // 담당업무내용
            string RPTV_OFFC_PHNE_NO;           // 담당자 직장전화번호
            string RPTV_OFFC_EXT_NO;            // 담당자 직장내선번호
            string RPTV_CELL_PHNE_NO;           // 담당자 휴대전화번호
            string RPTV_FAX_NO;                 // 담당자 팩스번호
            string RPTV_EMAL_ADDR;              // 담당자 이메일주소            
            
            CMPN_NO = Utils.GetSelectedComboBoxItemValue(SearchCooperativeNamecomboBox);
            RPTV_NM = SearchCooperativeEmployeeComboBox.Text.Trim();
            RPSB_BIZ_CNTS = SearchRpsbBizTextBox.Text.Trim();

            string query = string.Format("CALL db_gbridge_trip.SelectCooperativeEmployeeList('{0}','{1}','{2}')", CMPN_NO, RPTV_NM, RPSB_BIZ_CNTS);

            //Console.WriteLine(query);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("협력업체 직원정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                CMPN_NM = datarow["CMPN_NM"].ToString();
                RPTV_EMPL_CNMB = datarow["RPTV_EMPL_CNMB"].ToString();
                RPTV_NM = datarow["RPTV_NM"].ToString();
                DEPT_NM = datarow["DEPT_NM"].ToString();
                PSTN_NM = datarow["PSTN_NM"].ToString();
                RPSB_BIZ_CNTS = datarow["RPSB_BIZ_CNTS"].ToString();
                RPTV_OFFC_PHNE_NO = datarow["RPTV_OFFC_PHNE_NO"].ToString();
                RPTV_OFFC_EXT_NO = datarow["RPTV_OFFC_EXT_NO"].ToString();
                RPTV_CELL_PHNE_NO = datarow["RPTV_CELL_PHNE_NO"].ToString();
                RPTV_FAX_NO = datarow["RPTV_FAX_NO"].ToString();
                RPTV_EMAL_ADDR = datarow["RPTV_EMAL_ADDR"].ToString();                

                CooperativeEmployeeDataGridView.Rows.Add
                (
                    CMPN_NM,                     // 회사명
                    RPTV_EMPL_CNMB,              // 직원일련번호
                    RPTV_NM,                     // 직원명
                    DEPT_NM,                     // 부서명
                    PSTN_NM,                     // 직위명
                    RPSB_BIZ_CNTS,               // 담당업무내용
                    RPTV_OFFC_PHNE_NO,           // 담당자 직장전화번호
                    RPTV_OFFC_EXT_NO,            // 담당자 직장내선번호
                    RPTV_CELL_PHNE_NO,           // 담당자 휴대전화번호
                    RPTV_FAX_NO,                 // 담당자 팩스번호
                    RPTV_EMAL_ADDR               // 담당자 이메일주소                    
                );
            }
            CooperativeEmployeeDataGridView.ClearSelection();
        }

        // 셀 클릭
        private void CooperativeEmployeeListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CooperativeEmployeeDataGridView.SelectedRows.Count == 0)
                return;

            CMPN_NM = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.CMPN_NM].Value.ToString();                      // 회사명            
            RPTV_EMPL_CNMB = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_EMPL_CNMB].Value.ToString();        // 직원일련번호            
            RPTV_NM = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_NM].Value.ToString();                      // 직원명            
            DEPT_NM = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.DEPT_NM].Value.ToString();                      // 부서명
            PSTN_NM = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.PSTN_NM].Value.ToString();                      // 직위명
            RPSB_BIZ_CNTS = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPSB_BIZ_CNTS].Value.ToString();          // 담당업무내용
            RPTV_OFFC_PHNE_NO = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_OFFC_PHNE_NO].Value.ToString();  // 담당자직장전화번호
            RPTV_OFFC_EXT_NO = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_OFFC_EXT_NO].Value.ToString();    // 담당자직장내선번호
            RPTV_CELL_PHNE_NO = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_CELL_PHNE_NO].Value.ToString();  // 휴대전화번호
            RPTV_FAX_NO = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_FAX_NO].Value.ToString();              // 팩스번호                        
            RPTV_EMAL_ADDR = CooperativeEmployeeDataGridView.SelectedRows[0].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_EMAL_ADDR].Value.ToString();        // 이메일주소                       

            optionDataGridViewRowChoice();
        }

        private void optionDataGridViewRowChoice()
        {
            // 이메일 주소 분리
            if (RPTV_EMAL_ADDR != "")
            {
                string text = RPTV_EMAL_ADDR;
                string[] split_text;
                split_text = text.Split('@');

                CooperativeEmployeetxtEmailAddr.Text = split_text[0];                                  // 이메일주소
                CooperativeEmployeeemailAddr2ComboBox.Text = split_text[1];                             // 이메일주소2
                Utils.SelectComboBoxItemByValue(CooperativeEmployeeemailAddr2ComboBox, split_text[1]);
            }
            else
            {
                CooperativeEmployeetxtEmailAddr.Text = "";
                CooperativeEmployeeemailAddr2ComboBox.Text = "";
            }            

            CooperativeCompanyNameMgtComboBox.Text = CMPN_NM;                      // 회사명            
            RptvEmplCnmbTextBox.Text = RPTV_EMPL_CNMB;                             // 직원일련번호
            InputCooperativeEmployeeNameTextBox.Text = RPTV_NM;                    // 직원명
            InputCooperativeDepartmentMgtTextBox.Text = DEPT_NM;                   // 부서명
            InputEmployeePostiontionNameTextBox.Text = PSTN_NM;                    // 직위명
            RpsbBizCntsTextBox.Text = RPSB_BIZ_CNTS;                               // 담당업무내용
            OfficePhoneNumberTextBox.Text = RPTV_OFFC_PHNE_NO;                     // 담당자직장전화번호
            OfficePhoneExtNumberTextBox.Text = RPTV_OFFC_EXT_NO;                   // 담당자직장내선번호
            EmployeeCellphoneTextBox.Text = RPTV_CELL_PHNE_NO;                     // 휴대전화번호
            OfficeFaxTextBox.Text = RPTV_FAX_NO;                                   // 팩스번호            
        }

        private void CooperativeEmployeeGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = CooperativeEmployeeDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && CooperativeEmployeeDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (CooperativeEmployeeDataGridView.SelectedRows.Count == 0)
                return;

            CMPN_NM = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.CMPN_NM].Value.ToString();                      // 회사명            
            RPTV_EMPL_CNMB = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_EMPL_CNMB].Value.ToString();        // 직원일련번호            
            RPTV_NM = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_NM].Value.ToString();                      // 직원명            
            DEPT_NM = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.DEPT_NM].Value.ToString();                      // 부서명
            PSTN_NM = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.PSTN_NM].Value.ToString();                      // 직위명
            RPSB_BIZ_CNTS = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPSB_BIZ_CNTS].Value.ToString();          // 담당업무내용
            RPTV_OFFC_PHNE_NO = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_OFFC_PHNE_NO].Value.ToString();  // 담당자직장전화번호
            RPTV_OFFC_EXT_NO = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_OFFC_EXT_NO].Value.ToString();    // 담당자직장내선번호
            RPTV_CELL_PHNE_NO = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_CELL_PHNE_NO].Value.ToString();  // 휴대전화번호
            RPTV_FAX_NO = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_FAX_NO].Value.ToString();              // 팩스번호                        
            RPTV_EMAL_ADDR = CooperativeEmployeeDataGridView.Rows[rowIndex].Cells[(int)eCooperativeEmployeeListDataGridView.RPTV_EMAL_ADDR].Value.ToString();        // 이메일주소                       
               
            optionDataGridViewRowChoice();
        }

        private void btnDeleteCooperativeEmployeeInfo_Click(object sender, EventArgs e)
        {
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyNameMgtComboBox);    // 회사번호
            string RPTV_EMPL_CNMB = RptvEmplCnmbTextBox.Text.Trim();                                   // 직원번호

            if (CMPN_NO == "")
            {
                MessageBox.Show("회사명은 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요.");
                return;
            }

            string query = string.Format("CALL DeleteCooperativeEmployeeInfo ('{0}', '{1}')", CMPN_NO, RPTV_EMPL_CNMB);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("협력업체 직원정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchCooperativeEmployeeList();
                MessageBox.Show("협력업체 직원정보를 삭제했습니다.");
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        // 협력업체 직원정보 저장
        private void btnSaveCooperativeEmployeeInfo_Click(object sender, EventArgs e)
        {
            string CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyNameMgtComboBox); // 회사코드
            string RPTV_EMPL_CNMB = RptvEmplCnmbTextBox.Text.Trim();                                // 직원일련번호
            string RPTV_NM = InputCooperativeEmployeeNameTextBox.Text.Trim();                       // 직원명
            string DEPT_NM = InputCooperativeDepartmentMgtTextBox.Text.Trim();                      // 부서명
            string PSTN_NM = InputEmployeePostiontionNameTextBox.Text.Trim();                       // 직위명
            string RPSB_BIZ_CNTS = RpsbBizCntsTextBox.Text.Trim();                                  // 담당업무내용
            string RPTV_OFFC_PHNE_NO = OfficePhoneNumberTextBox.Text.Trim();                        // 담당자직장전화번호 
            string RPTV_OFFC_EXT_NO = OfficePhoneExtNumberTextBox.Text.Trim();                      // 담당자직장내선번호  
            string RPTV_CELL_PHNE_NO = EmployeeCellphoneTextBox.Text.Trim();                        // 휴대전화번호
            string RPTV_FAX_NO = OfficeFaxTextBox.Text.Trim();                                      // 팩스번호

            string emailFullAddress = "";                                                           // 이메일주소
            string emailDomainAddress = "";
            // EMAIL 주소 반영전 CONCAT 처리


            // 이메일주소가 입력된 경우
            if (CooperativeEmployeetxtEmailAddr.Text.Trim().Length > 0)
            {
                // 이메일주소 직접입력인 경우 처리
                if (CooperativeEmployeeemailAddr2TextBox.Text.Trim().Length > 0)
                {
                    emailDomainAddress = CooperativeEmployeeemailAddr2TextBox.Text.Trim();          // 이메일도메인주소
                }
                // 이메일주소 기존 도메인에 포함될 경우
                else
                {
                    emailDomainAddress = CooperativeEmployeeemailAddr2ComboBox.Text.Trim();         // 이메일도메인주소
                }

                emailFullAddress = CooperativeEmployeetxtEmailAddr.Text.Trim() + "@" + emailDomainAddress;            // 이메일주소조합
                RPTV_EMAL_ADDR = emailFullAddress;                                                                    // 이메일주소
            }
            // 이메일주소가 입력되지 않은 경우
            else
            {
                RPTV_EMAL_ADDR = null;
            }


            FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                                              // 최초등록자ID

            string query = "";
            
            
            // 직원일련번호 MAX+1로 채번하고 Insert처리            
            if(RPTV_EMPL_CNMB == "" || RPTV_EMPL_CNMB == null)
            { 
                query = string.Format("CALL SelectMaxCooperativeEmployeeNo ('{0}')", CMPN_NO);
                DataSet dataSet = DbHelper.SelectQuery(query);
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBox.Show("협력업체 직원번호를 가져올 수 없습니다.");
                    return;
                }
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                RPTV_EMPL_CNMB = dataRow["RPTV_EMPL_CNMB"].ToString(); // 협력업체 직원일련번호

                // 입력값 유효성 검증
                if (CheckRequireItems() == false) return;

                query = string.Format("CALL InsertCooperativeEmployeeInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                            CMPN_NO, RPTV_EMPL_CNMB, RPTV_NM, DEPT_NM, PSTN_NM, RPSB_BIZ_CNTS, RPTV_OFFC_PHNE_NO, RPTV_OFFC_EXT_NO, RPTV_CELL_PHNE_NO, RPTV_EMAL_ADDR, RPTV_FAX_NO, FRST_RGTR_ID);                
            }

            else
            {
                // 입력값 유효성 검증
                if (CheckRequireItems() == false) return;

                query = string.Format("CALL InsertCooperativeEmployeeInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                            CMPN_NO, RPTV_EMPL_CNMB, RPTV_NM, DEPT_NM, PSTN_NM, RPSB_BIZ_CNTS, RPTV_OFFC_PHNE_NO, RPTV_OFFC_EXT_NO, RPTV_CELL_PHNE_NO, RPTV_EMAL_ADDR, RPTV_FAX_NO, FRST_RGTR_ID);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("협력업체 직원정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchCooperativeEmployeeList();                            // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("협력업체 직원정보를 저장했습니다.");
            }
            // 저장 후 입력폼 초기화
            ResetInputFormField();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            CMPN_NO = Utils.GetSelectedComboBoxItemValue(CooperativeCompanyNameMgtComboBox); // 회사코드            
            RPTV_NM = InputCooperativeEmployeeNameTextBox.Text.Trim();                       // 직원명

            if (CMPN_NO == "")
            {
                MessageBox.Show("회사코드는 필수 입력항목입니다.");
                CooperativeCompanyNameMgtComboBox.Focus();
                return false;
            }            

            if (RPTV_NM == "")
            {
                MessageBox.Show("직위명은 필수 입력항목입니다.");
                InputCooperativeEmployeeNameTextBox.Focus();
                return false;
            }
            return true;
        }

        private void emailAddr2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CooperativeEmployeeemailAddr2ComboBox.SelectedIndex == 0)
            {
                CooperativeEmployeeemailAddr2TextBox.Visible = true;
                CooperativeEmployeeemailAddr2TextBox.Focus();
            }
        }
    }
}