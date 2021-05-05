namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Four : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "FormLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "FormLink");
        }
    }
}
