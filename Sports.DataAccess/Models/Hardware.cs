using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class Hardware
    {
        public int Id { get; set; }

        [Required]
        public int Port { get; set; }

        [MaxLength(50)]
        public string Usage { get; set; }

        public virtual Venue Venue { get; set; }

        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        public string Description { get; set; }
    }
}