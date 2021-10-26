using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using System.Runtime.Versioning;
using System.Dynamic;

namespace DDDNetCore.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class IntroductionsController : ControllerBase
    {
        private readonly IntroductionService _service;

        public IntroductionsController(IntroductionService service){
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
       /* [HttpPost]
        public async Task<ActionResult<IntroductionDto>> Create(CreatingIntroductionDto dto)
        {
            try
            {
                var intro = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = prod.Id }, prod);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        **/


    }
}

