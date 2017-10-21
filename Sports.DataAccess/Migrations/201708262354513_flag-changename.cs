namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flagchangename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flags", "Photo", c => c.Binary());
            DropColumn("dbo.Flags", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flags", "Image", c => c.Binary());
            DropColumn("dbo.Flags", "Photo");
        }
    }
}
