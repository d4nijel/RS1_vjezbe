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
                Redovi = _context.OdrzaniCasDetalji.Where(w => w.OdrzaniCasId == id).Select(s => new AjaxStavkaIndexVM.Red
                {
                    OdrzaniCasDetaljiId = s.Id,
                    Ucenik = s.OdjeljenjeStavka.Ucenik.ImePrezime,
                    Ocjena = s.Ocjena,
                    Prisutan = s.Prisutan,
                    OpravdanoOdsutan = s.OpravdanoOdsutan
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
                ocd.Ocjena = null;
            }
            else
            {
                ocd.Prisutan = true;
                ocd.OpravdanoOdsutan = null;
                ocd.Napomena = null;
            }
            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + ocd.OdrzaniCasId);
        }

        public IActionResult Uredi(int id)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Where(w => w.Id == id).Include(i => i.OdjeljenjeStavka).ThenInclude(t => t.Ucenik).SingleOrDefault();

            var model = new AjaxStavkaUrediVM
            {
                OdrzaniCasDetaljiId = ocd.Id,
                Ucenik = ocd.OdjeljenjeStavka.Ucenik.ImePrezime,
                Ocjena = ocd.Ocjena.HasValue ? ocd.Ocjena.Value : 1,
                Napomena = ocd.Napomena,
                OpravdanoOdsutan = ocd.OpravdanoOdsutan.HasValue ? ocd.OpravdanoOdsutan.Value : false
            };

            if (ocd.Prisutan)
            {
                return PartialView("Prisutan", model);
            }
            else
            {
                return PartialView("Odsutan", model);
            }
        }
        public IActionResult Snimi(AjaxStavkaUrediVM input)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(input.OdrzaniCasDetaljiId);

            if (ocd.Prisutan)
            {
                ocd.Ocjena = input.Ocjena;
            }
            else
            {
                ocd.Napomena = input.Napomena;
                ocd.OpravdanoOdsutan = input.OpravdanoOdsutan;
            }
            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + ocd.OdrzaniCasId);
        }
    }
}