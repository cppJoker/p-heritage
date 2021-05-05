namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Six : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SerialKeys", "GameID", "dbo.Games");
            DropIndex("dbo.SerialKeys", new[] { "GameID" });
            AlterColumn("dbo.SerialKeys", "GameID", c => c.Int());
            CreateIndex("dbo.SerialKeys", "GameID");
            AddForeignKey("dbo.SerialKeys", "GameID", "dbo.Games", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerialKeys", "GameID", "dbo.Games");
            DropIndex("dbo.SerialKeys", new[] { "GameID" });
            AlterColumn("dbo.SerialKeys", "GameID", c => c.Int(nullable: false));
            CreateIndex("dbo.SerialKeys", "GameID");
            AddForeignKey("dbo.SerialKeys", "GameID", "dbo.Games", "Id", cascadeDelete: true);
        }
    }
}
