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
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] CategoryCreateDto request)
        {
            var updateCategory=await _categoryService
                .Update(request.Id, request);
            await _categoryService.UpdateCache();
            return Ok(updateCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Create
            ([FromBody] CategoryCreateDto request)
        {
            await _categoryService.Create(request);
            await _categoryService.UpdateCache();
            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete
            (long id)
         {
            bool res = await _categoryService.Delete(id);
            if (res)
            {
                await _categoryService.UpdateCache();
                return Ok();
            }
            return BadRequest();
        }
        //[HttpGet("sort")]
        //public async Task<IActionResult> Search([FromQuery]string? searchString,
        //    [FromQuery] string? sortOrder)
        //{
        //    const int pageSize = 10;
        //    var regions = await _categoryService.Search(searchString, sortOrder);
        //    return Ok(regions);
        //}
    }
}
