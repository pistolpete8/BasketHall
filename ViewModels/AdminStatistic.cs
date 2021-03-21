using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KosSala.Models;

namespace KosSala.ViewModels
{
    public class AdminStatistic
    {
        public ApplicationUser Korisnik { get; set; }
        public byte[] Slika { get; set; }
        public IEnumerable<Sala> Sale { get; set; }
        public IEnumerable<Termin> Termini { get; set; }
        public IEnumerable<ApplicationUser> Korinsnici { get; set; }
        public IEnumerable<ApplicationUser> Upravnici { get; set; }
    }
}
