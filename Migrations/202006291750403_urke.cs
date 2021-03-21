namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urke : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Salas", "Naziv", c => c.String());
            AlterColumn("dbo.Salas", "Lokacija", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Salas", "Lokacija", c => c.String(nullable: false));
            AlterColumn("dbo.Salas", "Naziv", c => c.String(nullable: false));
        }
    }
}
