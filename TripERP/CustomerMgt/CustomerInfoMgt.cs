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
    public partial class CustomerInfoMgt : Form
    {
        public string VoCUST_NO { get; set; }
        public string VoCUST_NM { get; set; }
        public string VoCUST_ENG_NM { get; set; }
        public string VoSEX_DVSN_CD { get; set; }
        public string VoSEX_DVSN_NM { get; set; }
        public string VoBIRTH { get; set; }
        public string VoPRSN_CORP_DVSN_CD { get; set; }
        public string VoPRSN_CORP_DVSN_NM { get; set; }
        public string VoCELL_PHNE_NO { get; set; }
        public string VoOFFC_ADDR { get; set; }
        public string VoHOME_ADDR { get; set; }
        public string VoCUST_MEMO_CNTS { get; set; }
        public string VoOFFC_DETAIL_ADDR { get; set; }
        public string VoHOME_DETAIL_ADDR { get; set; }
        public string VoEMAL_ADDR { get; set; }
        public string VoOFFC_POST_NO { get; set; }
        public string VoHOME_POST_NO { get; set; }
        public string VoFRST_RGTR_ID { get; set; }
        public string emailDomainAddress;

        enum eCustomerListDataGridView
        {
            CUST_NO,              // 고객번호
            CUST_NM,              // 고객명
            CUST_ENG_NM,          // 고객영문명
            PRSN_CORP_DVSN_CD,    // 고객구분
            PRSN_CORP_DVSN_NM,    // 고객구분명
            CELL_PHNE_NO,         // 휴대전화번호
            OFFC_ADDR,            // 직장주소
            HOME_ADDR,            // 자택주소
            CUST_MEMO_CNTS,       // 고객메모내용
            OFFC_DETAIL_ADDR,     // 직장세부주소
            HOME_DETAIL_ADDR,     // 자택세부주소
            EMAL_ADDR,            // 이메일주소
            OFFC_POST_NO,         // 직장우편번호       
            HOME_POST_NO,         // 자택우편번호                 
            BIRTH,                // 생년월일
            SEX_DVSN_CD,          // 성별구분코드
            SEX_DVSN_NM           // 성별
        };

        public CustomerInfoMgt()
        {
            InitializeComponent();
        }

        // 폼 로딩 초기화
        private void Form1_Load(object sender, EventArgs e)
        {
            InitControls();
            SearchCustomerList();               // form load 시 내용 바로 출력
        }

        //======================================================================================================================================================================
        // 초기화
        //======================================================================================================================================================================
        private void InitControls()
        {
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // VO 초기화
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            resetVO();

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 공통코드 콤보박스 초기화
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            loadCommonCodeItems();

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 폼 입력필드 초기화
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            ResetInputFormField();

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 그리드 스타일 초기화
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------
            InitDataGridView();
        }


        //=========================================================================================================================================================================
        // 각종 콤보박스 초기값 로드
        //=========================================================================================================================================================================
        private void loadCommonCodeItems()
        {
            string[] groupNameArray = { "EMAL_DOMN_ADDR", "PRSN_CORP_DVSN_CD", "SEX_DVSN_CD" };

            ComboBox[] comboBoxArray = { emailAddr2ComboBox, customerDvsnComboBox, passengerSexDivisionComboBox };

            for (int gi = 0; gi < groupNameArray.Length; gi++)
            {
                comboBoxArray[gi].Items.Clear();

                List<CommonCodeItem> list = Global.GetCommonCodeList(groupNameArray[gi]);

                //comboBoxArray[i].Items.Add(new ComboBoxItem("전체", ""));
                for (int li = 0; li < list.Count; li++)
                {
                    string value = list[li].Value.ToString();
                    string desc = list[li].Desc;

                    ComboBoxItem item = new ComboBoxItem(desc, value);

                    comboBoxArray[gi].Items.Add(item);
                }

                if (comboBoxArray[gi].Items.Count > 0) comboBoxArray[gi].SelectedIndex = -1;
            }
        }

        private void resetVO()
        {
            VoCUST_NO = "";
            VoCUST_NM = "";
            VoCUST_ENG_NM = "";
            VoSEX_DVSN_CD = "";
            VoSEX_DVSN_NM = "";
            VoBIRTH = "";
            VoPRSN_CORP_DVSN_CD = "";
            VoPRSN_CORP_DVSN_NM = "";
            VoCELL_PHNE_NO = "";
            VoOFFC_ADDR = "";
            VoHOME_ADDR = "";
            VoCUST_MEMO_CNTS = "";
            VoOFFC_DETAIL_ADDR = "";
            VoHOME_DETAIL_ADDR = "";
            VoEMAL_ADDR = "";
            VoOFFC_POST_NO = "";
            VoHOME_POST_NO = "";
            VoFRST_RGTR_ID = "";
        }

        private void ResetInputFormField()
        {
            txtCustomerNumber.Text = "";              // 고객번호 조회
            txtSearchCustomerName.Text = "";          // 고객명 조회
            customerDvsnComboBox.Text = "";           // 고객구분 조회
            customerDvsnComboBox.SelectedIndex = -1;

            txtSearchEmailAddress.Text = "";   // 이메일조회
            txtCustomerName.Text = "";         // 고객명 입력
            txtCustomerEngName.Text = "";      // 고객영문명
            txtCellPhoneNo.Text = "";          // 휴대폰 입력
            txtEmailAddr1.Text = "";           // 이메일 앞부분 입력
            emailAddr2ComboBox.SelectedIndex = -1;             // 이메일 뒷부분 입력
            passengerSexDivisionComboBox.SelectedIndex = -1;   // 성별
            passengerBirthDateTextBox.Text = "";     // 생년월일
            txtHomePostNo.Text = "";                // 자택 우편번호 입력
            txtHomeAddr1.Text = "";                 // 자택 주소1 입력    
            txtHomeAddr2.Text = "";                 // 자택 주소2 입력
            txtOfficePostNo.Text = "";              // 사무실 우편번호 입력
            txtOfficeAddr1.Text = "";               // 사무실 주소1 입력
            txtOfficeAddr2.Text = "";               // 사무실 주소2 입력
            txtCustomerMemo.Text = "";              // 고객 메모

            emailAddr2TextBox.Visible = false;
        }

        private void InitDataGridView()
        {
            //DataGridView dataGridView1 = CustomerListDataGridView;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Azure;
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.SeaGreen;
            //dataGridView1.RowHeadersVisible = false;
        }

        // 고객목록 조회버튼 클릭
        private void searchCostPriceListButton_Click(object sender, EventArgs e)
        {
            SearchCustomerList();
        }

        // 고객 목록조회
        private void SearchCustomerList()
        {
            CustomerListDataGridView.Rows.Clear();

            string CUST_NO = "";              // 고객번호
            string CUST_NM = "";              // 고객명
            string CUST_ENG_NM = "";          // 고객영문명
            string SEX_DVSN_CD = "";          // 성별구분코드
            string SEX_DVSN_NM = "";          // 성별구분명
            string BIRTH = "";                // 생년월일
            string PRSN_CORP_DVSN_CD = "";    // 고객구분코드
            string PRSN_CORP_DVSN_NM = "";    // 고객구분명
            string CELL_PHNE_NO = "";         // 휴대전화번호
            string OFFC_ADDR = "";            // 직장주소
            string HOME_ADDR = "";            // 자택주소
            string CUST_MEMO_CNTS = "";       // 고객메모내용
            string OFFC_DETAIL_ADDR = "";     // 직장세부주소
            string HOME_DETAIL_ADDR = "";     // 자택세부주소
            string EMAL_ADDR = "";            // 이메일주소
            string OFFC_POST_NO = "";         // 직장우편번호                     
            string HOME_POST_NO = "";         // 자택우편번호       

            CUST_NM = txtSearchCustomerName.Text.Trim();
            CELL_PHNE_NO = txtSearchCellPhoneNo.Text.Trim();
            EMAL_ADDR = txtSearchEmailAddress.Text.Trim();

            string query = string.Format("CALL db_gbridge_trip.SelectCustInfoList2('{0}','{1}','{2}')", CUST_NM, CELL_PHNE_NO, EMAL_ADDR);

            //Console.WriteLine(query);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("협력업체기본정보를 가져올 수 없습니다.");
                return;
            }

            foreach (DataRow datarow in dataSet.Tables[0].Rows)
            {
                CUST_NO = datarow["CUST_NO"].ToString();
                CUST_NM = datarow["CUST_NM"].ToString();
                CUST_ENG_NM = datarow["CUST_ENG_NM"].ToString();
                SEX_DVSN_CD = datarow["SEX_DVSN_CD"].ToString();
                SEX_DVSN_NM = datarow["SEX_DVSN_NM"].ToString();
                BIRTH = datarow["BIRTH"].ToString();
                PRSN_CORP_DVSN_CD = datarow["PRSN_CORP_DVSN_CD"].ToString();
                PRSN_CORP_DVSN_NM = datarow["PRSN_CORP_DVSN_NM"].ToString();
                CELL_PHNE_NO = datarow["CELL_PHNE_NO"].ToString();
                OFFC_ADDR = datarow["OFFC_ADDR"].ToString();
                HOME_ADDR = datarow["HOME_ADDR"].ToString();
                CUST_MEMO_CNTS = datarow["CUST_MEMO_CNTS"].ToString();
                OFFC_DETAIL_ADDR = datarow["OFFC_DETAIL_ADDR"].ToString();
                HOME_DETAIL_ADDR = datarow["HOME_DETAIL_ADDR"].ToString();
                EMAL_ADDR = datarow["EMAL_ADDR"].ToString();
                OFFC_POST_NO = datarow["OFFC_POST_NO"].ToString();
                HOME_POST_NO = datarow["HOME_POST_NO"].ToString();

                CustomerListDataGridView.Rows.Add
                (
                    CUST_NO,              // 고객번호
                    CUST_NM,              // 고객명
                    CUST_ENG_NM,          // 고객영문명
                    PRSN_CORP_DVSN_CD,    // 고객구분코드
                    PRSN_CORP_DVSN_NM,    // 고객구분
                    CELL_PHNE_NO,         // 휴대전화번호
                    OFFC_ADDR,            // 직장주소
                    HOME_ADDR,            // 자택주소
                    CUST_MEMO_CNTS,       // 고객메모내용   
                    OFFC_DETAIL_ADDR,     // 직장주소세부
                    HOME_DETAIL_ADDR,     // 자택주소세부
                    EMAL_ADDR,            // 이메일주소
                    OFFC_POST_NO,         // 직장우편번호                    
                    HOME_POST_NO,         // 자택우편번호
                    BIRTH,                // 생년월일
                    SEX_DVSN_CD,          // 성별구분코드
                    SEX_DVSN_NM           // 성별구분명
                );
            }
            CustomerListDataGridView.ClearSelection();
        }


        private void btnInitializeCustomerInfo_Click(object sender, EventArgs e)
        {
            ResetInputFormField();
        }

        // 고객 정보 저장
        private void btnSaveCustomerInfo_Click(object sender, EventArgs e)
        {
            VoCUST_NO = txtCustomerNumber.Text.Trim();                                         // 고객번호
            VoCUST_NM = txtCustomerName.Text.Trim();                                           // 고객명
            VoCUST_ENG_NM = txtCustomerEngName.Text.Trim();                                    // 고객영문명
            VoPRSN_CORP_DVSN_CD = Utils.GetSelectedComboBoxItemValue(customerDvsnComboBox);    // 고객구분코드

            if (passengerBirthDateTextBox.Text.Trim() != "" && passengerBirthDateTextBox.Text.Trim().Length == 8)
            {
                VoBIRTH = string.Format("{0:yyyy/MM/dd}", passengerBirthDateTextBox.Text.Trim());  // 생년월일
                if (Utils.isYYYYMMDD(VoBIRTH) == false)
                {
                    MessageBox.Show("생년월일을 YYYYMMDD형식으로 입력하세요.");
                    passengerBirthDateTextBox.Focus();
                    return;
                }
            } else
            {
                VoBIRTH = "";
            }

            VoSEX_DVSN_CD = Utils.GetSelectedComboBoxItemValue(passengerSexDivisionComboBox);  // 성별구분코드
            VoCELL_PHNE_NO = txtCellPhoneNo.Text.Trim();                                       // 휴대전화번호
            VoOFFC_ADDR = txtOfficeAddr1.Text.Trim();                                          // 직장주소
            VoHOME_ADDR = txtHomeAddr1.Text.Trim();                                            // 자택주소
            VoCUST_MEMO_CNTS = txtCustomerMemo.Text.Trim();                                    // 고객메모내용 
            VoOFFC_DETAIL_ADDR = txtOfficeAddr2.Text.Trim();                                   // 직장세부주소  
            VoHOME_DETAIL_ADDR = txtHomeAddr2.Text.Trim();                                     // 자택세부주소

            string emailFullAddress = "";
            emailDomainAddress = "";
            // EMAIL 주소 반영전 CONCAT 처리

            // 이메일주소 직접입력인 경우 처리
            if (emailAddr2TextBox.Text.Trim().Length > 0)
            {
                emailDomainAddress = emailAddr2TextBox.Text.Trim();
            }
            else
            {
                emailDomainAddress = emailAddr2ComboBox.Text.Trim();        // 이메일도메인주소
            }

            emailFullAddress = txtEmailAddr1.Text + "@" + emailDomainAddress;                       // 이메일주소 조합
            VoEMAL_ADDR = emailFullAddress;                                                    // 이메일주소

            VoOFFC_POST_NO = txtOfficePostNo.Text.Trim();                                      // 직장우편번호
            VoHOME_POST_NO = txtHomePostNo.Text.Trim();                                        // 자택우편번호
            VoFRST_RGTR_ID = Global.loginInfo.ACNT_ID;                                         // 최초등록자ID

            string query = "";

            // 입력값 유효성 검증
            if (CheckRequireItems() == false) return;
            // 업체번호가 입력되지 않으면 MAX+1로 채번하고 Insert처리

            if (VoCUST_NO == "" || VoCUST_NO == null)
            {
                query = string.Format("CALL InsertCustInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')",
                            VoCUST_NM, VoCUST_ENG_NM, VoSEX_DVSN_CD, VoBIRTH, VoPRSN_CORP_DVSN_CD, VoCELL_PHNE_NO, VoEMAL_ADDR, VoOFFC_POST_NO,
                            VoOFFC_ADDR, VoOFFC_DETAIL_ADDR, VoHOME_POST_NO, VoHOME_ADDR, VoHOME_DETAIL_ADDR, VoCUST_MEMO_CNTS, VoFRST_RGTR_ID);
            }
            else
            {
                query = string.Format("CALL UpdateCustInfoItem ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                            VoCUST_NO, VoCUST_NM, VoCUST_ENG_NM, VoSEX_DVSN_CD, VoBIRTH, VoPRSN_CORP_DVSN_CD, VoCELL_PHNE_NO,
                            VoEMAL_ADDR, VoOFFC_ADDR, VoHOME_ADDR, VoCUST_MEMO_CNTS, VoOFFC_DETAIL_ADDR, VoHOME_DETAIL_ADDR, VoOFFC_POST_NO, VoHOME_POST_NO, VoFRST_RGTR_ID);
            }

            int retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal == -1)
            {
                MessageBox.Show("고객정보를 저장할 수 없습니다.");
                return;
            }
            else
            {
                SearchCustomerList();                            // 등록 후 그리드를 최신상태로 Refresh
                MessageBox.Show("고객정보를 저장했습니다.");
            }

            // 저장 후 입력폼 초기화
            ResetInputFormField();
        }

        // 입력값 유효성 검증
        private bool CheckRequireItems()
        {
            string CUST_NO = txtCustomerNumber.Text.Trim();                                         // 고객번호
            string CUST_NM = txtCustomerName.Text.Trim();                                           // 고객명
            string CUST_ENG_NM = txtCustomerEngName.Text.Trim();                                    // 고객영문명
            string PRSN_CORP_DVSN_CD = Utils.GetSelectedComboBoxItemValue(customerDvsnComboBox);    // 고객구분코드
            string CELL_PHNE_NO = txtCellPhoneNo.Text.Trim();                                       // 휴대전화번호
            string OFFC_ADDR = txtOfficeAddr1.Text.Trim();                                          // 직장주소
            string HOME_ADDR = txtHomeAddr1.Text.Trim();                                            // 자택주소
            string CUST_MEMO_CNTS = txtCustomerMemo.Text.Trim();                                    // 고객메모내용
            string OFFC_DETAIL_ADDR = txtOfficeAddr2.Text.Trim();                                   // 직장세부주소
            string HOME_DETAIL_ADDR = txtHomeAddr2.Text.Trim();                                     // 자택세부주소
            string EMAL_ADDR1 = txtEmailAddr1.Text.Trim();                                          // 이메일주소1  
            string EMAL_ADDR2 = emailDomainAddress;                                                 // 이메일주소2  
            string OFFC_POST_NO = txtOfficePostNo.Text.Trim();                                      // 직장우편번호
            string HOME_POST_NO = txtHomePostNo.Text.Trim();                                        // 자택우편번호

            if (CUST_NM == "")
            {
                MessageBox.Show("고객명은 필수 입력항목입니다.");
                txtCustomerName.Focus();
                return false;
            }

            /*
            if (CELL_PHNE_NO == "")
            {
                MessageBox.Show("휴대전화번호는 필수 입력항목입니다.");
                txtCellPhoneNo.Focus();
                return false;
            }

            if (EMAL_ADDR2 == "")
            {
                MessageBox.Show("이메일 도메인은 필수 입력항목입니다.");
                txtCellPhoneNo.Focus();
                return false;
            }
            */

            if (EMAL_ADDR2 != "")
            {
                string email = EMAL_ADDR1 + '@' + EMAL_ADDR2;
                EmailValidation emal_val = new EmailValidation();
                bool email_yn = emal_val.IsValidEmail(email);
                if (email_yn == false)
                {
                    MessageBox.Show("유효하지 않은 이메일 형식입니다.");
                    return false;
                }
            }                   
            else
            {
                VoEMAL_ADDR = "";
            }

            return true;
        }

        private void btnDeleteCustomerInfo_Click(object sender, EventArgs e)
        {
            string CUST_NO = txtCustomerNumber.Text.Trim();                                               // 고객번호
            string RSVT_NO = "";

            if (CUST_NO == "")
            {
                MessageBox.Show("고객번호는 필수 입력항목입니다. 목록에서 삭제 대상을 선택하세요.");
                return;
            }

            string query = string.Format("CALL SelectRsvtItemByCustNo ('{0}')", CUST_NO);
            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("예약목록 확인 중에 오류가 발생했습니다. 운영담당자에게 연락하세요.");
                return;
            }

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                RSVT_NO = dataRow["RSVT_NO"].ToString();         // 예약번호
            }

            if (RSVT_NO == "" || RSVT_NO == null)
            {
                // 예약고객이 존재하지 않으면 삭제 처리
                query = string.Format("CALL DeleteCustomerInfo ('{0}')", CUST_NO);
                int retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal == -1)
                {
                    MessageBox.Show("고객정보를 삭제할 수 없습니다. 운영담당자에게 연락하세요.");
                    return;
                }
                else
                {
                    SearchCustomerList();
                    MessageBox.Show("고객정보를 삭제했습니다.");
                }
            }
            else
            {
                MessageBox.Show("예약 실적이 있는 고객은 삭제 불가합니다.");
                return;
            }

            // 삭제 후 입력폼 초기화
            ResetInputFormField();
        }

        private void btnClosePopUp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 자택 주소찾기 버튼 클릭
        private void btnSearchHomeAddr_Click(object sender, EventArgs e)
        {
            PopUpSearchAddress form = new PopUpSearchAddress();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            string ZIP_CD = form.get_zipcode();
            string ROAD_NM = form.get_roadname();
            string ROAD_NM2 = form.get_roadname2();

            txtHomePostNo.Text = ZIP_CD;
            txtHomeAddr1.Text = String.Concat(ROAD_NM, ROAD_NM2);
        }

        // 그리드 행 클릭
        private void CustomerListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CustomerListDataGridView.SelectedRows.Count == 0)
                return;

            VoCUST_NO = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.CUST_NO].Value.ToString();                      // 고객번호
            VoCUST_NM = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.CUST_NM].Value.ToString();                      // 고객명
            VoCUST_ENG_NM = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.CUST_ENG_NM].Value.ToString();              // 고객영문명
            VoPRSN_CORP_DVSN_CD = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.PRSN_CORP_DVSN_CD].Value.ToString();  // 고객구분
            VoCELL_PHNE_NO = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.CELL_PHNE_NO].Value.ToString();            // 휴대전화번호
            VoOFFC_ADDR = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.OFFC_ADDR].Value.ToString();                  // 직장주소
            VoHOME_ADDR = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.HOME_ADDR].Value.ToString();                  // 자택주소
            VoCUST_MEMO_CNTS = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.CUST_MEMO_CNTS].Value.ToString();        // 고객메모내용
            VoOFFC_DETAIL_ADDR = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.OFFC_DETAIL_ADDR].Value.ToString();    // 직장세부주소
            VoHOME_DETAIL_ADDR = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.HOME_DETAIL_ADDR].Value.ToString();    // 자택세부주소
            VoEMAL_ADDR = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.EMAL_ADDR].Value.ToString();                  // 이메일주소
            VoOFFC_POST_NO = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.OFFC_POST_NO].Value.ToString();            // 직장 우편번호
            VoHOME_POST_NO = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.HOME_POST_NO].Value.ToString();            // 자택우편번호
            VoBIRTH = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.BIRTH].Value.ToString();                          // 생년월일
            VoSEX_DVSN_CD = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.SEX_DVSN_CD].Value.ToString();              // 성별구분코드
            VoSEX_DVSN_NM = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.SEX_DVSN_NM].Value.ToString();              // 성별구분명

            optionDataGridViewRowChoice();
        }

        // 직장 주소 찾기 팝업
        private void btnSearchOfficeAddr_Click(object sender, EventArgs e)
        {
            PopUpSearchAddress form = new PopUpSearchAddress();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            string ZIP_CD = form.get_zipcode();
            string ROAD_NM = form.get_roadname();
            string ROAD_NM2 = form.get_roadname2();

            txtOfficePostNo.Text = ZIP_CD;
            txtOfficeAddr1.Text = String.Concat(ROAD_NM, ROAD_NM2);
        }

        // 이메일 직접입력인 경우 처리
        private void emailAddr2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (emailAddr2ComboBox.SelectedIndex == 0)
            {
                emailAddr2TextBox.Visible = true;
                emailAddr2TextBox.Focus();
            }
            else
            {
                emailAddr2TextBox.Visible = false;
                emailAddr2ComboBox.Focus();
            }
        }

        private void CustomerGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 현재 Row의 인덱스
            int rowIndex = CustomerListDataGridView.CurrentRow.Index;

            // 첫번째 인덱스에서 ↑ 누르는거 방지
            if (e.KeyCode == Keys.Up && rowIndex == 0)
                return;
            // 마지막 인덱스에서 ↓ 누르는거 방지
            if (e.KeyCode == Keys.Down && CustomerListDataGridView.Rows.Count == rowIndex + 1)
                return;

            if (e.KeyCode == Keys.Up)
                rowIndex = rowIndex - 1;

            if (e.KeyCode == Keys.Down)
                rowIndex = rowIndex + 1;

            if (CustomerListDataGridView.SelectedRows.Count == 0)
                return;

            VoCUST_NO = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.CUST_NO].Value.ToString();                      // 고객번호
            VoCUST_NM = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.CUST_NM].Value.ToString();                      // 고객명
            VoCUST_ENG_NM = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.CUST_ENG_NM].Value.ToString();              // 고객명
            VoPRSN_CORP_DVSN_CD = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.PRSN_CORP_DVSN_CD].Value.ToString();  // 고객구분
            VoCELL_PHNE_NO = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.CELL_PHNE_NO].Value.ToString();            // 휴대전화번호
            VoOFFC_ADDR = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.OFFC_ADDR].Value.ToString();                  // 직장주소
            VoHOME_ADDR = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.HOME_ADDR].Value.ToString();                  // 자택주소  
            VoCUST_MEMO_CNTS = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.CUST_MEMO_CNTS].Value.ToString();        // 고객메모내용          
            VoOFFC_DETAIL_ADDR = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.OFFC_DETAIL_ADDR].Value.ToString();    // 직장세부주소            
            VoHOME_DETAIL_ADDR = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.HOME_DETAIL_ADDR].Value.ToString();    // 자택세부주소
            VoEMAL_ADDR = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.EMAL_ADDR].Value.ToString();                  // 이메일주소            
            VoOFFC_POST_NO = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.OFFC_POST_NO].Value.ToString();            // 직장 우편번호            
            VoHOME_POST_NO = CustomerListDataGridView.Rows[rowIndex].Cells[(int)eCustomerListDataGridView.HOME_POST_NO].Value.ToString();            // 자택우편번호   
            VoBIRTH = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.BIRTH].Value.ToString();                          // 생년월일
            VoSEX_DVSN_CD = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.SEX_DVSN_CD].Value.ToString();              // 성별구분코드
            VoSEX_DVSN_NM = CustomerListDataGridView.SelectedRows[0].Cells[(int)eCustomerListDataGridView.SEX_DVSN_NM].Value.ToString();              // 성별구분명

            optionDataGridViewRowChoice();
        }

        private void optionDataGridViewRowChoice()
        {
            // 이메일 주소 분리
            if (VoEMAL_ADDR != "")
            {
                string text = VoEMAL_ADDR;
                string[] split_text;
                split_text = text.Split('@');

                txtEmailAddr1.Text = split_text[0];                                  // 이메일주소
                emailAddr2ComboBox.Text = split_text[1];                             // 이메일주소2
                Utils.SelectComboBoxItemByValue(emailAddr2ComboBox, split_text[1]);
            }
            else
            {
                txtEmailAddr1.Text = "";
                emailAddr2ComboBox.Text = "";
            }

            txtCustomerNumber.Text = VoCUST_NO;                                            // 고객번호
            txtCustomerName.Text = VoCUST_NM;                                              // 고객명
            txtCustomerEngName.Text = VoCUST_ENG_NM;                                       // 고객영문명
            txtCellPhoneNo.Text = VoCELL_PHNE_NO;                                          // 휴대전화번호
            txtOfficeAddr1.Text = VoOFFC_ADDR;                                             // 직장주소
            txtHomeAddr1.Text = VoHOME_ADDR;                                               // 자택주소
            txtCustomerMemo.Text = VoCUST_MEMO_CNTS;                                       // 고객메모내용
            txtOfficeAddr2.Text = VoOFFC_DETAIL_ADDR;                                      // 직장세부주소
            txtHomeAddr2.Text = VoHOME_DETAIL_ADDR;                                        // 자택세부주소
            txtOfficePostNo.Text = VoOFFC_POST_NO;                                         // 직장우편번호
            txtHomePostNo.Text = VoHOME_POST_NO;                                           // 자택우편번호
            passengerBirthDateTextBox.Text = VoBIRTH;                                      // 생년월일
            Utils.SelectComboBoxItemByValue(customerDvsnComboBox, VoPRSN_CORP_DVSN_CD);    // 고객구분코드

            if (VoSEX_DVSN_CD != "")
            {
                Utils.SelectComboBoxItemByValue(passengerSexDivisionComboBox, VoSEX_DVSN_CD);  // 성별구분코드
            } else
            {
                passengerSexDivisionComboBox.SelectedIndex = -1;
            }
        }

        // 고객명을 입력하고 엔터키를 누르면 고객을 검색
        private void txtSearchCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearchCustomerName.Text != "")
            {
                SearchCustomerList();
            }
        }

        private void passengerBirthDateTextBox_Leave(object sender, EventArgs e)
        {
            if (passengerBirthDateTextBox.Text.Trim().Equals("YYYYMMDD")) passengerBirthDateTextBox.Text = "";
        }

        private void passengerBirthDateTextBox_Click(object sender, EventArgs e)
        {
            if (passengerBirthDateTextBox.Text.Trim().Length == 0) passengerBirthDateTextBox.Text = "YYYYMMDD";
            this.passengerBirthDateTextBox.SelectAll();
        }
    }
}
 