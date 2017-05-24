using System;
using System.Collections.Generic;
using System.Text;

namespace RiskFirst.RestClient
{
    public class RestClientException : Exception
    {
        public RestClientException() { }
        public RestClientException(string message) : base(message) { }
        public RestClientException(string message, Exception inner) : base(message, inner) { }

    }
}
