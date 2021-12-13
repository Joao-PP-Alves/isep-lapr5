using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Tags
{
    public interface ITagService
    {
        public Task<List<TagDto>> GetAllAsync();
        public Task<TagDto> GetByIdAsync(TagId id);
        public Task<List<TagDto>> GetByNameAsync(TagId id);
    }
}