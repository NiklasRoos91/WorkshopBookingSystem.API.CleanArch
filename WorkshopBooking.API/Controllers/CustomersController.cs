using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkshopBooking.API.Helpers;
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
        [Authorize(Roles = "Admin,Customer,Employee")]
        public async Task<ActionResult<CustomerWithUserDto>> GetCustomer(int customerId)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;

            var isAdmin = User.IsInRole("Admin");
            var isEmployee = User.IsInRole("Employee");

            var query = new GetCustomerByIdQuery(customerId, userId, isAdmin, isEmployee);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/Customers/filtered?filter=someFilter&sort=asc
        [HttpGet("filtered")]
        [Authorize(Roles = "Admin,Employee")]

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
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> PutCustomer(int customerId, [FromBody]UpdateCustomerWithUserDto updateCustomerWithUserDto)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;
            var isAdmin = User.IsInRole("Admin");

            var command = new UpdateCustomerWithUserCommand(customerId, userId, isAdmin, updateCustomerWithUserDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // POST: api/Customers
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

        // DELETE: api/Customers/5
        [HttpDelete("{customerId}")]
        [Authorize(Roles = "Admin")]
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
