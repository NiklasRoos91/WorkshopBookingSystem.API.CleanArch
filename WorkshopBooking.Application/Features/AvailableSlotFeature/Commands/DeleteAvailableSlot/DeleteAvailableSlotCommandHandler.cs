using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.DeleteAvailableSlot
{
    public class DeleteAvailableSlotCommandHandler : IRequestHandler<DeleteAvailableSlotCommand, OperationResult<bool>>
    {
        private readonly IGenericInterface<AvailableSlot> _availableSlotRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteAvailableSlotCommandHandler
            (IGenericInterface<AvailableSlot> availableSlotRepository,
            IEmployeeRepository employeeRepository
            )
        {
            _availableSlotRepository = availableSlotRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByUserIdAsync(request.UserId);
                if (employee == null)
                {
                    return OperationResult<bool>.Failure("Användaren har inte en kopplad Employee.");
                }

                var availableSlot = await _availableSlotRepository.GetByIdAsync(request.AvailableSlotId);
                if (availableSlot == null)
                {
                    return OperationResult<bool>.Failure($"AvailableSlot with ID {request.AvailableSlotId} not found.");
                }

                if (!request.IsAdmin && availableSlot.EmployeeId != employee.EmployeeId)
                {
                    return OperationResult<bool>.Failure("Du har inte behörighet att ta bort denna slot.");
                }

                var availableSlotDeleted = await _availableSlotRepository.DeleteAsync(request.AvailableSlotId);
                if (!availableSlotDeleted)
                {
                    return OperationResult<bool>.Failure($"Failed to delete AvailableSlot with ID {request.AvailableSlotId}.");
                }

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the AvailableSlot: {ex.Message}");
            }
        }
    }
}
