using CategoryServices.Data;
using CategoryServices.Model;
using CategoryServices.Services;
using Microsoft.EntityFrameworkCore;

namespace CategoryServices.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext _context;

        public CategoryRepository(CategoryDbContext context)
        {
            _context = context;
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
