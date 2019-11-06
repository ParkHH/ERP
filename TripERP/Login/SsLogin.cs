using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;
using System.Configuration;  // id 저장을 위해
using System.Net;
using System.Web;

namespace TripERP.Login
{
    public partial class SsLogin : Form
    {
        private TripERPmain _parent = null;
        private string MACAddress = null;

        public bool _loginButtonClicked = false;

        public SsLogin(TripERPmain parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void SsLogin_Load(object sender, EventArgs e)
        {
            InitControls();
            MACAddress = Utils.getMACAddress();            
        }

        private void InitControls()
        {
            this.AcceptButton = loginButton;

            if (Properties.Settings.Default.LoginID != string.Empty)
            {
                idTextBox.Text = Properties.Settings.Default.LoginID;                
            }
        }



        //=============================================================================================================
        // Login Button Click 했을때 동작하는 Event Method
        //=============================================================================================================
        private void loginButton_Click(object sender, EventArgs e)
        {           
            _loginButtonClicked = true;
            // ====================================================================================================
            // TripERP 로그인 로직 --> 관리자 웹페이지의 로그인요청 방식 - 190909 배장훈
            // ====================================================================================================
            string id = idTextBox.Text.Trim();
            string password = HashGenerate.GenerateMySQLHash(passwordTextBox.Text.Trim()).ToString().Trim();
            string otpNum = otpNumTextBox.Text.Trim();
            string encodedKey = "6HBZYWZUGA27C4U2";

            if (id == "")
            {
                MessageBox.Show("아이디가 입력되지 않았습니다.", "아이디 입력", MessageBoxButtons.OK);
                idTextBox.Focus();
                return; 
            }

            if (password == "")
            {
                MessageBox.Show("비밀번호가 입력되지 않았습니다.", "비밀번호 입력", MessageBoxButtons.OK);
                passwordTextBox.Focus();
                return;
            }


            // 입력한 ID, Password 값이 유효한지 check
            bool validateIdPassResult = vaildateLoginValue(id);
            if (!validateIdPassResult)
            {
                return;
            }


            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // MAC 주소를 비교하여 OTP 를 입력받을지 않받을지에 대해 구분 --> 박현호
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            bool checkMACResult = checkMACAddress(id, password);

            // Login 요청 URL, adminPage로 돌릴시 url 변경 ↓↓↓
            string url = "";
            //string url = "";

            string CMPN_NO = "";
            string CMPN_NM = "";
            string EMPL_NO = "";
            string EMPL_NM = "";
            string DEPT_NM = "";
            string PSTN_NM = "";
            string ACNT_ID = "";
            string ACNT_PW = "";
            string MAC = "";
            string CNNC_PRMI_YN = "";
            string HLOF_DVSN_CD = "";
            string SCRN_SORT_ORD = "";
            string OTP_CHECK = "";

            //**************************************************************************
            // DB 에 등록된 Mac 주소가 현재 PC 와의 MAC 주소와 같다면 아이디 비밀번호만 비교하여 로그인
            //**************************************************************************
            if (checkMACResult)     
            {                
                // adminPage 로그인 Parameter(오브젝트 타입)
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    {"id", id},
                    {"pwd", password},
                    {"otpNum", otpNum},
                    {"encodedKey", encodedKey},
                    {"otpFlag", "false"},
                    { "loginProgram", "ERP"}
                };

                string result = HttpPostRequest(url, dic);

                // Request --> "" : 서버 연결 ERROR
                if (result.Equals(""))
                {
                    MessageBox.Show("원격 서버에 접속할 수 없습니다. 운영담당자에게 연락하세요.");
                    return;
                }

                // Request --> {} : 존재하지 않는 ID/PW
                if (result.Equals("{}"))
                {
                    MessageBox.Show("아이디, 비밀번호 가 일치하지 않습니다.");
                    return;
                }
                else
                {
                    // JSON 타입의 "(Double Quotation) 문자열 제거
                    result = result.Replace("\"", "");

                    // JSON 타입의 {(중괄호) 제거
                    result = result.Replace("{", "");
                    result = result.Replace("}", "");

                    // 기존 로그인조회 StoredProcedure의 JSON Return값 11개
                    string[] split_result = new string[11];

                    // JSON Parsing
                    split_result = result.Split(',');
                    foreach (string msg in split_result)
                    {
                        string[] in_split_result = new string[2];
                        in_split_result = msg.Split(':');

                        if (in_split_result[0].Equals("CMPN_NO"))
                            CMPN_NO = in_split_result[1];
                        if (in_split_result[0].Equals("CMPN_NM"))
                            CMPN_NM = in_split_result[1];
                        if (in_split_result[0].Equals("EMPL_NO"))
                            EMPL_NO = in_split_result[1];
                        if (in_split_result[0].Equals("EMPL_NM"))
                            EMPL_NM = in_split_result[1];
                        if (in_split_result[0].Equals("DEPT_NM"))
                            DEPT_NM = in_split_result[1];
                        if (in_split_result[0].Equals("PSTN_NM"))
                            PSTN_NM = in_split_result[1];
                        if (in_split_result[0].Equals("ACNT_ID"))
                            ACNT_ID = in_split_result[1];
                        if (in_split_result[0].Equals("ACNT_PW"))
                            ACNT_PW = in_split_result[1];
                        if (in_split_result[0].Equals("MAC"))
                            MAC = in_split_result[1];
                        if (in_split_result[0].Equals("CNNC_PRMI_YN"))
                            CNNC_PRMI_YN = in_split_result[1];
                        if (in_split_result[0].Equals("HLOF_DVSN_CD"))
                            HLOF_DVSN_CD = in_split_result[1];
                        if (in_split_result[0].Equals("SCRN_SORT_ORD"))
                            SCRN_SORT_ORD = in_split_result[1];
                    }
                }

                if (CNNC_PRMI_YN.Equals("N"))
                {
                    MessageBox.Show("사용자 접속 권한이 없습니다. IT 담당자에게 문의하세요.");
                    return;
                }

                Global.loginInfo = new LoginInfo();
                Global.loginInfo.CMPN_NO = CMPN_NO;
                Global.loginInfo.CMPN_NM = CMPN_NM;
                Global.loginInfo.EMPL_NO = EMPL_NO;
                Global.loginInfo.EMPL_NM = EMPL_NM;
                Global.loginInfo.DEPT_NM = DEPT_NM;
                Global.loginInfo.PSTN_NM = PSTN_NM;
                Global.loginInfo.ACNT_ID = ACNT_ID;
                Global.loginInfo.ACNT_PW = ACNT_PW;
                Global.loginInfo.MAC = MAC;
                Global.loginInfo.CNNC_PRMI_YN = CNNC_PRMI_YN;
                Global.loginInfo.HLOF_DVSN_CD = HLOF_DVSN_CD;
                Global.loginInfo.SCRN_SORT_ORD = SCRN_SORT_ORD;
                Global.loginInfo.loginTime = DateTime.Now;
            }        
            else
            {
                //*********************************************************
                // DB 에 등록된 Mac 주소가 현재 PC 와의 MAC 주소와 다르다면 OTP 입력
                //*********************************************************                
                otpNumTextBox.ReadOnly = false;

                if(otpNum == "")
                {
                    MessageBox.Show("계정에 등록된 MAC 주소와 현재 MAC 주소가 불일치합니다.\n보안을 위해 OTP 를 사용합니다.");
                    otpNumTextBox.Focus();
                    return;
                }

                // adminPage 로그인 Parameter(오브젝트 타입)
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    {"id", id},
                    {"pwd", password},
                    {"otpNum", otpNum},
                    {"encodedKey", encodedKey},
                    {"otpFlag", "true"},
                    { "loginProgram", "ERP"}
                };

                string result = HttpPostRequest(url, dic);

                // Request --> "" : 서버 연결 ERROR
                if (result.Equals(""))
                {
                    MessageBox.Show("원격 서버에 접속할 수 없습니다. 운영담당자에게 연락하세요.");
                    return;
                }

                // Request --> {} : 존재하지 않는 ID/PW
                if (result.Equals("{}"))
                {
                    MessageBox.Show("아이디, 비밀번호 가 일치하지 않습니다.");
                    return;
                }
                else
                {
                    // JSON 타입의 "(Double Quotation) 문자열 제거
                    result = result.Replace("\"", "");

                    // JSON 타입의 {(중괄호) 제거
                    result = result.Replace("{", "");
                    result = result.Replace("}", "");

                    // 기존 로그인조회 StoredProcedure의 JSON Return값 11개
                    string[] split_result = new string[11];

                    // JSON Parsing
                    split_result = result.Split(',');
                    foreach (string msg in split_result)
                    {
                        string[] in_split_result = new string[2];
                        in_split_result = msg.Split(':');

                        if (in_split_result[0].Equals("CMPN_NO"))
                            CMPN_NO = in_split_result[1];
                        if (in_split_result[0].Equals("CMPN_NM"))
                            CMPN_NM = in_split_result[1];
                        if (in_split_result[0].Equals("EMPL_NO"))
                            EMPL_NO = in_split_result[1];
                        if (in_split_result[0].Equals("EMPL_NM"))
                            EMPL_NM = in_split_result[1];
                        if (in_split_result[0].Equals("DEPT_NM"))
                            DEPT_NM = in_split_result[1];
                        if (in_split_result[0].Equals("PSTN_NM"))
                            PSTN_NM = in_split_result[1];
                        if (in_split_result[0].Equals("ACNT_ID"))
                            ACNT_ID = in_split_result[1];
                        if (in_split_result[0].Equals("ACNT_PW"))
                            ACNT_PW = in_split_result[1];
                        if (in_split_result[0].Equals("CNNC_PRMI_YN"))
                            CNNC_PRMI_YN = in_split_result[1];
                        if (in_split_result[0].Equals("HLOF_DVSN_CD"))
                            HLOF_DVSN_CD = in_split_result[1];
                        if (in_split_result[0].Equals("SCRN_SORT_ORD"))
                            SCRN_SORT_ORD = in_split_result[1];
                        if (in_split_result[0].Equals("codeCheckResult"))
                            OTP_CHECK = in_split_result[1];
                    }
                }

                if (OTP_CHECK.Equals("false"))
                {
                    MessageBox.Show("OTP 비밀번호가 일치하지 않습니다.");
                    return;
                }

                if (CNNC_PRMI_YN.Equals("N"))
                {
                    MessageBox.Show("사용자 접속 권한이 없습니다. IT 담당자에게 문의하세요.");
                    return;
                }

                Global.loginInfo = new LoginInfo();
                Global.loginInfo.CMPN_NO = CMPN_NO;
                Global.loginInfo.CMPN_NM = CMPN_NM;
                Global.loginInfo.EMPL_NO = EMPL_NO;
                Global.loginInfo.EMPL_NM = EMPL_NM;
                Global.loginInfo.DEPT_NM = DEPT_NM;
                Global.loginInfo.PSTN_NM = PSTN_NM;
                Global.loginInfo.ACNT_ID = ACNT_ID;
                Global.loginInfo.ACNT_PW = ACNT_PW;
                Global.loginInfo.CNNC_PRMI_YN = CNNC_PRMI_YN;
                Global.loginInfo.HLOF_DVSN_CD = HLOF_DVSN_CD;
                Global.loginInfo.SCRN_SORT_ORD = SCRN_SORT_ORD;
                Global.loginInfo.loginTime = DateTime.Now;
                // ====================================================================================================

                //string query = string.Format("CALL SelectAcntInfoItem ('{0}', '{1}')", id, password);
                //DataSet dataSet = DbHelper.SelectQuery(query);
                //if (dataSet == null || dataSet.Tables.Count == 0)
                //{
                //    MessageBox.Show("로그인 정보를 가져올 수 없습니다.");
                //    return;
                //}

                //if (dataSet.Tables[0].Rows.Count == 0)
                //{
                //    MessageBox.Show("아이디, 비밀번호 가 일치하지 않습니다.");
                //    return;
                //}

                //// 접속권한이 없을 때 메시지 박스로 권한이 없음을 알림.            
                //if (dataSet.Tables[0].Rows[0]["CNNC_PRMI_YN"].ToString() == "N")
                //{
                //    MessageBox.Show("사용자 접속 권한이 없습니다. IT 담당자에게 문의하세요.");
                //    return;
                //}

                //DataRow dataRow = dataSet.Tables[0].Rows[0];

                //Global.loginInfo = new LoginInfo();
                //Global.loginInfo.CMPN_NO = dataRow["CMPN_NO"].ToString();
                //Global.loginInfo.CMPN_NM = dataRow["CMPN_NM"].ToString();
                //Global.loginInfo.EMPL_NO = dataRow["EMPL_NO"].ToString();
                //Global.loginInfo.EMPL_NM = dataRow["EMPL_NM"].ToString();
                //Global.loginInfo.DEPT_NM = dataRow["DEPT_NM"].ToString();
                //Global.loginInfo.PSTN_NM = dataRow["PSTN_NM"].ToString();
                //Global.loginInfo.ACNT_ID = dataRow["ACNT_ID"].ToString();
                //Global.loginInfo.ACNT_PW = dataRow["ACNT_PW"].ToString();
                //Global.loginInfo.CNNC_PRMI_YN = dataRow["CNNC_PRMI_YN"].ToString();
                //Global.loginInfo.HLOF_DVSN_CD = dataRow["HLOF_DVSN_CD"].ToString();
                //Global.loginInfo.SCRN_SORT_ORD = dataRow["SCRN_SORT_ORD"].ToString();
                //Global.loginInfo.loginTime = DateTime.Now;
            }

            checkAndCreateTemplateFolder();
            
            // ID 저장 
            if (IdStoreCheckBox.Checked)
            {
                Properties.Settings.Default.LoginID = idTextBox.Text.Trim();
                Properties.Settings.Default.LoginID_Checked = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.LoginID = "";
                Properties.Settings.Default.LoginID_Checked = false;
                Properties.Settings.Default.Save();
            }
          
            this.Close();
        }









        private string HttpPostRequest(string url, Dictionary<string, string> postParameters) {
            string postData = "";
            string pageContent = "";

            foreach (string key in postParameters.Keys) {
                postData += HttpUtility.UrlEncode(key) + "="
                      + HttpUtility.UrlEncode(postParameters[key]) + "&";
            }

            HttpWebResponse myHttpWebResponse = null;
            HttpWebRequest myHttpWebRequest = null;
            Stream responseStream = null;
            Stream requestStream = null;
            StreamReader myStreamReader = null;

            try {
                myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myHttpWebRequest.Method = "POST";

                byte[] data = Encoding.Default.GetBytes(postData);


                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.ContentLength = data.Length;

                requestStream = myHttpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                responseStream = myHttpWebResponse.GetResponseStream();

                myStreamReader = new StreamReader(responseStream, Encoding.UTF8, true);

                pageContent = myStreamReader.ReadToEnd();


                myStreamReader.Close();
                responseStream.Close();
                myHttpWebResponse.Close();
            } catch (WebException e) {
                //if (e.Message.Equals("원격 서버에 접속할 수 없습니다.")) ;
                //    MessageBox.Show(e.Message + " 운영담당자에게 연락하세요.");
            } 

            return pageContent;
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







        //=============================================================================================================
        // Form 닫기 Button Click 시 동작하는 Event Method
        //=============================================================================================================
        private void SsLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parent.TogleContaner();
            _parent.SetStatus("");

            if (_loginButtonClicked) _parent.OpenDashboardForm();
        }

        private void checkAndCreateTemplateFolder()
        {
            string folderPath = null;
            DirectoryInfo di = null;
            bool templateFileExists = true;

            // 다운로드 폴더가 없으면 생성
            folderPath = "C:/TripERP/Downloads";
            di = new DirectoryInfo(folderPath);

            if (di.Exists == false)
            {
                di.Create();
            }

            // 바우처 템플릿 폴더가 없으면 생성
            folderPath = "C:/TripERP/Vouchers";
            di = new DirectoryInfo(folderPath);

            if (di.Exists == false)
            {
                di.Create();
                templateFileExists = false;
            }

            // 바우처 템플릿 폴더가 없으면 생성
            folderPath = "C:/TripERP/Invoice";
            di = new DirectoryInfo(folderPath);

            if (di.Exists == false)
            {
                di.Create();
                templateFileExists = false;
            }

            if (templateFileExists == false)
            {
                MessageBox.Show("바우처와 인보이스 템플릿이 존재하지 않습니다. C:\\TripERP\\Vouchers 폴더와 Invoice 폴더에 바우처/인보이스 템플릿 파일을 저장하세요.");
            }
        }

        private void gunaControlBox1_Click(object sender, EventArgs e) {
        }








        //=============================================================================================================
        // MAC 주소를 비교하여 OTP 를 입력받을지 안받을지에 대해 구분 --> 190918 박현호
        //=============================================================================================================
        public bool checkMACAddress(string id, string password) {
            bool result = false;
            if (id.Equals("admin"))
            {
                return true;
            }
            string checkMACQuery = string.Format("SELECT MAC FROM TB_ACNT_INFO_M WHERE ACNT_ID='" + id+"'");
            DataSet MACAddressData = DbHelper.SelectQuery(checkMACQuery);
            int rowCount = MACAddressData.Tables[0].Rows.Count;
            if (rowCount == 0)
            {
                return result;
            }
            else
            {           
                DataRow row = MACAddressData.Tables[0].Rows[0];
                string getMAC = row["MAC"].ToString().Trim();                
                if (getMAC.Equals(MACAddress))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
