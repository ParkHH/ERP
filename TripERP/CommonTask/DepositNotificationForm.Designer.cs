namespace TripERP.CommonTask
{
    partial class DepositNotificationForm
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
            this.GunaPictureBox1 = new Guna.UI.WinForms.GunaPictureBox();
            this.GunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaPictureBox2 = new Guna.UI.WinForms.GunaPictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GunaPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gunaPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // GunaPictureBox1
            // 
            this.GunaPictureBox1.BaseColor = System.Drawing.Color.White;
            this.GunaPictureBox1.Image = global::TripERP.Properties.Resources.check_white;
            this.GunaPictureBox1.Location = new System.Drawing.Point(33, 37);
            this.GunaPictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GunaPictureBox1.Name = "GunaPictureBox1";
            this.GunaPictureBox1.Size = new System.Drawing.Size(61, 52);
            this.GunaPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GunaPictureBox1.TabIndex = 0;
            this.GunaPictureBox1.TabStop = false;
            // 
            // GunaLabel1
            // 
            this.GunaLabel1.AutoSize = true;
            this.GunaLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.GunaLabel1.Location = new System.Drawing.Point(123, 22);
            this.GunaLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GunaLabel1.Name = "GunaLabel1";
            this.GunaLabel1.Size = new System.Drawing.Size(183, 45);
            this.GunaLabel1.TabIndex = 1;
            this.GunaLabel1.Text = "gunaLabel1";
            // 
            // gunaPictureBox2
            // 
            this.gunaPictureBox2.BaseColor = System.Drawing.Color.White;
            this.gunaPictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gunaPictureBox2.Image = global::TripERP.Properties.Resources.multiply;
            this.gunaPictureBox2.Location = new System.Drawing.Point(763, 54);
            this.gunaPictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gunaPictureBox2.Name = "gunaPictureBox2";
            this.gunaPictureBox2.Size = new System.Drawing.Size(18, 18);
            this.gunaPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.gunaPictureBox2.TabIndex = 2;
            this.gunaPictureBox2.TabStop = false;
            this.gunaPictureBox2.Click += new System.EventHandler(this.gunaPictureBox2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GunaPanel1
            // 
            this.GunaPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GunaPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.GunaPanel1.Location = new System.Drawing.Point(0, 0);
            this.GunaPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GunaPanel1.Name = "GunaPanel1";
            this.GunaPanel1.Size = new System.Drawing.Size(8, 130);
            this.GunaPanel1.TabIndex = 3;
            // 
            // DepositNotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(171)))), ((int)(((byte)(160)))));
            this.ClientSize = new System.Drawing.Size(822, 130);
            this.Controls.Add(this.GunaPanel1);
            this.Controls.Add(this.gunaPictureBox2);
            this.Controls.Add(this.GunaLabel1);
            this.Controls.Add(this.GunaPictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DepositNotificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DepositNotificationForm";
            this.Load += new System.EventHandler(this.DepositNotificationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GunaPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gunaPictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Guna.UI.WinForms.GunaPictureBox GunaPictureBox1;
        private Guna.UI.WinForms.GunaLabel GunaLabel1;
        internal Guna.UI.WinForms.GunaPictureBox gunaPictureBox2;
        private System.Windows.Forms.Timer timer1;
        internal Guna.UI.WinForms.GunaPanel GunaPanel1;
    }
}