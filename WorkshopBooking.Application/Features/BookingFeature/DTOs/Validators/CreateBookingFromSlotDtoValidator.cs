using FluentValidation;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.DTOs.Validators
{
    public class CreateBookingFromSlotDtoValidator : AbstractValidator<CreateBookingFromSlotDto>
    {
        public CreateBookingFromSlotDtoValidator(
            IGenericInterface<AvailableSlot> availableSlotRepository)
        {
            RuleFor(x => x.AvailableSlotId)
                .MustAsync(async (id, ct) => await availableSlotRepository.ExistsAsync(id))
                .WithMessage("Available slot does not exist.");
        }
    }
}