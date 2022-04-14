using Newtonsoft.Json;

namespace Klir.TechChallenge.Web.Api.Models
{
    public class ItemCommand
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
