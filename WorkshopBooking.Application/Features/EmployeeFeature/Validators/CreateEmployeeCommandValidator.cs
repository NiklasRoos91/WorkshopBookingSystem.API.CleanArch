using FluentValidation;
using WorkshopBooking.Application.Features.EmployeeFeature.Commands;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator( IValidator<EmployeeInputDto> employeeInputValidator)
        {
            RuleFor(x => x.EmployeeInputDto)
                .SetValidator(employeeInputValidator);
        }
    }
}
