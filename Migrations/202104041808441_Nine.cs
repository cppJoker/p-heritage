namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Authors", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Authors");
        }
    }
}
