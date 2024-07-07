
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

        public async Task GetProducts()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
            Products = result?.Data;
        }
    }
}
