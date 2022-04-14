using AutoMapper;
using FluentValidation;
using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.Interfaces;
using Klir.TechChallenge.Infra.CrossCutting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Klir.TechChallenge.Service.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IBaseRepository<ShoppingCart> _repository;
        private readonly IBaseService<Product> _productService;
        private readonly ILogger<ShoppingCartService> _logger;
        private readonly IMapper _mapper;

        public ShoppingCartService(IBaseRepository<ShoppingCart> repository,
            IBaseService<Product> productService,
            IMapper mapper,
            ILogger<ShoppingCartService> logger)
        {
            _repository = repository;
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        public TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<ShoppingCart>
        {
            throw new NotImplementedException();
        }

        public CommomResponse<ShoppingCart> AddItem(int productId, int quantity)
        {
            try
            {
                var product = _productService.GetById<Product>(productId);
                if (product == null)
                    return new CommomResponse<ShoppingCart>(FailureDetails.NotFound, false, $"Product with id {productId} not found");

                var shoppingCart = new ShoppingCart();
                var entities = _repository.Select();
                if (entities != null)
                    shoppingCart = entities.FirstOrDefault();

                _logger.LogTrace($"[ShoppingCartService.AddItem] Adding {quantity} to product {product.Name}");
                var item = shoppingCart.Items.FirstOrDefault(p => p.Product != null && p.Product.Id == productId);
                if (item != null)
                    item.Quantity += quantity;
                else
                {
                    shoppingCart.Items.Add(new Item
                    {
                        Id = shoppingCart.Items.Count + 1,
                        Product = product,
                        Quantity = quantity
                    });
                }
                RecalculateShoppingCart(shoppingCart);
                _repository.Upsert(shoppingCart);
                return new CommomResponse<ShoppingCart>(shoppingCart, true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ShoppingCartService.AddItem] Error: {ex.Message}");
                return new CommomResponse<ShoppingCart>(FailureDetails.Exception, false, "Error while adding items to the shopping cart.");
            }

        }
        public CommomResponse<ShoppingCart> RemoveItem(int productId, int quantity)
        {
            try
            {
                var product = _productService.GetById<Product>(productId);
                if (product == null)
                    return new CommomResponse<ShoppingCart>(FailureDetails.NotFound, false, $"Product with id {productId} not found");

                var shoppingCart = new ShoppingCart();
                var entities = _repository.Select();
                if (entities != null)
                    shoppingCart = entities.FirstOrDefault();

                var item = shoppingCart.Items.FirstOrDefault(p => p.Product != null && p.Product.Id == productId);
                if (item == null)
                    return new CommomResponse<ShoppingCart>(FailureDetails.NotFound, false, $"Theres no {product.Name} in the shopping cart");

                if (item.Quantity < quantity)
                    return new CommomResponse<ShoppingCart>(FailureDetails.ValidationError, false, $"Its not possible to remove {quantity} quantities of product {product.Name}");

                _logger.LogTrace($"[ShoppingCartService.RemoveItem] Removing {quantity} from product {product.Name}");
                if (item.Quantity == quantity)
                    shoppingCart.Items.Remove(item);
                else
                    item.Quantity -= quantity;

                RecalculateShoppingCart(shoppingCart);
                _repository.Upsert(shoppingCart);
                return new CommomResponse<ShoppingCart>(shoppingCart, true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ShoppingCartService.RemoveItem] Error: {ex.Message}");
                return new CommomResponse<ShoppingCart>(FailureDetails.Exception, false, "Error while adding items to the shopping cart.");
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShoppingCart> Get<ShoppingCart>() where ShoppingCart : class
        {
            var entity = _repository.Select();
            var outputModel = _mapper.Map<IList<ShoppingCart>>(entity);
            return outputModel;
        }

        public TOutputModel GetById<TOutputModel>(int id) where TOutputModel : class
        {
            throw new NotImplementedException();
        }

        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<ShoppingCart>
        {
            throw new NotImplementedException();
        }

        private void RecalculateShoppingCart(ShoppingCart shoppingCart)
        {
            foreach (var item in shoppingCart.Items)
            {
                item.Total = item.Product.Price * item.Quantity;
                if (item.Product.Promotion != null)
                    PromotionService.CalculatePromotion(item);
            }
        }
    }
}
