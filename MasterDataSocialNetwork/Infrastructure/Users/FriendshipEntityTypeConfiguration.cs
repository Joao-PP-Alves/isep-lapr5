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
            builder.HasOne(b => b.friend).WithMany(f=>f.friendsList);
            builder.OwnsOne(b => b.friendshipTag);
            builder.OwnsOne(b => b.connection_strenght);
            builder.OwnsOne(b => b.relationship_strenght);
        }
    }
}