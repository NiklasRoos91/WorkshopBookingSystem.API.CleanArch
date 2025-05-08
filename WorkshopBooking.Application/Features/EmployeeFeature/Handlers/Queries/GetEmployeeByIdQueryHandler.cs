using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, OperationResult<EmployeeDto>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IGenericInterface<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(request.Id);
                if (employee == null)
                {
                    return OperationResult<EmployeeDto>.Failure("Employee not found");
                }

                var employeeDto = _mapper.Map<EmployeeDto>(employee);

                return OperationResult<EmployeeDto>.Success(employeeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
