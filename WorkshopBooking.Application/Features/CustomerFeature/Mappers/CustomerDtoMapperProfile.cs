using AutoMapper;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.CustomerFeature.Mappers
{
    public class CustomerDtoMapperProfile : Profile
    {
        public CustomerDtoMapperProfile()
        {
            CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.VehicleMake, opt => opt.MapFrom(src => src.VehicleMake));

            CreateMap<CustomerDto, Customer>()
            .ForMember(dest=> dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.VehicleMake, opt => opt.MapFrom(src => src.VehicleMake));
        }
    }
}
