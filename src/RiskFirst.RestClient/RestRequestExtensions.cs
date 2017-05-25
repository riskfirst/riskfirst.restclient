using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RiskFirst.RestClient
{
    public static class RestRequestExtensions
    {
        private static HttpClient DefaultHttpClient = new HttpClient(); 
        /// <summary>
        ///     Execute a get request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> GetAsync(this RestRequest request, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var client = httpClient ?? DefaultHttpClient;
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, request.Uri);
                AddHeadersToRequest(request.Headers, requestMessage);
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute get request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a head request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> HeadAsync(this RestRequest request, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var client = httpClient ?? DefaultHttpClient;
                var requestMessage = new HttpRequestMessage(HttpMethod.Head, request.Uri);
                AddHeadersToRequest(request.Headers, requestMessage);
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);                
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute head request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a delete request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this RestRequest request, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var client = httpClient ?? DefaultHttpClient;
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, request.Uri);
                AddHeadersToRequest(request.Headers, requestMessage);
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);                
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute delete request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a post request from the specified rest client and body
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this RestRequest request, T body, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");
            
            try
            {
                var client = httpClient ?? DefaultHttpClient;
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, request.Uri)
                {
                    Content = content
                };
                AddHeadersToRequest(request.Headers, requestMessage);
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute post request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a put request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this RestRequest request, T body, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");
            
            try
            {
                var client = httpClient ?? DefaultHttpClient;
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, request.Uri)
                {
                    Content = content
                };
                AddHeadersToRequest(request.Headers, requestMessage);
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);               
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute put request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a patch request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PatchJsonAsync<T>(this RestRequest request, T body, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var client = httpClient ?? DefaultHttpClient;
                var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), request.Uri)
                {
                    Content = content
                };
                AddHeadersToRequest(request.Headers, requestMessage);
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);               
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute patch request to {request.Uri.AbsoluteUri}", ex);
            }
        }


        private static void AddHeadersToRequest(IReadOnlyDictionary<string, IEnumerable<string>> headers, HttpRequestMessage message)
        {
            foreach (var header in headers)
                message.Headers.Add(header.Key, header.Value);
        }
    }
}
