using FluentValidation;
using WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingFromSlotCommand;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Validators
{
    public class CreateBookingFromSlotDtoValidator : AbstractValidator<CreateBookingFromSlotCommand>
    {
        public CreateBookingFromSlotDtoValidator(
            IGenericInterface<AvailableSlot> availableSlotRepository)
        {
            RuleFor(x => x.CreateBookingFromSlotDto)
                .SetValidator(new CreateBookingFromSlotCommandValidator(availableSlotRepository));
        }
    }
}
