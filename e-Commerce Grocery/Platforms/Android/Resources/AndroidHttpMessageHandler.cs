using e_Commerce_Grocery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace e_Commerce_Grocery.Platforms.Android.Resources
{
    public class AndroidHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() => new AndroidMessageHandler()
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, Chain, sslPolicyErrors) =>
            certificate.Issuer == "CN=localhost" || sslPolicyErrors == System.Net.Security.SslPolicyErrors.None
        };
    }
}
