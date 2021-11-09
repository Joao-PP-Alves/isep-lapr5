using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;


namespace DDDNetCore.Domain.Connections
{
    public class ConnectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionRepository _repo;
        private readonly IUserRepository _repoUser;

        public ConnectionService(IUnitOfWork unitOfWork, IConnectionRepository repo, IUserRepository repoUser){
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoUser = repoUser;
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

        private async Task checkUserIdAsync(UserId userId)
        {
            var user = await _repoUser.GetByIdAsync(userId);
            if (user == null)
                throw new BusinessRuleValidationException("Invalid User Id");
        }
        
        public async Task<ConnectionDto> AddAsync(CreatingConnectionDto dto)
        {
            await checkUserIdAsync(dto.requester);
            await checkUserIdAsync(dto.targetUser);
            var intro = new Connection(dto.requester,dto.targetUser,dto.description);

            await this._repo.AddAsync(intro);
            await this._unitOfWork.CommitAsync();

            return new ConnectionDto(intro.Id.AsGuid(),intro.requester,intro.targetUser,intro.description,intro.decision);

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