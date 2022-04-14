using Klir.TechChallenge.Domain.enumerators;
using Newtonsoft.Json;

namespace Klir.TechChallenge.Web.Api.Models
{
    public class ProductModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("promotion")]
        public Promotion? Promotion { get; set; }
    }
}
