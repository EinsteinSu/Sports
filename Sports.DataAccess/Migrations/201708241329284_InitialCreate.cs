namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 5),
                        Name = c.String(nullable: false, maxLength: 10),
                        ShortName = c.String(maxLength: 5),
                        FlagId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flags", t => t.FlagId)
                .Index(t => t.FlagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "FlagId", "dbo.Flags");
            DropIndex("dbo.Teams", new[] { "FlagId" });
            DropTable("dbo.Teams");
            DropTable("dbo.Flags");
        }
    }
}
