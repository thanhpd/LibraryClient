using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Utils;
using RestSharp;

namespace LibraryData.Services
{
    public class RequestHandling
    {
        public static T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(UrlBuilder.BaseUrl);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return response.Data;
        }

        public static bool DeleteBook(RestRequest request)
        {
            var client = new RestClient(UrlBuilder.BaseUrl);
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            
            //TO-DO: Analyze response to define whether succeed or not
            return true;
        }
    }
}
