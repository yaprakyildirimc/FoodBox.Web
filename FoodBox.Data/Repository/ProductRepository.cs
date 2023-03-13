using FoodBox.Core.Models;
using FoodBox.Core.Repositories;

namespace FoodBox.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(FoodBoxDbContext context)
        : base(context)
        {

        }

    }
}
