using DDDNetCore.Domain.Tags;
using DDDNetCore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infrastructure.Tags
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(tag => tag.Id);
            builder.HasMany(tag => tag.usersList).WithMany(u => u.tags);
        }
    }
}