﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.Models
{
    public class Predmet
    {
        public int Id { get; set; }
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public int ECTS { get; set; }

        public int Godina { get; set; }
    }
}
