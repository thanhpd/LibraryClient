using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace LibraryDesktop
{
    public partial class ConfirmDeleteBox : Telerik.WinControls.UI.RadForm
    {
        public ConfirmDeleteBox()
        {
            InitializeComponent();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
