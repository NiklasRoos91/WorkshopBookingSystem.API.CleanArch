using AutoMapper;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Mappers
{
    public class AvailableSlotDtoMapperProfile : Profile
    {
        public AvailableSlotDtoMapperProfile()
        {
            CreateMap<AvailableSlot, AvailableSlotDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));
        }
    }
}
