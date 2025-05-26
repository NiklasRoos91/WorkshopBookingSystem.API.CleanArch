using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Queries.GetCustomersWithFilterAndSort
{
    public class GetCustomersWithFilterAndSortQuery : IRequest<OperationResult<IEnumerable<CustomerWithUserDto>>>
    {
        public string? Filter { get; set; }
        public string? Sort { get; set; }

        public GetCustomersWithFilterAndSortQuery(string? filter, string? sort)
        {
            Filter = filter;
            Sort = sort;
        }
    }
}
