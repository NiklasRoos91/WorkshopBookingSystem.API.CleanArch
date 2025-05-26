using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, OperationResult<CustomerWithUserDto>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IGenericInterface<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterCustomerCommandHandler(
            IGenericInterface<Customer> customerRepository,
            IGenericInterface<User> userRepository,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<OperationResult<CustomerWithUserDto>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request.RegisterCustomerDto);

                customer.User.PasswordHash = _passwordHasher.HashPassword(customer.User, request.RegisterCustomerDto.Password);

                await _userRepository.AddAsync(customer.User);
                await _customerRepository.AddAsync(customer);

                var dto = _mapper.Map<CustomerWithUserDto>(customer);

                return OperationResult<CustomerWithUserDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<CustomerWithUserDto>.Failure($"An error occurred while registering customer: {ex.Message}");
            }
        }
    }
}
