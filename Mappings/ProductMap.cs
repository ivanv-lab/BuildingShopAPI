using BuildingShopAPI.DTO;
using BuildingShopAPI.Models;

namespace BuildingShopAPI.Mappings
{
    public class ProductMap:IMapper<Product,
        ProductDto,ProductCreateDto>
    {
        private readonly IMapper<ProductCategory,
                CategoryDto, CategoryCreateDto> _map;
        public ProductMap(IMapper<ProductCategory,
                CategoryDto, CategoryCreateDto> map)
        {
            _map = map;
        }
        public Product Map(ProductDto dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Count = dto.Count,
                Category = _map.Map(dto.Category)
            };
        }

        public ProductDto Map(Product model)
        {
            return new ProductDto
            {
                Id = model.Id,
                Name = model.Name,
                Count = model.Count,
                Category = _map.Map(model.Category)
            };
        }

        public Product Map(ProductCreateDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Count = dto.Count,
                CategoryId = dto.CategoryId
            };
        }

        public IEnumerable<ProductDto> MapList
            (IEnumerable<Product> models)
        {
            List<ProductDto> result = new List<ProductDto>();
            foreach (var model in models)
            {
                result.Add(Map(model));
            }
            return result;
        }

        public Product UpdateMap(Product model,
            ProductCreateDto dto)
        {
            model.Name = dto.Name;
            model.Count = dto.Count;
            model.CategoryId = dto.CategoryId;
            return model;
        }
    }
}
