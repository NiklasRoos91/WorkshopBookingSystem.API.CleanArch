using AutoMapper;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.ServiceTypeFeature.Mappers
{
    public class ServiceTypeInputDtoMapperProfile : Profile
    {
        public ServiceTypeInputDtoMapperProfile()
        {
            CreateMap<ServiceTypeInputDto, ServiceType>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration));
        }
    }
}
