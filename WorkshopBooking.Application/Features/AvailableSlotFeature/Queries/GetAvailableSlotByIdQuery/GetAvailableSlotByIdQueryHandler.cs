using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Queries.GetAvailableSlotByIdQuery
{
    public class GetAvailableSlotByIdQueryHandler : IRequestHandler<GetAvailableSlotByIdQuery, OperationResult<AvailableSlotDto>>
    {
        private readonly IGenericInterface<AvailableSlot> _availableSlotRepository;
        private readonly IMapper _mapper;

        public GetAvailableSlotByIdQueryHandler(IGenericInterface<AvailableSlot> availableSlotRepository, IMapper mapper)
        {
            _availableSlotRepository = availableSlotRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<AvailableSlotDto>> Handle(GetAvailableSlotByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var availableSlot = await _availableSlotRepository.GetByIdAsync(request.AvailableSlotId);
                if (availableSlot == null)
                {
                    return OperationResult<AvailableSlotDto>.Failure("Available slot not found.");
                }

                var availableSlotDto = _mapper.Map<AvailableSlotDto>(availableSlot);

                return OperationResult<AvailableSlotDto>.Success(availableSlotDto);
            }
            catch (Exception ex)
            {
                return OperationResult<AvailableSlotDto>.Failure($"An error occurred while retrieving the available slot: {ex.Message}");
            }
        }
    }
}
