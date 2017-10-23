using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class ScheduleTeam
    {
        public int Id { get; set; }

        public virtual Team Team { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public int Score { get; set; }
    }
}