using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands.UpdateEmployee
{
    public class UpdateEmployeeWithUserCommandHandler : IRequestHandler<UpdateEmployeeWithUserCommand, OperationResult<EmployeeWithUserDto>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;
        private readonly IGenericInterface<User> _userRepository;
        private readonly IEmployeeRepository _employeeRepositoryWithUser;
        private readonly IMapper _mapper;

        public UpdateEmployeeWithUserCommandHandler(
            IGenericInterface<Employee> employeeRepository,
            IGenericInterface<User> userRepository,
            IEmployeeRepository employeeRepositoryWithUser,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _employeeRepositoryWithUser = employeeRepositoryWithUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<EmployeeWithUserDto>> Handle(UpdateEmployeeWithUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingEmployee = await _employeeRepositoryWithUser.GetEmployeeWithUserByEmployeeIdAsync(request.EmployeeId);
                if (existingEmployee == null)
                {
                    return OperationResult<EmployeeWithUserDto>.Failure("Employee not found.");
                }

                var user = existingEmployee.User;
                if (user == null)
                {
                    return OperationResult<EmployeeWithUserDto>.Failure("User associated with the customer not found.");
                }

                if (!request.IsAdmin && existingEmployee.UserId != request.UserId)
                {
                    return OperationResult<EmployeeWithUserDto>.Failure("You do not have permission to update this employee.");
                }

                // Map the updated properties from the request to the existing employee
                _mapper.Map(request.UpdateEmployeeWithUserDto, existingEmployee);
                _mapper.Map(request.UpdateEmployeeWithUserDto, user);

                // Update the employee and user in the repository
                await _employeeRepository.UpdateAsync(existingEmployee);
                await _userRepository.UpdateAsync(user);

                // Map the updated employee to a DTO
                var employeeDto = _mapper.Map<EmployeeWithUserDto>(existingEmployee);

                return OperationResult<EmployeeWithUserDto>.Success(employeeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeWithUserDto>.Failure($"An error occurred while updating the employee: {ex.Message}");
            }
        }
    }
}
