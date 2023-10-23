using e_Commerce_Grocery.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace e_Commerce_Grocery.Services
{
    public class BaseApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected JsonSerializerOptions DefaultSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        protected HttpClient HttpClient => _httpClientFactory.CreateClient(AppConstants.httpClientName);

        protected TData Deserialize<TData>(string jsonData) =>
            JsonSerializer.Deserialize<TData>(jsonData, DefaultSerializerOptions);

        protected async Task<TData> HandleApiResponseAsync<TData>(HttpResponseMessage response, TData defaultValue)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    return Deserialize<TData>(content);
                }
            }
            return defaultValue;
        }
    }
}
