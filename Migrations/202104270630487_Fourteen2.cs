namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourteen2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "resourcePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "resourcePath");
        }
    }
}
