using FluentAssertions;
using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.enumerators;
using Klir.TechChallenge.Service.Services;
using Xunit;

namespace KlirTechChallenge.Tests
{
    public class PromotionThreeForTenUnitTests
    {
        [Fact]
        public void PromotionThreeForTen_When_Quantity_Is_Less_Than_Three()
        {
            var item = new Item
            {
                Product = new Product { Id = 1, Name = "test", Price = 7, Promotion = Promotion.ThreeForTen },
                Quantity = 2,
                Total = 14,
                PromotionApplied =  true
            };

            PromotionService.CalculatePromotion(item);

            item.Quantity.Should().BeLessThan(3);
            item.PromotionApplied.Should().BeFalse();
            item.Total.Should().Be(14);
        }

        [Fact]
        public void PromotionThreeForTen_When_Quantity_Is_Multiple_Of_Three()
        {
            var item = new Item
            {
                Product = new Product { Id = 1, Name = "test", Price = 7, Promotion = Promotion.ThreeForTen },
                PromotionApplied = true,
                Quantity = 3,
                Total = 10
            };

            PromotionService.CalculatePromotion(item);

            (item.Quantity % 3).Should().Be(0);
            item.PromotionApplied.Should().BeTrue();
            item.Total.Should().Be(10);
        }

        [Fact]
        public void PromotionThreeForTen_When_Quantity_Is_Not_Multiple_Of_Three()
        {
            var item = new Item
            {
                Product = new Product { Id = 1, Name = "test", Price = 7, Promotion = Promotion.ThreeForTen},
                PromotionApplied = true,                 Quantity = 5,
                Total = 24
            };

            PromotionService.CalculatePromotion(item);

            (item.Quantity % 2).Should().BeGreaterThan(0);
            item.PromotionApplied.Should().BeTrue();
            item.Total.Should().Be(24);
        }
    }
}
