using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.Models
{
    public class OdrzaniCasDetalji
    {
        public int Id { get; set; }
        public bool Prisutan { get; set; }
        public int BodoviNaCasu { get; set; }

        public virtual OdrzaniCas OdrzaniCas { get; set; }
        public int OdrzaniCasId { get; set; }

        public virtual SlusaPredmet SlusaPredmet { get; set; }
        public int SlusaPredmetId { get; set; }
    }
}
