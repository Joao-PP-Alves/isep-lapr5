using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Network;

namespace DDDNetCore.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly IFriendshipService _service;

        public FriendshipsController(FriendshipService service){
            _service = service;
        }

        public FriendshipsController(IFriendshipService service){
            _service = service;
        }


        // GET: api/Friendships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Introductions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendshipDto>> GetById(Guid id)
        {
            var friendship = await _service.GetByIdAsync(new FriendshipId(id));

            if (friendship == null)
            {
                return NotFound();
            }

            return friendship;
        }

        // POST: api/Friendships/id
       [HttpPost]
        public async Task<ActionResult<FriendshipDto>> Create(CreatingFriendshipDto dto)
        {
            try
            {
                var friendship = await _service.AddAsync(dto);
                _service.UpdateFriendsList(friendship, dto.requester.AsGuid());

                return CreatedAtAction(nameof(GetById), new { id = friendship.Id }, friendship);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // PUT: api/Introductions/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FriendshipDto>> Update(Guid id, FriendshipDto dto)
        {
            if (id.Equals(dto.Id))
            {
                return BadRequest();
            }

            try
            {
                var friendship = await _service.UpdateAsync(dto);
                
                if (friendship == null)
                {
                    return NotFound();
                }
                return Ok(friendship);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // Inactivate: api/Introductions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FriendshipDto>> SoftDelete(Guid id)
        {
            var friendship = await _service.InactivateAsync(new FriendshipId(id));

            if (friendship == null)
            {
                return NotFound();
            }

            return Ok(friendship);
        }

        // DELETE: api/Introductions/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<FriendshipDto>> HardDelete(Guid id)
        {
            try
            {
                var friendship = await _service.DeleteAsync(new FriendshipId(id));

                if (friendship == null)
                {
                    return NotFound();
                }

                return Ok(friendship);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }

      /*  public async Task<ActionResult<Network<UserDto,ConnectionDto>>> UserFriendships(int level, Guid userId)
        {
            
            Network<UserDto, ConnectionDto> network = new Network<UserDto, ConnectionDto>(false);
            var friendsList = await _service.GetByUserId(new UserId(userId));
            

            return network;
        } */
            
    }
}