using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using TripERP.Common;

namespace TripERP.CommonTask
{
    public partial class EmployeeInfoMgt : Form
    {
        enum eEmpdataGridView
        {
            EMPL_NO,                // 임직원 사번
            EMPL_NM,                // 임직원 성명
            PSTN_CD,                // 임직원 직위
            ENCP_DT,                // 임직원 입사일
            LVCP_DT,                // 임직원 퇴사일
            HLOF_DVSN_CD,           // 임직원 재직구분코드
            CELL_PHNE_NO,           // 임직원 휴대전화번호
            EMAL_ADDR,              // 임직원 이메일
            HOME_ADDR,              // 임직원 집 전화번호
            EMPL_MEMO_CNTS          // 임직원 메모내용
        };

        string EMPL_NO = "";                // 임직원 사번
        string EMPL_NM = "";                // 임직원 성명
        string PSTN_CD = "";                // 임직원 직위
        string ENCP_DT = "";                // 임직원 입사일
        string LVCP_DT = "";                // 임직원 퇴사일
        string HLOF_DVSN_CD = "";           // 임직원 재직구분코드
        string CELL_PHNE_NO = "";           // 임직원 휴대전화번호
        string EMAL_ADDR = "";              // 임직원 이메일
        string HOME_ADDR = "";              // 임직원 집 전화번호
        string EMPL_MEMO_CNTS = "";         // 임직원 메모내용

        public EmployeeInfoMgt()
        {
            InitializeComponent();
        }

        private void EmployeeInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchEmployeeCodeList();           // form load 시 내용 바로 출력
        }

        // 초기화
        private void InitControls()
        {
            string query = "";
            DataSet dataSet;

            // 임직원코드, 임직원 성명
            SearchEmpNocomboBox.Items.Clear();
            SearchEmpNmtextBox.Clear();

            query = "CALL SelectEmployeeCodeList ()";
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("임직원 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 임직원 정보를 콤보에 설정
                string EMPL_NO = dataRow["EMPL_NO"].ToString();
                string EMPL_NM = dataRow["EMPL_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(EMPL_NM, EMPL_NO);
                SearchEmpNocomboBox.Items.Add(item);
            }

            SearchEmpNocomboBox.SelectedIndex = -1;

            ResetInputFormField();    // 그리드 스타일 초기화
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = EmpdataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;
            //dataGridView.RowTemplate.Resizable = false;
        }

        private void btnInitializeEmpInfo_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 입력필드 초기화
        private void ResetInputFormField()
        {
            EmpNotextBox.Text = "";
            EmpNametextBox.Text = "";
            EmpCodecomboBox.Text = "";
            EntrydateTimePicker.Text = "";
            LeavedateTimePicker.Text = "";
            HlofDvsnCodecomboBox.Text = "";
            CellPhonetextBox.Text = "";
            txtEmailAddr1.Text = "";
            txtEmailAddr2.Text = "";
            EmptxtHomeAddr.Text = "";
            EmpMemotextBox.Text = "";

            EmpCodecomboBox.Items.Clear();
            HlofDvsnCodecomboBox.Items.Clear();
            txtEmailAddr2.Items.Clear();


            string query = "";
            DataSet dataSet;


            // 직위코드
            query = "CALL SelectEmpPositionCodeList ()";
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("임직원 직위 코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 임직원 직위 코드를 콤보에 설정
                string PSTN_CD = dataRow["CD_VLID_VAL"].ToString();
                string PSTN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                /*
                // 암호화 내용 복호화
                PSTN_NM = EncryptMgt.Decrypt(PSTN_NM, EncryptMgt.aesEncryptKey);
                */

                ComboBoxItem item = new ComboBoxItem(PSTN_NM, PSTN_CD);
                EmpCodecomboBox.Items.Add(item);
            }

            // 재직구분코드
            query = "CALL Select_HLOF_DVSN_CD_CodeList ()";
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("임직원 재직현황 코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 임직원 재직현황 코드를 콤보에 설정
                string PSTN_CD = dataRow["CD_VLID_VAL"].ToString();
                string PSTN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(PSTN_NM, PSTN_CD);
                HlofDvsnCodecomboBox.Items.Add(item);
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
                txtEmailAddr2.Items.Add(item);
            }

            txtEmailAddr2.SelectedIndex = -1;
            HlofDvsnCodecomboBox.SelectedIndex = -1;
            emailAddr2TextBox.Visible = false;
            InitDataGridView();
        }

        private void searchEmpMenuListButton_Click(object sender, EventArgs e)
        {
            SearchEmployeeCodeList();
        }

        // 임직원 목록 테이블 조회
        private void SearchEmployeeCodeList()
        {
            EmpdataGridView.Rows.Clear();

            string EMPL_NO = Utils.GetSelectedComboBoxItemValue(SearchEmpNocomboBox);
            string EMPL_NM = SearchEmpNmtextBox.Text.Trim();
            string PSTN_CD;
            string PSTN_NM;
            string ENCP_DT;
            string LVCP_DT;
            string HLOF_DVSN_CD;
            string CELL_PHNE_NO;
            string EMAL_ADDR = "";
            string HOME_ADDR;
            string EMPL_MEMO_CNTS;

            string query = string.Format("CALL SelectEmplListCondition ('{0}', '{1}')", EMPL_NO, EMPL_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("임직원 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                EMPL_NO = datarow["EMPL_NO"].ToString();
                EMPL_NM = datarow["EMPL_NM"].ToString();
                PSTN_NM = datarow["PSTN_NM"].ToString();
                ENCP_DT = datarow["ENCP_DT"].ToString().Substring(0,10);
                LVCP_DT = datarow["LVCP_DT"].ToString();
                if (LVCP_DT.Length > 10) LVCP_DT.Substring(0, 10);

                HLOF_DVSN_CD = datarow["HLOF_DVSN_CD"].ToString();
                CELL_PHNE_NO = datarow["CELL_PHNE_NO"].ToString();
                if (datarow["EMAL_ADDR"].ToString() != "@")
                { 
                    EMAL_ADDR = datarow["EMAL_ADDR"].ToString();
                }
                else
                {
                    EMAL_ADDR = "";
                }
                HOME_ADDR = datarow["HOME_ADDR"].ToString();
                EMPL_MEMO_CNTS = datarow["EMPL_MEMO_CNTS"].ToString();
                                
                /*
                //---------------------------------------------------------------------------------------
                // 개인정보 암호화내용 복호화           --> 191024 박현호
                //---------------------------------------------------------------------------------------
                EMPL_NM = EncryptMgt.Decrypt(EMPL_NM, EncryptMgt.aesEncryptKey);                                    // 직원명
                PSTN_NM = EncryptMgt.Decrypt(PSTN_NM, EncryptMgt.aesEncryptKey);                                    // 직위                
                CELL_PHNE_NO = EncryptMgt.Decrypt(CELL_PHNE_NO, EncryptMgt.aesEncryptKey);                  // 직원 전화번호
                EMAL_ADDR = EncryptMgt.Decrypt(EMAL_ADDR, EncryptMgt.aesEncryptKey);                            // 직원 이메일
                HOME_ADDR = EncryptMgt.Decrypt(HOME_ADDR, EncryptMgt.aesEncryptKey);                            // 직원 집주소
                */
                

                EmpdataGridView.Rows.Add(EMPL_NO, EMPL_NM, PSTN_NM, ENCP_DT, LVCP_DT, HLOF_DVSN_CD, CELL_PHNE_NO, EMAL_ADDR, HOME_ADDR, EMPL_MEMO_CNTS);
            }
            EmpdataGridView.ClearSelection();
        }

        // 마우스로 키다운하여 그리드 선택 시
        private void EmpdataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = EmpdataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;            
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && EmpdataGridView.Rows.Count == rowIndex + 1)
                return;            

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (EmpdataGridView.SelectedRows.Count == 0)
                return;
            EMPL_NO = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.EMPL_NO].Value.ToString();
            EMPL_NM = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.EMPL_NM].Value.ToString();
            PSTN_CD = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.PSTN_CD].Value.ToString();
            ENCP_DT = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.ENCP_DT].Value.ToString();
            LVCP_DT = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.LVCP_DT].Value.ToString();
            HLOF_DVSN_CD = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.HLOF_DVSN_CD].Value.ToString();
            CELL_PHNE_NO = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.CELL_PHNE_NO].Value.ToString();
            EMAL_ADDR = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.EMAL_ADDR].Value.ToString();
            HOME_ADDR = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.HOME_ADDR].Value.ToString();
            EMPL_MEMO_CNTS = EmpdataGridView.Rows[rowIndex].Cells[(int)eEmpdataGridView.EMPL_MEMO_CNTS].Value.ToString();
            optionDataGridViewRowChoice();
        }        

        // 데이터 그리드뷰 클릭
        private void EmpdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmpdataGridView.SelectedRows.Count == 0)
                return;

            EMPL_NO = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.EMPL_NO].Value.ToString();
            EMPL_NM = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.EMPL_NM].Value.ToString();
            PSTN_CD = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.PSTN_CD].Value.ToString();
            ENCP_DT = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.ENCP_DT].Value.ToString();
            LVCP_DT = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.LVCP_DT].Value.ToString();
            HLOF_DVSN_CD = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.HLOF_DVSN_CD].Value.ToString();
            CELL_PHNE_NO = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.CELL_PHNE_NO].Value.ToString();
            EMAL_ADDR = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.EMAL_ADDR].Value.ToString();
            HOME_ADDR = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.HOME_ADDR].Value.ToString();
            EMPL_MEMO_CNTS = EmpdataGridView.SelectedRows[0].Cells[(int)eEmpdataGridView.EMPL_MEMO_CNTS].Value.ToString();

            optionDataGridViewRowChoice();
        }

        // 선택한 그리드의 값을 입력란으로 전달
        private void optionDataGridViewRowChoice()
        {
            // 이메일 주소 분리
            if (EMAL_ADDR != "" && EMAL_ADDR != null)
            {
                string text = EMAL_ADDR;
                string[] split_text;
                split_text = text.Split('@');

                txtEmailAddr1.Text = split_text[0];                                 // 이메일주소
                txtEmailAddr2.Text = split_text[1];                                 // 이메일주소2
            }
            else
            {
                txtEmailAddr1.Text = "";
                txtEmailAddr2.Text = "";
            }

            EmpNotextBox.Text = EMPL_NO;
            EmpNametextBox.Text = EMPL_NM;
            EmpCodecomboBox.Text = PSTN_CD;
            EntrydateTimePicker.Text = ENCP_DT;            
            HlofDvsnCodecomboBox.Text = HLOF_DVSN_CD;
            CellPhonetextBox.Text = CELL_PHNE_NO;
            EmptxtHomeAddr.Text = HOME_ADDR;
            EmpMemotextBox.Text = EMPL_MEMO_CNTS;
            //LeavedateTimePicker.Text = null;

            if (HLOF_DVSN_CD == "3")
            {
                label7.Visible = true;
                LeavedateTimePicker.Visible = true;
                DateTime dateTime = DateTime.ParseExact(LVCP_DT, "yyyy-mm-dd", CultureInfo.InvariantCulture);                
                LeavedateTimePicker.Text = LVCP_DT; 
            }
            else
            {
                label7.Visible = false;
                LeavedateTimePicker.Visible = false;
            }
        }

        // 재직구분코드를 변경했을 때 이벤트 처리
        private void HlofDvsnCodecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HlofDvsnCodecomboBox.SelectedIndex == 2)
            {
                label7.Visible = true;
                LeavedateTimePicker.Visible = true;
            }
            else
            {
                label7.Visible = false;
                LeavedateTimePicker.Visible = false;
            }
        }

        private void btnDeleteEmpInfo_Click(object sender, EventArgs e)
        {
            string EMPL_NO = EmpNotextBox.Text.Trim();                                              

            if (EMPL_NO == "")
            {
                MessageBox.Show("임직원 사번은 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요.");
                return;
            }

            string query = string.Format("CALL DeleteEmployeeInfo ('{0}')", EMPL_NO);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("임직원 정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchEmployeeCodeList();
                MessageBox.Show("임직원 정보를 삭제했습니다.");
            }
            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        private void btnCloseEmpPopUp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveEmpInfo_Click(object sender, EventArgs e)
        {
            SaveEmployeeCodeList();
        }        

        // 임직원 저장 
        private void SaveEmployeeCodeList()
        {
            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;

            EMPL_NO = EmpNotextBox.Text.Trim();                                          // 직원번호
            EMPL_NM = EmpNametextBox.Text.Trim();                                        // 직원명
            PSTN_CD = Utils.GetSelectedComboBoxItemValue(EmpCodecomboBox);               // 직위코드
            ENCP_DT = EntrydateTimePicker.Value.ToString("yyyy-MM-dd");                  // 입사일자                        
            HLOF_DVSN_CD = Utils.GetSelectedComboBoxItemValue(HlofDvsnCodecomboBox);     // 재직구분코드

            if (HLOF_DVSN_CD == "3")
            {
                LVCP_DT = LeavedateTimePicker.Value.ToString("yyyy-MM-dd");              // 퇴사일자
            }
            else
            {
                LVCP_DT = "9999-12-31";
            }
                string CELL_PHNE_NO = CellPhonetextBox.Text.Trim();                       // 휴대전화번호

            string emailFullAddress = "";
            string emailDomainAddress = "";            

            // 이메일주소 직접입력인 경우 처리
            if (emailAddr2TextBox.Text.Trim().Length > 0)
            {
                emailDomainAddress = emailAddr2TextBox.Text.Trim();
            }
            else
            {
                emailDomainAddress = txtEmailAddr2.Text.Trim();        // 이메일도메인주소
            }

            if(txtEmailAddr1.Text != "" && txtEmailAddr1.Text != null)
            {
                emailFullAddress = txtEmailAddr1.Text + "@" + emailDomainAddress;              // 이메일주소 조합
            }
            string EMAL_ADDR = emailFullAddress;                                               // 이메일주소                    
            string HOME_ADDR = EmptxtHomeAddr.Text.Trim();                                     // 집주소
            string EMPL_MEMO_CNTS = EmpMemotextBox.Text.Trim();                                // 직원메모내용
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                    // 최초등록자ID

            string query = "";

            /*
            // 퇴사일자가 없을때 insert
            if (LVCP_DT == "" || LVCP_DT == null)
            {
                query = string.Format("CALL InsertEmployeeInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                            EMPL_NO, EMPL_NM, PSTN_CD, ENCP_DT, HLOF_DVSN_CD, CELL_PHNE_NO, EMAL_ADDR, HOME_ADDR, EMPL_MEMO_CNTS, FRST_RGTR_ID);
            }
            // 퇴사일자가 있을때 insert
            else
            {
            */

            /*
            //------------------------------------------------------------------------------------------------
            // 개인정보 암호화     --> 191024 박현호
            //------------------------------------------------------------------------------------------------
            EMPL_NM = EncryptMgt.Encrypt(EMPL_NM.Trim(), EncryptMgt.aesEncryptKey);                                // 직원명
            CELL_PHNE_NO = EncryptMgt.Encrypt(CELL_PHNE_NO.Trim(), EncryptMgt.aesEncryptKey);             // 직원 전화번호
            EMAL_ADDR = EncryptMgt.Encrypt(EMAL_ADDR.Trim(), EncryptMgt.aesEncryptKey);                       // 직원 이메일
            HOME_ADDR = EncryptMgt.Encrypt(HOME_ADDR.Trim(), EncryptMgt.aesEncryptKey);                     // 직원 집주소
            */


            query = string.Format("CALL InsertEmployeeInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')",
                            EMPL_NO, EMPL_NM, PSTN_CD, ENCP_DT, LVCP_DT, HLOF_DVSN_CD, CELL_PHNE_NO, EMAL_ADDR, HOME_ADDR, EMPL_MEMO_CNTS, FRST_RGTR_ID);                
            //}

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("직원정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchEmployeeCodeList();                                                       // 직원 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("직원정보를 저장했습니다.");
            }
            ResetInputFormField();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string EMPL_NO = EmpNotextBox.Text.Trim();                                         // 직원번호
            string EMPL_NM = EmpNametextBox.Text.Trim();                                       // 직원명
            string PSTN_CD = Utils.GetSelectedComboBoxItemValue(EmpCodecomboBox);              // 직위코드
            string ENCP_DT = EntrydateTimePicker.Value.ToString("yyyy-MM-dd");                 // 입사일자
            string HLOF_DVSN_CD = Utils.GetSelectedComboBoxItemValue(HlofDvsnCodecomboBox);    // 재직구분코드
            string CELL_PHNE_NO = CellPhonetextBox.Text.Trim();                                // 휴대전화번호            

            if (EMPL_NO == "")
            {
                MessageBox.Show("직원사번은 필수 입력항목입니다.");
                EmpNotextBox.Focus();
                return false;
            }

            if (EMPL_NM == "")
            {
                MessageBox.Show("직원 성명은 필수 입력항목입니다.");
                EmpNametextBox.Focus();
                return false;
            }

            if (PSTN_CD == "")
            {
                MessageBox.Show("직위 코드는 필수 입력항목입니다.");
                EmpCodecomboBox.Focus();
                return false;
            }

            if (ENCP_DT == "")
            {
                MessageBox.Show("입사일은 필수 입력항목입니다.");
                EntrydateTimePicker.Focus();
                return false;
            }

            if (HLOF_DVSN_CD == "")
            {
                MessageBox.Show("재직구분코드는 필수 입력항목입니다.");
                HlofDvsnCodecomboBox.Focus();
                return false;
            }
            /*
            if (CELL_PHNE_NO == "")
            {
                MessageBox.Show("휴대전화번호는 필수 입력항목입니다.");
                CellPhonetextBox.Focus();
                return false;
            }
            */
            return true;            
        }

        private void emailAddr2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEmailAddr2.SelectedIndex == 0)
            {
                emailAddr2TextBox.Visible = true;
                emailAddr2TextBox.Focus();
            }
            else
            {
                emailAddr2TextBox.Visible = false;                
            }
        }
    }
}