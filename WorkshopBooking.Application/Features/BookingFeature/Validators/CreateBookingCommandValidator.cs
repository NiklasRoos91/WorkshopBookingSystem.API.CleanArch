using FluentValidation;
using WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingCommand;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator(
            IGenericInterface<ServiceType> serviceTypeRepo,
            IGenericInterface<Customer> customerRepo)
        {
            RuleFor(x => x.CreateBookingDto)
                .SetValidator(new CreateBookingDtoValidator(serviceTypeRepo, customerRepo));
        }
    }
}
