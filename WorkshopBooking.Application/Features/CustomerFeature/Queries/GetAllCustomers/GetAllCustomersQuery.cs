using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<OperationResult<List<CustomerWithUserDto>>>
    {
        public GetAllCustomersQuery()
        {
        }
    }
}
