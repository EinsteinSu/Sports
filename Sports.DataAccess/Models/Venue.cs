using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sports.DataAccess.Models
{
    public class Venue
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(11)]
        public string IPAddress { get; set; }

        [Required]
        public int Port { get; set; }

        [MaxLength(20)]
        public string Location { get; set; }

        public byte[] Image { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //public virtual ICollection<Hardware> Hardwares { get; set; }

        //public virtual ICollection<Display> Displays { get; set; }
    }
}
