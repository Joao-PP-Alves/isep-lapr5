using DDDNetCore.Domain.Missions;
using DDDNetCore.Infrastructure.Shared;

namespace DDDNetCore.Infrastructure.Missions {

    public class MissionRepository : BaseRepository<Mission, MissionId>, IMissionRepository
    {
       public MissionRepository(DddNetCoreDbContext context) : base(context.Missions)
        {
            
        } 
    }


}