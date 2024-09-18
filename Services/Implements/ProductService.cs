using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using BuildingShopAPI.Services.Interfaces;

namespace BuildingShopAPI.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _repo;
        public ProductService(IProductRepo repo) {
            _repo = repo; }
        public async Task<Product> Create(Product product)
        {
            await _repo.Add(product);
            return product;
        }

        public async Task<bool> Delete(long id)
        {
            var product=await _repo.GetById(id);
            if (product != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(long categoryId)
        {
            return await _repo.GetByCategoryId(categoryId);
        }

        public async Task<Product> GetById(long id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Product> Update(long id, Product product)
        {
            var updateProduct = await _repo.GetById(id);
            updateProduct.Name = product.Name;
            updateProduct.Count = product.Count;
            updateProduct.CategoryId=product.CategoryId;
            await _repo.Update(updateProduct);
            return updateProduct;
        }
    }
}
