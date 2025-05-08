using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, OperationResult<CustomerDto>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IGenericInterface<Customer> customerRepository, IMapper autoMapper)
        {
            _customerRepository = customerRepository;
            _mapper = autoMapper;
        }

        public async Task<OperationResult<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(request.Id);
                if (customer == null)
                {
                    return OperationResult<CustomerDto>.Failure("Customer not found.");
                }

                var customerDto = _mapper.Map<CustomerDto>(customer);

                return OperationResult<CustomerDto>.Success(customerDto);

            }
            catch (Exception ex)
            {
                return OperationResult<CustomerDto>.Failure($"An error occurred while retrieving the customer: {ex.Message}");
            }
        }
    }
}
