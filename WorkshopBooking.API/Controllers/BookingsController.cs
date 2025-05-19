using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkshopBooking.API.Helpers;
using WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingCommand;
using WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingFromSlotCommand;
using WorkshopBooking.Application.Features.BookingFeature.Commands.DeleteBooking;
using WorkshopBooking.Application.Features.BookingFeature.Commands.UpdateBookingCommand;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Application.Features.BookingFeature.Queries.GetAllBookingsByQuery;
using WorkshopBooking.Application.Features.BookingFeature.Queries.GetBookingByIdQuery;
using WorkshopBooking.Application.Features.BookingFeature.Queries.GetBookingsByEmployeeIdQuery;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Bookings
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var query = new GetAllBookingsByQuery();
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // GET: api/Bookings/5
        [HttpGet("by-booking{bookingId}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<ActionResult<Booking>> GetBooking(int bookingId)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;
            var isAdmin = User.IsInRole("Admin");

            var query = new GetBookingByIdQuery(bookingId, userId, isAdmin);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // ADMIN – gets all bookings for a specific employee
        [HttpGet("by-employee/{employeeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsForEmployee(int employeeId)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }
            int userId = userIdResult.Value;

            var query = new GetBookingsByEmployeeIdQuery(employeeId, userId, true);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        // EMPLOYEE – gets only their own bookings
        [HttpGet("employee-my")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetMyBookingsEmployee()
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;

            var query = new GetBookingsByEmployeeIdQuery(0, userId, false);
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        // CUSTOMER – gets only their own bookings
        // EMPLOYEE – gets only their own bookings
        // ADMIN – gets all bookings for a specific customer
        [HttpGet("customer-my")]
        [Authorize(Roles = "Customer,Employee,Admin")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetMyBookingsCustomer()
        {
            throw new NotImplementedException();
        }

        // Employee – gets only their own bookings
        // Admin – gets all bookings for a specific service type
        [HttpGet("by-serviceType{serviceTypeId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingByServiceTypeId(int serviceTypeId)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{bookingId}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> PutBooking(int bookingId, [FromBody] UpdateBookingDto updateBookingDto )
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;

            var command = new UpdateBookingCommand
            {
                BookingId = bookingId,
                UpdateBookingDto = updateBookingDto,
                UserId = userId

            };

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-from-slot")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<BookingDto>> CreateFromSlotBooking([FromBody] CreateBookingFromSlotCommand command)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            command.UserId = userIdResult.Value;

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            command.UserId = userIdResult.Value;

            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{bookingId}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var userIdResult = UserHelper.GetUserIdFromClaims(User);
            if (userIdResult.Result != null)
            {
                return userIdResult.Result;
            }

            int userId = userIdResult.Value;

            var isAdmin = User.IsInRole("Admin");

            var command = new DeleteBookingCommand(bookingId, userId, isAdmin);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
