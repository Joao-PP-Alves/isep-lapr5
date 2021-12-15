using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Tags;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
         private readonly TagService _service;

        public TagsController(TagService service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetGetById(Guid id)
        {
            var prod = await _service.GetByIdAsync(new TagId(id));

            if (prod == null)
            {
                return NotFound();
            }

            return prod;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<TagDto>> Create(CreatingTagDto dto)
        {
            try
            {
                var tag = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetGetById), new { id = tag.Id }, tag);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TagDto>> Update(Guid id, TagDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var tag = await _service.UpdateAsync(dto);
                
                if (tag == null)
                {
                    return NotFound();
                }
                return Ok(tag);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // Inactivate: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TagDto>> SoftDelete(Guid id)
        {
            var prod = await _service.InactivateAsync(new TagId(id));

            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }
        
        // DELETE: api/Products/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<TagDto>> HardDelete(Guid id)
        {
            try
            {
                var prod = await _service.DeleteAsync(new TagId(id));

                if (prod == null)
                {
                    return NotFound();
                }

                return Ok(prod);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
    }
    }
