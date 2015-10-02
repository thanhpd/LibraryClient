using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LibraryData.Services;
using LibraryDesktop.Models;
using LibraryDesktop.Utils;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace LibraryDesktop
{
    public partial class CreateBookForm : Telerik.WinControls.UI.RadForm
    {
        private PostBookModel bookModel = new PostBookModel();
        private RadBrowseEditor openDialog = new RadBrowseEditor();
        public CreateBookForm()
        {
            InitializeComponent();
            radDataEntry1.DataSource = bookModel;
        }

        private void radDataEntry1_EditorInitializing(object sender, Telerik.WinControls.UI.EditorInitializingEventArgs e)
        {
            if (e.Property.Name == "image_path")
            {                
                openDialog.DialogType = BrowseEditorDialogType.OpenFileDialog;
                openDialog.ReadOnly = true;
                openDialog.ValueChanging += OpenDialogOnValueChanging;                

                e.Editor = openDialog;
            }
        }        

        private void OpenDialogOnValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.OldValue != null || e.NewValue != null)
            {
               e.Cancel = !File.Exists(e.NewValue.ToString());
               if (!e.Cancel)
               {
                   picturePanel1.BackgroundImage = FormHelper.FetchImage(e.NewValue.ToString(), 298, 182);
                   bookModel.image_path = e.NewValue.ToString();
               } 
            }
            
        }        

        private void radButton4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;  
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            //var result = DataProvider.AddNewBook(bookModel.book_name, bookModel.image_path, bookModel.book_description,
            //    bookModel.book_author, bookModel.book_publisher, bookModel.book_year);
            //if (result) DialogResult = DialogResult.OK;
            //else MessageBox.Show("Failed to add this object.");
        }

        private void radDataEntry1_BindingCreated(object sender, BindingCreatedEventArgs e)
        {
            if (e.DataMember == "image_path")
            {
                e.Binding.Parse += new ConvertEventHandler(Binding_Parse);
            }
        }

        void Binding_Parse(object sender, ConvertEventArgs e)
        {
            e.Value = openDialog.Value;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var result = DataProvider.AddNewBook(bookModel.book_name, bookModel.image_path, bookModel.book_description,
                bookModel.book_author, bookModel.book_publisher, bookModel.book_year);
            if (result) DialogResult = DialogResult.OK;
            else MessageBox.Show("Failed to add this object.");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
