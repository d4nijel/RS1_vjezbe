using RS1_Ispit_asp.net_core.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaOdabirVM
    {
        public class Red
        {
            public int OdrzaniCasId { get; set; }
            public DateTime DatumOdrzanogcasa { get; set; }
            public string SkolskaGodinaOdjeljenje { get; set; }
            public string Predmet { get; set; }
            public List<string> OdsutniUcenici { get; set; }
        }
        public List<Red> Redovi { get; set; }
        public int NastavnikId { get; set; }
        public string NastavnikImePrezime { get; set; }
        public string NastavnikSkola { get; set; }
    }
}
