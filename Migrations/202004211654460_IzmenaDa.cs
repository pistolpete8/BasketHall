namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzmenaDa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Termins", "PocetakTermina", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Termins", "KrajTermina", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Termins", "KrajTermina", c => c.DateTime());
            AlterColumn("dbo.Termins", "PocetakTermina", c => c.DateTime());
        }
    }
}
