using BarkodDProject.Web.Controllers;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodBox.Web.Controllers
{
	public class ProductController : BaseProcess<Product, IProductService>
	{
		private readonly IProductService _productService;
		public ProductController(IProductService service) 
			: base(service)
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
