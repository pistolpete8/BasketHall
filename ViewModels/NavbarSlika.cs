using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KosSala.Models;

namespace KosSala.ViewModels
{
    public class NavbarSlika
    {
        public ApplicationUser Korisnik { get; set; }
        public byte[] Slika { get; set; }
    }
}