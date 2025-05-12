using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs.WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands
{
    public class UpdateCustomerWithUserCommand : IRequest<OperationResult<CustomerWithUserDto>>
    {
        public int CustomerId { get; set; }
        public UpdateCustomerWithUserDto UpdateCustomerWithUserDto { get; set; }

        public UpdateCustomerWithUserCommand(int customerId, UpdateCustomerWithUserDto updateCustomerWithUserDto)
        {
            CustomerId = customerId;
            UpdateCustomerWithUserDto = updateCustomerWithUserDto;
        }
    }
}
