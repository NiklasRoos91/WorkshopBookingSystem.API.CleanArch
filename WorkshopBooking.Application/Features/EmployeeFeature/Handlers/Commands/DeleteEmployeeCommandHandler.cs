using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, OperationResult<bool>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;

        public DeleteEmployeeCommandHandler(IGenericInterface<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _employeeRepository.ExistsAsync(request.EmployeeId);

                if (!exists)
                {
                    return OperationResult<bool>.Failure($"Employee with ID {request.EmployeeId} not found.");
                }

                var result = await _employeeRepository.DeleteAsync(request.EmployeeId);

                return OperationResult<bool>.Success(result);

            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the employee: {ex.Message}");
            }
        }
    }
}
