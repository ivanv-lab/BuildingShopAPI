using BuildingShopAPI.DTO;
using BuildingShopAPI.Models;

namespace BuildingShopAPI.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDto> GetById(long id);
        public Task<IEnumerable<ProductDto>> GetAll();
        public Task<ProductDto> Create(ProductCreateDto product);
        public Task<ProductDto> Update(long id, ProductCreateDto product);
        public Task<IEnumerable<ProductDto>> GetByCategoryId(long categoryId);
        public Task<bool> Delete(long id);
    }
}
