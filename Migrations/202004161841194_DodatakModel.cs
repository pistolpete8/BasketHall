namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodatakModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Account", "Sala_Id", "dbo.Salas");
            DropForeignKey("dbo.Account", "Grupa_Id", "dbo.Grupas");
            DropForeignKey("dbo.Grupas", "Sala_Id", "dbo.Salas");
            DropIndex("dbo.Grupas", new[] { "Sala_Id" });
            DropIndex("dbo.Account", new[] { "Sala_Id" });
            DropIndex("dbo.Account", new[] { "Grupa_Id" });
            CreateTable(
                "dbo.KorisnikTermins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Korisnik_Id = c.String(maxLength: 128),
                        Termin_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Korisnik_Id)
                .ForeignKey("dbo.Termins", t => t.Termin_Id)
                .Index(t => t.Korisnik_Id)
                .Index(t => t.Termin_Id);
            
            AddColumn("dbo.Salas", "Upravnik_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Termins", "BrojPrijavljenih", c => c.Int(nullable: false));
            AddColumn("dbo.Termins", "VrstaBasketa", c => c.String());
            CreateIndex("dbo.Salas", "Upravnik_Id");
            AddForeignKey("dbo.Salas", "Upravnik_Id", "dbo.Account", "Id");
            DropColumn("dbo.Account", "Sala_Id");
            DropColumn("dbo.Account", "Grupa_Id");
            DropTable("dbo.Grupas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Grupas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojPrijavljenih = c.Int(nullable: false),
                        VrstaBasketa = c.String(),
                        Sala_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Account", "Grupa_Id", c => c.Int());
            AddColumn("dbo.Account", "Sala_Id", c => c.Int());
            DropForeignKey("dbo.KorisnikTermins", "Termin_Id", "dbo.Termins");
            DropForeignKey("dbo.Salas", "Upravnik_Id", "dbo.Account");
            DropForeignKey("dbo.KorisnikTermins", "Korisnik_Id", "dbo.Account");
            DropIndex("dbo.Salas", new[] { "Upravnik_Id" });
            DropIndex("dbo.KorisnikTermins", new[] { "Termin_Id" });
            DropIndex("dbo.KorisnikTermins", new[] { "Korisnik_Id" });
            DropColumn("dbo.Termins", "VrstaBasketa");
            DropColumn("dbo.Termins", "BrojPrijavljenih");
            DropColumn("dbo.Salas", "Upravnik_Id");
            DropTable("dbo.KorisnikTermins");
            CreateIndex("dbo.Account", "Grupa_Id");
            CreateIndex("dbo.Account", "Sala_Id");
            CreateIndex("dbo.Grupas", "Sala_Id");
            AddForeignKey("dbo.Grupas", "Sala_Id", "dbo.Salas", "Id");
            AddForeignKey("dbo.Account", "Grupa_Id", "dbo.Grupas", "Id");
            AddForeignKey("dbo.Account", "Sala_Id", "dbo.Salas", "Id");
        }
    }
}
