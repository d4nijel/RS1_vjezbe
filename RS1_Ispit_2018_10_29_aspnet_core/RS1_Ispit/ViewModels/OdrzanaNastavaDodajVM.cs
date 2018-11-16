using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaDodajVM
    {
        public string NastavnikImePrezime { get; set; }
        public int NastavnikId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumOdrzanogCasa { get; set; }
        public List<SelectListItem> PredavajuPredmet { get; set; }
        public int PredajePredmetId { get; set; }
    }
}
