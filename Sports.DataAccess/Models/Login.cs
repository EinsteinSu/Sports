using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class Login
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}