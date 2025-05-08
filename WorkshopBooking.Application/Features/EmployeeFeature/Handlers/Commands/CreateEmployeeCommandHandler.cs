using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, OperationResult<EmployeeDto>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IGenericInterface<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = _mapper.Map<Employee>(request.EmployeeInputDto);

                await _employeeRepository.AddAsync(employee);

                var employeeDto = _mapper.Map<EmployeeDto>(employee);

                return OperationResult<EmployeeDto>.Success(employeeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeDto>.Failure($"An error occurred while creating employee: {ex.Message}");
            }

        }
    }
}
