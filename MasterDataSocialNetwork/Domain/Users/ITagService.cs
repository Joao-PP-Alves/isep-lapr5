using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Users
{
    public interface ITagService
    {
        public Task<List<TagDto>> GetAllAsync();
        public Task<TagDto> GetByIdAsync(TagId id);
        public Task<TagDto> GetByNameAsync(TagId id);
    }
}