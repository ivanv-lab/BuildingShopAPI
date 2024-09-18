using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using BuildingShopAPI.Services.Interfaces;

namespace BuildingShopAPI.Services.Implements
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepo _repo;
        public ProductCategoryService(IProductCategoryRepo repo)
        {
            _repo = repo;
        }

        public async Task<ProductCategory> Create(ProductCategory category)
        {
            await _repo.Add(category);
            return category;
        }

        public async Task<bool> Delete(long id)
        {
            var category=await _repo.GetById(id);
            if (category != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<ProductCategory> GetById(long id)
        {
            return await _repo.GetById(id);
        }

        public async Task<ProductCategory> Update(long id, ProductCategory category)
        {
            var updateCategory=await _repo.GetById(id);
            updateCategory.Name = category.Name;
            await _repo.Update(updateCategory);
            return updateCategory;
        }
    }
}
