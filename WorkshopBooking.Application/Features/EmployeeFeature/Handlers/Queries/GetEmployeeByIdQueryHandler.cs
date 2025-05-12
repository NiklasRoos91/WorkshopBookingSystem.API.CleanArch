using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.EmployeeFeature.DTOs;
using WorkshopBooking.Application.Features.EmployeeFeature.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.EmployeeFeature.Handlers.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, OperationResult<EmployeeWithUserDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<EmployeeWithUserDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByIdAsync(request.EmployeeId);
                if (employee == null)
                {
                    return OperationResult<EmployeeWithUserDto>.Failure("Employee not found");
                }

                var employeeWithUserDto = _mapper.Map<EmployeeWithUserDto>(employee);

                return OperationResult<EmployeeWithUserDto>.Success(employeeWithUserDto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeWithUserDto>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
