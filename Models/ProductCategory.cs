using Newtonsoft.Json;

namespace BuildingShopAPI.Models
{
    public class ProductCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted {  get; set; }=false;
        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; }
    }
}
