using FluentValidation;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands.RegisterCustomer
{
    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator(IValidator<CustomerDtoBase> customerDtoValidator)
        {
            RuleFor(x => x.RegisterCustomerDto)
                .SetValidator(customerDtoValidator);
        }
    }
}
