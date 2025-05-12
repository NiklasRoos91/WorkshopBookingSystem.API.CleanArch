using System.ComponentModel.DataAnnotations;
using WorkshopBooking.Domain.Enums;

namespace WorkshopBooking.Application.Features.CustomerFeature.DTOs
{
    public class RegisterCustomerDto : CustomerDtoBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
