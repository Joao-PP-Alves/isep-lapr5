using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using System.Runtime.Versioning;
using System.Dynamic;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;


namespace DDDNetCore.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class IntroductionsController : ControllerBase
    {
        private readonly IIntroductionService _service;
        public IntroductionsController(IIntroductionService service){
            _service = service;
        }

        // GET: api/Introductions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntroductionDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Introductions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IntroductionDto>> GetById(Guid id)
        {
            var intro = await _service.GetByIdAsync(new IntroductionId(id));

            if (intro == null)
            {
                return NotFound();
            }

            return intro;
        }

        // POST: api/Introductions
       [HttpPost]
        public async Task<ActionResult<IntroductionDto>> Create(CreatingIntroductionDto dto)
        {
            try
            {
                var intro = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = intro.Id }, intro);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // PUT: api/Introductions/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IntroductionDto>> Update(Guid id, IntroductionDto dto)
        {
            if (id != dto.Id)
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

        // Inactivate: api/Introductions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IntroductionDto>> SoftDelete(Guid id)
        {
            var intro = await _service.InactivateAsync(new IntroductionId(id));

            if (intro == null)
            {
                return NotFound();
            }

            return Ok(intro);
        }

        // DELETE: api/Introductions/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<IntroductionDto>> HardDelete(Guid id)
        {
            try
            {
                var intro = await _service.DeleteAsync(new IntroductionId(id));

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
        
        [HttpGet("pendent/{id}")]
        public async Task<ActionResult<IEnumerable<IntroductionDto>>> GetPendent(Guid id){
            return await _service.GetPendentIntroductions(new UserId(id));
        }

        [HttpGet("pendentOnlyIntermediate/{id}")]
        public async Task<ActionResult<IEnumerable<IntroductionDto>>> GetPendentOnlyIntermediate(Guid id){
            return await _service.GetPendentIntroductionsOnlyIntermediate(new UserId(id));
        }

        [HttpGet("pendentOnlyTargetUser/{id}")]
        public async Task<ActionResult<IEnumerable<IntroductionDto>>> GetPendentOnlyTargetUser(Guid id){
            return await _service.GetPendentIntroductionsOnlyIntermediate(new UserId(id));
        }

        [HttpPut("approve/{id}")]
        public async Task<ActionResult<IntroductionDto>> ApproveIntroduction(Guid id){
            return await _service.ApproveIntroduction(new IntroductionId(id));
        }

        [HttpPut("reprove/{id}")]
        public async Task<ActionResult<IntroductionDto>> ReproveIntroduction(Guid id){
            return await _service.ReproveIntroduction(new IntroductionId(id));
        }

        [HttpGet("pendentWithNames/{id}")]
        public async Task<ActionResult<List<IntroductionWithUsersNamesDto>>> GetPendentWithNames(Guid id){
            return await _service.GetPendentIntroductionsWithUsernames(new UserId(id));
        }
        


    }
}

