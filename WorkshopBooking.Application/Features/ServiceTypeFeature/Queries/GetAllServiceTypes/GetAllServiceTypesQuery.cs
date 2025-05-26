using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Queries.GetAllServiceTypes
{
    public class GetAllServiceTypesQuery : IRequest<OperationResult<List<ServiceTypeDto>>>
    {
        public GetAllServiceTypesQuery()
        {
        }
    }
}
