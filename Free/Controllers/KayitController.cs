using Free.Data;
using Free.Models;
using Microsoft.AspNetCore.Mvc;

namespace Free.Controllers
{
    public class KayitController : Controller
    {
        private readonly UygulamaDbContext _db;

        public KayitController(UygulamaDbContext db)
        {
            _db=db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(KullaniciViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Kullanici kullanici = new Kullanici();
                kullanici.KullaniciAdi = vm.KullaniciAdi;
                kullanici.Sifre= vm.Sifre;

                _db.Kullanicilar.Add(kullanici);
                _db.SaveChanges();


                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
