using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibraryData.Services;

namespace LibraryDesktop
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        public RadForm1()
        {
            InitializeComponent();
        }

        private void RadForm1_Load(object sender, EventArgs e)
        {
            var result = DataProvider.GetAllBooks(100, 0);
            radGridView1.DataSource = result;            
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }
    }
}
