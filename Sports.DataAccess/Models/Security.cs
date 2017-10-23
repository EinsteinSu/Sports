using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class Security
    {
        public Security()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }

        [MaxLength(20),Required]
        public string Name { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public virtual SecurityGroup Group { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}