namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifteen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "FormLink", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "FormLink", c => c.String(nullable: false));
        }
    }
}
