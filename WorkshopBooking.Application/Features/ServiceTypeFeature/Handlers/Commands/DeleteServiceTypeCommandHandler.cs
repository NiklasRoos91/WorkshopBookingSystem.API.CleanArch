using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Commands;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Handlers.Commands
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
                var exists = await _serviceTypeRepository.ExistsAsync(request.ServiceTypeId);

                if (!exists)
                {
                    return OperationResult<bool>.Failure($"ServiceType with ID {request.ServiceTypeId} not found.");
                }

                var result = await _serviceTypeRepository.DeleteAsync(request.ServiceTypeId);

                return OperationResult<bool>.Success(result);

            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the ServiceType: {ex.Message}");
            }
        }
    }
}
