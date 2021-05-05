namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Five : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SerialKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Activated = c.Boolean(nullable: false),
                        GameID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameID, cascadeDelete: true)
                .Index(t => t.GameID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerialKeys", "GameID", "dbo.Games");
            DropIndex("dbo.SerialKeys", new[] { "GameID" });
            DropTable("dbo.SerialKeys");
        }
    }
}
