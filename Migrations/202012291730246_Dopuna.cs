namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dopuna : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KorisnikTermins", "Korisnik_Id", "dbo.Account");
            DropForeignKey("dbo.KorisnikTermins", "Termin_Id", "dbo.Termins");
            DropIndex("dbo.KorisnikTermins", new[] { "Korisnik_Id" });
            DropIndex("dbo.KorisnikTermins", new[] { "Termin_Id" });
            CreateTable(
                "dbo.Ocenes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OcenjivacId = c.String(),
                        OcenjenId = c.String(),
                        SalaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Komentaris", "SlikaPostavljaca", c => c.Binary());
            AddColumn("dbo.Komentaris", "Datum_Postavljanja", c => c.DateTime(nullable: false));
            AddColumn("dbo.Komentaris", "noviProperti", c => c.String());
            AddColumn("dbo.KorisnikTermins", "KorisnikId", c => c.String());
            AddColumn("dbo.KorisnikTermins", "TerminId", c => c.Int(nullable: false));
            AddColumn("dbo.Account", "SlikaString", c => c.String());
            AddColumn("dbo.Salas", "BrojOcena", c => c.Int(nullable: false));
            AddColumn("dbo.Salas", "SumaOcena", c => c.Int(nullable: false));
            AlterColumn("dbo.Termins", "VrstaBasketa", c => c.String(nullable: false));
            DropColumn("dbo.KorisnikTermins", "Korisnik_Id");
            DropColumn("dbo.KorisnikTermins", "Termin_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KorisnikTermins", "Termin_Id", c => c.Int());
            AddColumn("dbo.KorisnikTermins", "Korisnik_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Termins", "VrstaBasketa", c => c.String());
            DropColumn("dbo.Salas", "SumaOcena");
            DropColumn("dbo.Salas", "BrojOcena");
            DropColumn("dbo.Account", "SlikaString");
            DropColumn("dbo.KorisnikTermins", "TerminId");
            DropColumn("dbo.KorisnikTermins", "KorisnikId");
            DropColumn("dbo.Komentaris", "noviProperti");
            DropColumn("dbo.Komentaris", "Datum_Postavljanja");
            DropColumn("dbo.Komentaris", "SlikaPostavljaca");
            DropTable("dbo.Ocenes");
            CreateIndex("dbo.KorisnikTermins", "Termin_Id");
            CreateIndex("dbo.KorisnikTermins", "Korisnik_Id");
            AddForeignKey("dbo.KorisnikTermins", "Termin_Id", "dbo.Termins", "Id");
            AddForeignKey("dbo.KorisnikTermins", "Korisnik_Id", "dbo.Account", "Id");
        }
    }
}
