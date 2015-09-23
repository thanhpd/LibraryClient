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
using Telerik.WinControls.UI;

namespace LibraryDesktop
{
    public partial class RadForm2 : Telerik.WinControls.UI.RadForm
    {
        Font boldFont = new Font(SystemFonts.DialogFont, FontStyle.Bold);
        public RadForm2()
        {
            InitializeComponent();
            var books = DataProvider.GetAllBooks(100, 0);
            List<BookModel> listBookModels = books.Select(book => new BookModel(book)).ToList();
            radGridView2.DataSource = listBookModels;
            radGridView2.TableElement.RowHeight = 80;
            radGridView2.MasterTemplate.AllowAddNewRow = false;
        }

        private void radGridView1_LayoutLoaded(object sender, Telerik.WinControls.UI.LayoutLoadedEventArgs e)
        {
            
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void MasterTemplate_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                e.CellElement.Font = boldFont;
            }
        }
    }
}
