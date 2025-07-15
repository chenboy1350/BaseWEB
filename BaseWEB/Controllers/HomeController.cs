using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseWEB.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return PartialView("~/Views/Partial/_Dashboard.cshtml");
        }

        [Authorize]
        public IActionResult Users()
        {
            // You can add your logic here to get users data
            return PartialView("~/Views/Partial/_Users.cshtml");
        }

        [Authorize]
        public IActionResult Reports()
        {
            // You can add your logic here to get reports data
            return PartialView("~/Views/Partial/_Reports.cshtml");
        }

        [Authorize]
        public IActionResult Settings()
        {
            // You can add your logic here to get settings data
            return PartialView("~/Views/Partial/_Settings.cshtml");
        }

        [Authorize]
        public IActionResult Products()
        {
            // You can add your logic here to get products data
            return PartialView("~/Views/Partial/_Products.cshtml");
        }

        [Authorize]
        public IActionResult Orders()
        {
            // You can add your logic here to get orders data
            return PartialView("~/Views/Partial/_Orders.cshtml");
        }
    }
}
