using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Commands;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Queries;

namespace WorkshopBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/ServiceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceTypeDto>>> GetServiceTypes()
        {
            var query = new GetAllServiceTypesQuery();
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/ServiceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceTypeDto>> GetServiceType(int id)
        {
            var query = new GetServiceTypeByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // PUT: api/ServiceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]  // Endast admin kan uppdatera
        public async Task<IActionResult> PutServiceType(int id, [FromBody] ServiceTypeInputDto serviceTypeInputDto)
        {
            var command = new UpdateServiceTypeCommand(id, serviceTypeInputDto);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // POST: api/ServiceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]  // Endast admin kan skapa
        public async Task<ActionResult<ServiceTypeDto>> PostServiceType( [FromBody] ServiceTypeInputDto serviceTypeInputDto)
        {
            var command = new CreateServiceTypeCommand(serviceTypeInputDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // DELETE: api/ServiceTypes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]  // Endast admin kan ta bort
        public async Task<IActionResult> DeleteServiceType(int id)
        {
            var command = new DeleteServiceTypeCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
