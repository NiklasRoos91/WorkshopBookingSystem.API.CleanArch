using MediatR;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Commands
{
    public class DeleteCustomerWithUserCommandHandler : IRequestHandler<DeleteCustomerWithUserCommand, OperationResult<bool>>
    {
        private readonly IGenericInterface<Customer> _genericCustomerRepository;
        private readonly IGenericInterface<User> _userRepository;
        private readonly ICustomerRepository _customerRepository;


        public DeleteCustomerWithUserCommandHandler(
            IGenericInterface<Customer> genericCustomerRepository,
            IGenericInterface<User> userRepository,
            ICustomerRepository customerRepository)
        {
            _genericCustomerRepository = genericCustomerRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCustomerWithUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerWithUserByCustomerIdAsync(request.CustomerId);

                if (customer == null)
                {
                    return OperationResult<bool>.Failure($"Customer with ID {request.CustomerId} not found.");
                }

                var customerDeleted = await _genericCustomerRepository.DeleteAsync(customer.CustomerId);

                if (!customerDeleted)
                {
                    return OperationResult<bool>.Failure($"Failed to delete customer with ID {request.CustomerId}.");
                }

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting customer and user: {ex.Message}");
            }
        }
    }
}
