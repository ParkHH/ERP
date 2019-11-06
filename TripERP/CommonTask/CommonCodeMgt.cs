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
    public partial class CommonCodeMgt : Form
    {
        enum eCommonCodeListDataGridView { DOMN_KOR_NM, DOMN_ENG_NM, DATA_TYPE_CNTS, LIST_ENTT_NM, CD_DESC_CNTS };
        enum eCommonCodeValidValueListDataGrideView { DOMN_KOR_NM, DOMN_ENG_NM, CD_VLID_VAL, CD_VLID_VAL_DESC, SCRN_SORT_ORD };

        public CommonCodeMgt()
        {
            InitializeComponent();
        }

        private void InitControls()
        {
            // 그리드 스타일 초기화
            InitDataGridView();
        }

        // 그리드 스타일 초기화 
        private void InitDataGridView()
        {
            // 공통코드목록
            DataGridView dataGridView1 = CommonCodeListDataGridView;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Aqua;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.RosyBrown;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.RowTemplate.ReadOnly = true;

            // 공통코드유효값목록
            DataGridView dataGridView2 = CommonCodeValidValueListDataGrideView;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Aqua;
            dataGridView2.RowHeadersVisible = false;
            //dataGridView2.RowTemplate.ReadOnly = true;
        }

        // 폼 닫기
        private void closeCommonCodeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 폼 닫기
        private void closeCommonCodeValidValueButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 공통코드목록 검색버튼 클릭
        private void searchCommonCodeListButton_Click(object sender, EventArgs e)
        {
            SearchCommonCodeList();
        }

        // 공통코드목록 검색
        private void SearchCommonCodeList()
        {
            CommonCodeListDataGridView.Rows.Clear();

            string DOMN_KOR_NM = SearchCodeKorNameTextBox.Text.Trim();
            string DOMN_ENG_NM = SearchCodeEngNameTextBox.Text.Trim();
            string DATA_TYPE_CNTS = "";
            string LIST_ENTT_NM = "";
            string CD_DESC_CNTS = "";

            string query = string.Format("CALL SelectCommonCodeListForMgmt ('{0}', '{1}')", DOMN_KOR_NM, DOMN_ENG_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공통코드목록을 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                DOMN_KOR_NM = datarow["DOMN_KOR_NM"].ToString();
                DOMN_ENG_NM = datarow["DOMN_ENG_NM"].ToString();
                DATA_TYPE_CNTS = datarow["DATA_TYPE_CNTS"].ToString();
                LIST_ENTT_NM = datarow["LIST_ENTT_NM"].ToString();
                CD_DESC_CNTS = datarow["CD_DESC_CNTS"].ToString();

                CommonCodeListDataGridView.Rows.Add(DOMN_KOR_NM, DOMN_ENG_NM, DATA_TYPE_CNTS, LIST_ENTT_NM, CD_DESC_CNTS);
            }

            CommonCodeListDataGridView.ClearSelection();
        }

        // 공통코드목록 그리드 행 클릭
        private void CommonCodeListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CommonCodeListDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            string DOMN_KOR_NM = CommonCodeListDataGridView.SelectedRows[0].Cells[(int)eCommonCodeListDataGridView.DOMN_KOR_NM].Value.ToString();
            string DOMN_ENG_NM = CommonCodeListDataGridView.SelectedRows[0].Cells[(int)eCommonCodeListDataGridView.DOMN_ENG_NM].Value.ToString();
            string DATA_TYPE_CNTS = CommonCodeListDataGridView.SelectedRows[0].Cells[(int)eCommonCodeListDataGridView.DATA_TYPE_CNTS].Value.ToString();
            string CD_DESC_CNTS = CommonCodeListDataGridView.SelectedRows[0].Cells[(int)eCommonCodeListDataGridView.CD_DESC_CNTS].Value.ToString();
            string LIST_ENTT_NM = CommonCodeListDataGridView.SelectedRows[0].Cells[(int)eCommonCodeListDataGridView.LIST_ENTT_NM].Value.ToString();

            CodeKorNameTextBox.Text = DOMN_KOR_NM;
            CodeEngNameTextBox.Text = DOMN_ENG_NM;
            DataTypeTextBox.Text = DATA_TYPE_CNTS;
            ListEntityNameTextBox.Text = LIST_ENTT_NM;
            CodeDescriptionContentsRichTextBox.Text = CD_DESC_CNTS;
        }

        private void CommonCodeMgmt_Load(object sender, EventArgs e)
        {
            //this.IsMdiContainer = true;   // MDI 폼 선언
            SearchCommonCodeList();
            searchCommonCodeValidValueList();
        }

        // 공통코드목록 입력필드 초기화
        private void resetCommonCodeButton_Click(object sender, EventArgs e)
        {
            CodeKorNameTextBox.Text = "";
            CodeEngNameTextBox.Text = "";
            DataTypeTextBox.Text = "";
            ListEntityNameTextBox.Text = "";
            CodeDescriptionContentsRichTextBox.Text = "";
        }

        // 공통코드저장 버튼 클릭
        private void saveCommonCodeButton_Click(object sender, EventArgs e)
        {
            saveCommonCode();
        }

        // 공통코드기본 테이블 저장
        private void saveCommonCode()
        {
            string DOMN_KOR_NM = CodeKorNameTextBox.Text.Trim();                                // 도메인한글명
            string DOMN_ENG_NM = CodeEngNameTextBox.Text.Trim();                                // 도메인영문명
            string DATA_TYPE_CNTS = DataTypeTextBox.Text.Trim();                                // 데이터타입내용
            string LIST_ENTT_NM = ListEntityNameTextBox.Text.Trim();                            // 목록엔티티명
            string CD_DESC_CNTS = CodeDescriptionContentsRichTextBox.Text.Trim();               // 코드설명내용
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                     // 최초등록자ID

            // 입력값 유효성 검증
            if (CheckRequireItemsForCommonCode() == false)
                return;

            string query = string.Format("CALL InsertCommonCodeItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                DOMN_ENG_NM, DOMN_KOR_NM, LIST_ENTT_NM, DATA_TYPE_CNTS, CD_DESC_CNTS, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);

            if (retVal == -1)
            {
                MessageBox.Show("공통코드기본정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchCommonCodeList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("공통코드기본정보를 저장했습니다.");
                Global.LoadCommonCodeInfo();                // 코드 목록 새로고침
            }
        }

        // 입력값 유효성 검증 (공통코드기본)
        private bool CheckRequireItemsForCommonCode()
        {
            string DOMN_KOR_NM = CodeKorNameTextBox.Text.Trim();
            string DOMN_ENG_NM = CodeEngNameTextBox.Text.Trim();
            string DATA_TYPE_CNTS = DataTypeTextBox.Text.Trim();
            string LIST_ENTT_NM = ListEntityNameTextBox.Text.Trim();
            string CD_DESC_CNTS = CodeDescriptionContentsRichTextBox.Text.Trim();

            if (DOMN_KOR_NM == "")
            {
                MessageBox.Show("코드한글명을 입력하세요.");
                CodeKorNameTextBox.Focus();
                return false;
            }
            if (DOMN_ENG_NM == "")
            {
                MessageBox.Show("코드영문명을 입력하세요.");
                CodeEngNameTextBox.Focus();
                return false;
            }
            if (DATA_TYPE_CNTS == "")
            {
                MessageBox.Show("데이터타입을 입력하세요.");
                DataTypeTextBox.Focus();
                return false;
            }
            if (CD_DESC_CNTS == "")
            {
                MessageBox.Show("코드설명을 입력하세요.");
                CodeDescriptionContentsRichTextBox.Focus();
                return false;
            }

            return true;
        }

        // 공통코드 삭제버튼 클릭
        private void deleteCommonCodeButton_Click(object sender, EventArgs e)
        {
            deleteCommonCode();
        }

        // 공통코드기본 삭제
        private void deleteCommonCode()
        {
            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string DOMN_ENG_NM = CodeEngNameTextBox.Text.Trim();  // 코드영문명(도메인영문명)
            string query = string.Format("CALL DeleteCommonCodeItem ('{0}')", DOMN_ENG_NM);

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("공통코드기본정보를 삭제할 수 없습니다. 공통코드유효값이 존재하는지 확인하세요.");
                return;
            }
            else
            {
                SearchCommonCodeList();
                MessageBox.Show("해당 공통코드기본정보를 삭제했습니다.");
                Global.LoadCommonCodeInfo();            // 공통코드목록 새로고침
            }
        }

        // 공통코드유효값 목록조회
        private void searchCommonCodeValidValueListButton_Click(object sender, EventArgs e)
        {
            searchCommonCodeValidValueList();
        }

        private void searchCommonCodeValidValueList()
        {
            CommonCodeValidValueListDataGrideView.Rows.Clear();

            string DOMN_KOR_NM = SearchCodeKorName1TextBox.Text.Trim();
            string DOMN_ENG_NM = SearchCodeEngName1TextBox.Text.Trim();
            string CD_VLID_VAL = "";
            string CD_VLID_VAL_DESC = "";
            string SCRN_SORT_ORD = "";

            string query = string.Format("CALL SelectCommonCodeValidValueListForMgmt ('{0}', '{1}')", DOMN_ENG_NM, DOMN_KOR_NM);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공통코드유효값 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                DOMN_KOR_NM = datarow["DOMN_KOR_NM"].ToString();
                DOMN_ENG_NM = datarow["DOMN_ENG_NM"].ToString();
                CD_VLID_VAL = datarow["CD_VLID_VAL"].ToString();
                CD_VLID_VAL_DESC = datarow["CD_VLID_VAL_DESC"].ToString();
                SCRN_SORT_ORD = datarow["SCRN_SORT_ORD"].ToString();

                /*
                //---------------------------------------------------------------------------
                // 직위 관련 조회 건일때 복호화 진행
                //---------------------------------------------------------------------------
                if (DOMN_ENG_NM.Equals("PSTN_CD"))
                {
                    CD_VLID_VAL_DESC = EncryptMgt.Decrypt(CD_VLID_VAL_DESC, EncryptMgt.aesEncryptKey);
                }
                */ 

                CommonCodeValidValueListDataGrideView.Rows.Add(DOMN_KOR_NM, DOMN_ENG_NM, CD_VLID_VAL, CD_VLID_VAL_DESC, SCRN_SORT_ORD);
            }

            CommonCodeValidValueListDataGrideView.ClearSelection();
        }

        // 공통코드유효값목록 그리드 행 클릭
        private void CommonCodeValidValueListDataGrideView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CommonCodeValidValueListDataGrideView.SelectedRows.Count == 0)
                return;

            string DOMN_ENG_NM = CommonCodeValidValueListDataGrideView.SelectedRows[0].Cells[(int)eCommonCodeValidValueListDataGrideView.DOMN_ENG_NM].Value.ToString();
            string DOMN_KOR_NM = CommonCodeValidValueListDataGrideView.SelectedRows[0].Cells[(int)eCommonCodeValidValueListDataGrideView.DOMN_KOR_NM].Value.ToString();
            string CD_VLID_VAL = CommonCodeValidValueListDataGrideView.SelectedRows[0].Cells[(int)eCommonCodeValidValueListDataGrideView.CD_VLID_VAL].Value.ToString();
            string CD_VLID_VAL_DESC = CommonCodeValidValueListDataGrideView.SelectedRows[0].Cells[(int)eCommonCodeValidValueListDataGrideView.CD_VLID_VAL_DESC].Value.ToString();
            string SCRN_SORT_ORD = CommonCodeValidValueListDataGrideView.SelectedRows[0].Cells[(int)eCommonCodeValidValueListDataGrideView.SCRN_SORT_ORD].Value.ToString();

            CodeKorName1TextBox.Text = DOMN_KOR_NM;
            CodeEngName1TextBox.Text = DOMN_ENG_NM;
            CodeValidValueTextBox.Text = CD_VLID_VAL;
            CodeValidValueRichTextBox.Text = CD_VLID_VAL_DESC;
            ScreenSortOrderTextBox.Text = SCRN_SORT_ORD;
        }

        // 공통코드유효값 저장
        private void saveCommonCodeValidValueButton_Click(object sender, EventArgs e)
        {
            SaveCommonCodeValidValue();
        }

        // 공통코드유효값내역 INSERT
        private void SaveCommonCodeValidValue()
        {
            string DOMN_ENG_NM = CodeEngName1TextBox.Text.Trim();                               // 도메인영문명
            string CD_VLID_VAL = CodeValidValueTextBox.Text.Trim();                             // 코드유효값
            string CD_VLID_VAL_DESC = CodeValidValueRichTextBox.Text.Trim();                    // 코드유효값설명내용
            string SCRN_SORT_ORD = ScreenSortOrderTextBox.Text.Trim();                          // 화면정렬순서
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                     // 최초등록자ID

            // 입력값 유효성 검증
            if (CheckRequireItemsForCommonCodeValidValue() == false)
                return;

            /*
            //-------------------------------------------------------------------------
            // 직위 관련 Code 일 경우 입력값을 암호화 처리  --> 191024 박현호
            //-------------------------------------------------------------------------
            if (DOMN_ENG_NM.Equals("PSTN_CD"))
            {
                CD_VLID_VAL_DESC = EncryptMgt.Encrypt(CD_VLID_VAL_DESC, EncryptMgt.aesEncryptKey);                
            }
            */

            string query = string.Format("CALL InsertCommonCodeValidVauleItem ('{0}', '{1}', '{2}', '{3}', '{4}')",
                DOMN_ENG_NM, CD_VLID_VAL, SCRN_SORT_ORD, CD_VLID_VAL_DESC, FRST_RGTR_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);

            if (retVal == -1)
            {
                MessageBox.Show("공통코드유효값내역을 저장할 수 없습니다. 공통코드기본정보가 등록되어 있는지 확인하세요.");
                return;
            }
            else
            {
                searchCommonCodeValidValueList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("공통코드유효값내역을 저장했습니다.");
                Global.LoadCommonCodeInfo();            // 공통 코드 목록 새로고침
            }
        }

        // 입력값 유효성 검증 (공통코드유효값내역)
        private bool CheckRequireItemsForCommonCodeValidValue()
        {
            string DOMN_ENG_NM = CodeEngName1TextBox.Text.Trim();                               // 도메인영문명
            string CD_VLID_VAL = CodeValidValueTextBox.Text.Trim();                             // 코드유효값
            string CD_VLID_VAL_DESC = CodeValidValueRichTextBox.Text.Trim();                    // 코드유효값설명내용
            string SCRN_SORT_ORD = ScreenSortOrderTextBox.Text.Trim();                          // 화면정렬순서

            if (DOMN_ENG_NM == "")
            {
                MessageBox.Show("코드영문명을 입력하세요.");
                CodeEngName1TextBox.Focus();
                return false;
            }

            if (CD_VLID_VAL == "")
            {
                MessageBox.Show("코드유효값을 입력하세요.");
                CodeValidValueTextBox.Focus();
                return false;
            }

            if (CD_VLID_VAL_DESC == "")
            {
                MessageBox.Show("코드유효값 설명을 입력하세요.");
                CodeValidValueRichTextBox.Focus();
                return false;
            }

            if (SCRN_SORT_ORD == "")
            {
                MessageBox.Show("회면정렬순서를 입력하세요.");
                ScreenSortOrderTextBox.Focus();
                return false;
            } else
            {
                int numChk = 0;
                bool isNum = int.TryParse(SCRN_SORT_ORD, out numChk);
                if (!isNum)
                {
                    MessageBox.Show("회면정렬순서는 숫자값만 입력하세요.");
                    ScreenSortOrderTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        // 공통코드유효값 입력필드 초기화
        private void resetCommonCodeValidValueButton_Click(object sender, EventArgs e)
        {
            CodeKorName1TextBox.Text = "";
            CodeEngName1TextBox.Text = "";
            CodeValidValueTextBox.Text = "";
            CodeValidValueRichTextBox.Text = "";
            ScreenSortOrderTextBox.Text = "";
        }

        // 공통코드유효값 삭제버튼 클릭
        private void deleteCommonCodeValidValueButton_Click(object sender, EventArgs e)
        {
            deleteCommonCodeValidValue();
        }

        // 공통코드유효값 삭제
        private void deleteCommonCodeValidValue()
        {
            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string DOMN_ENG_NM = CodeEngName1TextBox.Text.Trim();                               // 도메인영문명
            string CD_VLID_VAL = CodeValidValueTextBox.Text.Trim();                             // 코드유효값

            string query = string.Format("CALL DeleteCommonCodeValidValue ('{0}','{1}')", DOMN_ENG_NM, CD_VLID_VAL);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("공통코드유효값내역을 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                searchCommonCodeValidValueList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("선택한 항목을 삭제했습니다.");
                Global.LoadCommonCodeInfo();            // 공통 코드 목록 새로고침
            }
        }

        // 화면정렬순서 필드에 숫자값만 입력되도록 체크
        private void ScreenSortOrderTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        // 코드한글명으로 공통코드기본 목록 조회
        private void SearchCodeKorNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & SearchCodeKorNameTextBox.Text != "")
            {
                SearchCommonCodeList();
            }
        }

        // 코드영문명으로 공통코드기본 목록 조회
        private void SearchCodeEngNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & SearchCodeEngNameTextBox.Text != "")
            {
                SearchCommonCodeList();
            }
        }

        // 코드한글명으로 공통코드유효값내역 목록 조회
        private void SearchCodeKorName1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & SearchCodeKorName1TextBox.Text != "")
            {
                searchCommonCodeValidValueList();
            }
        }

        // 코드영문명으로 공통코드유효값내역 목록 조회
        private void SearchCodeEngName1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & SearchCodeEngName1TextBox.Text != "")
            {
                searchCommonCodeValidValueList();
            }
        }
    }
}
