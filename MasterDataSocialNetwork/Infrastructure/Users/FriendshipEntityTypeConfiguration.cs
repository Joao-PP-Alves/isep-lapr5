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
            //builder.OwnsOne(b => b.friend);
            //builder.OwnsOne(b => b.requester);
            builder.OwnsOne(b => b.friendshipTag);
            builder.OwnsOne(b => b.connection_strength);
            builder.OwnsOne(b => b.relationship_strength);
        }
    }
}