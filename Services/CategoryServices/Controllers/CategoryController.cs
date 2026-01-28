using CategoryServices.Model;
using CategoryServices.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Kafka;

namespace CategoryServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IKafkaMessageBus<int, Category> _messageBus;

        public CategoryController(ICategoryRepository repository, IKafkaMessageBus<int, Category> messageBus)
        {
            _repository = repository;
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var result = await _repository.AddCategoryAsync(category);
            await _messageBus.PublishAsync(category.Id, category);
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
