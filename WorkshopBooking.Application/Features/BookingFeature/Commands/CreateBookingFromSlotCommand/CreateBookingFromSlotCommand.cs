using MediatR;
using System.Text.Json.Serialization;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingFromSlotCommand
{
    public class CreateBookingFromSlotCommand : IRequest<OperationResult<BookingDto>>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public CreateBookingFromSlotDto CreateBookingFromSlotDto { get; set; }

        public CreateBookingFromSlotCommand() { }

        public CreateBookingFromSlotCommand(int userId, CreateBookingFromSlotDto createBookingFromSlotDto)
        {
            UserId = userId;
            CreateBookingFromSlotDto = createBookingFromSlotDto;
        }
    }
}
