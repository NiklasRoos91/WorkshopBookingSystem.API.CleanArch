using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands
{
    public class UpdateCustomerCommand : IRequest<OperationResult<CustomerDto>>
    {
        public int CustomerId { get; set; }
        public CustomerInputDto CustomerInputDto { get; set; }

        public UpdateCustomerCommand(int customerId, CustomerInputDto customerInputDto)
        {
            CustomerId = customerId;
            CustomerInputDto = customerInputDto;
        }
    }
}
