namespace Projet_Heritage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "imagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "imagePath");
        }
    }
}
