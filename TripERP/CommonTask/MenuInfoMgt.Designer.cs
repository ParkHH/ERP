namespace TripERP.CommonTask
{
    partial class MenuInfoMgt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuInfoMgt));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MenudataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchMenuListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.SearchScreenNametextBox = new System.Windows.Forms.TextBox();
            this.SearchScreenCodecomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deleteButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.closeButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.MgtFormNametextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.label3 = new System.Windows.Forms.Label();
            this.resetButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.MgtYesNocomboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MgtScreenNametextBox = new System.Windows.Forms.TextBox();
            this.MgtScreenCodetextBox = new System.Windows.Forms.TextBox();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenudataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MenudataGridView);
            this.groupBox1.Controls.Add(this.searchMenuListButton);
            this.groupBox1.Controls.Add(this.SearchScreenNametextBox);
            this.groupBox1.Controls.Add(this.SearchScreenCodecomboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(866, 552);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "메뉴정보조회";
            // 
            // MenudataGridView
            // 
            this.MenudataGridView.AllowUserToAddRows = false;
            this.MenudataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.MenudataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MenudataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MenudataGridView.BackgroundColor = System.Drawing.Color.White;
            this.MenudataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MenudataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MenudataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MenudataGridView.ColumnHeadersHeight = 30;
            this.MenudataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column6,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MenudataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.MenudataGridView.EnableHeadersVisualStyles = false;
            this.MenudataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.MenudataGridView.Location = new System.Drawing.Point(6, 83);
            this.MenudataGridView.Name = "MenudataGridView";
            this.MenudataGridView.RowHeadersVisible = false;
            this.MenudataGridView.RowTemplate.Height = 23;
            this.MenudataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MenudataGridView.Size = new System.Drawing.Size(854, 463);
            this.MenudataGridView.TabIndex = 7;
            this.MenudataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.MenudataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.MenudataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.MenudataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.MenudataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.MenudataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.MenudataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.MenudataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.MenudataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.MenudataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MenudataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MenudataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.MenudataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.MenudataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.MenudataGridView.ThemeStyle.ReadOnly = false;
            this.MenudataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.MenudataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.MenudataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MenudataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.MenudataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.MenudataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.MenudataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.MenudataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MenudataGridView_CellClick);
            // 
            // Column4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "  화면코드";
            this.Column4.Name = "Column4";
            // 
            // Column6
            // 
            this.Column6.FillWeight = 150F;
            this.Column6.HeaderText = "  화면명";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.FillWeight = 200F;
            this.Column7.HeaderText = "  폼이름";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column8.HeaderText = "  사용여부";
            this.Column8.Name = "Column8";
            // 
            // searchMenuListButton
            // 
            this.searchMenuListButton.AnimationHoverSpeed = 0.07F;
            this.searchMenuListButton.AnimationSpeed = 0.03F;
            this.searchMenuListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchMenuListButton.BorderColor = System.Drawing.Color.Black;
            this.searchMenuListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchMenuListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchMenuListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchMenuListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchMenuListButton.CheckedImage")));
            this.searchMenuListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchMenuListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchMenuListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchMenuListButton.ForeColor = System.Drawing.Color.White;
            this.searchMenuListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchMenuListButton.Image")));
            this.searchMenuListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchMenuListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchMenuListButton.Location = new System.Drawing.Point(579, 35);
            this.searchMenuListButton.Name = "searchMenuListButton";
            this.searchMenuListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchMenuListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchMenuListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchMenuListButton.OnHoverImage = null;
            this.searchMenuListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchMenuListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchMenuListButton.Size = new System.Drawing.Size(82, 29);
            this.searchMenuListButton.TabIndex = 3;
            this.searchMenuListButton.Text = "검색";
            this.searchMenuListButton.Click += new System.EventHandler(this.searchMenuListButton_Click);
            // 
            // SearchScreenNametextBox
            // 
            this.SearchScreenNametextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.SearchScreenNametextBox.Location = new System.Drawing.Point(404, 35);
            this.SearchScreenNametextBox.Name = "SearchScreenNametextBox";
            this.SearchScreenNametextBox.Size = new System.Drawing.Size(150, 29);
            this.SearchScreenNametextBox.TabIndex = 2;
            // 
            // SearchScreenCodecomboBox
            // 
            this.SearchScreenCodecomboBox.FormattingEnabled = true;
            this.SearchScreenCodecomboBox.Location = new System.Drawing.Point(89, 35);
            this.SearchScreenCodecomboBox.Name = "SearchScreenCodecomboBox";
            this.SearchScreenCodecomboBox.Size = new System.Drawing.Size(234, 29);
            this.SearchScreenCodecomboBox.Sorted = true;
            this.SearchScreenCodecomboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "화면명";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "화면코드";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deleteButton);
            this.groupBox2.Controls.Add(this.closeButton);
            this.groupBox2.Controls.Add(this.MgtFormNametextBox);
            this.groupBox2.Controls.Add(this.saveButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.resetButton);
            this.groupBox2.Controls.Add(this.MgtYesNocomboBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.MgtScreenNametextBox);
            this.groupBox2.Controls.Add(this.MgtScreenCodetextBox);
            this.groupBox2.Location = new System.Drawing.Point(887, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 299);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "메뉴정보관리";
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
            this.deleteButton.Location = new System.Drawing.Point(220, 251);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.deleteButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.deleteButton.OnHoverForeColor = System.Drawing.Color.White;
            this.deleteButton.OnHoverImage = null;
            this.deleteButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.deleteButton.OnPressedColor = System.Drawing.Color.Black;
            this.deleteButton.Size = new System.Drawing.Size(99, 33);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = " 삭제";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
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
            this.closeButton.Location = new System.Drawing.Point(325, 251);
            this.closeButton.Name = "closeButton";
            this.closeButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.closeButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.closeButton.OnHoverForeColor = System.Drawing.Color.White;
            this.closeButton.OnHoverImage = null;
            this.closeButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.closeButton.OnPressedColor = System.Drawing.Color.Black;
            this.closeButton.Size = new System.Drawing.Size(99, 33);
            this.closeButton.TabIndex = 11;
            this.closeButton.Text = " 닫기";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // MgtFormNametextBox
            // 
            this.MgtFormNametextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.MgtFormNametextBox.Location = new System.Drawing.Point(117, 116);
            this.MgtFormNametextBox.Name = "MgtFormNametextBox";
            this.MgtFormNametextBox.Size = new System.Drawing.Size(175, 29);
            this.MgtFormNametextBox.TabIndex = 6;
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
            this.saveButton.Location = new System.Drawing.Point(115, 251);
            this.saveButton.Name = "saveButton";
            this.saveButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.saveButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.saveButton.OnHoverForeColor = System.Drawing.Color.White;
            this.saveButton.OnHoverImage = null;
            this.saveButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.saveButton.OnPressedColor = System.Drawing.Color.Black;
            this.saveButton.Size = new System.Drawing.Size(99, 33);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = " 저장";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 64;
            this.label3.Text = "폼이름";
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
            this.resetButton.Location = new System.Drawing.Point(10, 251);
            this.resetButton.Name = "resetButton";
            this.resetButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.resetButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.resetButton.OnHoverForeColor = System.Drawing.Color.White;
            this.resetButton.OnHoverImage = null;
            this.resetButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.resetButton.OnPressedColor = System.Drawing.Color.Black;
            this.resetButton.Size = new System.Drawing.Size(99, 33);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "초기화";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // MgtYesNocomboBox
            // 
            this.MgtYesNocomboBox.FormattingEnabled = true;
            this.MgtYesNocomboBox.Location = new System.Drawing.Point(117, 160);
            this.MgtYesNocomboBox.Name = "MgtYesNocomboBox";
            this.MgtYesNocomboBox.Size = new System.Drawing.Size(175, 29);
            this.MgtYesNocomboBox.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 62;
            this.label8.Text = "사용여부";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 21);
            this.label5.TabIndex = 62;
            this.label5.Text = "화면명";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 62;
            this.label4.Text = "화면코드";
            // 
            // MgtScreenNametextBox
            // 
            this.MgtScreenNametextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.MgtScreenNametextBox.Location = new System.Drawing.Point(117, 71);
            this.MgtScreenNametextBox.Name = "MgtScreenNametextBox";
            this.MgtScreenNametextBox.Size = new System.Drawing.Size(175, 29);
            this.MgtScreenNametextBox.TabIndex = 5;
            // 
            // MgtScreenCodetextBox
            // 
            this.MgtScreenCodetextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.MgtScreenCodetextBox.Location = new System.Drawing.Point(117, 27);
            this.MgtScreenCodetextBox.Name = "MgtScreenCodetextBox";
            this.MgtScreenCodetextBox.Size = new System.Drawing.Size(175, 29);
            this.MgtScreenCodetextBox.TabIndex = 4;
            // 
            // gunaResize1
            // 
            this.gunaResize1.TargetForm = this;
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this;
            // 
            // MenuInfoMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1336, 588);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MenuInfoMgt";
            this.Text = "메뉴정보관리";
            this.Load += new System.EventHandler(this.MenuInfoMgt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenudataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SearchScreenCodecomboBox;
        private System.Windows.Forms.TextBox SearchScreenNametextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox MgtScreenCodetextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MgtScreenNametextBox;
        private System.Windows.Forms.ComboBox MgtYesNocomboBox;
        private System.Windows.Forms.TextBox MgtFormNametextBox;
        private System.Windows.Forms.Label label3;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAdvenceButton deleteButton;
        private Guna.UI.WinForms.GunaAdvenceButton closeButton;
        private Guna.UI.WinForms.GunaAdvenceButton saveButton;
        private Guna.UI.WinForms.GunaAdvenceButton resetButton;
        private Guna.UI.WinForms.GunaAdvenceButton searchMenuListButton;
        private Guna.UI.WinForms.GunaDataGridView MenudataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}