using Microsoft.EntityFrameworkCore;
using DDDNetCore.Domain.Categories;
using DDDNetCore.Domain.Products;
using DDDNetCore.Domain.Families;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Infrastructure.Introductions;
using DDDNetCore.Infrastructure.Categories;
using DDDNetCore.Infrastructure.Products;
using DDDNetCore.Infrastructure.Users;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Connections;

namespace DDDNetCore.Infrastructure
{
    public class DDDNetCoreDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Family> Families { get; set; }

        // Ainda falta fazer configutrações
        public DbSet<User> Users {get; set;}

        public DbSet<Introduction> Introductions {get; set;}

        public DbSet<Friendship> Friendships {get; set;}

        public DbSet<Connection> Connections {get; set;}

        public DDDNetCoreDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IntroductionEntityTypeConfiguration());
        }
    }
}