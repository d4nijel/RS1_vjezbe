using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using RS1_PrakticniDioIspita_2017_01_24.ViewModels;

namespace RS1_PrakticniDioIspita_2017_01_24.Helper
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

    }
}