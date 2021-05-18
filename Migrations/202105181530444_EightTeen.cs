namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EightTeen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SerialKeys", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SerialKeys", "DateCreated");
        }
    }
}
