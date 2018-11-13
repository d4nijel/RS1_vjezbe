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
                Redovi = _context.OdrzaniCasDetalj.Where(w => w.OdrzaniCasId == id).Select(s => new AjaxStavkaIndexVM.Red
                {
                    OdrzaniCasDetaljID = s.Id,
                    Ucenik = s.UpisUOdjeljenje.Ucenik.Ime,
                    Ocjena = s.Ocjena.HasValue ? s.Ocjena.Value.ToString() : "",
                    Odsutan = s.Odsutan,
                    OpravdanoOdsutan = s.OpravdanoOdsutan.HasValue ? s.OpravdanoOdsutan.Value ? "DA" : "NE" : ""
                }).ToList()
            };
            return PartialView(model);
        }

        public IActionResult UcenikJePrisutan(int id)
        {
            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Find(id);

            if (ocd.Odsutan)
            {
                ocd.Odsutan = false;
                ocd.OpravdanoOdsutan = null;
            }
            else
            {
                ocd.Odsutan = true;
                ocd.Ocjena = null;
            }
            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + ocd.OdrzaniCasId);
        }

        public IActionResult Uredi(int id)
        {
            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Where(w => w.Id == id).Include(i => i.UpisUOdjeljenje).ThenInclude(t => t.Ucenik).SingleOrDefault();

            var model = new AjaxStavkaUrediVM
            {
                OdrzaniCasDetaljId = ocd.Id,
                Ucenik = ocd.UpisUOdjeljenje.Ucenik.Ime,
                Ocjena = ocd.Ocjena ?? 1,
                OpravdanoOdsutan = ocd.OpravdanoOdsutan ?? false,
                Odsutan = ocd.Odsutan
            };

            if (ocd.Odsutan)
            {

                return PartialView("Odsutan", model);
            }
            else
            {
                return PartialView("Prisutan", model);
            }
        }

        public IActionResult Snimi(AjaxStavkaUrediVM input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                if (input.Odsutan)
                {

                    return PartialView("Odsutan", input);
                }
                else
                {
                    return PartialView("Prisutan", input);
                }
            }
            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Find(input.OdrzaniCasDetaljId);

            if (ocd.Odsutan)
            {
                ocd.OpravdanoOdsutan = input.OpravdanoOdsutan;
            }
            else
            {
                ocd.Ocjena = input.Ocjena;
            }
            _context.SaveChanges();
            return Redirect("/AjaxStavka/Index?id=" + ocd.OdrzaniCasId);
        }
    }
}