using FoodBox.Core.Models;
using FoodBox.Core.Repositories;

namespace FoodBox.Data.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(FoodBoxDbContext context)
        : base(context) { 
        
        }

    }
}
