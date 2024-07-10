using BlazorECommerse.Server.Services.ProductServices.Interfaces;

namespace BlazorECommerse.Server.Services.ProductServices
{
    
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(p => p.Variants) // loads Variants
				.ThenInclude(v => v.ProductType) // loads ProductTypes
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null)
            {
                response.Success = false;
                response.Message = "Product not found";
            }
            else
            {
                response.Data = product;
            }

            return response;
            

        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.Include(p => p.Variants).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(x => x.Category!.CategoryUrl.ToLower().Equals(categoryUrl.ToLower()))
					.Include(p => p.Variants)
					.ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchTerm)
        {
            var products = await FindProductsBySearchTerm(searchTerm);
            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if(product.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }
                if(product.Description is not null)
                {
                    var punctuation = product.Description.Where(Char.IsPunctuation)
                                             .Distinct()
                                             .ToArray();
                    var words = product.Description.Split()
                                             .Select(x => x.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }      
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchTerm)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await FindProductsBySearchTerm(searchTerm)
            };

            return response;
        }

        private async Task<List<Product>> FindProductsBySearchTerm(string searchTerm)
        {
            return await _context.Products
                                .Where(x => x.Title.ToLower().Contains(searchTerm.ToLower())
                                    || x.Description.ToLower().Contains(searchTerm.ToLower()))
                                .Include(p => p.Variants)
                                .ToListAsync();
        }
    }
}
