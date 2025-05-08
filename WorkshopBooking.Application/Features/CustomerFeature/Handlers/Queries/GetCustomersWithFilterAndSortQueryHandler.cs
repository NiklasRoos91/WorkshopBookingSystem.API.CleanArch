using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Queries
{
    public class GetCustomersWithFilterAndSortQueryHandler : IRequestHandler<GetCustomersWithFilterAndSortQuery, OperationResult<IEnumerable<CustomerDto>>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersWithFilterAndSortQueryHandler(IGenericInterface<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<CustomerDto>>> Handle(GetCustomersWithFilterAndSortQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();

                
                var filteredCustomers = customers
                    // Filter customers based on the provided filter criteria
                    .Where(c =>
                    string.IsNullOrEmpty(request.Filter) ||
                    c.FirstName.Contains(request.Filter, StringComparison.OrdinalIgnoreCase) ||
                    c.LastName.Contains(request.Filter, StringComparison.OrdinalIgnoreCase) ||
                    c.VehicleMake.Contains(request.Filter, StringComparison.OrdinalIgnoreCase)
                    );

                var sortedCustomers = request.Sort?.ToLower() == "desc"
                    ? filteredCustomers.OrderByDescending(c => c.LastName) // Sort in descending order
                    : filteredCustomers.OrderBy(c => c.LastName); // Sort in ascending order

                var customerDtos = _mapper.Map<List<CustomerDto>>(sortedCustomers);

                return OperationResult<IEnumerable<CustomerDto>>.Success(customerDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<CustomerDto>>.Failure($"An error occurred while retrieving customers: {ex.Message}");
            }
        }
    }
}
