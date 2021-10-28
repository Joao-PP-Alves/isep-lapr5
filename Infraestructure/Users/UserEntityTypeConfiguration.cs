using DDDNetCore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infrastructure.Users{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.ToTable("Users",SchemaNames.DDDNetCore);
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.emotionalState);
        }
    }
}