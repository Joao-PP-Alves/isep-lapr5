using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infrastructure.Users{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.Name);
            builder.OwnsOne(b => b.emotionalState);
            builder.OwnsOne(b => b.PhoneNumber);
            builder.OwnsOne(b => b.Email);
            builder.OwnsOne(b => b.Password);
            builder.OwnsOne(b => b.EmotionTime);
            builder.OwnsOne(b => b.BirthDate);
            builder.HasMany(b => b.tags);
            builder.HasMany(b => b.friendsList).WithOne()
                .HasForeignKey(f => f.requester);

        }
    }
}