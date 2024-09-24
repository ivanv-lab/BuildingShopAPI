using BuildingShopAPI.DTO;
using BuildingShopAPI.Models;
using BuildingShopAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuildingShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products=await _productService.GetAll();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var product=await _productService.GetById(id);
            return Ok(product);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategory(long id)
        {
            var products=await _productService.GetByCategoryId(id);
            return Ok(products);
        }
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody]ProductCreateDto request)
        {
            var updateProduct=await _productService
                .Update(request.Id, request);
            return Ok(updateProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Create
            ([FromBody] ProductCreateDto request)
        {
            var newProduct=await _productService.Create(request);
            return Ok(newProduct);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool res=await _productService.Delete(id);
            if(res)
                return Ok();
            return BadRequest();
        }
    }
}
