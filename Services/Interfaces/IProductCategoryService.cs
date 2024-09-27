using BuildingShopAPI.DTO;
using BuildingShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuildingShopAPI.Services.Interfaces
{
    public interface IProductCategoryService
    {
        public Task<CategoryDto> GetById(long id);
        public Task<IEnumerable<CategoryDto>> GetAll();
        public Task<CategoryDto> Create(CategoryCreateDto category);
        public Task<CategoryDto> Update(long id,
            CategoryCreateDto category);
        public Task<IEnumerable<CategoryDto>> Search(string searchString,
            string? sortOrder);
        public Task<bool> Delete(long id);
        public Task UpdateCache();
    }
}
