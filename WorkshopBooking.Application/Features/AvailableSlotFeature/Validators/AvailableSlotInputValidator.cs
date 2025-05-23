﻿using FluentValidation;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Validators
{
    public class AvailableSlotInputValidator : AbstractValidator<AvailableSlotInputDto>
    {
        public AvailableSlotInputValidator(
            IGenericInterface<ServiceType> serviceTypeRepo)
        {
            RuleFor(x => x.ServiceTypeId)
                .MustAsync(async (id, ct) => await serviceTypeRepo.ExistsAsync(id))
                .WithMessage("Service type does not exist.");

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
