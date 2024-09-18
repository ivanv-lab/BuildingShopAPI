using Microsoft.EntityFrameworkCore;

namespace BuildingShopAPI.Models
{
    public class BuildingShopDbContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public BuildingShopDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString
                ("SQLServerConn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
