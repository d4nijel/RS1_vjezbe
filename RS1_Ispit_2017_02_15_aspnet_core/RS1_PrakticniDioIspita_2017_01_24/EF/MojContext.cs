using Microsoft.EntityFrameworkCore;
using RS1_PrakticniDioIspita_2017_01_24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.EF
{
    public class MojContext : DbContext
    {
        public MojContext(DbContextOptions<MojContext> options) : base(options)
        {

        }

        public DbSet<AkademskaGodina> AkademskaGodina { get; set; }
        public DbSet<Angazovan> Angazovan { get; set; }
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<SlusaPredmet> SlusaPredmet { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<UpisGodine> UpisGodine { get; set; }
        public DbSet<OdrzaniCas> OdrzaniCas { get; set; }
        public DbSet<OdrzaniCasDetalji> OdrzaniCasDetalji { get; set; }
    }
}
