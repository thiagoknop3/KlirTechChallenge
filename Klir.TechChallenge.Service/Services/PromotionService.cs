using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.enumerators;
using Klir.TechChallenge.Domain.Interfaces;

namespace Klir.TechChallenge.Service.Services
{
    public static class PromotionService
    {
        public static void CalculatePromotion(Item item)
        {
            item.PromotionApplied = false;
            if (item.Product == null || !item.Product.Promotion.HasValue)
                return;
            switch (item.Product.Promotion)
            {
                case Promotion.BuyOneGetOne:
                    ProcessBuyOneGetOne(item);
                    break;
                case Promotion.ThreeForTen:
                    ProcessThreeForTen(item);
                    break;
                default:
                    break;
            }
        }

        private static void ProcessThreeForTen(Item item)
        {
            var itemsWithPromotion = item.Quantity / 3;
            var itemsWithNoPromotion = item.Quantity - (itemsWithPromotion * 3);
            var promotionValue = (decimal)(itemsWithPromotion * 10);
            var value = (decimal)(itemsWithNoPromotion * item.Product.Price);
            item.Total = value + promotionValue;
            item.PromotionApplied = itemsWithPromotion > 0;
        }

        private static void ProcessBuyOneGetOne(Item item)
        {
            var freeItems = item.Quantity / 2;
            var items = item.Quantity - freeItems;
            item.Total = (decimal)(items * item.Product.Price);
            item.PromotionApplied = freeItems > 0;
        }
    }
}
