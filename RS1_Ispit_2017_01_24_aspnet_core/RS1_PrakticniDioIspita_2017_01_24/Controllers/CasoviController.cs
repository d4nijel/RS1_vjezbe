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
    public class CasoviController : Controller
    {
        private MojContext _context;

        public CasoviController(MojContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            NastavnikLoginVM logiraniNastavnik = HttpContext.GetLogiraniKorisnik();
            if (logiraniNastavnik == null)
            {
                RedirectToAction("Index", "Login");
            }

            var model = new CasoviIndexVM
            {
                Redovi = _context.OdrzaniCas.Where(w => w.Angazovan.NastavnikId == logiraniNastavnik.NastavnikId).Select(s => new CasoviIndexVM.Red
                {
                    OdrzaniCasId = s.Id,
                    DatumOdrzanogCasa = s.Datum,
                    Odjeljenje = s.Angazovan.Odjeljenje.Oznaka,
                    BrojPrisutnih = _context.OdrzaniCasDetalj.Where(t => t.OdrzaniCasId == s.Id).Count(c => c.Odsutan == false).ToString() + " od " + _context.OdrzaniCasDetalj.Where(t => t.OdrzaniCasId == s.Id).Count().ToString(),
                    Predmet = s.Angazovan.Predmet.Naziv,
                    NajboljiUcenikNaPredmetu = _context.OdrzaniCasDetalj.Where(p => p.OdrzaniCasId == s.Id).OrderByDescending(o => o.Ocjena).Select(z => z.UpisUOdjeljenje.Ucenik.Ime).FirstOrDefault().ToString()
                }).ToList()
            };
            return View(model);
        }
        public IActionResult Dodaj()
        {
            NastavnikLoginVM logiraniNastavnik = HttpContext.GetLogiraniKorisnik();

            var model = new CasoviDodajVM
            {
                Datum = DateTime.Now,
                Angazovani = _context.Angazovan.Where(w => w.NastavnikId == logiraniNastavnik.NastavnikId).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Odjeljenje.Oznaka + " / " + s.Predmet.Naziv
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Snimi(CasoviDodajVM input)
        {
            NastavnikLoginVM logiraniNastavnik = HttpContext.GetLogiraniKorisnik();

            if (!ModelState.IsValid)
            {
                input.Angazovani = _context.Angazovan.Where(w => w.NastavnikId == logiraniNastavnik.NastavnikId).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Odjeljenje.Oznaka + " / " + s.Predmet.Naziv
                }).ToList();

                return View("Dodaj", input);
            }

            OdrzaniCas ocs = _context.OdrzaniCas.Find(input.OdrzaniCasId);

            if (ocs != null)
            {
                ocs.Datum = input.Datum;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            OdrzaniCas oc = new OdrzaniCas
            {
                AngazovanId = input.AngazovaniId,
                Datum = input.Datum
            };
            _context.OdrzaniCas.Add(oc);
            _context.SaveChanges();

            //odjeljenje u kome je angozavan nastavnik
            var OdjeljenjeId = _context.Angazovan.Where(w => w.Id == input.AngazovaniId).Select(s => s.OdjeljenjeId).SingleOrDefault();

            var list = _context.UpisUOdjeljenje.Where(w => w.OdjeljenjeId == OdjeljenjeId).ToList();

            foreach (var x in list)
            {
                OdrzaniCasDetalj ocd = new OdrzaniCasDetalj
                {
                    OdrzaniCasId = oc.Id,
                    UpisUOdjeljenjeId = x.Id
                };
                _context.OdrzaniCasDetalj.Add(ocd);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            OdrzaniCas oc = _context.OdrzaniCas.Where(w => w.Id == id).Include(i => i.Angazovan).ThenInclude(t => t.Odjeljenje).Include(i => i.Angazovan).ThenInclude(t => t.Predmet).SingleOrDefault();

            NastavnikLoginVM logiraniNastavnik = HttpContext.GetLogiraniKorisnik();

            var model = new CasoviDodajVM
            {
                Datum = oc.Datum,
                Angazovan = oc.Angazovan.Odjeljenje.Oznaka + " / " + oc.Angazovan.Predmet.Naziv,
                OdrzaniCasId = oc.Id
            };
            return View(model);
        }
    }
}