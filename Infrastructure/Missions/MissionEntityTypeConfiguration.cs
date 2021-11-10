using DDDNetCore.Domain.Missions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNetCore.Infrastructure.Missions{
    internal class MissionEntityTypeConfiguration : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
           // builder.HasKey(b => b.Id);
            builder.HasOne(b => b.dificultyDegree);
            //builder.HasOne(b => b.dificultyDegree);
            //builder.HasOne(b => b.dificultyDegree);
        }
    }
}