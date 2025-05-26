using FluentValidation;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs.Validators
{
    public class AvailableSlotInputDtoValidator : AbstractValidator<AvailableSlotInputDto>
    {
        public AvailableSlotInputDtoValidator(
            IGenericInterface<ServiceType> serviceTypeRepo)
        {
            RuleFor(x => x.ServiceTypeId)
                .MustAsync(async (id, ct) => await serviceTypeRepo.ExistsAsync(id))
                .WithMessage("Service type does not exist.");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required.")
                .GreaterThan(DateTime.Now)
                .WithMessage("Start time must be in the future.");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("End time is required.")
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be later than start time.");
        }
    }
}
