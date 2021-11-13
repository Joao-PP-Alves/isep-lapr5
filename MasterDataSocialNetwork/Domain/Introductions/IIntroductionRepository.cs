using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Introductions
{
    public interface IIntroductionRepository : IRepository<Introduction,IntroductionId>
    {
        Task<List<Introduction>> getPendentIntroductions(UserId id);

        Task<List<Introduction>> getPendentIntroductionsOnlyIntermediate(UserId id);

        Task<List<Introduction>> getPendentIntroductionsOnlyTargetUser(UserId id);
    }
}