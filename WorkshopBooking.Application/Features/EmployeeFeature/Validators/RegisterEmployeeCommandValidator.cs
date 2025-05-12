using FluentValidation;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Validators
{
    public class RegisterEmployeeCommandValidator : AbstractValidator<RegisterEmployeeCommand>
    {
        public RegisterEmployeeCommandValidator( IValidator<EmployeeDtoBase> employeeInputValidator)
        {
            RuleFor(x => x.RegisterEmployeeDto)
                .SetValidator(employeeInputValidator);
        }
    }
}
