using Microsoft.EntityFrameworkCore;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Infrastructure.Introductions;
using DDDNetCore.Infrastructure.Users;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Infrastructure.Connections;
using DDDNetCore.Infrastructure.Missions;

namespace DDDNetCore.Infrastructure
{
    public class DDDNetCoreDbContext : DbContext
    {
        public DbSet<User> Users {get; set;}

        public DbSet<Introduction> Introductions {get; set;}

        public DbSet<Friendship> Friendships {get; set;}

        public DbSet<Connection> Connections {get; set;}

        public DbSet<Mission> Missions {get;set;}
        public DDDNetCoreDbContext(DbContextOptions<DDDNetCoreDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=vs398.dei.isep.ipp.pt;Database=master;User Id=Zezoca;Password=Tropita123;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("LAPR5");
            
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IntroductionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConnectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MissionEntityTypeConfiguration());

            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}