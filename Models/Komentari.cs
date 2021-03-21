using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KosSala.Models
{
    public class Komentari
    {
        public int Id { get; set; }
        public string ImePostavljaca { get; set; }

        public string PostavljacId { get; set; }
        
        public string PrimalacId { get; set; }
        public string Komentar { get; set; }
        public byte[] SlikaPostavljaca { get; set; }

        public DateTime Datum_Postavljanja { get; set; }

        public string noviProperti { get; set; }

    }
}