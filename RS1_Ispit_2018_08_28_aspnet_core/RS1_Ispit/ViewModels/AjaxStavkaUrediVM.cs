using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class AjaxStavkaUrediVM
    {
        public int MaturskiIspitStavkaId { get; set; }
        public string Ucenik { get; set; }
        [Range(0, 100, ErrorMessage = "Iznos rezultata može biti od 0 do 100!")]
        public float Bodovi { get; set; }
    }
}
