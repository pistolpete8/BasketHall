namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dopuna1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Salas", "Slika", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Salas", "Slika", c => c.Binary(nullable: false));
        }
    }
}
