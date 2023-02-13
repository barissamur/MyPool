using Free.Controllers;
using Free.Data;
using Free.Models;
using Microsoft.AspNetCore.Mvc;

namespace Free.Controllers
{
    public class GirisController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly IHttpContextAccessor _girenId;

        public GirisController(UygulamaDbContext db, IHttpContextAccessor girenId)
        {
            _db=db;
            _girenId=girenId;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(KullaniciViewModel vm)
        {
            Kullanici kullanici = _db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == vm.KullaniciAdi);

            if (kullanici == null)
            {
                return RedirectToAction("Index", "Basarisiz");
            }

            if (kullanici.KullaniciAdi == vm.KullaniciAdi && kullanici.Sifre == vm.Sifre)       
            {
                HttpContext.Session.SetInt32("userId", kullanici.Id);
                return RedirectToAction("Index", "Basarili", vm);
            }
            return View();
        }
    }
}
