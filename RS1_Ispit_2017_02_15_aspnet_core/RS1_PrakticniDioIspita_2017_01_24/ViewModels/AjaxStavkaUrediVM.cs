using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class AjaxStavkaUrediVM
    {
        public int OdrzaniCasDetaljiId { get; set; }
        public string Student { get; set; }
        [Range(1,100,ErrorMessage ="Bodovi mogu biti od 1 do 100!")]
        public int Bodovi { get; set; }
    }
}
