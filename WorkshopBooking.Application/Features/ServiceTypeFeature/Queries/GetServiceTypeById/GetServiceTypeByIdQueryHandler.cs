using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Queries.GetServiceTypeById
{
    public class GetServiceTypeByIdQueryHandler : IRequestHandler<GetServiceTypeByIdQuery, OperationResult<ServiceTypeDto>>
    {
        private readonly IGenericInterface<ServiceType> _serviceTypeRepository;
        private readonly IMapper _mapper;

        public GetServiceTypeByIdQueryHandler(
            IGenericInterface<ServiceType> serviceTypeRepository,
            IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ServiceTypeDto>> Handle(GetServiceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceType = await _serviceTypeRepository.GetByIdAsync(request.ServiceTypeId);
                if (serviceType == null)
                {
                    return OperationResult<ServiceTypeDto>.Failure("Service type not found.");
                }

                var serviceTypeDto = _mapper.Map<ServiceTypeDto>(serviceType);

                return OperationResult<ServiceTypeDto>.Success(serviceTypeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<ServiceTypeDto>.Failure($"An error occurred while retrieving the service type: {ex.Message}");
            }
        }
    }
}
