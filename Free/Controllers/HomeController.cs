using Free.Data;
using Free.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Free.Controllers
{
    public class HomeController : Controller
    {
        private readonly UygulamaDbContext _db;

        public HomeController(UygulamaDbContext db)
        {
            _db=db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}