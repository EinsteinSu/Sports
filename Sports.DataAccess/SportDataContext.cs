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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schedule>()
                .HasMany(p => p.Teams)
                .WithOptional()
                .WillCascadeOnDelete(true);
        }

        public IDbSet<Team> Teams { get; set; }

        public DbSet<Flag> Flags { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Display> Displays { get; set; }

        public DbSet<Hardware> Hardwares { get; set; }

        public DbSet<SecurityGroup> SecurityGroups { get; set; }

        public DbSet<Security> Securities { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<ScheduleTeam> ScheduleTeams { get; set; }
    }
}