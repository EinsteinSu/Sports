using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class ScheduleTeamPlayer
    {
        public int Id { get; set; }

        [ForeignKey("ScheduleTeam")]
        public int ScheduleTeamId { get; set; }

        public ScheduleTeam ScheduleTeam { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public Player Player { get; set; }
    }
}