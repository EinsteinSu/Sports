namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class limited : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Displays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        IPAddress = c.String(nullable: false, maxLength: 11),
                        Port = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.Int(nullable: false),
                        Usage = c.String(maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        DisplayName = c.String(maxLength: 20),
                        TeamId = c.Int(nullable: false),
                        RegistNumber = c.String(maxLength: 5),
                        BibNumber = c.String(maxLength: 5),
                        Gender = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        Hight = c.Single(nullable: false),
                        BirthDate = c.DateTime(),
                        BoneDate = c.DateTime(),
                        SelfTakenScore = c.Single(nullable: false),
                        IdentityType = c.Int(nullable: false),
                        IdentityNumber = c.String(maxLength: 50),
                        Level = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        IPAddress = c.String(maxLength: 11),
                        Port = c.Int(nullable: false),
                        Location = c.String(maxLength: 20),
                        Image = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropTable("dbo.Venues");
            DropTable("dbo.Players");
            DropTable("dbo.Hardwares");
            DropTable("dbo.Displays");
        }
    }
}
