using FluentValidation;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator(IValidator<CustomerInputDto> customerInputValidator)
        {
            // Validera det inbäddade CustomerInputDto i CreateCustomerCommand
            RuleFor(x => x.CustomerInputDto)
                .SetValidator(customerInputValidator);
        }
    }
}
