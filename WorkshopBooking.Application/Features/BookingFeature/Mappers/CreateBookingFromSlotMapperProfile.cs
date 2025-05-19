using AutoMapper;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.BookingFeature.Mappers
{
    public class CreateBookingFromSlotMapperProfile : Profile
    {
        public CreateBookingFromSlotMapperProfile()
        {
            CreateMap<AvailableSlot, Booking>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.BookingId, opt => opt.Ignore()) // skapas av databasen
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore()) // sätts i handler
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceType, opt => opt.Ignore());
        }
    }
}
