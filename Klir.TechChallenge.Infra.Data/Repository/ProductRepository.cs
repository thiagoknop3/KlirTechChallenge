using Klir.TechChallenge.Domain.Entities;

namespace Klir.TechChallenge.Infra.Data.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(string jsonFile) : base(jsonFile)
        {
        }
    }
}
