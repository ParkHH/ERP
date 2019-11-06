namespace TripERP.Report
{
    partial class SettlementSummaryReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettlementSummaryReportForm));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.searchArrangementPlaceCompanyComboBox = new System.Windows.Forms.ComboBox();
            this.searchUnpaidTargetListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.searchProductListComboBox = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.searchEndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.searchStartDatePicker = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.bt_close = new Guna.UI.WinForms.GunaAdvenceButton();
            this.groupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "";
            this.reportViewer1.LocalReport.ReportPath = "";
            this.reportViewer1.Location = new System.Drawing.Point(25, 176);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1800, 800);
            this.reportViewer1.TabIndex = 0;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label2.Location = new System.Drawing.Point(737, 39);
            this.Label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 21);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "거래일자";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Location = new System.Drawing.Point(19, 136);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(1821, 845);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Preview";
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
            this.Label13.Location = new System.Drawing.Point(1005, 35);
            this.Label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(19, 20);
            this.Label13.TabIndex = 11;
            this.Label13.Text = "~";
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.searchArrangementPlaceCompanyComboBox);
            this.GroupBox1.Controls.Add(this.searchUnpaidTargetListButton);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.searchProductListComboBox);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.searchEndDatePicker);
            this.GroupBox1.Controls.Add(this.searchStartDatePicker);
            this.GroupBox1.Controls.Add(this.btnSearch);
            this.GroupBox1.Controls.Add(this.Label13);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GroupBox1.Location = new System.Drawing.Point(19, 15);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.GroupBox1.Size = new System.Drawing.Size(1322, 88);
            this.GroupBox1.TabIndex = 24;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "검색";
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
            // searchUnpaidTargetListButton
            // 
            this.searchUnpaidTargetListButton.AnimationHoverSpeed = 0.07F;
            this.searchUnpaidTargetListButton.AnimationSpeed = 0.03F;
            this.searchUnpaidTargetListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchUnpaidTargetListButton.BorderColor = System.Drawing.Color.Black;
            this.searchUnpaidTargetListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchUnpaidTargetListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchUnpaidTargetListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchUnpaidTargetListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchUnpaidTargetListButton.CheckedImage")));
            this.searchUnpaidTargetListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchUnpaidTargetListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchUnpaidTargetListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchUnpaidTargetListButton.ForeColor = System.Drawing.Color.White;
            this.searchUnpaidTargetListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchUnpaidTargetListButton.Image")));
            this.searchUnpaidTargetListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchUnpaidTargetListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchUnpaidTargetListButton.Location = new System.Drawing.Point(1231, 31);
            this.searchUnpaidTargetListButton.Name = "searchUnpaidTargetListButton";
            this.searchUnpaidTargetListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchUnpaidTargetListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchUnpaidTargetListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchUnpaidTargetListButton.OnHoverImage = null;
            this.searchUnpaidTargetListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchUnpaidTargetListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchUnpaidTargetListButton.Size = new System.Drawing.Size(82, 29);
            this.searchUnpaidTargetListButton.TabIndex = 5;
            this.searchUnpaidTargetListButton.Text = "검색";
            this.searchUnpaidTargetListButton.Click += new System.EventHandler(this.searchUnpaidTargetListButton_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label5.Location = new System.Drawing.Point(10, 39);
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
            this.searchProductListComboBox.Location = new System.Drawing.Point(426, 33);
            this.searchProductListComboBox.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.searchProductListComboBox.Name = "searchProductListComboBox";
            this.searchProductListComboBox.Size = new System.Drawing.Size(288, 29);
            this.searchProductListComboBox.TabIndex = 2;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label1.Location = new System.Drawing.Point(363, 39);
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
            this.searchEndDatePicker.Location = new System.Drawing.Point(1034, 33);
            this.searchEndDatePicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchEndDatePicker.Name = "searchEndDatePicker";
            this.searchEndDatePicker.Size = new System.Drawing.Size(180, 29);
            this.searchEndDatePicker.TabIndex = 4;
            // 
            // searchStartDatePicker
            // 
            this.searchStartDatePicker.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchStartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.searchStartDatePicker.Location = new System.Drawing.Point(828, 33);
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
            // gunaResize1
            // 
            this.gunaResize1.TargetForm = this;
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this;
            // 
            // bt_close
            // 
            this.bt_close.AnimationHoverSpeed = 0.07F;
            this.bt_close.AnimationSpeed = 0.03F;
            this.bt_close.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.bt_close.BorderColor = System.Drawing.Color.Black;
            this.bt_close.CheckedBaseColor = System.Drawing.Color.Gray;
            this.bt_close.CheckedBorderColor = System.Drawing.Color.Black;
            this.bt_close.CheckedForeColor = System.Drawing.Color.White;
            this.bt_close.CheckedImage = ((System.Drawing.Image)(resources.GetObject("bt_close.CheckedImage")));
            this.bt_close.CheckedLineColor = System.Drawing.Color.DimGray;
            this.bt_close.FocusedColor = System.Drawing.Color.Empty;
            this.bt_close.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_close.ForeColor = System.Drawing.Color.White;
            this.bt_close.Image = ((System.Drawing.Image)(resources.GetObject("bt_close.Image")));
            this.bt_close.ImageSize = new System.Drawing.Size(20, 20);
            this.bt_close.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_close.Location = new System.Drawing.Point(1728, 95);
            this.bt_close.Name = "bt_close";
            this.bt_close.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.bt_close.OnHoverBorderColor = System.Drawing.Color.Black;
            this.bt_close.OnHoverForeColor = System.Drawing.Color.White;
            this.bt_close.OnHoverImage = null;
            this.bt_close.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_close.OnPressedColor = System.Drawing.Color.Black;
            this.bt_close.Size = new System.Drawing.Size(99, 33);
            this.bt_close.TabIndex = 80;
            this.bt_close.Text = " 닫기";
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // SettlementSummaryReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1858, 996);
            this.Controls.Add(this.bt_close);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettlementSummaryReportForm";
            this.Text = "SettlementSummaryReportForm";
            this.Load += new System.EventHandler(this.SettlementSummaryReportForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button button2;
        internal System.Windows.Forms.Button button3;
        internal System.Windows.Forms.Button button4;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ComboBox searchArrangementPlaceCompanyComboBox;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox searchProductListComboBox;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker searchEndDatePicker;
        internal System.Windows.Forms.DateTimePicker searchStartDatePicker;
        internal System.Windows.Forms.Button btnSearch;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAdvenceButton searchUnpaidTargetListButton;
        private Guna.UI.WinForms.GunaAdvenceButton bt_close;
    }
}