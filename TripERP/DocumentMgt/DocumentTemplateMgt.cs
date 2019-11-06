using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using TripERP.Common;
using System.Net.Sockets;
using System.IO;
using System.Net.Http;

namespace TripERP.DocumentMgt
{
    public partial class DocumentTemplateMgt : Form
    {
        string uploadFileFullPath = "";

        public DocumentTemplateMgt()
        {
            InitializeComponent();
            getDocumentType();
            getCompanies();
            getProducts();
        }

        // 문서종류 가져오기    --> 190816 박현호
        //==========================================================================================
        public void getDocumentType()
        {
            FileTypeComboBox.Items.Clear();            
            FileTypeComboBox.Text = "문서종류를 선택하세요.";

            string query = string.Format("CALL SelectDocuInfoList");
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;
            for(int i=0; i<dataRowList.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string CD_VLID_VAL = dataRow["CD_VLID_VAL"].ToString().Trim();
                string CD_VLID_VAL_DESC = dataRow["CD_VLID_VAL_DESC"].ToString().Trim();

                FileTypeComboBox.Items.Add(new ComboBoxItem(CD_VLID_VAL_DESC+" ("+CD_VLID_VAL+")", CD_VLID_VAL));
            }

            FileTypeComboBox.SelectedIndex = -1;
        }
        //==========================================================================================





        // 모객업체 가져오기    --> 190816 박현호
        //==========================================================================================
        public void getCompanies()
        {
            CompanyComboBox.Items.Clear();
            CompanyComboBox.Text = "모객업체를 선택하세요.";            

            string query = string.Format("CALL SelectCoopCmpnList('{0}', '{1}')", "10", ' ');
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;
            for (int i = 0; i < dataRowList.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string CMPN_NO = dataRow["CMPN_NO"].ToString();
                string CMPN_NM = dataRow["CMPN_NM"].ToString();

                CompanyComboBox.Items.Add(new ComboBoxItem(CMPN_NM+" ("+ CMPN_NO + ")", CMPN_NO));
            }

            CompanyComboBox.SelectedIndex = -1;
        }
        //==========================================================================================





        // 상품명 가져오기 --> 190816 박현호
        //==========================================================================================
        public void getProducts()
        {
            ProductComboBox.Items.Clear();
            ProductComboBox.Text = "상품명을 선택하세요.";            

            string query = string.Format("CALL SelectPrdtList");
            DataSet dataSet = DbHelper.SelectQuery(query);
            DataRowCollection dataRowList = dataSet.Tables[0].Rows;

            for (int i = 0; i < dataRowList.Count; i++)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[i];
                string PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();
                string PRDT_NM = dataRow["PRDT_NM"].ToString();
                ProductComboBox.Items.Add(new ComboBoxItem(PRDT_NM+" ("+ PRDT_CNMB + ")", PRDT_CNMB));
            }
            ProductComboBox.SelectedIndex = -1;
        }
        //==========================================================================================





        // 찾아보기 Button 눌렀을때 동작!!  --> 190816 박현호
        //==========================================================================================
        private void bt_selecteFile_Click(object sender, EventArgs e)
        {
            fileSelection();        // File 선택하기
        }

        // File 선택
        //------------------------------------------------------------------------------------
        public void fileSelection()
        {
            FileDialog selectFileWindow = new OpenFileDialog();
            selectFileWindow.ShowDialog();
            selectFileWindow.CheckFileExists = true;
            string selectedFileFullName = selectFileWindow.FileName.ToString().Trim();              // 경로를 포함한 파일의 이름을 가져온다.
            string[] selectedFileFullNameArr = selectedFileFullName.Split(new char[] {'\\'});       // '\' 을 기준으로 split 
            string selectedFileName = selectedFileFullName.Substring(selectedFileFullName.LastIndexOf("\\")+1, selectedFileFullNameArr[selectedFileFullNameArr.Length-1].Length);            
            filePathNameTextBox.Text = selectedFileName;                                    // 경로를 제외한 File 이름을 FileName TextBox 예 출력

            uploadFileFullPath = selectedFileFullName;                                          // File 전송을 위해 경로를 포함한 File 이름을 Member 변수로 지정
        }
        //------------------------------------------------------------------------------------
        //==========================================================================================






        // 업로드 Button 눌렀을때 동작!!     --> 190816 박현호
        //==========================================================================================
        private void bt_upload_Click(object sender, EventArgs e)
        {
            getUploadTemplateFileInfo();       // Template File Upload 정보 확인!
        }

        // Template File Upload data Confirm
        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        public void getUploadTemplateFileInfo()
        {
            string[] result = null;            // Upload File 정보와 Validate 결과를 담을 배열

            if (MessageBox.Show("Template File Upload 를 진행하시겠습니까?", "Template Upload", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            //1. 문서종류, 모객업체, 상품명 값 Validation            
            string selectedFileName = filePathNameTextBox.Text;
            ComboBoxItem selectedFileItem = (ComboBoxItem)FileTypeComboBox.SelectedItem;
            ComboBoxItem selectedCompanyItem = (ComboBoxItem)CompanyComboBox.SelectedItem;
            ComboBoxItem selectedProductItem = (ComboBoxItem)ProductComboBox.SelectedItem;

            // 2. 선택한 File 없을시 경고
            if (selectedFileName.Equals(""))
            {
                MessageBox.Show("Upload 할 Template File 을 선택하세요!", "경고");
                return;
            }

            // 3. Upload 정보 미선택시 경고
            if (selectedFileItem == null || selectedProductItem == null)
            {
                MessageBox.Show("Template Upload 정보를 확인하세요!", "경고");
                return;
            }

            // 3. Upload 정보 미선택시 경고
            if (CompanyComboBox.Enabled && selectedCompanyItem == null) {
                MessageBox.Show("Template Upload 정보를 확인하세요!", "경고");
                return;
            }

            // 문서종류가 바우처인 경우 협력업체는 0000으로 고정(양식 일정)
            string selectedCompanyValue = "0000";
            {

                // 문서종류가 바우처가 아닌경우
                if (CompanyComboBox.Enabled)
                    selectedCompanyValue = selectedCompanyItem.Value.ToString().Trim();

                string selectedFileValue = selectedFileItem.Value.ToString().Trim();
                string selectedProductValue = selectedProductItem.Value.ToString().Trim();
                //MessageBox.Show("Upload Template 정보 : " + selectedFileValue + " " + selectedCompanyValue + " " + selectedProductValue);

                result = new string[5];
                result[0] = selectedFileValue;
                result[1] = selectedCompanyValue;
                result[2] = selectedProductValue;
                result[4] = "true";

                requestUploadAsync(result);          // Upload 요청 진행!
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        // Template File Upload Request To WAS
        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task requestUploadAsync(string[] dataValidateResult)
        {
            string[] validateResult = dataValidateResult;
            string fileValue = validateResult[0];
            string companyValue = validateResult[1];
            string productValue = validateResult[2];
            string selectedFileName = filePathNameTextBox.Text;

            if (validateResult == null)
            {
                MessageBox.Show("Upload 요청 실패!!. \n관리자에게 문의하세요.");
                return;
            }                     
            

            HttpClient client = new HttpClient();            
            MultipartFormDataContent form = new MultipartFormDataContent();
            FileInfo file = new FileInfo(uploadFileFullPath);
            string message = "";
            form.Add(new StreamContent(file.OpenRead()), "FileParam", file.Name);            
            HttpResponseMessage respon = await client.PostAsync("http://gbrdg111.vps.phps.kr:5000/admin/template/upload?uploader="+Global.loginInfo.ACNT_ID+"&file_kind="+ fileValue + "&CMPN_NO=" + companyValue + "&PRDT_CNMB=" + productValue, form);
            //MessageBox.Show(respon.StatusCode.ToString());
            if (respon.StatusCode.ToString().Equals("OK"))
            {
                message = "Upload 완료!";
                resetData();
            }
            else
            {
                message = "Upload 실패!!\n관리자에게 문의하세요!";
            }
            MessageBox.Show(message);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //==========================================================================================



        // Component Data Reset --> 190818 박현호
        //========================================================================
        public void resetData()
        {
            filePathNameTextBox.Text = "";
            FileTypeComboBox.SelectedIndex = -1;
            CompanyComboBox.SelectedIndex = -1;
            ProductComboBox.SelectedIndex = -1;
            FileTypeComboBox.Text = "문서종류를 선택하세요.";
            CompanyComboBox.Text = "모객업체를 선택하세요.";
            ProductComboBox.Text = "상품명을 선택하세요.";
        }
        //========================================================================



        // 취소 Button 눌렀을때 동작!       --> 190816 박현호
        //=======================================        
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            string fileType = Utils.GetSelectedComboBoxItemText(FileTypeComboBox);

            if (fileType.Equals("바우처 안내 (V)"))
                CompanyComboBox.Enabled = false;
            else
                if (!CompanyComboBox.Enabled)
                    CompanyComboBox.Enabled = true;
            
        }
        //=======================================
    }
}
