using BuildingShopAPI.Models;
using BuildingShopAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuildingShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController:ControllerBase
    {
        private readonly IProductCategoryService _categoryService;
        public ProductCategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories=await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var category=await _categoryService.GetById(id);
            return Ok(category);
        }
        public record UpdateCategoryRequest(long Id,string Name);
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateCategoryRequest request)
        {
            var category=new ProductCategory { Name = request.Name };
            var updateCategory=await _categoryService
                .Update(request.Id, category);
            return Ok(updateCategory);
        }
        public record CreateCategoryRequest(string Name);
        [HttpPost]
        public async Task<IActionResult> Create
            ([FromBody] CreateCategoryRequest request)
        {
            var newCatrgory=new ProductCategory {Name = request.Name};
            await _categoryService.Create(newCatrgory);
            return Ok(newCatrgory);
        }
        //public record DeleteCategoryRequest(long Id);
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete
            (long id)
        {
            bool res = await _categoryService.Delete(id);
            if(res)
                return Ok();
            return BadRequest();
        }
    }
}
