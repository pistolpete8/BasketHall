namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PoboljsanjeKomentara : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Komentaris", "ImePostavljaca", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Komentaris", "ImePostavljaca");
        }
    }
}
