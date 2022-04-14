using Newtonsoft.Json;

namespace Klir.TechChallenge.Domain.Entities
{
    public class BaseEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
