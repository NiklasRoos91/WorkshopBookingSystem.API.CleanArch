using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.DeleteBooking
{
     public class DeleteBookingCommand : IRequest<OperationResult<bool>>
    {
        public int BookingId { get; }
        public int UserId { get; }
        public bool IsAdmin { get; }

        public DeleteBookingCommand(int bookingId, int userId, bool isAdmin)
        {
            BookingId = bookingId;
            UserId = userId;
            IsAdmin = isAdmin;
        }
    }
}
