using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
