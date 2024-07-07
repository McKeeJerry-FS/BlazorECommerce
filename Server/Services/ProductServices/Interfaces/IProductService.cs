namespace BlazorECommerse.Server.Services.ProductServices.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
    }
}
