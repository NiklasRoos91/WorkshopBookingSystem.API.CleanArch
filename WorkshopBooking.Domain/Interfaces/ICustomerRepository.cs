using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerWithUserByIdAsync(int customerId);
    }
}
