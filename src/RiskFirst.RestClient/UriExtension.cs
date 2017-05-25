using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace RiskFirst.RestClient
{
    public static class UriExtensions
    {
        public static RestRequest AsRestRequest(this Uri uri)
        {
            return RestRequest.FromUri(uri);
        }
    }
}
