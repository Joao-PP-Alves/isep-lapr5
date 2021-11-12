using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Connections
{
    public interface IConnectionService
    {
        Task<List<ConnectionDto>> GetPendentConnections(UserId id);

        Task<List<ConnectionDto>> GetAllAsync();

        Task<ConnectionDto> GetByIdAsync(ConnectionId id);

        Task<ConnectionDto> InactivateAsync(ConnectionId id);

        Task<ConnectionDto> DeleteAsync(ConnectionId id);

        Task checkUserIdAsync(UserId userId);

        Task<ConnectionDto> AddAsync(CreatingConnectionDto dto);

        Task<ConnectionDto> UpdateAsync(ConnectionDto dto);
    }
}