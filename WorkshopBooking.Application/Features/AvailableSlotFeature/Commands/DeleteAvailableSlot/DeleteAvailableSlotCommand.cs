using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.DeleteAvailableSlot
{
    public class DeleteAvailableSlotCommand : IRequest<OperationResult<bool>>
    {
        public int AvailableSlotId { get; }
        public int UserId { get; }
        public bool IsAdmin { get; }

        public DeleteAvailableSlotCommand(int availableSlotId, int userId, bool isAdmin)
        {
            AvailableSlotId = availableSlotId;
            UserId = userId;
            IsAdmin = isAdmin;
        }
    }
}
