
using FluentAssertions;
using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.enumerators;
using Klir.TechChallenge.Service.Services;
using Xunit;

namespace Klir.TechChallenge.Tests
{
    public class PromotionBuyOneGetOneUnitTests
    {

        [Fact]
        public void PromotionBuyOneGetOne_When_Quantity_Less_Than_Two()
        {
            var item = new Item
            {
                Product = new Product { Id = 1, Name = "test", Price = 2, Promotion = Promotion.BuyOneGetOne },
                Quantity = 1,
                Total = 2,
                PromotionApplied = true
            };
            PromotionService.CalculatePromotion(item);

            item.Quantity.Should().BeLessThan(2);
            item.PromotionApplied.Should().BeFalse();
            item.Total.Should().Be(2);
        }

        [Fact]
        public void PromotionBuyOneGetOne_When_Quantity_Multiple_Of_Two()
        {
            var item = new Item
            {
                Product = new Product { Id = 1, Name = "test", Price = 2, Promotion = Promotion.BuyOneGetOne },
                PromotionApplied = true,
                Quantity = 2,
                Total = 2
            };

            PromotionService.CalculatePromotion(item);

            (item.Quantity % 2).Should().Be(0);
            item.PromotionApplied.Should().BeTrue();
            item.Total.Should().Be(2);

        }


        [Fact]
        public void PromotionBuyOneGetOne_When_Quantity_NOT_Multiple_Of_Two()
        {
            var item = new Item
            {
                Product = new Product { Id = 1, Name = "test", Price = 2, Promotion = Promotion.BuyOneGetOne },
                PromotionApplied = true,
                Quantity = 3,
                Total = 4
            };

            PromotionService.CalculatePromotion(item);

            (item.Quantity % 2).Should().BeGreaterThan(0);
            item.PromotionApplied.Should().BeTrue() ;
            item.Total.Should().Be(4);
        }
    }
}
