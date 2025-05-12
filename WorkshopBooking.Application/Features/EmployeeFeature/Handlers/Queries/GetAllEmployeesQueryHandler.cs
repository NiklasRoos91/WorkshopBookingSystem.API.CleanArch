using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Queries
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, OperationResult<List<EmployeeWithUserDto>>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IGenericInterface<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<EmployeeWithUserDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync(c => c.User);
                var employeeWithUserDto = _mapper.Map<List<EmployeeWithUserDto>>(employees);

                return OperationResult<List<EmployeeWithUserDto>>.Success(employeeWithUserDto);
            }
            catch (Exception ex)
            {
                return OperationResult<List<EmployeeWithUserDto>>.Failure($"An error occurred while retrieving employees: {ex.Message}");
            }
        }
    }
}
