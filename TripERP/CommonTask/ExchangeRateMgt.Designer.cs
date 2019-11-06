namespace TripERP.CommonTask
{
    partial class ExchangeRateMgt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExchangeRateMgt));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Label5 = new System.Windows.Forms.Label();
            this.currencyCodeComboBox = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.notifyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.invoiceExrtTextBox = new TripERP.CustomControl.RealNumberTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.settlementExrtTextBox = new TripERP.CustomControl.RealNumberTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.deleteButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.basiExrtTextBox = new TripERP.CustomControl.RealNumberTextBox();
            this.closeButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.saveButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.resetButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.searchCurrencyCodeComboBox = new System.Windows.Forms.ComboBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.foreignExchangeRateDataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchForeignExchageRateListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.searchNotifyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaDragControl2 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaControlBox3 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foreignExchangeRateDataGridView)).BeginInit();
            this.gunaPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(45, 113);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 21);
            this.Label5.TabIndex = 157;
            this.Label5.Text = "기준환율";
            // 
            // currencyCodeComboBox
            // 
            this.currencyCodeComboBox.FormattingEnabled = true;
            this.currencyCodeComboBox.Location = new System.Drawing.Point(121, 73);
            this.currencyCodeComboBox.Name = "currencyCodeComboBox";
            this.currencyCodeComboBox.Size = new System.Drawing.Size(154, 29);
            this.currencyCodeComboBox.TabIndex = 5;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(45, 77);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(74, 21);
            this.Label4.TabIndex = 155;
            this.Label4.Text = "통화코드";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(45, 40);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(74, 21);
            this.Label3.TabIndex = 155;
            this.Label3.Text = "고시일자";
            // 
            // notifyDateTimePicker
            // 
            this.notifyDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.notifyDateTimePicker.Location = new System.Drawing.Point(121, 37);
            this.notifyDateTimePicker.Name = "notifyDateTimePicker";
            this.notifyDateTimePicker.Size = new System.Drawing.Size(128, 29);
            this.notifyDateTimePicker.TabIndex = 4;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.invoiceExrtTextBox);
            this.GroupBox2.Controls.Add(this.label7);
            this.GroupBox2.Controls.Add(this.settlementExrtTextBox);
            this.GroupBox2.Controls.Add(this.label6);
            this.GroupBox2.Controls.Add(this.deleteButton);
            this.GroupBox2.Controls.Add(this.basiExrtTextBox);
            this.GroupBox2.Controls.Add(this.closeButton);
            this.GroupBox2.Controls.Add(this.Label5);
            this.GroupBox2.Controls.Add(this.saveButton);
            this.GroupBox2.Controls.Add(this.resetButton);
            this.GroupBox2.Controls.Add(this.currencyCodeComboBox);
            this.GroupBox2.Controls.Add(this.Label4);
            this.GroupBox2.Controls.Add(this.Label3);
            this.GroupBox2.Controls.Add(this.notifyDateTimePicker);
            this.GroupBox2.Location = new System.Drawing.Point(773, 61);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(445, 297);
            this.GroupBox2.TabIndex = 5;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "환율등록";
            // 
            // invoiceExrtTextBox
            // 
            this.invoiceExrtTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.invoiceExrtTextBox.Location = new System.Drawing.Point(121, 181);
            this.invoiceExrtTextBox.Name = "invoiceExrtTextBox";
            this.invoiceExrtTextBox.Size = new System.Drawing.Size(97, 29);
            this.invoiceExrtTextBox.TabIndex = 8;
            this.invoiceExrtTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 21);
            this.label7.TabIndex = 281;
            this.label7.Text = "인보이스환율";
            // 
            // settlementExrtTextBox
            // 
            this.settlementExrtTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.settlementExrtTextBox.Location = new System.Drawing.Point(121, 145);
            this.settlementExrtTextBox.Name = "settlementExrtTextBox";
            this.settlementExrtTextBox.Size = new System.Drawing.Size(97, 29);
            this.settlementExrtTextBox.TabIndex = 7;
            this.settlementExrtTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 279;
            this.label6.Text = "정산환율";
            // 
            // deleteButton
            // 
            this.deleteButton.AnimationHoverSpeed = 0.07F;
            this.deleteButton.AnimationSpeed = 0.03F;
            this.deleteButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.deleteButton.BorderColor = System.Drawing.Color.Black;
            this.deleteButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.deleteButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.deleteButton.CheckedForeColor = System.Drawing.Color.White;
            this.deleteButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("deleteButton.CheckedImage")));
            this.deleteButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.deleteButton.FocusedColor = System.Drawing.Color.Empty;
            this.deleteButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageSize = new System.Drawing.Size(20, 20);
            this.deleteButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.deleteButton.Location = new System.Drawing.Point(224, 250);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.deleteButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.deleteButton.OnHoverForeColor = System.Drawing.Color.White;
            this.deleteButton.OnHoverImage = null;
            this.deleteButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.deleteButton.OnPressedColor = System.Drawing.Color.Black;
            this.deleteButton.Size = new System.Drawing.Size(99, 33);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = " 삭제";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // basiExrtTextBox
            // 
            this.basiExrtTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.basiExrtTextBox.Location = new System.Drawing.Point(121, 109);
            this.basiExrtTextBox.Name = "basiExrtTextBox";
            this.basiExrtTextBox.Size = new System.Drawing.Size(97, 29);
            this.basiExrtTextBox.TabIndex = 6;
            this.basiExrtTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // closeButton
            // 
            this.closeButton.AnimationHoverSpeed = 0.07F;
            this.closeButton.AnimationSpeed = 0.03F;
            this.closeButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.closeButton.BorderColor = System.Drawing.Color.Black;
            this.closeButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.closeButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.closeButton.CheckedForeColor = System.Drawing.Color.White;
            this.closeButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("closeButton.CheckedImage")));
            this.closeButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.closeButton.FocusedColor = System.Drawing.Color.Empty;
            this.closeButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Image = ((System.Drawing.Image)(resources.GetObject("closeButton.Image")));
            this.closeButton.ImageSize = new System.Drawing.Size(20, 20);
            this.closeButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.closeButton.Location = new System.Drawing.Point(329, 250);
            this.closeButton.Name = "closeButton";
            this.closeButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.closeButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.closeButton.OnHoverForeColor = System.Drawing.Color.White;
            this.closeButton.OnHoverImage = null;
            this.closeButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.closeButton.OnPressedColor = System.Drawing.Color.Black;
            this.closeButton.Size = new System.Drawing.Size(99, 33);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = " 닫기";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.AnimationHoverSpeed = 0.07F;
            this.saveButton.AnimationSpeed = 0.03F;
            this.saveButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.saveButton.BorderColor = System.Drawing.Color.Black;
            this.saveButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.saveButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.saveButton.CheckedForeColor = System.Drawing.Color.White;
            this.saveButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("saveButton.CheckedImage")));
            this.saveButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.saveButton.FocusedColor = System.Drawing.Color.Empty;
            this.saveButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageSize = new System.Drawing.Size(20, 20);
            this.saveButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.saveButton.Location = new System.Drawing.Point(119, 250);
            this.saveButton.Name = "saveButton";
            this.saveButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.saveButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.saveButton.OnHoverForeColor = System.Drawing.Color.White;
            this.saveButton.OnHoverImage = null;
            this.saveButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.saveButton.OnPressedColor = System.Drawing.Color.Black;
            this.saveButton.Size = new System.Drawing.Size(99, 33);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = " 저장";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.AnimationHoverSpeed = 0.07F;
            this.resetButton.AnimationSpeed = 0.03F;
            this.resetButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.resetButton.BorderColor = System.Drawing.Color.Black;
            this.resetButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.resetButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.resetButton.CheckedForeColor = System.Drawing.Color.White;
            this.resetButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("resetButton.CheckedImage")));
            this.resetButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.resetButton.FocusedColor = System.Drawing.Color.Empty;
            this.resetButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.resetButton.ForeColor = System.Drawing.Color.White;
            this.resetButton.Image = ((System.Drawing.Image)(resources.GetObject("resetButton.Image")));
            this.resetButton.ImageSize = new System.Drawing.Size(20, 20);
            this.resetButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.resetButton.Location = new System.Drawing.Point(14, 250);
            this.resetButton.Name = "resetButton";
            this.resetButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.resetButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.resetButton.OnHoverForeColor = System.Drawing.Color.White;
            this.resetButton.OnHoverImage = null;
            this.resetButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.resetButton.OnPressedColor = System.Drawing.Color.Black;
            this.resetButton.Size = new System.Drawing.Size(99, 33);
            this.resetButton.TabIndex = 9;
            this.resetButton.Text = "초기화";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // searchCurrencyCodeComboBox
            // 
            this.searchCurrencyCodeComboBox.FormattingEnabled = true;
            this.searchCurrencyCodeComboBox.Location = new System.Drawing.Point(325, 23);
            this.searchCurrencyCodeComboBox.Name = "searchCurrencyCodeComboBox";
            this.searchCurrencyCodeComboBox.Size = new System.Drawing.Size(128, 29);
            this.searchCurrencyCodeComboBox.TabIndex = 2;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.foreignExchangeRateDataGridView);
            this.GroupBox1.Controls.Add(this.searchForeignExchageRateListButton);
            this.GroupBox1.Controls.Add(this.searchCurrencyCodeComboBox);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.searchNotifyDateTimePicker);
            this.GroupBox1.Location = new System.Drawing.Point(13, 61);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(736, 597);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "환율조회";
            // 
            // foreignExchangeRateDataGridView
            // 
            this.foreignExchangeRateDataGridView.AllowUserToAddRows = false;
            this.foreignExchangeRateDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.foreignExchangeRateDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.foreignExchangeRateDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.foreignExchangeRateDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.foreignExchangeRateDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.foreignExchangeRateDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.foreignExchangeRateDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.foreignExchangeRateDataGridView.ColumnHeadersHeight = 30;
            this.foreignExchangeRateDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column2,
            this.Column1});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.foreignExchangeRateDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.foreignExchangeRateDataGridView.EnableHeadersVisualStyles = false;
            this.foreignExchangeRateDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.foreignExchangeRateDataGridView.Location = new System.Drawing.Point(13, 66);
            this.foreignExchangeRateDataGridView.Name = "foreignExchangeRateDataGridView";
            this.foreignExchangeRateDataGridView.RowHeadersVisible = false;
            this.foreignExchangeRateDataGridView.RowTemplate.Height = 23;
            this.foreignExchangeRateDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.foreignExchangeRateDataGridView.Size = new System.Drawing.Size(705, 525);
            this.foreignExchangeRateDataGridView.TabIndex = 5;
            this.foreignExchangeRateDataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.foreignExchangeRateDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.foreignExchangeRateDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.foreignExchangeRateDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.foreignExchangeRateDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.foreignExchangeRateDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.foreignExchangeRateDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.foreignExchangeRateDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.foreignExchangeRateDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.foreignExchangeRateDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.foreignExchangeRateDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.foreignExchangeRateDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.foreignExchangeRateDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.foreignExchangeRateDataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.foreignExchangeRateDataGridView.ThemeStyle.ReadOnly = false;
            this.foreignExchangeRateDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.foreignExchangeRateDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.foreignExchangeRateDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.foreignExchangeRateDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.foreignExchangeRateDataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.foreignExchangeRateDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.foreignExchangeRateDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.foreignExchangeRateDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.foreignExchangeRateDataGridView_CellClick);
            // 
            // Column4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "고시일자";
            this.Column4.MinimumWidth = 150;
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column5.HeaderText = "통화코드";
            this.Column5.MinimumWidth = 110;
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column6.FillWeight = 120F;
            this.Column6.HeaderText = "기준환율";
            this.Column6.MinimumWidth = 120;
            this.Column6.Name = "Column6";
            // 
            // Column2
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column2.FillWeight = 120F;
            this.Column2.HeaderText = "정산환율";
            this.Column2.MinimumWidth = 120;
            this.Column2.Name = "Column2";
            // 
            // Column1
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column1.FillWeight = 120F;
            this.Column1.HeaderText = "인보이스환율";
            this.Column1.MinimumWidth = 120;
            this.Column1.Name = "Column1";
            // 
            // searchForeignExchageRateListButton
            // 
            this.searchForeignExchageRateListButton.AnimationHoverSpeed = 0.07F;
            this.searchForeignExchageRateListButton.AnimationSpeed = 0.03F;
            this.searchForeignExchageRateListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchForeignExchageRateListButton.BorderColor = System.Drawing.Color.Black;
            this.searchForeignExchageRateListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchForeignExchageRateListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchForeignExchageRateListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchForeignExchageRateListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchForeignExchageRateListButton.CheckedImage")));
            this.searchForeignExchageRateListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchForeignExchageRateListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchForeignExchageRateListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchForeignExchageRateListButton.ForeColor = System.Drawing.Color.White;
            this.searchForeignExchageRateListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchForeignExchageRateListButton.Image")));
            this.searchForeignExchageRateListButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchForeignExchageRateListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchForeignExchageRateListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchForeignExchageRateListButton.Location = new System.Drawing.Point(463, 23);
            this.searchForeignExchageRateListButton.Name = "searchForeignExchageRateListButton";
            this.searchForeignExchageRateListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchForeignExchageRateListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchForeignExchageRateListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchForeignExchageRateListButton.OnHoverImage = null;
            this.searchForeignExchageRateListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchForeignExchageRateListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchForeignExchageRateListButton.Size = new System.Drawing.Size(35, 29);
            this.searchForeignExchageRateListButton.TabIndex = 3;
            this.searchForeignExchageRateListButton.Click += new System.EventHandler(this.searchForeignExchageRateListButton_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(247, 26);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 21);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "통화코드";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(23, 26);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(74, 21);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "고시일자";
            // 
            // searchNotifyDateTimePicker
            // 
            this.searchNotifyDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.searchNotifyDateTimePicker.Location = new System.Drawing.Point(101, 23);
            this.searchNotifyDateTimePicker.Name = "searchNotifyDateTimePicker";
            this.searchNotifyDateTimePicker.Size = new System.Drawing.Size(128, 29);
            this.searchNotifyDateTimePicker.TabIndex = 1;
            // 
            // gunaResize1
            // 
            this.gunaResize1.TargetForm = this;
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this;
            // 
            // gunaDragControl2
            // 
            this.gunaDragControl2.TargetControl = this.gunaPanel1;
            // 
            // gunaPanel1
            // 
            this.gunaPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.gunaPanel1.Controls.Add(this.gunaLabel1);
            this.gunaPanel1.Controls.Add(this.gunaControlBox3);
            this.gunaPanel1.Controls.Add(this.gunaControlBox2);
            this.gunaPanel1.Controls.Add(this.gunaControlBox1);
            this.gunaPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gunaPanel1.Location = new System.Drawing.Point(0, 0);
            this.gunaPanel1.Name = "gunaPanel1";
            this.gunaPanel1.Size = new System.Drawing.Size(1234, 32);
            this.gunaPanel1.TabIndex = 300;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.gunaLabel1.ForeColor = System.Drawing.Color.White;
            this.gunaLabel1.Location = new System.Drawing.Point(8, 5);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(100, 23);
            this.gunaLabel1.TabIndex = 3;
            this.gunaLabel1.Text = "환율관리";
            // 
            // gunaControlBox3
            // 
            this.gunaControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox3.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox3.AnimationSpeed = 0.03F;
            this.gunaControlBox3.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
            this.gunaControlBox3.IconColor = System.Drawing.Color.White;
            this.gunaControlBox3.IconSize = 15F;
            this.gunaControlBox3.Location = new System.Drawing.Point(1129, 2);
            this.gunaControlBox3.Name = "gunaControlBox3";
            this.gunaControlBox3.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox3.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox3.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox3.Size = new System.Drawing.Size(30, 30);
            this.gunaControlBox3.TabIndex = 2;
            // 
            // gunaControlBox2
            // 
            this.gunaControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox2.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox2.AnimationSpeed = 0.03F;
            this.gunaControlBox2.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MaximizeBox;
            this.gunaControlBox2.IconColor = System.Drawing.Color.White;
            this.gunaControlBox2.IconSize = 15F;
            this.gunaControlBox2.Location = new System.Drawing.Point(1165, 2);
            this.gunaControlBox2.Name = "gunaControlBox2";
            this.gunaControlBox2.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox2.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox2.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox2.Size = new System.Drawing.Size(30, 30);
            this.gunaControlBox2.TabIndex = 1;
            // 
            // gunaControlBox1
            // 
            this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox1.AnimationSpeed = 0.03F;
            this.gunaControlBox1.IconColor = System.Drawing.Color.White;
            this.gunaControlBox1.IconSize = 15F;
            this.gunaControlBox1.Location = new System.Drawing.Point(1201, 2);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(30, 30);
            this.gunaControlBox1.TabIndex = 0;
            // 
            // ExchangeRateMgt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1234, 669);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ExchangeRateMgt";
            this.Text = "환율조회";
            this.Load += new System.EventHandler(this.ForeignExchangeRateMgmt_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foreignExchangeRateDataGridView)).EndInit();
            this.gunaPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal TripERP.CustomControl.RealNumberTextBox basiExrtTextBox;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox currencyCodeComboBox;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DateTimePicker notifyDateTimePicker;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.ComboBox searchCurrencyCodeComboBox;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker searchNotifyDateTimePicker;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAdvenceButton deleteButton;
        private Guna.UI.WinForms.GunaAdvenceButton closeButton;
        private Guna.UI.WinForms.GunaAdvenceButton saveButton;
        private Guna.UI.WinForms.GunaAdvenceButton resetButton;
        private Guna.UI.WinForms.GunaDataGridView foreignExchangeRateDataGridView;
        private Guna.UI.WinForms.GunaAdvenceButton searchForeignExchageRateListButton;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl2;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox3;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        internal CustomControl.RealNumberTextBox invoiceExrtTextBox;
        internal System.Windows.Forms.Label label7;
        internal CustomControl.RealNumberTextBox settlementExrtTextBox;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}