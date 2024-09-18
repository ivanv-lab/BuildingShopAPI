using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildingShopAPI.Repositories.Implements
{
    public class ProductCategoryRepo : IProductCategoryRepo
    {
        private readonly BuildingShopDbContext _context;
        public ProductCategoryRepo(BuildingShopDbContext context)
        {
            _context = context;
        }
        public async Task Add(ProductCategory category)
        {
            await _context.ProductCategories
                .AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var category=await GetById(id);
            category.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _context.ProductCategories
                .Where(c => c.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<ProductCategory> GetById(long id)
        {
            return await _context.ProductCategories
                .Where(c=>c.IsDeleted==false 
                && c.Id==id)
                .FirstAsync();
        }

        public async Task Update(ProductCategory category)
        {
            _context.Entry(category).State=
                EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
