
using FluentValidation;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator(IValidator<EmployeeInputDto> employeeInputValidator)
        {
            // Validera det inbäddade EmployeeInputDto i UpdateEmployeeCommand
            RuleFor(x => x.EmployeeInputDto).SetValidator(employeeInputValidator);
        }
    }
}
