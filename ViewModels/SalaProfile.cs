using KosSala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KosSala.ViewModels
{
    public class SalaProfile
    {
        public Sala Sala { get; set; }
        public ApplicationUser Upravnik { get; set; }
        public IEnumerable<Komentari> Komentari { get; set; }
        public string IdPrimaoca { get; set; }
        public string  Komentar { get; set; }
        public int valueRadio { get; set; }
    }
}