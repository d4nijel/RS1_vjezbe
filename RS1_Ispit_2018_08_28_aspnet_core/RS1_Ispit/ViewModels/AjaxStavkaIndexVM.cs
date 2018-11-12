using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class AjaxStavkaIndexVM
    {
        public class Red
        {
            public int MaturskiIspitStavkaId { get; set; }
            public string Ucenik { get; set; }
            public float OpciUspjeh { get; set; }
            public bool ProstupioIspitu { get; set; }
            public bool Oslobodjen { get; set; }
            public float Rezultat { get; set; }
        }
        public List<Red> Redovi { get; set; }
    }
}
