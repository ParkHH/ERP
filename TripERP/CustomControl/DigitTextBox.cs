using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripERP.Common;

namespace TripERP.CustomControl
{
    public partial class DigitTextBox : TextBox
    {
        public DigitTextBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            Utils.AcceptOnlyDigit(this, e);
            base.OnKeyPress(e);
        }
    }
}
