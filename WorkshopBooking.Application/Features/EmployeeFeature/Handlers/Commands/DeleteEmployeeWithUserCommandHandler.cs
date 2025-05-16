using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Commands
{
    public class DeleteEmployeeWithUserCommandHandler : IRequestHandler<DeleteEmployeeWithUserCommand, OperationResult<bool>>
    {
        private readonly IGenericInterface<Employee> _genericEmployeeRepository;
        private readonly IGenericInterface<User> _userRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeWithUserCommandHandler(
            IGenericInterface<Employee> genericEmployeeRepository,
            IGenericInterface<User> userRepository,
            IEmployeeRepository employeeRepository)
        {
            _genericEmployeeRepository = genericEmployeeRepository;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteEmployeeWithUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByEmployeeIdAsync(request.EmployeeId);

                if (employee == null)
                {
                    return OperationResult<bool>.Failure($"Employee with ID {request.EmployeeId} not found.");
                }

                var userId = employee.UserId;

                var employeeDeleted = await _genericEmployeeRepository.DeleteAsync(employee.EmployeeId);
                if (!employeeDeleted)
                {
                    return OperationResult<bool>.Failure($"Failed to delete employee with ID {request.EmployeeId}.");
                }

                var userDeleted = await _userRepository.DeleteAsync(userId);
                if (!userDeleted)
                {
                    return OperationResult<bool>.Failure($"Failed to delete associated user with ID {userId}.");
                }

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the employee: {ex.Message}");
            }
        }
    }
}
