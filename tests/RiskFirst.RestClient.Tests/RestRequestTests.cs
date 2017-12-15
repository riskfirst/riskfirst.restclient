using System;
using Xunit;
using RiskFirst.RestClient;
using System.Net.Http;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace RiskFirst.RestClient.Tests
{
    public class RestRequestTests
    {
        const string RootUri = "https://example.com";

        [Fact]
        public void GivenPathSegment_UriIsValid()
        {         
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api", req.RequestUri.ToString());            
        }

        [Fact]
        public void GivenTrailingSlashAndPathSegment_UriIsValid()
        {
            var req = new Uri(RootUri + "/").AsRestRequest()
                            .WithPathSegment("api")
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenRepeatedCallsToWithPathSegment_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .WithPathSegment("entity")
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api/entity", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenPathSegments_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegments("api","entity",123)
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api/entity/123", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenPathSegmentsAsEnumerable_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegments(new List<string> { "api", "entity", "123" })
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api/entity/123", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenQueryParameter_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .WithQueryParameter("foo","bar")
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api?foo=bar", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenMultipleQueryParameter_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .WithQueryParameter("foo", "bar", "zoo")
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api?foo=bar&foo=zoo", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenMultipleQueryParameterAsArray_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .WithQueryParameter("foo", new[] { "bar", "zoo" })
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api?foo=bar&foo=zoo", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenRepeatedCallsToWithQueryParameter_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                .WithQueryParameter("a", 1)
                .WithQueryParameter("b", 2)
                .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/?a=1&b=2", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenQueryParameterWithNullValue_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .WithQueryParameter("foo", null)
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api?foo=", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenQueryParametersAsObject_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .WithQueryParameters(new { foo = "bar" })
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api?foo=bar", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenQueryParametersAsDictionary_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                            .WithPathSegment("api")
                            .WithQueryParameters(new Dictionary<string, object>() { { "foo", "bar" } })
                            .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/api?foo=bar", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenMultipleQueryParametersAsDictionary_UriIsValid()
        {
            var req = new Uri(RootUri).AsRestRequest()
                .WithQueryParameters(new Dictionary<string, object>() { { "a", 1 }, { "b", 2 } })
                .CreateRequestMessage(HttpMethod.Get);

            Assert.Equal($"{RootUri}/?a=1&b=2", req.RequestUri.ToString());
        }

        [Fact]
        public void GivenHeader_RequestContainsHeader()
        {
            var req = new Uri(RootUri).AsRestRequest()
                           .WithPathSegment("api")
                           .WithHeader("Foo", "Bar")
                           .CreateRequestMessage(HttpMethod.Get);
            
            Assert.Collection(req.Headers, 
                e => Assert.True( e.Key == "Foo" && e.Value.Single() == "Bar"));
        }

        [Fact]
        public void GivenMultipleHeaderWithList_RequestContainsHeader()
        {
            var req = new Uri(RootUri).AsRestRequest()
                           .WithPathSegment("api")
                           .WithHeader("Foo", "Bar")
                           .WithHeader("Coll", new[] { "Val1","Val2" })
                           .CreateRequestMessage(HttpMethod.Get);

            Assert.Collection(req.Headers,
                e => Assert.True(e.Key == "Foo" && e.Value.Single() == "Bar"),
                e => Assert.Equal(2,e.Value.Count()));
        }
    }
}
