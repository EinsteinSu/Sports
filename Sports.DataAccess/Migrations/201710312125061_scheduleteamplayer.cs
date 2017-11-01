namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduleteamplayer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleTeamPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleTeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.ScheduleTeams", t => t.ScheduleTeamId, cascadeDelete: true)
                .Index(t => t.ScheduleTeamId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleTeamPlayers", "ScheduleTeamId", "dbo.ScheduleTeams");
            DropForeignKey("dbo.ScheduleTeamPlayers", "PlayerId", "dbo.Players");
            DropIndex("dbo.ScheduleTeamPlayers", new[] { "PlayerId" });
            DropIndex("dbo.ScheduleTeamPlayers", new[] { "ScheduleTeamId" });
            DropTable("dbo.ScheduleTeamPlayers");
        }
    }
}
