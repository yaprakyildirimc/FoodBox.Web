using Microsoft.AspNetCore.Mvc;

namespace FoodBox.Web.ViewComponents.Footer
{
    public class FooterList:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
