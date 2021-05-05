namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ten : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Content");
        }
    }
}
