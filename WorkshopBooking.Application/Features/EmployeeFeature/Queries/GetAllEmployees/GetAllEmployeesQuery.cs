
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<OperationResult<List<EmployeeWithUserDto>>>
    {
        public GetAllEmployeesQuery()
        {
        }
    }
}
