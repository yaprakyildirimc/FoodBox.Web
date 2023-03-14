using BarkodDProject.Web.Controllers;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodBox.Web.Controllers
{
    public class StoreController : BaseProcess<Store, IStoreService>
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService service)
            : base(service)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
