namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eleven : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reviews", "starCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "starCount", c => c.Int(nullable: false));
        }
    }
}
