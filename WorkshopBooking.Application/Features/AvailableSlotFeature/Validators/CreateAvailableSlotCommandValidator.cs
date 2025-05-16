using FluentValidation;
using WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.CreateAvailableSlotCommand;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Validators
{
    public class CreateAvailableSlotCommandValidator : AbstractValidator<CreateAvailableSlotCommand>
    {
        public CreateAvailableSlotCommandValidator(IValidator<AvailableSlotInputDto> availableSlotinputDtoValidator)
        {
            RuleFor(x => x.AvailableSlotInputDto)
                .SetValidator(availableSlotinputDtoValidator);
        } 
    }
}
