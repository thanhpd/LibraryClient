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
        public static Task<T> ExecuteReceive<T>(RestRequest request, string baseUrl) where T : new()
        {
            var client = new RestClient(baseUrl);
            var tcs = new TaskCompletionSource<T>();  
            client.ExecuteAsync<T>(request, response =>
            {
                try
                {
                    if (response.ErrorException != null)
                    {
                        const string message = "Error retrieving response.  Check inner details for more info.";
                        var twilioException = new ApplicationException(message, response.ErrorException);
                        throw twilioException;
                    }

                    tcs.SetResult(response.Data);
                }
                catch (ApplicationException e)
                {
                    Debug.WriteLine(e.Message);
                    tcs.SetResult(new T());
                }                
            });
            return tcs.Task;
        }

        public static byte[] ExecuteReceive(RestRequest request, string baseUrl)
        {
            var client = new RestClient(baseUrl);
            var response = client.Execute(request);

            try
            {
                if (response.ErrorException != null)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var twilioException = new ApplicationException(message, response.ErrorException);
                    throw twilioException;
                }
                return response.RawBytes;
            }
            catch (ApplicationException e)
            {
                Debug.WriteLine(e);
                return new byte[1];
            }
            
        }

        public static Task<T> ExecuteSend<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(UrlBuilder.BaseUrl);
            var tcs = new TaskCompletionSource<T>();            
            client.ExecuteAsync<T>(request, response =>
            {
                try
                {
                    if (response.ErrorException != null)
                    {
                        const string message = "Error retrieving response.  Check inner details for more info.";
                        var twilioException = new ApplicationException(message, response.ErrorException);
                        throw twilioException;
                    }
                    tcs.SetResult(response.Data);
                }
                catch (ApplicationException e)
                {
                    Debug.WriteLine(e.Message);
                    tcs.SetResult(new T());
                }                    
            });            
                        
            return tcs.Task;           
        }
        
    }
}
