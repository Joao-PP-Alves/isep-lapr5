using DDDNetCore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infrastructure.Users{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.emotionalState);
            builder.OwnsOne(b => b.PhoneNumber);
            builder.OwnsOne(b => b.Email);
            builder.OwnsMany(b => b.tags);
        }
    }
}