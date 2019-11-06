using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Reflection;
using System.Diagnostics;
using TripERP.Common;
using TripERP.Login;
using TripERP.CommonTask;
using TripERP.ReservationMgt;
using TripERP.ProductMgt;
using TripERP.CustomerMgt;
using TripERP.SettlementMgt;
using TripERP.AccountMgt;
using TripERP.SettlementMgt;
using TripERP.Dashboard;
using TripERP.Report;
using TripERP.DocumentMgt;
using TripERP.Invoice;

namespace TripERP
{
    public partial class TripERPmain : Form
    {

        List<Form> formList = new List<Form>();
        List<string> formNameList = new List<string>();
        List<string> menuAuthList = new List<string>(); // 권한이 있는 화면에 대한 코드를 리스트로 저장함.

        private static System.Timers.Timer aTimer;

        private static System.Windows.Forms.Timer timer1; // Set Max Timeout

        public TripERPmain()
        {
            InitializeComponent();
        }


        private void TripERPmain_Load(object sender, EventArgs e)
        {
            Global.mainForm = this;                     

            InitControls();
            LoadCommonCodeInfo();
            OpenLoginForm();

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 팝업창 입금내역 10초마다 확인하는 타이머 09/05 배장훈
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;


            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(this.components);
            timer1.Interval = 600000000;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer1.Enabled = true;
        }

        public void resetTimer()
        {
            timer1.Stop();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("00분간 프로그램을 사용하지 않아 로그아웃 합니다.");
            Debug.WriteLine("Screen locked", "Timer");
            this.Close();
            resetTimer();
        }


        private void InitControls()
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void LoadCommonCodeInfo()
        {
            if (false == Global.LoadCommonCodeInfo())
            {
                MessageBox.Show("공통코드를 가져올 수 없습니다. 프로그램이 종료됩니다.");
                this.Close();
            }
        }

        private void OpenLoginForm()
        {
            // 로그인 출력
            SsLogin form = new SsLogin(this);
            form.MdiParent = this;
            form.Text = "로그인";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();

            TogleContaner(); // 컨테이너 토글
            SetStatus("환영 합니다. 여행ERP에 로그인 해주세요");
        }

        public void TogleContaner()
        {
            this.MenuStrip.Visible = !this.MenuStrip.Visible;
            this.menuStrip1.Visible = !this.menuStrip1.Visible;
            this.ToolStrip.Visible = !this.ToolStrip.Visible;
        }

        // 각 메뉴별 권한 정보를 데이터베이스에서 읽어서 리스트로 저장함.
        public List<string> GetMenuAuthInfo()
        {
            if (Global.loginInfo.EMPL_NO == null) return null;
            
            // 폼 로딩 시 사원번호로 DB에서 검색하여 접근 권한이 있는 메뉴를 읽어 옴.
            string EMPL_NO = Global.loginInfo.EMPL_NO;
            string query = string.Format("CALL SelectAllAuthMenuList ('{0}')", EMPL_NO);

            DataSet dataSet = DbHelper.SelectQuery(query);

            /*
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("권한 정보를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("권한이 있는 화면이 없습니다. 관리자에게 문의하세요.");
                return;
            }
            */

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string SCRN_CD = dataRow["SCRN_CD"].ToString();
                menuAuthList.Add(SCRN_CD);
            }

            //Console.WriteLine(string.Join("\t", menuAuthList));
            return menuAuthList;
        }

        public void SetStatus(string message)
        {
            this.ToolStripStatusLabel.Text = message;
        }






        //===============================================================================
        // 환율관리
        //===============================================================================
        private void exchangeRateMgtMenu_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            breadCrumbLabel.Text = "TripERP > 업무공통 > 환율관리";

            if (menuAuthList.Contains("C110")) 
            { 
                ExchangeRateMgt form = new ExchangeRateMgt();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }









        //===============================================================================
        // 예약목록 G210
        //===============================================================================
        private void reservationListMgtMenu_Click(object sender, EventArgs e)
        {
            OpenReservationListForm();
            /*
            Form form = checkFormOpened("ReservationList");

            if (form == null)
                OpenReservationListtForm();
            else
                form.Activate();   
            */
        }
        
        private void OpenReservationListForm()
        {        
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G210"))
            {
                ReservationList form = new ReservationList();
                form.MdiParent = this;
                form.Text = "예약목록";
                this.Text = "TripERP > 예약관리 > 예약목록";
                breadCrumbLabel.Text = "TripERP > 예약관리 > 예약목록";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //===============================================================================
        // 예약 상세관리 G310
        //===============================================================================
        private void detailReservationMgtMenu_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G310"))
            {
                ReservationDetailInfoMgt form = new ReservationDetailInfoMgt();
                form.MdiParent = this;
                form.Text = "예약관리";
                this.Text = "TripERP > 예약관리 > 예약상세";
                breadCrumbLabel.Text = "TripERP > 예약관리 > 예약상세";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }




        //===============================================================================
        // 입출금관리 G410
        //===============================================================================
        private void depositMgtMenu_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            breadCrumbLabel.Text = "TripERP > 예약관리 > 입금관리";
            if (menuAuthList.Contains("G410"))
            {
                PopUpDepositMgt form = new PopUpDepositMgt();
                form.SetSaveMode(Global.SAVEMODE_ADD);
                //form.SetReservationNumber(_reservationNumber);
                //form.SetBookerName(bookerNameTextBox.Text.Trim());
                //form.SetCustomerNumber(_customerNumber);
                //if (productComboBox.SelectedIndex != -1)
                //    form.SetProductName((productComboBox.SelectedItem as ComboBoxItem).Text);
                //form.SetDepatureDate(departureDateTimePicker.Value.ToString("yyyy-MM-dd"));
                //form.SetSalesPriceTextBox(salesPriceTextBox.Text.Trim());
                //form.SetReservationPriceTextBox(reservationPriceTextBox.Text.Trim());
                //form.SetPartPriceTextBox(partPriceTextBox.Text.Trim());
                //form.SetDepositPriceTextBox(depositPriceTextBox.Text.Trim());
                //form.SetOutstandingPriceTextBox(outstandingPriceTextBox.Text.Trim());
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();

                /*
                if (form.GetTotalDepositPrice() != "")
                    depositPriceTextBox.Text = Utils.SetComma(form.GetTotalDepositPrice());
                if (form.GetReservationPrice() != "")
                    reservationPriceTextBox.Text = Utils.SetComma(form.GetReservationPrice());
                if (form.GetPartPrice() != "")
                    partPriceTextBox.Text = Utils.SetComma(form.GetPartPrice());
                if (form.GetBalanceAmount() != "")
                    outstandingPriceTextBox.Text = Utils.SetComma(form.GetBalanceAmount());
                    */
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //===============================================================================
        // 미지급금관리 G510
        //===============================================================================
        private void unpaidCodeMgtMenu_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G510"))
            {
                UnpaidExpenseMgt form = new UnpaidExpenseMgt();
                form.MdiParent = this;
                form.Text = "미지급관리";
                this.Text = "TripERP > 예약관리 > 미지급관리";
                breadCrumbLabel.Text = "TripERP > 예약관리 > 미지급관리";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //===============================================================================
        // 요율관리 C210
        //===============================================================================
        private void menuFeeMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C210"))
            {
                OpenRateMgtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenRateMgtForm()
        {
            RateMgt form = new RateMgt();
            form.MdiParent = this;
            form.Text = "요율관리";
            this.Text = "TripERP > 업무공통 > 요율관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 요율관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }





        //===============================================================================
        // 공통코드관리 C310
        //===============================================================================
        private void menuCommonCodeMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C310"))
            {
                OpenCommonCodeMgtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenCommonCodeMgtForm()
        {
            CommonCodeMgt form = new CommonCodeMgt();
            form.MdiParent = this;
            form.Text = "공통코드관리";
            this.Text = "TripERP > 업무공통 > 코드관리 > 공통코드관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 코드관리 > 공통코드관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //===============================================================================
        // 국가코드관리 C311
        //===============================================================================
        private void menuNationCodeMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C311"))
            {
                OpenNationCodeMgtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenNationCodeMgtForm()
        {
            NationCodeMgt form = new NationCodeMgt();
            form.MdiParent = this;
            form.Text = "국가코드관리";
            this.Text = "TripERP > 업무공통 > 코드관리 > 국가코드관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 코드관리 > 국가코드관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }








        //===============================================================================
        // 통화코드관리 C312
        //===============================================================================
        private void menuCurrencyCodeMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C312"))
            {
                OpenCurrencyCodeMgtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenCurrencyCodeMgtForm()
        {
            CurrencyCodeMgt form = new CurrencyCodeMgt();
            form.MdiParent = this;
            form.Text = "통화코드관리";
            this.Text = "TripERP > 업무공통 > 코드관리 > 통화코드관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 코드관리 > 통화코드관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }







        //===============================================================================
        // 공항코드관리C313
        //===============================================================================
        private void menuAirportCodeMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C313"))
            {
                OpenAirportInfoMgt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenAirportInfoMgt()
        {
            AirportInfoMgt form = new AirportInfoMgt();
            form.MdiParent = this;
            form.Text = "공항코드관리";
            this.Text = "TripERP > 업무공통 > 코드관리 > 공항코드관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 코드관리 > 공항코드관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //===============================================================================
        // 상품카테고리코드관리 D110
        //===============================================================================
        private void mnuProductCategoryMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("D110"))
            {
                OpenProductCategoryCodeMgtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenProductCategoryCodeMgtForm()
        {
            ProductCategoryMgt form = new ProductCategoryMgt();
            form.MdiParent = this;
            form.Text = "상품카테고리코드관리";
            this.Text = "TripERP > 상품관리 > 상품카테고리코드관리";
            breadCrumbLabel.Text = "TripERP > 상품관리 > 상품카테고리코드관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //===============================================================================
        // 상품기본정보관리
        //===============================================================================
        private void mnuProductInfoMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("D210"))
            {
                OpenProductInfoMgtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenProductInfoMgtForm()
        {
            ProductBasicInfoMgt form = new ProductBasicInfoMgt();
            form.MdiParent = this;
            form.Text = "상품기본정보관리";
            this.Text = "TripERP > 상품관리 > 상품기본정보관리";
            breadCrumbLabel.Text = "TripERP > 상품관리 > 상품기본정보관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }





        //===============================================================================
        // 상품상세정보관리D310
        //===============================================================================
        private void mnuProductDetailMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("D310"))
            {
                OpenProductDetailInfoMgmt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenProductDetailInfoMgmt()
        {
            ProductDetailInfoMgt form = new ProductDetailInfoMgt();
            form.MdiParent = this;
            form.Text = "상품상세정보관리";
            this.Text = "TripERP > 상품관리 > 상품상세정보관리";
            breadCrumbLabel.Text = "TripERP > 상품관리 > 상품상세정보관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }




        //===============================================================================
        // 원가관리 D410
        //===============================================================================
        private void mnuCostPriceMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("D410"))
            {
                OpenCostPriceMgmt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenCostPriceMgmt()
        {
            CostPriceMgt form = new CostPriceMgt();
            form.MdiParent = this;
            form.Text = "원가관리";
            this.Text = "TripERP > 상품관리 > 원가관리";
            breadCrumbLabel.Text = "TripERP > 상품관리 > 원가관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }





        //===============================================================================
        // 옵션관리 G510
        //===============================================================================
        /*
        private void mnuOptionMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G510"))
            {
                OpenOptionMgmt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenOptionMgmt()
        {
            OptionMgt form = new OptionMgt();
            form.MdiParent = this;
            form.Text = "옵션관리";
            this.Text = "TripERP > 상품관리 > 옵션관리";
            breadCrumbLabel.Text = "TripERP > 상품관리 > 옵션관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }
        */

        //===============================================================================
        //프로모션 관리 D610
        //===============================================================================
        private void mnuPromotionMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("D610"))
            {
                OpenPromotionMgmt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenPromotionMgmt()
        {
            ProductPromotionMgt form = new ProductPromotionMgt();
            form.MdiParent = this;
            form.Text = "프로모션 관리";
            this.Text = "TripERP > 상품관리 > 프로모션관리";
            breadCrumbLabel.Text = "TripERP > 상품관리 > 프로모션관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            if (form.IsHandleCreated == true)
            {
                form.BringToFront();
            }
            else
            {
                form.Show();
            }

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //===============================================================================
        // 협력업체기본정보관리 E110
        //===============================================================================
        private void mnuCooperativeCompanyInfoMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("E110"))
            {
                OpenCooperativeCompanyInfoMgt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenCooperativeCompanyInfoMgt()
        {
            CooperativeCompanyInfoMgt form = new CooperativeCompanyInfoMgt();
            form.MdiParent = this;
            form.Text = "협력업체기본정보관리";
            this.Text = "TripERP > 고객관리 > 협력업체기본정보관리";
            breadCrumbLabel.Text = "TripERP > 고객관리 > 협력업체기본정보관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }





        //===============================================================================
        // 로그아웃
        //===============================================================================
        private void mnuLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        //===============================================================================
        // 구매목록등록 G110
        //===============================================================================
        private void mnuPurchaseListMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G110"))
            {
                PurchaseListMgt form = new PurchaseListMgt();
                form.MdiParent = this;
                form.Text = "구매목록등록";
                this.Text = "TripERP > 예약관리 > 구매목록등록";
                breadCrumbLabel.Text = "TripERP > 예약관리 > 구매목록등록";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }







        //===============================================================================
        // 프로모션관리 D610
        //===============================================================================
        private void MnuPromotionMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("D610"))
            {
                ProductPromotionMgt form = new ProductPromotionMgt();
                form.MdiParent = this;
                form.Text = "프로모션관리";
                this.Text = "TripERP > 상품관리 > 프로모션관리";
                breadCrumbLabel.Text = "TripERP > 상품관리 > 프로모션관리";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }







        //===============================================================================
        // 정산관리 H110
        //===============================================================================
        private void mnuClearingConfirmationMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("H110"))
            {
                TripERP.SettlementMgt.SettlementMgt form = new TripERP.SettlementMgt.SettlementMgt();
                form.MdiParent = this;
                form.Text = "정산관리";
                this.Text = "TripERP > 정산관리 > 정산관리";
                breadCrumbLabel.Text = "TripERP > 정산관리 > 정산관리";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }






        //===============================================================================
        // 정산취소 H210
        //===============================================================================
        private void SettlementCancellationMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("H210"))
            {
                SettlementCancellationMgmt form = new SettlementCancellationMgmt();
                form.MdiParent = this;
                form.Text = "정산변경취소";
                this.Text = "TripERP > 정산관리 > 정산변경취소";
                breadCrumbLabel.Text = "TripERP > 정산관리 > 정산변경취소";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }






        //===============================================================================
        // 고객관리 E210
        //===============================================================================
        private void mnuCustomerInfoMgmt_Click(object sender, EventArgs e)
        {   List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("H210"))
            {
                CustomerInfoMgt form = new CustomerInfoMgt();
                form.MdiParent = this;
                form.Text = "고객관리";
                this.Text = "TripERP > 고객관리 > 고객관리";
                breadCrumbLabel.Text = "TripERP > 고객관리 > 고객관리";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }






        //===============================================================================
        // SMS,LMS,EMail 템플릿 C510
        //===============================================================================
        private void mnuTemplateMgtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C510"))
            {
                OpenMessageTemplateMgmtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        // SMS,LMS,EMail 템플릿
        private void OpenMessageTemplateMgmtForm()
        {
            MessageTemplateMgt form = new MessageTemplateMgt();
            form.MdiParent = this;
            form.Text = "문자메시지 템플릿관리";
            this.Text = "TripERP > 업무공통 > 문자메시지 템플릿관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 문자메시지 템플릿관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }







        //===============================================================================
        // 계좌 관리 J110
        //===============================================================================
        private void mnuCurrentAccountMgtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J110"))
            {
                OpenCurrentAccountMgmtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        // 계좌 관리
        private void OpenCurrentAccountMgmtForm()
        {
            CurrentAccountMgmt form = new CurrentAccountMgmt();
            form.MdiParent = this;
            form.Text = "계좌관리";
            this.Text = "TripERP > 자금관리 > 계좌관리";
            breadCrumbLabel.Text = "TripERP > 자금관리 > 계좌관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //===============================================================================
        // 계좌입출금관리
        //===============================================================================
        private void mnuCurrentAccountDepositWithdrawlMgmtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J210"))
            {
                OpenAccountDepositWithdrawalMgmtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }
       
        private void OpenAccountDepositWithdrawalMgmtForm()
        {
            CurrentAccountDepositWithdrawalMgmt form = new CurrentAccountDepositWithdrawalMgmt();
            form.MdiParent = this;
            form.Text = "계좌입출금관리";
            this.Text = "TripERP > 자금관리 > 계좌입출금관리";
            breadCrumbLabel.Text = "TripERP > 자금관리 > 계좌입출금관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //===============================================================================
        // 메뉴정보관리 L410
        //===============================================================================
        private void MgtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("L410"))
            {
                OpenMenuInfoMgmt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenMenuInfoMgmt()
        {
            MenuInfoMgt form = new MenuInfoMgt();
            form.MdiParent = this;
            form.Text = "메뉴관리";
            this.Text = "TripERP > 관리자 > 메뉴관리";
            breadCrumbLabel.Text = "TripERP > 관리자 > 메뉴관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }





        //===============================================================================
        // 직원정보관리 L210
        //===============================================================================
        private void mnuEmployeeInfoMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("L210"))
            {
                OpenEmpInfoMgmt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenEmpInfoMgmt()
        {
            EmployeeInfoMgt form = new EmployeeInfoMgt();
            form.MdiParent = this;
            form.Text = "직원정보관리";
            breadCrumbLabel.Text = "TripERP > 관리자 > 직원정보관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }





        //===============================================================================
        // 발송이력조회 C513
        //===============================================================================
        private void mnuSendHistoryList_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C513"))
            {
                OpenSendHistoryListMgt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        // 발송이력조회 
        private void OpenSendHistoryListMgt()
        {
            SendHistoryList form = new SendHistoryList();
            form.MdiParent = this;
            form.Text = "발송이력조회";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 템플릿관리 > 구매자파일구조관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }





        //===============================================================================
        // 대시보드 --> 190730 배장훈
        //===============================================================================
        private void menuTodayTourSchedule_Click(object sender, EventArgs e)
        {
            Form form = checkFormOpened("DashboardForm");

            if (form == null)
                OpenDashboardForm();
            else
                form.Activate();
        }

        // 대시보드 B110
        public void OpenDashboardForm()
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("B110"))
            {
                DashboardForm form = new DashboardForm();

                form.MdiParent = this;
                form.Text = "마이페이지 > 대시보드";
                breadCrumbLabel.Text = "TripERP > 마이페이지 > 대시보드";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {                    
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }

            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //===============================================================================
        // 메뉴 권한 관리 L310
        //===============================================================================
        private void EmployeeMenuMgtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("L310"))
            {
                openEmployeeMenuInfoMgt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        public void openEmployeeMenuInfoMgt()
        {
            EmployeeMenuInfoMgt form = new EmployeeMenuInfoMgt();
            form.MdiParent = this;
            form.Text = "매뉴권한관리";
            breadCrumbLabel.Text = "TripERP > 관리자 > 메뉴권한관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //======================================================================================================
        // 모든 창 닫기  --> 190730 박현호
        //======================================================================================================
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("모든 창을 닫습니다.", "전체 닫기", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            if (formList.Count == 0)
            {
                MessageBox.Show("열려있는 창이 존재하지 않습니다.");
            }
            while (true)
            {
                if (formList.Count == 0)
                {
                    break;
                }

                formList[formList.Count - 1].Close();
            }
        }





        //======================================================================================================
        // 이미 열려있는 창인지 판별하여 기존창 띄우기 --> 190730 박현호
        //======================================================================================================
        public bool bringFrontExsistForm(Form form, string formText, string labelText)
        {
            bool flag = false;
            if (formNameList.Contains(formText))
            {
                for (int i = 0; i < formList.Count; i++)
                {
                    Form getForm = formList[i];
                    if (form.Text.Equals(getForm.Text))
                    {
                        breadCrumbLabel.Text = labelText;
                        getForm.BringToFront();                        
                        form.Dispose();
                    }
                }
                flag = true;
            }
            return flag;
        }






        //======================================================================================================
        // Form 닫힐시 EventHandler   --> 190730 박현호
        ///======================================================================================================
        /*
         * 모든 WindowForm 은 본 Handler 를 통해 ClosedEventMethod 를 구현한다!!!! (Form 생성시 body 하단에 본 Method 추가 필요!!!!) 
        */
        public void closeForm(Form form)
        {
            form.FormClosed += new FormClosedEventHandler(removedForm);
        }









        //======================================================================================================
        // 계단식 창배열 Menu Button Click Event Handler  --> 190730 박현호
        ///======================================================================================================
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stairPositionForm();
        }

        // 계단식 창 띄우기      
        public void stairPositionForm()
        {
            int posX = 0;
            int posY = 0;

            for (int i = 0; i < formList.Count; i++)
            {
                posX += 25;
                posY += 25;

                Form getForm = formList[i];
                getForm.Dock = DockStyle.None;
                getForm.WindowState = FormWindowState.Normal;
                getForm.AutoScroll = true;
                getForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                getForm.Size = new Size(this.Width / 3, this.Height / 3);
                getForm.Location = new Point(posX, posY);
                getForm.Show();
            }
        }







        //======================================================================================================
        // WindowForm 닫기버튼, 계단식에서 x 를 눌렀을떄 Event Method  --> 190730 - 박현호
        //======================================================================================================
        public void removedForm(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            string formName = form.Text;
            for (int i = 0; i < formList.Count; i++)
            {
                Form getForm = formList[i];
                if (formName.Equals(getForm.Text))
                {
                    formList.RemoveAt(i);
                    formNameList.RemoveAt(i);
                }
            }

            //MessageBox.Show("formList.size : "+ formList.Count+" , formNameList.size : "+formNameList.Count );
        }







        //======================================================================================================
        // 문자메세지 발송 --> 190731 배장훈
        //======================================================================================================
        private void mnuMessageSend_Click(object sender, EventArgs e)
        {
            openMessageSendForm();
        }

        private void openMessageSendForm()
        {
            PopUpSendSMS form = new PopUpSendSMS();
            form.ShowDialog();
            //form.MdiParent = this;
            //form.Text = "문자메세지 발송";
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.Dock = DockStyle.Fill;

            //// 기존에 출력된 창인지 확인
            //if (bringFrontExsistForm(form, form.Text))
            //{
            //    return;
            //}

            //if (form.IsHandleCreated == true)
            //    form.BringToFront();
            //else
            //    form.Show();

            //formList.Add(form);
            //formNameList.Add(form.Text);
            //closeForm(form);
        }





        //======================================================================================================
        // 이메일 발송 --> 190731 배장훈 C512
        //======================================================================================================
        private void mnuEmailSend_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C512"))
            {
                openEmailSendForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void openEmailSendForm()
        {
            PopUpSendMail form = new PopUpSendMail();
            form.ShowDialog();
            //form.MdiParent = this;
            //form.Text = "이메일 발송";
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.Dock = DockStyle.Fill;

            //// 기존에 출력된 창인지 확인
            //if (bringFrontExsistForm(form, form.Text))
            //{
            //    return;
            //}

            //if (form.IsHandleCreated == true)
            //    form.BringToFront();
            //else
            //    form.ShowDialog();

            //formList.Add(form);
            //formNameList.Add(form.Text);
            //closeForm(form);
        }






        //======================================================================================================
        // 계정관리 L110
        //======================================================================================================
        private void mnuAccountInfoMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("L110"))
            {
                openAccountInfoMgt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void openAccountInfoMgt()
        {
            UserAccountMgt form = new UserAccountMgt();
            form.MdiParent = this;
            form.Text = "계정관리";
            breadCrumbLabel.Text = "TripERP > 관리자 > 접속계정관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인 
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //======================================================================================================
        // 협력업체 직원정보 E111
        //======================================================================================================
        private void mnuCooperativeCompanyEmployeeInfoMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("E111"))
            {
                openCooperativeEmployeeInfoMgt();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void openCooperativeEmployeeInfoMgt()
        {
            CooperativeEmployeeInfoMgt form = new CooperativeEmployeeInfoMgt();
            form.MdiParent = this;
            form.Text = "협력업체 직원정보관리";
            breadCrumbLabel.Text = "TripERP > 고객관리 > 협력업체관리 > 협력업체 직원정보";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }







        //======================================================================================================
        // 대시보드 메뉴 클릭
        //======================================================================================================
        private void menuTodayTourSchedule_Click_1(object sender, EventArgs e)
        {
            Form form = checkFormOpened("DashboardForm");

            if (form == null)
                OpenDashboardForm();
            else
                form.Activate();
        }







        //======================================================================================================
        // 지정한 폼이 열려 있는지 체크
        //======================================================================================================
        private static Form checkFormOpened(string formName)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name.Equals(formName))
                {
                    return form;
                }
            }
            return null;
        }






        //======================================================================================================
        // 정산현황보고서     --> 박현호 J215
        //======================================================================================================
        private void mnuSettlementStatusReport_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J215"))
            {
                SettlementReportForm form = new SettlementReportForm();
                form.MdiParent = this;
                form.Text = "정산세부현황 보고서";
                breadCrumbLabel.Text = "TripERP > 보고서 > 현황분석 > 정산세부현황";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }







        //======================================================================================================
        // 정산요약보고서 Menu Click   --> 190819 박현호 J216
        //======================================================================================================
        private void SettlementSummaryReportMenu_Click(object sender, EventArgs e)
        { List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J216"))
            {
                SettlementSummaryReportForm form = new SettlementSummaryReportForm();
                form.MdiParent = this;
                form.Text = "정산요약보고서";
                breadCrumbLabel.Text = "TripERP > 보고서 > 현황분석 > 정산요약";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //======================================================================================================
        // 지급현황보고서  --> 박현호 J214
        //======================================================================================================
        private void mnuPaymentReport_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J214"))
            {
                /*
                PaymentReportForm form = new PaymentReportForm();
                form.MdiParent = this;
                form.Text = "지급현황 보고서";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
                */
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }









        //======================================================================================================
        // 미지급 현황 보고서       --> 190816 박현호 J213
        //======================================================================================================
        private void mnuAccruedexpenseReport_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J213"))
            {

                UnpaidReportForm form = new UnpaidReportForm();
                form.MdiParent = this;
                form.Text = "미지급현황 보고서";
                breadCrumbLabel.Text = "TripERP > 보고서 > 현황분석 > 미지급현황";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }








        //======================================================================================================
        // 바우처 인쇄 I310
        //======================================================================================================
        private void printVoucherMgtMenu_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("I310"))
            {

            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }








        //======================================================================================================
        // 구매자목록파일 구조관리
        //======================================================================================================
        private void mnuPurchaseFileStructureTemplate_Click(object sender, EventArgs e)
        {
            PurchaseListFileInfoMgt form = new PurchaseListFileInfoMgt();
            form.MdiParent = this;
            form.Text = "구매자파일구조관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 템플릿관리 > 구매자파일구조관리";
            //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //======================================================================================================
        // 바우처 C416
        //======================================================================================================
        private void printInvoiceMgtMenu_Click(object sender, EventArgs e)
        {

        }









        //======================================================================================================
        // Template 관리 winForms 출력! --> 190816 박현호 I110
        //======================================================================================================
        private void mnuDocumentTemplateMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("I110"))
            {
                DocumentTemplateMgt form = new DocumentTemplateMgt();
                form.MdiParent = this;
                form.Text = "Template 관리";
                breadCrumbLabel.Text = "TripERP > 문서관리 > 문서템플릿관리";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                //form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                form.Dock = DockStyle.None;
                form.StartPosition = FormStartPosition.CenterScreen;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }






        //======================================================================================================
        // 바우처 인쇄
        //======================================================================================================
        private void mnuVoucherPrintOutMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("I110"))
            {
                VoucherPrintOutMgt form = new VoucherPrintOutMgt();
                form.MdiParent = this;
                form.Text = "바우처 인쇄";
                breadCrumbLabel.Text = "TripERP > 문서관리 > 바우처 인쇄";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                //form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                form.Dock = DockStyle.None;
                form.StartPosition = FormStartPosition.CenterScreen;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //======================================================================================================
        // Invoice Menu --> 190821 박현호
        //======================================================================================================
        private void 인보이스관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G710"))
            {
                InvoiceManageForm form = new InvoiceManageForm();
                form.MdiParent = this;
                form.Text = "Invoice 관리";
                breadCrumbLabel.Text = "TripERP > 정산관리 > Invoice 관리";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.StartPosition = FormStartPosition.CenterScreen;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }








        //======================================================================================================
        // Invoice 인쇄 Menu  --> 190823 박현호
        //======================================================================================================
        private void 인보이스인쇄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G710"))
            {
                InvoiceDocumentForm form = new InvoiceDocumentForm();
                form.MdiParent = this;
                form.Text = "Invoice 인쇄";
                breadCrumbLabel.Text = "TripERP > 문서관리 > Invoice 인쇄";
                //form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.StartPosition = FormStartPosition.CenterScreen;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }






        //======================================================================================================
        // 예약 상세
        //======================================================================================================
        private void reservationDetailInfoMgtMenu_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("G310"))
            {
                ReservationDetailInfoMgt form = new ReservationDetailInfoMgt();
                form.MdiParent = this;
                form.Text = "예약관리";
                this.Text = "TripERP > 예약관리 > 예약상세";
                breadCrumbLabel.Text = "TripERP > 예약관리 > 예약상세";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }
        






        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 팝업창 입금내역 10초마다 확인하는 타이머 09/05 배장훈
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 10);
            dt = dt - ts;

            string query = string.Format("CALL SelectCurrentAccountDepositListByFirstRegistDateTime ('', '', '{0}', '{1}')", dt.ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyyMMddHHmmss"));
            DataSet dataSet = DbHelper.SelectQuery(query);

            if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                if (this.InvokeRequired)
                {// 날짜 이름 타입 금액
                    this.Invoke((MethodInvoker)delegate {

                        DateTime in_dt = new DateTime();
                        DateTime.TryParse(dataRow["FRST_RGST_DTM"].ToString(), null, System.Globalization.DateTimeStyles.AssumeLocal, out in_dt);
                        string strDt = dt.ToString("MM/dd HH:mm ");
                        string TRAN_CUST_NM = dataRow["TRAN_CUST_NM"].ToString() + "\t";        // 거래고객명
                        string REMK_CNTS = dataRow["REMK_CNTS"].ToString() + "\t";
                        string DPWH_WON_AMT = dataRow["DPWH_WON_AMT"].ToString();

                        Alert(strDt + "      " + TRAN_CUST_NM + "\n" + REMK_CNTS + "    " + Utils.SetComma(DPWH_WON_AMT) + "원", DepositNotificationForm.alertTypeEnum.Success);
                    });
                }
            }
        }











        //======================================================================================================
        // 지역코드관리
        //======================================================================================================
        private void menuRegionCodeMgmt_Click(object sender, EventArgs e)
        {

            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("C314"))
            {
                OpenRegionCodeMgtForm();
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }

        private void OpenRegionCodeMgtForm()
        {
            RegionCodeMgmt form = new RegionCodeMgmt();
            form.MdiParent = this;
            form.Text = "지역코드관리";
            this.Text = "TripERP > 업무공통 > 코드관리 > 지역코드관리";
            breadCrumbLabel.Text = "TripERP > 업무공통 > 코드관리 > 지역코드관리";
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // 기존에 출력된 창인지 확인
            if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
            {
                return;
            }

            if (form.IsHandleCreated == true)
                form.BringToFront();
            else
                form.Show();

            formList.Add(form);
            formNameList.Add(form.Text);
            closeForm(form);
        }






        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 팝업창 입금내역 10초마다 확인하는 타이머 09/05 배장훈
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Alert(string msg, DepositNotificationForm.alertTypeEnum type)
        {
            DepositNotificationForm form = new DepositNotificationForm();
            form.setAlert(msg, type);
        }









        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 게시판 자료실 링크
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void BBSToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://gbrdg111.vps.phps.kr:5000/admin");
        }

        private void menuMyPage_DropDownOpened(object sender, EventArgs e) {
          menuMyPage.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void menuMyPage_DropDownClosed(object sender, EventArgs e) {
          menuMyPage.ForeColor = Color.White;
        }

        private void menuBizCommon_DropDownOpened(object sender, EventArgs e) {
          menuBizCommon.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void menuBizCommon_DropDownClosed(object sender, EventArgs e) {
          menuBizCommon.ForeColor = Color.White;
        }

        private void mnuProductMgmt_DropDownOpened(object sender, EventArgs e) {
          mnuProductMgmt.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void mnuProductMgmt_DropDownClosed(object sender, EventArgs e) {
          mnuProductMgmt.ForeColor = Color.White;
        }

        private void mnuCustomerMgmt_DropDownOpened(object sender, EventArgs e) {
          mnuCustomerMgmt.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void mnuCustomerMgmt_DropDownClosed(object sender, EventArgs e) {
          mnuCustomerMgmt.ForeColor = Color.White;
        }

        private void mnuReservationMgmt_DropDownOpened(object sender, EventArgs e) {
          mnuReservationMgmt.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void mnuReservationMgmt_DropDownClosed(object sender, EventArgs e) {
          mnuReservationMgmt.ForeColor = Color.White;
        }

        private void ClearingMgmtToolStripMenuItem_DropDownOpened(object sender, EventArgs e) {
          ClearingMgmtToolStripMenuItem.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void ClearingMgmtToolStripMenuItem_DropDownClosed(object sender, EventArgs e) {
          ClearingMgmtToolStripMenuItem.ForeColor = Color.White;
        }

        private void mnuQuotationMgmt_DropDownOpened(object sender, EventArgs e) {
          mnuQuotationMgmt.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void mnuQuotationMgmt_DropDownClosed(object sender, EventArgs e) {
          mnuQuotationMgmt.ForeColor = Color.White;
        }

        private void DocumentMgmtToolStripMenuItem_DropDownOpened(object sender, EventArgs e) {
          DocumentMgmtToolStripMenuItem.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void DocumentMgmtToolStripMenuItem_DropDownClosed(object sender, EventArgs e) {
          DocumentMgmtToolStripMenuItem.ForeColor = Color.White;
        }

        private void mnuAccountMgmt_DropDownOpened(object sender, EventArgs e) {
          mnuAccountMgmt.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void mnuAccountMgmt_DropDownClosed(object sender, EventArgs e) {
          mnuAccountMgmt.ForeColor = Color.White;
        }

        private void ReportMgmtToolStripMenuItem_DropDownOpened(object sender, EventArgs e) {
          ReportMgmtToolStripMenuItem.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void ReportMgmtToolStripMenuItem_DropDownClosed(object sender, EventArgs e) {
          ReportMgmtToolStripMenuItem.ForeColor = Color.White;
        }

        private void AdminToolStripMenuItem_DropDownOpened(object sender, EventArgs e) {
          AdminToolStripMenuItem.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void AdminToolStripMenuItem_DropDownClosed(object sender, EventArgs e) {
          AdminToolStripMenuItem.ForeColor = Color.White;
        }

        private void WindowsMenu_DropDownOpened(object sender, EventArgs e) {
          WindowsMenu.ForeColor = Color.FromArgb(37, 35, 63);
        }

        private void WindowsMenu_DropDownClosed(object sender, EventArgs e) {
          WindowsMenu.ForeColor = Color.White;
        }








        //======================================================================================================
        // 계정과목관리
        //======================================================================================================
        private void mnuAccountTitleMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J310"))
            {
                AccountTitleMgmt form = new AccountTitleMgmt();
                form.MdiParent = this;
                form.Text = "계정과목관리";
                this.Text = "TripERP > 자금관리 > 계정과목관리";
                breadCrumbLabel.Text = "TripERP > 자금관리 > 계정과목관리";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }




        //======================================================================================================
        // 전표관리
        //======================================================================================================
        private void mnuSlipMgmt_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J311"))
            {
                SlipMgmt form = new SlipMgmt();
                form.MdiParent = this;
                form.Text = "전표관리";
                this.Text = "TripERP > 자금관리 > 전표관리";
                breadCrumbLabel.Text = "TripERP > 자금관리 > 전표관리";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //======================================================================================================
        // 자금수지일보
        //======================================================================================================
        private void mnuFinancialIncomeExpenseReport_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J312"))
            {
                FinancialIncomeExpenseReportMgmt form = new FinancialIncomeExpenseReportMgmt();
                form.MdiParent = this;
                form.Text = "자금수지일보";
                this.Text = "TripERP > 자금관리 > 자금수지일보";
                breadCrumbLabel.Text = "TripERP > 자금관리 > 자금수지일보";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }





        //======================================================================================================
        // 현금보통예금출납장
        //======================================================================================================
        private void mnuReceiptsPaymentReport_Click(object sender, EventArgs e)
        {
            List<string> menuAuthList = GetMenuAuthInfo();
            if (menuAuthList.Contains("J313"))
            {
                ReceiptsPaymentReportMgmt form = new ReceiptsPaymentReportMgmt();
                form.MdiParent = this;
                form.Text = "현금보통예금출납장";
                this.Text = "TripERP > 자금관리 > 현금보통예금출납장";
                breadCrumbLabel.Text = "TripERP > 자금관리 > 현금보통예금출납장";
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                // 기존에 출력된 창인지 확인
                if (bringFrontExsistForm(form, form.Text, breadCrumbLabel.Text))
                {
                    return;
                }

                if (form.IsHandleCreated == true)
                    form.BringToFront();
                else
                    form.Show();

                formList.Add(form);
                formNameList.Add(form.Text);
                closeForm(form);
            }
            else
            {
                MessageBox.Show("해당 메뉴에 대한 권한이 없습니다. 관리자에게 문의하세요.");
            }
        }
    }





    //======================================================================================================
    // DataGridView 렌더링 속도 개선용 DoubleBuffering 선언
    //======================================================================================================
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }


}

