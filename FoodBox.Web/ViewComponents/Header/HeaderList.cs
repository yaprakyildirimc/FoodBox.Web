using Microsoft.AspNetCore.Mvc;

namespace FoodBox.Web.ViewComponents.Header
{
	public class HeaderList: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
