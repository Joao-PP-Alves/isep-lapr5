using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDNetCore.Domain.Connections;

namespace DDDNetCore.Infrastructure.Connections{

    internal class ConnectionEntityTypeConfiguration : IEntityTypeConfiguration<Connection>
    {
       public void Configure(EntityTypeBuilder<Connection> builder)
        {
            //builder.ToTable("Connections",SchemaNames.DDDNetCore);
            builder.HasKey(b => b.Id);
        }
    }
}