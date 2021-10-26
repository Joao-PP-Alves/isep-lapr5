using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Introductions
{
    public interface IIntroductionRepository : IRepository<Introduction,IntroductionId>
    {
    }
}