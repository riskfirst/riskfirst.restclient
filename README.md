# Riskfirst.RestClient

![CI Build](https://riskfirst.visualstudio.com/_apis/public/build/definitions/2e2e46bb-1ab7-484e-8117-335c3855d65d/349/badge)

A [fluent inteface](https://en.wikipedia.org/wiki/Fluent_interface) used for making RESTful requests.

### Getting started

Install the package from [Nuget.org](https://www.nuget.org/packages/riskfirst.restclient)

```powershell
PM> Install-Package RiskFirst.RestClient
```

Make a simple RESTful `GET` request

```csharp
var myServiceUri = new Uri("https://myservice.com");
var response = myServiceUri.AsRestRequest()
                        .GetAsync()
                        .ReceiveAsync();
```

The above will return an instance of `HttpResponseMessage` which can be used to read the status code, or any other property. You can also receive a `JSON` response for your custom object.

```csharp
var myServiceUri = new Uri("https://myservice.com");
var response = myServiceUri.AsRestRequest()
                        .GetAsync()
                        .ReceiveJsonAsync<MyClass>();
```

The above will return an instance of `MyClass` deserialized from the response message, or throw a `RestResponseException` on a non-success response.

### A note on HttpClient

This library uses a static instance of `HttpClient` for all requests, as per the advice [in this advice](https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/) however clients of this library are free to provide their own instance of `HttpClient` if they wish. You should be aware of [this post regarding the use of a static HttpClient](http://byterot.blogspot.co.uk/2016/07/singleton-httpclient-dns.html)

This is an optional parameters on all request methods:

```csharp
var myServiceUri = new Uri("https://myservice.com");
var response = myServiceUri.AsRestRequest()
                        .GetAsync(myHttpClientInstance)
                        .ReceiveJsonAsync<MyClass>();
```
