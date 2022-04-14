using Klir.TechChallenge.Domain.enumerators;
using Newtonsoft.Json;

namespace Klir.TechChallenge.Domain.Entities
{
    public class Item : BaseEntity
    {
        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("promotionApplied")]
        public bool PromotionApplied { get; set; }
    }
}
