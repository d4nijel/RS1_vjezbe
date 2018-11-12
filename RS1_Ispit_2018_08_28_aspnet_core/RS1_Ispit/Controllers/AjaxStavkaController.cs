using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.Helper;
using RS1_Ispit_asp.net_core.ViewModels;
using System;
using System.Linq;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class AjaxStavkaController : Controller
    {
        private MojContext _context;

        public AjaxStavkaController(MojContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            var model = new AjaxStavkaIndexVM
            {
                Redovi = _context.MaturskiIspitStavka.Where(w => w.MaturskiIspitId == id).Select(s => new AjaxStavkaIndexVM.Red
                {
                    MaturskiIspitStavkaId = s.Id,
                    Ucenik = s.OdjeljenjeStavka.Ucenik.ImePrezime,
                    ProstupioIspitu = s.PristupioIspitu,
                    Oslobodjen = s.Oslobodjen,
                    Rezultat = s.Rezultat,
                    OpciUspjeh = _context.DodjeljenPredmet.Where(p => p.OdjeljenjeStavkaId == s.OdjeljenjeStavkaId && p.ZakljucnoKrajGodine > 1).Average(a => (float?)a.ZakljucnoKrajGodine) ?? 0
                }).ToList()
            };
            return PartialView(model);
        }
        public IActionResult PristupioIspitu(int id)
        {
            MaturskiIspitStavka mis = _context.MaturskiIspitStavka.Find(id);

            if (mis.PristupioIspitu)
            {
                mis.PristupioIspitu = false;
            }
            else
            {
                mis.PristupioIspitu = true;
            }
            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + mis.MaturskiIspitId);
        }

        public IActionResult Uredi(int id)
        {
            MaturskiIspitStavka mis = _context.MaturskiIspitStavka.Where(w => w.Id == id).Include(i => i.OdjeljenjeStavka).ThenInclude(t => t.Ucenik).SingleOrDefault();

            var model = new AjaxStavkaUrediVM
            {
                MaturskiIspitStavkaId = mis.Id,
                Ucenik = mis.OdjeljenjeStavka.Ucenik.ImePrezime,
                Bodovi = mis.Rezultat
            };
            return PartialView(model);
        }

        public IActionResult Snimi(AjaxStavkaUrediVM input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Uredi", input);
            }

            MaturskiIspitStavka mis = _context.MaturskiIspitStavka.Find(input.MaturskiIspitStavkaId);

            mis.Rezultat = input.Bodovi;

            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + mis.MaturskiIspitId);
        }

        public IActionResult SnimiBox(int MaturskiIspitStavkaId, float Rezultat)
        {
            MaturskiIspitStavka mis = _context.MaturskiIspitStavka.Find(MaturskiIspitStavkaId);

            mis.Rezultat = Rezultat;

            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + mis.MaturskiIspitId);
        }
    }
}