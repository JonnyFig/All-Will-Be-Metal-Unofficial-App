using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace All_Will_Be_Metal
{
    public partial class PopUpMessage : Form
    {
        string m;
        bool type;

        public PopUpMessage(string message, bool isYesNo)
        {
            m = message;
            type = isYesNo;
            InitializeComponent();
        }

        private void PopUpMessage_Load(object sender, EventArgs e)
        {
            
            QuestionTextbox.Text = m;
            if (type == true)
            {
                OKButton.Visible = false;
            }
            else
            {
                YesButton.Visible = false;
                NoButton.Visible = false;
            }

        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void MouseOver_Handler(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.White;
            ((Button)sender).ForeColor = Color.Black;
        }

        private void MouseLeave_Handler(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Black;
            ((Button)sender).ForeColor = Color.White;
        }

        private void pMouseOver_Handler(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.White;
        }

        private void pMouseLeave_Handler(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

    }
}
