using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class MaturskiIspitDetaljiVM
    {
        public int MaturskiIspitID { get; set; }
        public string Skola { get; set; }
        public string Odjeljenje { get; set; }
        public string Ispitivac { get; set; }
        public string SkolskaGodina { get; set; }
        public string DatumIspita { get; set; }
        public string Predmet { get; set; }
    }
}
