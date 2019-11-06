namespace TripERP.DocumentMgt
{
    partial class DocumentTemplateMgt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentTemplateMgt));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_selecteFile = new Guna.UI.WinForms.GunaAdvenceButton();
            this.bt_cancel = new Guna.UI.WinForms.GunaAdvenceButton();
            this.bt_upload = new Guna.UI.WinForms.GunaAdvenceButton();
            this.label4 = new System.Windows.Forms.Label();
            this.filePathNameTextBox = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ProductComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CompanyComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaDragControl2 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaControlBox3 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.groupBox1.SuspendLayout();
            this.gunaPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_selecteFile);
            this.groupBox1.Controls.Add(this.bt_cancel);
            this.groupBox1.Controls.Add(this.bt_upload);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.filePathNameTextBox);
            this.groupBox1.Controls.Add(this.lblPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ProductComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CompanyComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FileTypeComboBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 328);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template Upload";
            // 
            // bt_selecteFile
            // 
            this.bt_selecteFile.AnimationHoverSpeed = 0.07F;
            this.bt_selecteFile.AnimationSpeed = 0.03F;
            this.bt_selecteFile.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.bt_selecteFile.BorderColor = System.Drawing.Color.Black;
            this.bt_selecteFile.CheckedBaseColor = System.Drawing.Color.Gray;
            this.bt_selecteFile.CheckedBorderColor = System.Drawing.Color.Black;
            this.bt_selecteFile.CheckedForeColor = System.Drawing.Color.White;
            this.bt_selecteFile.CheckedImage = ((System.Drawing.Image)(resources.GetObject("bt_selecteFile.CheckedImage")));
            this.bt_selecteFile.CheckedLineColor = System.Drawing.Color.DimGray;
            this.bt_selecteFile.FocusedColor = System.Drawing.Color.Empty;
            this.bt_selecteFile.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_selecteFile.ForeColor = System.Drawing.Color.White;
            this.bt_selecteFile.Image = ((System.Drawing.Image)(resources.GetObject("bt_selecteFile.Image")));
            this.bt_selecteFile.ImageSize = new System.Drawing.Size(20, 20);
            this.bt_selecteFile.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_selecteFile.Location = new System.Drawing.Point(464, 78);
            this.bt_selecteFile.Name = "bt_selecteFile";
            this.bt_selecteFile.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.bt_selecteFile.OnHoverBorderColor = System.Drawing.Color.Black;
            this.bt_selecteFile.OnHoverForeColor = System.Drawing.Color.White;
            this.bt_selecteFile.OnHoverImage = null;
            this.bt_selecteFile.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_selecteFile.OnPressedColor = System.Drawing.Color.Black;
            this.bt_selecteFile.Size = new System.Drawing.Size(84, 30);
            this.bt_selecteFile.TabIndex = 1;
            this.bt_selecteFile.Text = "찾기";
            this.bt_selecteFile.Click += new System.EventHandler(this.bt_selecteFile_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.AnimationHoverSpeed = 0.07F;
            this.bt_cancel.AnimationSpeed = 0.03F;
            this.bt_cancel.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.bt_cancel.BorderColor = System.Drawing.Color.Black;
            this.bt_cancel.CheckedBaseColor = System.Drawing.Color.Gray;
            this.bt_cancel.CheckedBorderColor = System.Drawing.Color.Black;
            this.bt_cancel.CheckedForeColor = System.Drawing.Color.White;
            this.bt_cancel.CheckedImage = ((System.Drawing.Image)(resources.GetObject("bt_cancel.CheckedImage")));
            this.bt_cancel.CheckedLineColor = System.Drawing.Color.DimGray;
            this.bt_cancel.FocusedColor = System.Drawing.Color.Empty;
            this.bt_cancel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_cancel.ForeColor = System.Drawing.Color.White;
            this.bt_cancel.Image = ((System.Drawing.Image)(resources.GetObject("bt_cancel.Image")));
            this.bt_cancel.ImageSize = new System.Drawing.Size(20, 20);
            this.bt_cancel.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_cancel.Location = new System.Drawing.Point(295, 270);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.bt_cancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.bt_cancel.OnHoverForeColor = System.Drawing.Color.White;
            this.bt_cancel.OnHoverImage = null;
            this.bt_cancel.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_cancel.OnPressedColor = System.Drawing.Color.Black;
            this.bt_cancel.Size = new System.Drawing.Size(99, 33);
            this.bt_cancel.TabIndex = 6;
            this.bt_cancel.Text = " 닫기";
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // bt_upload
            // 
            this.bt_upload.AnimationHoverSpeed = 0.07F;
            this.bt_upload.AnimationSpeed = 0.03F;
            this.bt_upload.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.bt_upload.BorderColor = System.Drawing.Color.Black;
            this.bt_upload.CheckedBaseColor = System.Drawing.Color.Gray;
            this.bt_upload.CheckedBorderColor = System.Drawing.Color.Black;
            this.bt_upload.CheckedForeColor = System.Drawing.Color.White;
            this.bt_upload.CheckedImage = ((System.Drawing.Image)(resources.GetObject("bt_upload.CheckedImage")));
            this.bt_upload.CheckedLineColor = System.Drawing.Color.DimGray;
            this.bt_upload.FocusedColor = System.Drawing.Color.Empty;
            this.bt_upload.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_upload.ForeColor = System.Drawing.Color.White;
            this.bt_upload.Image = ((System.Drawing.Image)(resources.GetObject("bt_upload.Image")));
            this.bt_upload.ImageSize = new System.Drawing.Size(20, 20);
            this.bt_upload.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_upload.Location = new System.Drawing.Point(185, 270);
            this.bt_upload.Name = "bt_upload";
            this.bt_upload.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.bt_upload.OnHoverBorderColor = System.Drawing.Color.Black;
            this.bt_upload.OnHoverForeColor = System.Drawing.Color.White;
            this.bt_upload.OnHoverImage = null;
            this.bt_upload.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.bt_upload.OnPressedColor = System.Drawing.Color.Black;
            this.bt_upload.Size = new System.Drawing.Size(99, 33);
            this.bt_upload.TabIndex = 5;
            this.bt_upload.Text = " 선택";
            this.bt_upload.Click += new System.EventHandler(this.bt_upload_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(13, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(342, 21);
            this.label4.TabIndex = 35;
            this.label4.Text = "* Template File 을 Server 에 Upload 합니다.";
            // 
            // filePathNameTextBox
            // 
            this.filePathNameTextBox.Location = new System.Drawing.Point(99, 79);
            this.filePathNameTextBox.Name = "filePathNameTextBox";
            this.filePathNameTextBox.ReadOnly = true;
            this.filePathNameTextBox.Size = new System.Drawing.Size(359, 29);
            this.filePathNameTextBox.TabIndex = 31;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(9, 82);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(80, 21);
            this.lblPath.TabIndex = 30;
            this.lblPath.Text = "파일 선택";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "상품명";
            // 
            // ProductComboBox
            // 
            this.ProductComboBox.FormattingEnabled = true;
            this.ProductComboBox.Location = new System.Drawing.Point(95, 219);
            this.ProductComboBox.Name = "ProductComboBox";
            this.ProductComboBox.Size = new System.Drawing.Size(453, 29);
            this.ProductComboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "모객업체";
            // 
            // CompanyComboBox
            // 
            this.CompanyComboBox.FormattingEnabled = true;
            this.CompanyComboBox.Location = new System.Drawing.Point(95, 171);
            this.CompanyComboBox.Name = "CompanyComboBox";
            this.CompanyComboBox.Size = new System.Drawing.Size(453, 29);
            this.CompanyComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "문서종류";
            // 
            // FileTypeComboBox
            // 
            this.FileTypeComboBox.FormattingEnabled = true;
            this.FileTypeComboBox.Location = new System.Drawing.Point(95, 124);
            this.FileTypeComboBox.Name = "FileTypeComboBox";
            this.FileTypeComboBox.Size = new System.Drawing.Size(453, 29);
            this.FileTypeComboBox.TabIndex = 2;
            this.FileTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.FileTypeComboBox_SelectedIndexChanged);
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
            this.gunaPanel1.Size = new System.Drawing.Size(587, 32);
            this.gunaPanel1.TabIndex = 300;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.gunaLabel1.ForeColor = System.Drawing.Color.White;
            this.gunaLabel1.Location = new System.Drawing.Point(8, 5);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(130, 23);
            this.gunaLabel1.TabIndex = 3;
            this.gunaLabel1.Text = "문서템플릿 관리";
            // 
            // gunaControlBox3
            // 
            this.gunaControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox3.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox3.AnimationSpeed = 0.03F;
            this.gunaControlBox3.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
            this.gunaControlBox3.IconColor = System.Drawing.Color.White;
            this.gunaControlBox3.IconSize = 15F;
            this.gunaControlBox3.Location = new System.Drawing.Point(482, 2);
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
            this.gunaControlBox2.Location = new System.Drawing.Point(518, 2);
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
            this.gunaControlBox1.Location = new System.Drawing.Point(554, 2);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(30, 30);
            this.gunaControlBox1.TabIndex = 0;
            // 
            // DocumentTemplateMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(587, 388);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DocumentTemplateMgt";
            this.Text = "문서템플릿관리";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gunaPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ProductComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CompanyComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox FileTypeComboBox;
        private System.Windows.Forms.TextBox filePathNameTextBox;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label label4;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl2;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox3;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Guna.UI.WinForms.GunaAdvenceButton bt_selecteFile;
        private Guna.UI.WinForms.GunaAdvenceButton bt_cancel;
        private Guna.UI.WinForms.GunaAdvenceButton bt_upload;
    }
}