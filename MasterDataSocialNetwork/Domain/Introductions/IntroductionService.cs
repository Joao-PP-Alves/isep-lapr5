using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Missions;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Flurl.Http;


namespace DDDNetCore.Domain.Introductions
{
    public class IntroductionService : IIntroductionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIntroductionRepository _repo;
        private readonly IUserRepository _repoUser;
        private readonly IMissionRepository _repoMission;

        public IntroductionService(IUnitOfWork unitOfWork, IIntroductionRepository repo, IUserRepository repoUser,
            IMissionRepository repoMission)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoUser = repoUser;
            this._repoMission = repoMission;
        }

        public async Task<List<IntroductionDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<IntroductionDto> listDto = list.ConvertAll<IntroductionDto>(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }

        public async Task<IntroductionDto> GetByIdAsync(IntroductionId id)
        {
            var intro = await this._repo.GetByIdAsync(id);

            if (intro == null)
                return null;

            return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                intro.Requester, intro.Enabler, intro.TargetUser);
        }

        public async Task<IntroductionDto> InactivateAsync(IntroductionId id)
        {
            var introduction = await this._repo.GetByIdAsync(id);

            if (introduction == null)
                return null;

            introduction.MarkAsInative();

            await this._unitOfWork.CommitAsync();

            return new IntroductionDto(introduction.Id.AsGuid(), introduction.MissionId, introduction.decisionStatus,
                introduction.MessageToTargetUser, introduction.MessageToIntermediate,
                introduction.MessageFromIntermediateToTargetUser, introduction.Requester, introduction.Enabler,
                introduction.TargetUser);
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

            return new IntroductionDto(introduction.Id.AsGuid(), introduction.MissionId, introduction.decisionStatus,
                introduction.MessageToTargetUser, introduction.MessageToIntermediate,
                introduction.MessageFromIntermediateToTargetUser, introduction.Requester, introduction.Enabler,
                introduction.TargetUser);
        }

        private async Task checkUserIdAsync(UserId userId)
        {
            var user = await _repoUser.GetByIdAsync(userId);
            if (user == null)
                throw new BusinessRuleValidationException("Invalid User Id");
        }

        public async Task<IntroductionDto> AddAsync(CreatingIntroductionDto dto)
        {
            await checkUserIdAsync(dto.Requester);
            await checkUserIdAsync(dto.Enabler);
            await checkUserIdAsync(dto.TargetUser);
            
            var responseString = await BuildRequest(dto.Enabler, dto.TargetUser).GetStringAsync();
            
            if (ParseRequest(responseString).Count == 0)
            {
                throw new BusinessRuleValidationException(
                    "Users might not be connected. Check friendships between them.");
            }
            else
            {
                var intro = new Introduction(dto.MessageToTargetUser, dto.MessageToIntermediate, dto.MissionId,
                    dto.Requester, dto.Enabler, dto.TargetUser);

                await this._repo.AddAsync(intro);
                await this._unitOfWork.CommitAsync();

                return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                    intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                    intro.Requester, intro.Enabler, intro.TargetUser);
            }
        }
        
        public async Task<IntroductionDto> AddDisconectedEnablerAsync(CreatingIntroductionDto dto)
        {
            await checkUserIdAsync(dto.Requester);
            await checkUserIdAsync(dto.Enabler);
            await checkUserIdAsync(dto.TargetUser);
            
            var responseString = await BuildRequest(dto.Enabler, dto.TargetUser).GetStringAsync();
            
            if (ParseRequest(responseString).Count == 0)
            {
                throw new BusinessRuleValidationException(
                    "Users might not be connected. Check friendships between them.");
            }
            else
            {
                var newIntermediateDescription = new Description("[Repassed]" + dto.MessageToIntermediate.text);
                var newTargetDescription = new Description("[Repassed]" + dto.MessageToTargetUser.text);
                var intro = new Introduction(newTargetDescription, newIntermediateDescription, dto.MissionId,
                    new UserId(dto.Requester.Value), new UserId(dto.Enabler.Value), new UserId(dto.TargetUser.Value));

                await _repo.AddAsync(intro);
                //await _unitOfWork.CommitAsync();

                return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                    intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                    intro.Requester, intro.Enabler, intro.TargetUser);
            }
        }

        public async Task<IntroductionDto> UpdateAsync(IntroductionDto dto)
        {
            await checkUserIdAsync(dto.Requester);
            await checkUserIdAsync(dto.Enabler);
            await checkUserIdAsync(dto.TargetUser);
            var intro = await this._repo.GetByIdAsync(new IntroductionId(dto.Id));

            if (intro == null)
            {
                return null;
            }

            intro.changeMessageToTargetUser(dto.MessageToTargetUser);
            intro.changeMessageToIntermediate(dto.MessageToIntermediate);
            intro.changeIntermediateToTargetUserDescription(dto.MessageFromIntermediateToTargetUser);
            intro.makeDecision(dto.decisionStatus);
            intro.ChangeRequester(dto.Requester);
            intro.ChangeEnabler(dto.Enabler);
            intro.ChangeTargetUser(dto.TargetUser);

            await this._unitOfWork.CommitAsync();

            return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                intro.Requester, intro.Enabler, intro.TargetUser);
        }

        /// <summary>
        /// Returns all introductions directed to a user which are in pendent state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<IntroductionDto>> GetPendentIntroductions(UserId id)
        {
            var list = await _repo.getPendentIntroductions(id);

            var listDto = list.ConvertAll<IntroductionDto>(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }

        /// <summary>
        /// Changes the introduction state 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /*
         * In the future the method should verify if enabler has access to the target and sends a friendship request or has
         * to create another introduction to the next intermediate user
         */
        public async Task<IntroductionDto> ApproveIntroduction(IntroductionId id)
        {
            var intro = await this._repo.GetByIdAsync(id);

            if (intro == null)
            {
                return null;
            }

            if (intro.decisionStatus == IntroductionStatus.APPROVAL_ACCEPTED)
            {
                throw new BusinessRuleValidationException(
                    "The introduction has already been accepted to be proposed to the target user");
            }

            var enabler = await _repoUser.GetByIdAsync(intro.Enabler);
            var target = await _repoUser.GetByIdAsync(intro.TargetUser);


            var responseString = await BuildRequest(enabler.Id, target.Id).GetStringAsync();

            var introPath = ParseRequest(responseString);

            if (!CheckIfTargetIsNext(enabler, target, introPath))
            {
                var newEnabler = await _repoUser.GetByIdAsync(new UserId(introPath[1]));
                var newIntro = new CreatingIntroductionDto(intro.MessageToIntermediate, intro.MessageToTargetUser,
                    intro.MissionId, intro.Requester, newEnabler.Id, intro.TargetUser);
                await AddDisconectedEnablerAsync(newIntro);
            }
            intro.approveIntermediate();

            //intro.changeIntermediateToTargetUserDescription(message);

            await _unitOfWork.CommitAsync();


            return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                intro.Requester, intro.Enabler, intro.TargetUser);
        }
        
        

        /// <summary>
        /// Makes a http_get request to the prolog server in order to know the shortest path from the enabler to the target
        /// </summary>
        /// <param name="enabler"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private string BuildRequest(UserId enabler, UserId target)
        {
            var address = new StringBuilder("http://vs651.dei.isep.ipp.pt:8888/shortpath?");
            address.Append("orig=").Append(enabler.Value);
            address.Append('&');

            address.Append("dest=").Append(target.Value);

            return address.ToString();
        }

        private List<string> ParseRequest(string raw)
        {
            var list = raw.Trim().Split('\n').ToList();
            return list;
        }

        private bool CheckIfTargetIsNext(User enabler, User target, List<string> path)
        {
            return (path[path.IndexOf(enabler.Id.Value)+1].Equals(target.Id.Value));
        }

        public async Task<IntroductionDto> ReproveIntroduction(IntroductionId id)
        {
            var intro = await this._repo.GetByIdAsync(id);

            if (intro == null)
            {
                return null;
            }

            var mission = await this._repoMission.GetByIdAsync(intro.MissionId);

            intro.declineIntermediate();

            mission.UnsucessMissionStatus();

            await this._unitOfWork.CommitAsync();

            return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                intro.Requester, intro.Enabler, intro.TargetUser);
        }

        public async Task<List<IntroductionDto>> GetPendentIntroductionsOnlyIntermediate(UserId id)
        {
            var list = await this._repo.getPendentIntroductionsOnlyIntermediate(id);

            List<IntroductionDto> listDto = list.ConvertAll<IntroductionDto>(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }

        public async Task<List<IntroductionDto>> GetPendentIntroductionsOnlyTargetUser(UserId id)
        {
            var list = await this._repo.getPendentIntroductionsOnlyTargetUser(id);

            List<IntroductionDto> listDto = list.ConvertAll<IntroductionDto>(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }
    }
}