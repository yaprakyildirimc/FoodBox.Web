using FoodBox.Core.Models;
using FoodBox.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBox.Data.Repository
{
    public class StoreUserRepository : Repository<StoreUser>, IStoreUserRepository
    {
        public StoreUserRepository(FoodBoxDbContext context) 
            : base(context)
        {
        }
    }
}
