using System.ComponentModel.DataAnnotations;

namespace Sports.DataAccess.Models
{
    public class Hardware
    {
        public int Id { get; set; }

        [Required]
        public int Port { get; set; }

        [MaxLength(50)]
        public string Usage { get; set; }

        public string Description { get; set; }
    }
}