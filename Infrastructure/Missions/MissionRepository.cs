using DDDNetCore.Domain.Missions;
using DDDNetCore.Infrastructure.Shared;

namespace DDDNetCore.Infrastructure.Missions {

    public class MissionRepository : BaseRepository<Mission, MissionId>, IMissionRepository
    {
        public MissionRepository(DDDNetCoreDbContext context) : base(context.Missions)
        {
            
        }
    }


}