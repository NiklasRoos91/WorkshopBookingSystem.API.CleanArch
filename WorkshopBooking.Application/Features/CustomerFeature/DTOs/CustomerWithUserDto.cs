﻿namespace WorkshopBooking.Application.Features.CustomerFeature.DTOs
{
    public class CustomerWithUserDto
    {
        public int CustomerId { get; set; }
        public string VehicleMake { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
