using DDDNetCore.Domain.Introductions;
using DDDNetCore.Infrastructure.Shared;

namespace DDDNetCore.Infrastructure.Introductions
{
    public class IntroductionRepository : BaseRepository<Introduction, IntroductionId>, IIntroductionRepository
    {
        public IntroductionRepository(DDDNetCoreDbContext context): base(context.Introductions){
            
        }
    }
}