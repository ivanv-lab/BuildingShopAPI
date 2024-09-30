using BuildingShopAPI.Models;

namespace BuildingShopAPI.Repositories.Interfaces
{
    public interface IProductCategoryRepo
    {
        public Task<IEnumerable<ProductCategory>> GetAll();
        public Task<ProductCategory> GetById(long id);
        public Task Add(ProductCategory category);
        public Task Update(ProductCategory category);
        public Task<int> Count();
        public Task Delete(long id);
    }
}
