namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Centrals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IPAddress = c.String(nullable: false, maxLength: 20),
                        Port = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Displays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IPAddress = c.String(nullable: false, maxLength: 20),
                        Port = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                        FontFormat = c.String(),
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
                        Name = c.String(nullable: false, maxLength: 50),
                        IPAddress = c.String(maxLength: 20),
                        Port = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                        State = c.Int(nullable: false),
                        Image = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        Group = c.String(),
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
                        Usage = c.String(maxLength: 200),
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
                        Securities = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
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
                        VenueId = c.Int(),
                        RaceData = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.ScheduleTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamType = c.Int(nullable: false),
                        TeamId = c.Int(),
                        Score = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                        Players = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.TeamId)
                .Index(t => t.ScheduleId);
            
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
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Securities", "GroupId", "dbo.SecurityGroups");
            DropForeignKey("dbo.Schedules", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.ScheduleTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.ScheduleTeams", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "FlagId", "dbo.Flags");
            DropForeignKey("dbo.Logins", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Hardwares", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Displays", "VenueId", "dbo.Venues");
            DropIndex("dbo.Securities", new[] { "GroupId" });
            DropIndex("dbo.ScheduleTeams", new[] { "ScheduleId" });
            DropIndex("dbo.ScheduleTeams", new[] { "TeamId" });
            DropIndex("dbo.Schedules", new[] { "VenueId" });
            DropIndex("dbo.Teams", new[] { "FlagId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.Logins", new[] { "RoleId" });
            DropIndex("dbo.Hardwares", new[] { "VenueId" });
            DropIndex("dbo.Displays", new[] { "VenueId" });
            DropTable("dbo.SecurityGroups");
            DropTable("dbo.Securities");
            DropTable("dbo.ScheduleTeams");
            DropTable("dbo.Schedules");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.Roles");
            DropTable("dbo.Logins");
            DropTable("dbo.Hardwares");
            DropTable("dbo.Flags");
            DropTable("dbo.Features");
            DropTable("dbo.Venues");
            DropTable("dbo.Displays");
            DropTable("dbo.Centrals");
        }
    }
}
