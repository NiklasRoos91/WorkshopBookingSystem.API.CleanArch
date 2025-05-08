using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer?> GetCustomersById(int customerId);
        public Task<Customer> CreateCustomer(Customer customer);
        public Task<Customer> UpdateCustomer(int customerId, Customer customer);
        public Task<bool> DeleteCustomer(int customerId);
        public Task<List<Customer>> GetCustomersWithFilterAndSort(string? filter, string sort);

    }
}
