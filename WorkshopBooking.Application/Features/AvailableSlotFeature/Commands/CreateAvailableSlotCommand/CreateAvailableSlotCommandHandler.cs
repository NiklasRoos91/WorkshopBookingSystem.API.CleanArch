using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Commands.CreateAvailableSlotCommand
{
    public class CreateAvailableSlotCommandHandler : IRequestHandler<CreateAvailableSlotCommand, OperationResult<AvailableSlotDto>>
    {
        private readonly IGenericInterface<AvailableSlot> _availableSlotRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateAvailableSlotCommandHandler(
            IGenericInterface<AvailableSlot> availableSlotRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _availableSlotRepository = availableSlotRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<AvailableSlotDto>> Handle(CreateAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByUserIdAsync(request.UserId);
                if (employee == null)
                {
                    return OperationResult<AvailableSlotDto>.Failure("The user does not have a linked Employee.");
                }

                var slot = _mapper.Map<AvailableSlot>(request.AvailableSlotInputDto);
                slot.EmployeeId = employee.EmployeeId;
                slot.IsAvailable = true;

                await _availableSlotRepository.AddAsync(slot);

                var dto = _mapper.Map<AvailableSlotDto>(slot);
                return OperationResult<AvailableSlotDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<AvailableSlotDto>.Failure($"An error occurred while creating available slot: {ex.Message}");

            }
        }
    }
}
