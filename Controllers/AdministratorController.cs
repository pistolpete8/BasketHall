using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KosSala.Models;
using KosSala.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;

namespace KosSala.Controllers
{
    public class AdministratorController : Controller

    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public AdministratorController()
        {
        }
        public AdministratorController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Administrator
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
            
        }
        public ActionResult NavbarSlika(NavbarSlika model)
        {
            model.Korisnik = db.Users.Where(s => s.Id == User.Identity.GetUserId()).FirstOrDefault();
            model.Slika = model.Korisnik.SlikaKorisnika;
            return View(model);
        }

        public ActionResult AdministratorSignedUp(AdminStatistic model)
        {
            ViewBag.Message = "Admin Home Page";
            model.Korinsnici = db.Users.Where(s => s.FKorisnik == true).ToList();
            model.Upravnici = db.Users.Where(s => s.FUpravnikSale == true).ToList();
            model.Sale = db.Sale.ToList();
            model.Termini = db.Termini.ToList();
            model.Korisnik = db.Users.Where(s => s.UserName == User.Identity.Name).FirstOrDefault();
            model.Slika = model.Korisnik.SlikaKorisnika;
            return View(model);

        }
        public ActionResult PrikaziKorisnike()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var Korisnici = db.Users.Where(s => s.FKorisnik == true).ToList();
            foreach (ApplicationUser a in Korisnici)
            {
                var base64 = Convert.ToBase64String(a.SlikaKorisnika);
                var imgsrc = string.Format("data:image/png;base64,{0}", base64);
                a.SlikaString = imgsrc;
            }
            return Json(new { data = Korisnici }, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult PrikaziUpravnike()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var Korisnici = db.Users.Where(s => s.FUpravnikSale == true).ToList();
            foreach (ApplicationUser a in Korisnici)
            {
                var base64 = Convert.ToBase64String(a.SlikaKorisnika);
                var imgsrc = string.Format("data:image/png;base64,{0}", base64);
                a.SlikaString = imgsrc;
            }
            return Json(new { data = Korisnici }, JsonRequestBehavior.AllowGet);

        }
       


        public ActionResult SviKorisnici()
        {
            return View();
        }

        public ActionResult SviUpravnici()
        {
            return View();
        }
        public ActionResult SveSale()
        {
            string upravnikId = User.Identity.GetUserId();
            SaleView sale = new SaleView();
            sale.Sale = db.Sale.ToList();

            return View(sale);
        }




        public ActionResult DodajEdit(string id ="0")
        {
            var korisnik = new KorisnikIzm();
            //korisnik.Korisnik = db.Users.Where(s => s.Id == id).FirstOrDefault();
            //korisnik.Slika = korisnik.Korisnik.SlikaKorisnika;
            if (id == "0")
            {
                korisnik.Indikator = false;
                return View(korisnik);
            }
            else
            {


                korisnik.Korisnik = db.Users.Where(s => s.Id == id).FirstOrDefault();
                korisnik.Indikator = true;
                return View(korisnik);
            }
        }
        [HttpPost]
        public  ActionResult DodajEdit([Bind(Exclude = "slika")]KorisnikIzm kor)
        {

            if (kor.Registracija != null)
            {
                byte[] imageData = null;

                HttpPostedFileBase objFiles = Request.Files["slika"];

                using (var binaryReader = new BinaryReader(objFiles.InputStream))
                {
                    imageData = binaryReader.ReadBytes(objFiles.ContentLength);
                }
                if(imageData.Length==0)
                {
                    string imageFile = Path.Combine(Server.MapPath("~/Content/DefaultPictures"), "defaultUser.png");
                    imageData = System.IO.File.ReadAllBytes(imageFile);
                }
                


                var user = new ApplicationUser { Ime = kor.Korisnik.Ime, Prezime = kor.Korisnik.Prezime, UserName = kor.Korisnik.UserName, FKorisnik = true, Email=kor.Korisnik.Email, Opis=kor.Korisnik.Opis, Rejting=(float)2.5, SlikaKorisnika= imageData };
                var result = UserManager.Create(user, kor.Registracija.Password);
            AddErrors(result);
            if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Korisnik");
                    return RedirectToAction("SviKorisnici");
                }
           
                return RedirectToAction("SviKorisnici");

            }
            
            else
            {
                byte[] imageData = null;

                HttpPostedFileBase objFiles = Request.Files["slika"];

                using (var binaryReader = new BinaryReader(objFiles.InputStream))
                {
                    imageData = binaryReader.ReadBytes(objFiles.ContentLength);
                }
                if(imageData.Length==0)
                {
                    //kor.Korisnik.SlikaKorisnika = kor.Slika;
                    db.Entry(kor.Korisnik).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                else
                {
                    kor.Korisnik.SlikaKorisnika = imageData;
                    db.Entry(kor.Korisnik).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                return RedirectToAction("SviKorisnici");



                //return Json(new { success = true, message = "Izmenjen termin", JsonRequestBehavior.AllowGet });
            }
        }

        public ActionResult DodajEditUpravnik(string id = "0")
        {
            var korisnik = new KorisnikIzm();
            
            if (id == "0")
            {
                korisnik.Indikator = false;
                return View(korisnik);
            }
            else
            {
                korisnik.Korisnik = db.Users.Where(s => s.Id == id).FirstOrDefault();
                korisnik.Slika = korisnik.Korisnik.SlikaKorisnika;
                korisnik.Indikator = true;
                return View(korisnik);
            }
        }
        [HttpPost]
        public ActionResult DodajEditUpravnik([Bind(Exclude = "slika")]KorisnikIzm kor)
        {

            if (kor.Registracija != null)
            {
                byte[] imageData = null;

                HttpPostedFileBase objFiles = Request.Files["slika"];

                using (var binaryReader = new BinaryReader(objFiles.InputStream))
                {
                    imageData = binaryReader.ReadBytes(objFiles.ContentLength);
                }
                if (imageData.Length == 0)
                {
                    string imageFile = Path.Combine(Server.MapPath("~/Content/DefaultPictures"), "defaultUser.png");
                    imageData = System.IO.File.ReadAllBytes(imageFile);
                }


                var user = new ApplicationUser { Ime = kor.Korisnik.Ime, Prezime = kor.Korisnik.Prezime, UserName = kor.Korisnik.UserName, FUpravnikSale = true, Email = kor.Korisnik.Email, Opis = kor.Korisnik.Opis, Rejting = kor.Korisnik.Rejting, SlikaKorisnika = imageData };
                var result = UserManager.Create(user, kor.Registracija.Password);
                AddErrors(result);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Upravnik sale");
                    return RedirectToAction("SviUpravnici");
                }

                return RedirectToAction("SviUpravnici");

            }

            else
            {
                byte[] imageData = null;

                HttpPostedFileBase objFiles = Request.Files["slika"];

                using (var binaryReader = new BinaryReader(objFiles.InputStream))
                {
                    imageData = binaryReader.ReadBytes(objFiles.ContentLength);
                }
                if (imageData.Length == 0)
                {
                    //kor.Korisnik.SlikaKorisnika = kor.Slika;
                    db.Entry(kor.Korisnik).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    kor.Korisnik.SlikaKorisnika = imageData;
                    db.Entry(kor.Korisnik).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("SviUpravnici");



                //return Json(new { success = true, message = "Izmenjen termin", JsonRequestBehavior.AllowGet });
            }
        }
        
        public ActionResult Obrisi(string id) // brisanje za upravnika
        {
            
            var kor = new ApplicationUser();
            kor.Id = id;
            return View(kor);
        }
        

        [HttpPost]
        public ActionResult Obrisi(ApplicationUser model) // brisanje za upravnika
        {
            
            var korisnik = db.Users.Where(s => s.Id == model.Id).FirstOrDefault();
            var sale = db.Sale.Where(s => s.UpravnikId == model.Id).ToList();
            foreach(var sala in sale)
            {
                var termini = db.Termini.Where(t => t.SalaId == sala.Id.ToString()).ToList();
                foreach (var t in termini)
                {
                    var korisnikTermins = db.KorisnikTermini.Where(kt => kt.TerminId == t.Id).ToList();
                    foreach (var kt in korisnikTermins)
                    {
                        db.KorisnikTermini.Remove(kt);
                    }
                    db.Termini.Remove(t);
                }
                db.Sale.Remove(sala);
            }

            db.Users.Remove(korisnik);
            db.SaveChanges();
            return RedirectToAction("SviUpravnici");
        }
        public ActionResult ObrisiKorisnik(string id) 
        {
            var kor = new ApplicationUser();
            kor.Id = id;
            return View(kor);
        }
        [HttpPost]
        public ActionResult ObrisiKorisnik(ApplicationUser model) // brisanje za igraca
        {

            var korisnik = db.Users.Where(s => s.Id == model.Id).FirstOrDefault();
            var korisnikTermini = db.KorisnikTermini.Where(k => k.KorisnikId == model.Id ).ToList();
            foreach (var elem in korisnikTermini)
            {
                if (elem == null) { return RedirectToAction("SviKorisnici"); }          
                var termin = db.Termini.Where(t => t.Id == elem.TerminId).FirstOrDefault();
                termin.BrojPrijavljenih--; // da se smanji broj prijavljenih tom terminu
                db.KorisnikTermini.Remove(elem);
            }
            var komentariKorisnika = db.Komentari.Where(k => k.PostavljacId == model.Id || k.PrimalacId == model.Id).ToList(); //one koje je on postavljao i postavljene njemu
            foreach (var komentar in komentariKorisnika)
            {
                db.Komentari.Remove(komentar);
            }
            db.Users.Remove(korisnik);
            db.SaveChanges();
            return RedirectToAction("SviKorisnici");
        }

       
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        public ActionResult IzmeniSalu(int id)
        {
            SalaIzm salaizm = new SalaIzm();
            salaizm.Sala = db.Sale.Where(s => s.Id == id).FirstOrDefault();
            salaizm.Slikapom = salaizm.Sala.Slika;
            return View(salaizm);
        }
        [HttpPost]
        public ActionResult IzmeniSalu([Bind(Exclude = "slika")]SalaIzm model)
        {
            byte[] imageData = null;

            HttpPostedFileBase objFiles = Request.Files["slika"];

            using (var binaryReader = new BinaryReader(objFiles.InputStream))
            {
                imageData = binaryReader.ReadBytes(objFiles.ContentLength);
            }
            if (imageData.Length == 0)
            {
                model.Sala.Slika = model.Slikapom;
                db.Entry(model.Sala).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                model.Sala.Slika = imageData;
                db.Entry(model.Sala).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("SveSale");
        }
        public ActionResult ListaSala()
        {
            return View();
        }
        public ActionResult PribaviSale()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var result = db.Sale.ToList().Join(db.Users.ToList(),
                e => e.UpravnikId, d => d.Id, (sala, upravnik) => new { Naziv = sala.Naziv, Lokacija=sala.Lokacija, Rejting = sala.Rejting, UpravnikIme=upravnik.Ime, UpravnikPrezime=upravnik.Prezime, SalaId=sala.Id });

            return Json(new { data = result, }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListaTermina()
        {
            return View();
        }
        public ActionResult PribaviTermine()
        {
            db.Configuration.LazyLoadingEnabled = false;
            
            var result = db.Sale.ToList().Join(db.Termini.ToList(),
                e => e.Id.ToString(), d => d.SalaId, (sala, termin) => new { Naziv = sala.Naziv, Pocetaktermina = termin.PocetakTermina.ToString(), Krajtermina = termin.KrajTermina.ToString(), BrojPrijavljenih = termin.BrojPrijavljenih, TerminId = termin.Id });

            return Json(new { data = result, }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObrisiSalu(string id)
        {
            int idsale = Int32.Parse(id);
            var sala = db.Sale.Where(s => s.Id == idsale).FirstOrDefault();
            var termini = db.Termini.Where(t => t.SalaId == sala.Id.ToString()).ToList();
            foreach (var t in termini)
            {
                var korisnikTermins = db.KorisnikTermini.Where(kt => kt.TerminId == t.Id).ToList();
                foreach (var kt in korisnikTermins)
                {
                    db.KorisnikTermini.Remove(kt);
                }
                db.Termini.Remove(t);
            }
            db.Sale.Remove(sala);
            db.SaveChanges();
            return RedirectToAction("SveSale");
        }




        
    }
}
