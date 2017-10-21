namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class player1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "Weight", c => c.Single());
            AlterColumn("dbo.Players", "Hight", c => c.Single());
            AlterColumn("dbo.Players", "IdentityType", c => c.Int());
            AlterColumn("dbo.Players", "Level", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "Level", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "IdentityType", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "Hight", c => c.Single(nullable: false));
            AlterColumn("dbo.Players", "Weight", c => c.Single(nullable: false));
        }
    }
}
