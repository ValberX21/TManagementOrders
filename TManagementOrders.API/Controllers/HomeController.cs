using Microsoft.AspNetCore.Mvc;

namespace TManagementOrders.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
