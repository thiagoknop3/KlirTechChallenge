using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klir.TechChallenge.Web.Api.Models
{
    public class ItemModel
    {
        [JsonProperty("product")]
        public ProductModel Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("promotionApplied")]
        public bool PromotionApplied { get; set; }
    }
}
