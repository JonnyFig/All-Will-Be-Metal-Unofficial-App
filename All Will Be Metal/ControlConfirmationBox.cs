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
    public partial class ControlConfirmationBox : Form
    {
        public ControlConfirmationBox(int type) /// type 0 is for crisis team getting device, 1 for home base, 2 for ultron to kill citizen, 3 for ultron getting device
        {
            InitializeComponent();

            switch (type)
            {
                case 0:
                    TextLabel.Text = "Can The Crisis Team Capture this console?";
                    break;
                case 3:
                    TextLabel.Text = "Can Ultron capture this console?";
                    break;
                case 1:
                    TextLabel.Text = "Can the Crisis Team save a Citizen?";
                    break;
                case 2:
                    TextLabel.Text = "Can Ultron destroy a Citizen?";
                    break;
            }
        }

        private void CancelControlButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void YesControlButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void UltronControlButton_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
