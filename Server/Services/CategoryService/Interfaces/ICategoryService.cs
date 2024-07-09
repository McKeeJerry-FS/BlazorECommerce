namespace BlazorECommerse.Server.Services.CategoryService.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<Category>> GetCategory(string categoryId);
        Task<ServiceResponse<List<Category>>> GetCategories();
    }
}
