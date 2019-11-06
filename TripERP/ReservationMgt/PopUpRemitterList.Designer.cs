namespace TripERP.ReservationMgt
{
    partial class PopUpRemitterList
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
            System.Windows.Forms.Label RSVT_CNMBLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpRemitterList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.searchDepositListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.depositTypeComboBox = new System.Windows.Forms.ComboBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.remittanceDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Label10 = new System.Windows.Forms.Label();
            this.searchRemitterTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.depositListDataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.DPWH_CNMB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRAN_DT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BANK_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BANK_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DPWH_WON_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DPWH_WON_BAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRAN_CUST_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaControlBox3 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaDragControl2 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.closeButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.choiceButton = new Guna.UI.WinForms.GunaAdvenceButton();
            RSVT_CNMBLabel = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depositListDataGridView)).BeginInit();
            this.gunaPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RSVT_CNMBLabel
            // 
            RSVT_CNMBLabel.AutoSize = true;
            RSVT_CNMBLabel.Location = new System.Drawing.Point(11, 27);
            RSVT_CNMBLabel.Name = "RSVT_CNMBLabel";
            RSVT_CNMBLabel.Size = new System.Drawing.Size(58, 21);
            RSVT_CNMBLabel.TabIndex = 155;
            RSVT_CNMBLabel.Text = "입금인";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.searchDepositListButton);
            this.GroupBox1.Controls.Add(this.depositTypeComboBox);
            this.GroupBox1.Controls.Add(this.Label17);
            this.GroupBox1.Controls.Add(this.remittanceDateTimePicker);
            this.GroupBox1.Controls.Add(this.Label10);
            this.GroupBox1.Controls.Add(RSVT_CNMBLabel);
            this.GroupBox1.Controls.Add(this.searchRemitterTextBox);
            this.GroupBox1.Location = new System.Drawing.Point(12, 43);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(931, 66);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "검색";
            // 
            // searchDepositListButton
            // 
            this.searchDepositListButton.AnimationHoverSpeed = 0.07F;
            this.searchDepositListButton.AnimationSpeed = 0.03F;
            this.searchDepositListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchDepositListButton.BorderColor = System.Drawing.Color.Black;
            this.searchDepositListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchDepositListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchDepositListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchDepositListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchDepositListButton.CheckedImage")));
            this.searchDepositListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchDepositListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchDepositListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchDepositListButton.ForeColor = System.Drawing.Color.White;
            this.searchDepositListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchDepositListButton.Image")));
            this.searchDepositListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchDepositListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchDepositListButton.Location = new System.Drawing.Point(822, 24);
            this.searchDepositListButton.Name = "searchDepositListButton";
            this.searchDepositListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchDepositListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchDepositListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchDepositListButton.OnHoverImage = null;
            this.searchDepositListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchDepositListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchDepositListButton.Size = new System.Drawing.Size(82, 29);
            this.searchDepositListButton.TabIndex = 4;
            this.searchDepositListButton.Text = "검색";
            this.searchDepositListButton.Click += new System.EventHandler(this.searchDepositListButton_Click);
            // 
            // depositTypeComboBox
            // 
            this.depositTypeComboBox.FormattingEnabled = true;
            this.depositTypeComboBox.Location = new System.Drawing.Point(486, 24);
            this.depositTypeComboBox.Name = "depositTypeComboBox";
            this.depositTypeComboBox.Size = new System.Drawing.Size(315, 29);
            this.depositTypeComboBox.TabIndex = 3;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(443, 27);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(42, 21);
            this.Label17.TabIndex = 203;
            this.Label17.Text = "계좌";
            // 
            // remittanceDateTimePicker
            // 
            this.remittanceDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.remittanceDateTimePicker.Location = new System.Drawing.Point(278, 24);
            this.remittanceDateTimePicker.Name = "remittanceDateTimePicker";
            this.remittanceDateTimePicker.Size = new System.Drawing.Size(139, 29);
            this.remittanceDateTimePicker.TabIndex = 2;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(202, 27);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(74, 21);
            this.Label10.TabIndex = 179;
            this.Label10.Text = "입금일자";
            // 
            // searchRemitterTextBox
            // 
            this.searchRemitterTextBox.Location = new System.Drawing.Point(69, 24);
            this.searchRemitterTextBox.Name = "searchRemitterTextBox";
            this.searchRemitterTextBox.Size = new System.Drawing.Size(113, 29);
            this.searchRemitterTextBox.TabIndex = 1;
            this.searchRemitterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.depositListDataGridView);
            this.groupBox2.Location = new System.Drawing.Point(12, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(931, 413);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "조회결과";
            // 
            // depositListDataGridView
            // 
            this.depositListDataGridView.AllowUserToAddRows = false;
            this.depositListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.depositListDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.depositListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.depositListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.depositListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.depositListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.depositListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.depositListDataGridView.ColumnHeadersHeight = 30;
            this.depositListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DPWH_CNMB,
            this.TRAN_DT,
            this.BANK_CD,
            this.BANK_NM,
            this.ACCT_NO,
            this.DPWH_WON_AMT,
            this.DPWH_WON_BAL,
            this.TRAN_CUST_NM});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.depositListDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.depositListDataGridView.EnableHeadersVisualStyles = false;
            this.depositListDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.depositListDataGridView.Location = new System.Drawing.Point(3, 28);
            this.depositListDataGridView.Name = "depositListDataGridView";
            this.depositListDataGridView.RowHeadersVisible = false;
            this.depositListDataGridView.RowTemplate.Height = 23;
            this.depositListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.depositListDataGridView.Size = new System.Drawing.Size(925, 379);
            this.depositListDataGridView.TabIndex = 305;
            this.depositListDataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.depositListDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.depositListDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.depositListDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.depositListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.depositListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.depositListDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.depositListDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.depositListDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.depositListDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.depositListDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.depositListDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.depositListDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.depositListDataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.depositListDataGridView.ThemeStyle.ReadOnly = false;
            this.depositListDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.depositListDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.depositListDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.depositListDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.depositListDataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.depositListDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.depositListDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.depositListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.depositListDataGridView_CellDoubleClick);
            // 
            // DPWH_CNMB
            // 
            this.DPWH_CNMB.HeaderText = "입출금번";
            this.DPWH_CNMB.Name = "DPWH_CNMB";
            this.DPWH_CNMB.Visible = false;
            // 
            // TRAN_DT
            // 
            this.TRAN_DT.HeaderText = "  거래일자";
            this.TRAN_DT.Name = "TRAN_DT";
            // 
            // BANK_CD
            // 
            this.BANK_CD.HeaderText = "은행코드";
            this.BANK_CD.Name = "BANK_CD";
            this.BANK_CD.Visible = false;
            // 
            // BANK_NM
            // 
            this.BANK_NM.HeaderText = "  은행명";
            this.BANK_NM.Name = "BANK_NM";
            // 
            // ACCT_NO
            // 
            this.ACCT_NO.FillWeight = 130F;
            this.ACCT_NO.HeaderText = "  계좌번호";
            this.ACCT_NO.Name = "ACCT_NO";
            // 
            // DPWH_WON_AMT
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DPWH_WON_AMT.DefaultCellStyle = dataGridViewCellStyle3;
            this.DPWH_WON_AMT.HeaderText = "  입금금액";
            this.DPWH_WON_AMT.Name = "DPWH_WON_AMT";
            // 
            // DPWH_WON_BAL
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.DPWH_WON_BAL.DefaultCellStyle = dataGridViewCellStyle4;
            this.DPWH_WON_BAL.HeaderText = "  잔액";
            this.DPWH_WON_BAL.Name = "DPWH_WON_BAL";
            // 
            // TRAN_CUST_NM
            // 
            this.TRAN_CUST_NM.HeaderText = "  송금인";
            this.TRAN_CUST_NM.Name = "TRAN_CUST_NM";
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
            this.gunaPanel1.Size = new System.Drawing.Size(955, 32);
            this.gunaPanel1.TabIndex = 301;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.gunaLabel1.ForeColor = System.Drawing.Color.White;
            this.gunaLabel1.Location = new System.Drawing.Point(8, 5);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(100, 23);
            this.gunaLabel1.TabIndex = 3;
            this.gunaLabel1.Text = "입금목록";
            // 
            // gunaControlBox3
            // 
            this.gunaControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox3.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox3.AnimationSpeed = 0.03F;
            this.gunaControlBox3.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
            this.gunaControlBox3.IconColor = System.Drawing.Color.White;
            this.gunaControlBox3.IconSize = 15F;
            this.gunaControlBox3.Location = new System.Drawing.Point(850, 2);
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
            this.gunaControlBox2.Location = new System.Drawing.Point(886, 2);
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
            this.gunaControlBox1.Location = new System.Drawing.Point(922, 2);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(30, 30);
            this.gunaControlBox1.TabIndex = 0;
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
            this.closeButton.Location = new System.Drawing.Point(985, 578);
            this.closeButton.Name = "closeButton";
            this.closeButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.closeButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.closeButton.OnHoverForeColor = System.Drawing.Color.White;
            this.closeButton.OnHoverImage = null;
            this.closeButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.closeButton.OnPressedColor = System.Drawing.Color.Black;
            this.closeButton.Size = new System.Drawing.Size(92, 33);
            this.closeButton.TabIndex = 61;
            this.closeButton.Text = "닫기";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // choiceButton
            // 
            this.choiceButton.AnimationHoverSpeed = 0.07F;
            this.choiceButton.AnimationSpeed = 0.03F;
            this.choiceButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.choiceButton.BorderColor = System.Drawing.Color.Black;
            this.choiceButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.choiceButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.choiceButton.CheckedForeColor = System.Drawing.Color.White;
            this.choiceButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("choiceButton.CheckedImage")));
            this.choiceButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.choiceButton.FocusedColor = System.Drawing.Color.Empty;
            this.choiceButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.choiceButton.ForeColor = System.Drawing.Color.White;
            this.choiceButton.Image = ((System.Drawing.Image)(resources.GetObject("choiceButton.Image")));
            this.choiceButton.ImageSize = new System.Drawing.Size(20, 20);
            this.choiceButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.choiceButton.Location = new System.Drawing.Point(750, 538);
            this.choiceButton.Name = "choiceButton";
            this.choiceButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.choiceButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.choiceButton.OnHoverForeColor = System.Drawing.Color.White;
            this.choiceButton.OnHoverImage = null;
            this.choiceButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.choiceButton.OnPressedColor = System.Drawing.Color.Black;
            this.choiceButton.Size = new System.Drawing.Size(92, 33);
            this.choiceButton.TabIndex = 5;
            this.choiceButton.Text = "선택";
            this.choiceButton.Click += new System.EventHandler(this.choiceButton_Click);
            // 
            // PopUpRemitterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(955, 583);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.choiceButton);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PopUpRemitterList";
            this.Text = "입금목록";
            this.Load += new System.EventHandler(this.PopUpRemitterList_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.depositListDataGridView)).EndInit();
            this.gunaPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox searchRemitterTextBox;
        internal System.Windows.Forms.DateTimePicker remittanceDateTimePicker;
        internal System.Windows.Forms.Label Label10;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ComboBox depositTypeComboBox;
        internal System.Windows.Forms.Label Label17;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox3;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl2;
        private Guna.UI.WinForms.GunaAdvenceButton searchDepositListButton;
        private Guna.UI.WinForms.GunaAdvenceButton closeButton;
        private Guna.UI.WinForms.GunaAdvenceButton choiceButton;
        private Guna.UI.WinForms.GunaDataGridView depositListDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn DPWH_CNMB;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRAN_DT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BANK_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn BANK_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACCT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DPWH_WON_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DPWH_WON_BAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRAN_CUST_NM;
    }
}