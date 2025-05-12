using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands
{
    public class DeleteEmployeeWithUserCommand : IRequest<OperationResult<bool>>
    {
        public int EmployeeId { get; set; }

        public DeleteEmployeeWithUserCommand(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
