using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs
{
    public class AvailableSlotInputDto
    {
        public int EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceTypeId { get; set; }
    }
}
