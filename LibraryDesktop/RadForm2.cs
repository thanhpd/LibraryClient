using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibraryData.Services;
using LibraryDesktop.Models;
using Telerik.WinControls;

namespace LibraryDesktop
{
    public partial class RadForm2 : Telerik.WinControls.UI.RadForm
    {
        public RadForm2()
        {
            InitializeComponent();
            var books = DataProvider.GetAllBooks(100, 0);
            List<BookModel> listBookModels = books.Select(book => new BookModel(book)).ToList();
            radGridView2.DataSource = listBookModels; 
        }

        private void radGridView1_LayoutLoaded(object sender, Telerik.WinControls.UI.LayoutLoadedEventArgs e)
        {
            
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }
    }
}
