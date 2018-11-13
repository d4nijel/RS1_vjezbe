using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class AjaxStavkaUrediVM
    {
        public int OdrzaniCasDetaljId { get; set; }
        public string Ucenik { get; set; }
        [Range(1,5,ErrorMessage ="Ocjena može biti od 1 do 5!")]
        public int Ocjena { get; set; }
        public bool OpravdanoOdsutan { get; set; }
        public bool Odsutan { get; set; }
    }
}
