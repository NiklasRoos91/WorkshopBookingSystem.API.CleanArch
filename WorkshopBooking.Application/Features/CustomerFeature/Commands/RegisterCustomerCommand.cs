using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands
{
    public class RegisterCustomerCommand : IRequest<OperationResult<CustomerWithUserDto>>
    {
        public RegisterCustomerDto RegisterCustomerDto { get; set; }

        public RegisterCustomerCommand(RegisterCustomerDto registerCustomerDto)
        {
            RegisterCustomerDto = registerCustomerDto;
        }
    }
}
