using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;

namespace WorkshopBooking.Application.Features.BookingFeature.Queries.GetAllBookingsByQuery
{
    public class GetAllBookingsByQuery : IRequest<OperationResult<List<BookingDto>>>
    {
        public GetAllBookingsByQuery() { }
    }
}
