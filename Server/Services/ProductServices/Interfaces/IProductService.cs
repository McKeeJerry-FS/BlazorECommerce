﻿namespace BlazorECommerse.Server.Services.ProductServices.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<List<Product>>> SearchProducts(string searchTerm);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchTerm);
    }
}
