using BuildingShopAPI.DTO;
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
        public async Task<IActionResult> Update(
            long id,[FromBody] CategoryCreateDto request)
        {
            var updateCategory=await _categoryService
                .Update(id, request);
            return Ok(updateCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Create
            ([FromBody] CategoryCreateDto request)
        {
            await _categoryService.Create(request);
            return Ok(request);
        }

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
