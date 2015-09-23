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
using LibraryDesktop.Utils;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace LibraryDesktop
{
    public partial class RadForm2 : Telerik.WinControls.UI.RadForm
    {
        Font boldFont = new Font(SystemFonts.DialogFont, FontStyle.Bold);        
        List<BookModel> listBookModels = new List<BookModel>();
        public RadForm2()
        {
            InitializeComponent();            
            
            radGridView2.TableElement.RowHeight = 80;
            radGridView2.MasterTemplate.AllowAddNewRow = false;

            listBookModels = (DataProvider.GetAllBooks(100, 0)).Select(book => new BookModel(book)).ToList();
            radGridView2.DataSource = listBookModels;
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

        private void radGridView2_CurrentRowChanging(object sender, CurrentRowChangingEventArgs e)
        {
            try
            {
                var id = e.NewRow.Cells[0].Value.ToString();
                BookModel bookModel = listBookModels.Where(b => b.id == id).ToList().FirstOrDefault();
                radPropertyGrid1.SelectedObject = bookModel;
                pictureBox1.Image = FormHelper.FetchLargeThumb(bookModel.book_image);
            }
            catch (Exception ex)
            {

            }
        }        

        private void RadForm2_Load(object sender, EventArgs e)
        {

        }        
    }
}
