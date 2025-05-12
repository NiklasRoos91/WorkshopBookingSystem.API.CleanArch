using AutoMapper;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Enums;

namespace WorkshopBooking.Application.Features.CustomerFeature.Mappers
{
    public class RegisterCustomerDtoMapperProfile : Profile
    {
        public RegisterCustomerDtoMapperProfile()
        {
            CreateMap<RegisterCustomerDto, Customer>()
            .ForMember(dest => dest.VehicleMake, opt => opt.MapFrom(src => src.VehicleMake))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                Address = src.Address,
                PhoneNumber = src.PhoneNumber,
                Email = src.Email,
                PasswordHash = src.Password,
                Role = UserRole.Customer
            }));

            // Mappa från Customer till RegisterCustomerDto
            CreateMap<Customer, RegisterCustomerDto>()
                .ForMember(dest => dest.VehicleMake, opt => opt.MapFrom(src => src.VehicleMake))

                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
