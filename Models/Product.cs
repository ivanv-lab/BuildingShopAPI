﻿using Newtonsoft.Json;

namespace BuildingShopAPI.Models
{
    public class Product
    {
        public long Id { get; set; }    
        public string Name { get; set; }
        public long CategoryId {  get; set; }
        public int Count {  get; set; }
        public bool IsDeleted { get; set; } = false;
        [JsonProperty]
        public virtual ProductCategory? Category { get; set; }
    }
}
