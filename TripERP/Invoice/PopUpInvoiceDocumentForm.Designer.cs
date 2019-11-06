namespace TripERP.Invoice
{
    partial class PopUpInvoiceDocumentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpInvoiceDocumentForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button6 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.label8 = new System.Windows.Forms.Label();
            this.button7 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.tb_date = new System.Windows.Forms.TextBox();
            this.tb_year = new System.Windows.Forms.TextBox();
            this.tb_month = new System.Windows.Forms.TextBox();
            this.cb_account_foreign = new System.Windows.Forms.ComboBox();
            this.cb_account_won = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_foreign_account = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_won_account = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.groupBox2.Location = new System.Drawing.Point(16, 21);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.groupBox2.Size = new System.Drawing.Size(1318, 866);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Invoice Preview";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1805, 1939);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(162, 29);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1628, 1948);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "정산기준환율";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2863, 1939);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 89);
            this.button1.TabIndex = 5;
            this.button1.Text = "닫기";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(2412, 1939);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 89);
            this.button2.TabIndex = 4;
            this.button2.Text = "정산실행";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(2713, 1939);
            this.button3.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 89);
            this.button3.TabIndex = 2;
            this.button3.Text = "인쇄";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(2562, 1939);
            this.button4.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 89);
            this.button4.TabIndex = 1;
            this.button4.Text = "엑셀";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "";
            this.reportViewer1.LocalReport.ReportPath = "";
            this.reportViewer1.Location = new System.Drawing.Point(25, 52);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1300, 892);
            this.reportViewer1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.tb_date);
            this.groupBox3.Controls.Add(this.tb_year);
            this.groupBox3.Controls.Add(this.tb_month);
            this.groupBox3.Controls.Add(this.cb_account_foreign);
            this.groupBox3.Controls.Add(this.cb_account_won);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tb_foreign_account);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tb_won_account);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.groupBox3.Location = new System.Drawing.Point(1342, 21);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(446, 442);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "입력값";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(313, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 21);
            this.label10.TabIndex = 13;
            this.label10.Text = "일";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(204, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 21);
            this.label9.TabIndex = 12;
            this.label9.Text = "월";
            // 
            // button6
            // 
            this.button6.AnimationHoverSpeed = 0.07F;
            this.button6.AnimationSpeed = 0.03F;
            this.button6.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.button6.BorderColor = System.Drawing.Color.Black;
            this.button6.CheckedBaseColor = System.Drawing.Color.Gray;
            this.button6.CheckedBorderColor = System.Drawing.Color.Black;
            this.button6.CheckedForeColor = System.Drawing.Color.White;
            this.button6.CheckedImage = ((System.Drawing.Image)(resources.GetObject("button6.CheckedImage")));
            this.button6.CheckedLineColor = System.Drawing.Color.DimGray;
            this.button6.FocusedColor = System.Drawing.Color.Empty;
            this.button6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageSize = new System.Drawing.Size(20, 20);
            this.button6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button6.Location = new System.Drawing.Point(169, 370);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button6.Name = "button6";
            this.button6.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.button6.OnHoverBorderColor = System.Drawing.Color.Black;
            this.button6.OnHoverForeColor = System.Drawing.Color.White;
            this.button6.OnHoverImage = null;
            this.button6.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button6.OnPressedColor = System.Drawing.Color.Black;
            this.button6.Size = new System.Drawing.Size(99, 43);
            this.button6.TabIndex = 8;
            this.button6.Text = "초기화";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(96, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 21);
            this.label8.TabIndex = 11;
            this.label8.Text = "년";
            // 
            // button7
            // 
            this.button7.AnimationHoverSpeed = 0.07F;
            this.button7.AnimationSpeed = 0.03F;
            this.button7.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.button7.BorderColor = System.Drawing.Color.Black;
            this.button7.CheckedBaseColor = System.Drawing.Color.Gray;
            this.button7.CheckedBorderColor = System.Drawing.Color.Black;
            this.button7.CheckedForeColor = System.Drawing.Color.White;
            this.button7.CheckedImage = ((System.Drawing.Image)(resources.GetObject("button7.CheckedImage")));
            this.button7.CheckedLineColor = System.Drawing.Color.DimGray;
            this.button7.FocusedColor = System.Drawing.Color.Empty;
            this.button7.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageSize = new System.Drawing.Size(20, 20);
            this.button7.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button7.Location = new System.Drawing.Point(283, 370);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button7.Name = "button7";
            this.button7.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.button7.OnHoverBorderColor = System.Drawing.Color.Black;
            this.button7.OnHoverForeColor = System.Drawing.Color.White;
            this.button7.OnHoverImage = null;
            this.button7.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.button7.OnPressedColor = System.Drawing.Color.Black;
            this.button7.Size = new System.Drawing.Size(99, 43);
            this.button7.TabIndex = 9;
            this.button7.Text = " 적용";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // tb_date
            // 
            this.tb_date.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.tb_date.Location = new System.Drawing.Point(241, 87);
            this.tb_date.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_date.Name = "tb_date";
            this.tb_date.Size = new System.Drawing.Size(69, 29);
            this.tb_date.TabIndex = 3;
            this.tb_date.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // tb_year
            // 
            this.tb_year.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.tb_year.Location = new System.Drawing.Point(25, 87);
            this.tb_year.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_year.Name = "tb_year";
            this.tb_year.Size = new System.Drawing.Size(69, 29);
            this.tb_year.TabIndex = 1;
            this.tb_year.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // tb_month
            // 
            this.tb_month.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.tb_month.Location = new System.Drawing.Point(133, 87);
            this.tb_month.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_month.Name = "tb_month";
            this.tb_month.Size = new System.Drawing.Size(69, 29);
            this.tb_month.TabIndex = 2;
            this.tb_month.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // cb_account_foreign
            // 
            this.cb_account_foreign.FormattingEnabled = true;
            this.cb_account_foreign.Location = new System.Drawing.Point(25, 282);
            this.cb_account_foreign.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_account_foreign.Name = "cb_account_foreign";
            this.cb_account_foreign.Size = new System.Drawing.Size(157, 29);
            this.cb_account_foreign.TabIndex = 6;
            // 
            // cb_account_won
            // 
            this.cb_account_won.FormattingEnabled = true;
            this.cb_account_won.Location = new System.Drawing.Point(25, 184);
            this.cb_account_won.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_account_won.Name = "cb_account_won";
            this.cb_account_won.Size = new System.Drawing.Size(157, 29);
            this.cb_account_won.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 5;
            this.label7.Text = "외화계좌";
            // 
            // tb_foreign_account
            // 
            this.tb_foreign_account.Location = new System.Drawing.Point(188, 282);
            this.tb_foreign_account.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_foreign_account.Name = "tb_foreign_account";
            this.tb_foreign_account.Size = new System.Drawing.Size(226, 29);
            this.tb_foreign_account.TabIndex = 7;
            this.tb_foreign_account.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "원화계좌";
            // 
            // tb_won_account
            // 
            this.tb_won_account.Location = new System.Drawing.Point(188, 184);
            this.tb_won_account.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_won_account.Name = "tb_won_account";
            this.tb_won_account.Size = new System.Drawing.Size(226, 29);
            this.tb_won_account.TabIndex = 5;
            this.tb_won_account.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterKeyPressed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "납기일";
            // 
            // PopUpInvoiceDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1792, 901);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PopUpInvoiceDocumentForm";
            this.Text = "PopUpInvoiceDocumentForm";
            this.Load += new System.EventHandler(this.PopUpInvoiceDocumentForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button button2;
        internal System.Windows.Forms.Button button3;
        internal System.Windows.Forms.Button button4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private Guna.UI.WinForms.GunaAdvenceButton button6;
        private System.Windows.Forms.Label label8;
        private Guna.UI.WinForms.GunaAdvenceButton button7;
        private System.Windows.Forms.TextBox tb_date;
        private System.Windows.Forms.TextBox tb_year;
        private System.Windows.Forms.TextBox tb_month;
        private System.Windows.Forms.ComboBox cb_account_foreign;
        private System.Windows.Forms.ComboBox cb_account_won;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_foreign_account;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_won_account;
        private System.Windows.Forms.Label label4;
    }
}