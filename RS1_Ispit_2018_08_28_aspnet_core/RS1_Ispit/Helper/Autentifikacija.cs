using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, NastavnikLoginVM korisnik)
        {
            context.Session.Set(LogiraniKorisnik, korisnik);
        }

        public static NastavnikLoginVM GetLogiraniKorisnik(this HttpContext context)
        {
            return context.Session.Get<NastavnikLoginVM>(LogiraniKorisnik);
        }

        public static SkolskaGodina GetAktuelnaSkolskaGodina(this HttpContext context)
        {
            MojContext db = context.RequestServices.GetService<MojContext>();
            return db.SkolskaGodina.FirstOrDefault(s => s.Aktuelna);
        }

    }
}