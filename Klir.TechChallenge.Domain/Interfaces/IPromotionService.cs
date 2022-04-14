using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.enumerators;

namespace Klir.TechChallenge.Domain.Interfaces
{
    public interface IPromotionService 
    {
        void CalculatePromotion(Item item);
    }
}
