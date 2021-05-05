namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seven : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "DatePublished", c => c.DateTime(nullable: false));
            AddColumn("dbo.SerialKeys", "CanEdit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SerialKeys", "CanEdit");
            DropColumn("dbo.Games", "DatePublished");
        }
    }
}
