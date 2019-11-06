using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TripERP.Common
{
    public partial class PopUpResultMessage : Form
    {
        public string _userMessage { get; set; }

        public PopUpResultMessage()
        {
            InitializeComponent();
        }

        private void PopUpResultMessage_Load(object sender, EventArgs e)
        {
            userMessageRichTextBox.Refresh();
        }

        public void setUserMessage(string userMessage)
        {
            userMessageRichTextBox.Text = userMessage.Trim();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
