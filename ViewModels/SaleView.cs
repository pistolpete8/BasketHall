using KosSala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KosSala.ViewModels
{
    public class SaleView
    {
        public IEnumerable<Sala> Sale { get; set; }
        public IEnumerable<Termin> Termini { get; set; }

        public KorisnikTermin KorisnikTermin { get; set; }

        public ApplicationUser Igrac{ get; set; }
        public IEnumerable<ApplicationUser> Players { get; set; }

        public ApplicationUser UpravnikSale { get; set; }
        public  bool initialFlag { get; set; }

        public Termin Termin { get; set; }

        public Sala Sala { get; set; }
        public int ID { get; set; }

        public string LoggedUserId { get; set; }

        

    }
    
}
