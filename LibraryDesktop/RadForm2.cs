using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        List<BookModel> listBookModels2 = new List<BookModel>();
        private Queue<Book> rawData;
        private List<Book> copyPartialQueue = new List<Book>();
        private bool op = true;
        BookModel cacheLastRow = new BookModel(new Book());
        BookModel cacheNewRow = new BookModel(new Book());
        public RadForm2()
        {
            InitializeComponent();
            var tmp = DataProvider.GetAllBooks(100, 0).OrderBy(b => b.id).ToList();
            rawData = new Queue<Book>(tmp);
            var copycat = new Queue<Book>(tmp);
            for (int i = 0; i < 10; i++)
            {
                copyPartialQueue.Add(copycat.Dequeue());   
            }   
            modelTransform();
            bindData();
            radGridView2.TableElement.RowHeight = 80;
            radGridView2.MasterTemplate.AllowAddNewRow = false;
            radGridView2.MasterTemplate.EnableSorting = true;
            radLabelElement1.Font = boldFont;
        }

        private void modelTransform()
        {                                        
            var listWorkers = new List<BackgroundWorker>();
            bool[] restart = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
                backgroundWorker.WorkerSupportsCancellation = true;
                backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
                backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;                                
                listWorkers.Add(backgroundWorker);
            }            
            while (rawData.Count > 0)
            {
                Application.DoEvents();
                foreach (var worker in listWorkers)
                {
                    if (!worker.IsBusy)
                    {
                        worker.RunWorkerAsync();
                    }
                    //else if (!worker.CancellationPending)
                    //{
                    //    worker.CancelAsync();
                    //}
                }
            }

            listBookModels2 = copyPartialQueue.Select(book => new BookModel(book)).ToList();
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (runWorkerCompletedEventArgs.Error != null)
            {
                Debug.WriteLine(runWorkerCompletedEventArgs.Error);
            }
            else
            {
                //Debug.WriteLine("Completed " + DateTime.Now);                   
            }            
        }

        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            //Debug.WriteLine("Progress " + progressChangedEventArgs.ProgressPercentage + " " + DateTime.Now);
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            BackgroundWorker worker = sender as BackgroundWorker;       
            if (rawData.Count > 0)
            {
                //Debug.WriteLine("Rawdata" + rawData.Count);
                var rawItem = rawData.Dequeue();
                var bookModel = new BookModel(rawItem);
                //Debug.WriteLine("bookModel " + bookModel.id);
                listBookModels.Add(bookModel);
                //Debug.WriteLine("Working " + DateTime.Now);
                doWorkEventArgs.Result = bookModel;
            }            
        }

        private void bindData()
        {
            var a = listBookModels2;
            radGridView2.DataSource = null;
            listBookModels = listBookModels.OrderBy(b => int.Parse(b.id)).ToList();
            radGridView2.DataSource = listBookModels;
            radButtonElement1.Enabled = false;
            radButtonElement2.Enabled = false;
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
                radButtonElement1.Enabled = true;
                radButtonElement2.Enabled = true;
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

            if (e.Row is GridViewDataRowInfo)
            {
                e.CellElement.ToolTipText = e.CellElement.Text;
                
            }
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            var createForm = new CreateBookForm();
            createForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = createForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                radLabelElement1.Text = "Book added!";
                radLabelElement1.ForeColor = Color.Green;
                var latestId = Convert.ToInt32(listBookModels.OrderByDescending(b => b.id).First().id);
                var newestItem = (DataProvider.GetAllBooks(1, latestId)).Select(book => new BookModel(book)).ToList();
                listBookModels.Add(newestItem[0]);
                bindData();
                createForm.Dispose();
            } else if (result == DialogResult.Cancel)
            {
                radLabelElement1.Text = "Operation Canceled!";
                radLabelElement1.ForeColor = Color.DarkRed;
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
                try
                {
                    var a = radGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    var b = DataProvider.DeleteBook(a);
                    if (b)
                    {
                        radLabelElement1.Text = "Deleted successfully";
                        radLabelElement1.ForeColor = Color.Green;
                        removeRow(a);
                        bindData();
                    }
                    else
                    {
                        radLabelElement1.Text = "Deleting failed";
                        radLabelElement1.ForeColor = Color.DarkRed;
                    }
                    confirmForm.Dispose();
                }
                catch (Exception)
                {
                    radLabelElement1.Text = "Deleting failed";
                    radLabelElement1.ForeColor = Color.DarkRed;
                    confirmForm.Dispose();
                }
                
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

        private void radToggleButtonElement1_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            radGridView2.EnableFiltering = !radGridView2.EnableFiltering;
        }

        private void radToggleButtonElement2_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            radGridView2.AllowSearchRow = !radGridView2.AllowSearchRow;
        }

        private void radToggleButtonElement3_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            radGridView2.EnableGrouping = !radGridView2.EnableGrouping;
            radGridView2.ShowGroupPanel = !radGridView2.ShowGroupPanel;
        }

        private void radMenuButtonItem2_Click(object sender, EventArgs e)
        {
            var dialog = new RadAboutBox1();
            dialog.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                dialog.Dispose();
            }
            dialog.Dispose();
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            LoadingForm dialog = new LoadingForm();
            rawData = new Queue<Book>(DataProvider.GetAllBooks(100, 0));
            listBookModels.Clear();
            
            Thread splashThread = new Thread(new ThreadStart(
                delegate
                {
                    dialog = new LoadingForm();
                    Application.Run(dialog);                    
                }
                ));

            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();

            modelTransform();
            bindData();
            splashThread.Abort();            
        }

        private void radGridView2_PageChanging(object sender, PageChangingEventArgs e)
        {
            radButtonElement1.Enabled = false;
            radButtonElement2.Enabled = false;
        }


    }
}
