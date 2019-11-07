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
using TripERP.CustomerMgt;
using TripERP.ReservationMgt;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;

namespace TripERP.DocumentMgt
{
    public partial class VoucherPrintOutMgt : Form
    {
        string CNFM_NO;                     // 컨펌번호
        string CUST_NM;                     // 여행자명
        string CUST_ENG_NM;                 // 여행자영문명
        string ADLT_NBR;                    // 인원-ADULT
        string CHLD_NBR;                    // 인원-CHILD
        string DPTR_DT;                     // 탑승일
        string PRDT_GRAD_NM;                // 캐빈타입
        string PRDT_NM;                     // 상품명
        string RSVT_NO;                     // 예약번호
        string PRDT_CNMB { get; set; }      // 상품일련번호

        string _COOP_CMPN_NO { get; set; }

        bool is_solo_travel = false;        // 혼자여행유무 DEFAULT : 0
        bool _IS_FROM_RSVT_DTL = false;     // 예약상세로부터 넘어온지 판별

        private dynamic doc;                // 엑셀 데이터 INSERT 제어

        public VoucherPrintOutMgt()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(closing);

            _COOP_CMPN_NO = "0000";
        }

        private void VoucherPrintOutMgt_Shown(object sender, EventArgs e)
        {
            if (reservationNumberTextBox.Text.Trim() != "")
            {
                getReservationInfo();
            }
        }

        private void printOutVoucherButton_Click(object sender, EventArgs e)
        {
            //try {
            //    this.axFramerControls._PrintOutOld();
            //} catch (Exception ex) { MessageBox.Show(ex.Message); }

            DirectoryInfo di = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\Downloads\");
            string xlsxPath = di + RSVT_NO + "_" + DPTR_DT + "_" + CUST_NM + "_" + PRDT_NM + ".xlsx";
            string pdfPath = di + RSVT_NO + "_" + DPTR_DT + "_" + CUST_NM + "_" + PRDT_NM + ".pdf";

            this.axFramerControls.Save(di.ToString() + RSVT_NO + "_" + DPTR_DT + "_" + CUST_NM + "_" + PRDT_NM, true, "", "");

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var workbooks = excelApplication.Workbooks;
            var workbook = workbooks.Open(xlsxPath);
            workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfPath);

            workbook.Close();
            workbooks.Close();
            excelApplication.Quit();

            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(workbooks);
            Marshal.ReleaseComObject(excelApplication);


            System.Diagnostics.Process.Start(pdfPath);
        }

        // 조회버튼 클릭
        private void searchCustomerButton_Click(object sender, EventArgs e)
        {
            getReservationInfo();
        }

        // 예약번호로 예약정보 조회하여 엑셀에 출력
        private void getReservationInfo()
        {
            RSVT_NO = reservationNumberTextBox.Text.Trim();
            if (RSVT_NO == "") {
                MessageBox.Show("예약번호를 입력해주세요.");
                return;
            }
            
            // 예약정보 검색
            string query = string.Format("CALL SelectRsvtItem ( '{0}' )", RSVT_NO);
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("예약상세정보를 가져올 수 없습니다.");
                return;
            }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            // 최상단 정보
            CNFM_NO = dataRow["CNFM_NO"].ToString();                     // 컨펌번호
            CUST_NM = dataRow["CUST_NM"].ToString();
            CUST_ENG_NM = dataRow["RPRS_ENG_NM"].ToString();            // 여행자명
            ADLT_NBR = dataRow["ADLT_NBR"].ToString();                  // 인원-ADULT
            CHLD_NBR = dataRow["CHLD_NBR"].ToString();                  // 인원-CHILD
            DPTR_DT = dataRow["DPTR_DT"].ToString().Substring(0, 10);   // 탑승일
            PRDT_GRAD_NM = dataRow["PRDT_GRAD_NM"].ToString();          // 캐빈타입
            PRDT_NM = dataRow["PRDT_NM"].ToString();                    //   상품명

            // 어린이 없는경우 CHILD : 0으로 표시
            if (CHLD_NBR.Equals(""))
                CHLD_NBR = "1";

            query = string.Format("CALL SelectAllReservationPartnerList ('{0}')", RSVT_NO);
            dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                is_solo_travel = true;
                //MessageBox.Show("동반자목록 가져올 수 없습니다.");
                // return;
            }

            PRDT_CNMB = dataRow["PRDT_CNMB"].ToString();               // 상품일련번호
            string PRDT_GRAD_CD = dataRow["PRDT_GRAD_CD"].ToString();         // 상품등급코드

            //string xlsPath = Path.Combine("C:\\TripERP\\Vouchers", PRDT_NM + ".xlsx");
            DirectoryInfo di = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\Downloads\");

            string fileNamePath = di + RSVT_NO + "_" + DPTR_DT + "_" + CUST_NM + "_" + PRDT_NM + ".xlsx";
            // 파일명 변경 (하노이통킨쇼 --> 19090216_2019-09-24_조아영_하노이통킨쇼)

            // 문서관리 --> 바우처인쇄로 넘어온 경우
            if (!_IS_FROM_RSVT_DTL) {
                // SFTP로 서버의 바우처 양식 Download후, 열기
                const string Host = "";
                const int Port = ;
                const string Username = "";
                const string Password = "";
                const string Source = "";
                //const string Destination = @"c:\TripERP";

                // 다운로드 경로 : TripERP 프로그램 아래의 Downloads 폴더 안
                di = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\Downloads");

                if (!di.Exists)
                    di.Create();

                var connectionInfo = new KeyboardInteractiveConnectionInfo(Host, Port, Username);

                connectionInfo.AuthenticationPrompt += delegate (object senders, AuthenticationPromptEventArgs ex) {
                    foreach (var prompt in ex.Prompts) {
                        if (prompt.Request.Equals("Password: ", StringComparison.InvariantCultureIgnoreCase)) {
                            prompt.Response = Password;
                        }
                    }
                };

                using (var client = new SftpClient(Host, 15342, Username, Password)) {
                    client.Connect();
                    DownloadDirectory(client, Source, di.ToString());
                }
            }
            
            // 기존의 바우처 삭제후, 새로 갱신
            try {
                if (File.Exists(fileNamePath)) {
                    File.Delete(fileNamePath);
                    System.IO.File.Move(di + "\\V_" + (_COOP_CMPN_NO + "_" + PRDT_CNMB + ".xlsx"), fileNamePath);
                } else {
                    System.IO.File.Move(di + "\\V_" + (_COOP_CMPN_NO + "_" + PRDT_CNMB + ".xlsx"), fileNamePath);
                }
            } catch (IOException e) {
                MessageBox.Show("동일한 바우처템플릿의 엑셀을 종료하시고, 실행하시길 바랍니다.");
                return;
            }
                

            try {
                this.axFramerControls.Open(fileNamePath);
                doc = this.axFramerControls.ActiveDocument;

                // 템플릿 공통 컬럼위치의 정보 SET ~
                // 예약자 정보 - 영문명
                if (!CUST_ENG_NM.Equals(""))
                    doc.ActiveSheet.Range("B3").Cells.value = CUST_ENG_NM;
                else
                    doc.ActiveSheet.Range("B3").Cells.value = CUST_NM;
                // 인원수
                doc.ActiveSheet.Range("D3").Cells.value = int.Parse(ADLT_NBR) + int.Parse(CHLD_NBR) + "명";
                // 일자
                doc.ActiveSheet.Range("B4").Cells.value = DPTR_DT;

                // 양식이 가장 다른 통킨쇼 먼저 예외처리
                if (PRDT_NM.Equals("통킨쇼"))
                    // 상품등급명
                    doc.ActiveSheet.Range("D5").Cells.value = PRDT_GRAD_NM;
                else {
                    // 크루즈 바우처의 공통 부분 SET ~
                    // 상품등급명
                    doc.ActiveSheet.Range("B5").Cells.value = PRDT_GRAD_NM;

                    // 템플릿 종류별 컬럼 위치 SET
                    if (PRDT_NM.Equals("럭셔리 데이 크루즈")) {

                    } else if (PRDT_NM.Equals("프레지던트크루즈")) {

                    }

                    // ======================================================================================================================
                    // 동반자 목록 사라짐 - 템플릿 0905버전
                    // ======================================================================================================================
                    /* 
                    int added_cell_num = 5;
                    // 동반자가 있는경우 SET
                    if (!is_solo_travel)
                    {
                        foreach (DataRow datarow in dataSet.Tables[0].Rows)
                        {
                            if (dataSet.Tables[0].Rows.IndexOf(datarow).Equals(0)) // 0
                                doc.ActiveSheet.Range("C5").Cells.value = datarow["CUST_ENG_NM"];
                            else
                            {
                                if (dataSet.Tables[0].Rows.IndexOf(datarow) % 2 != 0) // [B]1, 3, 5 ~ 
                                    doc.ActiveSheet.Range("B" + (dataSet.Tables[0].Rows.IndexOf(datarow) + added_cell_num).ToString()).Cells.value = datarow["CUST_ENG_NM"];
                                else                                                                // // [C]2, 4, 6 ~ 
                                {
                                    doc.ActiveSheet.Range("C" + (dataSet.Tables[0].Rows.IndexOf(datarow) + (added_cell_num - 1)).ToString()).Cells.value = datarow["CUST_ENG_NM"];
                                    added_cell_num -= 1;
                                }
                            }
                        }
                    }
                    */

                    // ======================================================================================================================
                    // 익스플로러 제외 - 템플릿 0905버전
                    // ======================================================================================================================
                    /*
                    else if (PRDT_NM.Equals("익스플로러데이크루즈"))
                    {
                        // 익스플로러는 CAVIN타입이 존재X
                        // # 3 크루즈 탑승일(출발 일자)
                        doc.ActiveSheet.Range("B9").Cells.value = DPTR_DT;
                    }
                    */
                }
            } catch (Exception ex) {
                MessageBox.Show(PRDT_NM + "에 대한 템플릿파일을 찾을 수 없습니다@.");
                return;
            }
        }

        // 단축키.. 안먹어서 주석
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    Keys key = keyData & ~(Keys.Shift | Keys.Control);

        //    switch (key)
        //    {
        //        case Keys.F9:
        //            // 조합키 사용 시
        //            if ((keyData & Keys.Control) != 0)
        //            {
        //                MessageBox.Show("Ctrl+F");
        //            }
        //            break;

        //        case Keys.F5:
        //            // 단일키 사용시
        //            MessageBox.Show("f5");
        //            break;

        //        default:
        //            MessageBox.Show("지정되지 않은 키입니다.");
        //            return base.ProcessCmdKey(ref msg, keyData);

        //    }
        //    return true;
        //}

        // 폼 종료시 기존의 DsoFramer 종료 필수.
        private void closing(object sender, FormClosingEventArgs e)
        {
            //doc.Close();
            this.axFramerControls.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //int result = this.axFramerControls.SaveAs(DPTR_DT + "_" + CUST_NM + "_" + PRDT_NM, "");
            DirectoryInfo di = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\Downloads\");
            this.axFramerControls.Save(di.ToString() + RSVT_NO + "_" + DPTR_DT + "_" + CUST_NM + "_" + PRDT_NM, true, "", "");

            //if (result == 1)
            //    MessageBox.Show("엑셀저장을 완료했습니다.");
            //else
            //    MessageBox.Show("엑셀저장을 실패했습니다.");
        }

        public void SetBookerName(string IN_BOKR_NAME)
        {
            bookerNameTextBox.Text = IN_BOKR_NAME;
        }

        public void SetReservationNum(string IN_RSV_NUM)
        {
            reservationNumberTextBox.Text = IN_RSV_NUM;
        }

        public void SetCooperationCompanyNo(string IN_COOP_CMPN_NO) {
            this._COOP_CMPN_NO = IN_COOP_CMPN_NO;
            _IS_FROM_RSVT_DTL = true;
        }

        // 종료버튼
        private void button1_Click(object sender, EventArgs e)
        {
            this.axFramerControls.Close();
            this.Close();
        }

        //=========================================================================================================================================================================
        // SFTP ServerFile Download Function_1 [19/09/03 배장훈]
        //=========================================================================================================================================================================
        private void DownloadDirectory(SftpClient client, string source, string destination) {
            string IN_COOP_CMPN_NO = "0000";   // 바우처는 업체별로 동일하므로 0000으로 고정

            string reservationNum = reservationNumberTextBox.Text.Trim();
            string bookerName = bookerNameTextBox.Text.Trim();
            var files = client.ListDirectory(source);

            bool is_template_exist = false;

            foreach (var file in files) {
                // 디렉토리, 심볼릭링크 제외
                if (!file.IsDirectory && !file.IsSymbolicLink) {
                    // /home/gbridge/template/Voucher 경로의 바우처 템플릿 조회
                    if (file.Name.Equals("V_" + IN_COOP_CMPN_NO + "_" + PRDT_CNMB + ".xlsx")) {
                        is_template_exist = true;
                        DownloadFile(client, file, destination);
                        reservationNumberTextBox.Text = reservationNum;
                        bookerNameTextBox.Text = bookerName;
                        _COOP_CMPN_NO = IN_COOP_CMPN_NO;
                        client.Disconnect();
                    }
                }
            }

            string productName = "";
            if (PRDT_CNMB.Equals("19")) {
                productName = "통킨쇼";
            } else if (PRDT_CNMB.Equals("21")) {
                productName = "프레지던트크루즈";
            } else {
                productName = "럭셔리 데이 크루즈";
            }



            if (!is_template_exist)
                MessageBox.Show("'" + productName + "'상품의 바우처 템플릿이 존재하지 않습니다. 운영담당자에게 연락하세요.");
        }

        //=========================================================================================================================================================================
        // SFTP ServerFile Download Function_2 [19/09/03 배장훈]
        //=========================================================================================================================================================================
        private void DownloadFile(SftpClient client, SftpFile file, string directory) {
            DirectoryInfo di = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + @"\Downloads\");

            // cooperativeCompanyComboBox :모객업체번호
            // productComboBox :상품명 일련번호

            string reservationNum = reservationNumberTextBox.Text.Trim();
            string bookerName = bookerNameTextBox.Text.Trim();

            string fileNamePath = di + reservationNum + "_" + DPTR_DT + "_" + bookerName + "_" + file.Name;
            // 파일명 변경 (하노이통킨쇼 --> 19090216_2019-09-24_조아영_하노이통킨쇼)

            using (Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name))) {
                client.DownloadFile(file.FullName, fileStream);
            }
        }
    }
}
