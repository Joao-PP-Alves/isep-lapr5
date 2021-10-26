using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Introductions
{
    public interface IIntroductionRepository : IRepository<Introduction,IntroductionId>
    {
    }
}