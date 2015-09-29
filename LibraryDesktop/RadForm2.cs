using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        BookModel cacheLastRow = new BookModel(new Book());
        BookModel cacheNewRow = new BookModel(new Book());
        private BookModel newBookModel = new BookModel(new Book());
        public RadForm2()
        {
            InitializeComponent();
            //Thread.Sleep(10000);
            listBookModels = (DataProvider.GetAllBooks(100, 0)).Select(book => new BookModel(book)).Reverse().ToList();
            bindData();
            radGridView2.TableElement.RowHeight = 80;
            radGridView2.MasterTemplate.AllowAddNewRow = false;
            radGridView2.MasterTemplate.EnableSorting = true;
        }

        private void bindData()
        {
            radGridView2.DataSource = null;
            radGridView2.DataSource = listBookModels;
        }

        private void updateRow(BookModel model)
        {
            BookModel bookModel = listBookModels.FirstOrDefault(m => m.id == model.id);
            bookModel = model;            
        }

        private void removeRow(string id)
        {
            BookModel bookModel = listBookModels.FirstOrDefault(m => m.id == id);
            listBookModels.Remove(bookModel);

        }

        private void addRow(BookModel model)
        {
            listBookModels.Add(model);
        }

        private BookModel rowCaching(string id)
        {            
            BookModel bookModel = listBookModels.Where(b => b.id == id).ToList().FirstOrDefault();
            return bookModel;
        }        

        private void radGridView2_CurrentRowChanging(object sender, CurrentRowChangingEventArgs e)
        {
            try
            {                
                cacheLastRow = rowCaching(e.CurrentRow.Cells[0].Value.ToString());
                cacheNewRow = rowCaching(e.NewRow.Cells[0].Value.ToString());
                radPropertyGrid1.SelectedObject = cacheNewRow;

                if (!String.IsNullOrWhiteSpace(cacheNewRow.book_image))
                {
                    pictureBox2.Image = FormHelper.FetchLargeThumb(cacheNewRow.book_image);
                }
                //else if (!String.IsNullOrWhiteSpace(bookModel.image_path))
                //{
                //    pictureBox2.Image = FormHelper.FetchImage(bookModel.image_path, 250, 150);
                //}
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
            var updateResult = DataProvider.EditBook(Convert.ToInt32(bookModel.id), bookModel.book_name, bookModel.image_url, bookModel.book_description, bookModel.book_description, bookModel.book_publisher, bookModel.book_year);
            if (updateResult)
            {
                updateRow(bookModel);                
                radGridView2.DataSource = null;
                radGridView2.DataSource = listBookModels;
                //bindData();
            }
            else
            {
                bookModel = cacheLastRow;
            }
        }

        private void radPropertyGrid1_Edited(object sender, PropertyGridItemEditedEventArgs e)
        {
            //try
            //{
            //    newBookModel = (BookModel)radPropertyGrid1.SelectedObject;
            //    newBookModel.image_url = e.Editor.Value.ToString();
            //    pictureBox2.Image = FormHelper.FetchImage(cacheNewRow.NewBookImage, 250, 150);
            //}
            //catch (Exception)
            //{

            //}                                   
        }

        private void radGridView2_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                e.CellElement.Font = boldFont;
            }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            var createForm = new CreateBookForm();
            createForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = createForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var latestId = Convert.ToInt32(listBookModels.OrderByDescending(b => b.id).First().id);
                var newestItem = (DataProvider.GetAllBooks(1, latestId)).Select(book => new BookModel(book)).ToList();
                listBookModels.Add(newestItem[0]);
                bindData();
                createForm.Dispose();
            } else if (result == DialogResult.Cancel)
            {
                createForm.Dispose();
            }
        }

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            var confirmForm = new ConfirmDeleteBox();
            confirmForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = confirmForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var a = radGridView2.SelectedRows[0].Cells[0].Value.ToString();
                var b = DataProvider.DeleteBook(a);
                if (b)
                {
                    MessageBox.Show("Deleted Successfully");
                    removeRow(a);
                    bindData();
                }
                else
                {
                    MessageBox.Show("Deleting failed");
                }
                confirmForm.Dispose();
            }
            else if (result == DialogResult.Cancel)
            {
                confirmForm.Dispose();
            }
        }

        private void radRibbonBar1_Click(object sender, EventArgs e)
        {            
            var button = (RadButtonElement) sender;
            var themeName = button.AccessibleName;
            ThemeResolutionService.ApplicationThemeName = themeName;
        }


    }
}
