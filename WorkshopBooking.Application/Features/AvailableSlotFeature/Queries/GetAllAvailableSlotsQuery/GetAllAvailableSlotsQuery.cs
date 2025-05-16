using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Queries.GetAllAvailableSlotsQuery
{
    public class GetAllAvailableSlotsQuery : IRequest<OperationResult<List<AvailableSlotDto>>>
    {
        public GetAllAvailableSlotsQuery()
        {
        }
    }
}
