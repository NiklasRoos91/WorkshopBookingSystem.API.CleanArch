using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.UpdateAvailableSlotCommand
{
    public class UpdateAvailableSlotCommand : IRequest<OperationResult<AvailableSlotDto>>
    {
        public int AvailableSlotId { get; }
        public int UserId { get; }
        public bool IsAdmin { get; }
        public AvailableSlotInputDto AvailableSlotInputDto { get; }

        public UpdateAvailableSlotCommand(int availableSlotId, int userId, bool isAdmin, AvailableSlotInputDto availableSlotInputDto)
        {
            AvailableSlotId = availableSlotId;
            UserId = userId;
            IsAdmin = isAdmin;
            AvailableSlotInputDto = availableSlotInputDto;
        }
    }
}
