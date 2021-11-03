using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Infrastructure.Connections{

    internal class ConnectionEntityTypeConfiguration : IEntityTypeConfiguration<Connection>
    {
       public void Configure(EntityTypeBuilder<Connection> builder)
        {
            //builder.ToTable("Connections",SchemaNames.DDDNetCore);
            builder.HasKey(b => b.Id);
            //builder.HasOne(b => b.requester);
            //builder.HasOne(b => b.targetUser);
            //builder.OwnsOne(b => b.description);
            //builder.Property(b => b.decision).HasConversion<string>();
        }
    }
}