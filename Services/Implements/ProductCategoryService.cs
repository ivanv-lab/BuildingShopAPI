using BuildingShopAPI.DTO;
using BuildingShopAPI.Mappings;
using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using BuildingShopAPI.Services.Interfaces;

namespace BuildingShopAPI.Services.Implements
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepo _repo;
        private readonly IMapper<ProductCategory,
            CategoryDto,CategoryCreateDto> _map;
        public ProductCategoryService(IProductCategoryRepo repo,
            IMapper<ProductCategory,
            CategoryDto, CategoryCreateDto> map)
        {
            _repo = repo;
            _map = map;
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
            var categories=await _repo.GetAll();
            return _map.MapList(categories);
        }

        public async Task<CategoryDto> GetById(long id)
        {
            var category=await _repo.GetById(id);
            return _map.Map(category);
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
    }
}
