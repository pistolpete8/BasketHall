using KosSala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KosSala.ViewModels
{
    public class KomentariView
    {
        public IEnumerable<ApplicationUser> Korinsnici { get; set; }
        public IEnumerable<ApplicationUser> Postavljaci { get; set; }
        public IEnumerable<Komentari> Komentari { get; set; }

        public IEnumerable<Sala> Sale { get; set; }
        public IEnumerable<Termin> Termini { get; set; }
        public int valueRadio { get; set; }

        public string Komentar { get; set; }
        public string idPrimaoca { get; set; }
        
        public string idUser { get; set; }


    }
}