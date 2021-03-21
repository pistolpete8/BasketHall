namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzmeneKorisnikTermin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KorisnikTermins", "Korisnik_Id", "dbo.Account");
            DropForeignKey("dbo.KorisnikTermins", "Termin_Id", "dbo.Termins");
            DropIndex("dbo.KorisnikTermins", new[] { "Korisnik_Id" });
            DropIndex("dbo.KorisnikTermins", new[] { "Termin_Id" });
            AddColumn("dbo.KorisnikTermins", "KorisnikId", c => c.String());
            AddColumn("dbo.KorisnikTermins", "TerminId", c => c.Int(nullable: false));
            DropColumn("dbo.KorisnikTermins", "Korisnik_Id");
            DropColumn("dbo.KorisnikTermins", "Termin_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KorisnikTermins", "Termin_Id", c => c.Int());
            AddColumn("dbo.KorisnikTermins", "Korisnik_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.KorisnikTermins", "TerminId");
            DropColumn("dbo.KorisnikTermins", "KorisnikId");
            CreateIndex("dbo.KorisnikTermins", "Termin_Id");
            CreateIndex("dbo.KorisnikTermins", "Korisnik_Id");
            AddForeignKey("dbo.KorisnikTermins", "Termin_Id", "dbo.Termins", "Id");
            AddForeignKey("dbo.KorisnikTermins", "Korisnik_Id", "dbo.Account", "Id");
        }
    }
}
