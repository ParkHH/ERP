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
    public partial class PurchaseListFileInfoMgt : Form
    {
        enum eFileStructureDataGridView { TKTR_CMPN_NO, TKTR_CMPN_NM, PRDT_CNMB, PRDT_NM, DATA_ROW_NO };
        enum eFileStructureDetailDataGridView { TKTR_CMPN_NO, TKTR_CMPN_NM, PRDT_CNMB, PRDT_NM, STD_TRMY_NM, DATA_COL_NO, ALOP_SYNO_NM };

        public PurchaseListFileInfoMgt()
        {
            InitializeComponent();
        }

        private void PurchaseListFileInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            searchFileStructureInfoList();              // form load 시 상단 Grid 내용 바로 출력
            searchFileStructureDetailList();            // form load 시 하단 Grid 내용 바로 출력
        }

        private void InitControls()
        {
            // 그리드 스타일 초기화
            InitDataGridView();

            LoadComboBox();
        }

        //=========================================================================================================================================================================
        // 그리드 스타일 초기화 
        //=========================================================================================================================================================================
        private void InitDataGridView()
        {
            // 엑셀파일구조기본
            //DataGridView dataGridView1 = fileStructureDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            //dataGridView1.RowHeadersVisible = false;

            // 엑셀파일구조내역
            //DataGridView dataGridView2 = fileStructureDetailDataGridView;
            //dataGridView2.RowHeadersVisible = false;
            //dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView2.MultiSelect = false;
            //dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView2.EnableHeadersVisualStyles = false;
            //dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            //dataGridView2.RowHeadersVisible = false;
        }

        //=========================================================================================================================================================================
        // 콤보박스 설정
        //=========================================================================================================================================================================
        private void LoadComboBox()
        {
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 모객매체 콤보박스 설정
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            searchCompany1ComboBox.Items.Clear();
            searchCompany2ComboBox.Items.Clear();
            requestCompany1ComboBox.Items.Clear();
            requestCompany2ComboBox.Items.Clear();

            // 소셜모객업체만 구매자파일을 관리함
            string COOP_CMPN_DVSN_CD = "10";                                                                  // 모객업체_소셜마켓

            string query = string.Format("CALL SelectCoopCmpnList ('{0}', '{1}')", COOP_CMPN_DVSN_CD, ' ');
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("모객업체 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(CMPN_NM, CMPN_NO);

                searchCompany1ComboBox.Items.Add(item);
                searchCompany2ComboBox.Items.Add(item);
                requestCompany1ComboBox.Items.Add(item);
                requestCompany2ComboBox.Items.Add(item);
            }

            searchCompany1ComboBox.SelectedIndex = -1;
            searchCompany2ComboBox.SelectedIndex = -1;
            requestCompany1ComboBox.SelectedIndex = -1;
            requestCompany2ComboBox.SelectedIndex = -1;

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 상품콤보박스 설정
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            searchProduct1ComboBox.Items.Clear();
            searchProduct2ComboBox.Items.Clear();
            requestProduct1ComboBox.Items.Clear();
            requestProduct2ComboBox.Items.Clear();

            query = "CALL SelectPrdtListForPurchaseFileProc ()";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_NM, PRDT_CNMB);

                searchProduct1ComboBox.Items.Add(item);
                searchProduct2ComboBox.Items.Add(item);
                requestProduct1ComboBox.Items.Add(item);
                requestProduct2ComboBox.Items.Add(item);
            }

            searchProduct1ComboBox.SelectedIndex = -1;
            searchProduct2ComboBox.SelectedIndex = -1;
            requestProduct1ComboBox.SelectedIndex = -1;
            requestProduct2ComboBox.SelectedIndex = -1;
        }

        //=========================================================================================================================================================================
        // 엑셀파일기본정보조회 검색버튼 클릭
        //=========================================================================================================================================================================
        private void searchFileStructureInfoListButton_Click(object sender, EventArgs e)
        {
            searchFileStructureInfoList();
        }

        //=========================================================================================================================================================================
        // 엑셀파일기본정보 검색
        //=========================================================================================================================================================================
        private void searchFileStructureInfoList()
        {
            fileStructureDataGridView.Rows.Clear();

            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(searchCompany1ComboBox);
            string TKTR_CMPN_NM = "";
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(searchProduct1ComboBox);
            string PRDT_NM = "";
            string DATA_ROW_NO = "";

            string query = string.Format("CALL SelectExcelFileStructureInfoList ('{0}', '{1}')", TKTR_CMPN_NO, PRDT_CNMB);


            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("엑셀파일구조기본 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                TKTR_CMPN_NO = datarow["TKTR_CMPN_NO"].ToString();
                TKTR_CMPN_NM = datarow["CMPN_NM"].ToString();
                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();
                PRDT_NM = datarow["PRDT_NM"].ToString();
                DATA_ROW_NO = datarow["DATA_ROW_NO"].ToString();

                fileStructureDataGridView.Rows.Add(TKTR_CMPN_NO, TKTR_CMPN_NM, PRDT_CNMB, PRDT_NM, DATA_ROW_NO);
            }

            fileStructureDataGridView.ClearSelection();
        }

        //=========================================================================================================================================================================
        // 파일구조정보 그리드 행 클릭
        //=========================================================================================================================================================================
        private void fileStructureDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (fileStructureDataGridView.SelectedRows.Count == 0)
                return;

            string TKTR_CMPN_NO = fileStructureDataGridView.SelectedRows[0].Cells[(int)eFileStructureDataGridView.TKTR_CMPN_NO].Value.ToString();
            string TKTR_CMPN_NM = fileStructureDataGridView.SelectedRows[0].Cells[(int)eFileStructureDataGridView.TKTR_CMPN_NM].Value.ToString();
            string PRDT_CNMB = fileStructureDataGridView.SelectedRows[0].Cells[(int)eFileStructureDataGridView.PRDT_CNMB].Value.ToString();
            string PRDT_NM = fileStructureDataGridView.SelectedRows[0].Cells[(int)eFileStructureDataGridView.PRDT_NM].Value.ToString();
            string DATA_ROW_NO = fileStructureDataGridView.SelectedRows[0].Cells[(int)eFileStructureDataGridView.DATA_ROW_NO].Value.ToString();

            Utils.SelectComboBoxItemByValue(requestCompany1ComboBox, TKTR_CMPN_NO);                      // 모객업체 콤보박스
            Utils.SelectComboBoxItemByValue(requestProduct1ComboBox, PRDT_CNMB);                      // 상품 콤보박스

            requestStartRowNumberTextBox.Text = DATA_ROW_NO;
        }

        //=========================================================================================================================================================================
        // 파일구조정보 저장버튼 클릭
        //=========================================================================================================================================================================
        private void saveFileStructureButton_Click(object sender, EventArgs e)
        {
            if (CheckRequireItemsForFileStructureInfo() == false) return;

            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(requestCompany1ComboBox);
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(requestProduct1ComboBox);
            string DATA_ROW_NO = requestStartRowNumberTextBox.Text.Trim();

            string query = string.Format("CALL InsertExcelFileStructureItem ('{0}', '{1}', '{2}', '{3}')",
                TKTR_CMPN_NO, PRDT_CNMB, DATA_ROW_NO, Global.loginInfo.ACNT_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);

            if (retVal == -1)
            {
                MessageBox.Show("엑셀파일구조기본정보를 저장할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                resetFileStructureDetailInputField();
                searchFileStructureInfoList();                               // 등록 후 그리드를 최신상태로 Refresh
                //MessageBox.Show("엑셀파일구조기본정보를 저장했습니다.");
            }
        }

        //=========================================================================================================================================================================
        // 엑셀파일기본정보 입력필드 유효성 검증
        //=========================================================================================================================================================================
        private bool CheckRequireItemsForFileStructureInfo()
        {
            if (requestCompany1ComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("모객업체를 선택하세요.");
                requestCompany1ComboBox.Focus();
                return false;
            }

            if (requestProduct1ComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품을 선택하세요.");
                requestProduct1ComboBox.Focus();
                return false;
            }

            if (requestStartRowNumberTextBox.Text.Trim() == "") requestStartRowNumberTextBox.Text = "0";
            string DATA_ROW_NO = requestStartRowNumberTextBox.Text.Trim();

            if (DATA_ROW_NO == "" || DATA_ROW_NO == "0")
            {
                MessageBox.Show("시작행위치는 필수 입력항목입니다.");
                requestStartRowNumberTextBox.Focus();
                return false;
            }
            return true;
        }

        //=========================================================================================================================================================================
        // 엑셀파일기본정보 입력필드 초기화 버튼 클릭
        //=========================================================================================================================================================================
        private void resetFileStuctureButton_Click(object sender, EventArgs e)
        {
            resetFileStructureInputField();
        }

        //=========================================================================================================================================================================
        // 엑셀파일기본정보 입력필드 초기화
        //=========================================================================================================================================================================
        private void resetFileStructureInputField()
        {
            requestCompany1ComboBox.Text = "";
            requestProduct1ComboBox.Text = "";
            requestStartRowNumberTextBox.Text = "";
        }

        //=========================================================================================================================================================================
        // 엑셀파일기본정보 삭제버튼 클릭
        //=========================================================================================================================================================================
        private void deleteFileStructureButton_Click(object sender, EventArgs e)
        {
            if (CheckRequireItemsForFileStructureInfo() == false) return;

            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(requestCompany1ComboBox);
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(requestProduct1ComboBox);

            string query = string.Format("CALL DeleteExcelFileStructureItem ('{0}', '{1}')", TKTR_CMPN_NO, PRDT_CNMB);
            int retVal = DbHelper.ExecuteNonQuery(query);

            if (retVal == -1)
            {
                MessageBox.Show("엑셀파일구조기본정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                resetFileStructureDetailInputField();
                searchFileStructureInfoList();                               // 등록 후 그리드를 최신상태로 Refresh
                //MessageBox.Show("엑셀파일구조기본정보를 삭제했습니다.");
            }
        }

        //=========================================================================================================================================================================
        // 폼닫기
        //=========================================================================================================================================================================
        private void closeFileStructureButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //=========================================================================================================================================================================
        // 엑셀파일상세내역조회 검색버튼 클릭
        //=========================================================================================================================================================================
        private void searchFileStructureDetailListButton_Click(object sender, EventArgs e)
        {
            searchFileStructureDetailList();
        }

        //=========================================================================================================================================================================
        // 엑셀파일상세내역조회
        //=========================================================================================================================================================================
        private void searchFileStructureDetailList()
        {
            fileStructureDetailDataGridView.Rows.Clear();

            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(searchCompany2ComboBox);
            string TKTR_CMPN_NM = "";
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(searchProduct2ComboBox);
            string PRDT_NM = "";
            string STD_TRMY_NM = searchStandardTermTextBox.Text.Trim();
            string DATA_COL_NO = "";
            string ALOP_SYNO_NM = "";

            string query = string.Format("CALL SelectExcelFileStructureDetailInfoList ('{0}', '{1}', '{2}')", TKTR_CMPN_NO, PRDT_CNMB, STD_TRMY_NM);


            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("엑셀파일구조기본 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                TKTR_CMPN_NO = datarow["TKTR_CMPN_NO"].ToString();
                TKTR_CMPN_NM = datarow["CMPN_NM"].ToString();
                PRDT_CNMB = datarow["PRDT_CNMB"].ToString();
                PRDT_NM = datarow["PRDT_NM"].ToString();
                STD_TRMY_NM = datarow["STD_TRMY_NM"].ToString();
                DATA_COL_NO = datarow["DATA_COL_NO"].ToString();
                ALOP_SYNO_NM = datarow["ALOP_SYNO_NM"].ToString();

                fileStructureDetailDataGridView.Rows.Add(TKTR_CMPN_NO, TKTR_CMPN_NM, PRDT_CNMB, PRDT_NM, STD_TRMY_NM, DATA_COL_NO, ALOP_SYNO_NM);
            }

            fileStructureDetailDataGridView.ClearSelection();
        }

        //=========================================================================================================================================================================
        // 엑셀파일상세정보 입력필드 초기화 버튼 클릭
        //=========================================================================================================================================================================
        private void resetFileStructureDetailButton_Click(object sender, EventArgs e)
        {
            resetFileStructureDetailInputField();
        }

        //=========================================================================================================================================================================
        // 엑셀파일상세정보 입력필드 초기화
        //=========================================================================================================================================================================
        private void resetFileStructureDetailInputField()
        {
            //requestCompany2ComboBox.Text = "";
            //requestProduct2ComboBox.Text = "";
            requestStandardTermTextBox.Text = "";
            requestDataColNumberTextBox.Text = "";
            requestSynonymTextBox.Text = "";
        }

        //=========================================================================================================================================================================
        // 파일구조상세정보 저장버튼 클릭
        //=========================================================================================================================================================================
        private void saveFileStructureDetailButton_Click(object sender, EventArgs e)
        {
            if (CheckRequireItemsForFileStructureDetailInfo() == false) return;

            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(requestCompany2ComboBox);
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(requestProduct2ComboBox);
            string STD_TRMY_NM = requestStandardTermTextBox.Text.Trim();
            string DATA_COL_NO = requestDataColNumberTextBox.Text.Trim();
            string ALOP_SYNO_NM = requestSynonymTextBox.Text.Trim();

            string query = string.Format("CALL InsertExcelFileStructureDetailItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                TKTR_CMPN_NO, PRDT_CNMB, STD_TRMY_NM, DATA_COL_NO, ALOP_SYNO_NM, Global.loginInfo.ACNT_ID);
            int retVal = DbHelper.ExecuteNonQuery(query);

            if (retVal == -1)
            {
                MessageBox.Show("엑셀파일구조상세정보를 저장할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                resetFileStructureDetailInputField();
                searchFileStructureDetailList();
                //MessageBox.Show("엑셀파일구조상세정보를 저장했습니다.");
            }
        }

        //=========================================================================================================================================================================
        // 엑셀파일상세정보 입력필드 유효성 검증
        //=========================================================================================================================================================================
        private bool CheckRequireItemsForFileStructureDetailInfo()
        {
            if (requestCompany2ComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("모객업체를 선택하세요.");
                requestCompany2ComboBox.Focus();
                return false;
            }

            if (requestProduct2ComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("상품을 선택하세요.");
                requestProduct1ComboBox.Focus();
                return false;
            }

            string STD_TRMY_NM = requestStandardTermTextBox.Text.Trim();
            if (STD_TRMY_NM == "")
            {
                MessageBox.Show("표준용어는 필수 입력항목입니다.");
                requestStandardTermTextBox.Focus();
                return false;
            }

            if (requestDataColNumberTextBox.Text.Trim() == "") requestDataColNumberTextBox.Text = "0";
            string DATA_COL_NO = requestDataColNumberTextBox.Text.Trim();

            if (DATA_COL_NO == "" || DATA_COL_NO == "0")
            {
                MessageBox.Show("열위치는 필수 입력항목입니다.");
                requestDataColNumberTextBox.Focus();
                return false;
            }

            string ALOP_SYNO_NM = requestSynonymTextBox.Text.Trim();
            if (ALOP_SYNO_NM == "")
            {
                MessageBox.Show("이음동의어는 필수 입력항목입니다.");
                requestSynonymTextBox.Focus();
                return false;
            }

            return true;
        }

        //=========================================================================================================================================================================
        // 엑셀파일상세정보 삭제버튼 클릭
        //=========================================================================================================================================================================
        private void deleteFileStructureDetailButton_Click(object sender, EventArgs e)
        {
            if (CheckRequireItemsForFileStructureDetailInfo() == false) return;

            string TKTR_CMPN_NO = Utils.GetSelectedComboBoxItemValue(requestCompany2ComboBox);
            string PRDT_CNMB = Utils.GetSelectedComboBoxItemValue(requestProduct2ComboBox);
            string STD_TRMY_NM = requestStandardTermTextBox.Text.Trim();

            string query = string.Format("CALL DeleteExcelFileStructureDetailItem ('{0}', '{1}', '{2}')", TKTR_CMPN_NO, PRDT_CNMB, STD_TRMY_NM);
            int retVal = DbHelper.ExecuteNonQuery(query);

            if (retVal == -1)
            {
                MessageBox.Show("엑셀파일구조상세정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                resetFileStructureDetailInputField();
                searchFileStructureInfoList();                               // 등록 후 그리드를 최신상태로 Refresh
                //MessageBox.Show("엑셀파일구조상세정보를 삭제했습니다.");
            }

        }

        //=========================================================================================================================================================================
        // 파일구조상세정보 그리드 행 클릭
        //=========================================================================================================================================================================
        private void fileStructureDetailDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (fileStructureDetailDataGridView.SelectedRows.Count == 0)
                return;

            string TKTR_CMPN_NO = fileStructureDetailDataGridView.SelectedRows[0].Cells[(int)eFileStructureDetailDataGridView.TKTR_CMPN_NO].Value.ToString();
            string TKTR_CMPN_NM = fileStructureDetailDataGridView.SelectedRows[0].Cells[(int)eFileStructureDetailDataGridView.TKTR_CMPN_NM].Value.ToString();
            string PRDT_CNMB = fileStructureDetailDataGridView.SelectedRows[0].Cells[(int)eFileStructureDetailDataGridView.PRDT_CNMB].Value.ToString();
            string PRDT_NM = fileStructureDetailDataGridView.SelectedRows[0].Cells[(int)eFileStructureDetailDataGridView.PRDT_NM].Value.ToString();
            string STD_TRMY_NM = fileStructureDetailDataGridView.SelectedRows[0].Cells[(int)eFileStructureDetailDataGridView.STD_TRMY_NM].Value.ToString();
            string DATA_COL_NO = fileStructureDetailDataGridView.SelectedRows[0].Cells[(int)eFileStructureDetailDataGridView.DATA_COL_NO].Value.ToString();
            string ALOP_SYNO_NM = fileStructureDetailDataGridView.SelectedRows[0].Cells[(int)eFileStructureDetailDataGridView.ALOP_SYNO_NM].Value.ToString();

            Utils.SelectComboBoxItemByValue(requestCompany2ComboBox, TKTR_CMPN_NO);                      // 모객업체 콤보박스
            Utils.SelectComboBoxItemByValue(requestProduct2ComboBox, PRDT_CNMB);                         // 상품 콤보박스

            requestStandardTermTextBox.Text = STD_TRMY_NM;
            requestDataColNumberTextBox.Text = DATA_COL_NO;
            requestSynonymTextBox.Text = ALOP_SYNO_NM;
        }
    }
}
