using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Queries
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, OperationResult<List<EmployeeDto>>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IGenericInterface<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

                return OperationResult<List<EmployeeDto>>.Success(employeeDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<List<EmployeeDto>>.Failure($"An error occurred while retrieving employees: {ex.Message}");
            }
        }
    }
}
