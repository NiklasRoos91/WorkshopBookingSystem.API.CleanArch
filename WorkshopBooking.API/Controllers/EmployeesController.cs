using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries;
using WorkshopBooking.Domain.Entities;

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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]  // Endast Admin kan skapa en Employee
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

        // GET: api/Employees
        [HttpGet]
        [Authorize(Roles = "Admin")]  // Admin kan hämta listan
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
        [Authorize(Roles = "Admin,Employee")]  // Admin eller Employee kan hämta en specifik Employee
        public async Task<ActionResult<EmployeeWithUserDto>> GetEmployee(int employeeId)
        {
            // Kontrollera om den inloggade användaren försöker hämtar sin egen data
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (employeeIdClaim != employeeId.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var query = new GetEmployeeByIdQuery(employeeId);
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
        [Authorize(Roles = "Admin,Employee")] // Employee och Admin kan uppdatera
        public async Task<IActionResult> PutEmployee(int employeeId, [FromBody] UpdateEmployeeWithUserDto updateEmployeeWithUserDto)
        {
            // Kontrollera om den inloggade användaren försöker uppdatera sin egen data
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (employeeIdClaim != employeeId.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var command = new UpdateEmployeeWithUserCommand(employeeId, updateEmployeeWithUserDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]  // Endast Admin kan radera en Employee
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
