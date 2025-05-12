using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands
{
    public class RegisterEmployeeCommand : IRequest<OperationResult<EmployeeWithUserDto>>
    {
        public RegisterEmployeeDto RegisterEmployeeDto { get; set; }

        public RegisterEmployeeCommand(RegisterEmployeeDto registerEmployeeDto)
        {
            RegisterEmployeeDto = registerEmployeeDto;
        }
    }
}
