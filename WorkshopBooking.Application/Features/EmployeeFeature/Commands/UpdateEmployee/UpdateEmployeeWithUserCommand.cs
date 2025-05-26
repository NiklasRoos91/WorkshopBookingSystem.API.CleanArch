using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;


namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands.UpdateEmployee
{
    public class UpdateEmployeeWithUserCommand : IRequest<OperationResult<EmployeeWithUserDto>>
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public UpdateEmployeeWithUserDto UpdateEmployeeWithUserDto { get; set; }


        public UpdateEmployeeWithUserCommand(int employeeId, int userId, bool isAdmin, UpdateEmployeeWithUserDto updateEmployeeWithUserDto)
        {
            EmployeeId = employeeId;
            UserId = userId;
            IsAdmin = isAdmin;
            UpdateEmployeeWithUserDto = updateEmployeeWithUserDto;
        }
    }
}
