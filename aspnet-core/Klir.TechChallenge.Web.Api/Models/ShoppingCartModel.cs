using Newtonsoft.Json;
using System.Collections.Generic;

namespace Klir.TechChallenge.Web.Api.Models
{
    public class ShoppingCartModel
    {
        [JsonProperty("items")]
        public List<ItemCommand> Items { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }
    }
}
