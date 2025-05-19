using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;

namespace WorkshopBooking.Application.Features.BookingFeature.Queries.GetBookingByIdQuery
{
    public class GetBookingByIdQuery : IRequest<OperationResult<BookingDto>>
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }

        public GetBookingByIdQuery(int bookingId, int userId, bool isAdmin)
        {
            BookingId = bookingId;
            UserId = userId;
            IsAdmin = isAdmin;
        }
    }
}
