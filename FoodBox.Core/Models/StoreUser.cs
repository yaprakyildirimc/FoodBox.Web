using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBox.Core.Models
{
    public class StoreUser : EntitiyBase
    {
        public Guid StoreId { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Store Store { get; set; }
    }
}
