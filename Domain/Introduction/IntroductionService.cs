using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Introduction;

namespace DDDSample1.Domain.Introduction
{
    public class IntroductionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIntroductionRepository _repo;

        public IntroductionService(IUnitOfWork unitOfWork, IIntroductionRepository repo){
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

    }
}