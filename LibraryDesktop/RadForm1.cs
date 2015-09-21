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

namespace LibraryDesktop
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        public static string IdHeader = "ID";
        public static string BookNameHeader = "Book Name";
        public static string AuthorHeader = "Author";
        public static string YearHeader = "Year";
        public static string PublisherHeader = "Publisher";
        public static string StatusHeader = "Status";
        public static string DescriptionHeader = "Description";
        public static string CreateDateHeader = "Create Time";
        public static string LastUpdateHeader = "Last Update";

        enum ColumnId
        {
            Id,
            BookImage,
            BookName,
            Author,
            Year,
            Publisher,
            Status,
            Description,
            CreateTime,
            LastUpdate
        }

        public RadForm1()
        {
            InitializeComponent();
        }

        private void RadForm1_Load(object sender, EventArgs e)
        {
            var books = DataProvider.GetAllBooks(100, 0);
            List<BookModel> listBookModels = books.Select(book => new BookModel(book)).ToList();
            radGridView1.DataSource = listBookModels;            
            
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }        

        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows.Count != 0)
            {
                FormHelper.SetTextForTextBox(radID, radGridView1, 0);
                FormHelper.SetTextForTextBox(radBookName, radGridView1, 2);
                FormHelper.SetTextForTextBox(radAuthor, radGridView1, 3);
                //FormHelper.SetTextForTextBox(radPublishYear, radGridView1, YearHeader);
                FormHelper.SetTextForTextBox(radPublisher, radGridView1, PublisherHeader);
                FormHelper.SetTextForTextBox(radDescription, radGridView1, DescriptionHeader);
            }
        }
    }
}
