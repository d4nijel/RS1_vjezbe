using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.Helper;
using RS1_Ispit_asp.net_core.ViewModels;

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
            MojDBInitializer.Izvrsi(_db);
            HttpContext.SetLogiraniKorisnik(null);
            return RedirectToAction("Index");
        }

        public ActionResult Provjera(string username, string password)
        {
            MojDBInitializer.Izvrsi(_db);
            var Nastavnik = _db.Nastavnik.Where(x => x.Username == username && x.Password == password).Select(s => new NastavnikLoginVM
            {
                Username = s.Username,
                ImePrezime = s.Ime + " " + s.Prezime,
                NastavnikId = s.Id,
                SkolaID = s.SkolaID,
                SkolaNaziv = s.Skola.Naziv
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