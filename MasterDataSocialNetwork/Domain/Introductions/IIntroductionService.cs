using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Introductions
{
    public interface IIntroductionService {

        Task<List<IntroductionDto>> GetAllAsync();

        Task<IntroductionDto> GetByIdAsync(IntroductionId id);

        Task<IntroductionDto> InactivateAsync(IntroductionId id);

        Task<IntroductionDto> DeleteAsync(IntroductionId id);

        Task<IntroductionDto> AddAsync(CreatingIntroductionDto dto);

        Task<IntroductionDto> UpdateAsync(IntroductionDto dto);

        Task<List<IntroductionDto>> GetPendentIntroductions(UserId id);

        Task<IntroductionDto> ApproveIntroduction(IntroductionId id);

        Task<IntroductionDto> ReproveIntroduction(IntroductionId id);

        Task<List<IntroductionDto>> GetPendentIntroductionsOnlyIntermediate(UserId id);

        Task<List<IntroductionDto>> GetPendentIntroductionsOnlyTargetUser(UserId id);
    }
}
