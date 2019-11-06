namespace TripERP.Dashboard
{
    partial class DashboardForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flIncome = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.nextMonth = new System.Windows.Forms.Button();
            this.prevMonth = new System.Windows.Forms.Button();
            this.lblYearAndMonth = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSaturday = new System.Windows.Forms.Label();
            this.lblFriday = new System.Windows.Forms.Label();
            this.lblThursday = new System.Windows.Forms.Label();
            this.lblWendsDay = new System.Windows.Forms.Label();
            this.lblTuesDay = new System.Windows.Forms.Label();
            this.lblMonday = new System.Windows.Forms.Label();
            this.lblSunday = new System.Windows.Forms.Label();
            this.flDays = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.panel4);
            this.flowLayoutPanel1.Controls.Add(this.flIncome);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1554, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(370, 1055);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Cornsilk;
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(365, 39);
            this.panel4.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::TripERP.Properties.Resources.refresh;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(322, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 27);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "입금내역";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flIncome
            // 
            this.flIncome.AutoScroll = true;
            this.flIncome.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.flIncome.Location = new System.Drawing.Point(1, 43);
            this.flIncome.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.flIncome.Name = "flIncome";
            this.flIncome.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.flIncome.Size = new System.Drawing.Size(365, 220);
            this.flIncome.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.lblYearAndMonth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1554, 94);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.nextMonth);
            this.panel3.Controls.Add(this.prevMonth);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1267, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(283, 90);
            this.panel3.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(105, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Today";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nextMonth
            // 
            this.nextMonth.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.nextMonth.Location = new System.Drawing.Point(197, 28);
            this.nextMonth.Name = "nextMonth";
            this.nextMonth.Size = new System.Drawing.Size(73, 32);
            this.nextMonth.TabIndex = 1;
            this.nextMonth.Text = ">";
            this.nextMonth.UseVisualStyleBackColor = true;
            this.nextMonth.Click += new System.EventHandler(this.nextMonth_Click);
            // 
            // prevMonth
            // 
            this.prevMonth.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.prevMonth.Location = new System.Drawing.Point(12, 28);
            this.prevMonth.Name = "prevMonth";
            this.prevMonth.Size = new System.Drawing.Size(73, 32);
            this.prevMonth.TabIndex = 0;
            this.prevMonth.Text = "<";
            this.prevMonth.UseVisualStyleBackColor = true;
            this.prevMonth.Click += new System.EventHandler(this.prevMonth_Click);
            // 
            // lblYearAndMonth
            // 
            this.lblYearAndMonth.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblYearAndMonth.Location = new System.Drawing.Point(10, 19);
            this.lblYearAndMonth.Name = "lblYearAndMonth";
            this.lblYearAndMonth.Size = new System.Drawing.Size(264, 50);
            this.lblYearAndMonth.TabIndex = 0;
            this.lblYearAndMonth.Text = "2019년, 7월";
            this.lblYearAndMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblSaturday);
            this.panel2.Controls.Add(this.lblFriday);
            this.panel2.Controls.Add(this.lblThursday);
            this.panel2.Controls.Add(this.lblWendsDay);
            this.panel2.Controls.Add(this.lblTuesDay);
            this.panel2.Controls.Add(this.lblMonday);
            this.panel2.Controls.Add(this.lblSunday);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1554, 46);
            this.panel2.TabIndex = 4;
            // 
            // lblSaturday
            // 
            this.lblSaturday.AutoSize = true;
            this.lblSaturday.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold);
            this.lblSaturday.Location = new System.Drawing.Point(1324, 6);
            this.lblSaturday.Name = "lblSaturday";
            this.lblSaturday.Size = new System.Drawing.Size(37, 31);
            this.lblSaturday.TabIndex = 7;
            this.lblSaturday.Text = "토";
            this.lblSaturday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFriday
            // 
            this.lblFriday.AutoSize = true;
            this.lblFriday.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold);
            this.lblFriday.Location = new System.Drawing.Point(1123, 6);
            this.lblFriday.Name = "lblFriday";
            this.lblFriday.Size = new System.Drawing.Size(37, 31);
            this.lblFriday.TabIndex = 13;
            this.lblFriday.Text = "금";
            this.lblFriday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblThursday
            // 
            this.lblThursday.AutoSize = true;
            this.lblThursday.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold);
            this.lblThursday.Location = new System.Drawing.Point(904, 6);
            this.lblThursday.Name = "lblThursday";
            this.lblThursday.Size = new System.Drawing.Size(37, 31);
            this.lblThursday.TabIndex = 12;
            this.lblThursday.Text = "목";
            this.lblThursday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWendsDay
            // 
            this.lblWendsDay.AutoSize = true;
            this.lblWendsDay.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold);
            this.lblWendsDay.Location = new System.Drawing.Point(702, 6);
            this.lblWendsDay.Name = "lblWendsDay";
            this.lblWendsDay.Size = new System.Drawing.Size(37, 31);
            this.lblWendsDay.TabIndex = 11;
            this.lblWendsDay.Text = "수";
            this.lblWendsDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTuesDay
            // 
            this.lblTuesDay.AutoSize = true;
            this.lblTuesDay.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold);
            this.lblTuesDay.Location = new System.Drawing.Point(503, 6);
            this.lblTuesDay.Name = "lblTuesDay";
            this.lblTuesDay.Size = new System.Drawing.Size(37, 31);
            this.lblTuesDay.TabIndex = 10;
            this.lblTuesDay.Text = "화";
            this.lblTuesDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMonday
            // 
            this.lblMonday.AutoSize = true;
            this.lblMonday.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold);
            this.lblMonday.Location = new System.Drawing.Point(292, 6);
            this.lblMonday.Name = "lblMonday";
            this.lblMonday.Size = new System.Drawing.Size(37, 31);
            this.lblMonday.TabIndex = 9;
            this.lblMonday.Text = "월";
            this.lblMonday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSunday
            // 
            this.lblSunday.AutoSize = true;
            this.lblSunday.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold);
            this.lblSunday.Location = new System.Drawing.Point(85, 6);
            this.lblSunday.Name = "lblSunday";
            this.lblSunday.Size = new System.Drawing.Size(37, 31);
            this.lblSunday.TabIndex = 8;
            this.lblSunday.Text = "일";
            this.lblSunday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flDays
            // 
            this.flDays.BackColor = System.Drawing.Color.White;
            this.flDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flDays.Location = new System.Drawing.Point(0, 140);
            this.flDays.Name = "flDays";
            this.flDays.Size = new System.Drawing.Size(1554, 915);
            this.flDays.TabIndex = 5;
            this.flDays.MouseMove += new System.Windows.Forms.MouseEventHandler(this.flDays_MouseMove);
            // 
            // DashboardForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.flDays);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DashboardForm";
            this.Text = "지브리지 대시보드";
            this.Activated += new System.EventHandler(this.DashboardForm_Enter);
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DashboardForm_KeyDown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSaturday;
        private System.Windows.Forms.Label lblFriday;
        private System.Windows.Forms.Label lblThursday;
        private System.Windows.Forms.Label lblWendsDay;
        private System.Windows.Forms.Label lblTuesDay;
        private System.Windows.Forms.Label lblMonday;
        private System.Windows.Forms.Label lblSunday;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button nextMonth;
        private System.Windows.Forms.Button prevMonth;
        private System.Windows.Forms.Label lblYearAndMonth;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flDays;
        private System.Windows.Forms.FlowLayoutPanel flIncome;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}