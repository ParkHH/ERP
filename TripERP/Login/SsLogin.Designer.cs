namespace TripERP.Login
{
    partial class SsLogin
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
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.IdStoreCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.otpNumTextBox = new System.Windows.Forms.TextBox();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.Label2.Location = new System.Drawing.Point(987, 583);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(98, 21);
            this.Label2.TabIndex = 13;
            this.Label2.Text = "비 밀  번 호";
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.Label1.Location = new System.Drawing.Point(987, 542);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(94, 21);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "아   이   디";
            // 
            // loginButton
            // 
            this.loginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loginButton.AutoEllipsis = true;
            this.loginButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loginButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.loginButton.Location = new System.Drawing.Point(1321, 580);
            this.loginButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(89, 71);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "로 그 인";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.passwordTextBox.Location = new System.Drawing.Point(1111, 579);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.Size = new System.Drawing.Size(172, 29);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.Text = "1";
            // 
            // idTextBox
            // 
            this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.idTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.idTextBox.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.idTextBox.Location = new System.Drawing.Point(1111, 538);
            this.idTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(172, 29);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.Text = global::TripERP.Properties.Settings.Default.LoginID;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PictureBox1.Image = global::TripERP.Properties.Resources.G_Bridge;
            this.PictureBox1.Location = new System.Drawing.Point(224, 118);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(540, 540);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 14;
            this.PictureBox1.TabStop = false;
            // 
            // IdStoreCheckBox
            // 
            this.IdStoreCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.IdStoreCheckBox.AutoSize = true;
            this.IdStoreCheckBox.Checked = global::TripERP.Properties.Settings.Default.LoginID_Checked;
            this.IdStoreCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IdStoreCheckBox.Location = new System.Drawing.Point(1111, 665);
            this.IdStoreCheckBox.Name = "IdStoreCheckBox";
            this.IdStoreCheckBox.Size = new System.Drawing.Size(115, 25);
            this.IdStoreCheckBox.TabIndex = 4;
            this.IdStoreCheckBox.Text = "아이디 저장";
            this.IdStoreCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label3.Location = new System.Drawing.Point(987, 625);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "O T P 번 호";
            // 
            // otpNumTextBox
            // 
            this.otpNumTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.otpNumTextBox.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.otpNumTextBox.Location = new System.Drawing.Point(1111, 622);
            this.otpNumTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.otpNumTextBox.Name = "otpNumTextBox";
            this.otpNumTextBox.PasswordChar = '●';
            this.otpNumTextBox.ReadOnly = true;
            this.otpNumTextBox.Size = new System.Drawing.Size(172, 29);
            this.otpNumTextBox.TabIndex = 3;
            // 
            // gunaResize1
            // 
            this.gunaResize1.TargetForm = this;
            // 
            // SsLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1584, 781);
            this.Controls.Add(this.otpNumTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IdStoreCheckBox);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.idTextBox);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SsLogin";
            this.Text = "로그인";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SsLogin_FormClosed);
            this.Load += new System.EventHandler(this.SsLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button loginButton;
        internal System.Windows.Forms.TextBox passwordTextBox;
        internal System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.CheckBox IdStoreCheckBox;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox otpNumTextBox;
        private Guna.UI.WinForms.GunaResize gunaResize1;
    }
}