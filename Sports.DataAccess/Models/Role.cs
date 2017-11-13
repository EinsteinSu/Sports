using System.ComponentModel.DataAnnotations;

namespace Sports.DataAccess.Models
{
    public class Role
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public string Securities { get; set; }
    }
}