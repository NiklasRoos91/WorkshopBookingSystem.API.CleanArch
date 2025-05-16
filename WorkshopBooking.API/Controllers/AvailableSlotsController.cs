using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using Microsoft.AspNetCore.Authorization;
using WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.CreateAvailableSlotCommand;
using WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.DeleteAvailableSlotsCommand;
using WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.UpdateAvailableSlotCommand;
using WorkshopBooking.Application.Features.AvailableSlotFeature.Queries.GetAllAvailableSlotsQuery;
using WorkshopBooking.Application.Features.AvailableSlotFeature.Queries.GetAvailableSlotByIdQuery;
using WorkshopBooking.API.Helpers;

namespace WorkshopBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableSlotsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AvailableSlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/AvailableSlots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvailableSlot>>> GetAvailableSlots()
        {
            var query = new GetAllAvailableSlotsQuery();
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/AvailableSlots/5
        [HttpGet("{availableSlotId}")]
        public async Task<ActionResult<AvailableSlot>> GetAvailableSlot(int availableSlotId)
        {
            var query = new GetAvailableSlotByIdQuery(availableSlotId);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // PUT: api/AvailableSlots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{availableSlotId}")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> PutAvailableSlot(int availableSlotId, [FromBody] AvailableSlotInputDto availableSlotInputDto)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;

            var isAdmin = User.IsInRole("Admin");

            var command = new UpdateAvailableSlotCommand(availableSlotId, userId, isAdmin, availableSlotInputDto);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        // POST: api/AvailableSlots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<AvailableSlotDto>> PostAvailableSlot([FromBody] AvailableSlotInputDto inputAvailableSlotDto)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;

            var command = new CreateAvailableSlotCommand(userId, inputAvailableSlotDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // DELETE: api/AvailableSlots/5
        [HttpDelete("{availableSlotId}")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> DeleteAvailableSlot(int availableSlotId)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;

            var isAdmin = User.IsInRole("Admin");

            var command = new DeleteAvailableSlotCommand(availableSlotId, userId, isAdmin);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
