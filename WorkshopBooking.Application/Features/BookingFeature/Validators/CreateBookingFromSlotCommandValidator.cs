using FluentValidation;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingFromSlotCommand
{
    public class CreateBookingFromSlotCommandValidator : AbstractValidator<CreateBookingFromSlotDto>
    {
        public CreateBookingFromSlotCommandValidator(
            IGenericInterface<AvailableSlot> availableSlotRepository)
        {
            RuleFor(x => x.AvailableSlotId)
                .MustAsync(async (id, ct) => await availableSlotRepository.ExistsAsync(id))
                .WithMessage("Available slot does not exist.");
        }
    }
}