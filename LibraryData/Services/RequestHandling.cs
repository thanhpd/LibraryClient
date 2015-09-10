using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Utils;
using RestSharp;

namespace LibraryData.Services
{
    class RequestHandling
    {
        static IRestResponse GetAllBooksRequest()
        {
            var client = new RestClient(UrlBuilder.BaseUrl);
            var request = new RestRequest(UrlBuilder.GetAllBooksPath, Method.GET);

            IRestResponse result = null;

            client.ExecuteAsync(request, response =>
            {
                result = response;
            });

            return result;
        }
    }
}
