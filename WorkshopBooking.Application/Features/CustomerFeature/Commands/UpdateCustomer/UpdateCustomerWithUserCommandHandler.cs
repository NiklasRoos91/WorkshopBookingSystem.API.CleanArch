using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands.UpdateCustomer
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
                var existingCustomer = await _customerRepositoryWithUser.GetCustomerWithUserByCustomerIdAsync(request.CustomerId);
                if (existingCustomer == null)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("Customer not found.");
                }

                var user = existingCustomer.User;
                if (user == null)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("User associated with the customer not found.");
                }

                if (!request.IsAdmin && existingCustomer.UserId != request.UserId)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("You do not have permission to update this customer.");
                }

                _mapper.Map(request.UpdateCustomerWithUserDto, existingCustomer);
                _mapper.Map(request.UpdateCustomerWithUserDto, user);

                await _customerRepository.UpdateAsync(existingCustomer);
                await _userRepository.UpdateAsync(user);

                var customerWithUserDto = _mapper.Map<CustomerWithUserDto>(existingCustomer);

                return OperationResult<CustomerWithUserDto>.Success(customerWithUserDto);
            }
            catch (Exception ex)
            {
                return OperationResult<CustomerWithUserDto>.Failure($"An error occurred while updating customer and user: {ex.Message}");
            }
        }
    }
}