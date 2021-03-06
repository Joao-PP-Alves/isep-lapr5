using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Connections;
using System.Runtime.Versioning;
using System.Dynamic;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;


namespace DDDNetCore.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly IConnectionService _service;

        public ConnectionsController(IConnectionService service){
            _service = service;
        }

        // GET: api/Connections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConnectionDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Connections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectionDto>> GetById(Guid id)
        {
            var intro = await _service.GetByIdAsync(new ConnectionId(id));

            if (intro == null)
            {
                return NotFound();
            }

            return intro;
        }

        // GET: api/Connections/pendent/5
        [HttpGet("pendent/{id}")]
        public async Task<ActionResult<IEnumerable<ConnectionWithRequesterDto>>> GetPendent(Guid id){
            return await _service.GetPendentConnections(new UserId(id));
        }

        // POST: api/Connections
       [HttpPost]
        public async Task<ActionResult<ConnectionDto>> Create(CreatingConnectionDto dto)
        {
            
            try
            {
                var connection = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = connection.id }, connection);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpPut("accept/{id}")]
        public async Task<ActionResult<ConnectionDto>> Accept(Guid id){
            try{
                var intro = await _service.Accept(id);

             if (intro == null)
                {
                    return NotFound();
                }
                return Ok(intro);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpPut("decline/{id}")]
        public async Task<ActionResult<ConnectionDto>> Decline(Guid id){
            try{
                var intro = await _service.Decline(id);

             if (intro == null)
                {
                    return NotFound();
                }
                return Ok(intro);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // PUT: api/Connections/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ConnectionDto>> Update(Guid id, ConnectionDto dto)
        {
            if (id != dto.id)
            {
                return BadRequest();
            }

            try
            {
                var intro = await _service.UpdateAsync(dto);
                
                if (intro == null)
                {
                    return NotFound();
                }
                return Ok(intro);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // Inactivate: api/Connections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConnectionDto>> SoftDelete(Guid id)
        {
            var intro = await _service.InactivateAsync(new ConnectionId(id));

            if (intro == null)
            {
                return NotFound();
            }

            return Ok(intro);
        }

        // DELETE: api/Connections/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<ConnectionDto>> HardDelete(Guid id)
        {
            try
            {
                var intro = await _service.DeleteAsync(new ConnectionId(id));

                if (intro == null)
                {
                    return NotFound();
                }

                return Ok(intro);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
        


    }
}
