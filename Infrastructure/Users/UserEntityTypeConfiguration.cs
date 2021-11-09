using DDDNetCore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infrastructure.Users{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Name);
            builder.HasOne(b => b.emotionalState);
            builder.HasOne(b => b.PhoneNumber);
            builder.HasOne(b => b.Email);
            builder.HasOne(b => b.Password);
            builder.HasMany(b => b.tags);
        }
    }
}