using CategoryServices.Model;
using CategoryServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoryServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var result = await _repository.AddCategoryAsync(category);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repository.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
