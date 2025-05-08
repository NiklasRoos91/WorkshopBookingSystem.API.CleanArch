using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.Queries;

namespace WorkshopBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer([FromBody]CustomerInputDto customerInputDto)
        {
            var command = new CreateCustomerCommand(customerInputDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                {
                return Ok(result);
                }

            return BadRequest(result);
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var query = new GetAllCustomersQuery();
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var query = new GetCustomerByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/Customers/filtered?filter=someFilter&sort=asc
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetFilteredCustomers(
            [FromQuery] string? filter = null,
            [FromQuery] string sort = "asc")
        {
            var query = new GetCustomersWithFilterAndSortQuery(filter, sort);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound("result");
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody]CustomerInputDto customerInputDto)
        {
            var command = new UpdateCustomerCommand(id, customerInputDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var command = new DeleteCustomerCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
