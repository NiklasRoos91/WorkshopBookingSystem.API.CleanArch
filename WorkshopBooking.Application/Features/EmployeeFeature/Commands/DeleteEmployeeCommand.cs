using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands
{
    public class DeleteEmployeeCommand : IRequest<OperationResult<bool>>
    {
        public int EmployeeId { get; set; }

        public DeleteEmployeeCommand(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
