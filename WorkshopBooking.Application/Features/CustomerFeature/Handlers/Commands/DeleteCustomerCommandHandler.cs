using MediatR;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, OperationResult<bool>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;

        public DeleteCustomerCommandHandler(IGenericInterface<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _customerRepository.ExistsAsync(request.CustomerId);

                if (!exists)
                {
                    return OperationResult<bool>.Failure($"Customer with ID {request.CustomerId} not found.");
                }

                var result = await _customerRepository.DeleteAsync(request.CustomerId);

                return OperationResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting customer: {ex.Message}");
            }
        }
    }
}
