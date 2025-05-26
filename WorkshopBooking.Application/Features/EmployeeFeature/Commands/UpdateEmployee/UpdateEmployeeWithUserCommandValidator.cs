
using FluentValidation;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands.UpdateEmployee
{
    public class UpdateEmployeeWithUserCommandValidator : AbstractValidator<UpdateEmployeeWithUserCommand>
    {
        public UpdateEmployeeWithUserCommandValidator(IValidator<EmployeeDtoBase> employeeInputValidator)
        {
            RuleFor(x => x.UpdateEmployeeWithUserDto)
                .SetValidator(employeeInputValidator);
        }
    }
}
