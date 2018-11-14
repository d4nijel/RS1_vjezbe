using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class CasDodajVM
    {
        [DataType(DataType.Date)]
        public DateTime DatumOdrzanogCasa { get; set; }
        public List<SelectListItem> Angazovani { get; set; }
        public int AngazovanId { get; set; }

        public int OdrzaniCasId { get; set; }
        public string AkademskaGodinaPredmet { get; set; }
    }
}
