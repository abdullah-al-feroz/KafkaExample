using CategoryServices.Model;

namespace CategoryServices.Services
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
