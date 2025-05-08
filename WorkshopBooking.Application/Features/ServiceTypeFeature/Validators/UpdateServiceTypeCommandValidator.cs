using FluentValidation;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Commands;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Validators
{
    public class UpdateServiceTypeCommandValidator : AbstractValidator<UpdateServiceTypeCommand>
    {
        public UpdateServiceTypeCommandValidator(IValidator<ServiceTypeInputDto> serviceTypeInputValidator)
        {
            // Validera det inbäddade ServiceTypeInputDto i UpdateServiceTypeCommand
            RuleFor(x => x.ServiceTypeInputDto)
                .SetValidator(serviceTypeInputValidator);
        }
    }
}
