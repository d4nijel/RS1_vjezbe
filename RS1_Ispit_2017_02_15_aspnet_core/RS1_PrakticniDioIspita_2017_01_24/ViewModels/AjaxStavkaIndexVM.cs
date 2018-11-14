using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class AjaxStavkaIndexVM
    {
        public class Red
        {
            public int OdrzaniCasDetaljiId { get; set; }
            public string Student { get; set; }
            public int Bodovi { get; set; }
            public bool Prisutan { get; set; }
        }
        public List<Red> Redovi { get; set; }
    }
}
