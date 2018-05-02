using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RiskFirst.RestClient
{
    public class RestResponseException : Exception
    {
        public RestResponseException(HttpStatusCode statusCode, HttpRequestMessage requestMessage, string message,string body=null) 
            : base(string.Format($"[{requestMessage.Method}]{{0}}", 
                (string.IsNullOrEmpty(message) ? requestMessage.RequestUri?.ToString() : $"{requestMessage.RequestUri} {message}")))
        {
            Uri = requestMessage.RequestUri;
            StatusCode = statusCode;
            Body = body;
        }       

        public RestResponseException(HttpStatusCode statusCode, HttpRequestMessage requestMessage, string message, Exception inner)
            : base(string.Format($"[{requestMessage.Method}]{{0}}", 
                (string.IsNullOrEmpty(message) ? requestMessage.RequestUri?.ToString() : $"{requestMessage.RequestUri} {message}")), inner)
        {
            Uri = requestMessage.RequestUri;
            StatusCode = statusCode;
            Body = string.Empty;
        }

        public RestResponseException(HttpStatusCode statusCode, HttpRequestMessage requestMessage, string message, string body, Exception inner)
           : base(string.Format($"[{requestMessage.Method}]{{0}}", 
               (string.IsNullOrEmpty(message) ? requestMessage.RequestUri?.ToString() : $"{requestMessage.RequestUri} {message}")), inner)
        {
            Uri = requestMessage.RequestUri;
            StatusCode = statusCode;
            Body = body;
        }

        public Uri Uri { get; }

        public HttpStatusCode StatusCode { get; }
        public override string Message => string.IsNullOrEmpty(Body) ? base.Message :  $"{base.Message} {Body}";
        public string Body { get; }
    }
}
