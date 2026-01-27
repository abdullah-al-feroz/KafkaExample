using ProductServices.Model;

namespace ProductServices.Services
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product category);
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
