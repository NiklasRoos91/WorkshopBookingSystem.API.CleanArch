using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands
{
    public class DeleteCustomerWithUserCommand : IRequest<OperationResult<bool>>
    {
        public int CustomerId { get; set; }

        public DeleteCustomerWithUserCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
