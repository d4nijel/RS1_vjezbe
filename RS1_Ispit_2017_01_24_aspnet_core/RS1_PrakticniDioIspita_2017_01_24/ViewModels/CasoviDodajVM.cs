using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class CasoviDodajVM
    {
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public List<SelectListItem> Angazovani { get; set; }
        public int AngazovaniId { get; set; }
        public string Angazovan { get; set; }
        public int OdrzaniCasId { get; set; }
    }
}
