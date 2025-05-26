using FluentValidation;
using WorkshopBooking.Application.Features.BookingFeature.DTOs.Validators;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingFromSlotCommand
{
    public class CreateBookingFromSlotCommandValidator : AbstractValidator<CreateBookingFromSlotCommand>
    {
        public CreateBookingFromSlotCommandValidator(
            IGenericInterface<AvailableSlot> availableSlotRepository)
        {
            RuleFor(x => x.CreateBookingFromSlotDto)
                .SetValidator(new CreateBookingFromSlotDtoValidator(availableSlotRepository));
        }
    }
}