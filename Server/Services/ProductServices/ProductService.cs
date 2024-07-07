﻿using BlazorECommerse.Server.Services.ProductServices.Interfaces;

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
            var product = await _context.Products.FindAsync(productId);
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
                Data = await _context.Products.ToListAsync()
            };
            return response;
        }
    }
}
