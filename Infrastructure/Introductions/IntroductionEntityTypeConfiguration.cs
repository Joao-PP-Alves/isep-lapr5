using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDNetCore.Domain.Introductions;

namespace DDDNetCore.Infrastructure.Introductions{

    internal class IntroductionEntityTypeConfiguration : IEntityTypeConfiguration<Introduction>
    {
       public void Configure(EntityTypeBuilder<Introduction> builder)
        {
            //builder.ToTable("Introductions",SchemaNames.DDDNetCore);
            builder.HasKey(b => b.Id);
            //builder.HasOne(b => b.Decision);
            
        }
    }
}
