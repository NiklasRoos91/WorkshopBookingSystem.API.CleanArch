using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands
{
    public class DeleteCustomerCommand : IRequest<OperationResult<bool>>
    {
        public int CustomerId { get; set; }

        public DeleteCustomerCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
