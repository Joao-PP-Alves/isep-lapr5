using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Missions
{

    public interface IMissionService
    {
        public Task<List<MissionDto>> GetAllAsync();
        public Task<MissionDto> GetByIdAsync(MissionId id);
        public Task<MissionDto> AddAsync(CreatingMissionDto dto);
        public Task<MissionDto> UpdateAsync(MissionDto dto);
        public Task<MissionDto> InactivateAsync(MissionId id);
        public Task<MissionDto> DeleteAsync(MissionId id);

    }
}