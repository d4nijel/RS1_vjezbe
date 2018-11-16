using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class AjaxStavkaIndexVM
    {
        public class Red
        {
            public int OdrzaniCasDetaljiId { get; set; }
            public string Ucenik { get; set; }
            public int? Ocjena { get; set; }
            public bool Prisutan { get; set; }
            public bool? OpravdanoOdsutan { get; set; }
        }
        public List<Red> Redovi { get; set; }
    }
}
