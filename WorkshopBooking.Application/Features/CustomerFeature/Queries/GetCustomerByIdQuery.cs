using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Queries
{
    public class GetCustomerByIdQuery : IRequest<OperationResult<CustomerWithUserDto>>
    {
        public int CustomerId { get; set; }

        public GetCustomerByIdQuery(int customeIid)
        {
            CustomerId = customeIid;
        }
    }
}
