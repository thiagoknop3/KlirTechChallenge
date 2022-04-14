using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Infra.CrossCutting;

namespace Klir.TechChallenge.Domain.Interfaces
{
    public interface IShoppingCartService : IBaseService<ShoppingCart>
    {
        CommomResponse<ShoppingCart> AddItem(int productId, int quantity);
        CommomResponse<ShoppingCart> RemoveItem(int productId, int quantity);
    }
}
