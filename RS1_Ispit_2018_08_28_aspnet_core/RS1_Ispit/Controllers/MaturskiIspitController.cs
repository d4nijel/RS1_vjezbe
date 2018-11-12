using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.Helper;
using RS1_Ispit_asp.net_core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class MaturskiIspitController : Controller
    {
        private MojContext _context;

        public MaturskiIspitController(MojContext context)
        {
            _context = context;
        }
        // GET: MaturskiIspit
        public ActionResult Index()
        {

            NastavnikLoginVM logiraniNastavnik = HttpContext.GetLogiraniKorisnik();
            SkolskaGodina aktuelnaSkolskaGodina = HttpContext.GetAktuelnaSkolskaGodina();

            if (logiraniNastavnik == null)
                return RedirectToAction("Index", "Login");

            var model = new MaturskiIspitIndexVM
            {
                Redovi = _context.MaturskiIspit.Where(w => w.Nastavnik.SkolaID == logiraniNastavnik.SkolaID).Select(s => new MaturskiIspitIndexVM.Red
                {
                    MaturskiIspitId = s.Id,
                    Datum = s.Datum,
                    Skola = s.Nastavnik.Skola.Naziv,
                    Predmet = s.Predmet.Naziv,
                    Ispitivac = s.Nastavnik.Ime,
                    ProsjecniBodovi = _context.MaturskiIspitStavka.Where(w => w.MaturskiIspitId == s.Id && w.PristupioIspitu==true).Average(a => (float?)a.Rezultat) ?? 0
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Dodaj()
        {
            NastavnikLoginVM logiraniNastavnik = HttpContext.GetLogiraniKorisnik();
            SkolskaGodina aktuelnaSkolskaGodina = HttpContext.GetAktuelnaSkolskaGodina();

            var model = new MaturskiIspitDodajVM
            {
                Skola = logiraniNastavnik.SkolaNaziv,
                Odjeljenja = _context.Odjeljenje.Where(w => w.Razred == 4 && w.SkolaID == logiraniNastavnik.SkolaID && w.SkolskaGodina.Aktuelna == true).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Oznaka
                }).ToList(),
                Ispitivac = logiraniNastavnik.ImePrezime,
                IspitivacId = logiraniNastavnik.NastavnikId,
                SkolskaGodina = aktuelnaSkolskaGodina.Naziv,
                DatumIspita = DateTime.Now
            };

            //lista predmeta (PredmetId) koje predaje nastavnik u aktuelnoj školskog godini
            var lista1 = _context.PredajePredmet.Where(w => w.NastavnikID == logiraniNastavnik.NastavnikId && w.Odjeljenje.SkolskaGodina.Aktuelna == true).Select(s => s.PredmetID).Distinct().ToList();
            //lista svih predmeta
            var lista2 = _context.Predmet.ToList();

            model.Predmeti = new List<SelectListItem>();

            foreach (var x in lista1)
            {
                foreach (var y in lista2)
                {
                    if (x == y.Id)
                    {
                        model.Predmeti.Add(new SelectListItem { Value = y.Id.ToString(), Text = y.Naziv });
                    }
                }
            }
            return View(model);
        }
        public IActionResult Snimi(MaturskiIspitDodajVM input)
        {
            MaturskiIspit mi = new MaturskiIspit
            {
                Datum = input.DatumIspita,
                NastavnikId = input.IspitivacId,
                OdjeljenjeId = input.OdjeljenjeId,
                PredmetId = input.PredmetId
            };

            _context.MaturskiIspit.Add(mi);
            _context.SaveChanges();

            var y = _context.OdjeljenjeStavka.Where(w => w.OdjeljenjeId == mi.OdjeljenjeId).ToList();

            foreach (var i in y)
            {
                //broj negativnih zakljucnih ocjena
                int NegOcjena = _context.DodjeljenPredmet.Where(w => w.OdjeljenjeStavkaId == i.Id).Count(c => c.ZakljucnoKrajGodine == 1);
                //rezultat ucenika na maturskom ispitu za njegovo odjeljenje od x nastavnika za y predmet (ovaj dio nisam siguran ali sam ostavio??)
                //float Rezultat = _context.MaturskiIspitStavka.Where(w => w.OdjeljenjeStavkaId == i.Id && w.MaturskiIspit.OdjeljenjeId == mi.OdjeljenjeId && w.MaturskiIspit.NastavnikId == mi.NastavnikId && w.MaturskiIspit.PredmetId == mi.PredmetId).Select(s => s.Rezultat).FirstOrDefault();

                if (NegOcjena == 0 /*&& Rezultat < 55*/)
                {
                    double prosjek = _context.DodjeljenPredmet.Where(w => w.OdjeljenjeStavkaId == i.Id).Average(c => c.ZakljucnoKrajGodine);

                    MaturskiIspitStavka mis = new MaturskiIspitStavka
                    {
                        MaturskiIspitId = mi.Id,
                        OdjeljenjeStavkaId = i.Id,
                    };
                    if (prosjek > 4.5)
                    {
                        mis.Oslobodjen = true;
                    }
                    _context.MaturskiIspitStavka.Add(mis);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Detalji(int id)
        {
            MaturskiIspit mi = _context.MaturskiIspit.Where(w => w.Id == id).Include(i => i.Nastavnik).ThenInclude(t => t.Skola).Include(i => i.Odjeljenje).ThenInclude(t => t.SkolskaGodina).Include(i => i.Predmet).SingleOrDefault();

            var model = new MaturskiIspitDetaljiVM
            {
                MaturskiIspitID = mi.Id,
                Skola = mi.Nastavnik.Skola.Naziv,
                Odjeljenje = mi.Odjeljenje.Oznaka,
                Ispitivac = mi.Nastavnik.Ime.First() + ". " + mi.Nastavnik.Prezime,
                SkolskaGodina = mi.Odjeljenje.SkolskaGodina.Naziv,
                DatumIspita = mi.Datum.ToShortDateString(),
                Predmet = mi.Predmet.Naziv
            };

            return View(model);
        }
    }
}