
using BlazorECommerse.Shared;

namespace BlazorECommerse.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<Category>? Categories { get; set; }

        public async Task GetCategories()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            Categories = result?.Data;
        }

        public async Task<ServiceResponse<Category>> GetCategory(int categoryId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Category>>($"api/category/{categoryId}");
            if (result is not null)
            {
                return result;
            }
            else
            {
                return new ServiceResponse<Category> { Success = false, Message = "Category not found." };
            }
        }
    }
}
