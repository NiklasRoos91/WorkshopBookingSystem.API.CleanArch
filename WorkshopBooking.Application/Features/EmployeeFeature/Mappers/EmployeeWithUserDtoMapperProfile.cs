using AutoMapper;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Mappers
{
    public class EmployeeWithUserDtoMapperProfile : Profile
    {
        public EmployeeWithUserDtoMapperProfile()
        {
            CreateMap<Employee, EmployeeWithUserDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle))

                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
        }
    }
}
