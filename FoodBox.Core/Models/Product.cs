using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FoodBox.Core.Models
{
    public class Product : EntitiyBase
    {
		public int ProductCode { get; set; }
		public string ProductName { get; set; }
        public string Brand { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Stock { get; set; }
	}
}
