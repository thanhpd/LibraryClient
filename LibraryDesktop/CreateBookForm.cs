using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LibraryDesktop.Models;
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
        }
    }
}
