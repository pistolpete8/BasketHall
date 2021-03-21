namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PromenaUpravnikId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Salas", "Upravnik_Id", "dbo.Account");
            DropIndex("dbo.Salas", new[] { "Upravnik_Id" });
            AddColumn("dbo.Salas", "UpravnikId", c => c.String());
            DropColumn("dbo.Salas", "Upravnik_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Salas", "Upravnik_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Salas", "UpravnikId");
            CreateIndex("dbo.Salas", "Upravnik_Id");
            AddForeignKey("dbo.Salas", "Upravnik_Id", "dbo.Account", "Id");
        }
    }
}
