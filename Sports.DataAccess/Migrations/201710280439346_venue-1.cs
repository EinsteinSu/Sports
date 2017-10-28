namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class venue1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venues", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Venues", "State");
        }
    }
}
