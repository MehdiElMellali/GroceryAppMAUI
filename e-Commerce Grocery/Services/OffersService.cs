using e_Commerce_Grocery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce_Grocery.Services
{
    public class OffersService : BaseApiService
    {
        public OffersService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
        public async Task<IEnumerable<Offer>> GetActiveOffersAsync()
        {
            var response = await HttpClient.GetAsync("/masters/offers");
            return await HandleApiResponseAsync(response, Enumerable.Empty<Offer>());
        }
    }
}
