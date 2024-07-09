

namespace BlazorECommerse.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product>? Products { get; set; }

        public event Action ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            if (result is not null)
            {
                return result;
            }
            else
            {
               return new ServiceResponse<Product> { Success = false, Message = "Product not found." };
            }
            
        }

        public async Task GetProducts(string categoryUrl)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            Products = result?.Data;

            ProductsChanged.Invoke();
        }
    }
}
