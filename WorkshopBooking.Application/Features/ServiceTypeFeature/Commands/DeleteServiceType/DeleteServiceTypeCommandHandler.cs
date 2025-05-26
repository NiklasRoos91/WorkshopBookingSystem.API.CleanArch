using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Commands.DeleteServiceType
{
    public class DeleteServiceTypeCommandHandler : IRequestHandler<DeleteServiceTypeCommand, OperationResult<bool>>
    {
        private readonly IGenericInterface<ServiceType> _serviceTypeRepository;

        public DeleteServiceTypeCommandHandler(IGenericInterface<ServiceType> serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteServiceTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceType = await _serviceTypeRepository.GetByIdAsync(request.ServiceTypeId);
                if (serviceType == null)
                {
                    return OperationResult<bool>.Failure($"ServiceType with ID {request.ServiceTypeId} not found.");
                }

                var serviceTypeDeleted = await _serviceTypeRepository.DeleteAsync(request.ServiceTypeId);

                return OperationResult<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the ServiceType: {ex.Message}");
            }
        }
    }
}
