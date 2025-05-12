using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Queries
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, OperationResult<List<CustomerWithUserDto>>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(IGenericInterface<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CustomerWithUserDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync(c => c.User);
                var customerWithUserDtos = _mapper.Map<List<CustomerWithUserDto>>(customers);

                return OperationResult<List<CustomerWithUserDto>>.Success(customerWithUserDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<List<CustomerWithUserDto>>.Failure($"An error occurred while retrieving customers: {ex.Message}");
            }
        }
    }
}
