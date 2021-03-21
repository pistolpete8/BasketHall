namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlikaKorisnik : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "SlikaKorisnika", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "SlikaKorisnika");
        }
    }
}
