using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.Queries;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, OperationResult<CustomerWithUserDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper autoMapper)
        {
            _customerRepository = customerRepository;
            _mapper = autoMapper;
        }

        public async Task<OperationResult<CustomerWithUserDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetCustomerWithUserByCustomerIdAsync(request.CustomerId);
                if (existingCustomer == null)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("Customer not found.");
                }

                var user = existingCustomer.User;
                if (user == null)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("User associated with the customer not found.");
                }

                if (!request.IsAdmin && !request.IsEmployee && existingCustomer.UserId != request.UserId)
                {
                    return OperationResult<CustomerWithUserDto>.Failure("You do not have permission to see this customer.");
                }

                var customerWithUserDtos = _mapper.Map<CustomerWithUserDto>(existingCustomer);

                return OperationResult<CustomerWithUserDto>.Success(customerWithUserDtos);

            }
            catch (Exception ex)
            {
                return OperationResult<CustomerWithUserDto>.Failure($"An error occurred while retrieving the customer: {ex.Message}");
            }
        }
    }
}
