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
        public static Task<T> ExecuteReceive<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(UrlBuilder.BaseUrl);
            var tcs = new TaskCompletionSource<T>();  
            client.ExecuteAsync<T>(request, response =>
            {
                if (response.ErrorException != null)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var twilioException = new ApplicationException(message, response.ErrorException);
                    throw twilioException;
                }

                tcs.SetResult(response.Data);
            });
            return tcs.Task;
        }

        public static byte[] ExecuteReceive(RestRequest request)
        {
            var client = new RestClient(UrlBuilder.BaseUrl);
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return response.RawBytes;
        }

        public static Task<bool> ExecuteSend(RestRequest request)
        {
            var client = new RestClient(UrlBuilder.BaseUrl);
            var tcs = new TaskCompletionSource<bool>();
            client.ExecuteAsync(request, response =>
            {
                if (response.ErrorException != null)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var twilioException = new ApplicationException(message, response.ErrorException);
                    throw twilioException;
                }

                tcs.SetResult(true);
            });

            //TO-DO: Analyze response to define whether succeed or not
            return tcs.Task;

            //var response = client.Execute(request);
            //if (response.ErrorException != null)
            //{
            //    const string message = "Error retrieving response.  Check inner details for more info.";
            //    var twilioException = new ApplicationException(message, response.ErrorException);
            //    throw twilioException;
            //}

            //return true;
        }
        
    }
}
