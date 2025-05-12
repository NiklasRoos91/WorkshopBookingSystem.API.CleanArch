using System.ComponentModel.DataAnnotations;
using WorkshopBooking.Domain.Enums;

namespace WorkshopBooking.Application.Features.EmployeeFeature.DTOs
{
    public class RegisterEmployeeDto : EmployeeDtoBase
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
    }
}
