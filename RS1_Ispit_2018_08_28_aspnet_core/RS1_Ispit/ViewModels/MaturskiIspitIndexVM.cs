using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class MaturskiIspitIndexVM
    {
        public class Red
        {
            public int MaturskiIspitId { get; set; }
            [DataType(DataType.Date)]
            public DateTime Datum { get; set; }
            public string Skola { get; set; }
            public string Predmet { get; set; }
            public string Ispitivac { get; set; }
            public float ProsjecniBodovi { get; set; }
        }
        public List<Red> Redovi { get; set; }
    }
}
