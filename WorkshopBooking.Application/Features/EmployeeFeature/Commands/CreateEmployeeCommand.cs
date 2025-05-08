using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands
{
    public class CreateEmployeeCommand : IRequest<OperationResult<EmployeeDto>>
    {
        public EmployeeInputDto EmployeeInputDto { get; set; }

        public CreateEmployeeCommand(EmployeeInputDto employeeInputDto)
        {
            EmployeeInputDto = employeeInputDto;
        }
    }
}
