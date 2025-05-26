using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Commands.UpdateServiceType
{
    public class UpdateServiceTypeCommandHandler : IRequestHandler<UpdateServiceTypeCommand, OperationResult<ServiceTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericInterface<ServiceType> _serviceTypeRepository;

        public UpdateServiceTypeCommandHandler(IGenericInterface<ServiceType> serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ServiceTypeDto>> Handle(UpdateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceType = await _serviceTypeRepository.GetByIdAsync(request.ServiceTypeId);

                if (serviceType == null)
                {
                    return OperationResult<ServiceTypeDto>.Failure("Service type not found.");
                }

                // Map the updated properties from the request to the existing service type
                _mapper.Map(request.ServiceTypeInputDto, serviceType);

                // Update the service type in the repository
                await _serviceTypeRepository.UpdateAsync(serviceType);

                // Map the updated service type to a DTO
                var serviceTypeDto = _mapper.Map<ServiceTypeDto>(serviceType);

                return OperationResult<ServiceTypeDto>.Success(serviceTypeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<ServiceTypeDto>.Failure($"An error occurred while updating the service type: {ex.Message}");
            }
        }
    }
}
