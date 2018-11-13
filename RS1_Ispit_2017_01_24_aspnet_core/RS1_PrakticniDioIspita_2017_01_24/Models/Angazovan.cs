﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.Models
{
    public class Angazovan
    {
        public int Id { get; set; }

        public Predmet Predmet { get; set; }
        public int? PredmetId { get; set; }

        public Nastavnik Nastavnik { get; set; }
        public int? NastavnikId { get; set; }

        public Odjeljenje Odjeljenje { get; set; }
        public int? OdjeljenjeId { get; set; }
    }
}
