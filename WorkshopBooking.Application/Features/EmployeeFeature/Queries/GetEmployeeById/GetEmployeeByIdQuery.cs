using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<OperationResult<EmployeeWithUserDto>>
    {
        public int EmployeeId { get; }
        public int UserId { get; }
        public bool IsAdmin { get; }

        public GetEmployeeByIdQuery(int employeeId, int userId, bool isAdmin)
        {
            EmployeeId = employeeId;
            UserId = userId;
            IsAdmin = isAdmin;
        }
    }
}
