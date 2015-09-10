﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Utils
{
    public class UrlBuilder
    {        
        public static string BaseUrl = "";
        public static string GetAllBooksPath = "";
        public static string FindBookPath = "";
        public static string AddBookPath = "";
        public static string EditBookPath = "";
        public static string DeleteBookPath = "";

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
