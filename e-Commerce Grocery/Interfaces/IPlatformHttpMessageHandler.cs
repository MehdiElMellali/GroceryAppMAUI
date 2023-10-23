using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce_Grocery.Interfaces
{
    public interface IPlatformHttpMessageHandler
    {
        HttpMessageHandler GetHttpMessageHandler();
    }
}
