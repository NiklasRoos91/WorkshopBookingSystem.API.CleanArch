using FluentValidation;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Enums;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Validators
{
    public class EmployeeDtoBaseValidator : AbstractValidator<EmployeeDtoBase>
    {
        public EmployeeDtoBaseValidator()
        {
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

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .WithMessage("Job title is required.")
                .Length(2, 50)
                .WithMessage("Job title must be between 2 and 50 characters.");

            // Validering för lösenord som endast ska användas vid registrering
            When(x => x is RegisterEmployeeDto, () =>
            {
                RuleFor(x => ((RegisterEmployeeDto)x).Password)
                    .NotEmpty()
                    .WithMessage("Password is required.")
                    .MinimumLength(8)
                    .WithMessage("Password must be at least 8 characters long.");

                RuleFor(x => ((RegisterEmployeeDto)x).Email)
                    .NotEmpty()
                    .WithMessage("Email is required.")
                    .EmailAddress()
                    .WithMessage("Email must be a valid email address.");

                RuleFor(x => ((RegisterEmployeeDto)x).Role)
                    .Must(role => role == UserRole.Employee || role == UserRole.Admin)
                    .WithMessage("Role must be either Employee or Admin.");
            });
        }
    }
}
