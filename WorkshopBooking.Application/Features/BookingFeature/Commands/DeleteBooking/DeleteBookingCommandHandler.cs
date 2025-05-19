using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, OperationResult<bool>>
    {
        private readonly IGenericInterface<Booking> _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteBookingCommandHandler(
            IGenericInterface<Booking> bookingRepository,
            IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByUserIdAsync(request.UserId);
                if (employee == null)
                {
                    return OperationResult<bool>.Failure("Användaren har inte en kopplad Employee.");
                }

                var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
                if (booking == null)
                {
                    return OperationResult<bool>.Failure($"Booking with ID {request.BookingId} not found.");
                }

                if (!request.IsAdmin && booking.EmployeeId != employee.EmployeeId)
                {
                    return OperationResult<bool>.Failure("Du har inte behörighet att ta bort denna bokning.");
                }

                var bookingDeleted = await _bookingRepository.DeleteAsync(request.BookingId);
                if (!bookingDeleted)
                {
                    return OperationResult<bool>.Failure($"Failed to delete Booking with ID {request.BookingId}.");
                }

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the Booking: {ex.Message}");
            }
        }
    }
}
