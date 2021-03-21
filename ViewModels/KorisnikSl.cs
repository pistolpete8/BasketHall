using KosSala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KosSala.ViewModels
{
    public class KorisnikSl
    {
        public ApplicationUser Korisnik { get; set; }

        public IEnumerable<ApplicationUser> Igraci { get; set; }

        public float prosecanRejting { get; set; }
        public int[] brojeviPoOcenama { get; set; }
        public byte[] Slika { get; set; }

        public IEnumerable<ApplicationUser> Korinsnici { get; set; }
        public IEnumerable<ApplicationUser> Postavljaci { get; set; }
        public IEnumerable<Komentari> Komentari { get; set; }

        public string Komentar { get; set; }
        public string idPrimaoca { get; set; }

        public string idUser { get; set; }
    }
}