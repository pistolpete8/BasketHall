using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KosSala.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Lokacija { get; set; }
        public double Rejting { get; set; }
        public string Opis { get; set; }

        
        public byte[] Slika { get; set; }
        public string  UpravnikId { get; set; }
        public int BrojOcena { get; set; }
        public int SumaOcena { get; set; }
    }
}