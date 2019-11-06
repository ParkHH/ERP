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
    public partial class PopUpAddCustomerInfo : Form
    {
        private PopUpSearchCustomerInfo _parent = null; 

        public PopUpAddCustomerInfo(PopUpSearchCustomerInfo parent)
        {
            InitializeComponent();
            _parent = parent; 
        }

        private void PopUpAddCustomerInfo_Load(object sender, EventArgs e)
        {
            loadCommonBomboBox();
            resetInputForm();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetInputForm();
        }

        private void resetInputForm()
        {
            customerNameTextBox.Text = "";
            customerEngNameTextBox.Text = "";
            cellPhoneNumberTextBox.Text = "";
            passengerSexDivisionComboBox.SelectedIndex = -1;
            emailIdTextBox.Text = "";
            emailDomainComboBox.SelectedIndex = -1;
            emailDomainComboBox.SelectedIndex = -1;
            homePostNumberTextBox.Text = "";
            homeAddressTextBox.Text = "";
        }

        //===============================================================================================================================================================
        // 공통코드 콤보박스 일괄 설정
        //===============================================================================================================================================================
        private void loadCommonBomboBox()
        {
            string[] groupNameArray = { "EMAL_DOMN_ADDR", "PRSN_CORP_DVSN_CD", "SEX_DVSN_CD" };

            ComboBox[] comboBoxArray = { emailDomainComboBox, personalCorporationDivisionComboBox, passengerSexDivisionComboBox };

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

            // 개인법인구분코드: 개인을 기본으로 설정
            if (personalCorporationDivisionComboBox.Items.Count > 0) personalCorporationDivisionComboBox.SelectedIndex = 0;
        }

        //===============================================================================================================================================================
        // 고객정보 저장
        //===============================================================================================================================================================
        private void saveCustomerButton_Click(object sender, EventArgs e)
        {
            string customerName = customerNameTextBox.Text.Trim();
            string customerEngName = customerEngNameTextBox.Text.Trim();
            string sexDivisionCode = "";
            string birthDay = "";

            // 성별은 필수입력항목이 아니므로 예외처리
            if (passengerSexDivisionComboBox.SelectedIndex > -1)
            {
                sexDivisionCode = Utils.GetSelectedComboBoxItemValue(passengerSexDivisionComboBox);    // 성별구분코드
            }

            // 생년월일 텍스트박스 예외처리
            if (passengerBirthDateTextBox.Text.Trim() != "" && passengerBirthDateTextBox.Text.Trim().Length == 8)
            {
                birthDay = string.Format("{0:yyyy/MM/dd}", passengerBirthDateTextBox.Text.Trim());  // 생년월일
                if (Utils.isYYYYMMDD(birthDay) == false)
                {
                    MessageBox.Show("생년월일을 YYYYMMDD형식으로 입력하세요.");
                    passengerBirthDateTextBox.Focus();
                    return;
                }
            }
            else
            {
                birthDay = null;
            }

            string cellPhoneNumber = cellPhoneNumberTextBox.Text.Trim();
            string emailId = emailIdTextBox.Text.Trim();

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------
            // 이메일 Domain 입력시 조건값 동작 --> 190820 박현호
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------
            string emailDomain = "";
            if (emailDomainComboBox.SelectedIndex == -1 && cellPhoneNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("휴대폰번호와 이메일주소 둘 중 하나는 필수 입력사항입니다.");
                cellPhoneNumberTextBox.Focus();
                return; 
            }

            /*
            if (emailDomainComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("이메일은 필수 입력사항입니다.");                        // 경고문구 출력
                emailDomainComboBox.Focus();
                return;
            }
            */

            if (emailDomainComboBox.SelectedIndex == 0)                                         // 선택한 ComboBoxItem 값이 '---- 선택 ----' 이 아니고
            {
                if (directInsertEmailDomainTextBox.Text.Trim() == "")
                {
                    /*
                    MessageBox.Show("이메일은 필수 입력사항입니다.");                        // 경고문구 출력
                    directInsertEmailDomainTextBox.Focus();
                    return;
                    */
                }
                else
                {
                    emailDomain = directInsertEmailDomainTextBox.Text.Trim();       // visible = true 가 되는 TextBox 의 Text 값을 emailDomain 변수에 대입
                }
            } else
            {
                emailDomain = Utils.GetSelectedComboBoxItemText(emailDomainComboBox);
            }

            //-----------------------------------------------------------------------------------------------------------------------------------------------------------
            string homePostNumber = homePostNumberTextBox.Text.Trim();
            string homeAddress = homeAddressTextBox.Text.Trim();
            string homeDetailAddress = homeDetailAddressTextBox.Text.Trim();
            string officePostNumber = officePostNumberTextBox.Text.Trim();
            string officeAddress = officeAddressTextBox.Text.Trim();
            string officeDetailAddress= officeDetailAddressTextBox.Text.Trim();
            string memo = memoTextBox.Text.Trim();

            // 개인법인구분코드
            string personalCorporationDivision = "";
            if (personalCorporationDivisionComboBox.SelectedIndex != -1)
            {
                personalCorporationDivision = Utils.GetSelectedComboBoxItemValue(personalCorporationDivisionComboBox);
            }

            if (customerName == "")
            {
                MessageBox.Show("고객명을 입력해 주십시오.");
                customerNameTextBox.Focus();
                return;
            }

            string emailAddress = "";
            if (emailId != "" && emailDomain != "")  emailAddress = emailId + "@" + emailDomain;

            
            EmailValidation emal_val = new EmailValidation();
            bool email_yn = emal_val.IsValidEmail(emailAddress);

            if (email_yn == false)
            {
                MessageBox.Show("유효하지 않은 이메일 형식입니다.");
                return;
            }

            string query = string.Format("CALL InsertCustInfoItem ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                customerName,
                customerEngName,
                sexDivisionCode,
                birthDay,
                personalCorporationDivision,
                cellPhoneNumber,
                emailAddress,
                officePostNumber,
                officeAddress,
                officeDetailAddress,
                homePostNumber,
                homeAddress,
                officeDetailAddress,
                memo, 
                Global.loginInfo.ACNT_ID
            );

            long retVal = DbHelper.ExecuteScalar(query); 
            if(retVal != -1)
            {
                _parent.customerDataGridView.Rows.Add(retVal, customerName, customerEngName, cellPhoneNumber, emailAddress);
                MessageBox.Show("고객을 등록했습니다."); 
            }
            else
            {
                MessageBox.Show("고객을 등록 할 수 없습니다.");
            }

            this.Close();
        }

        // 호출창에서 설정한 고객명을 입력검색 고객명에 Setting
        public void SetCustomerName(string customerName)
        {
            customerNameTextBox.Text = customerName;
        }

        // 호출창에서 설정한 휴대폰번호를 입력검색 고객명에 Setting
        public void SetBookerCellPhoneNo(string cellPhoneNo)
        {
            cellPhoneNumberTextBox.Text = cellPhoneNo;
        }

        // 호출창에서 설정한 이메일주소를 입력검색 고객명에 Setting
        public void SetBookerEmailAddress(string emailAddress)
        {
            if (emailAddress != "")
            {
                string[] emailAddressSplit;
                emailAddressSplit = emailAddress.Split('@');

                string _emailId = emailAddressSplit[0];
                string _emailDomain = emailAddressSplit[1];
                 
                emailIdTextBox.Text = _emailId;                                     // 이메일ID
                Utils.SelectComboBoxItemByValue(emailDomainComboBox, _emailDomain); // 이메일도메인주소
            }
        }


        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchHomeAddr_Click(object sender, EventArgs e)
        {
            PopUpSearchAddress form = new PopUpSearchAddress();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            string ZIP_CD = form.get_zipcode();
            string ROAD_NM = form.get_roadname();
            string ROAD_NM2 = form.get_roadname2();

            homePostNumberTextBox.Text = ZIP_CD;
            homeAddressTextBox.Text = String.Concat(ROAD_NM, ROAD_NM2);
        }

        private void btnSearchOfficeAddr_Click(object sender, EventArgs e)
        {
            PopUpSearchAddress form = new PopUpSearchAddress();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            string ZIP_CD = form.get_zipcode();
            string ROAD_NM = form.get_roadname();
            string ROAD_NM2 = form.get_roadname2();

            officePostNumberTextBox.Text = ZIP_CD;
            officeAddressTextBox.Text = String.Concat(ROAD_NM, ROAD_NM2);
        }



        // 이메일 도메인 ComboBox 값 변경되었을때 동작         --> 190820 박현호
        //================================================================
        private void emailDomainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (emailDomainComboBox.SelectedIndex == 0)
            {
                directInsertEmailDomainTextBox.Visible = true;
                directInsertEmailDomainTextBox.Focus();
            }
            else
            {
                directInsertEmailDomainTextBox.Visible = false;
                emailDomainComboBox.Focus();
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
        //================================================================
    }
}
