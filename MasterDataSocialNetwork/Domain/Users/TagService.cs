using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Users
{
    public class TagService : ITagService
    {
        public Task<List<TagDto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<TagDto> GetByIdAsync(TagId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TagDto> GetByNameAsync(TagId id)
        {
            throw new System.NotImplementedException();
        }
    }
}