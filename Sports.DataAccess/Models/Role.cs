using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sports.DataAccess.Models
{
    public class Role
    {
        public Role()
        {
            Securities = new HashSet<Security>();
        }

        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Security> Securities { get; set; }
    }
}