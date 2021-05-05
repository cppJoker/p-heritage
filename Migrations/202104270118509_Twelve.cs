namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Twelve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "SolutionLink", c => c.String(nullable: false));
            AddColumn("dbo.Games", "GuideLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "GuideLink");
            DropColumn("dbo.Games", "SolutionLink");
        }
    }
}
