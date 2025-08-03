using Microsoft.AspNetCore.Mvc;

namespace TManagementOrders.API.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GotoCreateOrderPage()
        {
            return View("CreateUpdateOrder");
        }
    }
}
