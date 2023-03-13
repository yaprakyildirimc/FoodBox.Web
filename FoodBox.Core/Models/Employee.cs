using Microsoft.AspNetCore.Identity;

namespace FoodBox.Core.Models
{
    public class Employee : IdentityUser
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
