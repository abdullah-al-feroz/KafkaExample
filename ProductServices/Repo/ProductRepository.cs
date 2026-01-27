using Microsoft.EntityFrameworkCore;
using ProductServices.Data;
using ProductServices.Model;
using ProductServices.Services;

namespace ProductServices.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product category)
        {
            _context.Products.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
