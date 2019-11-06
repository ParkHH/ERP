namespace TripERP.CommonTask
{
    partial class CurrencyCodeMgt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrencyCodeMgt));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.currencyCodeListDataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchCurrencyCodeListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.searchCurrencyNameTextBox = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.searchCurrencyCodeTextBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.deleteButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btnCloseForm = new Guna.UI.WinForms.GunaAdvenceButton();
            this.screenSortOrderTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.Label7 = new System.Windows.Forms.Label();
            this.resetButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.useYnComboBox = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.currencySymbolTextBox = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.currencyNameTextBox = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.currencyCodeTextBox = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currencyCodeListDataGridView)).BeginInit();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.currencyCodeListDataGridView);
            this.GroupBox1.Controls.Add(this.searchCurrencyCodeListButton);
            this.GroupBox1.Controls.Add(this.searchCurrencyNameTextBox);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.searchCurrencyCodeTextBox);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 9);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(672, 750);
            this.GroupBox1.TabIndex = 1;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "통화코드 조회";
            // 
            // currencyCodeListDataGridView
            // 
            this.currencyCodeListDataGridView.AllowUserToAddRows = false;
            this.currencyCodeListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.currencyCodeListDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.currencyCodeListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.currencyCodeListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.currencyCodeListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.currencyCodeListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.currencyCodeListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.currencyCodeListDataGridView.ColumnHeadersHeight = 30;
            this.currencyCodeListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.currencyCodeListDataGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.currencyCodeListDataGridView.EnableHeadersVisualStyles = false;
            this.currencyCodeListDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.currencyCodeListDataGridView.Location = new System.Drawing.Point(6, 83);
            this.currencyCodeListDataGridView.Name = "currencyCodeListDataGridView";
            this.currencyCodeListDataGridView.RowHeadersVisible = false;
            this.currencyCodeListDataGridView.RowTemplate.Height = 23;
            this.currencyCodeListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.currencyCodeListDataGridView.Size = new System.Drawing.Size(660, 661);
            this.currencyCodeListDataGridView.TabIndex = 3;
            this.currencyCodeListDataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.currencyCodeListDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.currencyCodeListDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.currencyCodeListDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.currencyCodeListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.currencyCodeListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.currencyCodeListDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.currencyCodeListDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.currencyCodeListDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.currencyCodeListDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.currencyCodeListDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.currencyCodeListDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.currencyCodeListDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.currencyCodeListDataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.currencyCodeListDataGridView.ThemeStyle.ReadOnly = false;
            this.currencyCodeListDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.currencyCodeListDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.currencyCodeListDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.currencyCodeListDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.currencyCodeListDataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.currencyCodeListDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.currencyCodeListDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.currencyCodeListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.currencyCodeListDataGridView_CellClick);
            // 
            // Column6
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column6.HeaderText = "통화코드";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "통화명";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column8.HeaderText = "통화기호";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column9.HeaderText = "사용여부";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column10.HeaderText = "화면정렬순서";
            this.Column10.Name = "Column10";
            // 
            // searchCurrencyCodeListButton
            // 
            this.searchCurrencyCodeListButton.AnimationHoverSpeed = 0.07F;
            this.searchCurrencyCodeListButton.AnimationSpeed = 0.03F;
            this.searchCurrencyCodeListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchCurrencyCodeListButton.BorderColor = System.Drawing.Color.Black;
            this.searchCurrencyCodeListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchCurrencyCodeListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchCurrencyCodeListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchCurrencyCodeListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchCurrencyCodeListButton.CheckedImage")));
            this.searchCurrencyCodeListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchCurrencyCodeListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchCurrencyCodeListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchCurrencyCodeListButton.ForeColor = System.Drawing.Color.White;
            this.searchCurrencyCodeListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchCurrencyCodeListButton.Image")));
            this.searchCurrencyCodeListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchCurrencyCodeListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCurrencyCodeListButton.Location = new System.Drawing.Point(415, 38);
            this.searchCurrencyCodeListButton.Name = "searchCurrencyCodeListButton";
            this.searchCurrencyCodeListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchCurrencyCodeListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchCurrencyCodeListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchCurrencyCodeListButton.OnHoverImage = null;
            this.searchCurrencyCodeListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCurrencyCodeListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchCurrencyCodeListButton.Size = new System.Drawing.Size(82, 29);
            this.searchCurrencyCodeListButton.TabIndex = 3;
            this.searchCurrencyCodeListButton.Text = "검색";
            this.searchCurrencyCodeListButton.Click += new System.EventHandler(this.searchCurrencyCodeListButton_Click);
            // 
            // searchCurrencyNameTextBox
            // 
            this.searchCurrencyNameTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.searchCurrencyNameTextBox.Location = new System.Drawing.Point(290, 38);
            this.searchCurrencyNameTextBox.Name = "searchCurrencyNameTextBox";
            this.searchCurrencyNameTextBox.Size = new System.Drawing.Size(100, 29);
            this.searchCurrencyNameTextBox.TabIndex = 2;
            this.searchCurrencyNameTextBox.Enter += new System.EventHandler(this.searchCurrencyNameTextBox_Enter);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(231, 42);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(58, 21);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "통화명";
            // 
            // searchCurrencyCodeTextBox
            // 
            this.searchCurrencyCodeTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.searchCurrencyCodeTextBox.Location = new System.Drawing.Point(97, 38);
            this.searchCurrencyCodeTextBox.Name = "searchCurrencyCodeTextBox";
            this.searchCurrencyCodeTextBox.Size = new System.Drawing.Size(100, 29);
            this.searchCurrencyCodeTextBox.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(25, 41);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(74, 21);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "통화코드";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.deleteButton);
            this.GroupBox2.Controls.Add(this.btnCloseForm);
            this.GroupBox2.Controls.Add(this.screenSortOrderTextBox);
            this.GroupBox2.Controls.Add(this.saveButton);
            this.GroupBox2.Controls.Add(this.Label7);
            this.GroupBox2.Controls.Add(this.resetButton);
            this.GroupBox2.Controls.Add(this.useYnComboBox);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Controls.Add(this.currencySymbolTextBox);
            this.GroupBox2.Controls.Add(this.Label5);
            this.GroupBox2.Controls.Add(this.currencyNameTextBox);
            this.GroupBox2.Controls.Add(this.Label4);
            this.GroupBox2.Controls.Add(this.currencyCodeTextBox);
            this.GroupBox2.Controls.Add(this.Label3);
            this.GroupBox2.Location = new System.Drawing.Point(701, 9);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(445, 282);
            this.GroupBox2.TabIndex = 2;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "통화코드 등록";
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
            this.deleteButton.Location = new System.Drawing.Point(225, 236);
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
            // btnCloseForm
            // 
            this.btnCloseForm.AnimationHoverSpeed = 0.07F;
            this.btnCloseForm.AnimationSpeed = 0.03F;
            this.btnCloseForm.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.btnCloseForm.BorderColor = System.Drawing.Color.Black;
            this.btnCloseForm.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnCloseForm.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnCloseForm.CheckedForeColor = System.Drawing.Color.White;
            this.btnCloseForm.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnCloseForm.CheckedImage")));
            this.btnCloseForm.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnCloseForm.FocusedColor = System.Drawing.Color.Empty;
            this.btnCloseForm.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseForm.Image")));
            this.btnCloseForm.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCloseForm.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnCloseForm.Location = new System.Drawing.Point(330, 236);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnCloseForm.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCloseForm.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCloseForm.OnHoverImage = null;
            this.btnCloseForm.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnCloseForm.OnPressedColor = System.Drawing.Color.Black;
            this.btnCloseForm.Size = new System.Drawing.Size(99, 33);
            this.btnCloseForm.TabIndex = 12;
            this.btnCloseForm.Text = " 닫기";
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // screenSortOrderTextBox
            // 
            this.screenSortOrderTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.screenSortOrderTextBox.Location = new System.Drawing.Point(113, 176);
            this.screenSortOrderTextBox.Name = "screenSortOrderTextBox";
            this.screenSortOrderTextBox.Size = new System.Drawing.Size(100, 29);
            this.screenSortOrderTextBox.TabIndex = 8;
            this.screenSortOrderTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.saveButton.Location = new System.Drawing.Point(120, 236);
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
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(4, 180);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(106, 21);
            this.Label7.TabIndex = 180;
            this.Label7.Text = "화면정렬순서";
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
            this.resetButton.Location = new System.Drawing.Point(15, 236);
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
            // useYnComboBox
            // 
            this.useYnComboBox.FormattingEnabled = true;
            this.useYnComboBox.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.useYnComboBox.Location = new System.Drawing.Point(113, 141);
            this.useYnComboBox.Name = "useYnComboBox";
            this.useYnComboBox.Size = new System.Drawing.Size(100, 29);
            this.useYnComboBox.TabIndex = 7;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(36, 145);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(74, 21);
            this.Label6.TabIndex = 162;
            this.Label6.Text = "사용여부";
            // 
            // currencySymbolTextBox
            // 
            this.currencySymbolTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.currencySymbolTextBox.Location = new System.Drawing.Point(113, 104);
            this.currencySymbolTextBox.Name = "currencySymbolTextBox";
            this.currencySymbolTextBox.Size = new System.Drawing.Size(100, 29);
            this.currencySymbolTextBox.TabIndex = 6;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(36, 108);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 21);
            this.Label5.TabIndex = 160;
            this.Label5.Text = "통화기호";
            // 
            // currencyNameTextBox
            // 
            this.currencyNameTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.currencyNameTextBox.Location = new System.Drawing.Point(113, 69);
            this.currencyNameTextBox.Name = "currencyNameTextBox";
            this.currencyNameTextBox.Size = new System.Drawing.Size(269, 29);
            this.currencyNameTextBox.TabIndex = 5;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(52, 73);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(58, 21);
            this.Label4.TabIndex = 158;
            this.Label4.Text = "통화명";
            // 
            // currencyCodeTextBox
            // 
            this.currencyCodeTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.currencyCodeTextBox.Location = new System.Drawing.Point(113, 34);
            this.currencyCodeTextBox.Name = "currencyCodeTextBox";
            this.currencyCodeTextBox.Size = new System.Drawing.Size(100, 29);
            this.currencyCodeTextBox.TabIndex = 4;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(36, 37);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(74, 21);
            this.Label3.TabIndex = 158;
            this.Label3.Text = "통화코드";
            // 
            // gunaResize1
            // 
            this.gunaResize1.TargetForm = this;
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this;
            // 
            // CurrencyCodeMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1155, 771);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CurrencyCodeMgt";
            this.Text = "통화코드관리";
            this.Load += new System.EventHandler(this.CurrencyCodeMgmt_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currencyCodeListDataGridView)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox searchCurrencyNameTextBox;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox searchCurrencyCodeTextBox;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.TextBox screenSortOrderTextBox;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox useYnComboBox;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox currencySymbolTextBox;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox currencyNameTextBox;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox currencyCodeTextBox;
        internal System.Windows.Forms.Label Label3;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAdvenceButton deleteButton;
        private Guna.UI.WinForms.GunaAdvenceButton btnCloseForm;
        private Guna.UI.WinForms.GunaAdvenceButton saveButton;
        private Guna.UI.WinForms.GunaAdvenceButton resetButton;
        private Guna.UI.WinForms.GunaAdvenceButton searchCurrencyCodeListButton;
        private Guna.UI.WinForms.GunaDataGridView currencyCodeListDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    }
}