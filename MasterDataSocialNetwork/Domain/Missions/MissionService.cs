using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Missions {
    public class MissionService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMissionRepository _repo;

        public MissionService(IUnitOfWork unitOfWork, IMissionRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<MissionDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<MissionDto> listDto = list.ConvertAll<MissionDto>(m => new MissionDto(m.Id, m.dificultyDegree, m.status));

            return listDto;
        }

        public async Task<MissionDto> GetByIdAsync(MissionId id)
        {
            var mission = await this._repo.GetByIdAsync(id);
            
            if(mission == null)
                return null;

            return new MissionDto(mission.Id, mission.dificultyDegree, mission.status);
        }

        public async Task<MissionDto> AddAsync(CreatingMissionDto dto)
        {
            var mission = new Mission(dto.dificultyDegree, dto.status);

            await this._repo.AddAsync(mission);

            await this._unitOfWork.CommitAsync();

            return new MissionDto(mission.Id, mission.dificultyDegree, mission.status);
        }

        public async Task<MissionDto> UpdateAsync(MissionDto dto)
        {
            var mission = await this._repo.GetByIdAsync(dto.Id); 

            if (mission == null){
                return null;   
            }

            mission.ChangeDificultyDegree(dto.dificultyDegree);

            await this._unitOfWork.CommitAsync();

            return new MissionDto(mission.Id, mission.dificultyDegree, mission.status);
        }

        public async Task<MissionDto> InactivateAsync(MissionId id)
        {
            var mission = await this._repo.GetByIdAsync(id); 

            if (mission == null)
                return null;   

            mission.deactivate();
            
            await this._unitOfWork.CommitAsync();

            return new MissionDto(mission.Id, mission.dificultyDegree, mission.status);
        }

        public async Task<MissionDto> DeleteAsync(MissionId id)
        {
            var mission = await this._repo.GetByIdAsync(id); 

            if (mission == null)
                return null;   

            if (mission.Active){
                throw new BusinessRuleValidationException("It is not possible to delete an active product.");
            }
            this._repo.Remove(mission);
            await this._unitOfWork.CommitAsync();

            return new MissionDto(mission.Id, mission.dificultyDegree, mission.status);
        }
    }
}