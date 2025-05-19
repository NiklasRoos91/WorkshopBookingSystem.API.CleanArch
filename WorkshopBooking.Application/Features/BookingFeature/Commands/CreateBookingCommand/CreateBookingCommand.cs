using MediatR;
using System.Text.Json.Serialization;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingCommand
{
    public class CreateBookingCommand : IRequest<OperationResult<BookingDto>>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public CreateBookingDto CreateBookingDto { get; set; }

        public CreateBookingCommand() { }

        public CreateBookingCommand(int userId, CreateBookingDto createBookingDto)
        {
            UserId = userId;
            CreateBookingDto = createBookingDto;
        }
    }
}
