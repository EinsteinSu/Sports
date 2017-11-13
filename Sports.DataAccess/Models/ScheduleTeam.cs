using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class ScheduleTeam
    {
        public int Id { get; set; }

        public TeamType TeamType { get; set; }

        public virtual Team Team { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }

        public int Score { get; set; }

        public virtual Schedule Schedule { get; set; }

        [ForeignKey("Schedule")]
        public int ScheduleId { get; set; }

        public string Players { get; set; }
    }

    public enum TeamType
    {
        Host, Guest
    }
}