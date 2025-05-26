using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Queries.GetCustomersWithFilterAndSort
{
    public class GetCustomersWithFilterAndSortQueryHandler : IRequestHandler<GetCustomersWithFilterAndSortQuery, OperationResult<IEnumerable<CustomerWithUserDto>>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersWithFilterAndSortQueryHandler(IGenericInterface<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<CustomerWithUserDto>>> Handle(GetCustomersWithFilterAndSortQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync(c => c.User);


                var filteredCustomers = customers
                    // Filter customers based on the provided filter criteria
                    .Where(c =>
                    string.IsNullOrEmpty(request.Filter) ||
                    c.User.FirstName.Contains(request.Filter, StringComparison.OrdinalIgnoreCase) ||
                    c.User.LastName.Contains(request.Filter, StringComparison.OrdinalIgnoreCase) ||
                    c.VehicleMake.Contains(request.Filter, StringComparison.OrdinalIgnoreCase)
                    );

                var sortedCustomers = request.Sort?.ToLower() == "desc"
                    ? filteredCustomers.OrderByDescending(c => c.User.LastName) // Sort in descending order
                    : filteredCustomers.OrderBy(c => c.User.LastName); // Sort in ascending order

                var customerDtos = _mapper.Map<List<CustomerWithUserDto>>(sortedCustomers);

                return OperationResult<IEnumerable<CustomerWithUserDto>>.Success(customerDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<CustomerWithUserDto>>.Failure($"An error occurred while retrieving customers: {ex.Message}");
            }
        }
    }
}
