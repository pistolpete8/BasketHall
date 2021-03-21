namespace KosSala.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Termins", "Sala_Id", "dbo.Salas");
            DropIndex("dbo.Termins", new[] { "Sala_Id" });
            AddColumn("dbo.Termins", "SalaId", c => c.String());
            DropColumn("dbo.Termins", "Sala_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Termins", "Sala_Id", c => c.Int());
            DropColumn("dbo.Termins", "SalaId");
            CreateIndex("dbo.Termins", "Sala_Id");
            AddForeignKey("dbo.Termins", "Sala_Id", "dbo.Salas", "Id");
        }
    }
}
