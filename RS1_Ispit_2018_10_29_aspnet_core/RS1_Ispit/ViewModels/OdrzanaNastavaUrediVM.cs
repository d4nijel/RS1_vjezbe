using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class OdrzanaNastavaUrediVM
    {
        public int OdrzaniCasId { get; set; }
        public int NastavnikId { get; set; }
        public DateTime DatumCasa { get; set; }
        public string Odjeljenje { get; set; }
        public string SadrzajCasa { get; set; }
    }
}
