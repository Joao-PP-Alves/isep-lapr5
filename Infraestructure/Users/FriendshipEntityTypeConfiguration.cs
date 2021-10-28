using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Infrastructure.Users
{
    internal class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.user1);
            builder.HasOne(b => b.user2);
            builder.OwnsOne(b => b.friendshipTag);
        }
    }
}