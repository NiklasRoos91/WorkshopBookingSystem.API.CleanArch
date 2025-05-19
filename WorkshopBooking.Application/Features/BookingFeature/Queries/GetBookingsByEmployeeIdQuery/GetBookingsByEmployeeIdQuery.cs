using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;

namespace WorkshopBooking.Application.Features.BookingFeature.Queries.GetBookingsByEmployeeIdQuery
{
    public class GetBookingsByEmployeeIdQuery : IRequest<OperationResult<List<BookingDto>>>
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }

        public GetBookingsByEmployeeIdQuery(int employeeId, int userId, bool isAdmin)
        {
            EmployeeId = employeeId;
            UserId = userId;
            IsAdmin = isAdmin;
        }
    }
}
