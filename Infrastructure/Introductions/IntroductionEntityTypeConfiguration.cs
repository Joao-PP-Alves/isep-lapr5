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
            builder.HasOne(b => b.decision);
            builder.HasOne(b => b.MissionId);
            builder.HasOne(b => b.Description);
            builder.HasOne(b => b.Requester);
            builder.HasOne(b => b.Enabler);
            builder.HasOne(b => b.TargetUser);
        }
    }
}
