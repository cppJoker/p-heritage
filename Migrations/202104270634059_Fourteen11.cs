namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourteen11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "yearContent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "yearContent", c => c.String());
        }
    }
}
