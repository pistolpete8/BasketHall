namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrvaMigracija : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Salas", t => t.Sala_Id)
                .Index(t => t.Sala_Id);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Ime = c.String(),
                        Prezime = c.String(),
                        FKorisnik = c.Boolean(nullable: false),
                        Opis = c.String(),
                        Rejting = c.Single(nullable: false),
                        Komentari = c.String(),
                        FUpravnikSale = c.Boolean(nullable: false),
                        FAdministrator = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Sala_Id = c.Int(),
                        Grupa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Salas", t => t.Sala_Id)
                .ForeignKey("dbo.Grupas", t => t.Grupa_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Sala_Id)
                .Index(t => t.Grupa_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Account", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Account", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Lokacija = c.String(),
                        Rejting = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Termins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PocetakTermina = c.DateTime(nullable: false),
                        KrajTermina = c.DateTime(nullable: false),
                        Slobodan = c.Boolean(nullable: false),
                        Sala_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Salas", t => t.Sala_Id)
                .Index(t => t.Sala_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Grupas", "Sala_Id", "dbo.Salas");
            DropForeignKey("dbo.Account", "Grupa_Id", "dbo.Grupas");
            DropForeignKey("dbo.Account", "Sala_Id", "dbo.Salas");
            DropForeignKey("dbo.Termins", "Sala_Id", "dbo.Salas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Account");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Account");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Account");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Termins", new[] { "Sala_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Account", new[] { "Grupa_Id" });
            DropIndex("dbo.Account", new[] { "Sala_Id" });
            DropIndex("dbo.Account", "UserNameIndex");
            DropIndex("dbo.Grupas", new[] { "Sala_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Termins");
            DropTable("dbo.Salas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Account");
            DropTable("dbo.Grupas");
        }
    }
}
