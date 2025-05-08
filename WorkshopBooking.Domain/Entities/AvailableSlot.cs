using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopBooking.Domain.Entities
{
    public class AvailableSlot
    {
        public int AvailableSlotId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
