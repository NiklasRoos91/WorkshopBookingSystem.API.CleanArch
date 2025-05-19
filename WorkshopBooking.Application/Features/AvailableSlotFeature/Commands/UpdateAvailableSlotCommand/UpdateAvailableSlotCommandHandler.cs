using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.UpdateAvailableSlotCommand
{
    public class UpdateAvailableSlotCommandHandler : IRequestHandler<UpdateAvailableSlotCommand, OperationResult<AvailableSlotDto>>
    {
        private readonly IGenericInterface<AvailableSlot> _availableSlotRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateAvailableSlotCommandHandler(
            IGenericInterface<AvailableSlot> availableSlotRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _availableSlotRepository = availableSlotRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<AvailableSlotDto>> Handle(UpdateAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByUserIdAsync(request.UserId);
                if (employee == null)
                {
                    return OperationResult<AvailableSlotDto>.Failure("User is not connected to Employee.");
                }

                var availableSlot = await _availableSlotRepository.GetByIdAsync(request.AvailableSlotId);
                if (availableSlot == null)
                {
                    return OperationResult<AvailableSlotDto>.Failure("Available slot not found.");
                }

                if (!request.IsAdmin && availableSlot.EmployeeId != employee.EmployeeId)
                {
                    return OperationResult<AvailableSlotDto>.Failure("You don.t have clearenc to update this slot.");
                }

                _mapper.Map(request.AvailableSlotInputDto, availableSlot);
                await _availableSlotRepository.UpdateAsync(availableSlot);

                var slotDto = _mapper.Map<AvailableSlotDto>(availableSlot);

                return OperationResult<AvailableSlotDto>.Success(slotDto);

            }
            catch (Exception ex)
            {
                return OperationResult<AvailableSlotDto>.Failure($"An error occurred while updating available slot: {ex.Message}");
            }
        }
    }
}
