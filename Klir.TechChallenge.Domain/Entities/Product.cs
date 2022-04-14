using Klir.TechChallenge.Domain.enumerators;
using Newtonsoft.Json;

namespace Klir.TechChallenge.Domain.Entities
{
    public class Product : BaseEntity
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("promotion")]
        public Promotion? Promotion { get; set; }

    }

}
