using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Tags;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Tags
{
    public class TagRepository : BaseRepository<Tag, TagId>, ITagRepository
    {
        private readonly DddNetCoreDbContext _context;
        
        public TagRepository(DddNetCoreDbContext context) : base(context.Tags)
        {
            _context = context;
        }
        
        public new Task<Tag> GetByIdAsync(TagId id)
        {
            return _context.Tags.Where(tag => tag.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Tag> GetByName(string name)
        {
            return await _context.Tags.Where(x => name.Equals(x.name.text)).FirstAsync();
        }
    }
}