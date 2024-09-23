using BuildingShopAPI.Models;

namespace BuildingShopAPI.DTO
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public virtual CategoryDto? Category { get; set; }
    }
}
