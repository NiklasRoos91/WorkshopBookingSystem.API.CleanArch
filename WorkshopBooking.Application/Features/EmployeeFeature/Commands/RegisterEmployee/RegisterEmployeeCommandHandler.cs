using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, OperationResult<EmployeeWithUserDto>>
    {
        private readonly IGenericInterface<Employee> _employeeRepository;
        private readonly IGenericInterface<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterEmployeeCommandHandler(
            IGenericInterface<Employee> employeeRepository,
            IGenericInterface<User> userRepository,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<OperationResult<EmployeeWithUserDto>> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = _mapper.Map<Employee>(request.RegisterEmployeeDto);

                employee.User.PasswordHash = _passwordHasher.HashPassword(employee.User, request.RegisterEmployeeDto.Password);

                await _userRepository.AddAsync(employee.User);
                await _employeeRepository.AddAsync(employee);

                var dto = _mapper.Map<EmployeeWithUserDto>(employee);

                return OperationResult<EmployeeWithUserDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeWithUserDto>.Failure($"An error occurred while creating employee: {ex.Message}");
            }
        }
    }
}
