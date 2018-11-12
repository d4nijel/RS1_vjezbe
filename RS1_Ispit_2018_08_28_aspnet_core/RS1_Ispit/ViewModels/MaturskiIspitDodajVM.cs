using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class MaturskiIspitDodajVM
    {
        public string Skola { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }
        public int OdjeljenjeId { get; set; }
        public string Ispitivac { get; set; }
        public int IspitivacId { get; set; }
        public string SkolskaGodina { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DatumIspita { get; set; }
        public List<SelectListItem> Predmeti { get; set; }
        public int PredmetId { get; set; }
    }
}
