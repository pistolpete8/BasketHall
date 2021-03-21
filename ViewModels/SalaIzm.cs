using KosSala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KosSala.ViewModels
{
    public class SalaIzm
    {
        public Sala Sala { get; set; }
        public bool Izmena { get; set; }
        public byte[] Slikapom { get; set; }
    }
}