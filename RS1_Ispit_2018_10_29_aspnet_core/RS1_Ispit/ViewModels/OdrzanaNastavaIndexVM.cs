using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaIndexVM
    {
        public class Red
        {
            public int NastavnikId { get; set; }
            public string NastavnikImePrezime { get; set; }
            public string Skola { get; set; }
        }
        public List<Red> Redovi { get; set; }
    }
}
