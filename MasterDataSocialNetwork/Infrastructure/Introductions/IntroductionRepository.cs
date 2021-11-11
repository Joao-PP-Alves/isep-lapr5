using DDDNetCore.Domain.Introductions;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Introductions
{
    public class IntroductionRepository : BaseRepository<Introduction, IntroductionId>, IIntroductionRepository
    {
        public IntroductionRepository(DDDNetCoreDbContext context): base(context.Introductions){
            
        } 

        public async Task<List<Introduction>> getPendentIntroductions(UserId id){
            return await ((DbSet<Introduction>)base.getContext()).Where(x => (
                (id.Equals(x.Enabler) && x.decisionStatus.Equals(IntroductionStatus.PENDING_APPROVAL))
            || (id.Equals(x.TargetUser) && x.decisionStatus.Equals(IntroductionStatus.APPROVAL_ACCEPTED))
            )).ToListAsync();
        }
    }
}