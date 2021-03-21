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
using Microsoft.AspNet.Identity;
using KosSala.ViewModels;

namespace KosSala.Controllers
{
    public class KorisnikController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        [CustomAuthorize(Roles = "Korisnik")]
        public  ActionResult Index()
        {
            
            return View();
        }

        public ActionResult TerminInfo(SaleView model,int id)
        {


            var termin = db.Termini.Where(t => t.Id == id).FirstOrDefault();
            var sala = db.Sale.Where(s => s.Id.ToString() == termin.SalaId).FirstOrDefault();
            // var terminKorisnik = db.KorisnikTermini.Where(t => t.Termin.Id == termin.Id).ToList();
           //string idUpravnika = sala.UpravnikId;
            
            ApplicationUser menager = db.Users.Where(a => a.Id == sala.UpravnikId).FirstOrDefault();

            model.UpravnikSale =menager;

            string ID = User.Identity.GetUserId();
            model.LoggedUserId = ID;
            model.Sala = sala;
            model.Termin = termin;

            List<ApplicationUser> igraci = db.Users.ToList();
            List<KorisnikTermin> igracTermin = db.KorisnikTermini.Where(t => t.TerminId == termin.Id).ToList();

            var result = from i in igraci
                         join t in igracTermin on i.Id equals t.KorisnikId into table1
                         from t in table1.ToList()

                         select new SaleView
                         {
                             Igrac = i,
                             KorisnikTermin = t,

                         };
            List<ApplicationUser> players= new List<ApplicationUser>();
            foreach ( var item in result)
            {
                ApplicationUser user = db.Users.Where(u => u.Id == item.Igrac.Id).FirstOrDefault();
                players.Add(user);

            }
            model.Players = players;

            KorisnikTermin value = db.KorisnikTermini.Where(o => o.KorisnikId == ID && o.TerminId == id).FirstOrDefault();
            if (value == null)
            {
                TempData["nepridruzen"] = "nepridruzen";
            }


            return View(model);
        }

        public ActionResult Termini( SaleView model)
        {

            DateTime pre2Meseca = DateTime.Now.AddDays(-60);

            List<Sala> sale = db.Sale.ToList();
            List<Termin> termini = db.Termini.Where(date=>date.PocetakTermina > pre2Meseca ).OrderByDescending(date => date.PocetakTermina).ToList(); // treba da se dodada where slobodan ==false ili da je veci od DateTime.Now

            
            var result = from s in sale
                         join t in termini on s.Id.ToString() equals t.SalaId into table1
                         from t in table1.ToList()
                         
                        select new SaleView
                                     {
                                         Sala = s,
                                         Termin = t,
                                         initialFlag=true
                                        
                                     };
            
            return View(result);
            

            //return View(model);
        }

        public ActionResult PridruziSeTerminu(int id)
        {
            string ID = User.Identity.GetUserId();
            var korisnik = db.Users.Where(user => user.Id == ID).FirstOrDefault();
            var termin = db.Termini.Where(ter => ter.Id == id).FirstOrDefault();
            if (termin != null && korisnik != null)
            {
                KorisnikTermin novi = new KorisnikTermin { KorisnikId = korisnik.Id, TerminId = termin.Id };
                db.KorisnikTermini.Add(novi);
                if (termin.VrstaBasketa == "3 na 3" || termin.VrstaBasketa == "3v3")
                {
                    if ((termin.BrojPrijavljenih + 1) <= 6)
                    {
                        termin.BrojPrijavljenih++;
                        if (termin.BrojPrijavljenih == 6)
                        {
                            termin.Slobodan = false;
                            TempData["pun"] = "pun";
                        }
                    }


                }
                if (termin.VrstaBasketa == "5 na 5" || termin.VrstaBasketa == "5v5")
                {
                    if ((termin.BrojPrijavljenih + 1) <= 10)
                    {
                        termin.BrojPrijavljenih++;
                        if (termin.BrojPrijavljenih == 10)
                        {
                            termin.Slobodan = false;
                            TempData["pun"] = "pun";
                        }
                    }


                }
                if (termin.VrstaBasketa == "4 na 4" || termin.VrstaBasketa == "4v4")
                {
                    if ((termin.BrojPrijavljenih + 1) <= 8)
                    {
                        termin.BrojPrijavljenih++;
                        if (termin.BrojPrijavljenih == 8)
                        {
                            termin.Slobodan = false;
                            TempData["pun"] = "pun";
                        }
                    }


                }

                db.SaveChanges();

            }






            return RedirectToAction("TerminInfo", new { id = id });

        }
        
        public ActionResult Odustani(int id)
        {
            string ID = User.Identity.GetUserId();
            var korisnik = db.Users.Where(user => user.Id == ID).FirstOrDefault();
            var termin = db.Termini.Where(ter => ter.Id == id).FirstOrDefault();

            var korisniktermin = db.KorisnikTermini.Where(te => te.KorisnikId == korisnik.Id).FirstOrDefault();
            db.KorisnikTermini.Remove(korisniktermin);
            termin.BrojPrijavljenih--;
            if(termin.Slobodan==false)
            {
                termin.Slobodan = true;
            }
            db.SaveChanges();

            return RedirectToAction("TerminInfo", new { id = id });

        }

        public ActionResult GetKorisnici()
        {
            string idkor = User.Identity.GetUserId();
            db.Configuration.LazyLoadingEnabled = false;
            var Korisnici = db.Users.Where(s => s.FKorisnik == true && s.Id!= idkor).ToList();
            foreach ( ApplicationUser a in Korisnici)
            {
                var base64 = Convert.ToBase64String(a.SlikaKorisnika);
                var imgsrc = string.Format("data:image/png;base64,{0}", base64);
                a.SlikaString = imgsrc;
            }
            return Json(new { data = Korisnici }, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult KorisnikSignedUp(string id)
        {
            ViewBag.Message = "Your contact page.";
            var model = new KorisnikSl();
            string ID = User.Identity.GetUserId();
            var user = db.Users.Where(s => s.Id == ID).FirstOrDefault();
            model.Korisnik = user;
            byte[] picture = user.SlikaKorisnika;
            model.Slika = picture;

            // deo za prosecne ocene // niz (prvi element je broj korisnika, nulti: broj sa 5 zvezdia, prvi: broj sa 4 itd..
            var Korisnici0 = db.Users.Where(s => s.FKorisnik == true).OrderByDescending(s => s.Rejting).ToList();
            int broj = db.Users.Where(s => s.FKorisnik == true).Count();

            int[] niz = { broj, 0, 0, 0 ,0, 0};
            
            
            foreach( var korisnik in Korisnici0)
            {
                if(korisnik.Rejting>=5)
                {
                   niz[1]++;
                }
                if(korisnik.Rejting>=4 && korisnik.Rejting<=5)
                {
                    niz[2]++;
                }
                if (korisnik.Rejting >=3 && korisnik.Rejting <= 4)
                {
                    niz[3]++;
                }
                if (korisnik.Rejting >= 2 && korisnik.Rejting <= 3)
                {
                    niz[4]++;
                }
                if (korisnik.Rejting >= 1 && korisnik.Rejting <= 2)
                {
                    niz[5]++;
                }

            }
            float suma = 0;
            foreach (var korisnik in Korisnici0)
            {
                suma = suma + korisnik.Rejting;

            }
            model.prosecanRejting =(float)Math.Round((suma/broj), 2, MidpointRounding.ToEven);
            model.brojeviPoOcenama = niz;

            // deo za top 5 players
            var Korisnici = db.Users.Where(s => s.FKorisnik == true).OrderByDescending(s=>s.Rejting).ToList().Take(5);
            model.Igraci = Korisnici;

            


            return View(model);
        }
        public async Task<ActionResult> Profile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var model = new KomentariView();
            model.Korinsnici = db.Users.Where(s => s.Id == id).ToList();
            model.Komentari = db.Komentari.Where(s=>s.PrimalacId==id).OrderByDescending(s=>s.Datum_Postavljanja).ToList();
            model.idPrimaoca = id;
            model.idUser = User.Identity.GetUserId();
            string posId = model.idUser;

            Ocene ocena = db.Ocene.Where(o => o.OcenjenId == id && o.OcenjivacId == posId).FirstOrDefault();
            if (ocena==null)
            {
                TempData["neocenjen"] = "neocenjen";
            }
            
            
           
            if (model.Korinsnici == null)
            {
                return HttpNotFound();
            }
            return View(model);


        }
        public ActionResult Oceni( string id)
        {
            var model = new KomentariView();
            model.idPrimaoca = id;



            return View(model);

        }
        [HttpPost]
        public ActionResult Oceni(KomentariView model)
        {
            if (model.valueRadio != 0)
           {

                var kor = db.Users.Where(s => s.Id == model.idPrimaoca).FirstOrDefault();
                kor.BrojOcena++;
                kor.Rejting = (((float)kor.AccessFailedCount + (float)model.valueRadio) / (float)kor.BrojOcena);
                kor.AccessFailedCount = (kor.AccessFailedCount + model.valueRadio);
            string posId = User.Identity.GetUserId();
            var ocena = new Ocene { OcenjenId = kor.Id, OcenjivacId = posId };
             db.Ocene.Add(ocena);
                
                db.SaveChanges();
                return RedirectToAction("Profile", new { id = model.idPrimaoca });
            }
           else
            {
                return RedirectToAction("Profile", new { id = model.idPrimaoca });
            }

               

        }

        public  ActionResult Komentarisi(KomentariView model, string id)
            {
            if (model.Komentar != null)
            {
                string posId = User.Identity.GetUserId();
                string ime = User.Identity.Name;

               
                var postavljac = db.Users.Where(s => s.Id == posId).FirstOrDefault();
                byte[] picture = postavljac.SlikaKorisnika;

                var komentar = new Komentari { Komentar = model.Komentar, PostavljacId = posId, PrimalacId = model.idPrimaoca, ImePostavljaca = ime, SlikaPostavljaca = picture, Datum_Postavljanja = DateTime.Now };
                db.Komentari.Add(komentar);
                db.SaveChanges();
            }
            
            //var provera =  db.Komentari.Where(s => s.Komentar == model.Komentar).FirstOrDefault();
            return RedirectToAction("Profile", new { id = model.idPrimaoca });
            //return View(model);
            /*if (provera == null)
                return View(model);
            else
            {
                
                return RedirectToAction("Profile", new { id = model.idPrimaoca});
            }*/
        }

        public ActionResult SveSale()
        {

            var tmp = db.Sale.ToList();
            
            foreach(var item in tmp)
            {
                var base64 = Convert.ToBase64String(item.Slika);
                var imgsrc = string.Format("data:image/png;base64,{0}", base64);
                item.Opis = imgsrc;
            }

            db.Configuration.LazyLoadingEnabled = false;
           // var result = db.Users.Where(s=>s.FUpravnikSale==true).ToList().Join(tmp,
               // e => e.Id.ToString(), d => d.UpravnikId, ( upravnik, sala) => new { Naziv = sala.Naziv, Lokacija=sala.Lokacija,Rejting=sala.Rejting, ImeUpravnika=upravnik.Ime, PrezimeUpravnika=upravnik.Prezime,SlikaSale=sala.Slika,SlikaString=sala.Opis, SalaId=sala.Id });

            db.Configuration.LazyLoadingEnabled = false;
            var upravnici = db.Users.Where(us => us.FUpravnikSale == true).ToList();
            var result = tmp.Join(upravnici,
                s => s.UpravnikId.ToString(), u => u.Id, (sala, upravnik) => new { Naziv = sala.Naziv, Lokacija = sala.Lokacija, Rejting = sala.Rejting, ImeUpravnika = upravnik.Ime, PrezimeUpravnika = upravnik.Prezime, SlikaSale = sala.Slika, SlikaString = sala.Opis, SalaId = sala.Id });

            return Json(new { data = result, }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DostupneSale()
        {
            return View();
        }
        public ActionResult SalaProfile(int id)
        {
            var model = new SalaProfile();
            var sala = db.Sale.Where(s => s.Id == id).FirstOrDefault();
            var upravnik = db.Users.Where(s => s.Id == sala.UpravnikId).FirstOrDefault();
            model.Sala = sala;
            string primalacid = id.ToString();
            model.Komentari = db.Komentari.Where(s => s.PrimalacId == primalacid).OrderByDescending(s => s.Datum_Postavljanja).ToList();
            model.IdPrimaoca = primalacid;
            model.Upravnik = upravnik;
            string posId = User.Identity.GetUserId();
            Ocene ocena = db.Ocene.Where(o => o.SalaId == id && o.OcenjivacId == posId).FirstOrDefault();
            if (ocena == null)
            {
                TempData["neocenjena"] = "neocenjena";
            }

            return View(model);
        }

        public ActionResult KomentarisiSalu(SalaProfile model)
        {
            if (model.Komentar != null)
            {
                string posId = User.Identity.GetUserId();
                string ime = User.Identity.Name;


                var postavljac = db.Users.Where(s => s.Id == posId).FirstOrDefault();
                byte[] picture = postavljac.SlikaKorisnika;

                var komentar = new Komentari { Komentar = model.Komentar, PostavljacId = posId, PrimalacId = model.IdPrimaoca, ImePostavljaca = ime, SlikaPostavljaca = picture, Datum_Postavljanja = DateTime.Now };
                db.Komentari.Add(komentar);
                db.SaveChanges();
            }
            return RedirectToAction("SalaProfile", new { id = model.IdPrimaoca });
            /*var provera = db.Komentari.Where(s => s.Komentar == model.Komentar).FirstOrDefault();
            if (provera == null)
                return View(model);
            else
            {

                return RedirectToAction("SalaProfile", new { id = model.IdPrimaoca });
            }*/
        }

        public ActionResult OceniSalu(SalaProfile model)
        {
             if (model.valueRadio != 0)
            {
            int salaid = Int32.Parse(model.IdPrimaoca);
            var sala = db.Sale.Where(s => s.Id == salaid).FirstOrDefault();
               
            sala.BrojOcena++;
            sala.Rejting = (((float)sala.SumaOcena + (float)model.valueRadio) / (float)sala.BrojOcena);
            sala.SumaOcena = (sala.SumaOcena + model.valueRadio);

            string posId = User.Identity.GetUserId();
            var ocena = new Ocene { SalaId = sala.Id, OcenjivacId = posId };
            db.Ocene.Add(ocena);

            db.SaveChanges();
            return RedirectToAction("SalaProfile", new { id = model.IdPrimaoca });
             }
            else
            {
                return RedirectToAction("SalaProfile", new { id = model.IdPrimaoca });
            }

            

        }


    }
   


       
    }


