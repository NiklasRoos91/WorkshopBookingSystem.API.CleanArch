using FluentValidation;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator(IValidator<CustomerInputDto> customerInputValidator)
        {
            // Validera det inbäddade CustomerInputDto i UpdateCustomerCommand
            RuleFor(x => x.CustomerInputDto).SetValidator(customerInputValidator);
        }
    }
}
