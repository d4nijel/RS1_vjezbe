using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using RS1_PrakticniDioIspita_2017_01_24.Helper;
using RS1_PrakticniDioIspita_2017_01_24.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_PrakticniDioIspita_2017_01_24.Models;
using Microsoft.EntityFrameworkCore;

namespace RS1_PrakticniDioIspita_2017_01_24.Controllers
{
    public class CasController : Controller
    {
        private MojContext _context;

        public CasController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            NastavnikLoginVM LogiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (LogiraniKorisnik == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var model = new CasIndexVM
            {
                Redovi = _context.OdrzaniCas.Where(w => w.Angazovan.NastavnikId == LogiraniKorisnik.NastavnikId).Select(s => new CasIndexVM.Red
                {
                    OdrzaniCasId = s.Id,
                    DatumOdrzanogCasa = s.Datum,
                    AkademskaGodina = s.Angazovan.AkademskaGodina.Opis,
                    Predmet = s.Angazovan.Predmet.Naziv,
                    BrojPrisutnihStudenata = _context.OdrzaniCasDetalji.Where(w => w.OdrzaniCasId == s.Id && w.Prisutan == true).Count().ToString() + " od " + _context.OdrzaniCasDetalji.Where(w => w.OdrzaniCasId == s.Id).Count().ToString(),
                    ProsjecnaOcjena = _context.SlusaPredmet.Where(w => w.AngazovanId == s.AngazovanId).Average(a => (float?)a.Ocjena) ?? 0
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Dodaj()
        {
            NastavnikLoginVM LogiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (LogiraniKorisnik == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var model = new CasDodajVM
            {
                Angazovani = _context.Angazovan.Where(w => w.NastavnikId == LogiraniKorisnik.NastavnikId).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.AkademskaGodina.Opis + " / " + s.Predmet.Naziv
                }).ToList(),
                DatumOdrzanogCasa = DateTime.Now
            };

            return View(model);
        }

        public IActionResult Snimi(CasDodajVM input)
        {
            NastavnikLoginVM LogiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (LogiraniKorisnik == null)
            {
                return RedirectToAction("Index", "Login");
            }

            OdrzaniCas ocs = _context.OdrzaniCas.Find(input.OdrzaniCasId);

            if (!ModelState.IsValid)
            {
                input.Angazovani = _context.Angazovan.Where(w => w.NastavnikId == LogiraniKorisnik.NastavnikId).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.AkademskaGodina.Opis + " / " + s.Predmet.Naziv
                }).ToList();

                if (ocs != null)
                {
                    return View("Uredi", input);
                }
                return View("Dodaj", input);
            }

            if (ocs != null)
            {
                ocs.Datum = input.DatumOdrzanogCasa;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            OdrzaniCas oc = new OdrzaniCas
            {
                Datum = input.DatumOdrzanogCasa,
                AngazovanId = input.AngazovanId
            };

            _context.OdrzaniCas.Add(oc);
            _context.SaveChanges();

            var listaSlusaPredmet = _context.SlusaPredmet.Where(w => w.AngazovanId == oc.AngazovanId).ToList();

            foreach (var x in listaSlusaPredmet)
            {
                OdrzaniCasDetalji ocd = new OdrzaniCasDetalji
                {
                    OdrzaniCasId = oc.Id,
                    SlusaPredmetId = x.Id
                };
                _context.OdrzaniCasDetalji.Add(ocd);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Uredi(int id)
        {
            NastavnikLoginVM LogiraniKorisnik = HttpContext.GetLogiraniKorisnik();
            if (LogiraniKorisnik == null)
            {
                return RedirectToAction("Index", "Login");
            }

            OdrzaniCas oc = _context.OdrzaniCas.Where(w => w.Id == id).Include(i => i.Angazovan).ThenInclude(t => t.AkademskaGodina).Include(i => i.Angazovan).ThenInclude(t => t.Predmet).SingleOrDefault();

            var model = new CasDodajVM
            {
                AkademskaGodinaPredmet = oc.Angazovan.AkademskaGodina.Opis + " / " + oc.Angazovan.Predmet.Naziv,
                DatumOdrzanogCasa = oc.Datum,
                OdrzaniCasId = oc.Id
            };

            return View(model);
        }
    }
}