namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoviProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Komentaris", "noviProperti", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Komentaris", "noviProperti");
        }
    }
}
