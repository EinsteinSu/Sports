namespace Sports.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reduce_a_table_scheduleteamplayer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleTeamPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.ScheduleTeamPlayers", "ScheduleTeamId", "dbo.ScheduleTeams");
            DropIndex("dbo.ScheduleTeamPlayers", new[] { "ScheduleTeamId" });
            DropIndex("dbo.ScheduleTeamPlayers", new[] { "PlayerId" });
            AddColumn("dbo.ScheduleTeams", "Players", c => c.String());
            DropTable("dbo.ScheduleTeamPlayers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScheduleTeamPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleTeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.ScheduleTeams", "Players");
            CreateIndex("dbo.ScheduleTeamPlayers", "PlayerId");
            CreateIndex("dbo.ScheduleTeamPlayers", "ScheduleTeamId");
            AddForeignKey("dbo.ScheduleTeamPlayers", "ScheduleTeamId", "dbo.ScheduleTeams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScheduleTeamPlayers", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
