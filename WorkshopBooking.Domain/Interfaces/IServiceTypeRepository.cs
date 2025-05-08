using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface IServiceTypeRepository
    {
        public Task<List<ServiceType>> GetAllServiceTypes();
        public Task<ServiceType?> GetServiceTypeById(int id);
        public Task<ServiceType> CreateServiceType(ServiceType serviceType);
        public Task<ServiceType> UpdateServiceType(int serviceTypeId, ServiceType serviceType);
        public Task<bool> DeleteServiceType(int serviceTypeId);

    }
}
