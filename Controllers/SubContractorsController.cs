using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unibouw.DTOs;
using Unibouw.Models;
using Unibouw.Services;

namespace Unibouw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubContractorsController : ControllerBase
    {
        private readonly ISubContractorService _subcontractorService;
        private readonly IMapper _mapper;


        public SubContractorsController(ISubContractorService subContractorService, IMapper mapper)
        {
            _subcontractorService = subContractorService;
            _mapper = mapper;

        }

        [HttpPost]
        public ActionResult<SubContractorDTO> Create(SubContractorDTO itemDto)
        {
            if (itemDto == null)
                return BadRequest("WorkItem cannot be null");

            // Map DTO -> Model
            var item = _mapper.Map<SubContractor>(itemDto);

            var createdItem = _subcontractorService.Add(item);

            // Map Model -> DTO
            var createdDto = _mapper.Map<SubContractorDTO>(createdItem);

            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        // GET: api/WorkItems/{id}
        [HttpGet("{id}")]
        public ActionResult<SubContractorDTO> GetById(int id)
        {
            var item = _subcontractorService.GetById(id);

            if (item == null)
                return NotFound();

            // Map Model -> DTO
            var dto = _mapper.Map<SubContractorDTO>(item);

            return Ok(dto);
        }

        // GET: api/WorkItems
        [HttpGet]
        public ActionResult<IEnumerable<SubContractorDTO>> GetAll()
        {
            var items = _subcontractorService.GetAll();
            var dtos = _mapper.Map<IEnumerable<SubContractorDTO>>(items);

            return Ok(dtos);
        }
    }
}
