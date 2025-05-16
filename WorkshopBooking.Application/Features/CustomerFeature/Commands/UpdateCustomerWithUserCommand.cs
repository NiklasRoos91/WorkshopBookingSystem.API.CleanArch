using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs.WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands
{
    public class UpdateCustomerWithUserCommand : IRequest<OperationResult<CustomerWithUserDto>>
    {
        public int CustomerId { get; }
        public int UserId { get; }
        public bool IsAdmin { get; }
        public UpdateCustomerWithUserDto UpdateCustomerWithUserDto { get; }

        public UpdateCustomerWithUserCommand(int customerId, int userId, bool isAdmin, UpdateCustomerWithUserDto updateCustomerWithUserDto)
        {
            CustomerId = customerId;
            UserId = userId;
            IsAdmin = isAdmin;
            UpdateCustomerWithUserDto = updateCustomerWithUserDto;
        }
    }
}
