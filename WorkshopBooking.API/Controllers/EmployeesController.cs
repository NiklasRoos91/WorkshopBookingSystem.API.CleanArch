using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkshopBooking.API.Helpers;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands.DeleteEmployee;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands.RegisterEmployee;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands.UpdateEmployee;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries.GetAllEmployees;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries.GetEmployeeById;

namespace WorkshopBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Employees
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<EmployeeWithUserDto>>> GetEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/Employees/5
        [HttpGet("{employeeId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<EmployeeWithUserDto>> GetEmployee(int employeeId)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;
            var isAdmin = User.IsInRole("Admin");

            var query = new GetEmployeeByIdQuery(employeeId, userId, isAdmin);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{employeeId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> PutEmployee(int employeeId, [FromBody] UpdateEmployeeWithUserDto updateEmployeeWithUserDto)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;
            var isAdmin = User.IsInRole("Admin");

            var command = new UpdateEmployeeWithUserCommand(employeeId, userId, isAdmin, updateEmployeeWithUserDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeWithUserDto>> RegisterEmployee([FromBody] RegisterEmployeeDto registerEmployeeDto)
        {
            if (registerEmployeeDto == null)
            {
                return BadRequest("Invalid employee data.");
            }

            var command = new RegisterEmployeeCommand(registerEmployeeDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var command = new DeleteEmployeeWithUserCommand(employeeId);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
