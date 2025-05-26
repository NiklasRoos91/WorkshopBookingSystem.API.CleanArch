using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Commands.CreateServiceType
{
    public class CreateServiceTypeCommand : IRequest<OperationResult<ServiceTypeDto>>
    {
        public ServiceTypeInputDto ServiceTypeInputDto { get; set; }

        public CreateServiceTypeCommand(ServiceTypeInputDto serviceTypeInputDto)
        {
            ServiceTypeInputDto = serviceTypeInputDto;
        }
    }
}
