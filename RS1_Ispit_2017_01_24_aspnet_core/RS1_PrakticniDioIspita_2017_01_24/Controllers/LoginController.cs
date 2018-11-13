using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using RS1_PrakticniDioIspita_2017_01_24.Helper;
using RS1_PrakticniDioIspita_2017_01_24.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class LoginController : Controller
    {
        private MojContext _db;

        public LoginController(MojContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);
            return RedirectToAction("Index");
        }

        public ActionResult Provjera(string username, string password)
        {
            var Nastavnik = _db.Nastavnik.Where(x => x.Username == username && x.Password == password).Select(s => new NastavnikLoginVM
            {
                Username = s.Username,
                ImePrezime = s.Ime,
                NastavnikId = s.Id
            }).FirstOrDefault();

            HttpContext.SetLogiraniKorisnik(Nastavnik);
            if (Nastavnik == null)
                return RedirectToAction("Index", "Login");
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}