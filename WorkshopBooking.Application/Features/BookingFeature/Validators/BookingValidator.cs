using FluentValidation;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;


namespace WorkshopBooking.Application.Features.BookingFeature.Validators
{
    public class BookingValidator : AbstractValidator<BookingInputDto>
    {
        public BookingValidator(IGenericInterface<Customer> customerRepo,
            IGenericInterface<Employee> employeeRepo,
            IGenericInterface<ServiceType> serviceTypeRepo)
        {        

            RuleFor(x => x.ServiceTypeId)
                .MustAsync(async (id, ct) => await serviceTypeRepo.ExistsAsync(id))
                .WithMessage("Service type does not exist.");

            RuleFor(x => x.EmployeeId)
                .MustAsync(async (id, ct) => await employeeRepo.ExistsAsync(id))
                .WithMessage("Employee does not exist.");

            RuleFor(c => c.CustomerId)
                .MustAsync(async (id, ct) => await customerRepo.ExistsAsync(id))
                .WithMessage("Customer does not exist.");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required.")
                .GreaterThan(DateTime.Now)
                .WithMessage("Start time must be in the future.");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("End time is required.")
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be later than start time.");
        }
    }
}
