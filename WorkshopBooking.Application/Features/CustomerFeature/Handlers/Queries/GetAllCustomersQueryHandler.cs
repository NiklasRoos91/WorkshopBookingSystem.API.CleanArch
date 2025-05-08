using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Queries
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, OperationResult<List<CustomerDto>>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(IGenericInterface<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();
                var customerDtos = _mapper.Map<List<CustomerDto>>(customers);

                return OperationResult<List<CustomerDto>>.Success(customerDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<List<CustomerDto>>.Failure($"An error occurred while retrieving customers: {ex.Message}");
            }
        }
    }

}
