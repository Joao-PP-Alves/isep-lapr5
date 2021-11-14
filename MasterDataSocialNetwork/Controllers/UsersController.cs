using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Network;


namespace DDDNetCore.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase{
        private readonly IUserService _service;

        public UsersController(IUserService service){
            _service = service;
        }

        //GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(){
            return await _service.GetAllAsync();
        } 

        //GET: api/users/ByEmail/j@gmail.com
        [HttpGet("ByEmail/{email}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByEmail(string email){
            return await _service.GetByEmail(email);
        }
        
        //GET: api/users/ByEmail/j
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByName(string name){
            return await _service.GetByName(name);
        }

        //GET: api/Users
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetGetById(Guid id){
            var user = await _service.GetByIdAsync(new UserId(id));
            if(user == null){
                return NotFound();
            }
            return user;
        }

        //GET: api/Users/MyFriends/(GUID)/1
        [HttpGet("MyFriends/{id}/{level}")]
        // Level is the depth of the friendships
        public async Task<Task<Network<UserDto, FriendshipDto>>> GetMyFriends(Guid id, int level)
        {
            var user = await _service.GetByIdAsync(new UserId(id));
            if (user == null)
            {
                return null;
            }

            Task<Network<UserDto, FriendshipDto>> net = _service.GetMyFriends(new UserId(user.Id), new Network<UserDto, FriendshipDto>(false), level);
            // NOt implemented
            return net;
        }


        //GET: api/Users/GetPossibleIntroductionTargets/1/2
        [HttpGet("GetPossibleIntroductionTargets/{id}/{id2}")]
        
        public async Task<ActionResult<IEnumerable<UserDto>>> GetPossibleIntroductionTargets(Guid id, Guid id2){
            var user = await _service.GetByIdAsync(new UserId(id));
            if(user == null){
                return NotFound();
            }
            var user2 = await _service.GetByIdAsync(new UserId(id));
            if(user == null){
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

                return CreatedAtAction(nameof(GetGetById), new { id = user.Id }, user);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateProfile(Guid id, UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var showUser = await GetGetById(id);  //para mostrar as informações do perfil do user antes de as alterar

                var user = await _service.UpdateProfileAsync(dto);
                
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

         // PUT: api/Users/5/EmotionalStateUpdate
        [HttpPut("{id}/EmotionalStateUpdate")]
        public async Task<ActionResult<UserDto>> UpdateEmotionalState(Guid id, UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var showUser = await GetGetById(id);  //para mostrar as informações do perfil do user antes de as alterar

                var user = await _service.UpdateEmotionalStateAsync(dto);
                
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch(BusinessRuleValidationException ex)
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
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }

        //GET: api/Users/GetFriendsSuggestion/1
        [HttpGet("GetFriendsSuggestion/{id}")]
        public async Task<ActionResult<IEnumerable<UserId>>> GetFriendsSuggestion(Guid id)
        {
            var user = await GetGetById(id);
            if (user == null)
            {
                return NotFound();
            }
            
            return await _service.GetFriendsSuggestionForNewUsers(new UserId(id));
        }
    }
    
    
}