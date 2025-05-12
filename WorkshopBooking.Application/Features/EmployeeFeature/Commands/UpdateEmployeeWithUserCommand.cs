using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;


namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands
{
    public class UpdateEmployeeWithUserCommand : IRequest<OperationResult<EmployeeWithUserDto>>
    {
        public int EmployeeId { get; set; }
        public UpdateEmployeeWithUserDto UpdateEmployeeWithUserDto { get; set; }

        public UpdateEmployeeWithUserCommand(int employeeId, UpdateEmployeeWithUserDto updateEmployeeWithUserDto)
        {
            EmployeeId = employeeId;
            UpdateEmployeeWithUserDto = updateEmployeeWithUserDto;
        }
    }
}
