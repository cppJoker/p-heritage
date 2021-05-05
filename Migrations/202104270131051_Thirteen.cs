namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thirteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "yearContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "yearContent");
        }
    }
}
