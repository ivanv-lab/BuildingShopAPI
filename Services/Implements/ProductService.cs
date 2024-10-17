using BuildingShopAPI.DTO;
using BuildingShopAPI.Mappings;
using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using BuildingShopAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BuildingShopAPI.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _repo;
        private readonly IMapper<Product,
            ProductDto,ProductCreateDto> _map;
        private readonly IDistributedCache _cache;
        public ProductService(IProductRepo repo,
            IMapper<Product,
            ProductDto, ProductCreateDto> map, IDistributedCache cache)
        {
            _repo = repo;
            _map = map;
            _cache = cache;
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
            var produts=await _repo.GetAll();
            return _map.MapList(produts);
            //var allCachedProducts = await _cache.GetStringAsync
            //    ("allProducts");
            //if (!string.IsNullOrEmpty(allCachedProducts))
            //{
            //    var allProducts=JsonConvert
            //        .DeserializeObject<IEnumerable<Product>>
            //        (allCachedProducts);
            //    return _map.MapList(allProducts);
            //}
            //var dbProducts=await _repo.GetAll();
            //await _cache.SetStringAsync("allProducts",
            //    JsonConvert.SerializeObject(dbProducts),
            //    new DistributedCacheEntryOptions
            //    {
                    
            //        AbsoluteExpirationRelativeToNow =
            //        TimeSpan.FromMinutes(5)
            //    });
            //return _map.MapList(dbProducts);
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryId(long categoryId)
        {
            var products=await _repo.GetByCategoryId(categoryId);
            return _map.MapList(products);
        }

        public async Task<ProductDto> GetById(long id)
        {
            var product=await _repo.GetById(id);
            return _map.Map(product);
            //var cachedProduct = await _cache.GetStringAsync
            //     ($"product:{id}");
            //if (!string.IsNullOrEmpty(cachedProduct))
            //{
            //    var cacheProduct = JsonConvert
            //        .DeserializeObject<Product>(cachedProduct);
            //    return _map.Map(cacheProduct);
            //}
            //var dbProduct=await _repo.GetById(id);
            //await _cache.SetStringAsync($"product:{id}",
            //    JsonConvert.SerializeObject(dbProduct),
            //    new DistributedCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow =
            //        TimeSpan.FromMinutes(5)
            //    });
            //return _map.Map(dbProduct);
        }

        public async Task<ProductDto> Update(long id, 
            ProductCreateDto product)
        {
            var updateProduct = await _repo.GetById(id);
            updateProduct = _map.UpdateMap
                (updateProduct, product);
            await _repo.Update(updateProduct);
            updateProduct=await _repo.GetById(id);
            return _map.Map(updateProduct);
        }
        public async Task UpdateCache()
        {
            await _cache.RemoveAsync("allProducts");
        }
        public async Task<int> Count()
        {
            int count = await _repo.Count();
            return count;
        }
        public async Task<IEnumerable<ProductDto>> SortSearch
            (string? sortOrder,string? searchString)
        {
            var products = await GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                products = products.Where(p => p.Name.ToLower()
                .Contains(searchString)
                || p.Id.ToString().Contains(searchString)
                || p.Count.ToString().Contains(searchString)
                || p.Category.Name.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "Name_desc":
                    products=products.OrderByDescending(p => p.Name);
                    break;
                case "Count":
                    products=products.OrderBy(p => p.Count);
                    break;
                case "Count_desc":
                    products=products.OrderByDescending(p=>p.Count);
                    break;
                case "CatName":
                    products = products.OrderBy(p => p.Category.Name);
                    break;
                case "CatName_desc":
                    products = products.OrderByDescending(p => p.Category.Name);
                    break;
                case "Id":
                    products=products.OrderBy(p=>p.Id);
                    break;
                default:
                    products = products.OrderByDescending(p => p.Id);
                    break;
            }
            return products;
        }
    }
}
