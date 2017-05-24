using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiskFirst.RestClient
{
    public static class RestRequestExtensions
    {
        /// <summary>
        ///     Execute a get request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> GetAsync(this RestRequest request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    AddHeadersToClient(request.Headers, client);
                    return await client.GetAsync(request.Uri,request.CancellationToken);
                }
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
        public static async Task<HttpResponseMessage> HeadAsync(this RestRequest request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    AddHeadersToClient(request.Headers, client);
                    var requestMsg = new HttpRequestMessage(HttpMethod.Head, request.Uri);
                    return await client.SendAsync(requestMsg, HttpCompletionOption.ResponseHeadersRead, request.CancellationToken);
                }
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute get request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a delete request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this RestRequest request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    AddHeadersToClient(request.Headers, client);
                    return await client.DeleteAsync(request.Uri, request.CancellationToken);
                }
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute get request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a put request from the specified rest client and body
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this RestRequest request, T body)
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    AddHeadersToClient(request.Headers, client);
                    return await client.PostAsync(request.Uri, content, request.CancellationToken);
                }
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
        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this RestRequest request, T body)
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    AddHeadersToClient(request.Headers, client);
                    return await client.PutAsync(request.Uri, content, request.CancellationToken);
                }
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute post request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a patch request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PatchJsonAsync<T>(this RestRequest request, T body)
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    AddHeadersToClient(request.Headers, client);
                    var requestMsg = new HttpRequestMessage(new HttpMethod("PATCH"), request.Uri)
                    {
                        Content = content
                    };
                    return await client.SendAsync(requestMsg, request.CancellationToken);
                }
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute post request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        private static void AddHeadersToClient(IReadOnlyDictionary<string, IEnumerable<string>> headers, HttpClient client)
        {
            foreach (var header in headers)
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }
}
