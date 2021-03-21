using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foolproof;

namespace KosSala.Models
{
    public class Termin
    {
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime PocetakTermina { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        
        public DateTime KrajTermina { get; set; }
        public int Id { get; set; }
        public bool Slobodan { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string SalaId { get; set; }
        public int BrojPrijavljenih { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string VrstaBasketa { get; set; }


       


    }
   

}
