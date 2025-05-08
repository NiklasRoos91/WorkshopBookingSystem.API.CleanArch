
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Application.Features.CustomerFeature.Validators;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Infrastructure.Presistence;

namespace WorkshopBooking.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly WorkshopBookingDbContext _context;

        public Task<Customer> CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetCustomersById(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetCustomersWithFilterAndSort(string? filter, string sort)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomer(int customerId, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
