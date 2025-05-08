using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopBooking.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
