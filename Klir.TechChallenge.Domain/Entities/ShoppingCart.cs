using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Klir.TechChallenge.Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; } = new List<Item>();

        [JsonProperty("total")]
        public decimal Total
        {
            get { return Items.Any() ? Items.Sum(i => i.Total) : default(decimal); }
        }
    }
}
