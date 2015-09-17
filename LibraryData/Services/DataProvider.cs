using System;
using System.Collections.Generic;
using System.Linq;
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
            request.Resource = UrlBuilder.GetAllBooksPath;
            request.AddUrlSegment("limit", limit.ToString());
            request.AddUrlSegment("offset", offset.ToString());
            request.RootElement = "data";
            request.OnBeforeDeserialization = response => { response.ContentType = "application/json"; };

            var result = RequestHandling.Execute<List<Book>>(request);
            return result;
        }

        public static bool DeleteBook(string bookId)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = UrlBuilder.DeleteBookPath;
            request.AddParameter("book_id", bookId);
            request.OnBeforeDeserialization = response => { response.ContentType = "application/json"; };

            var result = RequestHandling.DeleteBook(request);
            return result;
        }
    }
}

