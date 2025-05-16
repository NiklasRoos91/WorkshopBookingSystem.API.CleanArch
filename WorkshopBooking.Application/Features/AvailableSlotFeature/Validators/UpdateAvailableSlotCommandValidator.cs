using FluentValidation;
using WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.UpdateAvailableSlotCommand;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Validators
{
    public class UpdateAvailableSlotCommandValidator : AbstractValidator<UpdateAvailableSlotCommand>
    {
        public UpdateAvailableSlotCommandValidator(IValidator<AvailableSlotInputDto> availableSlotInputValidator)
        {
            RuleFor(x => x.AvailableSlotInputDto)
                .SetValidator(availableSlotInputValidator);
        }
    }
}
