using AutoMapper;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs.WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.CustomerFeature.Mappers
{
    public class UpdateCustomerWithUserDtoMapperProfile : Profile
    {
        public UpdateCustomerWithUserDtoMapperProfile()
        {
            CreateMap<Customer, UpdateCustomerWithUserDto>()
                .ForMember(dest => dest.VehicleMake, opt => opt.MapFrom(src => src.VehicleMake))

                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            CreateMap<UpdateCustomerWithUserDto, Customer>()
                .ForMember(dest => dest.VehicleMake, opt => opt.MapFrom(src => src.VehicleMake));

            CreateMap<UpdateCustomerWithUserDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
