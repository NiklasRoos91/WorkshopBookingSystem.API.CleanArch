using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands
{
    public class CreateCustomerCommand : IRequest<OperationResult<CustomerDto>>
    {
        public CustomerInputDto CustomerInputDto { get; set; }

        public CreateCustomerCommand(CustomerInputDto customerInputDto)
        {
            CustomerInputDto = customerInputDto;
        }
    }
}
