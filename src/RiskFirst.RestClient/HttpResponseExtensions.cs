using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RiskFirst.RestClient
{
    public static class HttpResponseExtensions
    {

        /// <summary>
        /// Returns the content of the response HttpResponseMessage as the specified type
        /// </summary>
        public static async Task<T> ReceiveJsonAsync<T>(this Task<HttpResponseMessage> message, JsonSerializerSettings settings = null)
        {
            var response = await message.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), settings);
            }
            throw new RestResponseException(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Returns the content of the HttpResponseMessage as a string
        /// </summary>
        public static async Task<string> ReceiveStringAsync(this Task<HttpResponseMessage> message)
        {
            var response = await message.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new RestResponseException(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Returns the content of the HttpResponseMessage as a stream
        /// </summary>
        public static async Task<Stream> ReceiveStreamAsync(this Task<HttpResponseMessage> message)
        {
            var response = await message.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            throw new RestResponseException(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Returns the HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> ReceiveAsync(this Task<HttpResponseMessage> message)
        {
            return await message.ConfigureAwait(false);
        }
    }
}
