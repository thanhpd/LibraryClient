using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Models;
using LibraryData.Services;
using LibraryDesktop.Utils;

namespace LibraryDesktop.Models
{
    public class BookModel
    {   
        [DisplayName("ID")]
        [Description("The identify number of the book.")]
        [ReadOnly(true)]
        public string id { get; set; }

        [DisplayName("Image Url")]
        [Description("The url path to the image.")]
        public string book_image { get; set; }

        [DisplayName("Book Name")]
        [Description("Current book's name.")]
        public string book_name { get; set; }

        [DisplayName("Author")]
        [Description("Author(s) of the book.")]
        public string book_author { get; set; }

        [DisplayName("Publisher")]
        [Description("Publisher of the book.")]
        public string book_publisher { get; set; }

        [DisplayName("Date Created")]
        [Description("Imported time of the book.")]
        [ReadOnly(true)]
        public DateTime created_at { get; set; }

        [DisplayName("Last Update")]
        [Description("The most recent time that this book had been edited.")]
        [ReadOnly(true)]
        public DateTime updated_at { get; set; }

        [DisplayName("Year")]
        [Description("Publish year of the book.")]
        public string book_year { get; set; }

        [DisplayName("Is Active")]
        [Description("Status of the book")]
        public bool status { get; set; }

        [DisplayName("Description")]
        [Description("The description of the book.")]
        public string book_description { get; set; }

        [DisplayName("Book Image")]
        [Description("The cover image of the book.")]
        [ReadOnly(true)]
        public Image BookImage { get; set; }

        public BookModel(Book book)
        {
            id = book.id;
            book_image = book.book_image;
            book_name = book.book_name;
            book_author = book.book_author;
            book_publisher = book.book_publisher;
            book_year = book.book_year;
            book_description = book.book_description;
            created_at = book.created_at;
            updated_at = book.updated_at;
            status = book.status == "1";

            if (!String.IsNullOrWhiteSpace(book_image))
            {
                BookImage = FormHelper.FetchImage(DataProvider.GetImage(book_image));
            }
        }
    }
}
