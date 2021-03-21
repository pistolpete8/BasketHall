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
using System.IO;
using System.IO.IsolatedStorage;

namespace KosSala.Controllers
{
    public class UpravnikSaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public ActionResult UpravnikSaleSignedUp(string id)
        {
          
            var model = new KorisnikSl();
            string ID = User.Identity.GetUserId();
            var user = db.Users.Where(s => s.Id == ID).FirstOrDefault();
            model.Korisnik = user;
            byte[] picture = user.SlikaKorisnika;
            model.Slika = picture;


            return View(model);

           
        }
        [HttpGet]
        public ActionResult DodajSalu()
        {
                return View();

        }
        [HttpPost]
        public ActionResult DodajSalu([Bind(Exclude = "slika")]Sala model)
        {
            /*  if(slika!=null)
              {
                  model.Slika = new byte[slika.ContentLength];
                  slika.InputStream.Read(model.Slika, 0, slika.ContentLength);
              }
              string upravnikId = User.Identity.GetUserId();
              model.UpravnikId = upravnikId;
              db.Sale.Add(model);
              db.SaveChangesAsync();
              return RedirectToAction("PrikazSala");*/

            byte[] imageData = null;
            
                HttpPostedFileBase objFiles = Request.Files["slika"];

                using (var binaryReader = new BinaryReader(objFiles.InputStream))
                {
                    imageData = binaryReader.ReadBytes(objFiles.ContentLength);
                }
            if (imageData.Length!=0)
            {
                model.Slika = imageData;
                }
            else
            {
                

                string imageFile = Path.Combine(Server.MapPath("~/Content/DefaultPictures"), "defaultSala.png");
                byte[] buffer = System.IO.File.ReadAllBytes(imageFile);
                model.Slika = buffer;
            }
                string upravnikId = User.Identity.GetUserId();
                model.UpravnikId = upravnikId;
            model.Rejting = 2.5;
            
                db.Sale.Add(model);
                db.SaveChanges();
                return RedirectToAction("PrikazivanjeSala");
            

        }

        public ActionResult PrikazivanjeSala()
        {
            string upravnikId = User.Identity.GetUserId();
            SaleView sale = new SaleView();
            sale.Sale = db.Sale.Where(s => s.UpravnikId == upravnikId).ToList();
            return View(sale);
        }

        public ActionResult PrikazTermina()
        {
            string upravnikId = User.Identity.GetUserId();
            var pom = new SaleView();
             pom.Sale= db.Sale.Where(s => s.UpravnikId == upravnikId);
            
            return View(pom);
        }
        public ActionResult getSale()
        {
            
            db.Configuration.LazyLoadingEnabled = false;
            string upravnikId = User.Identity.GetUserId();
            var result = db.Sale.Where(s => s.UpravnikId == upravnikId).ToList().Join(db.Termini.Where(t=>t.PocetakTermina>=DateTime.Now).ToList(),
                e => e.Id.ToString(), d => d.SalaId, (sala, termin) => new { Naziv = sala.Naziv, Pocetaktermina = termin.PocetakTermina.ToString(), Krajtermina = termin.KrajTermina.ToString(), BrojPrijavljenih = termin.BrojPrijavljenih ,TerminId=termin.Id});

            return Json(new { data = result, }, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult DodajEdit(int id = 0)
        {
            var termin = new TerminiView();
            termin.VrstaBasketa = new List<SelectListItem>();
            termin.VrstaBasketa.Add(new SelectListItem() { Text = "3 na 3", Value = "3 na 3" });
            termin.VrstaBasketa.Add(new SelectListItem() { Text = "4 na 4", Value = "4 na 4" });
            termin.VrstaBasketa.Add(new SelectListItem() { Text = "5 na 5", Value = "5 na 5" });

            if (id==0)
            { 
            
            string upravnikId = User.Identity.GetUserId();
            termin.Sale = db.Sale.Where(s => s.UpravnikId == upravnikId).ToList();
            termin.listaSala = new List<SelectListItem>();
            
            termin.indikator = false;
            foreach(var item in termin.Sale)
            {
                termin.listaSala.Add(new SelectListItem() { Text = item.Naziv, Value = item.Id.ToString() });
            }
                
                return View(termin);
            }
            else
            {
                termin.indikator = true;
                termin.Termin = db.Termini.Where(s => s.Id == id).FirstOrDefault();
                return View(termin);
            }
        }
        [HttpPost]
        public ActionResult DodajEdit(TerminiView ter)
        {

            if (ter.Termin.Id == 0)
            {
                
                ter.Termin.Slobodan = true;
                db.Termini.Add(ter.Termin);
                
                db.SaveChanges();
                //return Json(new { success = true, message = "Sacuvan termin", JsonRequestBehavior.AllowGet });
                return RedirectToAction("PrikazTermina");
            }
            else
            {
                ter.Termin.Slobodan = true;
                db.Entry(ter.Termin).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("PrikazTermina");
                //return Json(new { success = true, message = "Izmenjen termin", JsonRequestBehavior.AllowGet });
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
            if (imageData.Length ==0 )
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

            return RedirectToAction("PrikazivanjeSala");
        }
       

        

        public ActionResult Obrisi(int id) // brisanje termina kaskadno sa svim elementima u korisnikTermins
        {
            var termin = db.Termini.Where(s => s.Id == id).FirstOrDefault();
            
            var korisnikTermins = db.KorisnikTermini.Where(kt => kt.TerminId == termin.Id).ToList();
            foreach(var kt in korisnikTermins)
            {
                db.KorisnikTermini.Remove(kt);
            }
            db.Termini.Remove(termin);
            db.SaveChanges();
            return Json(new { success = true, message="Uspešno ste obrisali termin",JsonRequestBehavior.AllowGet });
        }

        public ActionResult ObrisiSalu(string id) //brisanje sale -> brisanje termina->brisanje svih KorisnikTermins
        {
            int idsale = Int32.Parse(id);
            var sala = db.Sale.Where(s => s.Id == idsale).FirstOrDefault();
            var termini = db.Termini.Where(t => t.SalaId == sala.Id.ToString()).ToList();
            foreach( var t in termini)
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
            return RedirectToAction("PrikazivanjeSala");
        }


    }
}
