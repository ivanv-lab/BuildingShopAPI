using BuildingShopAPI.DTO;
using BuildingShopAPI.Mappings;
using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using BuildingShopAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BuildingShopAPI.Services.Implements
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepo _repo;
        private readonly IMapper<ProductCategory,
            CategoryDto,CategoryCreateDto> _map;
        private readonly IDistributedCache _cache;
        public ProductCategoryService(IProductCategoryRepo repo,
            IMapper<ProductCategory,
            CategoryDto, CategoryCreateDto> map, IDistributedCache
             cache)
        {
            _repo = repo;
            _map = map;
            _cache = cache;
        }

        public async Task<CategoryDto> Create(CategoryCreateDto categoryDto)
        {
            var category=_map.Map(categoryDto);
            await _repo.Add(category);
            return _map.Map(category);
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

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var allCachedCategories = await _cache.GetStringAsync
                ("allCategories");
            if (!string.IsNullOrEmpty(allCachedCategories))
            {
                var allCategories = JsonConvert
                    .DeserializeObject<IEnumerable<ProductCategory>>
                    (allCachedCategories);
                return _map.MapList(allCategories);
            }
            var dbCategories = await _repo.GetAll();
            await _cache.SetStringAsync("allCategories",
                JsonConvert.SerializeObject(dbCategories),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5)
                });
            return _map.MapList(dbCategories);
        }

        public async Task<CategoryDto> GetById(long id)
        {
            var cachedCategory = await _cache.GetStringAsync
                ($"productCategory:{id}");
            if (!string.IsNullOrEmpty(cachedCategory))
            {
               var cacheCategory=JsonConvert
                    .DeserializeObject<ProductCategory>(cachedCategory);
                return _map.Map(cacheCategory);
            }
            var dbCategory=await _repo.GetById(id);
            await _cache.SetStringAsync($"productCategory:{id}",
                JsonConvert.SerializeObject(dbCategory),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5)
                });
            return _map.Map(dbCategory);
        }

        public async Task<CategoryDto> Update(long id, CategoryCreateDto categoryDto)
        {
            var updateCategory = await _repo.GetById(id);
            updateCategory=_map.UpdateMap(updateCategory,categoryDto);
            await _repo.Update(updateCategory);
            return _map.Map(updateCategory);
        }
        public async Task<IEnumerable<CategoryDto>> Search
            (string? searchString,string? sortOrder)
        {
            var categories = await _repo.GetAll();

            if (!string.IsNullOrEmpty(searchString)
                || searchString==" ")
            {
                searchString = searchString.ToLower();
                categories=categories.Where(c=>c.Name.ToLower().Contains(searchString)
                || c.Id.ToString().Contains(searchString));
            }

            switch (sortOrder) {
                case "Name":
                    categories=categories.OrderBy(c=>c.Name); break;
                case "Name_desc":
                    categories=categories.OrderByDescending(c=>c.Name); break;
                case "Id":
                    categories=categories.OrderBy(c=> c.Id); break;
                default:
                    categories = categories.OrderByDescending(c => c.Id);
                    break;
            }
            return _map.MapList(categories);
        }
        public async Task UpdateCache()
        {
            await _cache.RemoveAsync("allCategories");
        }
        public async Task<IEnumerable<CategoryDto>> SortSearch
            (string? sortOrder, string? searchString)
        {
            var categories=await GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                categories = categories.Where(c => c.Name.ToLower()
                .Contains(searchString)
                || c.Id.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    categories=categories.OrderBy(c=>c.Name);
                    break;
                case "Name_desc":
                    categories = categories.OrderByDescending(c => c.Name);
                    break;
                case "Id":
                    categories = categories.OrderBy(c => c.Id);
                    break;
                default:
                    categories = categories.OrderByDescending(c => c.Id);
                    break;
            }
            return categories;
        }
        public async Task<int> Count()
        {
            int count = await _repo.Count();
            return count;
        }
    }
}
