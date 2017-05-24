using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RiskFirst.RestClient
{
    public class RestResponseException : Exception
    {
        public RestResponseException(HttpStatusCode statusCode) { StatusCode = statusCode; }
        public RestResponseException(HttpStatusCode statusCode, string message) : base(message) { StatusCode = statusCode; }
        public RestResponseException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner) { StatusCode = statusCode; }

        public HttpStatusCode StatusCode { get; }
    }
}
