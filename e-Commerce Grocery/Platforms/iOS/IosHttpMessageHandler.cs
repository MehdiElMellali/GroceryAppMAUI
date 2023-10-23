using e_Commerce_Grocery.Interfaces;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce_Grocery.Platforms.iOS
{
    public class IosHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() => new NSUrlSessionHandler()
        {
           TrustOverrideForUrl = (NSUrlSessionHandler sender,string url,SecTrust trust) => url.StartsWith("https://localhost")
        };
    }
}