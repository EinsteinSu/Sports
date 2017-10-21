namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playerselftakenscore : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "SelfTakenScore", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "SelfTakenScore", c => c.Single(nullable: false));
        }
    }
}
