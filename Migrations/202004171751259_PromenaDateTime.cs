namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PromenaDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Termins", "PocetakTermina", c => c.DateTime());
            AlterColumn("dbo.Termins", "KrajTermina", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Termins", "KrajTermina", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Termins", "PocetakTermina", c => c.DateTime(nullable: false));
        }
    }
}
