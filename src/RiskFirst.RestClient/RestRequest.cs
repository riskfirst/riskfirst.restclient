using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace RiskFirst.RestClient
{
    public class RestRequest
    {
        private readonly UriBuilder uriBuilder;
        private Dictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>();
        private CancellationTokenSource cancelSource = new CancellationTokenSource();
        private RestRequest(Uri baseUri)
        {
            this.uriBuilder = new UriBuilder(baseUri);
        }
        public IReadOnlyDictionary<string, IEnumerable<string>> Headers
        {
            get { return new ReadOnlyDictionary<string, IEnumerable<string>>(this.headers); }
        }

        public Uri Uri
        {
            get { return uriBuilder.Uri; }
        }

        public CancellationToken CancellationToken
        {
            get { return this.cancelSource.Token; }
        }

        public RestRequest WithHeader(string key, string value)
        {
            return WithHeader(key, new[] { value });
        }

        public RestRequest WithHeader(String key, IEnumerable<string> values)
        {
            this.headers.Add(key, values);
            return this;
        }

        public RestRequest WithBearerToken(string token)
        {
            return WithHeader("Authorization", $"bearer {token}");
        }  
        
        public RestRequest WithCancellationTokenSource(CancellationTokenSource source)
        {
            this.cancelSource = source;
            return this;
        }

        public static RestRequest FromUri(Uri uri)
        {
            return new RestRequest(uri);
        }

        public RestRequest AppendPathSegment(string segment)
        {
            this.uriBuilder.Path += segment;
            return this;
        }
        public RestRequest AppendPathSegments(params object[] segments)
        {
            AppendPathSegments(segments.Select(s => s == null ? String.Empty : s.ToString()));
            return this;
        }

        public RestRequest AppendPathSegments(IEnumerable<string> segments)
        {
            this.uriBuilder.Path += String.Join("/", segments);
            return this;
        }

        public RestRequest SetQueryParameter(string param, string value)
        {
            this.uriBuilder.Query += String.IsNullOrEmpty(this.uriBuilder.Query)
                ? $"{param}={value}" : $"&{param}={value}";
            return this;
        }

        public RestRequest SetQueryParameters(object values)
        {
            var props = values.GetType().GetProperties();
            foreach (var prop in props)
                SetQueryParameter(prop.Name, prop.GetValue(values, null)?.ToString());
            return this;
        }
    }
}
