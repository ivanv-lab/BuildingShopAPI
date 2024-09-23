using BuildingShopAPI.DTO;
using BuildingShopAPI.Models;

namespace BuildingShopAPI.Services.Interfaces
{
    public interface IProductCategoryService
    {
        public Task<CategoryDto> GetById(long id);
        public Task<IEnumerable<CategoryDto>> GetAll();
        public Task<CategoryDto> Create(CategoryCreateDto category);
        public Task<CategoryDto> Update(long id,
            CategoryCreateDto category);
        public Task<bool> Delete(long id);
    }
}
