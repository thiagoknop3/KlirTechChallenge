using AutoMapper;
using FluentAssertions;
using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.Interfaces;
using Klir.TechChallenge.Infra.CrossCutting;
using Klir.TechChallenge.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Klir.TechChallenge.Tests
{
    public class ShoppingCartUnitTests
    {
        private readonly Mock<IBaseRepository<ShoppingCart>> _mockRepository;
        private readonly Mock<IBaseService<Product>> _mockProductService;
        private readonly Mock<ILogger<ShoppingCartService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ShoppingCartService _service;

        public ShoppingCartUnitTests()
        {
            _mockLogger = new Mock<ILogger<ShoppingCartService>>();
            _mockMapper = new Mock<IMapper>();
            _mockProductService = new Mock<IBaseService<Product>>();
            _mockRepository = new Mock<IBaseRepository<ShoppingCart>>();
            _service = new ShoppingCartService(_mockRepository.Object,
                _mockProductService.Object,
                _mockMapper.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void AddItemn_When_Product_Not_found()
        {
            _mockProductService.Setup(p => p.GetById<Product>(It.IsAny<int>()))
                .Returns<Product>(null);

            var result = _service.AddItem(1, 1);

            result.FailureDetails.Should().Be(FailureDetails.NotFound);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void AddItemn_Success()
        {
            _mockProductService.Setup(p => p.GetById<Product>(It.IsAny<int>()))
                .Returns(GetNewProduct());

            var result = _service.AddItem(1, 1);

            result.FailureDetails.Should().Be(null);
            result.Success.Should().BeTrue();
        }

        [Fact]
        public void RemoveItemn_When_Product_Not_found()
        {
            _mockProductService.Setup(p => p.GetById<Product>(It.IsAny<int>()))
                .Returns<Product>(null);

            var result = _service.RemoveItem(1, 1);

            result.FailureDetails.Should().Be(FailureDetails.NotFound);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void RemoveItemn_When_Item_Not_found()
        {
            _mockProductService.Setup(p => p.GetById<Product>(It.IsAny<int>()))
                .Returns(GetNewProduct());

            var result = _service.RemoveItem(1, 1);

            result.FailureDetails.Should().Be(FailureDetails.NotFound);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void RemoveItemn_When_Quantity_Is_Lower()
        {
            _mockProductService.Setup(p => p.GetById<Product>(It.IsAny<int>()))
                .Returns(GetNewProduct());
            _mockRepository.Setup(p => p.Select())
                .Returns(new List<ShoppingCart> { GetNewShoppingCart() });
            
            var result = _service.RemoveItem(1, 2);

            result.FailureDetails.Should().Be(FailureDetails.ValidationError);
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void RemoveItemn_When_Quantity_Success()
        {
            _mockProductService.Setup(p => p.GetById<Product>(It.IsAny<int>()))
                .Returns(GetNewProduct());
            _mockRepository.Setup(p => p.Select())
                .Returns(new List<ShoppingCart> { GetNewShoppingCart() });

            var result = _service.RemoveItem(1, 1);

            result.FailureDetails.Should().Be(null);
            result.Success.Should().BeTrue();
        }

        private Product GetNewProduct()
            => new Product { Id = 1, Name = "Name 1", Price = 10 };

        private ShoppingCart GetNewShoppingCart()
            => new ShoppingCart
            {
                Id = 1,
                Items = new List<Item>
                {
                    new Item
                    {
                        Id=1,
                        Product = GetNewProduct(),
                        Quantity=1,
                        Total=10
                    }
                }
            };

    }
}
