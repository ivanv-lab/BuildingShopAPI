using BuildingShopAPI.DTO;
using BuildingShopAPI.Mappings;
using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using BuildingShopAPI.Services.Interfaces;

namespace BuildingShopAPI.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _repo;
        private readonly IMapper<Product,
            ProductDto,ProductCreateDto> _map;
        public ProductService(IProductRepo repo,
            IMapper<Product,
            ProductDto, ProductCreateDto> map)
        {
            _repo = repo;
            _map = map;
        }
        public async Task<ProductDto> Create(ProductCreateDto productDto)
        {
            var product=_map.Map(productDto);
            await _repo.Add(product);
            product=await _repo.GetById(product.Id);
            return _map.Map(product);
        }

        public async Task<bool> Delete(long id)
        {
            var product = await _repo.GetById(id);
            if (product != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products= await _repo.GetAll();
            return _map.MapList(products);
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryId(long categoryId)
        {
            var products=await _repo.GetByCategoryId(categoryId);
            return _map.MapList(products);
        }

        public async Task<ProductDto> GetById(long id)
        {
           var category=await _repo.GetById(id);
            return _map.Map(category);
        }

        public async Task<ProductDto> Update(long id, 
            ProductCreateDto product)
        {
            var updateProduct = await _repo.GetById(id);
            updateProduct = _map.UpdateMap
                (updateProduct, product);
            await _repo.Update(updateProduct);
            updateProduct=await _repo.GetById(id);
            //здесь категория не сохраняется, сброс после Update
            return _map.Map(updateProduct);
        }
    }
}
