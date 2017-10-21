using System.Data.Entity;
using Sports.DataAccess.Models;

namespace Sports.DataAccess
{
    public class SportDataContext : DbContext
    {
        public SportDataContext()
            : base("Sports")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Team> Teams { get; set; }

        public DbSet<Flag> Flags { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Display> Displays { get; set; }

        public DbSet<Hardware> Hardwares { get; set; }
    }
}