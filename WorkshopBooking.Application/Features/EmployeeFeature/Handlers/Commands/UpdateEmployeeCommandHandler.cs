using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, OperationResult<EmployeeDto>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IGenericInterface<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {                 
                var existingEmployee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
                if (existingEmployee == null)
                {
                    return OperationResult<EmployeeDto>.Failure("Employee not found.");
                }

                // Map the updated properties from the request to the existing employee
                _mapper.Map(request.EmployeeInputDto, existingEmployee);

                // Update the employee in the repository
                await _employeeRepository.UpdateAsync(existingEmployee);

                // Map the updated employee to a DTO
                var employeeDto = _mapper.Map<EmployeeDto>(existingEmployee);

                return OperationResult<EmployeeDto>.Success(employeeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeDto>.Failure($"An error occurred while updating the employee: {ex.Message}");
            }
        }
    }
}
