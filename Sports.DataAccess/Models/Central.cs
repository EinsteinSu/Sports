using System.ComponentModel.DataAnnotations;

namespace Sports.DataAccess.Models
{
    public class Central
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string IPAddress { get; set; }

        public int Port { get; set; }

        public string Description { get; set; }
    }
}