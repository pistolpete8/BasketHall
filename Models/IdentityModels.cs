using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KosSala.Models
{
    
    public class ApplicationUser : IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public bool FKorisnik { get; set; }
        public int BrojOcena { get; set; }
        public string Opis { get; set; }
        
        public float Rejting { get; set; }

        public bool FUpravnikSale { get; set; }
        
        public bool FAdministrator { get; set; }
        public byte[] SlikaKorisnika { get; set; }
        public string SlikaString { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Sala> Sale { get; set; }
        public DbSet<Termin> Termini { get; set; }
        public DbSet<Komentari> Komentari { get; set; }
        public DbSet<KorisnikTermin> KorisnikTermini { get; set; }

        public DbSet<Ocene> Ocene { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Account");
            
        }

        
    }
}