using BuildingShopAPI.Models;

namespace BuildingShopAPI.Repositories.Interfaces
{
    public interface IProductRepo
    {
        public Task<Product> GetById(long id);
        public Task<IEnumerable<Product>> GetAll();
        public Task<IEnumerable<Product>> GetByCategoryId(long categoryId);
        public Task Add(Product product);
        public Task Update(Product product);
        public Task Delete(long id);
        public Task<int> Count();
    }
}
