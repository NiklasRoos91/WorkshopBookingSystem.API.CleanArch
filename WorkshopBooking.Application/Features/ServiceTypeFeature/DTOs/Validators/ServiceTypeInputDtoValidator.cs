using FluentValidation;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs.Validators
{

    public class ServiceTypeInputDtoValidator : AbstractValidator<ServiceTypeInputDto>
    {
        public ServiceTypeInputDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("Service type name is required.")
                .Length(2, 50)
                .WithMessage("Service type name must be between 2 and 50 characters long.");

            RuleFor(s => s.Price)
                .NotEmpty()
                .WithMessage("Service type price is required.")
                 .GreaterThanOrEqualTo(0)
                .WithMessage("The price must be a positive number. Please check the value.");

            RuleFor(s => s.Duration)
                .NotEmpty()
                .WithMessage("Service type duration is required.");
        }
    }
}
