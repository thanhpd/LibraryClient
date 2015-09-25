using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibraryData.Models;
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
        BookModel cacheBookModel = new BookModel(new Book());
        public RadForm2()
        {
            InitializeComponent();                       

            listBookModels = (DataProvider.GetAllBooks(100, 0)).Select(book => new BookModel(book)).ToList();
            radGridView2.DataSource = listBookModels;

            radGridView2.TableElement.RowHeight = 80;
            radGridView2.MasterTemplate.AllowAddNewRow = false;
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
                cacheBookModel = bookModel;
                radPropertyGrid1.SelectedObject = bookModel;

                if (!String.IsNullOrWhiteSpace(bookModel.book_image) && String.IsNullOrWhiteSpace(bookModel.image_path))
                {
                    pictureBox2.Image = FormHelper.FetchLargeThumb(bookModel.book_image);
                }
                else if (!String.IsNullOrWhiteSpace(bookModel.image_path))
                {
                    pictureBox2.Image = FormHelper.FetchImage(bookModel.image_path, 250, 150);
                }
                else
                {
                    pictureBox2.Image = null;
                }
            }
            catch (Exception ex)
            {

            }
        }        

        private void RadForm2_Load(object sender, EventArgs e)
        {

        }

        private void radImageButtonElement1_Click(object sender, EventArgs e)
        {
            
        }        

        private void radPropertyGrid1_Editing(object sender, PropertyGridItemEditingEventArgs e)
        {
            radButton1.Enabled = true;
            radButton2.Enabled = true;
        }

        private void radPropertyGrid1_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            var bookModel = (BookModel) radPropertyGrid1.SelectedObject;
        }



            
    }
}
