using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Tags
{
    public interface ITagRepository : IRepository<Tag,TagId>
    {
        public Task<Tag> GetByName(string name);
    }
}