using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Commands
{
    public class UpdateCustomerWithUserCommandHandler : IRequestHandler<UpdateCustomerWithUserCommand, OperationResult<CustomerWithUserDto>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IGenericInterface<User> _userRepository;
        private readonly ICustomerRepository _customerRepositoryWithUser;
        private readonly IMapper _mapper;

        public UpdateCustomerWithUserCommandHandler(
            IGenericInterface<Customer> customerRepository,
            IGenericInterface<User> userRepository,
            ICustomerRepository customerRepositoryWithUser,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _customerRepositoryWithUser = customerRepositoryWithUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<CustomerWithUserDto>> Handle(UpdateCustomerWithUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämta Customer och dess associerade User
                var existingCustomer = await _customerRepositoryWithUser.GetCustomerWithUserByIdAsync(request.CustomerId);
                if (existingCustomer == null)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("Customer not found.");
                }

                // Hämta User som är kopplad till Customer
                var user = existingCustomer.User;
                if (user == null)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("User associated with the customer not found.");
                }

                // Uppdatera Customer och User med de nya värdena från DTO:n
                _mapper.Map(request.UpdateCustomerWithUserDto, existingCustomer);
                _mapper.Map(request.UpdateCustomerWithUserDto, user);

                // Uppdatera Customer och User i databasen
                await _customerRepository.UpdateAsync(existingCustomer);
                await _userRepository.UpdateAsync(user);

                // Skapa CustomerDto från den uppdaterade Customer-objektet
                var customerWithUserDto = _mapper.Map<CustomerWithUserDto>(existingCustomer);

                // Returnera det uppdaterade resultatet
                return OperationResult<CustomerWithUserDto>.Success(customerWithUserDto);
            }
            catch (Exception ex)
            {
                // Hantera eventuella undantag
                return OperationResult<CustomerWithUserDto>.Failure($"An error occurred while updating customer and user: {ex.Message}");
            }
        }
    }
}