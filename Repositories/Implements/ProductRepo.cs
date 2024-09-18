using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildingShopAPI.Repositories.Implements
{
    public class ProductRepo : IProductRepo
    {
        private readonly BuildingShopDbContext _context;
        public ProductRepo(BuildingShopDbContext context)
        {
            _context = context;
        }
        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var product=await GetById(id);
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(long categoryId)
        {
            return await _context.Products
                .Where(p=>p.CategoryId==categoryId 
                && p.IsDeleted==false)
                .ToListAsync();
        }

        public async Task<Product> GetById(long id)
        {
            return await _context.Products
                .Where(p=>p.IsDeleted==false
                && p.Id==id)
                .FirstAsync();
        }

        public async Task Update(Product product)
        {
            _context.Entry(product).State= EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
