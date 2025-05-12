using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AuthFeature.DTOs;

namespace WorkshopBooking.Application.Features.AuthFeature.Commands
{
    public class LoginCommand : IRequest<OperationResult<string>>
    {
        public LoginDto LoginDto { get; set; }

        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto; 
        }
    }
}
