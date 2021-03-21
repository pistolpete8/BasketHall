namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Komentari : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Account", "Komentari");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "Komentari", c => c.String());
        }
    }
}
