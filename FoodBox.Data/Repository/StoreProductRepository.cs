using FoodBox.Core.Models;
using FoodBox.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBox.Data.Repository
{
    public class StoreProductRepository : Repository<StoreProduct>, IStoreProductRepository
    {
        public StoreProductRepository(FoodBoxDbContext context)
        : base(context)
        {

        }

    }
}
