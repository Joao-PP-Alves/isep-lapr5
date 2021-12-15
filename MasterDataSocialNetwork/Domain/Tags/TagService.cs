using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Tags
{
    public class TagService : ITagService
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly ITagRepository _repo;
        private readonly ITagService _tagService;

        public TagService(IUnitOfWork unitOfWork, ITagRepository repo, ITagService tagService)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._tagService = tagService;
        }

        public async Task<List<TagDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<TagDto> listDto = list.ConvertAll<TagDto>(tag => 
                new TagDto(tag.Id.AsGuid(),tag.name));

            return listDto;
        }

        public async Task<TagDto> GetByIdAsync(TagId id)
        {
            var tag = await this._repo.GetByIdAsync(id);
            
            if(tag == null)
                return null;

            return new TagDto(tag.Id.AsGuid(),tag.name);
        }

        public Task<List<TagDto>> GetByNameAsync(TagId id)
        {
            return _tagService.GetByNameAsync(id);
        }

        public async Task<TagDto> GetByNameAsync(String name)
        {
            try
            {
                var nameConfirmation = new Name(name);
            }
            catch (BusinessRuleValidationException b)
            {
                throw new Exception("The provided name is invalid");
            }

            var tag = await _repo.GetByName(name);

            if (tag == null)
            {
                return null;
            }

            return new TagDto(tag.Id.AsGuid(), tag.name);
        }

        public async Task<TagDto> AddAsync(CreatingTagDto dto)
        {
            var tag = new Tag(dto.name);

            await this._repo.AddAsync(tag);

            await this._unitOfWork.CommitAsync();

            return new TagDto(tag.Id.AsGuid(),tag.name);
        }

        public async Task<TagDto> UpdateAsync(TagDto dto)
        {
            var tag = await this._repo.GetByIdAsync(new TagId(dto.Id)); 

            if (tag == null)
                return null;   

            // change all fields
            tag.ChangeTagName(dto.name);

            await this._unitOfWork.CommitAsync();

            return new TagDto(tag.Id.AsGuid(),tag.name);
        }

        public async Task<TagDto> InactivateAsync(TagId id)
        {
            var tag = await this._repo.GetByIdAsync(id); 

            if (tag == null)
                return null;   

            tag.deactivate();
            
            await this._unitOfWork.CommitAsync();

            return new TagDto(tag.Id.AsGuid(),tag.name);
        }

        public async Task<TagDto> DeleteAsync(TagId id)
        {
            var tag = await this._repo.GetByIdAsync(id); 

            if (tag == null)
                return null;   

            if (tag.Active)
                throw new BusinessRuleValidationException("It is not possible to delete an active product.");
            
            this._repo.Remove(tag);
            await this._unitOfWork.CommitAsync();

            return new TagDto(tag.Id.AsGuid(),tag.name);
        }
    }
}