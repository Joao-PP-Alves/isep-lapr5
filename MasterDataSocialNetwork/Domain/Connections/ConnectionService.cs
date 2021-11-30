using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Connections
{
    public class ConnectionService : IConnectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionRepository _repo;
        private readonly IUserRepository _repoUser;

        private readonly IUserService userService;

        private readonly IFriendshipService friendshipService;

        private readonly IMissionService missionService;

        public ConnectionService(IUnitOfWork unitOfWork, IConnectionRepository repo, IUserRepository repoUser,
            IUserService userService, IFriendshipService friendshipService, IMissionService missionService)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _repoUser = repoUser;
            this.userService = userService;
            this.friendshipService = friendshipService;
            this.missionService = missionService;
        }

        public async Task<List<ConnectionDto>> GetPendentConnections(UserId id)
        {
            var list = await _repo.getPendentConnections(id);

            List<ConnectionDto> listDto = list.ConvertAll(intro =>
                new ConnectionDto(intro.Id.AsGuid(), intro.requester, intro.targetUser, intro.description,
                    intro.decision));

            return listDto;
        }

        public async Task<List<ConnectionDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();

            List<ConnectionDto> listDto = list.ConvertAll(intro =>
                new ConnectionDto(intro.Id.AsGuid(), intro.requester, intro.targetUser, intro.description,
                    intro.decision));

            return listDto;
        }

        public async Task<ConnectionDto> GetByIdAsync(ConnectionId id)
        {
            var intro = await _repo.GetByIdAsync(id);

            if (intro == null)
                return null;

            return new ConnectionDto(intro.Id.AsGuid(), intro.requester, intro.targetUser, intro.description,
                intro.decision);
        }

        public async Task<ConnectionDto> InactivateAsync(ConnectionId id)
        {
            var connection = await _repo.GetByIdAsync(id);

            if (connection == null)
                return null;

            connection.MarkAsInative();

            await _unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(), connection.requester, connection.targetUser,
                connection.description, connection.decision);
        }

        public async Task<ConnectionDto> DeleteAsync(ConnectionId id)
        {
            var connection = await _repo.GetByIdAsync(id);

            if (connection == null)
                return null;

            if (connection.active)
                throw new BusinessRuleValidationException("It is not possible to delete an active Connection.");

            _repo.Remove(connection);
            await _unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(), connection.requester, connection.targetUser,
                connection.description, connection.decision);
        }

        public async Task checkUserIdAsync(UserId userId)
        {
            var user = await _repoUser.GetByIdAsync(userId);
            if (user == null)
                throw new BusinessRuleValidationException("Invalid User Id");
        }

        public async Task checkConnectionIdAsync(ConnectionId connectionId)
        {
            var connection = await _repo.GetByIdAsync(connectionId);
            if (connection == null)
            {
                throw new BusinessRuleValidationException("Invalid Connection Id");
            }
        }

        public async Task<ConnectionDto> AddAsync(CreatingConnectionDto dto)
        {
            // checks if the users id are valid
            await checkUserIdAsync(dto.requester);
            await checkUserIdAsync(dto.targetUser);
            // checks if the users are already friends
            await userService.checkIfTwoUsersAreFriends(dto.requester, dto.targetUser);
            
            
            var connection = new Connection(dto.requester, dto.targetUser, dto.description);


            await _repo.AddAsync(connection);
            await _unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(), connection.requester, connection.targetUser, connection.description,
                connection.decision);
        }

        public async Task<ConnectionDto> Accept(Guid connectionId)
        {
            await checkConnectionIdAsync(new ConnectionId(connectionId));

            var connection = await _repo.GetByIdAsync(new ConnectionId(connectionId));

            await friendshipService.createFriends(connection.requester, connection.targetUser);

            connection.acceptConnection();

            // if (connection.missionId != null)
            // {
            //     await missionService.SuccessAsync(connection.missionId);
            // }

            await _unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(), connection.requester, connection.targetUser,
                connection.description, connection.decision);
        }

        public async Task<ConnectionDto> Decline(Guid connectionId)
        {
            await checkConnectionIdAsync(new ConnectionId(connectionId));

            var connection = await _repo.GetByIdAsync(new ConnectionId(connectionId));

            connection.declineConnection();

            // if (connection.missionId != null)
            // {
            //     await missionService.UnsuccessAsync(connection.missionId);
            // }

            await _unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(), connection.requester, connection.targetUser,
                connection.description, connection.decision);
        }

        public async Task<ConnectionDto> UpdateAsync(ConnectionDto dto)
        {
            await checkUserIdAsync(dto.requester);
            await checkUserIdAsync(dto.targetUser);
            var intro = await _repo.GetByIdAsync(new ConnectionId(dto.id));

            if (intro == null)
            {
                return null;
            }

            intro.ChangeDescription(dto.description);
            intro.MakeDecision(dto.decision);
            intro.ChangeRequester(dto.requester);
            intro.ChangeTargetUser(dto.targetUser);

            await _unitOfWork.CommitAsync();

            return new ConnectionDto(intro.Id.AsGuid(), intro.requester, intro.targetUser, intro.description,
                intro.decision);
        }
    }
}