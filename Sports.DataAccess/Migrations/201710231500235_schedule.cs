namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule : DbMigration
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
                        VenueId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.VenueId);
            
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
            
            CreateTable(
                "dbo.Flags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.Binary(),
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
                        VenueId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Securities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SecurityGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.SecurityGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
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
                        Weight = c.Single(),
                        Hight = c.Single(),
                        BirthDate = c.DateTime(),
                        BoneDate = c.DateTime(),
                        SelfTakenScore = c.Single(),
                        IdentityType = c.Int(),
                        IdentityNumber = c.String(maxLength: 50),
                        Level = c.Int(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
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
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.ScheduleTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id)
                .Index(t => t.TeamId)
                .Index(t => t.Schedule_Id);
            
            CreateTable(
                "dbo.SecurityRoles",
                c => new
                    {
                        Security_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Security_Id, t.Role_Id })
                .ForeignKey("dbo.Securities", t => t.Security_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Security_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.ScheduleTeams", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.ScheduleTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "FlagId", "dbo.Flags");
            DropForeignKey("dbo.Logins", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.SecurityRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.SecurityRoles", "Security_Id", "dbo.Securities");
            DropForeignKey("dbo.Securities", "GroupId", "dbo.SecurityGroups");
            DropForeignKey("dbo.Hardwares", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Displays", "VenueId", "dbo.Venues");
            DropIndex("dbo.SecurityRoles", new[] { "Role_Id" });
            DropIndex("dbo.SecurityRoles", new[] { "Security_Id" });
            DropIndex("dbo.ScheduleTeams", new[] { "Schedule_Id" });
            DropIndex("dbo.ScheduleTeams", new[] { "TeamId" });
            DropIndex("dbo.Schedules", new[] { "VenueId" });
            DropIndex("dbo.Teams", new[] { "FlagId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.Securities", new[] { "GroupId" });
            DropIndex("dbo.Logins", new[] { "RoleId" });
            DropIndex("dbo.Hardwares", new[] { "VenueId" });
            DropIndex("dbo.Displays", new[] { "VenueId" });
            DropTable("dbo.SecurityRoles");
            DropTable("dbo.ScheduleTeams");
            DropTable("dbo.Schedules");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.SecurityGroups");
            DropTable("dbo.Securities");
            DropTable("dbo.Roles");
            DropTable("dbo.Logins");
            DropTable("dbo.Hardwares");
            DropTable("dbo.Flags");
            DropTable("dbo.Venues");
            DropTable("dbo.Displays");
        }
    }
}
