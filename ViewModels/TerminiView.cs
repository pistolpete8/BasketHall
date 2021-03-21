using KosSala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KosSala.ViewModels
{
    public class TerminiView
    {
        public List<SelectListItem> listaSala { get; set; }
        public List<SelectListItem> VrstaBasketa { get; set; }
        public Termin Termin { get; set; }
        public IEnumerable<Sala> Sale { get; set; }
        public bool indikator { get; set; }
    }
}