namespace TripERP.ProductMgt
{
    partial class ProductCategoryMgt
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
            System.Windows.Forms.Label S_PRDT_CDLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductCategoryMgt));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.searchProductCategoryListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.SearchProductCategoryCodeComboBox = new System.Windows.Forms.ComboBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.ProductCategoryListDataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.deleteButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.closeButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.saveButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.resetButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.LowestCategoryYNComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ProductCategoryDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LowerCategoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ProductCategoryNameTextBox = new System.Windows.Forms.TextBox();
            this.UpperCategoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.ProductCategoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.UseYnComboBox = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            S_PRDT_CDLabel = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductCategoryListDataGridView)).BeginInit();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // S_PRDT_CDLabel
            // 
            S_PRDT_CDLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            S_PRDT_CDLabel.AutoSize = true;
            S_PRDT_CDLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            S_PRDT_CDLabel.Location = new System.Drawing.Point(21, 27);
            S_PRDT_CDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            S_PRDT_CDLabel.Name = "S_PRDT_CDLabel";
            S_PRDT_CDLabel.Size = new System.Drawing.Size(106, 21);
            S_PRDT_CDLabel.TabIndex = 3;
            S_PRDT_CDLabel.Text = "상품카테고리";
            // 
            // GroupBox1
            // 
            this.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GroupBox1.Controls.Add(this.searchProductCategoryListButton);
            this.GroupBox1.Controls.Add(this.SearchProductCategoryCodeComboBox);
            this.GroupBox1.Controls.Add(S_PRDT_CDLabel);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox1.Size = new System.Drawing.Size(526, 67);
            this.GroupBox1.TabIndex = 30;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "검 색";
            // 
            // searchProductCategoryListButton
            // 
            this.searchProductCategoryListButton.AnimationHoverSpeed = 0.07F;
            this.searchProductCategoryListButton.AnimationSpeed = 0.03F;
            this.searchProductCategoryListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchProductCategoryListButton.BorderColor = System.Drawing.Color.Black;
            this.searchProductCategoryListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchProductCategoryListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchProductCategoryListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchProductCategoryListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchProductCategoryListButton.CheckedImage")));
            this.searchProductCategoryListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchProductCategoryListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchProductCategoryListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchProductCategoryListButton.ForeColor = System.Drawing.Color.White;
            this.searchProductCategoryListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchProductCategoryListButton.Image")));
            this.searchProductCategoryListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchProductCategoryListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchProductCategoryListButton.Location = new System.Drawing.Point(425, 24);
            this.searchProductCategoryListButton.Name = "searchProductCategoryListButton";
            this.searchProductCategoryListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchProductCategoryListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchProductCategoryListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchProductCategoryListButton.OnHoverImage = null;
            this.searchProductCategoryListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchProductCategoryListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchProductCategoryListButton.Size = new System.Drawing.Size(82, 29);
            this.searchProductCategoryListButton.TabIndex = 2;
            this.searchProductCategoryListButton.Text = "검색";
            this.searchProductCategoryListButton.Click += new System.EventHandler(this.searchProductCategoryListButton_Click);
            // 
            // SearchProductCategoryCodeComboBox
            // 
            this.SearchProductCategoryCodeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SearchProductCategoryCodeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchProductCategoryCodeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SearchProductCategoryCodeComboBox.DisplayMember = "PRDT_NM";
            this.SearchProductCategoryCodeComboBox.DropDownHeight = 120;
            this.SearchProductCategoryCodeComboBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SearchProductCategoryCodeComboBox.FormattingEnabled = true;
            this.SearchProductCategoryCodeComboBox.IntegralHeight = false;
            this.SearchProductCategoryCodeComboBox.ItemHeight = 21;
            this.SearchProductCategoryCodeComboBox.Location = new System.Drawing.Point(129, 24);
            this.SearchProductCategoryCodeComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SearchProductCategoryCodeComboBox.MaxDropDownItems = 100;
            this.SearchProductCategoryCodeComboBox.Name = "SearchProductCategoryCodeComboBox";
            this.SearchProductCategoryCodeComboBox.Size = new System.Drawing.Size(278, 29);
            this.SearchProductCategoryCodeComboBox.TabIndex = 1;
            this.SearchProductCategoryCodeComboBox.ValueMember = "PRDT_CD";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.ProductCategoryListDataGridView);
            this.GroupBox2.Location = new System.Drawing.Point(12, 87);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(1467, 526);
            this.GroupBox2.TabIndex = 182;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "조회결과";
            // 
            // ProductCategoryListDataGridView
            // 
            this.ProductCategoryListDataGridView.AllowUserToAddRows = false;
            this.ProductCategoryListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            this.ProductCategoryListDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.ProductCategoryListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductCategoryListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ProductCategoryListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductCategoryListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductCategoryListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.ProductCategoryListDataGridView.ColumnHeadersHeight = 30;
            this.ProductCategoryListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column17,
            this.Column16,
            this.Column18});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductCategoryListDataGridView.DefaultCellStyle = dataGridViewCellStyle15;
            this.ProductCategoryListDataGridView.EnableHeadersVisualStyles = false;
            this.ProductCategoryListDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ProductCategoryListDataGridView.Location = new System.Drawing.Point(0, 28);
            this.ProductCategoryListDataGridView.Name = "ProductCategoryListDataGridView";
            this.ProductCategoryListDataGridView.RowHeadersVisible = false;
            this.ProductCategoryListDataGridView.RowTemplate.Height = 23;
            this.ProductCategoryListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductCategoryListDataGridView.Size = new System.Drawing.Size(1461, 492);
            this.ProductCategoryListDataGridView.TabIndex = 297;
            this.ProductCategoryListDataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.ProductCategoryListDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.ProductCategoryListDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.ProductCategoryListDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.ProductCategoryListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.ProductCategoryListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.ProductCategoryListDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.ProductCategoryListDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ProductCategoryListDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.ProductCategoryListDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ProductCategoryListDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ProductCategoryListDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.ProductCategoryListDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.ProductCategoryListDataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.ProductCategoryListDataGridView.ThemeStyle.ReadOnly = false;
            this.ProductCategoryListDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.ProductCategoryListDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.ProductCategoryListDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ProductCategoryListDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.ProductCategoryListDataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.ProductCategoryListDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ProductCategoryListDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.ProductCategoryListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductCategoryListDataGridView_CellClick);
            // 
            // Column10
            // 
            this.Column10.HeaderText = "  상품카테고리";
            this.Column10.Name = "Column10";
            this.Column10.Visible = false;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "  상위카테고리";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "  하위카테고리";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.FillWeight = 120F;
            this.Column13.HeaderText = "  상품카테고리명";
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            this.Column14.FillWeight = 250F;
            this.Column14.HeaderText = "  상품설명";
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "사용여부코드";
            this.Column15.Name = "Column15";
            this.Column15.Visible = false;
            // 
            // Column17
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column17.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column17.HeaderText = "  사용여부";
            this.Column17.Name = "Column17";
            // 
            // Column16
            // 
            this.Column16.HeaderText = "최하위여부코드";
            this.Column16.Name = "Column16";
            this.Column16.Visible = false;
            // 
            // Column18
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column18.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column18.HeaderText = "  최하위여부";
            this.Column18.Name = "Column18";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.deleteButton);
            this.GroupBox3.Controls.Add(this.closeButton);
            this.GroupBox3.Controls.Add(this.saveButton);
            this.GroupBox3.Controls.Add(this.resetButton);
            this.GroupBox3.Controls.Add(this.LowestCategoryYNComboBox);
            this.GroupBox3.Controls.Add(this.label6);
            this.GroupBox3.Controls.Add(this.ProductCategoryDescriptionTextBox);
            this.GroupBox3.Controls.Add(this.label5);
            this.GroupBox3.Controls.Add(this.LowerCategoryCodeTextBox);
            this.GroupBox3.Controls.Add(this.label4);
            this.GroupBox3.Controls.Add(this.ProductCategoryNameTextBox);
            this.GroupBox3.Controls.Add(this.UpperCategoryCodeTextBox);
            this.GroupBox3.Controls.Add(this.ProductCategoryCodeTextBox);
            this.GroupBox3.Controls.Add(this.UseYnComboBox);
            this.GroupBox3.Controls.Add(this.Label9);
            this.GroupBox3.Controls.Add(this.label3);
            this.GroupBox3.Controls.Add(this.label1);
            this.GroupBox3.Controls.Add(this.Label2);
            this.GroupBox3.Location = new System.Drawing.Point(12, 634);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(1464, 161);
            this.GroupBox3.TabIndex = 183;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "입력";
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
            this.deleteButton.Location = new System.Drawing.Point(1245, 113);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.deleteButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.deleteButton.OnHoverForeColor = System.Drawing.Color.White;
            this.deleteButton.OnHoverImage = null;
            this.deleteButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.deleteButton.OnPressedColor = System.Drawing.Color.Black;
            this.deleteButton.Size = new System.Drawing.Size(99, 33);
            this.deleteButton.TabIndex = 12;
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
            this.closeButton.Location = new System.Drawing.Point(1350, 113);
            this.closeButton.Name = "closeButton";
            this.closeButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.closeButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.closeButton.OnHoverForeColor = System.Drawing.Color.White;
            this.closeButton.OnHoverImage = null;
            this.closeButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.closeButton.OnPressedColor = System.Drawing.Color.Black;
            this.closeButton.Size = new System.Drawing.Size(99, 33);
            this.closeButton.TabIndex = 13;
            this.closeButton.Text = " 닫기";
            this.closeButton.Click += new System.EventHandler(this.btnCloseForm_Click);
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
            this.saveButton.Location = new System.Drawing.Point(1140, 113);
            this.saveButton.Name = "saveButton";
            this.saveButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.saveButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.saveButton.OnHoverForeColor = System.Drawing.Color.White;
            this.saveButton.OnHoverImage = null;
            this.saveButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.saveButton.OnPressedColor = System.Drawing.Color.Black;
            this.saveButton.Size = new System.Drawing.Size(99, 33);
            this.saveButton.TabIndex = 11;
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
            this.resetButton.Location = new System.Drawing.Point(1035, 113);
            this.resetButton.Name = "resetButton";
            this.resetButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.resetButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.resetButton.OnHoverForeColor = System.Drawing.Color.White;
            this.resetButton.OnHoverImage = null;
            this.resetButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.resetButton.OnPressedColor = System.Drawing.Color.Black;
            this.resetButton.Size = new System.Drawing.Size(99, 33);
            this.resetButton.TabIndex = 10;
            this.resetButton.Text = "초기화";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // LowestCategoryYNComboBox
            // 
            this.LowestCategoryYNComboBox.FormattingEnabled = true;
            this.LowestCategoryYNComboBox.Location = new System.Drawing.Point(1096, 27);
            this.LowestCategoryYNComboBox.Name = "LowestCategoryYNComboBox";
            this.LowestCategoryYNComboBox.Size = new System.Drawing.Size(97, 29);
            this.LowestCategoryYNComboBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1003, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 21);
            this.label6.TabIndex = 47;
            this.label6.Text = "최하위여부";
            // 
            // ProductCategoryDescriptionTextBox
            // 
            this.ProductCategoryDescriptionTextBox.Location = new System.Drawing.Point(579, 73);
            this.ProductCategoryDescriptionTextBox.MaxLength = 100;
            this.ProductCategoryDescriptionTextBox.Name = "ProductCategoryDescriptionTextBox";
            this.ProductCategoryDescriptionTextBox.Size = new System.Drawing.Size(777, 29);
            this.ProductCategoryDescriptionTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(435, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 21);
            this.label5.TabIndex = 28;
            this.label5.Text = "상품카테고리설명";
            // 
            // LowerCategoryCodeTextBox
            // 
            this.LowerCategoryCodeTextBox.Location = new System.Drawing.Point(867, 27);
            this.LowerCategoryCodeTextBox.MaxLength = 10;
            this.LowerCategoryCodeTextBox.Name = "LowerCategoryCodeTextBox";
            this.LowerCategoryCodeTextBox.Size = new System.Drawing.Size(97, 29);
            this.LowerCategoryCodeTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(723, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 21);
            this.label4.TabIndex = 26;
            this.label4.Text = "하위카테고리코드";
            // 
            // ProductCategoryNameTextBox
            // 
            this.ProductCategoryNameTextBox.Location = new System.Drawing.Point(176, 73);
            this.ProductCategoryNameTextBox.MaxLength = 10;
            this.ProductCategoryNameTextBox.Name = "ProductCategoryNameTextBox";
            this.ProductCategoryNameTextBox.Size = new System.Drawing.Size(231, 29);
            this.ProductCategoryNameTextBox.TabIndex = 8;
            // 
            // UpperCategoryCodeTextBox
            // 
            this.UpperCategoryCodeTextBox.Location = new System.Drawing.Point(579, 27);
            this.UpperCategoryCodeTextBox.MaxLength = 10;
            this.UpperCategoryCodeTextBox.Name = "UpperCategoryCodeTextBox";
            this.UpperCategoryCodeTextBox.Size = new System.Drawing.Size(97, 29);
            this.UpperCategoryCodeTextBox.TabIndex = 4;
            // 
            // ProductCategoryCodeTextBox
            // 
            this.ProductCategoryCodeTextBox.Location = new System.Drawing.Point(176, 27);
            this.ProductCategoryCodeTextBox.MaxLength = 10;
            this.ProductCategoryCodeTextBox.Name = "ProductCategoryCodeTextBox";
            this.ProductCategoryCodeTextBox.Size = new System.Drawing.Size(97, 29);
            this.ProductCategoryCodeTextBox.TabIndex = 3;
            // 
            // UseYnComboBox
            // 
            this.UseYnComboBox.FormattingEnabled = true;
            this.UseYnComboBox.Location = new System.Drawing.Point(1318, 27);
            this.UseYnComboBox.Name = "UseYnComboBox";
            this.UseYnComboBox.Size = new System.Drawing.Size(97, 29);
            this.UseYnComboBox.TabIndex = 7;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(1238, 31);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(74, 21);
            this.Label9.TabIndex = 15;
            this.Label9.Text = "사용여부";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 21);
            this.label3.TabIndex = 23;
            this.label3.Text = "상품카테고리명";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 21);
            this.label1.TabIndex = 21;
            this.label1.Text = "상위카테고리코드";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(34, 31);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(138, 21);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "상품카테고리코드";
            // 
            // gunaResize1
            // 
            this.gunaResize1.TargetForm = this;
            // 
            // gunaDragControl1
            // 
            this.gunaDragControl1.TargetControl = this;
            // 
            // ProductCategoryMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1502, 839);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ProductCategoryMgt";
            this.Text = "상품카테고리관리";
            this.Load += new System.EventHandler(this.ProductCategoryMgt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductCategoryMgt_KeyDown);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductCategoryListDataGridView)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ComboBox SearchProductCategoryCodeComboBox;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox UseYnComboBox;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox ProductCategoryNameTextBox;
        internal System.Windows.Forms.TextBox UpperCategoryCodeTextBox;
        internal System.Windows.Forms.TextBox ProductCategoryCodeTextBox;
        internal System.Windows.Forms.TextBox LowerCategoryCodeTextBox;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox ProductCategoryDescriptionTextBox;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox LowestCategoryYNComboBox;
        internal System.Windows.Forms.Label label6;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAdvenceButton deleteButton;
        private Guna.UI.WinForms.GunaAdvenceButton closeButton;
        private Guna.UI.WinForms.GunaAdvenceButton saveButton;
        private Guna.UI.WinForms.GunaAdvenceButton resetButton;
        private Guna.UI.WinForms.GunaAdvenceButton searchProductCategoryListButton;
        private Guna.UI.WinForms.GunaDataGridView ProductCategoryListDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
    }
}