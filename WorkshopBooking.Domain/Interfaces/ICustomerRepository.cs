using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerWithUserByCustomerIdAsync(int customerId);
        Task<Customer?> GetCustomerWithUserByUserIdAsync(int userId);
    }
}
