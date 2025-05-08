using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Commands;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Handlers.Commands
{
    public class CreateServiceTypeCommandHandler : IRequestHandler<CreateServiceTypeCommand, OperationResult<ServiceTypeDto>>
    {
        private readonly IGenericInterface<ServiceType> _serviceTypeRepository;
        private readonly IMapper _mapper;

        public CreateServiceTypeCommandHandler(IGenericInterface<ServiceType> serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ServiceTypeDto>> Handle(CreateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the input DTO to the entity
                var serviceType = _mapper.Map<ServiceType>(request.ServiceTypeInputDto);

                // Add the entity to the repository
                await _serviceTypeRepository.AddAsync(serviceType);

                // Map the entity back to the DTO for the response
                var serviceTypeDto = _mapper.Map<ServiceTypeDto>(serviceType);

                return OperationResult<ServiceTypeDto>.Success(serviceTypeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<ServiceTypeDto>.Failure($"An error occurred while creating service type: {ex.Message}");
            }
        }
    }
}
