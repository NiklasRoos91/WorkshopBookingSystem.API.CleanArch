using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<OperationResult<CustomerWithUserDto>>
    {
        public int CustomerId { get; }
        public int UserId { get; }
        public bool IsAdmin { get; }
        public bool IsEmployee { get; }

        public GetCustomerByIdQuery(int customerId, int userId, bool isAdmin, bool isEmployee)
        {
            CustomerId = customerId;
            UserId = userId;
            IsAdmin = isAdmin;
            IsEmployee = isEmployee;
        }
    }
}
