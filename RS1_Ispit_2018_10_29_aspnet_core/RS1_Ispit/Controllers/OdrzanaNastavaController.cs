using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class OdrzanaNastavaController : Controller
    {
        private MojContext _context;
        public OdrzanaNastavaController(MojContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new OdrzanaNastavaIndexVM
            {
                Redovi = _context.Nastavnik.Select(s => new OdrzanaNastavaIndexVM.Red
                {
                    NastavnikId = s.Id,
                    Skola = s.Skola.Naziv,
                    NastavnikImePrezime = s.Ime + " " + s.Prezime
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Odabir(int id)
        {
            Nastavnik nastavnik = _context.Nastavnik.Where(w => w.Id == id).Include(i => i.Skola).SingleOrDefault();

            var model = new OdrzanaNastavaOdabirVM
            {
                Redovi = _context.OdrzaniCas.Where(w => w.PredajePredmet.NastavnikID == nastavnik.Id).Select(s => new OdrzanaNastavaOdabirVM.Red
                {
                    OdrzaniCasId = s.Id,
                    DatumOdrzanogcasa = s.Datum,
                    SkolskaGodinaOdjeljenje = s.PredajePredmet.Odjeljenje.SkolskaGodina.Naziv + " / " + s.PredajePredmet.Odjeljenje.Oznaka,
                    Predmet = s.PredajePredmet.Predmet.Naziv,
                }).ToList(),
                NastavnikId = id,
                NastavnikImePrezime = nastavnik.Ime.First() + ". " + nastavnik.Prezime,
                NastavnikSkola = nastavnik.Skola.Naziv
            };

            foreach (var x in model.Redovi)
            {
                x.OdsutniUcenici = new List<string>();

                //lista OdjeljenjeStavkaId koji nisu prisutni na casu
                var ListaOdjeljenjeStavka = _context.OdrzaniCasDetalji.Where(w => w.OdrzaniCasId == x.OdrzaniCasId && w.Prisutan == false).Select(s => s.OdjeljenjeStavkaId).ToList();

                if (ListaOdjeljenjeStavka.Count() != 0)
                {
                    foreach (var y in ListaOdjeljenjeStavka)
                    {
                        //dodavanje imena i prezimena učenika koji nisu prisutni u listu
                        x.OdsutniUcenici.Add(_context.OdjeljenjeStavka.Where(w => w.Id == y).Select(s => s.Ucenik.ImePrezime).SingleOrDefault() + ", ");
                    }
                }
            }
            return View(model);
        }

        public IActionResult Dodaj(int id)
        {
            Nastavnik nastavnik = _context.Nastavnik.Find(id);

            var model = new OdrzanaNastavaDodajVM
            {
                NastavnikImePrezime = nastavnik.Ime.First() + ". " + nastavnik.Prezime,
                DatumOdrzanogCasa = DateTime.Now,
                PredavajuPredmet = _context.PredajePredmet.Where(w => w.NastavnikID == nastavnik.Id).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Odjeljenje.Oznaka + " / " + s.Predmet.Naziv
                }).ToList(),
                NastavnikId = nastavnik.Id
            };
            return View(model);
        }

        public IActionResult Snimi(OdrzanaNastavaDodajVM input)
        {
            OdrzaniCas oc = new OdrzaniCas
            {
                Datum = input.DatumOdrzanogCasa,
                PredajePredmetId = input.PredajePredmetId
            };

            _context.OdrzaniCas.Add(oc);
            _context.SaveChanges();

            //OdjeljenjeId za koje je održan čas
            var OdjeljenjeId = _context.PredajePredmet.Where(w => w.Id == input.PredajePredmetId).Select(s => s.OdjeljenjeID).SingleOrDefault();

            var listaOdjeljenjeStavka = _context.OdjeljenjeStavka.Where(w => w.OdjeljenjeId == OdjeljenjeId).ToList();

            foreach (var x in listaOdjeljenjeStavka)
            {
                OdrzaniCasDetalji ocd = new OdrzaniCasDetalji
                {
                    OdjeljenjeStavkaId = x.Id,
                    OdrzaniCasId = oc.Id
                };
                _context.OdrzaniCasDetalji.Add(ocd);
            }
            _context.SaveChanges();

            return Redirect("/OdrzanaNastava/Odabir?id=" + input.NastavnikId);
        }

        public IActionResult Uredi(int id)
        {
            OdrzaniCas oc = _context.OdrzaniCas.Where(w => w.Id == id).Include(i => i.PredajePredmet).ThenInclude(t => t.Odjeljenje)
                .Include(i => i.PredajePredmet).ThenInclude(t => t.Predmet).SingleOrDefault();

            var model = new OdrzanaNastavaUrediVM
            {
                OdrzaniCasId = oc.Id,
                DatumCasa = oc.Datum,
                Odjeljenje = oc.PredajePredmet.Odjeljenje.Oznaka + " / " + oc.PredajePredmet.Predmet.Naziv,
                SadrzajCasa = oc.SadrzajCasa,
                NastavnikId = oc.PredajePredmet.NastavnikID
            };
            return View(model);
        }

        public IActionResult SnimiEdit(OdrzanaNastavaUrediVM input)
        {
            OdrzaniCas oc = _context.OdrzaniCas.Find(input.OdrzaniCasId);

            oc.SadrzajCasa = input.SadrzajCasa;

            _context.SaveChanges();

            return Redirect("/OdrzanaNastava/Odabir?id=" + input.NastavnikId);
        }

        public IActionResult Obrisi(int id)
        {
            OdrzaniCas oc = _context.OdrzaniCas.Where(w => w.Id == id).Include(i => i.PredajePredmet).SingleOrDefault();

            int NastavnikId = oc.PredajePredmet.NastavnikID;

            _context.OdrzaniCas.Remove(oc);
            _context.SaveChanges();

            return Redirect("/OdrzanaNastava/Odabir?id=" + NastavnikId);
        }
    }
}