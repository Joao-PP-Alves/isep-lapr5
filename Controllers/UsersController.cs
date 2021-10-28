using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase{
        private readonly UserService _service;

        /**
        * contrutor do controller  
        **/
        public UsersController(UserService service){
            _service = service;
        }

        //GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(){
            return await _service.GetAllAsync();
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

        // POST: api/Products
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
                var showUser = GetGetById(id);  //para mostrar as informações do perfil do user antes de as alterar

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
    }
}