using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands
{
    public class CreateBookingCommand : IRequest<OperationResult<int>>
    {
        public int EmployeeId { get; set; }
        public int ServiceTypeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BookingDate { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleRegistrationNumber { get; set; }
    }

}
