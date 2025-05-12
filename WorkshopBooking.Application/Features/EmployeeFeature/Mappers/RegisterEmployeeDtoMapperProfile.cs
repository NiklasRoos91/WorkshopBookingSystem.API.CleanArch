using AutoMapper;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Mappers
{
    public class RegisterEmployeeDtoMapperProfile : Profile
    {
        public RegisterEmployeeDtoMapperProfile()
        {
            CreateMap<RegisterEmployeeDto, Employee>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle))

                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    Address = src.Address,
                    PhoneNumber = src.PhoneNumber,
                    Email = src.Email,
                    PasswordHash = src.Password,
                    Role = src.Role

                }));

            CreateMap<Employee, RegisterEmployeeDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle))

                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
