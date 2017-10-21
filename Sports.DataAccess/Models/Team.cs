using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class Team
    {
        public int Id { get; set; }

        [MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(10)]
        [Required]
        public string Name { get; set; }

        [MaxLength(5)]
        public string ShortName { get; set; }

        [ForeignKey("Flag")]
        public int? FlagId { get; set; }

        public virtual Flag Flag { get; set; }

        public bool Deleted { get; set; }

        public string Description { get; set; }
    }
}