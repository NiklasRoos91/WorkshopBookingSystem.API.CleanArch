using FluentValidation;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;

namespace WorkshopBooking.Application.Features.CustomerFeature.Commands.UpdateCustomer
{
    public class UpdateCustomerWithUserCommandValidator : AbstractValidator<UpdateCustomerWithUserCommand>
    {
        public UpdateCustomerWithUserCommandValidator(IValidator<CustomerDtoBase> customerDtoValidator)
        {
            RuleFor(x => x.UpdateCustomerWithUserDto)
                .SetValidator(customerDtoValidator);
        }
    }
}
