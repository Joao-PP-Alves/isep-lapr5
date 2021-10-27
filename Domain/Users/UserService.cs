using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<UserDto> listDto = list.ConvertAll<UserDto>(user =>
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.tags, user.emotionalState));
            return listDto;
        }

        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.tags, user.emotionalState);
        }

        public async Task<UserDto> AddAsync(CreatingUserDto dto)
        {
            var user = new User(dto.name, dto.email, dto.tags, dto.emotionalState);
            await this._repo.AddAsync(user);
            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.tags, user.emotionalState);
        }

        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id));

            if (user == null)
            {
                return null;
            }

            //change all field
            user.ChangeName(dto.name);
            user.ChangeTags(dto.tags);

            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.tags, user.emotionalState);
        }

        public async Task<UserDto> InactiveAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            //change all fields
            user.MarkAsInative();

            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.tags, user.emotionalState);
        }

        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;

            if (user.Active)
                throw new BusinessRuleValidationException("It is not possible to delete an active category.");

            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.tags, user.emotionalState);
        }
    }
}
