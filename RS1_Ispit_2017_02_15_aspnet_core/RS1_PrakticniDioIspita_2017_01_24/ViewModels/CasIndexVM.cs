using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class CasIndexVM
    {
        public class Red
        {
            public int OdrzaniCasId { get; set; }
            public DateTime DatumOdrzanogCasa { get; set; }
            public string AkademskaGodina { get; set; }
            public string Predmet { get; set; }
            public string BrojPrisutnihStudenata { get; set; }
            public float ProsjecnaOcjena { get; set; }
        }
        public List<Red> Redovi { get; set; }
    }
}
