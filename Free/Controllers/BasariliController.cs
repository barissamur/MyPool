using Free.Controllers;
using Free.Data;
using Free.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace Free.Controllers
{
    public class BasariliController : Controller
    {
        private readonly UygulamaDbContext _db;

        public BasariliController(UygulamaDbContext db)
        {
            _db=db;
        }

        public IActionResult Index()
        {
            var session = HttpContext.Session.GetInt32("userId");
            var _kullanici = _db.Kullanicilar.Find(session);
            if (_kullanici == null)
            {
                return View();
            }

            ViewData["kullaniciId"] = _kullanici.Id;
            TempData["KullaniciAdi"] = _kullanici.KullaniciAdi;
            IQueryable<Post> posts = _db.Posts;
            List<Post> postsList = posts.Include(x => x.Kullanicilar).OrderByDescending(x => x.OlusturmaZamani).ToList();

            return View(postsList);
        }

        public IActionResult Profil()
        {
            var session = HttpContext.Session.GetInt32("userId");
            var _kullanici = _db.Kullanicilar.Find(session);
            ViewData["kullaniciId"] = _kullanici.Id;

            if (_kullanici != null)
            {
                _kullanici.Posts = _db.Posts.Where(p => p.KullaniciId == _kullanici.Id).OrderByDescending(x => x.OlusturmaZamani).ToList();

                return View(_kullanici);
            }

            return View();
        }

        public IActionResult PostYaz(int? id)
        {
            var session = HttpContext.Session.GetInt32("userId");
            var _kullanici = _db.Kullanicilar.Find(session);
            TempData["KullaniciAdi"] = _kullanici.KullaniciAdi;

            PostViewModel vm = new PostViewModel()
            {
                Kullanici = _kullanici
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult PostYaz(PostViewModel vm, int id)
        {
            _db.Kullanicilar.Include(i => i.Posts).ToList();
            var _kullanici = _db.Kullanicilar.Find(id);
            TempData["KullaniciAdi"] = _kullanici.KullaniciAdi;
            vm.Kullanici= _kullanici;

            Post post = new Post();
            post.Baslik = vm.Baslik;
            post.Icerik= vm.Icerik;
            post.KullaniciId = _kullanici.Id;
            post.OlusturmaZamani = DateTime.Now;
            post.Kullanicilar.Add(_kullanici);

            if (ModelState.IsValid)
            {
                _db.Posts.Add(post);
                _kullanici.Posts.Add(post);
                _db.SaveChanges();
                return RedirectToAction("Profil", new { id });
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            return View(vm);
        }

        public IActionResult PostBegen(int id)
        {
            if (ModelState.IsValid)
            {
                var session = HttpContext.Session.GetInt32("userId");
                var _kullanici = _db.Kullanicilar.Find(session);
                var post = _db.Posts.Find(id);

                Kullanici kullanici = new Kullanici();
                kullanici.KullaniciAdi = _kullanici.KullaniciAdi;
                kullanici.Sifre= _kullanici.Sifre;

                if (post.Kullanicilar.Any(x => x.KullaniciAdi != kullanici.KullaniciAdi))
                {
                    post.Kullanicilar.Add(kullanici);

                    post.BegeniSayisi = post.Kullanicilar.Count;

                    _db.SaveChanges();
                    return RedirectToAction("Profil", new { id });
                }
                post.Kullanicilar.Remove(kullanici);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult PostSil(int id)
        {
            if (ModelState.IsValid)
            {
                var post = _db.Posts.Find(id);
                _db.Posts.Remove(post);
                _db.SaveChanges();
                return RedirectToAction("Profil");
            }

            return View("Index");
        }

        public IActionResult ProfilAc(int id)
        {
            var session = HttpContext.Session.GetInt32("userId");
            var _kullanici = _db.Kullanicilar.Find(session);
            var kullanici = _db.Kullanicilar.Find(id);
            ViewData["kullaniciId"] = _kullanici.Id;
            TempData["KullaniciAdi"] = _kullanici.KullaniciAdi;
            kullanici.Posts = _db.Posts.Where(p => p.KullaniciId == kullanici.Id).OrderByDescending(x => x.OlusturmaZamani).ToList();

            return View(kullanici);
        }
    }
}
