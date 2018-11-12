using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.Helper;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class HomeController : Controller
    {
        private MojContext _context;

        public HomeController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //NastavnikLoginVM logiraniNastavnik = HttpContext.GetLogiraniKorisnik();
            //if (logiraniNastavnik == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            return View();
        }

        public IActionResult TestDB()
        {
            MojDBInitializer.Izvrsi(_context);
            return View(_context);
        }


    }
}