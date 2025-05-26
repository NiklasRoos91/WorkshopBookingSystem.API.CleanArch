using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Queries.GetServiceTypeById
{
    public class GetServiceTypeByIdQuery : IRequest<OperationResult<ServiceTypeDto>>
    {
        public int ServiceTypeId { get; set; }

        public GetServiceTypeByIdQuery(int serviceTypeId)
        {
            ServiceTypeId = serviceTypeId;
        }
    }
}
