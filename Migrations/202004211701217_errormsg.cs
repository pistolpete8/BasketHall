namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errormsg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Termins", "SalaId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Termins", "SalaId", c => c.String());
        }
    }
}
