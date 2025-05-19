using FluentValidation;
using WorkshopBooking.Application.Features.BookingFeature.Commands.UpdateBookingCommand;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Validators
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator(
            IGenericInterface<Booking> bookingRepository,
            IGenericInterface<ServiceType> serviceTypeRepository)
        {
            RuleFor(x => x.BookingId)
                .MustAsync(async (id, ct) => await bookingRepository.ExistsAsync(id))
                .WithMessage("Booking does not exist.");

            RuleFor(x => x.UpdateBookingDto)
                .SetValidator(new UpdateBookingDtoValidator(serviceTypeRepository));
        }
    }
}
