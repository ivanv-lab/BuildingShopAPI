namespace BuildingShopAPI.DTO
{
    public class ProductCreateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public long CategoryId {  get; set; }
    }
}
