using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Queries.GetAllAvailableSlotsQuery
{
    public class GetAllAvailableSlotsQueryHandler : IRequestHandler<GetAllAvailableSlotsQuery, OperationResult<List<AvailableSlotDto>>>
    {
        private readonly IGenericInterface<AvailableSlot> _availableSlotRepository;
        private readonly IMapper _mapper;

        public GetAllAvailableSlotsQueryHandler(IGenericInterface<AvailableSlot> availableSlotRepository, IMapper mapper)
        {
            _availableSlotRepository = availableSlotRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<AvailableSlotDto>>> Handle(GetAllAvailableSlotsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var availableSlots = await _availableSlotRepository.GetAllAsync();
                var availableSlotDtos = _mapper.Map<List<AvailableSlotDto>>(availableSlots);

                return OperationResult<List<AvailableSlotDto>>.Success(availableSlotDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<List<AvailableSlotDto>>.Failure($"An error occurred while retrieving available slots: {ex.Message}");
            }
        }
    }
}
