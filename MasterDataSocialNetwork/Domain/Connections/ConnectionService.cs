using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Connections;
using System;

namespace DDDNetCore.Domain.Connections
{
    public class ConnectionService : IConnectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionRepository _repo;
        private readonly IUserRepository _repoUser;

        private readonly IUserService userService;

        private readonly IFriendshipService friendshipService;

        public ConnectionService(IUnitOfWork unitOfWork, IConnectionRepository repo, IUserRepository repoUser,IUserService userService,IFriendshipService friendshipService){
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoUser = repoUser;
            this.userService = userService;
            this.friendshipService = friendshipService;
        }

        public async Task<List<ConnectionDto>> GetPendentConnections(UserId id){
            var list = await this._repo.getPendentConnections(id);

            List<ConnectionDto> listDto = list.ConvertAll<ConnectionDto>(intro =>
                new ConnectionDto(intro.Id.AsGuid(),intro.requester,intro.targetUser,intro.description,intro.decision));
        
            return listDto;
        }

        public async Task<List<ConnectionDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<ConnectionDto> listDto = list.ConvertAll<ConnectionDto>(intro => 
                new ConnectionDto(intro.Id.AsGuid(),intro.requester,intro.targetUser,intro.description,intro.decision));

            return listDto;
        }

         public async Task<ConnectionDto> GetByIdAsync(ConnectionId id)
        {
            var intro = await this._repo.GetByIdAsync(id);
            
            if(intro == null)
                return null;

            return new ConnectionDto(intro.Id.AsGuid(),intro.requester,intro.targetUser,intro.description,intro.decision);
        }

        public async Task<ConnectionDto> InactivateAsync(ConnectionId id)
        {
            var connection = await this._repo.GetByIdAsync(id); 

            if (connection == null)
                return null;   

            connection.MarkAsInative();
            
            await this._unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(),connection.requester,connection.targetUser,connection.description,connection.decision);
        }

        public async Task<ConnectionDto> DeleteAsync(ConnectionId id)
        {
            var connection = await this._repo.GetByIdAsync(id); 

            if (connection == null)
                return null;   

            if (connection.active)
                throw new BusinessRuleValidationException("It is not possible to delete an active Connection.");
            
            this._repo.Remove(connection);
            await this._unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(),connection.requester,connection.targetUser,connection.description,connection.decision);
        }

        public async Task checkUserIdAsync(UserId userId)
        {
            var user = await _repoUser.GetByIdAsync(userId);
            if (user == null)
                throw new BusinessRuleValidationException("Invalid User Id");
        }

        public async Task checkConnectionIdAsync(ConnectionId connectionId){
            var connection = await this._repo.GetByIdAsync(connectionId);
            if(connection == null){
                throw new BusinessRuleValidationException("Invalid Connection Id");
            }
        }
        
        public async Task<ConnectionDto> AddAsync(CreatingConnectionDto dto)
        {
            // checks if the users id are valid
            await checkUserIdAsync(dto.requester);
            await checkUserIdAsync(dto.targetUser);
            // checks if the users are already friends
            await userService.checkIfTwoUsersAreFriends(dto.requester,dto.targetUser);
            
            var intro = new Connection(dto.requester,dto.targetUser,dto.description);

            await this._repo.AddAsync(intro);
            await this._unitOfWork.CommitAsync();

            return new ConnectionDto(intro.Id.AsGuid(),intro.requester,intro.targetUser,intro.description,intro.decision);

        }

        public async Task<ConnectionDto> Accept(Guid connectionId){
            await checkConnectionIdAsync(new ConnectionId(connectionId));

            var connection = await this._repo.GetByIdAsync(new ConnectionId(connectionId));

            await friendshipService.createFriends(connection.requester,connection.targetUser);

            connection.acceptConnection();

            await _unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(),connection.requester,connection.targetUser,connection.description,connection.decision);

        }

        public async Task<ConnectionDto> Decline(Guid connectionId){
            await checkConnectionIdAsync(new ConnectionId(connectionId));

            var connection = await this._repo.GetByIdAsync(new ConnectionId(connectionId));

            connection.declineConnection();

            await _unitOfWork.CommitAsync();

            return new ConnectionDto(connection.Id.AsGuid(),connection.requester,connection.targetUser,connection.description,connection.decision);

        }

        public async Task<ConnectionDto> UpdateAsync(ConnectionDto dto)
        {
            await checkUserIdAsync(dto.requester);
            await checkUserIdAsync(dto.targetUser);
            var intro = await this._repo.GetByIdAsync(new ConnectionId(dto.id));

            if (intro == null){
                return null;
            }

            intro.ChangeDescription(dto.description);
            intro.MakeDecision(dto.decision);
            intro.ChangeRequester(dto.requester);
            intro.ChangeTargetUser(dto.targetUser);

            await this._unitOfWork.CommitAsync();

            return new ConnectionDto(intro.Id.AsGuid(),intro.requester,intro.targetUser,intro.description,intro.decision);
        }
        
    }
}