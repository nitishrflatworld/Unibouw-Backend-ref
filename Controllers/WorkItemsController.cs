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
    public class WorkItemsController : ControllerBase
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;


        public WorkItemsController(IWorkItemService workItemService, IMapper mapper)
        {
            _workItemService = workItemService;
            _mapper = mapper;

        }
        [HttpPost]
        public ActionResult<WorkItemDTO> Create(WorkItemDTO itemDto)
        {
            if (itemDto == null)
                return BadRequest("WorkItem cannot be null");

            // Map DTO -> Model
            var item = _mapper.Map<WorkItems>(itemDto);

            var createdItem = _workItemService.Add(item);

            // Map Model -> DTO
            var createdDto = _mapper.Map<WorkItemDTO>(createdItem);

            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        // GET: api/WorkItems/{id}
        [HttpGet("{id}")]
        public ActionResult<WorkItemDTO> GetById(int id)
        {
            var item = _workItemService.GetById(id);

            if (item == null)
                return NotFound();

            // Map Model -> DTO
            var dto = _mapper.Map<WorkItemDTO>(item);

            return Ok(dto);
        }

        // GET: api/WorkItems
        [HttpGet]
        public ActionResult<IEnumerable<WorkItemDTO>> GetAll()
        {
            var items = _workItemService.GetAll();
            var dtos = _mapper.Map<IEnumerable<WorkItemDTO>>(items);

            return Ok(dtos);
        }
    }
}
