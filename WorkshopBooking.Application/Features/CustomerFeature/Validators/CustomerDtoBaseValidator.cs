using FluentValidation;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Validators
{
    public class CustomerDtoBaseValidator : AbstractValidator<CustomerDtoBase>
    {
        private readonly IVehicleMakeApiService _vehicleMakeApiService;

        public CustomerDtoBaseValidator(IVehicleMakeApiService vehicleMakeApiService)
        {
            _vehicleMakeApiService = vehicleMakeApiService;

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .Length(2, 50)
                .WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .Length(2, 50)
                .WithMessage("Last name must be between 2 and 50 characters.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .Length(5, 100)
                .WithMessage("Address must be between 5 and 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Phone number must only contain digits and may start with '+'. No spaces or special characters are allowed. Example: +46701234567");

            RuleFor(x => x.VehicleMake)
                .NotEmpty()
                .WithMessage("Vehicle make is required.")
                .MustAsync(async (vehicleMake, cancellationToken) =>
                    await _vehicleMakeApiService.DoesVehicleMakeExistAsync(vehicleMake, cancellationToken))
                .WithMessage("The vehicle make is not valid.");

            // Validering för lösenord som endast ska användas vid registrering
            When(x => x is RegisterCustomerDto, () =>
            {
                RuleFor(x => ((RegisterCustomerDto)x).Password)
                    .NotEmpty()
                    .WithMessage("Password is required.")
                    .MinimumLength(8)
                    .WithMessage("Password must be at least 8 characters long.");

                RuleFor(x => ((RegisterCustomerDto)x).Email)
                    .NotEmpty()
                    .WithMessage("Email is required.")
                    .EmailAddress()
                    .WithMessage("Email must be a valid email address.");
            });
        }
    }
}
