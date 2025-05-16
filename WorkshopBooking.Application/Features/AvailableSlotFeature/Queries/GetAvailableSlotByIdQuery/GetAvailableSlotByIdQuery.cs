using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Queries.GetAvailableSlotByIdQuery
{
    public class GetAvailableSlotByIdQuery : IRequest<OperationResult<AvailableSlotDto>>
    {
        public int AvailableSlotId { get; }

        public GetAvailableSlotByIdQuery(int availableSlotId)
        {
            AvailableSlotId = availableSlotId;
        }
    }
}
