using AutoMapper;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.BookingFeature.Mappers
{
    public class CreateBookingDtoMapperProfile : Profile
    {   
        public CreateBookingDtoMapperProfile()
        {
            CreateMap<CreateBookingDto, Booking>()
               .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
               .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
               .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
               .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
               .ForMember(dest => dest.BookingId, opt => opt.Ignore()) // skapas av databasen
               .ForMember(dest => dest.EmployeeId, opt => opt.Ignore()) // sätts i handler
               .ForMember(dest => dest.Employee, opt => opt.Ignore())
               .ForMember(dest => dest.Customer, opt => opt.Ignore())
               .ForMember(dest => dest.ServiceType, opt => opt.Ignore());
        }
    }
}
