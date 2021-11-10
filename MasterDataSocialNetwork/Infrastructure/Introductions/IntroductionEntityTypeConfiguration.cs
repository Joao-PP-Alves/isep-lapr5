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
            builder.OwnsOne(b => b.decision);
          //  builder.HasOne(b => b.MissionId);
            builder.OwnsOne(b => b.Description);
            builder.OwnsOne(b => b.Requester, req => {req.Property(p => p.Value);});
        //    builder.OwnsOne(b => b.Enabler);
        //    builder.HasOne(b => b.TargetUser);
        }
    }
}
