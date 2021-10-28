using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;
using System.Runtime.Versioning;
using System.Dynamic;

namespace DDDNetCore.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly MissionService _service;

        public MissionsController(MissionService service){
            _service = service;
        }

        // GET: api/Introductions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Introductions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MissionDto>> GetById(Guid id)
        {
            var mission = await _service.GetByIdAsync(new MissionId(id));

            if (mission == null)
            {
                return NotFound();
            }

            return mission;
        }

        // POST: api/Introductions
       [HttpPost]
        public async Task<ActionResult<MissionDto>> Create(CreatingMissionDto dto)
        {
            try
            {
                var mission = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = mission.Id }, mission);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // PUT: api/Introductions/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MissionDto>> Update(Guid id, MissionDto dto)
        {
            if (id.Equals(dto.Id))
            {
                return BadRequest();
            }

            try
            {
                var mission = await _service.UpdateAsync(dto);
                
                if (mission == null)
                {
                    return NotFound();
                }
                return Ok(mission);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // Inactivate: api/Introductions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MissionDto>> SoftDelete(Guid id)
        {
            var mission = await _service.InactivateAsync(new MissionId(id));

            if (mission == null)
            {
                return NotFound();
            }

            return Ok(mission);
        }

        // DELETE: api/Introductions/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<MissionDto>> HardDelete(Guid id)
        {
            try
            {
                var mission = await _service.DeleteAsync(new MissionId(id));

                if (mission == null)
                {
                    return NotFound();
                }

                return Ok(mission);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
        


    }
}