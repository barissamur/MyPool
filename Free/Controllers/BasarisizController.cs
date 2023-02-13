using Microsoft.AspNetCore.Mvc;

namespace Free.Controllers
{
    public class BasarisizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
