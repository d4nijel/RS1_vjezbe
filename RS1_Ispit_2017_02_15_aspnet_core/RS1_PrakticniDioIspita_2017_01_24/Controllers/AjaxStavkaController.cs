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
    public class AjaxStavkaController : Controller
    {
        private MojContext _context;
        public AjaxStavkaController(MojContext db)
        {
            _context = db;
        }
        public IActionResult Index(int id)
        {
            var model = new AjaxStavkaIndexVM
            {
                Redovi = _context.OdrzaniCasDetalji.Where(w => w.OdrzaniCasId == id).Select(s => new AjaxStavkaIndexVM.Red
                {
                    OdrzaniCasDetaljiId = s.Id,
                    Student = s.SlusaPredmet.UpisGodine.Student.Ime + " " + s.SlusaPredmet.UpisGodine.Student.Prezime,
                    Bodovi = s.BodoviNaCasu,
                    Prisutan = s.Prisutan
                }).ToList()
            };
            return PartialView(model);
        }

        public IActionResult UcenikJePrisutan(int id)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(id);

            if (ocd.Prisutan)
            {
                ocd.Prisutan = false;
            }
            else
            {
                ocd.Prisutan = true;
            }
            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + ocd.OdrzaniCasId);
        }

        public IActionResult Uredi(int id)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Where(w => w.Id == id).Include(i => i.SlusaPredmet).ThenInclude(t => t.UpisGodine).ThenInclude(p => p.Student).SingleOrDefault();

            var model = new AjaxStavkaUrediVM
            {
                Student = ocd.SlusaPredmet.UpisGodine.Student.Ime + " " + ocd.SlusaPredmet.UpisGodine.Student.Prezime,
                Bodovi = ocd.BodoviNaCasu,
                OdrzaniCasDetaljiId = ocd.Id
            };

            return PartialView(model);
        }

        public IActionResult Snimi(AjaxStavkaUrediVM input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Uredi", input);
            }

            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(input.OdrzaniCasDetaljiId);

            ocd.BodoviNaCasu = input.Bodovi;

            _context.SaveChanges();

            return Redirect("/AjaxStavka/Index?id=" + ocd.OdrzaniCasId);
        }
    }
}