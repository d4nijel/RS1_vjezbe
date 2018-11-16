using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class OdrzaniCasDetalji
    {
        public int Id { get; set; }
        public int? Ocjena { get; set; }
        public bool Prisutan { get; set; }
        public bool? OpravdanoOdsutan { get; set; }
        public string Napomena { get; set; }

        public OdrzaniCas OdrzaniCas { get; set; }
        public int OdrzaniCasId { get; set; }

        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
    }
}
