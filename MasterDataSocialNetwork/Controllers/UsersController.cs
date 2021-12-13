using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using DDDNetCore.Domain.Services;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Network;


namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        private readonly IFriendshipService _serviceFriendships;


        public UsersController(IUserService service, IFriendshipService serviceFriendships)
        {
            _service = service;
            _serviceFriendships = serviceFriendships;
        }

        //GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        //GET: api/users/ByEmail/j@gmail.com
        [HttpGet("ByEmail/{email}")]
        public async Task<ActionResult<UserDto>> GetByEmail(string email)
        {
            UserDto  user;
            try
            {
                user = await _service.GetByEmail(email);
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return user;
        }

        //GET: api/users/ByEmail/j
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByName(string name)
        {
            List<UserDto> users;
            try
            {
                users = await _service.GetByName(name);

                if (users == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return users;
        }

        //GET: api/users/ByTags/tag1.tag2.tag3
        [HttpGet("ByTags/{tags}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByTags(string tags)
        {
            List<UserDto> users;
            try
            {
                users = await _service.GetByTags(tags);
                if (users == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return users;
        }

        //GET: api/Users
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetGetById(Guid id)
        {
            UserDto user;
            try
            {
                user = await _service.GetByIdAsync(new UserId(id));
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }


            return user;
        }

        //GET: api/Users/MyFriends/(GUID)/1
        [HttpGet("MyFriends/{id}/{level}")]
        // Level is the depth of the friendships
        public async Task<ActionResult<Network<UserDto, FriendshipDto>>> GetMyFriends(Guid id, int level)
        {
            UserDto user;
            Network<UserDto, FriendshipDto> net;
            try
            {
                user = await _service.GetByIdAsync(new UserId(id));
                if (user == null)
                {
                    return NotFound();
                }

                net = await _service.GetMyFriends(new UserId(user.Id),
                    new Network<UserDto, FriendshipDto>(false), level);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return net;
        }

        [HttpGet("Friendships/{id}")]
        public async Task<ActionResult<List<FriendshipWithFriendDto>>> GetFriendshipsByUserId(Guid id){
            List<FriendshipWithFriendDto> list;
            try
            {
                list = await _serviceFriendships.GetByUserIdWithFriend(new UserId(id));
                if (list == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }


            return list;
        }


        //GET: api/Users/GetPossibleIntroductionTargets/1/2
        [HttpGet("GetPossibleIntroductionTargets/{id}/{id2}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetPossibleIntroductionTargets(Guid id, Guid id2)
        {
            var user = await _service.GetByIdAsync(new UserId(id));
            var user2 = await _service.GetByIdAsync(new UserId(id));
            if (user == null || user2 == null)
            {
                return NotFound();
            }

            return await _service.GetPossibleIntroductionTargets(new UserId(user.Id), new UserId(user2.Id));
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreatingUserDto dto)
        {
            try
            {
                var user = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetGetById), new {id = user.Id}, user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO dto)
        {
            try
            {
                var user = await _service.Login(dto);

                return CreatedAtAction(nameof(GetGetById), new {id = user.Id}, user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return Forbid();
            }
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateProfile(Guid id, UserDto dto)
        {
            /*if (id != dto.Id)
            {
                return BadRequest();
            }*/
            dto.Id = id;
            try
            {
                var showUser = await GetGetById(id); //para mostrar as informações do perfil do user antes de as alterar

                var user = await _service.UpdateProfileAsync(dto);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // PUT: api/Users/5/EmotionalState
        [HttpPut("{id}/EmotionalState")]
        public async Task<ActionResult<UserDto>> UpdateEmotionalState(Guid id, UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var showUser = await GetGetById(id); //para mostrar as informações do perfil do user antes de as alterar

                var user = await _service.UpdateEmotionalStateAsync(dto);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // Inactivate: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> SoftDelete(Guid id)
        {
            var user = await _service.InactivateAsync(new UserId(id));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<UserDto>> HardDelete(Guid id)
        {
            try
            {
                var user = await _service.DeleteAsync(new UserId(id));

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        //GET: api/Users/GetFriendsSuggestion/1
        [HttpGet("GetFriendsSuggestion/{id}")]
        public async Task<ActionResult<List<UserDto>>> GetFriendsSuggestion(Guid id)
        {
            var user = await _service.GetByIdAsync(new UserId(id));
            if (user == null)
            {
                return NotFound();
            }

            return await _service.GetFriendsSuggestionForNewUsers(new UserId(id));
        }

        [HttpPost("NewFriendship")]
        public async Task<ActionResult<FriendshipDto>> NewFriendship(CreatingFriendshipDto dto)
        {
            try
            {
                return await _service.NewFriendship(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
		//GET: api/Users/MyPerspective/id/param
        [HttpGet("MyPerspective/{id}/{param}")]
        public async Task<ActionResult<List<UserPerspectiveDto>>> getPerspective(
            Guid id, int param)
        {
            try
            {
                var user = await _service.GetByIdAsync(new UserId(id));
                if (user == null)
                {
                    return NotFound();
                }
                
                return await _service.MyPerspective(new UserId(id),param);
            }
            catch (Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

		[HttpPut("ConnectionStrength/{userId}")]
        public async Task<ActionResult<FriendshipDto>> UpdateFriendshipConnectionStrength(Guid userId,UpdateFriendshipConnectionStrengthDto dto){
            try
            {
                return await _serviceFriendships.UpdateFriendshipConnectionStrength(userId,dto.Id,dto.connection_strength);
            }
            catch (Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpPut("Tag/{userId}")]
        public async Task<ActionResult<FriendshipDto>> UpdateFriendshipTag(Guid userId,UpdateFriendshipTagDto dto){
            try
            {
                return await _serviceFriendships.UpdateFriendshipTag(userId,dto.Id,dto.tag);
            }
            catch (Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // GET: api/Users/NetworkNSizeDTO
        [HttpGet("NetworkNSizeDTO")]
        public async Task<ActionResult<NSizeResponseDTO>> GetNetowrkSize(NetworkNSizeDTO dto)
        {
            try
            {
                var tamanho = await _service.GetNetworkSize(dto);

                if (tamanho == null)
                {
                    return NotFound();
                }

                return Ok(tamanho);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }    
    }
}