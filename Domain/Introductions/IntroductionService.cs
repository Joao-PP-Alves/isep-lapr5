using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Introductions
{
    public class IntroductionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIntroductionRepository _repo;
        //private readonly IUserRepository _repoUser;

        public IntroductionService(IUnitOfWork unitOfWork, IIntroductionRepository repo/*, IUserRepository repoUser **/){
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            //this._repoUser = repoUser;
        }

        public async Task<List<IntroductionDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<IntroductionDto> listDto = list.ConvertAll<IntroductionDto>(intro => 
                new IntroductionDto(intro.Id.AsGuid(),intro.Description,intro.Resquester,intro.Enabler,intro.TargetUser));

            return listDto;
        }

         public async Task<IntroductionDto> GetByIdAsync(IntroductionId id)
        {
            var intro = await this._repo.GetByIdAsync(id);
            
            if(intro == null)
                return null;

            return new IntroductionDto(intro.Id.AsGuid(),intro.Description,intro.Resquester,intro.Enabler,intro.TargetUser);
        }

        public async Task<IntroductionDto> InactivateAsync(IntroductionId id)
        {
            var introduction = await this._repo.GetByIdAsync(id); 

            if (introduction == null)
                return null;   

            //introduction.MarkAsInative();
            
            await this._unitOfWork.CommitAsync();

            return new IntroductionDto(introduction.Id.AsGuid(),introduction.Description,introduction.Resquester,introduction.Enabler,introduction.TargetUser);
        }

        public async Task<IntroductionDto> DeleteAsync(IntroductionId id)
        {
            var introduction = await this._repo.GetByIdAsync(id); 

            if (introduction == null)
                return null;   

            if (introduction.Active)
                throw new BusinessRuleValidationException("It is not possible to delete an active introduction.");
            
            this._repo.Remove(introduction);
            await this._unitOfWork.CommitAsync();

            return new IntroductionDto(introduction.Id.AsGuid(),introduction.Description,introduction.Resquester,introduction.Enabler,introduction.TargetUser);
        }

        //TODO: Add/Update/CheckUserId Async
    }
}