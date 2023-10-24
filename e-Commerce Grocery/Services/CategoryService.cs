using e_Commerce_Grocery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce_Grocery.Services
{
    public class CategoryService : BaseApiService
    {
        public CategoryService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
        private IEnumerable<Category>? _categories;

        public async ValueTask<IEnumerable<Category>> GetCategoriesAsync()
        {
            if (_categories is null)
            {
                var response = await HttpClient.GetAsync("/masters/categories");
                var categories = await HandleApiResponseAsync<IEnumerable<Category>>(response, null);

                if (categories is null)
                    return Enumerable.Empty<Category>();

                _categories = categories;
            }
            return _categories;
        }

        public async ValueTask<IEnumerable<Category>> GetMainCategoriesAsync() =>
            (await GetCategoriesAsync())
            .Where(c => c.ParentId == 0);

        public async Task<IEnumerable<Category>> GetSubbrSiblingCategories(short mainOrSiblingCategoryId)
        {
            var allCategories = await GetCategoriesAsync();
            var thisCategory = allCategories.First(c => c.Id == mainOrSiblingCategoryId);

            var mainCategoryId = thisCategory.IsMainCategory ? mainOrSiblingCategoryId : thisCategory.ParentId;
            return allCategories.Where(c => c.ParentId == mainCategoryId).ToList();
        }

    }
}
