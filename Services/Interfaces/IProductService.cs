using BuildingShopAPI.Models;

namespace BuildingShopAPI.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Product> GetById(long id);
        public Task<IEnumerable<Product>> GetAll();
        public Task<Product> Create(Product product);
        public Task<Product> Update(long id,Product product);
        public Task<IEnumerable<Product>> GetByCategoryId(long categoryId);
        public Task<bool> Delete(long id);
    }
}
