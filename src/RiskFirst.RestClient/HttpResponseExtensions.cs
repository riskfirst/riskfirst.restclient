using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiskFirst.RestClient
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ReceiveJsonAsync<T>(this Task<HttpResponseMessage> message)
        {
            var response = await message.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            throw new RestResponseException(response.StatusCode, response.ReasonPhrase);
        }

        public static async Task<HttpResponseMessage> ReceiveAsync(this Task<HttpResponseMessage> message)
        {
            return await message.ConfigureAwait(false); ;
        }
    }
}
