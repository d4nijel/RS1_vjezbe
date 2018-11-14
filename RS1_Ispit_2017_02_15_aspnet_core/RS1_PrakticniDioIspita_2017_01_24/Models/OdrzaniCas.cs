using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.Models
{
    public class OdrzaniCas
    {
        public int Id { get; set; }

        public DateTime Datum { get; set; }

        public virtual Angazovan Angazovan { get; set; }
        public int AngazovanId { get; set; }
    }
}
