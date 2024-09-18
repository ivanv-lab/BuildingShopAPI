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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id,
            ProductCategory category)
        {
            var updateCategory=await _categoryService
                .Update(id, category);
            return Ok(updateCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategory category)
        {
            var newCatrgory=await _categoryService
                .Create(category);
            return Ok(newCatrgory);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool res = await _categoryService.Delete(id);
            if(res)
                return Ok();
            return BadRequest();
        }
    }
}
