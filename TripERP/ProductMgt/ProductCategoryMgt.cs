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

namespace TripERP.ProductMgt
{
    public partial class ProductCategoryMgt : Form
    {
        enum eProductCategoryListDataGridView { PRDT_CTGR_CD, UPPR_PRDT_CTGR_CD, LOWR_PRDT_CTGR_CD, PRDT_CTGR_NM, PRDT_DESC_CNTS, USE_YN, USE_YN_NM, LWRK_CTGR_YN, LWRK_CTGR_YN_NM};

        public ProductCategoryMgt()
        {
            InitializeComponent();
        }

        // 상품카테고리목록 조회
        private void searchProductCategoryListButton_Click(object sender, EventArgs e)
        {
            SearchProductCategoryList();
        }

        // 상품카테고리목록 조회
        private void SearchProductCategoryList()
        {
            // 그리드 초기화
            ProductCategoryListDataGridView.Rows.Clear();

            string PRDT_CTGR_CD =  Utils.GetSelectedComboBoxItemValue(SearchProductCategoryCodeComboBox);  // 상품카테고리코드
            string UPPR_PRDT_CTGR_CD = "";
            string LOWR_PRDT_CTGR_CD = "";
            string PRDT_CTGR_NM = "";
            string PRDT_DESC_CNTS = "";
            string USE_YN = "";
            string USE_YN_NM = "";
            string LWRK_CTGR_YN = "";
            string LWRK_CTGR_YN_NM = "";

            string query = string.Format("CALL SelectProductCategoryCodeList ('{0}')", PRDT_CTGR_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품카테고리정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datalow in dataSet.Tables[0].Rows)
            {
                PRDT_CTGR_CD = datalow["PRDT_CTGR_CD"].ToString();
                UPPR_PRDT_CTGR_CD = datalow["UPPR_PRDT_CTGR_CD"].ToString();
                LOWR_PRDT_CTGR_CD = datalow["LOWR_PRDT_CTGR_CD"].ToString();
                PRDT_CTGR_NM = datalow["PRDT_CTGR_NM"].ToString();
                PRDT_DESC_CNTS = datalow["PRDT_DESC_CNTS"].ToString();
                USE_YN = datalow["USE_YN"].ToString();
                USE_YN_NM = datalow["USE_YN_NM"].ToString();
                LWRK_CTGR_YN = datalow["LWRK_CTGR_YN"].ToString();
                LWRK_CTGR_YN_NM = datalow["LWRK_CTGR_YN_NM"].ToString();

                ProductCategoryListDataGridView.Rows.Add(PRDT_CTGR_CD, UPPR_PRDT_CTGR_CD, LOWR_PRDT_CTGR_CD, PRDT_CTGR_NM, PRDT_DESC_CNTS, USE_YN, USE_YN_NM, LWRK_CTGR_YN, LWRK_CTGR_YN_NM);
            }

            ProductCategoryListDataGridView.ClearSelection();
            clearForm();                // 입력 Component 초기화
        }

        // 폼 닫기
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 폼 초기화
        private void resetButton_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            ProductCategoryCodeTextBox.Enabled = true;
            ProductCategoryCodeTextBox.Text = "";
            UpperCategoryCodeTextBox.Text = "";
            LowerCategoryCodeTextBox.Text = "";
            ProductCategoryNameTextBox.Text = "";
            ProductCategoryDescriptionTextBox.Text = "";
            LowestCategoryYNComboBox.SelectedIndex = 0;
            UseYnComboBox.SelectedIndex = 0;            
        }

        // 상품카테고리기본 저장
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveProductCategoryCode();
        }

        private void SaveProductCategoryCode()
        {
            string PRDT_CTGR_CD = ProductCategoryCodeTextBox.Text.Trim();                       // 상품카테고리코드
            string UPPR_PRDT_CTGR_CD = UpperCategoryCodeTextBox.Text.Trim();                    // 상위상품카테고리코드
            string LOWR_PRDT_CTGR_CD = LowerCategoryCodeTextBox.Text.Trim(); ;                  // 하위상품카테고리코드
            string PRDT_CTGR_NM = ProductCategoryNameTextBox.Text.Trim(); ;                     // 상품카테고리명
            string PRDT_DESC_CNTS = ProductCategoryDescriptionTextBox.Text.Trim(); ;            // 상품카테고리설명내용
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                  // 사용여부
            string LWRK_CTGR_YN = Utils.GetSelectedComboBoxItemValue(LowestCategoryYNComboBox); // 최하위여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                     // 최초등록자ID
            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false)
                return;

            // 만약 하위 카테고리 코드가 입력되지 않았다면 상품카테고리명을 따르도록 설정        --> 초기 카테고리 설정 상태 반영 (191106 - 박현호)
            if (LOWR_PRDT_CTGR_CD.Equals(""))
            {
                LOWR_PRDT_CTGR_CD = PRDT_CTGR_NM;
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------
            // Data 를 Insert 하는것인지 Update 하는것인지 구분...
            //      1. GridView 에서 선택한 Row 가 있다면 해당 Row 의 Data 를 수정하는것
            //      2. 선택한 Row 가 없다면 새로운 Data 를 Insert 하는것... 카테고리 코드가 PK 이므로 Exception 방지를 위해 중복 CHECK  진행 후 Insert 처리
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------
            if (ProductCategoryListDataGridView.SelectedRows.Count != 0)
            {               
                query = string.Format("CALL UpdateProductCategoryCode('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", 
                    PRDT_CTGR_CD, UPPR_PRDT_CTGR_CD, LOWR_PRDT_CTGR_CD, PRDT_CTGR_NM, PRDT_DESC_CNTS, USE_YN, LWRK_CTGR_YN, FRST_RGTR_ID);
            }
            else
            {
                // PK 중복 여부 확인
                string confirmQuery = "SELECT COUNT(*) AS COUNT FROM TB_PRDT_CTGR_M WHERE PRDT_CTGR_CD=" + PRDT_CTGR_CD;
                DataSet ds = DbHelper.SelectQuery(confirmQuery);
                DataRow row = ds.Tables[0].Rows[0];
                int COUNT = int.Parse(row["COUNT"].ToString().Trim());
                if (COUNT > 0)
                {
                    MessageBox.Show("이미 등록된 상품코드입니다.");
                    return;
                }
                else
                {                    
                    MessageBox.Show("상품 코드 '"+ PRDT_CTGR_CD+"' 의\n상위코드 '"+ UPPR_PRDT_CTGR_CD + "'\n하위코드 '"+ LOWR_PRDT_CTGR_CD+"' 로 상품 카테고리를 등록합니다.");
                }
              
                query = string.Format("CALL InsertProductCategoryCode ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    PRDT_CTGR_CD, UPPR_PRDT_CTGR_CD, LOWR_PRDT_CTGR_CD, PRDT_CTGR_NM, PRDT_DESC_CNTS, USE_YN, LWRK_CTGR_YN, FRST_RGTR_ID);
            }
           
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("상품카테고리정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchProductCategoryList();    // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("상품카테고리정보를 저장했습니다.");                
            }
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string PRDT_CTGR_CD = ProductCategoryCodeTextBox.Text.Trim();                       // 상품카테고리코드
            string UPPR_PRDT_CTGR_CD = UpperCategoryCodeTextBox.Text.Trim();                    // 상위상품카테고리코드
            string LOWR_PRDT_CTGR_CD = ProductCategoryNameTextBox.Text.Trim(); ;                  // 하위상품카테고리코드
            string PRDT_CTGR_NM = ProductCategoryNameTextBox.Text.Trim(); ;                     // 상품카테고리명
            string PRDT_DESC_CNTS = ProductCategoryDescriptionTextBox.Text.Trim(); ;            // 상품카테고리설명내용
            string USE_YN = Utils.GetSelectedComboBoxItemValue(UseYnComboBox);                  // 사용여부

            if (PRDT_CTGR_CD == "")
            {
                MessageBox.Show("상품카테고리코드는 필수 입력항목입니다.");
                ProductCategoryCodeTextBox.Focus();
                return false;
            }
            /*
            if (UPPR_PRDT_CTGR_CD == "")
            {
                MessageBox.Show("상위카테고리코드를 입력하지 않았습니다.");
                UpperCategoryCodeTextBox.Focus();
                return false;
            }
            */
            /*
            if (LOWR_PRDT_CTGR_CD == "")
            {
                MessageBox.Show("하위카테고리코드를 입력하지 않았습니다.");
                ProductCategoryNameTextBox.Focus();
                return false;
            }
            */

            if (UPPR_PRDT_CTGR_CD == LOWR_PRDT_CTGR_CD)
            {
                MessageBox.Show("하위카테고리코드는 상위카테고리와 같을 수 없습니다. 최상위카테고리인 경우 하위카테고리는 지정하지 마십시오.");
                LowerCategoryCodeTextBox.Focus();
                return false;
            }

            if (ProductCategoryCodeTextBox.Text == UpperCategoryCodeTextBox.Text)
            {
                MessageBox.Show("상품카테고리코드가 상위카테고리와 동일합니다. 상위카테고리보다 작은 값을 입력하십시오.");
                ProductCategoryCodeTextBox.Focus();
                return false;
            }

            if (ProductCategoryCodeTextBox.Text == LowerCategoryCodeTextBox.Text)
            {
                MessageBox.Show("상품카테고리코드가 하위카테고리와 동일합니다. 상위카테고리보다 큰 값을 입력하십시오.");
                ProductCategoryCodeTextBox.Focus();
                return false;
            }

            if (PRDT_CTGR_NM == "")
            {
                MessageBox.Show("상품카테고리명을 입력하지 않았습니다.");
                ProductCategoryNameTextBox.Focus();
                return false;
            }

            if (PRDT_DESC_CNTS == "")
            {
                MessageBox.Show("상품카테고리 설명을 입력하지 않았습니다.");
                ProductCategoryDescriptionTextBox.Focus();
                return false;
            }

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
                UseYnComboBox.Focus();
                return false;
            }

            return true;
        }

        private void ProductCategoryMgt_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            InitControls();                                    // 폼 로딩 초기화
            SearchProductCategoryList();            // Form Load 시 내용 바로검색            
        }

        private void InitControls()
        {
            // 그리드 스타일 초기화
            InitDataGridView();

            // 검색용 상품카테고리코드
            SearchProductCategoryCodeComboBox.Items.Clear();

            string query = "CALL SelectAllProductCategoryCodeList()";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("상품카테고리목록 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 업체번호와 업체명을 콤보에 설정
                string PRDT_CTGR_CD = dataRow["PRDT_CTGR_CD"].ToString();
                string PRDT_CTGR_NM = dataRow["PRDT_CTGR_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(PRDT_CTGR_NM, PRDT_CTGR_CD);
                SearchProductCategoryCodeComboBox.Items.Add(item);
            }

            SearchProductCategoryCodeComboBox.SelectedIndex = -1;

            // 사용여부 콤보박스
            UseYnComboBox.Items.Clear();

            query = "CALL SelectCommoncodeList ('YN')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용여부 공통코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 공통코드 테이블에서 여부 속성의 유효값을 검색하여 콤보에 설정
                string USE_YN = dataRow["CD_VLID_VAL"].ToString();
                string USE_YN_NM = dataRow["CD_VLID_VAL_DESC"].ToString();

                ComboBoxItem item = new ComboBoxItem(USE_YN_NM, USE_YN);
                UseYnComboBox.Items.Add(item);
                LowestCategoryYNComboBox.Items.Add(item);
            }

            LowestCategoryYNComboBox.SelectedIndex = 0;                         // 최하위여부Combobox index 초기화
            UseYnComboBox.SelectedIndex = 0;                                            // 상품카테고리 사용여부 Combobox index 초기화
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView1 = ProductCategoryListDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersVisible = false;
            dataGridView1.DoubleBuffered(true);
        }
        
        // 그리드 행 클릭
        private void ProductCategoryListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProductCategoryListDataGridView.SelectedRows.Count == 0)
                return;

            // 그리드 행을 클릭했을시에는 상품카테고리에 대한 정보를 조회 수정, 삭제를 하는데 필요한 key 값인 상품카테고리코드를 enabled=false 로 처리
            ProductCategoryCodeTextBox.Enabled = false;

            string PRDT_CTGR_CD = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.PRDT_CTGR_CD].Value.ToString();
            string UPPR_PRDT_CTGR_CD = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.UPPR_PRDT_CTGR_CD].Value.ToString();
            string LOWR_PRDT_CTGR_CD = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.LOWR_PRDT_CTGR_CD].Value.ToString();
            string PRDT_CTGR_NM = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.PRDT_CTGR_NM].Value.ToString();
            string PRDT_DESC_CNTS = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.PRDT_DESC_CNTS].Value.ToString();
            string USE_YN = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.USE_YN].Value.ToString();
            string USE_YN_NM = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.USE_YN_NM].Value.ToString();
            string LWRK_CTGR_YN = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.LWRK_CTGR_YN].Value.ToString();
            string LWRK_CTGR_YN_NM = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.LWRK_CTGR_YN_NM].Value.ToString();

            ProductCategoryCodeTextBox.Text = PRDT_CTGR_CD;
            UpperCategoryCodeTextBox.Text = UPPR_PRDT_CTGR_CD;
            LowerCategoryCodeTextBox.Text = LOWR_PRDT_CTGR_CD;            
            ProductCategoryNameTextBox.Text = PRDT_CTGR_NM;
            ProductCategoryDescriptionTextBox.Text = PRDT_DESC_CNTS;

            Utils.SelectComboBoxItemByValue(UseYnComboBox, USE_YN);
            Utils.SelectComboBoxItemByValue(LowestCategoryYNComboBox, LWRK_CTGR_YN);
        }

        // 상품카테고리코드 삭제
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string PRDT_CTGR_CD = ProductCategoryCodeTextBox.Text.Trim();

            if (PRDT_CTGR_CD == "")
            {
                MessageBox.Show("상품카테고리코드는 필수 입력항목입니다.");
                ProductCategoryCodeTextBox.Focus();
                return;
            }

            // 해당 카테코리코드를 사용하는 상품이 존재할 경우 삭제 방지 처리
            if (checkThisCategoryCodeIsUsed() == true)
            {
                MessageBox.Show("삭제하려는 카테고리코드는 상품에서 사용 중이어서 삭제할 수 없습니다. \r\n 삭제가 필요한 경우, 상품상세정보와 상품기본정보를 먼저 삭제하셔야 합니다.");
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string query = string.Format("CALL DeleteProductCategoryCode ('{0}')", PRDT_CTGR_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("상품카테고리코드를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchProductCategoryList();
                MessageBox.Show("선택한 항목을 삭제했습니다.");                
            }
        }

        // 상품카테고리코드를 사용 중인 상품기본 테이블이 존재할 경우 에러 처리
        private bool checkThisCategoryCodeIsUsed()
        {
            string PRDT_CTGR_CD = ProductCategoryCodeTextBox.Text.Trim();
            string query = string.Format("CALL SelectProductListByCategoryCode('{0}')", PRDT_CTGR_CD);

            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            if(dataRowList.Count > 0)
                return true;
            else
                return false;
        }

        private void ProductCatgoryGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = ProductCategoryListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && ProductCategoryListDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (ProductCategoryListDataGridView.SelectedRows.Count == 0)
                return;

            string PRDT_CTGR_CD = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.PRDT_CTGR_CD].Value.ToString();
            string UPPR_PRDT_CTGR_CD = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.UPPR_PRDT_CTGR_CD].Value.ToString();
            string LOWR_PRDT_CTGR_CD = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.LOWR_PRDT_CTGR_CD].Value.ToString();
            string PRDT_CTGR_NM = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.PRDT_CTGR_NM].Value.ToString();
            string PRDT_DESC_CNTS = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.PRDT_DESC_CNTS].Value.ToString();
            string USE_YN = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.USE_YN].Value.ToString();
            string USE_YN_NM = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.USE_YN_NM].Value.ToString();
            string LWRK_CTGR_YN = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.LWRK_CTGR_YN].Value.ToString();
            string LWRK_CTGR_YN_NM = ProductCategoryListDataGridView.SelectedRows[0].Cells[(int)eProductCategoryListDataGridView.LWRK_CTGR_YN_NM].Value.ToString();

            ProductCategoryCodeTextBox.Text = PRDT_CTGR_CD;
            UpperCategoryCodeTextBox.Text = UPPR_PRDT_CTGR_CD;
            ProductCategoryNameTextBox.Text = LOWR_PRDT_CTGR_CD;
            ProductCategoryNameTextBox.Text = PRDT_CTGR_NM;
            ProductCategoryDescriptionTextBox.Text = PRDT_DESC_CNTS;

            Utils.SelectComboBoxItemByValue(UseYnComboBox, USE_YN);
            Utils.SelectComboBoxItemByValue(LowestCategoryYNComboBox, LWRK_CTGR_YN);
        }



        //=======================================================================================
        // ESC KEY 누를시 초기화 버튼 동작                --> 191106 박현호
        //=======================================================================================
        private void ProductCategoryMgt_KeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = e.KeyCode.ToString();
            keyDownAction(keyCode);
        }

        //---------------------------------------------------------------------------
        // Key 눌렀을때 KeyCode 값에 따라 동작 결정     --> 191106 박현호
        //---------------------------------------------------------------------------
        private void keyDownAction(string keyCode)
        {
            if (keyCode.Equals(Keys.Escape.ToString()))
            {
                clearForm();
                ProductCategoryListDataGridView.ClearSelection();
            }
        }
    }
}