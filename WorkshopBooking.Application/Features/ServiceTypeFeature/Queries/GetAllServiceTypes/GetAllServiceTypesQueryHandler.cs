using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Queries.GetAllServiceTypes
{
    public class GetAllServiceTypesQueryHandler : IRequestHandler<GetAllServiceTypesQuery, OperationResult<List<ServiceTypeDto>>>
    {
        private readonly IGenericInterface<ServiceType> _serviceTypeRepository;
        private readonly IMapper _mapper;

        public GetAllServiceTypesQueryHandler(IGenericInterface<ServiceType> serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<ServiceTypeDto>>> Handle(GetAllServiceTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceTypes = await _serviceTypeRepository.GetAllAsync();
                var serviceTypeDtos = _mapper.Map<List<ServiceTypeDto>>(serviceTypes);

                return OperationResult<List<ServiceTypeDto>>.Success(serviceTypeDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<List<ServiceTypeDto>>.Failure($"An error occurred while retrieving service types: {ex.Message}");
            }
        }
    }
}
