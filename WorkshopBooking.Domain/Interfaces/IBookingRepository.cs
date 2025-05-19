using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetBookingsByEmployeeIdAsync(int employeeId);
        Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId);
        Task<List<Booking>> GetBookingsByServiceTypeIdAsync(int serviceTypeId);
        Task<List<Booking>> GetBookingsByDateAsync(DateTime date);
        Task<List<Booking>> GetBookingsByDateRangeAsync(DateTime from, DateTime to);
    }
}
