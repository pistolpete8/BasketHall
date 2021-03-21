using KosSala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KosSala.ViewModels
{
    public class KorisnikIzm
    {
        public ApplicationUser Korisnik { get; set; }
        public byte[] Slika { get; set; }
        public bool Indikator { get; set; }
        public RegisterViewModel Registracija { get; set; }
    }
}