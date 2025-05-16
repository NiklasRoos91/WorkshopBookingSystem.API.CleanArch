using AutoMapper;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.AvailableSlotFeature.Mappers
{
    public class AvailableSlotInputDtoMapperProfile : Profile
    {
        public AvailableSlotInputDtoMapperProfile()
        {
            CreateMap<AvailableSlotInputDto, AvailableSlot>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId));

            CreateMap<AvailableSlot, AvailableSlotInputDto>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId));
        }
    }
}
