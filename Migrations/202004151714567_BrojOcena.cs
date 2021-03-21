namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrojOcena : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "BrojOcena", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "BrojOcena");
        }
    }
}
