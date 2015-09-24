using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LibraryDesktop.Models;
using LibraryDesktop.Utils;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace LibraryDesktop
{
    public partial class CreateBookForm : Telerik.WinControls.UI.RadForm
    {
        public CreateBookForm()
        {
            InitializeComponent();
            radDataEntry1.DataSource = new PostBookModel();                        
        }

        private void radDataEntry1_EditorInitializing(object sender, Telerik.WinControls.UI.EditorInitializingEventArgs e)
        {
            if (e.Property.Name == "image_path")
            {
                var openDialog = new RadBrowseEditor();
                openDialog.DialogType = BrowseEditorDialogType.OpenFileDialog;
                openDialog.ReadOnly = true;
                openDialog.ValueChanging += OpenDialogOnValueChanging;                

                e.Editor = openDialog;
            }
        }        

        private void OpenDialogOnValueChanging(object sender, ValueChangingEventArgs e)
        {
            e.Cancel = !File.Exists(e.NewValue.ToString());
            if (!e.Cancel)
            {
                picturePanel1.BackgroundImage = FormHelper.FetchImage(e.NewValue.ToString(), 298, 182);
            }
        }        

        private void radButton4_Click(object sender, EventArgs e)
        {
            Close();            
        }
    }
}
