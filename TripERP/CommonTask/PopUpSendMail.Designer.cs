namespace TripERP.CommonTask
{
    partial class PopUpSendMail
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
            System.Windows.Forms.Label S_MSG_CONTLabel;
            System.Windows.Forms.Label S_MSG_NameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpSendMail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_mail_sender = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_mail_receiver = new System.Windows.Forms.TextBox();
            this.txt_mail_sender_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_mail_subject = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_mail_pw = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_mail_receiver_cc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_mail_body = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchCustomerButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Mail_Send_2 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btn_close = new Guna.UI.WinForms.GunaAdvenceButton();
            this.FileDeleteButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.FileSearchButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.EmailTemplateListDataGridView = new Guna.UI.WinForms.GunaDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SearchTemplateTitleComboBox = new System.Windows.Forms.ComboBox();
            this.searchCostPriceListButton = new Guna.UI.WinForms.GunaAdvenceButton();
            this.SearchTemplateContentsTextBox = new System.Windows.Forms.TextBox();
            this.gunaResize1 = new Guna.UI.WinForms.GunaResize(this.components);
            this.gunaDragControl1 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaDragControl2 = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaControlBox3 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            S_MSG_CONTLabel = new System.Windows.Forms.Label();
            S_MSG_NameLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmailTemplateListDataGridView)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.gunaPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // S_MSG_CONTLabel
            // 
            S_MSG_CONTLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            S_MSG_CONTLabel.AutoSize = true;
            S_MSG_CONTLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            S_MSG_CONTLabel.Location = new System.Drawing.Point(10, 69);
            S_MSG_CONTLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            S_MSG_CONTLabel.Name = "S_MSG_CONTLabel";
            S_MSG_CONTLabel.Size = new System.Drawing.Size(74, 21);
            S_MSG_CONTLabel.TabIndex = 13;
            S_MSG_CONTLabel.Text = "메일내용";
            // 
            // S_MSG_NameLabel
            // 
            S_MSG_NameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            S_MSG_NameLabel.AutoSize = true;
            S_MSG_NameLabel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            S_MSG_NameLabel.Location = new System.Drawing.Point(10, 33);
            S_MSG_NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            S_MSG_NameLabel.Name = "S_MSG_NameLabel";
            S_MSG_NameLabel.Size = new System.Drawing.Size(74, 21);
            S_MSG_NameLabel.TabIndex = 3;
            S_MSG_NameLabel.Text = "메일제목";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "발신자 주소";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_mail_sender
            // 
            this.txt_mail_sender.BackColor = System.Drawing.Color.Beige;
            this.txt_mail_sender.Location = new System.Drawing.Point(122, 62);
            this.txt_mail_sender.Name = "txt_mail_sender";
            this.txt_mail_sender.Size = new System.Drawing.Size(230, 29);
            this.txt_mail_sender.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(383, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "받는 사람 주소";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_mail_receiver
            // 
            this.txt_mail_receiver.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt_mail_receiver.Location = new System.Drawing.Point(530, 20);
            this.txt_mail_receiver.Name = "txt_mail_receiver";
            this.txt_mail_receiver.Size = new System.Drawing.Size(192, 29);
            this.txt_mail_receiver.TabIndex = 1;
            this.txt_mail_receiver.TextChanged += new System.EventHandler(this.txt_mail_receiver_TextChanged);
            this.txt_mail_receiver.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_mail_receiver_KeyDown);
            // 
            // txt_mail_sender_name
            // 
            this.txt_mail_sender_name.BackColor = System.Drawing.Color.Beige;
            this.txt_mail_sender_name.Location = new System.Drawing.Point(122, 28);
            this.txt_mail_sender_name.Name = "txt_mail_sender_name";
            this.txt_mail_sender_name.Size = new System.Drawing.Size(140, 29);
            this.txt_mail_sender_name.TabIndex = 9;
            this.txt_mail_sender_name.Text = "티앤씨소프트";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "발신자 이름";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 25);
            this.label6.TabIndex = 8;
            this.label6.Text = "메일 제목";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_mail_subject
            // 
            this.txt_mail_subject.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt_mail_subject.Location = new System.Drawing.Point(23, 49);
            this.txt_mail_subject.Name = "txt_mail_subject";
            this.txt_mail_subject.Size = new System.Drawing.Size(492, 29);
            this.txt_mail_subject.TabIndex = 9;
            this.txt_mail_subject.Text = "GMail 시험발송메일 (제목입니다.)";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(10, 92);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 25);
            this.label8.TabIndex = 10;
            this.label8.Text = "패스워드";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_mail_pw
            // 
            this.txt_mail_pw.BackColor = System.Drawing.Color.Beige;
            this.txt_mail_pw.Location = new System.Drawing.Point(122, 97);
            this.txt_mail_pw.Name = "txt_mail_pw";
            this.txt_mail_pw.PasswordChar = '*';
            this.txt_mail_pw.Size = new System.Drawing.Size(230, 29);
            this.txt_mail_pw.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(383, 56);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 25);
            this.label9.TabIndex = 14;
            this.label9.Text = "참조(CC)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_mail_receiver_cc
            // 
            this.txt_mail_receiver_cc.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt_mail_receiver_cc.Location = new System.Drawing.Point(530, 55);
            this.txt_mail_receiver_cc.Name = "txt_mail_receiver_cc";
            this.txt_mail_receiver_cc.Size = new System.Drawing.Size(192, 29);
            this.txt_mail_receiver_cc.TabIndex = 3;
            this.txt_mail_receiver_cc.TextChanged += new System.EventHandler(this.txt_mail_receiver_cc_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(7, 135);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 25);
            this.label10.TabIndex = 15;
            this.label10.Text = "메일 내용";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_mail_body
            // 
            this.txt_mail_body.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt_mail_body.Location = new System.Drawing.Point(23, 165);
            this.txt_mail_body.Multiline = true;
            this.txt_mail_body.Name = "txt_mail_body";
            this.txt_mail_body.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_mail_body.Size = new System.Drawing.Size(578, 400);
            this.txt_mail_body.TabIndex = 13;
            this.txt_mail_body.Text = "메일 본문 내용입니다.\r\n_Attachment.jpg를 첨부 (특수문자)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchCustomerButton);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_mail_receiver_cc);
            this.groupBox1.Controls.Add(this.txt_mail_pw);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_mail_receiver);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_mail_sender_name);
            this.groupBox1.Controls.Add(this.txt_mail_sender);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 175);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "공통정보";
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
            this.searchCustomerButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchCustomerButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchCustomerButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCustomerButton.Location = new System.Drawing.Point(727, 20);
            this.searchCustomerButton.Name = "searchCustomerButton";
            this.searchCustomerButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchCustomerButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchCustomerButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchCustomerButton.OnHoverImage = null;
            this.searchCustomerButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCustomerButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchCustomerButton.Size = new System.Drawing.Size(35, 29);
            this.searchCustomerButton.TabIndex = 2;
            this.searchCustomerButton.Click += new System.EventHandler(this.searchCustomerButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.textBox2.Location = new System.Drawing.Point(530, 123);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(192, 29);
            this.textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.textBox1.Location = new System.Drawing.Point(530, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 29);
            this.textBox1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Mail_Send_2);
            this.groupBox2.Controls.Add(this.btn_close);
            this.groupBox2.Controls.Add(this.FileDeleteButton);
            this.groupBox2.Controls.Add(this.FileSearchButton);
            this.groupBox2.Controls.Add(this.FileNameTextBox);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txt_mail_body);
            this.groupBox2.Controls.Add(this.txt_mail_subject);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(787, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(606, 627);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "이메일발송";
            // 
            // btn_Mail_Send_2
            // 
            this.btn_Mail_Send_2.AnimationHoverSpeed = 0.07F;
            this.btn_Mail_Send_2.AnimationSpeed = 0.03F;
            this.btn_Mail_Send_2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.btn_Mail_Send_2.BorderColor = System.Drawing.Color.Black;
            this.btn_Mail_Send_2.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btn_Mail_Send_2.CheckedBorderColor = System.Drawing.Color.Black;
            this.btn_Mail_Send_2.CheckedForeColor = System.Drawing.Color.White;
            this.btn_Mail_Send_2.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btn_Mail_Send_2.CheckedImage")));
            this.btn_Mail_Send_2.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btn_Mail_Send_2.FocusedColor = System.Drawing.Color.Empty;
            this.btn_Mail_Send_2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Mail_Send_2.ForeColor = System.Drawing.Color.White;
            this.btn_Mail_Send_2.Image = ((System.Drawing.Image)(resources.GetObject("btn_Mail_Send_2.Image")));
            this.btn_Mail_Send_2.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_Mail_Send_2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btn_Mail_Send_2.Location = new System.Drawing.Point(175, 577);
            this.btn_Mail_Send_2.Name = "btn_Mail_Send_2";
            this.btn_Mail_Send_2.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btn_Mail_Send_2.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btn_Mail_Send_2.OnHoverForeColor = System.Drawing.Color.White;
            this.btn_Mail_Send_2.OnHoverImage = null;
            this.btn_Mail_Send_2.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btn_Mail_Send_2.OnPressedColor = System.Drawing.Color.Black;
            this.btn_Mail_Send_2.Size = new System.Drawing.Size(140, 33);
            this.btn_Mail_Send_2.TabIndex = 14;
            this.btn_Mail_Send_2.Text = " Mail 전송";
            this.btn_Mail_Send_2.Click += new System.EventHandler(this.btn_Mail_Send_2_Click);
            // 
            // btn_close
            // 
            this.btn_close.AnimationHoverSpeed = 0.07F;
            this.btn_close.AnimationSpeed = 0.03F;
            this.btn_close.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.btn_close.BorderColor = System.Drawing.Color.Black;
            this.btn_close.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btn_close.CheckedBorderColor = System.Drawing.Color.Black;
            this.btn_close.CheckedForeColor = System.Drawing.Color.White;
            this.btn_close.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btn_close.CheckedImage")));
            this.btn_close.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btn_close.FocusedColor = System.Drawing.Color.Empty;
            this.btn_close.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_close.ForeColor = System.Drawing.Color.White;
            this.btn_close.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.Image")));
            this.btn_close.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_close.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btn_close.Location = new System.Drawing.Point(324, 577);
            this.btn_close.Name = "btn_close";
            this.btn_close.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btn_close.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btn_close.OnHoverForeColor = System.Drawing.Color.White;
            this.btn_close.OnHoverImage = null;
            this.btn_close.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btn_close.OnPressedColor = System.Drawing.Color.Black;
            this.btn_close.Size = new System.Drawing.Size(140, 33);
            this.btn_close.TabIndex = 15;
            this.btn_close.Text = " 닫기";
            this.btn_close.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // FileDeleteButton
            // 
            this.FileDeleteButton.AnimationHoverSpeed = 0.07F;
            this.FileDeleteButton.AnimationSpeed = 0.03F;
            this.FileDeleteButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.FileDeleteButton.BorderColor = System.Drawing.Color.Black;
            this.FileDeleteButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.FileDeleteButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.FileDeleteButton.CheckedForeColor = System.Drawing.Color.White;
            this.FileDeleteButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("FileDeleteButton.CheckedImage")));
            this.FileDeleteButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.FileDeleteButton.FocusedColor = System.Drawing.Color.Empty;
            this.FileDeleteButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FileDeleteButton.ForeColor = System.Drawing.Color.White;
            this.FileDeleteButton.Image = null;
            this.FileDeleteButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FileDeleteButton.ImageSize = new System.Drawing.Size(20, 20);
            this.FileDeleteButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.FileDeleteButton.Location = new System.Drawing.Point(533, 106);
            this.FileDeleteButton.Name = "FileDeleteButton";
            this.FileDeleteButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.FileDeleteButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.FileDeleteButton.OnHoverForeColor = System.Drawing.Color.White;
            this.FileDeleteButton.OnHoverImage = null;
            this.FileDeleteButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.FileDeleteButton.OnPressedColor = System.Drawing.Color.Black;
            this.FileDeleteButton.Size = new System.Drawing.Size(54, 29);
            this.FileDeleteButton.TabIndex = 12;
            this.FileDeleteButton.Text = "삭제";
            this.FileDeleteButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FileDeleteButton.Click += new System.EventHandler(this.FileDeleteButton_Click);
            // 
            // FileSearchButton
            // 
            this.FileSearchButton.AnimationHoverSpeed = 0.07F;
            this.FileSearchButton.AnimationSpeed = 0.03F;
            this.FileSearchButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.FileSearchButton.BorderColor = System.Drawing.Color.Black;
            this.FileSearchButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.FileSearchButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.FileSearchButton.CheckedForeColor = System.Drawing.Color.White;
            this.FileSearchButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("FileSearchButton.CheckedImage")));
            this.FileSearchButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.FileSearchButton.FocusedColor = System.Drawing.Color.Empty;
            this.FileSearchButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FileSearchButton.ForeColor = System.Drawing.Color.White;
            this.FileSearchButton.Image = null;
            this.FileSearchButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FileSearchButton.ImageSize = new System.Drawing.Size(20, 20);
            this.FileSearchButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.FileSearchButton.Location = new System.Drawing.Point(473, 106);
            this.FileSearchButton.Name = "FileSearchButton";
            this.FileSearchButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.FileSearchButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.FileSearchButton.OnHoverForeColor = System.Drawing.Color.White;
            this.FileSearchButton.OnHoverImage = null;
            this.FileSearchButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.FileSearchButton.OnPressedColor = System.Drawing.Color.Black;
            this.FileSearchButton.Size = new System.Drawing.Size(54, 29);
            this.FileSearchButton.TabIndex = 11;
            this.FileSearchButton.Text = "찾기";
            this.FileSearchButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FileSearchButton.Click += new System.EventHandler(this.FileSearchButton_Click);
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(23, 106);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.ReadOnly = true;
            this.FileNameTextBox.Size = new System.Drawing.Size(441, 29);
            this.FileNameTextBox.TabIndex = 10;
            this.FileNameTextBox.TextChanged += new System.EventHandler(this.FileNameTextBox_TextChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(7, 78);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 25);
            this.label11.TabIndex = 53;
            this.label11.Text = "파일 첨부";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.EmailTemplateListDataGridView);
            this.groupBox3.Location = new System.Drawing.Point(12, 365);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(768, 321);
            this.groupBox3.TabIndex = 187;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "템플릿 목록";
            // 
            // EmailTemplateListDataGridView
            // 
            this.EmailTemplateListDataGridView.AllowUserToAddRows = false;
            this.EmailTemplateListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.EmailTemplateListDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.EmailTemplateListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EmailTemplateListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.EmailTemplateListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EmailTemplateListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmailTemplateListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.EmailTemplateListDataGridView.ColumnHeadersHeight = 30;
            this.EmailTemplateListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EmailTemplateListDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.EmailTemplateListDataGridView.EnableHeadersVisualStyles = false;
            this.EmailTemplateListDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.EmailTemplateListDataGridView.Location = new System.Drawing.Point(6, 25);
            this.EmailTemplateListDataGridView.Name = "EmailTemplateListDataGridView";
            this.EmailTemplateListDataGridView.RowHeadersVisible = false;
            this.EmailTemplateListDataGridView.RowTemplate.Height = 23;
            this.EmailTemplateListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EmailTemplateListDataGridView.Size = new System.Drawing.Size(756, 290);
            this.EmailTemplateListDataGridView.TabIndex = 288;
            this.EmailTemplateListDataGridView.Theme = Guna.UI.WinForms.GunaDataGridViewPresetThemes.Guna;
            this.EmailTemplateListDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.EmailTemplateListDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.EmailTemplateListDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.EmailTemplateListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.EmailTemplateListDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.EmailTemplateListDataGridView.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.EmailTemplateListDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.EmailTemplateListDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.EmailTemplateListDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.EmailTemplateListDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EmailTemplateListDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.EmailTemplateListDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.EmailTemplateListDataGridView.ThemeStyle.HeaderStyle.Height = 30;
            this.EmailTemplateListDataGridView.ThemeStyle.ReadOnly = false;
            this.EmailTemplateListDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.EmailTemplateListDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this.EmailTemplateListDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EmailTemplateListDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.EmailTemplateListDataGridView.ThemeStyle.RowsStyle.Height = 23;
            this.EmailTemplateListDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.EmailTemplateListDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.EmailTemplateListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmailTemplateListDataGridView_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "문자메세지번호";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "문자제목";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 200F;
            this.Column3.HeaderText = "문자내용";
            this.Column3.Name = "Column3";
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(S_MSG_CONTLabel);
            this.groupBox4.Controls.Add(this.SearchTemplateTitleComboBox);
            this.groupBox4.Controls.Add(this.searchCostPriceListButton);
            this.groupBox4.Controls.Add(this.SearchTemplateContentsTextBox);
            this.groupBox4.Controls.Add(S_MSG_NameLabel);
            this.groupBox4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.Location = new System.Drawing.Point(12, 250);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(397, 111);
            this.groupBox4.TabIndex = 186;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "템플릿 검색";
            // 
            // SearchTemplateTitleComboBox
            // 
            this.SearchTemplateTitleComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SearchTemplateTitleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SearchTemplateTitleComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SearchTemplateTitleComboBox.DisplayMember = "PRDT_NM";
            this.SearchTemplateTitleComboBox.DropDownHeight = 120;
            this.SearchTemplateTitleComboBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SearchTemplateTitleComboBox.FormattingEnabled = true;
            this.SearchTemplateTitleComboBox.IntegralHeight = false;
            this.SearchTemplateTitleComboBox.ItemHeight = 21;
            this.SearchTemplateTitleComboBox.Location = new System.Drawing.Point(85, 30);
            this.SearchTemplateTitleComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SearchTemplateTitleComboBox.MaxDropDownItems = 100;
            this.SearchTemplateTitleComboBox.Name = "SearchTemplateTitleComboBox";
            this.SearchTemplateTitleComboBox.Size = new System.Drawing.Size(210, 29);
            this.SearchTemplateTitleComboBox.TabIndex = 6;
            this.SearchTemplateTitleComboBox.ValueMember = "PRDT_CD";
            // 
            // searchCostPriceListButton
            // 
            this.searchCostPriceListButton.AnimationHoverSpeed = 0.07F;
            this.searchCostPriceListButton.AnimationSpeed = 0.03F;
            this.searchCostPriceListButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.searchCostPriceListButton.BorderColor = System.Drawing.Color.Black;
            this.searchCostPriceListButton.CheckedBaseColor = System.Drawing.Color.Gray;
            this.searchCostPriceListButton.CheckedBorderColor = System.Drawing.Color.Black;
            this.searchCostPriceListButton.CheckedForeColor = System.Drawing.Color.White;
            this.searchCostPriceListButton.CheckedImage = ((System.Drawing.Image)(resources.GetObject("searchCostPriceListButton.CheckedImage")));
            this.searchCostPriceListButton.CheckedLineColor = System.Drawing.Color.DimGray;
            this.searchCostPriceListButton.FocusedColor = System.Drawing.Color.Empty;
            this.searchCostPriceListButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.searchCostPriceListButton.ForeColor = System.Drawing.Color.White;
            this.searchCostPriceListButton.Image = ((System.Drawing.Image)(resources.GetObject("searchCostPriceListButton.Image")));
            this.searchCostPriceListButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchCostPriceListButton.ImageSize = new System.Drawing.Size(20, 20);
            this.searchCostPriceListButton.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCostPriceListButton.Location = new System.Drawing.Point(357, 69);
            this.searchCostPriceListButton.Name = "searchCostPriceListButton";
            this.searchCostPriceListButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.searchCostPriceListButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.searchCostPriceListButton.OnHoverForeColor = System.Drawing.Color.White;
            this.searchCostPriceListButton.OnHoverImage = null;
            this.searchCostPriceListButton.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.searchCostPriceListButton.OnPressedColor = System.Drawing.Color.Black;
            this.searchCostPriceListButton.Size = new System.Drawing.Size(35, 29);
            this.searchCostPriceListButton.TabIndex = 8;
            this.searchCostPriceListButton.Click += new System.EventHandler(this.searchCostPriceListButton_Click);
            // 
            // SearchTemplateContentsTextBox
            // 
            this.SearchTemplateContentsTextBox.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.SearchTemplateContentsTextBox.Location = new System.Drawing.Point(85, 69);
            this.SearchTemplateContentsTextBox.MaxLength = 10;
            this.SearchTemplateContentsTextBox.Name = "SearchTemplateContentsTextBox";
            this.SearchTemplateContentsTextBox.Size = new System.Drawing.Size(267, 29);
            this.SearchTemplateContentsTextBox.TabIndex = 7;
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
            this.gunaPanel1.Size = new System.Drawing.Size(1405, 32);
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
            this.gunaLabel1.Text = "이메일 전송";
            // 
            // gunaControlBox3
            // 
            this.gunaControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox3.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox3.AnimationSpeed = 0.03F;
            this.gunaControlBox3.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
            this.gunaControlBox3.IconColor = System.Drawing.Color.White;
            this.gunaControlBox3.IconSize = 15F;
            this.gunaControlBox3.Location = new System.Drawing.Point(1300, 2);
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
            this.gunaControlBox2.Location = new System.Drawing.Point(1336, 2);
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
            this.gunaControlBox1.Location = new System.Drawing.Point(1372, 2);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(30, 30);
            this.gunaControlBox1.TabIndex = 0;
            // 
            // PopUpSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1405, 777);
            this.Controls.Add(this.gunaPanel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PopUpSendMail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.PopUpSendMail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EmailTemplateListDataGridView)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gunaPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_mail_sender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_mail_receiver;
        private System.Windows.Forms.TextBox txt_mail_sender_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_mail_subject;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_mail_pw;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_mail_receiver_cc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_mail_body;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.ComboBox SearchTemplateTitleComboBox;
        internal System.Windows.Forms.TextBox SearchTemplateContentsTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private Guna.UI.WinForms.GunaResize gunaResize1;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl1;
        private Guna.UI.WinForms.GunaAdvenceButton searchCustomerButton;
        private Guna.UI.WinForms.GunaAdvenceButton searchCostPriceListButton;
        private Guna.UI.WinForms.GunaAdvenceButton FileDeleteButton;
        private Guna.UI.WinForms.GunaAdvenceButton FileSearchButton;
        private Guna.UI.WinForms.GunaAdvenceButton btn_Mail_Send_2;
        private Guna.UI.WinForms.GunaAdvenceButton btn_close;
        private Guna.UI.WinForms.GunaDataGridView EmailTemplateListDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private Guna.UI.WinForms.GunaDragControl gunaDragControl2;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox3;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
    }
}