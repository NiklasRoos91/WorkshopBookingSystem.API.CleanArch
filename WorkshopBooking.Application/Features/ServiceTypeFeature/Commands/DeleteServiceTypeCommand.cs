using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Commands
{
    public class DeleteServiceTypeCommand : IRequest<OperationResult<bool>>
    {
        public int ServiceTypeId { get; set; }

        public DeleteServiceTypeCommand(int serviceTypeId)
        {
            ServiceTypeId = serviceTypeId;
        }
    }
}
