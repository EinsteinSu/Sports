using System.ComponentModel.DataAnnotations;

namespace Sports.DataAccess.Models
{
    public class Display
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(11)]
        [Required]
        public string IPAddress { get; set; }

        [Required]
        public int Port { get; set; }

        public string Description { get; set; }
    }
}