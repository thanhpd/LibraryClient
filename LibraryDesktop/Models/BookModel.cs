using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Models;
using LibraryData.Services;
using LibraryDesktop.Utils;

namespace LibraryDesktop.Models
{
    public class BookModel : Book
    {
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
            status = book.status;

            if (!String.IsNullOrWhiteSpace(book_image))
            {
                BookImage = FormHelper.FetchImage(DataProvider.GetImage(book_image));
            }
        }
    }
}
