﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RiskFirst.RestClient
{
    public static class RestRequestExtensions
    {
        private static HttpClient DefaultHttpClient = new HttpClient(); 

        /// <summary>
        /// Creates the bare HttpRequestMessage from the rest request
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <param name="method">The http method to use</param>
        /// <returns></returns>
        public static HttpRequestMessage CreateRequestMessage(this RestRequest request, HttpMethod method, HttpContent content = null)
        {
            var message = new HttpRequestMessage(method, request.Uri)
            {
                Content = content
            };
            foreach (var header in request.Headers)
                message.Headers.Add(header.Key, header.Value);
            return message;
        }

        /// <summary>
        ///     Execute a get request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> GetAsync(this RestRequest request, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var requestMessage = request.CreateRequestMessage(HttpMethod.Get);
                var client = httpClient ?? DefaultHttpClient;
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
                var requestMessage = request.CreateRequestMessage(HttpMethod.Head);
                var client = httpClient ?? DefaultHttpClient;
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
                var requestMessage = request.CreateRequestMessage(HttpMethod.Delete);
                var client = httpClient ?? DefaultHttpClient;
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);                
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute delete request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a delete request from the specified rest client with the body serialized as JSON
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> DeleteJsonAsync<T>(this RestRequest request, T body, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken), JsonSerializerSettings settings = null)
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body, settings), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var requestMessage = request.CreateRequestMessage(HttpMethod.Delete, content);
                var client = httpClient ?? DefaultHttpClient;
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute delete request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a post request from the specified rest client with the body serialized as JSON
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this RestRequest request, T body, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken), JsonSerializerSettings settings = null)
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body, settings), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var requestMessage = request.CreateRequestMessage(HttpMethod.Post, content);
                var client = httpClient ?? DefaultHttpClient;
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute post request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a put request from the specified rest client with the body serialized as JSON
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this RestRequest request, T body, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken), JsonSerializerSettings settings = null)
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body, settings), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var requestMessage = request.CreateRequestMessage(HttpMethod.Put, content);
                var client = httpClient ?? DefaultHttpClient;
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute put request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a patch request from the specified rest client with the body serialized as JSON
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PatchJsonAsync<T>(this RestRequest request, T body, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken), JsonSerializerSettings settings = null)
        {
            var content = new StringContent(
                    JsonConvert.SerializeObject(body, settings), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var requestMessage = request.CreateRequestMessage(new HttpMethod("PATCH"), content);
                var client = httpClient ?? DefaultHttpClient;
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute patch request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a post request from the specified rest client and body
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PostAsync(this RestRequest request, HttpContent content, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {

            try
            {
                var requestMessage = request.CreateRequestMessage(HttpMethod.Post, content);
                var client = httpClient ?? DefaultHttpClient;
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
        public static async Task<HttpResponseMessage> PutAsync(this RestRequest request, HttpContent content, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var requestMessage = request.CreateRequestMessage(HttpMethod.Put, content);
                var client = httpClient ?? DefaultHttpClient;
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
        public static async Task<HttpResponseMessage> PatchAsync(this RestRequest request, HttpContent content, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var requestMessage = request.CreateRequestMessage(new HttpMethod("PATCH"), content);
                var client = httpClient ?? DefaultHttpClient;
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute patch request to {request.Uri.AbsoluteUri}", ex);
            }
        }

        /// <summary>
        ///     Execute a delete request from the specified rest client
        /// </summary>
        /// <param name="request">The rest request</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this RestRequest request, HttpContent content, HttpClient httpClient = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var requestMessage = request.CreateRequestMessage(HttpMethod.Delete, content);
                var client = httpClient ?? DefaultHttpClient;
                return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RestClientException($"Failed to execute delete request to {request.Uri.AbsoluteUri}", ex);
            }
        }
    }
}
