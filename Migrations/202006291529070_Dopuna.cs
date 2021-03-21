namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dopuna : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Salas", "Naziv", c => c.String(nullable: false));
            AlterColumn("dbo.Salas", "Lokacija", c => c.String(nullable: false));
            AlterColumn("dbo.Salas", "Slika", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Salas", "Slika", c => c.Binary());
            AlterColumn("dbo.Salas", "Lokacija", c => c.String());
            AlterColumn("dbo.Salas", "Naziv", c => c.String());
        }
    }
}
