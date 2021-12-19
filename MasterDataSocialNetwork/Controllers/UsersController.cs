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

                if (user == null)
                {
                    // Email is correct but password is wrong
                    return Unauthorized();
                }

                return CreatedAtAction(nameof(GetGetById), new {id = user.Id}, user);
            }
            catch (Exception ex)
            {
                // User does not exist
                return NotFound("User");
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

        // GET: api/Users/NetworkSize/123812-f-093123/1
        [HttpGet("NetworkSize/{id}/{level}")]
        public async Task<ActionResult<NSizeResponseDTO>> GetNetworkSize(Guid id, int level)
        {
            try
            {
                var tamanho = await _service.GetNetworkSize(new UserId(id), level);

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


        // GET: api/Users/NetworkDimensionSize
        [HttpGet("NetworkDimensionSize/{userId}/{level}")]
        public async Task<ActionResult<NSizeResponseDTO>> GetNetworkDimensionSize(Guid userId, int level)
        {
            try
            {
                var tamanho = await _service.GetNetworkDimensionSize(new UserId(userId), level);

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

        // GET: api/Users/ShortestPath
        [HttpGet("ShortestPath/{userId1}/{userId2}")]
        public async Task<ActionResult<NSizeResponseDTO>> GetShortestPath(Guid userId1, Guid userId2)
        {
            try
            {
                var tamanho = await _service.GetShortestPath(new UserId(userId1), new UserId(userId2));

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

        // GET: api/Users/NetworkNSizeDTO
        [HttpGet("LeaderboardNetworkSize/{N}")]
        public async Task<ActionResult<List<LeaderboardUserNetworkSizeDto>>> GetLeaderboardNetworkSize(int N)
        {
            try
            {
                var leaderboard = await _service.GetLeaderBoardNetworkSize(N);

                if (leaderboard == null)
                {
                    return NotFound();
                }
    
                return Ok(leaderboard);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
        
        // GET: api/Users/AllUsersTagCloud
        [HttpGet("AllUsersTagCloud")]
        public async Task<ActionResult<List<TagCloudDto>>> GetAllUsersTagCloud()
        {
            try
            {
                var tagCloud = await _service.GetAllUsersTagCloud();

                if (tagCloud == null)
                {
                    return NotFound();
                }
    
                return Ok(tagCloud);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        // GET: api/Users/AllFriendshipsTagCloud
        [HttpGet("AllFriendshipsTagCloud")]
        public async Task<ActionResult<List<TagCloudDto>>> GetAllFriendshipsTagCloud()
        {
            try
            {
                var tagCloud = await _service.GetAllFriendshipsTagCloud();

                if (tagCloud == null)
                {
                    return NotFound();
                }
    
                return Ok(tagCloud);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        // GET: api/Users/MyTagCloud/id
        [HttpGet("MyTagCloud/{id}")]
        public async Task<ActionResult<List<TagCloudDto>>> GetMyTagCloud(Guid userId)
        {
            try
            {
                var tagCloud = await _service.GetMyTagCloud(new UserId(userId));

                if (tagCloud == null)
                {
                    return NotFound();
                }
    
                return Ok(tagCloud);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        // GET: api/Users/MyFriendshipsTagCloud/id
        [HttpGet("MyFriendshipsTagCloud/{id}")]
        public async Task<ActionResult<List<TagCloudDto>>> GetMyFriendshipsTagCloud(Guid userId)
        {
            try
            {
                var user = await _service.GetByIdAsync(new UserId(userId));
                if (user == null)
                {
                    return NotFound();
                }
                var tagCloud = await _service.GetMyFriendshipsTagCloud(new UserId(user.Id));

                if (tagCloud == null)
                {
                    return NotFound();
                }
    
                return Ok(tagCloud);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}