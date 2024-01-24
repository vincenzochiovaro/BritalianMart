using Newtonsoft.Json;

namespace BritalianMart.Models
{
    public class Entity
    {
        public string Id { get; set; }
        public DateTime Created { get; set;}
        public DateTime Modified { get; set; }
    }

    public class ProductModel : Entity
    {
        public string? Plu { get; set; }
        [JsonProperty("Description")]//TODO - to delete
        public string? Description { get; set; }
        [JsonProperty("Price")] //TODO - to delete
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
    }



}
