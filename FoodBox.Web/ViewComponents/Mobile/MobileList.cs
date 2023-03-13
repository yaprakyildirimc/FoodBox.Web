using Microsoft.AspNetCore.Mvc;

namespace FoodBox.Web.ViewComponents.Mobile
{
	public class MobileList: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
