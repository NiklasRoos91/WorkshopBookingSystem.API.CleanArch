using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;


namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands
{
    public class UpdateEmployeeCommand : IRequest<OperationResult<EmployeeDto>>
    {
        public int EmployeeId { get; set; }
        public EmployeeInputDto EmployeeInputDto { get; set; }

        public UpdateEmployeeCommand(int employeeId, EmployeeInputDto employeeInputDto)
        {
            EmployeeId = employeeId;
            EmployeeInputDto = employeeInputDto;
        }
    }
}
