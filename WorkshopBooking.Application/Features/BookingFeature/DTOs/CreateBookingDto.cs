﻿namespace WorkshopBooking.Application.Features.BookingFeature.DTOs
{
    public class CreateBookingDto
    {
        public int CustomerId { get; set; }
        public int ServiceTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
