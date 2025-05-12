using AutoMapper;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Mappers
{
    public class UpdateEmployeeWithUserDtoMapperProfile : Profile
    {
        public UpdateEmployeeWithUserDtoMapperProfile()
        {
            CreateMap<Employee, UpdateEmployeeWithUserDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle))

                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            CreateMap<UpdateEmployeeWithUserDto, Employee>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle));

            CreateMap<UpdateEmployeeWithUserDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
