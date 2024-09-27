using Bogus;
using BuildingShopAPI.Models;

namespace BuildingShopAPI
{
    public class DbInitializer
    {
        public static async void Initialize(BuildingShopDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            //var faker = new Faker<ProductCategory>()
            //    .RuleFor(pc => pc.Name, f => f.Vehicle.Manufacturer());

            //var pc = faker.Generate(50);
            //context.ProductCategories.AddRange(pc);
            //await context.SaveChangesAsync();
        }
    }
}
