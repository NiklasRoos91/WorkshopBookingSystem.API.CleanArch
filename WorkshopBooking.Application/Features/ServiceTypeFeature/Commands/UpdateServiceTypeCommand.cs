using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Commands
{
    public class UpdateServiceTypeCommand : IRequest<OperationResult<ServiceTypeDto>>
    {
        public int ServiceTypeId { get; set; }
        public ServiceTypeInputDto ServiceTypeInputDto { get; set; }

        public UpdateServiceTypeCommand(int serviceTypeId, ServiceTypeInputDto serviceTypeInputDto)
        {
            ServiceTypeId = serviceTypeId;
            ServiceTypeInputDto = serviceTypeInputDto;
        }
    }
}
