using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Models;
using LibraryData.Utils;
using RestSharp;

namespace LibraryData.Services
{
    public class DataProvider
    {
        public static List<Book> GetAllBooks(int limit, int offset)
        {
            var request = new RestRequest();
            request.Resource = String.Format("{0}?limit={1}&offset={2}", UrlBuilder.GetAllBooksPath, 100, 0);
            request.RootElement = "data";
            request.OnBeforeDeserialization = response => { response.ContentType = "application/json"; };

            var result = RequestHandling.ExecuteReceive<List<Book>>(request).Result;
            return result;
        }

        public static bool DeleteBook(string bookId)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = UrlBuilder.DeleteBookPath;
            var body = String.Format("book_id={0}", bookId);
            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);      
            request.OnBeforeDeserialization = response => { response.ContentType = "application/json"; };
            
            var result = RequestHandling.ExecuteSend(request).Result;
            return result;
        }

        public static byte[] GetImage(string path)
        {
            var request = new RestRequest();
            request.Resource = path;

            var result = RequestHandling.ExecuteReceive(request);
            return result;
        }

        public static bool AddNewBook(string bookName, string bookImagePath, string bookDescription, string bookAuthor,
            string bookPublisher, string bookYear)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = UrlBuilder.AddBookPath;
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("book_name", bookName);
            request.AddFile("book_image", bookImagePath);
            request.AddParameter("book_description", bookDescription);
            request.AddParameter("book_author", bookAuthor);
            request.AddParameter("book_publisher", bookPublisher);
            request.AddParameter("book_year", bookYear);

            var result = RequestHandling.ExecuteSend(request).Result;
            return result;
        }
    }
}

