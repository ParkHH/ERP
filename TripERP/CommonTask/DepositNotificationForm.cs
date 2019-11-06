using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripERP.CommonTask
{
    public partial class DepositNotificationForm : Form
    {
        public enum alertTypeEnum { Info, Success };
        enum actionEnum { wait, start, close };

        private int x, y;
        Form frmAlert;

        actionEnum action = actionEnum.start;


        public DepositNotificationForm()
        {
            InitializeComponent();
        }

        private void DepositNotificationForm_Load(object sender, EventArgs e)
        {

        }

        public void setAlert(string msg, alertTypeEnum type)
        {
            string fname;

            for (int i = 1; i < 10; i++)
            {
                fname = "alert" + i.ToString();

                frmAlert = Application.OpenForms[fname];

                DepositNotificationForm f = (DepositNotificationForm)Application.OpenForms[fname];

                if (f == null)
                {
                    this.Name = fname;
                    x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    this.Location = new Point(x, y);
                    break;
                }
            }

            x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 5;

            switch (type)
            {
                case alertTypeEnum.Info:
                    this.GunaPictureBox1.Image = global::TripERP.Properties.Resources.check_white;
                    this.BackColor = Color.FromArgb(255, 248, 220);
                    break;
                case alertTypeEnum.Success:
                    this.GunaPictureBox1.Image = global::TripERP.Properties.Resources.check_white;
                    this.BackColor = Color.FromArgb(255, 179, 2);
                    break;
            }

            this.GunaLabel1.Text = msg;
            this.Show();
            this.timer1.Interval = 1;
            this.timer1.Start();

        }

        private void gunaPictureBox2_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 1;
            action = actionEnum.close;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case actionEnum.start:
                    this.timer1.Interval = 1;
                    this.Opacity += 0.1;
                    if (x < this.Location.X)
                        this.Left -= 1;
                    else
                    {
                        if (this.Opacity == 1)
                            action = actionEnum.wait;
                    }
                    break;
                case actionEnum.wait:
                    timer1.Interval = 86400000;
                    action = actionEnum.close;
                    break;
                case actionEnum.close:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;
                    this.Left -= 3;
                    if (this.Opacity == 0)
                        Close();
                    break;
            }
        }

    }
}
