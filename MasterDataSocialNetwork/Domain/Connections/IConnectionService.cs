using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using System;

namespace DDDNetCore.Domain.Connections
{
    public interface IConnectionService
    {
        Task<List<ConnectionWithRequesterDto>> GetPendentConnections(UserId id);

        Task<List<ConnectionDto>> GetAllAsync();

        Task<ConnectionDto> GetByIdAsync(ConnectionId id);

        Task<ConnectionDto> InactivateAsync(ConnectionId id);

        Task<ConnectionDto> DeleteAsync(ConnectionId id);

        Task checkUserIdAsync(UserId userId);

        Task<ConnectionDto> AddAsync(CreatingConnectionDto dto);

        Task<ConnectionDto> UpdateAsync(ConnectionDto dto);

        Task checkConnectionIdAsync(ConnectionId connectionId);

        Task<ConnectionDto> Accept(Guid connectionId);

        Task<ConnectionDto> Decline(Guid connectionId);
    }
}