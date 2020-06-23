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
    public partial class ResetUltronMessage : Form
    {
        public ResetUltronMessage()
        {
            InitializeComponent();
        }

        private void UltronResetOKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
