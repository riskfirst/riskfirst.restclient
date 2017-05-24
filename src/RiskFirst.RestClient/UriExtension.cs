using System;
using System.Collections.Generic;
using System.Text;

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
