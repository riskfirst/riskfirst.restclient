using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RiskFirst.RestClient
{
    public class RestResponseException : Exception
    {
        public RestResponseException(HttpStatusCode statusCode, HttpRequestMessage requestMessage, string message) 
            : base($"[{requestMessage.Method}]{requestMessage.RequestUri} {message}")
        {
            Uri = requestMessage.RequestUri;
            StatusCode = statusCode;
        }

        public RestResponseException(HttpStatusCode statusCode, HttpRequestMessage requestMessage, string message, Exception inner)
            : base($"[{requestMessage.Method}]{requestMessage.RequestUri} {message}", inner)
        {
            Uri = requestMessage.RequestUri;
            StatusCode = statusCode;
        }

        public Uri Uri { get; }

        public HttpStatusCode StatusCode { get; }

    }
}
