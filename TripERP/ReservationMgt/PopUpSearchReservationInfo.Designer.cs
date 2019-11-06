namespace TripERP.ReservationMgt
{
    partial class PopUpSearchReservationInfo
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
            System.Windows.Forms.Label Label4;
            System.Windows.Forms.Label Label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpSearchReservationInfo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.customerNameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchCustomerButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.customerDataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.CUST_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSVT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSVT_DT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRDT_CNMB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRDT_GRAD_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRDT_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DPTR_DT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CELL_PHNE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSVT_STTS_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMAL_ADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaControlBox3 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaDragControl2 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.closeButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.selectButton = new Guna.UI.WinForms.GunaAdvenceButton();
            Label4 = new System.Windows.Forms.Label();
            Label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customerDataGridView)).BeginInit();
            this.gunaPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label4
            // 
            Label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            Label4.AutoSize = true;
            Label4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            Label4.Location = new System.Drawing.Point(463, 254);
            Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label4.Name = "Label4";
            Label4.Size = new System.Drawing.Size(74, 21);
            Label4.TabIndex = 189;
            Label4.Text = "검색결과";
            // 
            // Label1
            // 
            Label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            Label1.AutoSize = true;
            Label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            Label1.Location = new System.Drawing.Point(18, 28);
            Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new System.Drawing.Size(74, 21);
            Label1.TabIndex = 182;
            Label1.Text = "예약자명";
            // 
            // customerNameTextBox
            // 
            this.customerNameTextBox.Location = new System.Drawing.Point(95, 24);
            this.customerNameTextBox.Name = "customerNameTextBox";
            this.customerNameTextBox.Size = new System.Drawing.Size(100, 29);
            this.customerNameTextBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchCustomerButton);
            this.groupBox1.Controls.Add(Label1);
            this.groupBox1.Controls.Add(this.customerNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 69);
            this.groupBox1.TabIndex = 194;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "검색";
            // 
            // searchCustomerButton
            // 
            this.searchCustomerButton.AnimationHoverSpeed = 0.07F;
            this.searchCustomerButton.AnimationSpeed = 0.03F;
            this.searchCustomerButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchCustomerButton.BorderColor = System.Drawing.Color.Black;
            this.searchCustomerButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchCustomerButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchCustomerButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchCustomerButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchCustomerButton.CheckedImage")));
            this.searchCustomerButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchCustomerButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchCustomerButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchCustomerButton.ForeColor = System.Drawing.Color.White;
            this.searchCustomerButton.Image = ((System.Drawing.Image)(resources.GetObject("searchCustomerButton.Image")));
            this.searchCustomerButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchCustomerButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCustomerButton.Location = new System.Drawing.Point(211, 24);
            this.searchCustomerButton.Name = "searchCustomerButton";
            this.searchCustomerButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchCustomerButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchCustomerButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchCustomerButton.OnHoverImage = null;
            this.searchCustomerButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCustomerButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchCustomerButton.Size = new System.Drawing.Size(82, 29);
            this.searchCustomerButton.TabIndex = 2;
            this.searchCustomerButton.Text = "검색";
            this.searchCustomerButton.Click += new System.EventHandler(this.searchCustomerButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.customerDataGridView);
            this.groupBox2.Location = new System.Drawing.Point(16, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(881, 396);
            this.groupBox2.TabIndex = 195;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "검색결과";
            // 
            // customerDataGridView
            // 
            this.customerDataGridView.AllowUserToAddRows = false;
            this.customerDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.customerDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.customerDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customerDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.customerDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.customerDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customerDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.customerDataGridView.ColumnHeadersHeight = 30;
            this.customerDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CUST_NO,
            this.CUST_NM,
            this.RSVT_NO,
            this.RSVT_DT,
            this.PRDT_CNMB,
            this.PRDT_GRAD_CD,
            this.PRDT_NM,
            this.DPTR_DT,
            this.CELL_PHNE_NO,
            this.RSVT_STTS_CD,
            this.EMAL_ADDR});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customerDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.customerDataGridView.EnableHeadersVisualStyles = false;
            this.customerDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.customerDataGridView.Location = new System.Drawing.Point(3, 28);
            this.customerDataGridView.Name = "customerDataGridView";
            this.customerDataGridView.RowHeadersVisible = false;
            this.customerDataGridView.RowTemplate.Height = 23;
            this.customerDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customerDataGridView.Size = new System.Drawing.Size(875, 362);
            this.customerDataGridView.TabIndex = 308;
            this.customerDataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.customerDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.customerDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.customerDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.customerDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.customerDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.customerDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.customerDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.customerDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.customerDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.customerDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.customerDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.customerDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.customerDataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.customerDataGridView.ThemeStyle.ReadOnly = false;
            this.customerDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.customerDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.customerDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.customerDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.customerDataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.customerDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.customerDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.customerDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customerDataGridView_CellDoubleClick);
            // 
            // CUST_NO
            // 
            this.CUST_NO.HeaderText = "  고객번호";
            this.CUST_NO.Name = "CUST_NO";
            // 
            // CUST_NM
            // 
            this.CUST_NM.HeaderText = "  고객명";
            this.CUST_NM.Name = "CUST_NM";
            // 
            // RSVT_NO
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RSVT_NO.DefaultCellStyle = dataGridViewCellStyle3;
            this.RSVT_NO.HeaderText = "  예약번호";
            this.RSVT_NO.Name = "RSVT_NO";
            // 
            // RSVT_DT
            // 
            this.RSVT_DT.HeaderText = "  예약일자";
            this.RSVT_DT.Name = "RSVT_DT";
            // 
            // PRDT_CNMB
            // 
            this.PRDT_CNMB.HeaderText = "상품번호";
            this.PRDT_CNMB.Name = "PRDT_CNMB";
            this.PRDT_CNMB.Visible = false;
            // 
            // PRDT_GRAD_CD
            // 
            this.PRDT_GRAD_CD.HeaderText = "상품등급코드";
            this.PRDT_GRAD_CD.Name = "PRDT_GRAD_CD";
            this.PRDT_GRAD_CD.Visible = false;
            // 
            // PRDT_NM
            // 
            this.PRDT_NM.FillWeight = 180F;
            this.PRDT_NM.HeaderText = "  상품명";
            this.PRDT_NM.Name = "PRDT_NM";
            // 
            // DPTR_DT
            // 
            this.DPTR_DT.HeaderText = "  출발일자";
            this.DPTR_DT.Name = "DPTR_DT";
            // 
            // CELL_PHNE_NO
            // 
            this.CELL_PHNE_NO.HeaderText = "수신자번호";
            this.CELL_PHNE_NO.Name = "CELL_PHNE_NO";
            this.CELL_PHNE_NO.Visible = false;
            // 
            // RSVT_STTS_CD
            // 
            this.RSVT_STTS_CD.HeaderText = "예약진행코드";
            this.RSVT_STTS_CD.Name = "RSVT_STTS_CD";
            this.RSVT_STTS_CD.Visible = false;
            // 
            // EMAL_ADDR
            // 
            this.EMAL_ADDR.HeaderText = "이메일주소";
            this.EMAL_ADDR.Name = "EMAL_ADDR";
            this.EMAL_ADDR.Visible = false;
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
            this.gunaPanel1.Size = new System.Drawing.Size(905, 32);
            this.gunaPanel1.TabIndex = 302;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.gunaLabel1.ForeColor = System.Drawing.Color.White;
            this.gunaLabel1.Location = new System.Drawing.Point(8, 5);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(100, 23);
            this.gunaLabel1.TabIndex = 3;
            this.gunaLabel1.Text = "예약자 검색";
            // 
            // gunaControlBox3
            // 
            this.gunaControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox3.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox3.AnimationSpeed = 0.03F;
            this.gunaControlBox3.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
            this.gunaControlBox3.IconColor = System.Drawing.Color.White;
            this.gunaControlBox3.IconSize = 15F;
            this.gunaControlBox3.Location = new System.Drawing.Point(800, 2);
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
            this.gunaControlBox2.Location = new System.Drawing.Point(836, 2);
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
            this.gunaControlBox1.Location = new System.Drawing.Point(872, 2);
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
            this.closeButton.Location = new System.Drawing.Point(802, 535);
            this.closeButton.Name = "closeButton";
            this.closeButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.closeButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.closeButton.OnHoverForeColor = System.Drawing.Color.White;
            this.closeButton.OnHoverImage = null;
            this.closeButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.closeButton.OnPressedColor = System.Drawing.Color.Black;
            this.closeButton.Size = new System.Drawing.Size(92, 33);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "닫기";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.AnimationHoverSpeed = 0.07F;
            this.selectButton.AnimationSpeed = 0.03F;
            this.selectButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.selectButton.BorderColor = System.Drawing.Color.Black;
            this.selectButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.selectButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.selectButton.CheckedForeColor = System.Drawing.Color.White;
            this.selectButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("selectButton.CheckedImage")));
            this.selectButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.selectButton.FocusedColor = System.Drawing.Color.Empty;
            this.selectButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.selectButton.ForeColor = System.Drawing.Color.White;
            this.selectButton.Image = ((System.Drawing.Image)(resources.GetObject("selectButton.Image")));
            this.selectButton.ImageSize = new System.Drawing.Size(20, 20);
            this.selectButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.selectButton.Location = new System.Drawing.Point(704, 535);
            this.selectButton.Name = "selectButton";
            this.selectButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.selectButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.selectButton.OnHoverForeColor = System.Drawing.Color.White;
            this.selectButton.OnHoverImage = null;
            this.selectButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.selectButton.OnPressedColor = System.Drawing.Color.Black;
            this.selectButton.Size = new System.Drawing.Size(92, 33);
            this.selectButton.TabIndex = 3;
            this.selectButton.Text = "선택";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // PopUpSearchReservationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(905, 580);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(Label4);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PopUpSearchReservationInfo";
            this.Text = "예약자 검색";
            this.Load += new System.EventHandler(this.PopUpSearchReservationInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customerDataGridView)).EndInit();
            this.gunaPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.TextBox customerNameTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox3;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl2;
        private Guna.UI.WinForms.GunaAdvenceButton searchCustomerButton;
        private Guna.UI.WinForms.GunaAdvenceButton closeButton;
        private Guna.UI.WinForms.GunaAdvenceButton selectButton;
        private Guna.UI.WinForms.GunaDataGridView customerDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSVT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSVT_DT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRDT_CNMB;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRDT_GRAD_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRDT_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DPTR_DT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CELL_PHNE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSVT_STTS_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMAL_ADDR;
    }
}