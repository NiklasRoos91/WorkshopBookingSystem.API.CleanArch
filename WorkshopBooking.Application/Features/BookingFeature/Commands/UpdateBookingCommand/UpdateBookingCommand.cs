using MediatR;
using System.Text.Json.Serialization;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.UpdateBookingCommand
{
    public class UpdateBookingCommand : IRequest<OperationResult<BookingDto>>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public UpdateBookingDto UpdateBookingDto { get; set; }

        public UpdateBookingCommand(int bookingId, int userId, UpdateBookingDto updateBookingDto)
        {
            BookingId = bookingId;
            UserId = userId;
            UpdateBookingDto = updateBookingDto;
        }
    }
}
