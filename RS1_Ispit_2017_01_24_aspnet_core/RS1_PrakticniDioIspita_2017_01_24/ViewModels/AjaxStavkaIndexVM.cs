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
            public int OdrzaniCasDetaljID { get; set; }
            public string Ucenik { get; set; }
            public string Ocjena { get; set; }
            public bool Odsutan { get; set; }
            public string OpravdanoOdsutan { get; set; }
        }

        public List<Red> Redovi { get; set; }
    }
}
