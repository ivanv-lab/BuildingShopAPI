
using BuildingShopAPI.DTO;
using BuildingShopAPI.Mappings;
using BuildingShopAPI.Models;
using BuildingShopAPI.Repositories.Implements;
using BuildingShopAPI.Repositories.Interfaces;
using BuildingShopAPI.Services.Implements;
using BuildingShopAPI.Services.Interfaces;

namespace BuildingShopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<BuildingShopDbContext>();

            builder.Services.AddTransient<IMapper<Product, ProductDto,
                ProductCreateDto>, ProductMap>();
            builder.Services.AddTransient<IMapper<ProductCategory,
                CategoryDto, CategoryCreateDto>, CategoryMap>();

            builder.Services.AddTransient<IProductCategoryRepo, ProductCategoryRepo>();
            builder.Services.AddTransient<IProductRepo, ProductRepo>();

            builder.Services.AddTransient<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddTransient<IProductService, ProductService>();


            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5180");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            var app = builder.Build();

            using var scope=app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider
                .GetRequiredService<BuildingShopDbContext>();
            dbContext.Database.EnsureCreatedAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseCors();
            app.UseAuthorization();
            app.Run();
        }
    }
}
