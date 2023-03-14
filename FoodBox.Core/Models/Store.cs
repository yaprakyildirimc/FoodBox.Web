using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBox.Core.Models
{
    public class Store : EntitiyBase
    {
        public string StoreName { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
		ICollection<Product> Products { get; set; }
	}
}
