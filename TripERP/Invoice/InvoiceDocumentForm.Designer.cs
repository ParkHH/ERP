namespace TripERP.Invoice
{
    partial class InvoiceDocumentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceDocumentForm));
            this.searchArrangementPlaceCompanyComboBox = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.searchProductListComboBox = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.searchEndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.searchStartDatePicker = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.searchSettlementTargetListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button6 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.label8 = new System.Windows.Forms.Label();
            this.button7 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.tb_date = new System.Windows.Forms.TextBox();
            this.tb_year = new System.Windows.Forms.TextBox();
            this.tb_month = new System.Windows.Forms.TextBox();
            this.cb_account_foreign = new System.Windows.Forms.ComboBox();
            this.cb_account_won = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_foreign_account = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_won_account = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.button5 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.groupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchArrangementPlaceCompanyComboBox
            // 
            this.searchArrangementPlaceCompanyComboBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchArrangementPlaceCompanyComboBox.FormattingEnabled = true;
            this.searchArrangementPlaceCompanyComboBox.Items.AddRange(new object[] {
            "바라랜드"});
            this.searchArrangementPlaceCompanyComboBox.Location = new System.Drawing.Point(89, 33);
            this.searchArrangementPlaceCompanyComboBox.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.searchArrangementPlaceCompanyComboBox.Name = "searchArrangementPlaceCompanyComboBox";
            this.searchArrangementPlaceCompanyComboBox.Size = new System.Drawing.Size(247, 29);
            this.searchArrangementPlaceCompanyComboBox.TabIndex = 1;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label5.Location = new System.Drawing.Point(12, 37);
            this.Label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 21);
            this.Label5.TabIndex = 157;
            this.Label5.Text = "모객업체";
            // 
            // searchProductListComboBox
            // 
            this.searchProductListComboBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchProductListComboBox.FormattingEnabled = true;
            this.searchProductListComboBox.Items.AddRange(new object[] {
            "전체",
            "프레지던트 크루즈 1박2일",
            "베트남 하노이 통킨쇼"});
            this.searchProductListComboBox.Location = new System.Drawing.Point(403, 33);
            this.searchProductListComboBox.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.searchProductListComboBox.Name = "searchProductListComboBox";
            this.searchProductListComboBox.Size = new System.Drawing.Size(288, 29);
            this.searchProductListComboBox.TabIndex = 2;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label1.Location = new System.Drawing.Point(357, 36);
            this.Label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(42, 21);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "상품";
            // 
            // searchEndDatePicker
            // 
            this.searchEndDatePicker.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchEndDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.searchEndDatePicker.Location = new System.Drawing.Point(1001, 32);
            this.searchEndDatePicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchEndDatePicker.Name = "searchEndDatePicker";
            this.searchEndDatePicker.Size = new System.Drawing.Size(180, 29);
            this.searchEndDatePicker.TabIndex = 4;
            // 
            // searchStartDatePicker
            // 
            this.searchStartDatePicker.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchStartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.searchStartDatePicker.Location = new System.Drawing.Point(795, 32);
            this.searchStartDatePicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchStartDatePicker.Name = "searchStartDatePicker";
            this.searchStartDatePicker.Size = new System.Drawing.Size(171, 29);
            this.searchStartDatePicker.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(2762, 74);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(43, 30);
            this.btnSearch.TabIndex = 42;
            this.btnSearch.Text = "조회";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2227, 1108);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 51);
            this.button1.TabIndex = 5;
            this.button1.Text = "닫기";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1876, 1108);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 51);
            this.button2.TabIndex = 4;
            this.button2.Text = "정산실행";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(2110, 1108);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 51);
            this.button3.TabIndex = 2;
            this.button3.Text = "인쇄";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1993, 1108);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 51);
            this.button4.TabIndex = 1;
            this.button4.Text = "엑셀";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label13.Location = new System.Drawing.Point(974, 39);
            this.Label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(19, 20);
            this.Label13.TabIndex = 11;
            this.Label13.Text = "~";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1404, 1108);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 29);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Location = new System.Drawing.Point(15, 137);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(1372, 845);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Invoice Preview";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1266, 1113);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "정산기준환율";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label2.Location = new System.Drawing.Point(718, 36);
            this.Label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 21);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "출발일자";
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "";
            this.reportViewer1.LocalReport.ReportPath = "";
            this.reportViewer1.Location = new System.Drawing.Point(25, 176);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1440, 680);
            this.reportViewer1.TabIndex = 0;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.searchSettlementTargetListButton);
            this.GroupBox1.Controls.Add(this.searchArrangementPlaceCompanyComboBox);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.searchProductListComboBox);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.searchEndDatePicker);
            this.GroupBox1.Controls.Add(this.searchStartDatePicker);
            this.GroupBox1.Controls.Add(this.btnSearch);
            this.GroupBox1.Controls.Add(this.Label13);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GroupBox1.Location = new System.Drawing.Point(15, 16);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.GroupBox1.Size = new System.Drawing.Size(1293, 88);
            this.GroupBox1.TabIndex = 21;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "검색";
            // 
            // searchSettlementTargetListButton
            // 
            this.searchSettlementTargetListButton.AnimationHoverSpeed = 0.07F;
            this.searchSettlementTargetListButton.AnimationSpeed = 0.03F;
            this.searchSettlementTargetListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchSettlementTargetListButton.BorderColor = System.Drawing.Color.Black;
            this.searchSettlementTargetListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchSettlementTargetListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchSettlementTargetListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchSettlementTargetListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchSettlementTargetListButton.CheckedImage")));
            this.searchSettlementTargetListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchSettlementTargetListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchSettlementTargetListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchSettlementTargetListButton.ForeColor = System.Drawing.Color.White;
            this.searchSettlementTargetListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchSettlementTargetListButton.Image")));
            this.searchSettlementTargetListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchSettlementTargetListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchSettlementTargetListButton.Location = new System.Drawing.Point(1188, 32);
            this.searchSettlementTargetListButton.Name = "searchSettlementTargetListButton";
            this.searchSettlementTargetListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchSettlementTargetListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchSettlementTargetListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchSettlementTargetListButton.OnHoverImage = null;
            this.searchSettlementTargetListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchSettlementTargetListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchSettlementTargetListButton.Size = new System.Drawing.Size(82, 29);
            this.searchSettlementTargetListButton.TabIndex = 5;
            this.searchSettlementTargetListButton.Text = "검색";
            this.searchSettlementTargetListButton.Click += new System.EventHandler(this.searchSettlementTargetListButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.tb_date);
            this.groupBox3.Controls.Add(this.tb_year);
            this.groupBox3.Controls.Add(this.tb_month);
            this.groupBox3.Controls.Add(this.cb_account_foreign);
            this.groupBox3.Controls.Add(this.cb_account_won);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tb_foreign_account);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tb_won_account);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(1406, 137);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(446, 337);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "입력값";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(313, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 21);
            this.label10.TabIndex = 13;
            this.label10.Text = "일";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(204, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 21);
            this.label9.TabIndex = 12;
            this.label9.Text = "월";
            // 
            // button6
            // 
            this.button6.AnimationHoverSpeed = 0.07F;
            this.button6.AnimationSpeed = 0.03F;
            this.button6.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.button6.BorderColor = System.Drawing.Color.Black;
            this.button6.CheckedBaseColor = System.Drawing.Color.Gray;
            this.button6.CheckedBorderColor = System.Drawing.Color.Black;
            this.button6.CheckedForeColor = System.Drawing.Color.White;
            this.button6.CheckedImage = ((System.Drawing.Image)(resources.GetObject("button6.CheckedImage")));
            this.button6.CheckedLineColor = System.Drawing.Color.DimGray;
            this.button6.FocusedColor = System.Drawing.Color.Empty;
            this.button6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageSize = new System.Drawing.Size(20, 20);
            this.button6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button6.Location = new System.Drawing.Point(169, 282);
            this.button6.Name = "button6";
            this.button6.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.button6.OnHoverBorderColor = System.Drawing.Color.Black;
            this.button6.OnHoverForeColor = System.Drawing.Color.White;
            this.button6.OnHoverImage = null;
            this.button6.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button6.OnPressedColor = System.Drawing.Color.Black;
            this.button6.Size = new System.Drawing.Size(99, 33);
            this.button6.TabIndex = 13;
            this.button6.Text = "초기화";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(96, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 21);
            this.label8.TabIndex = 11;
            this.label8.Text = "년";
            // 
            // button7
            // 
            this.button7.AnimationHoverSpeed = 0.07F;
            this.button7.AnimationSpeed = 0.03F;
            this.button7.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.button7.BorderColor = System.Drawing.Color.Black;
            this.button7.CheckedBaseColor = System.Drawing.Color.Gray;
            this.button7.CheckedBorderColor = System.Drawing.Color.Black;
            this.button7.CheckedForeColor = System.Drawing.Color.White;
            this.button7.CheckedImage = ((System.Drawing.Image)(resources.GetObject("button7.CheckedImage")));
            this.button7.CheckedLineColor = System.Drawing.Color.DimGray;
            this.button7.FocusedColor = System.Drawing.Color.Empty;
            this.button7.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageSize = new System.Drawing.Size(20, 20);
            this.button7.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button7.Location = new System.Drawing.Point(283, 282);
            this.button7.Name = "button7";
            this.button7.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.button7.OnHoverBorderColor = System.Drawing.Color.Black;
            this.button7.OnHoverForeColor = System.Drawing.Color.White;
            this.button7.OnHoverImage = null;
            this.button7.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button7.OnPressedColor = System.Drawing.Color.Black;
            this.button7.Size = new System.Drawing.Size(99, 33);
            this.button7.TabIndex = 14;
            this.button7.Text = " 적용";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // tb_date
            // 
            this.tb_date.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.tb_date.Location = new System.Drawing.Point(241, 66);
            this.tb_date.Name = "tb_date";
            this.tb_date.Size = new System.Drawing.Size(69, 29);
            this.tb_date.TabIndex = 8;
            this.tb_date.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // tb_year
            // 
            this.tb_year.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.tb_year.Location = new System.Drawing.Point(25, 66);
            this.tb_year.Name = "tb_year";
            this.tb_year.Size = new System.Drawing.Size(69, 29);
            this.tb_year.TabIndex = 6;
            // 
            // tb_month
            // 
            this.tb_month.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.tb_month.Location = new System.Drawing.Point(133, 66);
            this.tb_month.Name = "tb_month";
            this.tb_month.Size = new System.Drawing.Size(69, 29);
            this.tb_month.TabIndex = 7;
            // 
            // cb_account_foreign
            // 
            this.cb_account_foreign.FormattingEnabled = true;
            this.cb_account_foreign.Location = new System.Drawing.Point(25, 215);
            this.cb_account_foreign.Name = "cb_account_foreign";
            this.cb_account_foreign.Size = new System.Drawing.Size(157, 29);
            this.cb_account_foreign.TabIndex = 11;
            // 
            // cb_account_won
            // 
            this.cb_account_won.FormattingEnabled = true;
            this.cb_account_won.Location = new System.Drawing.Point(25, 140);
            this.cb_account_won.Name = "cb_account_won";
            this.cb_account_won.Size = new System.Drawing.Size(157, 29);
            this.cb_account_won.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 5;
            this.label7.Text = "외화계좌";
            // 
            // tb_foreign_account
            // 
            this.tb_foreign_account.Location = new System.Drawing.Point(188, 215);
            this.tb_foreign_account.Name = "tb_foreign_account";
            this.tb_foreign_account.Size = new System.Drawing.Size(226, 29);
            this.tb_foreign_account.TabIndex = 12;
            this.tb_foreign_account.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "원화계좌";
            // 
            // tb_won_account
            // 
            this.tb_won_account.Location = new System.Drawing.Point(188, 140);
            this.tb_won_account.Name = "tb_won_account";
            this.tb_won_account.Size = new System.Drawing.Size(226, 29);
            this.tb_won_account.TabIndex = 10;
            this.tb_won_account.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "납기일";
            // 
            // gunaResize1
            // 
            this.gunaResize1.TargetForm = this;
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this;
            // 
            // button5
            // 
            this.button5.AnimationHoverSpeed = 0.07F;
            this.button5.AnimationSpeed = 0.03F;
            this.button5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.button5.BorderColor = System.Drawing.Color.Black;
            this.button5.CheckedBaseColor = System.Drawing.Color.Gray;
            this.button5.CheckedBorderColor = System.Drawing.Color.Black;
            this.button5.CheckedForeColor = System.Drawing.Color.White;
            this.button5.CheckedImage = ((System.Drawing.Image)(resources.GetObject("button5.CheckedImage")));
            this.button5.CheckedLineColor = System.Drawing.Color.DimGray;
            this.button5.FocusedColor = System.Drawing.Color.Empty;
            this.button5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageSize = new System.Drawing.Size(20, 20);
            this.button5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button5.Location = new System.Drawing.Point(1736, 88);
            this.button5.Name = "button5";
            this.button5.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.button5.OnHoverBorderColor = System.Drawing.Color.Black;
            this.button5.OnHoverForeColor = System.Drawing.Color.White;
            this.button5.OnHoverImage = null;
            this.button5.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button5.OnPressedColor = System.Drawing.Color.Black;
            this.button5.Size = new System.Drawing.Size(99, 33);
            this.button5.TabIndex = 15;
            this.button5.Text = " 닫기";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // InvoiceDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "InvoiceDocumentForm";
            this.Text = "InvoiceDocumentForm";
            this.Load += new System.EventHandler(this.InvoiceDocumentForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ComboBox searchArrangementPlaceCompanyComboBox;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox searchProductListComboBox;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker searchEndDatePicker;
        internal System.Windows.Forms.DateTimePicker searchStartDatePicker;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button button2;
        internal System.Windows.Forms.Button button3;
        internal System.Windows.Forms.Button button4;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label Label2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_foreign_account;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_won_account;
        private System.Windows.Forms.ComboBox cb_account_foreign;
        private System.Windows.Forms.ComboBox cb_account_won;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_date;
        private System.Windows.Forms.TextBox tb_year;
        private System.Windows.Forms.TextBox tb_month;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAdvenceButton button7;
        private Guna.UI.WinForms.GunaAdvenceButton button6;
        private Guna.UI.WinForms.GunaAdvenceButton button5;
        private Guna.UI.WinForms.GunaAdvenceButton searchSettlementTargetListButton;
    }
}