namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SevenTeen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Stars", c => c.Int(nullable: true, defaultValue: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Stars");
        }
    }
}
