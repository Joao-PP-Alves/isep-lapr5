using Microsoft.EntityFrameworkCore;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Infrastructure.Introductions;
using DDDNetCore.Infrastructure.Users;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Domain.Missions;
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

        public DDDNetCoreDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IntroductionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MissionEntityTypeConfiguration());
        }
    }
}