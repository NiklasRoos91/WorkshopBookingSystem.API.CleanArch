using FluentValidation;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Commands;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Validators
{
    public class CreateServiceTypeCommandValidator : AbstractValidator<CreateServiceTypeCommand>
    {
        public CreateServiceTypeCommandValidator(IValidator<ServiceTypeInputDto> serviceTypeInputValidator)
        {
            // Validera det inbäddade ServiceTypeInputDto i CreateServiceTypeCommand
            RuleFor(x => x.ServiceTypeInputDto)
                .SetValidator(serviceTypeInputValidator);
        }
    }
}
