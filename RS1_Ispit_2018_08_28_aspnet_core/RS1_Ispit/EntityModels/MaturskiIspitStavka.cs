using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspitStavka
    {
        public int Id { get; set; }
        public float Rezultat { get; set; }
        public bool PristupioIspitu { get; set; }
        public bool Oslobodjen { get; set; }

        [ForeignKey(nameof(OdjeljenjeStavkaId))]
        public virtual OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }

        [ForeignKey(nameof(MaturskiIspitId))]
        public virtual MaturskiIspit MaturskiIspit { get; set; }
        public int MaturskiIspitId { get; set; }
    }
}
