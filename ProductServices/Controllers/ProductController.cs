using Microsoft.AspNetCore.Mvc;
using ProductServices.Model;
using ProductServices.Services;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var result = await _repository.AddProductAsync(product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repository.GetProductsAsync();
            return Ok(categories);
        }
    }
}
