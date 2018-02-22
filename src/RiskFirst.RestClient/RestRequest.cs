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
        
        private RestRequest(Uri baseUri)
        {
            this.uriBuilder = new UriBuilder(baseUri);
        }

        /// <summary>
        /// Construct an instance of RestRequest from a Uri
        /// </summary>
        /// <param name="uri">The uri to base the request from</param>
        /// <returns>RestRequest</returns>
        public static RestRequest FromUri(Uri uri)
        {
            return new RestRequest(uri);
        }

        internal IReadOnlyDictionary<string, IEnumerable<string>> Headers
        {
            get { return new ReadOnlyDictionary<string, IEnumerable<string>>(this.headers); }
        }

        internal Uri Uri
        {
            get { return uriBuilder.Uri; }
        }

        /// <summary>
        ///  Adds a specified key/value to the request header
        /// </summary>
        /// <param name="key">Header key</param>
        /// <param name="value">Header value</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithHeader(string key, string value)
        {
            return WithHeader(key, new[] { value });
        }

        /// <summary>
        ///  Adds a specified key/list of values to the request header
        /// </summary>
        /// <param name="key">Header key</param>
        /// <param name="value">Header values</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithHeader(String key, IEnumerable<string> values)
        {
            this.headers.Add(key, values);
            return this;
        }

        /// <summary>
        ///  Adds a set of values to the request header
        /// </summary>
        /// <param name="values">Any object to add to the headers</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithHeaders(object values)
        {
            var props = values.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var prop in props)
                WithHeader(prop.Name, prop.GetValue(values, null)?.ToString());
            return this;
        }

        /// <summary>
        /// Adds parameter(s) to the header from a dictionary
        /// </summary>
        /// <param name="values">dictionary of key/value to add to the headers</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithHeaders(IDictionary<string, object> values)
        {
            foreach (var kv in values)
                WithHeader(kv.Key, kv.Value?.ToString());
            return this;
        }

        /// <summary>
        ///  Adds a bearer token to the request header
        /// </summary>
        /// <param name="token">The bearer token to add to the request</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithBearerToken(string token)
        {
            return WithHeader("Authorization", $"bearer {token}");
        }

        /// <summary>
        /// Adds a single path segment to the request Uri
        /// </summary>
        /// <param name="segment">The segment to add</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithPathSegment(string segment)
        {
            if(!string.IsNullOrEmpty(uriBuilder.Path) && !this.uriBuilder.Path.EndsWith("/"))
                this.uriBuilder.Path += "/";

            this.uriBuilder.Path += segment;

            return this;
        }

        /// <summary>
        /// Adds one or more path segments to the request Uri
        /// </summary>
        /// <param name="segments">The path segments to add</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithPathSegments(params object[] segments)
        {
            return WithPathSegments(segments.Select(s => s == null ? String.Empty : s.ToString()));
        }

        /// <summary>
        /// Adds an enumerable list of segments to the request Uri
        /// </summary>
        /// <param name="segments">The path segments to add</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithPathSegments(IEnumerable<string> segments)
        {
            if (!string.IsNullOrEmpty(uriBuilder.Path) && !this.uriBuilder.Path.EndsWith("/"))
                this.uriBuilder.Path += "/";

            this.uriBuilder.Path += String.Join("/", segments);
            return this;
        }

        ///// <summary>
        ///// Adds a single parameter to the query part of the request
        ///// </summary>
        ///// <param name="param">The parameter name</param>
        ///// <param name="value">The parameter value</param>
        ///// <returns>Current RestRequest instance</returns>
        //public RestRequest WithQueryParameter(string param, object value)
        //{
        //    this.uriBuilder.Query += String.IsNullOrEmpty(this.uriBuilder.Query)
        //        ? $"{param}={value}" : $"&{param}={value?.ToString()}";
        //    return this;
        //}

        /// <summary>
        /// Adds a single parameter with one or multiple values to the query part of the request
        /// </summary>
        /// <param name="param">The parameter name</param>
        /// <param name="value">The parameter value</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithQueryParameter(string param, params object[] values)
        {
            if (values != null && values.Length > 0)
            {
                var parameters = String.Join("&", values.Select(v => $"{param}={v?.ToString()}"));
                this.uriBuilder.Query = String.IsNullOrEmpty(this.uriBuilder.Query)
                    ? parameters
                    : this.uriBuilder.Query[0] == '?'
                        ? $"{this.uriBuilder.Query.Substring(1)}&{parameters}"
                        : $"{this.uriBuilder.Query}&{parameters}";
            }
            else
            {
                this.uriBuilder.Query = String.IsNullOrEmpty(this.uriBuilder.Query)
                        ? $"{param}="
                        : this.uriBuilder.Query[0] == '?'
                            ? $"{this.uriBuilder.Query.Substring(1)}&{param}="
                            : $"{this.uriBuilder.Query}&{param}=";
            }
            return this;
        }

        /// <summary>
        /// Adds parameter(s) to the request from any object
        /// </summary>
        /// <remarks>
        /// All public properties are added to the query
        /// </remarks>
        /// <param name="values">Any object to add to the parameters</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithQueryParameters(object values)
        {
            var props = values.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var prop in props)
                WithQueryParameter(prop.Name, prop.GetValue(values, null)?.ToString());
            return this;
        }

        /// <summary>
        /// Adds parameter(s) to the request from a dictionary
        /// </summary>
        /// <param name="values">dictionary of key/value to add to the query</param>
        /// <returns>Current RestRequest instance</returns>
        public RestRequest WithQueryParameters(IDictionary<string,object> values)
        {
            foreach (var kv in values)
                WithQueryParameter(kv.Key, kv.Value?.ToString());
            return this;
        }
    }
}
