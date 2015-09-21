using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Utils
{
    public class UrlBuilder
    {
        public static string BaseUrl = "http://128.199.167.255/soa/";
        public static string GetAllBooksPath = "book/list";
        public static string FindBookPath = "book/search";
        public static string AddBookPath = "book/add";
        public static string EditBookPath = "book/update";
        public static string DeleteBookPath = "book/delete";

        public static string GetAllBooks = BuildUrl(GetAllBooksPath);
        public static string FindBook = BuildUrl(FindBookPath);
        public static string AddBook = BuildUrl(AddBookPath);
        public static string EditBook = BuildUrl(EditBookPath);
        public static string DeleteBook = BuildUrl(DeleteBookPath);

        private static string BuildUrl(string path)
        {
            return String.Format("{0}/{1}", BaseUrl, path);
        }
    }
}
