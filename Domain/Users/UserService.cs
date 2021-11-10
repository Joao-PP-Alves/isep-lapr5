using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;

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
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState));

            foreach (var dto in listDto)
            {
                EmotionalState.updateElapsedTime(dto.emotionalState);
            }
            return listDto;
        } 

        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }
            EmotionalState.updateElapsedTime(user.emotionalState);
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState);
        }

        public async Task<UserDto> AddAsync(CreatingUserDto dto)
        {
            var user = new User(dto.name, dto.email, dto.password, dto.phoneNumber, dto.tags, dto.emotionalState);
            await this._repo.AddAsync(user);
            await this._unitOfWork.CommitAsync();
            EmotionalState.updateElapsedTime(dto.emotionalState);
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState);
        }

        /**
        * método para dar atualização no perfil
        **/
        public async Task<UserDto> UpdateProfileAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id));

            if (user == null)
            {
                return null;
            }

            //change all field
            user.ChangeName(dto.name);
            user.ChangeTags(dto.tags);
            user.ChangePhoneNumber(dto.phoneNumber);
            user.ChangeEmotionalState(dto.emotionalState);
            user.ChangeEmail(dto.email);
            EmotionalState.updateElapsedTime(dto.emotionalState);
            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState);
        }

        public async Task<UserDto> InactivateAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id); 

            if (user == null)
                return null;   

            user.MarkAsInative();
            
            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState);
        }

        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;

            // Mark as inactive. An exception may be thrown in the User class
            user.MarkAsInative();

            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState);
        }

         public async Task<UserDto> UpdateEmotionalStateAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id));

            if (user == null)
            {
                return null;
            }

            //change all field
            user.ChangeEmotionalState(dto.emotionalState);
            EmotionalState.updateElapsedTime(dto.emotionalState);
            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState);
        }
    }
}
