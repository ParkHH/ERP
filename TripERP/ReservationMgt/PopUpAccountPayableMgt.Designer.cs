namespace TripERP.ReservationMgt
{
    partial class PopUpAccountPayableMgt
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
            System.Windows.Forms.Label Label2;
            System.Windows.Forms.Label RSVT_CNMBLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpAccountPayableMgt));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.searchBookerButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.searchBookerNameTextBox = new System.Windows.Forms.TextBox();
            this.searchReservationNumberTextBox = new System.Windows.Forms.TextBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.costPriceDataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.CSPR_CNMB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSPR_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARPL_CMPN_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARPL_CMPN_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSPR_CUR_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSPR_CUR_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADLT_DVSN_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADLT_DVSN_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSPR_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NMPS_NBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSPR_AMT_SUB_TOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STEM_BASI_EXRT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STEM_UNPA_WON_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.deleteButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.saveButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.closeButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.resetButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.payableDivisionCodeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.unpaidExpenseWonSumLabel = new System.Windows.Forms.Label();
            this.unpaidExpenseWonSumTextBox = new System.Windows.Forms.TextBox();
            this.unpaidExpenseForeignSumLabel = new System.Windows.Forms.Label();
            this.exchangeRateLabel = new System.Windows.Forms.Label();
            this.unpaidExpenseForeignSumTextBox = new System.Windows.Forms.TextBox();
            this.exchangeRateTextBox = new System.Windows.Forms.TextBox();
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaControlBox3 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaDragControl2 = new Guna.UI.WinForms.GunaDragControl(this.components);
            Label2 = new System.Windows.Forms.Label();
            RSVT_CNMBLabel = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceDataGridView)).BeginInit();
            this.GroupBox3.SuspendLayout();
            this.gunaPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new System.Drawing.Point(207, 26);
            Label2.Name = "Label2";
            Label2.Size = new System.Drawing.Size(58, 21);
            Label2.TabIndex = 157;
            Label2.Text = "예약자";
            // 
            // RSVT_CNMBLabel
            // 
            RSVT_CNMBLabel.AutoSize = true;
            RSVT_CNMBLabel.Location = new System.Drawing.Point(20, 26);
            RSVT_CNMBLabel.Name = "RSVT_CNMBLabel";
            RSVT_CNMBLabel.Size = new System.Drawing.Size(74, 21);
            RSVT_CNMBLabel.TabIndex = 155;
            RSVT_CNMBLabel.Text = "예약번호";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.searchBookerButton);
            this.GroupBox1.Controls.Add(Label2);
            this.GroupBox1.Controls.Add(this.searchBookerNameTextBox);
            this.GroupBox1.Controls.Add(RSVT_CNMBLabel);
            this.GroupBox1.Controls.Add(this.searchReservationNumberTextBox);
            this.GroupBox1.Location = new System.Drawing.Point(12, 44);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(502, 66);
            this.GroupBox1.TabIndex = 10;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "검색";
            // 
            // searchBookerButton
            // 
            this.searchBookerButton.AnimationHoverSpeed = 0.07F;
            this.searchBookerButton.AnimationSpeed = 0.03F;
            this.searchBookerButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchBookerButton.BorderColor = System.Drawing.Color.Black;
            this.searchBookerButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchBookerButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchBookerButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchBookerButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchBookerButton.CheckedImage")));
            this.searchBookerButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchBookerButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchBookerButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchBookerButton.ForeColor = System.Drawing.Color.White;
            this.searchBookerButton.Image = ((System.Drawing.Image)(resources.GetObject("searchBookerButton.Image")));
            this.searchBookerButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchBookerButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchBookerButton.Location = new System.Drawing.Point(405, 23);
            this.searchBookerButton.Name = "searchBookerButton";
            this.searchBookerButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchBookerButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchBookerButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchBookerButton.OnHoverImage = null;
            this.searchBookerButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchBookerButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchBookerButton.Size = new System.Drawing.Size(82, 29);
            this.searchBookerButton.TabIndex = 1;
            this.searchBookerButton.Text = "검색";
            this.searchBookerButton.Click += new System.EventHandler(this.searchBookerButton_Click);
            // 
            // searchBookerNameTextBox
            // 
            this.searchBookerNameTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.searchBookerNameTextBox.Location = new System.Drawing.Point(266, 23);
            this.searchBookerNameTextBox.Name = "searchBookerNameTextBox";
            this.searchBookerNameTextBox.ReadOnly = true;
            this.searchBookerNameTextBox.Size = new System.Drawing.Size(126, 29);
            this.searchBookerNameTextBox.TabIndex = 5;
            // 
            // searchReservationNumberTextBox
            // 
            this.searchReservationNumberTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.searchReservationNumberTextBox.Location = new System.Drawing.Point(94, 23);
            this.searchReservationNumberTextBox.Name = "searchReservationNumberTextBox";
            this.searchReservationNumberTextBox.ReadOnly = true;
            this.searchReservationNumberTextBox.Size = new System.Drawing.Size(113, 29);
            this.searchReservationNumberTextBox.TabIndex = 1;
            this.searchReservationNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.costPriceDataGridView);
            this.GroupBox2.Location = new System.Drawing.Point(9, 116);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(1135, 237);
            this.GroupBox2.TabIndex = 8;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "수배처 원가내역";
            // 
            // costPriceDataGridView
            // 
            this.costPriceDataGridView.AllowUserToAddRows = false;
            this.costPriceDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.costPriceDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.costPriceDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.costPriceDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.costPriceDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.costPriceDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.costPriceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.costPriceDataGridView.ColumnHeadersHeight = 30;
            this.costPriceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CSPR_CNMB,
            this.CSPR_NM,
            this.ARPL_CMPN_NO,
            this.ARPL_CMPN_NM,
            this.CSPR_CUR_CD,
            this.CSPR_CUR_NM,
            this.ADLT_DVSN_CD,
            this.ADLT_DVSN_NM,
            this.CSPR_AMT,
            this.NMPS_NBR,
            this.CSPR_AMT_SUB_TOT,
            this.STEM_BASI_EXRT,
            this.STEM_UNPA_WON_AMT});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.costPriceDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.costPriceDataGridView.EnableHeadersVisualStyles = false;
            this.costPriceDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.costPriceDataGridView.Location = new System.Drawing.Point(2, 28);
            this.costPriceDataGridView.Name = "costPriceDataGridView";
            this.costPriceDataGridView.RowHeadersVisible = false;
            this.costPriceDataGridView.RowTemplate.Height = 23;
            this.costPriceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.costPriceDataGridView.Size = new System.Drawing.Size(1133, 203);
            this.costPriceDataGridView.TabIndex = 302;
            this.costPriceDataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.costPriceDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.costPriceDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.costPriceDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.costPriceDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.costPriceDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.costPriceDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.costPriceDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.costPriceDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.costPriceDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.costPriceDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.costPriceDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.costPriceDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.costPriceDataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.costPriceDataGridView.ThemeStyle.ReadOnly = false;
            this.costPriceDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.costPriceDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.costPriceDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.costPriceDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.costPriceDataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.costPriceDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.costPriceDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // CSPR_CNMB
            // 
            this.CSPR_CNMB.HeaderText = "원가일련번호";
            this.CSPR_CNMB.Name = "CSPR_CNMB";
            this.CSPR_CNMB.Visible = false;
            // 
            // CSPR_NM
            // 
            this.CSPR_NM.FillWeight = 150F;
            this.CSPR_NM.HeaderText = "  원가명";
            this.CSPR_NM.Name = "CSPR_NM";
            // 
            // ARPL_CMPN_NO
            // 
            this.ARPL_CMPN_NO.HeaderText = "수배처업체번호";
            this.ARPL_CMPN_NO.Name = "ARPL_CMPN_NO";
            this.ARPL_CMPN_NO.Visible = false;
            // 
            // ARPL_CMPN_NM
            // 
            this.ARPL_CMPN_NM.FillWeight = 150F;
            this.ARPL_CMPN_NM.HeaderText = "  수배처";
            this.ARPL_CMPN_NM.Name = "ARPL_CMPN_NM";
            // 
            // CSPR_CUR_CD
            // 
            this.CSPR_CUR_CD.HeaderText = "통화";
            this.CSPR_CUR_CD.Name = "CSPR_CUR_CD";
            this.CSPR_CUR_CD.Visible = false;
            // 
            // CSPR_CUR_NM
            // 
            this.CSPR_CUR_NM.HeaderText = "  통화명";
            this.CSPR_CUR_NM.Name = "CSPR_CUR_NM";
            // 
            // ADLT_DVSN_CD
            // 
            this.ADLT_DVSN_CD.HeaderText = "성인구분코드";
            this.ADLT_DVSN_CD.Name = "ADLT_DVSN_CD";
            this.ADLT_DVSN_CD.Visible = false;
            // 
            // ADLT_DVSN_NM
            // 
            this.ADLT_DVSN_NM.HeaderText = "  인원구분";
            this.ADLT_DVSN_NM.Name = "ADLT_DVSN_NM";
            // 
            // CSPR_AMT
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C3";
            dataGridViewCellStyle3.NullValue = "0";
            this.CSPR_AMT.DefaultCellStyle = dataGridViewCellStyle3;
            this.CSPR_AMT.HeaderText = "  원가";
            this.CSPR_AMT.Name = "CSPR_AMT";
            // 
            // NMPS_NBR
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NMPS_NBR.DefaultCellStyle = dataGridViewCellStyle4;
            this.NMPS_NBR.HeaderText = "  인원수";
            this.NMPS_NBR.Name = "NMPS_NBR";
            // 
            // CSPR_AMT_SUB_TOT
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CSPR_AMT_SUB_TOT.DefaultCellStyle = dataGridViewCellStyle5;
            this.CSPR_AMT_SUB_TOT.HeaderText = "  원가소계";
            this.CSPR_AMT_SUB_TOT.Name = "CSPR_AMT_SUB_TOT";
            // 
            // STEM_BASI_EXRT
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.STEM_BASI_EXRT.DefaultCellStyle = dataGridViewCellStyle6;
            this.STEM_BASI_EXRT.HeaderText = "  가정산환율";
            this.STEM_BASI_EXRT.Name = "STEM_BASI_EXRT";
            // 
            // STEM_UNPA_WON_AMT
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.STEM_UNPA_WON_AMT.DefaultCellStyle = dataGridViewCellStyle7;
            this.STEM_UNPA_WON_AMT.HeaderText = "미지급금(₩)";
            this.STEM_UNPA_WON_AMT.Name = "STEM_UNPA_WON_AMT";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.deleteButton);
            this.GroupBox3.Controls.Add(this.saveButton);
            this.GroupBox3.Controls.Add(this.closeButton);
            this.GroupBox3.Controls.Add(this.resetButton);
            this.GroupBox3.Controls.Add(this.payableDivisionCodeComboBox);
            this.GroupBox3.Controls.Add(this.label3);
            this.GroupBox3.Controls.Add(this.unpaidExpenseWonSumLabel);
            this.GroupBox3.Controls.Add(this.unpaidExpenseWonSumTextBox);
            this.GroupBox3.Controls.Add(this.unpaidExpenseForeignSumLabel);
            this.GroupBox3.Controls.Add(this.exchangeRateLabel);
            this.GroupBox3.Controls.Add(this.unpaidExpenseForeignSumTextBox);
            this.GroupBox3.Controls.Add(this.exchangeRateTextBox);
            this.GroupBox3.Location = new System.Drawing.Point(8, 359);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(1136, 121);
            this.GroupBox3.TabIndex = 9;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "미지급금등록";
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
            this.deleteButton.Image = null;
            this.deleteButton.ImageSize = new System.Drawing.Size(20, 20);
            this.deleteButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.deleteButton.Location = new System.Drawing.Point(834, 76);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.deleteButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.deleteButton.OnHoverForeColor = System.Drawing.Color.White;
            this.deleteButton.OnHoverImage = null;
            this.deleteButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.deleteButton.OnPressedColor = System.Drawing.Color.Black;
            this.deleteButton.Size = new System.Drawing.Size(141, 33);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "미지급 일괄삭제";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
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
            this.saveButton.Image = null;
            this.saveButton.ImageSize = new System.Drawing.Size(20, 20);
            this.saveButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.saveButton.Location = new System.Drawing.Point(689, 76);
            this.saveButton.Name = "saveButton";
            this.saveButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.saveButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.saveButton.OnHoverForeColor = System.Drawing.Color.White;
            this.saveButton.OnHoverImage = null;
            this.saveButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.saveButton.OnPressedColor = System.Drawing.Color.Black;
            this.saveButton.Size = new System.Drawing.Size(139, 33);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "미지급 일괄저장";
            this.saveButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
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
            this.closeButton.Location = new System.Drawing.Point(980, 76);
            this.closeButton.Name = "closeButton";
            this.closeButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.closeButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.closeButton.OnHoverForeColor = System.Drawing.Color.White;
            this.closeButton.OnHoverImage = null;
            this.closeButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.closeButton.OnPressedColor = System.Drawing.Color.Black;
            this.closeButton.Size = new System.Drawing.Size(99, 33);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = " 닫기";
            this.closeButton.Click += new System.EventHandler(this.btnCloseForm_Click);
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
            this.resetButton.Location = new System.Drawing.Point(585, 76);
            this.resetButton.Name = "resetButton";
            this.resetButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.resetButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.resetButton.OnHoverForeColor = System.Drawing.Color.White;
            this.resetButton.OnHoverImage = null;
            this.resetButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.resetButton.OnPressedColor = System.Drawing.Color.Black;
            this.resetButton.Size = new System.Drawing.Size(99, 33);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "초기화";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // payableDivisionCodeComboBox
            // 
            this.payableDivisionCodeComboBox.FormattingEnabled = true;
            this.payableDivisionCodeComboBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.payableDivisionCodeComboBox.Location = new System.Drawing.Point(160, 30);
            this.payableDivisionCodeComboBox.Name = "payableDivisionCodeComboBox";
            this.payableDivisionCodeComboBox.Size = new System.Drawing.Size(121, 29);
            this.payableDivisionCodeComboBox.TabIndex = 2;
            this.payableDivisionCodeComboBox.SelectedIndexChanged += new System.EventHandler(this.payableDivisionCodeComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(29, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 21);
            this.label3.TabIndex = 237;
            this.label3.Text = "미지급/선급구분";
            // 
            // unpaidExpenseWonSumLabel
            // 
            this.unpaidExpenseWonSumLabel.AutoSize = true;
            this.unpaidExpenseWonSumLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.unpaidExpenseWonSumLabel.Location = new System.Drawing.Point(804, 34);
            this.unpaidExpenseWonSumLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.unpaidExpenseWonSumLabel.Name = "unpaidExpenseWonSumLabel";
            this.unpaidExpenseWonSumLabel.Size = new System.Drawing.Size(154, 21);
            this.unpaidExpenseWonSumLabel.TabIndex = 236;
            this.unpaidExpenseWonSumLabel.Text = "미지급금 합계(원화)";
            this.unpaidExpenseWonSumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // unpaidExpenseWonSumTextBox
            // 
            this.unpaidExpenseWonSumTextBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.unpaidExpenseWonSumTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.unpaidExpenseWonSumTextBox.Location = new System.Drawing.Point(961, 30);
            this.unpaidExpenseWonSumTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.unpaidExpenseWonSumTextBox.Name = "unpaidExpenseWonSumTextBox";
            this.unpaidExpenseWonSumTextBox.ReadOnly = true;
            this.unpaidExpenseWonSumTextBox.Size = new System.Drawing.Size(118, 29);
            this.unpaidExpenseWonSumTextBox.TabIndex = 29;
            this.unpaidExpenseWonSumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // unpaidExpenseForeignSumLabel
            // 
            this.unpaidExpenseForeignSumLabel.AutoSize = true;
            this.unpaidExpenseForeignSumLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.unpaidExpenseForeignSumLabel.Location = new System.Drawing.Point(509, 34);
            this.unpaidExpenseForeignSumLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.unpaidExpenseForeignSumLabel.Name = "unpaidExpenseForeignSumLabel";
            this.unpaidExpenseForeignSumLabel.Size = new System.Drawing.Size(154, 21);
            this.unpaidExpenseForeignSumLabel.TabIndex = 234;
            this.unpaidExpenseForeignSumLabel.Text = "미지급금 합계(외화)";
            this.unpaidExpenseForeignSumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // exchangeRateLabel
            // 
            this.exchangeRateLabel.AutoSize = true;
            this.exchangeRateLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.exchangeRateLabel.Location = new System.Drawing.Point(300, 34);
            this.exchangeRateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.exchangeRateLabel.Name = "exchangeRateLabel";
            this.exchangeRateLabel.Size = new System.Drawing.Size(90, 21);
            this.exchangeRateLabel.TabIndex = 232;
            this.exchangeRateLabel.Text = "가정산환율";
            this.exchangeRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // unpaidExpenseForeignSumTextBox
            // 
            this.unpaidExpenseForeignSumTextBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.unpaidExpenseForeignSumTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.unpaidExpenseForeignSumTextBox.Location = new System.Drawing.Point(666, 30);
            this.unpaidExpenseForeignSumTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.unpaidExpenseForeignSumTextBox.Name = "unpaidExpenseForeignSumTextBox";
            this.unpaidExpenseForeignSumTextBox.ReadOnly = true;
            this.unpaidExpenseForeignSumTextBox.Size = new System.Drawing.Size(118, 29);
            this.unpaidExpenseForeignSumTextBox.TabIndex = 28;
            this.unpaidExpenseForeignSumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // exchangeRateTextBox
            // 
            this.exchangeRateTextBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.exchangeRateTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.exchangeRateTextBox.Location = new System.Drawing.Point(392, 30);
            this.exchangeRateTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exchangeRateTextBox.Name = "exchangeRateTextBox";
            this.exchangeRateTextBox.Size = new System.Drawing.Size(89, 29);
            this.exchangeRateTextBox.TabIndex = 3;
            this.exchangeRateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exchangeRateTextBox.TextChanged += new System.EventHandler(this.exchangeRateTextBox_TextChanged);
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
            this.gunaPanel1.Size = new System.Drawing.Size(1152, 32);
            this.gunaPanel1.TabIndex = 301;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.gunaLabel1.ForeColor = System.Drawing.Color.White;
            this.gunaLabel1.Location = new System.Drawing.Point(8, 5);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(196, 23);
            this.gunaLabel1.TabIndex = 3;
            this.gunaLabel1.Text = "선급금/미지급 비용등록";
            // 
            // gunaControlBox3
            // 
            this.gunaControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox3.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox3.AnimationSpeed = 0.03F;
            this.gunaControlBox3.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
            this.gunaControlBox3.IconColor = System.Drawing.Color.White;
            this.gunaControlBox3.IconSize = 15F;
            this.gunaControlBox3.Location = new System.Drawing.Point(1047, 2);
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
            this.gunaControlBox2.Location = new System.Drawing.Point(1083, 2);
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
            this.gunaControlBox1.Location = new System.Drawing.Point(1119, 2);
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
            // PopUpAccountPayableMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1152, 494);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox3);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PopUpAccountPayableMgt";
            this.Text = "선급금/미지급비용등록";
            this.Load += new System.EventHandler(this.PopUpAccountPayableMgt_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.costPriceDataGridView)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.gunaPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox searchBookerNameTextBox;
        internal System.Windows.Forms.TextBox searchReservationNumberTextBox;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Label unpaidExpenseWonSumLabel;
        internal System.Windows.Forms.TextBox unpaidExpenseWonSumTextBox;
        internal System.Windows.Forms.Label unpaidExpenseForeignSumLabel;
        internal System.Windows.Forms.Label exchangeRateLabel;
        internal System.Windows.Forms.TextBox unpaidExpenseForeignSumTextBox;
        internal System.Windows.Forms.TextBox exchangeRateTextBox;
        private System.Windows.Forms.ComboBox payableDivisionCodeComboBox;
        internal System.Windows.Forms.Label label3;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox3;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl2;
        private Guna.UI.WinForms.GunaAdvenceButton closeButton;
        private Guna.UI.WinForms.GunaAdvenceButton resetButton;
        private Guna.UI.WinForms.GunaAdvenceButton deleteButton;
        private Guna.UI.WinForms.GunaAdvenceButton saveButton;
        private Guna.UI.WinForms.GunaAdvenceButton searchBookerButton;
        private Guna.UI.WinForms.GunaDataGridView costPriceDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSPR_CNMB;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSPR_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARPL_CMPN_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARPL_CMPN_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSPR_CUR_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSPR_CUR_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADLT_DVSN_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADLT_DVSN_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSPR_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NMPS_NBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSPR_AMT_SUB_TOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn STEM_BASI_EXRT;
        private System.Windows.Forms.DataGridViewTextBoxColumn STEM_UNPA_WON_AMT;
    }
}