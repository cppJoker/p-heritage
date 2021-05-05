namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eight : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialKeyId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        Published = c.DateTime(nullable: false),
                        starCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.SerialKeys", t => t.SerialKeyId, cascadeDelete: true)
                .Index(t => t.SerialKeyId)
                .Index(t => t.GameId);
            
            AddColumn("dbo.Games", "largeImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "SerialKeyId", "dbo.SerialKeys");
            DropForeignKey("dbo.Reviews", "GameId", "dbo.Games");
            DropIndex("dbo.Reviews", new[] { "GameId" });
            DropIndex("dbo.Reviews", new[] { "SerialKeyId" });
            DropColumn("dbo.Games", "largeImagePath");
            DropTable("dbo.Reviews");
        }
    }
}
