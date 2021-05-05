namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourteen1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Modules", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Modules");
        }
    }
}
