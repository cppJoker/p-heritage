namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixTeen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlatformSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainTitle = c.String(),
                        MainTitleIcon = c.String(),
                        MainSubTitle = c.String(),
                        MainDescription = c.String(),
                        HandInDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlatformSettings");
        }
    }
}
