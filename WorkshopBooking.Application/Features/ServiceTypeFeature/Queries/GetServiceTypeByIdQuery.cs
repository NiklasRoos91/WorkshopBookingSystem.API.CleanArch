using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Queries
{
    public class GetServiceTypeByIdQuery : IRequest<OperationResult<ServiceType>>
    {
        public int ServiceTypeId { get; set; }

        public GetServiceTypeByIdQuery(int serviceTypeId)
        {
            ServiceTypeId = serviceTypeId;
        }
    }
}
