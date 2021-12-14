using Microsoft.EntityFrameworkCore;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Infrastructure.Introductions;
using DDDNetCore.Infrastructure.Users;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Tags;
using DDDNetCore.Infrastructure.Connections;
using DDDNetCore.Infrastructure.Missions;
using DDDNetCore.Infrastructure.Tags;

namespace DDDNetCore.Infrastructure
{
    public class DddNetCoreDbContext : DbContext
    {
        public virtual DbSet<User> Users {get; set;}

        public DbSet<Introduction> Introductions {get; set;}

        //public DbSet<Friendship> Friendships {get; set;}

        public DbSet<Connection> Connections {get; set;}

        public DbSet<Mission> Missions {get;set;}
        
        public DbSet<Tag> Tags { get; set; }
        public DddNetCoreDbContext(DbContextOptions<DddNetCoreDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=tcp:lapr5g020.database.windows.net,1433;Initial Catalog=LAPR5G020_DB;Persist Security Info=False;User ID=Zezoca;Password=Tropita123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("LAPR5");
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IntroductionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConnectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MissionEntityTypeConfiguration());
            
        }
    }
}