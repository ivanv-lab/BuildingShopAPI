using BuildingShopAPI.DTO;
using BuildingShopAPI.Models;

namespace BuildingShopAPI.Mappings
{
    public class CategoryMap:IMapper<ProductCategory,
        CategoryDto,CategoryCreateDto>
    {
        public ProductCategory Map(CategoryDto dto)
        {
            return new ProductCategory
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public CategoryDto Map(ProductCategory model)
        {
            return new CategoryDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public ProductCategory Map(CategoryCreateDto dto)
        {
            return new ProductCategory
            {
                Name = dto.Name
            };
        }

        public IEnumerable<CategoryDto> MapList(
            IEnumerable<ProductCategory> models)
        {
            List<CategoryDto> result = new List<CategoryDto>();
            foreach (var model in models)
            {
                result.Add(Map(model));
            }
            return result;
        }

        public ProductCategory UpdateMap(ProductCategory model,
            CategoryCreateDto dto)
        {
            model.Name = dto.Name;
            return model;
        }
    }
}