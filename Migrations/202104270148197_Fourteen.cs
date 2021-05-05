namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Theme", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Theme");
        }
    }
}
