using AutoMapper;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Mappers
{
    public class ServiceTypeDtoMapperProfile : Profile
    {
        public ServiceTypeDtoMapperProfile()
        {
            CreateMap<ServiceType, ServiceTypeDto>()
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.ToString(@"hh\:mm\:ss")));
        }
    }
}
