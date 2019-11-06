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
    public partial class AirportInfoMgt : Form
    {
        enum eAirportListDataGridView
        {
            CITY_CD,              // 도시코드
            CITY_NM,              // 도시명
            ARPT_NM,              // 공항명
            NATN_CD,              // 국가코드
            USE_YN,               // 사용여부
            FRST_RGST_DTM,        // 최초등록일시
            FRST_RGTR_ID,         // 최초등록자ID
            FINL_MDFC_DTM,        // 최종변경일시
            FINL_MDFR_ID          // 최종변경자ID
        };

        public AirportInfoMgt()
        {
            InitializeComponent();
        }

        private void AirportInfoMgt_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchAirportCodeList();            // form load 시 내용 바로 출력
        }

        // 초기화
        private void InitControls()
        {
            string query = "";
            DataSet dataSet;

            // 도시명, 공항명
            SearchCityNameTextBox.Clear();
            SearchAirportNameTextBox.Clear();

            query = "CALL SelectCityCodeList ()";
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("도시코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 도시코드를 콤보에 설정
                string CITY_CD = dataRow["CITY_CD"].ToString();
                //string CITY_NM = dataRow["CITY_NM"].ToString();

                //ComboBoxItem item = new ComboBoxItem(CITY_NM, CITY_CD);
                SearchCityCodeComboBox.Items.Add(CITY_CD);
            }

            SearchCityCodeComboBox.SelectedIndex = -1;

            // 국가코드
            SearchNationCodeComboBox.Items.Clear();
            NationCodeComboBox.Items.Clear();

            query = "CALL SelectNationCodeList('','','')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 국가코드를 콤보에 설정
                string NATN_CD = dataRow["NATN_CD"].ToString();
                string KOR_NATN_NM = dataRow["KOR_NATN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(KOR_NATN_NM, NATN_CD);
                SearchNationCodeComboBox.Items.Add(item);
                NationCodeComboBox.Items.Add(item);
            }

            SearchNationCodeComboBox.SelectedIndex = -1;
            NationCodeComboBox.SelectedIndex = -1;            

            ResetInputFormField();    // 그리드 스타일 초기화
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = AirportListDataGridView;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ColumnHeadersVisible = true;
            //dataGridView.RowTemplate.Resizable = false;
        }

        // 초기화버튼 클릭
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 입력필드 초기화
        private void ResetInputFormField()
        {
            CityCodeTextBox.Text = "";
            CityNameTextBox.Text = "";
            AirportNameTextBox.Text = "";
            NationCodeComboBox.Text = "";
            useYnComboBox.Text = "";

            NationCodeComboBox.Items.Clear();
            useYnComboBox.Items.Clear();            

            string query = "";
            DataSet dataSet;


            query = "CALL SelectNationCodeList('','','')";
            dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("국가코드 정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                // 국가코드를 콤보에 설정
                string NATN_CD = dataRow["NATN_CD"].ToString();
                string KOR_NATN_NM = dataRow["KOR_NATN_NM"].ToString();

                ComboBoxItem item = new ComboBoxItem(KOR_NATN_NM, NATN_CD);
                NationCodeComboBox.Items.Add(item);                
            }            

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
                useYnComboBox.Items.Add(item);
            }

            useYnComboBox.SelectedIndex = -1;
            NationCodeComboBox.SelectedIndex = -1;            

            InitDataGridView();
        }

        private void searchAirportListButton_Click(object sender, EventArgs e)
        {
            SearchAirportCodeList();
        }

        // 공항목록 테이블 조회
        private void SearchAirportCodeList()
        {
            AirportListDataGridView.Rows.Clear();            

            string CITY_CD = SearchCityCodeComboBox.Text.Trim();
            string CITY_NM = SearchCityNameTextBox.Text.Trim();
            string ARPT_NM = SearchAirportNameTextBox.Text.Trim();
            string NATN_CD = Utils.GetSelectedComboBoxItemValue(SearchNationCodeComboBox);
            string KOR_NATN_NM;
            string USE_YN;

            string query = string.Format("CALL SelectAirportList ('{0}', '{1}', '{2}', '{3}')", CITY_CD, CITY_NM, ARPT_NM, NATN_CD);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공항정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                CITY_CD = datarow["CITY_CD"].ToString();
                CITY_NM = datarow["CITY_NM"].ToString();
                ARPT_NM = datarow["ARPT_NM"].ToString();
                KOR_NATN_NM = datarow["KOR_NATN_NM"].ToString();
                USE_YN = datarow["USE_YN"].ToString();

                AirportListDataGridView.Rows.Add(CITY_CD, CITY_NM, ARPT_NM, KOR_NATN_NM, USE_YN);
            }
            AirportListDataGridView.ClearSelection();
        }

        private void AirportListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (AirportListDataGridView.SelectedRows.Count == 0)
                return;

            string CITY_CD = AirportListDataGridView.SelectedRows[0].Cells[(int)eAirportListDataGridView.CITY_CD].Value.ToString();
            string CITY_NM = AirportListDataGridView.SelectedRows[0].Cells[(int)eAirportListDataGridView.CITY_NM].Value.ToString();
            string ARPT_NM = AirportListDataGridView.SelectedRows[0].Cells[(int)eAirportListDataGridView.ARPT_NM].Value.ToString();
            string NATN_CD = AirportListDataGridView.SelectedRows[0].Cells[(int)eAirportListDataGridView.NATN_CD].Value.ToString();
            string USE_YN = AirportListDataGridView.SelectedRows[0].Cells[(int)eAirportListDataGridView.USE_YN].Value.ToString();

            CityCodeTextBox.Text = CITY_CD;
            CityNameTextBox.Text = CITY_NM;
            AirportNameTextBox.Text = ARPT_NM;
            NationCodeComboBox.Text = NATN_CD;
            useYnComboBox.Text = USE_YN;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string CITY_CD = CityCodeTextBox.Text.Trim();                                               // 도시번호

            if (CITY_CD == "")
            {
                MessageBox.Show("도시번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요.");
                return;
            }

            string query = string.Format("CALL DeleteAirportInfo ('{0}')", CITY_CD);
            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("도시번호에 해당하는 공항정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                return;
            }
            else
            {
                SearchAirportCodeList();
                MessageBox.Show("공항정보를 삭제했습니다.");
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string CITY_CD = CityCodeTextBox.Text.Trim();                                      // 도시코드
            string CITY_NM = CityNameTextBox.Text.Trim();                                      // 도시명
            string ARPT_NM = AirportNameTextBox.Text.Trim();                                   // 공항명
            string NATN_CD = Utils.GetSelectedComboBoxItemValue(NationCodeComboBox);           // 국가코드
            string USE_YN = Utils.GetSelectedComboBoxItemValue(useYnComboBox);                 // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                    // 최초등록자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;

            query = string.Format("CALL InsertAirportInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                            CITY_CD, CITY_NM, ARPT_NM, NATN_CD, USE_YN, FRST_RGTR_ID);

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("공항정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchAirportCodeList();                            // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("공항정보를 저장했습니다.");
            }
            ResetInputFormField();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string CITY_CD = CityCodeTextBox.Text.Trim();                                      // 도시코드
            string CITY_NM = CityNameTextBox.Text.Trim();                                      // 도시명
            string ARPT_NM = AirportNameTextBox.Text.Trim();                                   // 공항명
            string NATN_CD = Utils.GetSelectedComboBoxItemValue(NationCodeComboBox);           // 국가코드
            string USE_YN = Utils.GetSelectedComboBoxItemValue(useYnComboBox);                 // 사용여부
            string FRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                    // 최초등록자ID

            if (CITY_CD == "")
            {
                MessageBox.Show("도시코드는 필수 입력항목입니다.");
                CityCodeTextBox.Focus();
                return false;
            }

            if (CITY_NM == "")
            {
                MessageBox.Show("도시명은 필수 입력항목입니다.");
                CityNameTextBox.Focus();
                return false;
            }

            if (ARPT_NM == "")
            {
                MessageBox.Show("공항명은 필수 입력항목입니다.");
                AirportNameTextBox.Focus();
                return false;
            }

            if (NATN_CD == "")
            {
                MessageBox.Show("국가명은 필수 입력항목입니다.");
                NationCodeComboBox.Focus();
                return false;
            }

            if (USE_YN == "")
            {
                MessageBox.Show("사용여부는 필수 입력항목입니다.");
                useYnComboBox.Focus();
                return false;
            }
            return true;
        }

        private void AirportInfoGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = AirportListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && AirportListDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (AirportListDataGridView.SelectedRows.Count == 0)
                return;

            string CITY_CD = AirportListDataGridView.Rows[rowIndex].Cells[(int)eAirportListDataGridView.CITY_CD].Value.ToString();
            string CITY_NM = AirportListDataGridView.Rows[rowIndex].Cells[(int)eAirportListDataGridView.CITY_NM].Value.ToString();
            string ARPT_NM = AirportListDataGridView.Rows[rowIndex].Cells[(int)eAirportListDataGridView.ARPT_NM].Value.ToString();
            string NATN_CD = AirportListDataGridView.Rows[rowIndex].Cells[(int)eAirportListDataGridView.NATN_CD].Value.ToString();
            string USE_YN = AirportListDataGridView.Rows[rowIndex].Cells[(int)eAirportListDataGridView.USE_YN].Value.ToString();

            CityCodeTextBox.Text = CITY_CD;
            CityNameTextBox.Text = CITY_NM;
            AirportNameTextBox.Text = ARPT_NM;
            NationCodeComboBox.Text = NATN_CD;
            useYnComboBox.Text = USE_YN;
        }
    }
}