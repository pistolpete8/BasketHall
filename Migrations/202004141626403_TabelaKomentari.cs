namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaKomentari : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Komentaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostavljacId = c.String(),
                        PrimalacId = c.String(),
                        Komentar = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Komentaris");
        }
    }
}
