using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class Display
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(20)]
        [Required]
        public string IPAddress { get; set; }

        [Required]
        public int Port { get; set; }

        public virtual Venue Venue { get; set; }

        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        public string FontFormat { get; set; }

        public string Description { get; set; }
    }
}