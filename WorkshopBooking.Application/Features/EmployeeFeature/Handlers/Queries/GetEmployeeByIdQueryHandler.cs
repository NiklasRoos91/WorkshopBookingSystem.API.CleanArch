using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, OperationResult<EmployeeWithUserDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<EmployeeWithUserDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingEmployee = await _employeeRepository.GetEmployeeWithUserByEmployeeIdAsync(request.EmployeeId);
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
                    return OperationResult<EmployeeWithUserDto>.Failure("You do not have permission to see this employee.");
                }

                var employeeWithUserDto = _mapper.Map<EmployeeWithUserDto>(existingEmployee);

                return OperationResult<EmployeeWithUserDto>.Success(employeeWithUserDto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeWithUserDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
