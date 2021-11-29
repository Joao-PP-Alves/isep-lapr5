using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Flurl.Http;
using Microsoft.Data.SqlClient;

namespace DDDNetCore.Domain.Introductions
{
    public class IntroductionService : IIntroductionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIntroductionRepository _repo;
        private readonly IUserRepository _repoUser;
        private readonly IMissionRepository _repoMission;
        private readonly IConnectionService _connectionService;

        public IntroductionService(IUnitOfWork unitOfWork, IIntroductionRepository repo, IUserRepository repoUser,
            IMissionRepository repoMission, IConnectionService connectionService)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _repoUser = repoUser;
            _repoMission = repoMission;
            _connectionService = connectionService;
        }

        public async Task<List<IntroductionDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();

            List<IntroductionDto> listDto = list.ConvertAll(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }

        public async Task<IntroductionDto> GetByIdAsync(IntroductionId id)
        {
            var intro = await _repo.GetByIdAsync(id);

            if (intro == null)
                return null;

            return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                intro.Requester, intro.Enabler, intro.TargetUser);
        }

        public async Task<IntroductionDto> InactivateAsync(IntroductionId id)
        {
            var introduction = await _repo.GetByIdAsync(id);

            if (introduction == null)
                return null;

            introduction.MarkAsInative();

            await _unitOfWork.CommitAsync();

            return new IntroductionDto(introduction.Id.AsGuid(), introduction.MissionId, introduction.decisionStatus,
                introduction.MessageToTargetUser, introduction.MessageToIntermediate,
                introduction.MessageFromIntermediateToTargetUser, introduction.Requester, introduction.Enabler,
                introduction.TargetUser);
        }

        public async Task<IntroductionDto> DeleteAsync(IntroductionId id)
        {
            var introduction = await _repo.GetByIdAsync(id);

            if (introduction == null)
                return null;

            if (introduction.Active)
                throw new BusinessRuleValidationException("It is not possible to delete an active introduction.");

            _repo.Remove(introduction);
            await _unitOfWork.CommitAsync();

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

            /*var responseString = await BuildRequest(dto.Enabler, dto.TargetUser).GetStringAsync();

            if (ParseRequest(responseString).Count == 0)
            {
                throw new BusinessRuleValidationException(
                    "Users might not be connected. Check friendships between them.");
            }**/

            var intro = new Introduction(dto.MessageToTargetUser, dto.MessageToIntermediate, dto.MissionId,
                dto.Requester, dto.Enabler, dto.TargetUser);

            await _repo.AddAsync(intro);
            await _unitOfWork.CommitAsync();

            return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                intro.Requester, intro.Enabler, intro.TargetUser);
        }

        public async Task<IntroductionDto> AddDisconectedEnablerAsync(CreatingIntroductionDto dto)
        {
            await checkUserIdAsync(dto.Requester);
            await checkUserIdAsync(dto.Enabler);
            await checkUserIdAsync(dto.TargetUser);
            
            var emailEnabler = _repoUser.GetByIdAsync(dto.Enabler).Result.Email.EmailAddress;
            var emailTarget = _repoUser.GetByIdAsync(dto.TargetUser).Result.Email.EmailAddress;

            var responseString = await BuildRequest(emailEnabler, emailTarget).GetStringAsync();

            if (ParseRequest(responseString).Count == 0)
            {
                throw new BusinessRuleValidationException(
                    "Users might not be connected. Check friendships between them.");
            }

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

        public async Task<IntroductionDto> UpdateAsync(IntroductionDto dto)
        {
            await checkUserIdAsync(dto.Requester);
            await checkUserIdAsync(dto.Enabler);
            await checkUserIdAsync(dto.TargetUser);
            var intro = await _repo.GetByIdAsync(new IntroductionId(dto.Id));

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

            await _unitOfWork.CommitAsync();

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

            var listDto = list.ConvertAll(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }

        /// <summary>
        /// Changes the introduction state 
        /// </summary>
        /// <param name="id">The introduction id [Guid]</param>
        /// <returns></returns>
        public async Task<IntroductionDto> ApproveIntroduction(IntroductionId id)
        {
            try
            {
                var intro = await _repo.GetByIdAsync(id);
                
                if (intro == null)
                {
                    return null;
                }

                var emailEnabler = _repoUser.GetByIdAsync(intro.Enabler).Result.Email.EmailAddress;
                var emailTarget = _repoUser.GetByIdAsync(intro.TargetUser).Result.Email.EmailAddress;

                var responseString = await BuildRequest(emailEnabler, emailTarget).GetStringAsync();
                //var responseString = "eadf5abc-fa98-4f87-820b-33e320582327\nf4b32456-5989-4e2a-b8fd-49b2197ebfea";
                var introPath = ParseRequest(responseString);

                // If we still didn't reach the target user, we must create a new introduction 
                // In this new introduction, the requester and target will remain the same but the enabler will the next person in the path to the target
                // Once the next person is the target, we accept the introduction and create a connection (Friend Request) from the requester to the target, with a message
                if (!CheckIfTargetIsNext(intro.Enabler, intro.TargetUser, introPath))
                {
                    var newEnabler = _repoUser.GetByEmail(introPath[1]).Result.FirstOrDefault();
                    if (newEnabler != null)
                    {
                        var newIntro = new CreatingIntroductionDto(intro.MessageToIntermediate, intro.MessageToTargetUser,
                            intro.MissionId, intro.Requester, newEnabler.Id, intro.TargetUser);
                        await AddDisconectedEnablerAsync(newIntro);
                    }

                    intro.approveIntermediate();
                }
                else
                {
                    await _connectionService.AddAsync(new CreatingConnectionDto(intro.MessageToTargetUser,
                        intro.Requester, intro.TargetUser,intro.MissionId));
                    intro.AcceptedIntroduction();
                    
                }
                
                await _unitOfWork.CommitAsync();
                
                return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                    intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                    intro.Requester, intro.Enabler, intro.TargetUser);
            }
            catch (SqlException exception)
            {
                throw new Exception(
                    "The introduction either does not exist or there was an error with the database \n" +
                    exception.Message);
            }
        }


        /// <summary>
        /// Makes a http_get request to the prolog server in order to know the shortest path from the enabler to the target
        /// </summary>
        /// <param name="enabler"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private string BuildRequest(String enabler, String target)
        {
            var address = new StringBuilder("http://localhost:3000/shortpath?");
            address.Append("orig=").Append(enabler);
            address.Append('&');

            address.Append("dest=").Append(target);

            return address.ToString();
        }

        private List<string> ParseRequest(string raw)
        {
            var list = raw.Trim().Split('\n').ToList();
            return list;
        }

        private bool CheckIfTargetIsNext(UserId enabler, UserId target, List<string> path)
        {
            return (path[path.IndexOf(enabler.Value) + 1].Equals(target.Value));
        }

        public async Task<IntroductionDto> ReproveIntroduction(IntroductionId id)
        {
            var intro = await _repo.GetByIdAsync(id);

            if (intro == null)
            {
                return null;
            }

            var mission = await _repoMission.GetByIdAsync(intro.MissionId);

            intro.declineIntermediate();

            mission.UnsucessMissionStatus();

            await _unitOfWork.CommitAsync();

            return new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus,
                intro.MessageToTargetUser, intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser,
                intro.Requester, intro.Enabler, intro.TargetUser);
        }

        public async Task<List<IntroductionDto>> GetPendentIntroductionsOnlyIntermediate(UserId id)
        {
            var list = await _repo.getPendentIntroductionsOnlyIntermediate(id);

            List<IntroductionDto> listDto = list.ConvertAll(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }

        public async Task<List<IntroductionDto>> GetPendentIntroductionsOnlyTargetUser(UserId id)
        {
            var list = await _repo.getPendentIntroductionsOnlyTargetUser(id);

            List<IntroductionDto> listDto = list.ConvertAll(intro =>
                new IntroductionDto(intro.Id.AsGuid(), intro.MissionId, intro.decisionStatus, intro.MessageToTargetUser,
                    intro.MessageToIntermediate, intro.MessageFromIntermediateToTargetUser, intro.Requester,
                    intro.Enabler, intro.TargetUser));

            return listDto;
        }
    }
}