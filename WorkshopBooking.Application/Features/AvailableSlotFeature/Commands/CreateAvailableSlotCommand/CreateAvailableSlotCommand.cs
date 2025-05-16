using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.CreateAvailableSlotCommand
{
    public class CreateAvailableSlotCommand : IRequest<OperationResult<AvailableSlotDto>>
    {
        public int UserId { get; }
        public AvailableSlotInputDto AvailableSlotInputDto { get; }

        public CreateAvailableSlotCommand(int userId, AvailableSlotInputDto AvailableSlotinputDto)
        {
            UserId = userId;
            AvailableSlotInputDto = AvailableSlotinputDto;
        }
    }
}
