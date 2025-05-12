using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkshopBooking.Application.Features.AuthFeature.Commands;
using WorkshopBooking.Application.Features.AuthFeature.DTOs;

namespace WorkshopBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand(loginDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new { Token = result.Data });
            }

            return Unauthorized(result.ErrorMessage);
        }
    }
}
