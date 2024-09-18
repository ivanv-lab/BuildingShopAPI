using BuildingShopAPI.Models;

namespace BuildingShopAPI.Services.Interfaces
{
    public interface IProductCategoryService
    {
        public Task<ProductCategory> GetById(long id);
        public Task<IEnumerable<ProductCategory>> GetAll();
        public Task<ProductCategory> Create(ProductCategory category);
        public Task<ProductCategory> Update(long id,
            ProductCategory category);
        public Task<bool> Delete(long id);
    }
}
