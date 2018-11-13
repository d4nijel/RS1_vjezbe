using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class CasoviIndexVM
    {
        public class Red
        {
            public int OdrzaniCasId { get; set; }
            public DateTime DatumOdrzanogCasa { get; set; }
            public string Odjeljenje { get; set; }
            public string Predmet { get; set; }
            public string BrojPrisutnih { get; set; }
            public string NajboljiUcenikNaPredmetu { get; set; }
        }
        public List<Red> Redovi { get; set; }
    }
}
