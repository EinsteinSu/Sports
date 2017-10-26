using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }


        public Venue Venue { get; set; }

        [ForeignKey("Venue")]
        public int? VenueId { get; set; }

        public virtual ICollection<ScheduleTeam> Teams { get; set; }
    }
}