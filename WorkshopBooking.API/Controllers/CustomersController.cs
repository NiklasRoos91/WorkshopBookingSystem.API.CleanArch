using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs.WorkshopBooking.Application.Features.CustomerFeature.DTOs;
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

        [HttpPost("register")]
        public async Task<ActionResult<CustomerWithUserDto>> RegisterCustomer([FromBody] RegisterCustomerDto registerCustomerDto)
        {
            var command = new RegisterCustomerCommand(registerCustomerDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // GET: api/Customers
        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]  // Endast Admin eller Employee kan hämta alla Customer
        public async Task<ActionResult<IEnumerable<CustomerWithUserDto>>> GetCustomers()
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
        [HttpGet("{customerId}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<ActionResult<CustomerWithUserDto>> GetCustomer(int customerId)
        {
            // Kontrollera om den inloggade användaren försöker hämta sin egen data
            var customerIdClaim = User.FindFirst("CustomerId")?.Value;
            if (customerIdClaim != customerId.ToString() && !User.IsInRole("Admin") && !User.IsInRole("Employee"))
            {
                return Forbid();
            }


            var query = new GetCustomerByIdQuery(customerId);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/Customers/filtered?filter=someFilter&sort=asc
        [HttpGet("filtered")]
        [Authorize(Roles = "Admin,Employee")]  // Endast Admin eller Employee kan hämta en Customer med filter och sortering

        public async Task<ActionResult<IEnumerable<CustomerWithUserDto>>> GetFilteredCustomers(
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
        [HttpPut("{customerId}")]
        [Authorize(Roles = "Admin,Customer,Employee")]  // Alla kan uppdatera
        public async Task<IActionResult> PutCustomer(int customerId, [FromBody]UpdateCustomerWithUserDto updateCustomerWithUserDto)
        {
            // Kontrollera om den inloggade användaren försöker uppdatera sin egen data
            var customerIdClaim = User.FindFirst("CustomerId")?.Value;
            if (customerIdClaim != customerId.ToString() && !User.IsInRole("Admin") && !User.IsInRole("Employee"))
            {
                return Forbid();
            }

            var command = new UpdateCustomerWithUserCommand(customerId, updateCustomerWithUserDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{customerId}")]
        [Authorize(Roles = "Admin")]  // Endast Admin kan radera en Customer
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var command = new DeleteCustomerWithUserCommand(customerId);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
