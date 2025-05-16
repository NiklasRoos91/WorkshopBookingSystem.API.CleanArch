using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Handlers.Queries
{
    public class GetServiceTypeByIdQueryHandler : IRequestHandler<GetServiceTypeByIdQuery, OperationResult<ServiceType>>
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

        public async Task<OperationResult<ServiceType>> Handle(GetServiceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceType = await _serviceTypeRepository.GetByIdAsync(request.ServiceTypeId);
                if (serviceType == null)
                {
                    return OperationResult<ServiceType>.Failure("Service type not found.");
                }

                var serviceTypeDto = _mapper.Map<ServiceType>(serviceType);

                return OperationResult<ServiceType>.Success(serviceTypeDto);
            }
            catch (Exception ex)
            {
                return OperationResult<ServiceType>.Failure($"An error occurred while retrieving the service type: {ex.Message}");
            }
        }
    }
}
